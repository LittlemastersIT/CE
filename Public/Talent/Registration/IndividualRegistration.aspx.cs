using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using CE.Data;

namespace CE.Pages
{
    public partial class IndividualRegistrationPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
   //       Response.Redirect("TalentRegistration.aspx");
            if (!IsPostBack)
            {
                bool initialized = false;

                CompetitionCategoryList.DataTextField = "Key";
                CompetitionCategoryList.DataValueField = "Value";
                CompetitionDivisionList.DataTextField = "Key";
                CompetitionDivisionList.DataValueField = "Value";
                CompetitionClassOptions.DataTextField = "Key";
                CompetitionClassOptions.DataValueField = "Value";
                ContestantGradeList.DataTextField = "Key";
                ContestantGradeList.DataValueField = "Value";
                TalentShowSubCategory.Value = string.Empty;
                TalentShowSubCategoryList.Value = string.Join(",", CompetitionControlData.TalentShowTypes.ToArray());

                string payment = Request.QueryString["payment"];
                string division = Request.QueryString["division"];
                string categories = Request.QueryString["category"];
                string team = Request.QueryString["team"];
                if (payment == "1" || payment == "0")
                {
                    List<CECompetitionEntry> competitionEntries = (List<CECompetitionEntry>)Session[CEConstants.CE_PAYMENT_COOKIE_ID];
                    if (competitionEntries != null && competitionEntries.Count > 0)
                    {
                        initialized = true;
                        CompetitionDivisionList.Items.Clear();
                        CompetitionDivisionList.DataSource = CompetitionControlData.AllDivisions;
                        CompetitionDivisionList.DataBind();
                        CompetitionDivisionList.SelectedValue = division;
                        OnDivisionSelectionChanged(CompetitionDivisionList, null); // this will initialize division list

                        CompetitionClassOptions.Items.Clear();
                        CompetitionClassOptions.DataSource = CompetitionControlData.ValidClasses;
                        CompetitionClassOptions.DataBind();
                        CompetitionClassOptions.Visible = false;

                        ReloadCompetitionData(competitionEntries, categories);

                        if (payment == "1") // paypal successes
                        {
                            // we save the registration here in case the user forgot to click the completion button after Paypal returns
                            ExportAllCompetitionCategories(competitionEntries, 5.0, PaymentType.Paypal);

                            SendConfirmationEmail(competitionEntries, false, false);

                            // trigger the javascript function to go to the registration completion page
                            ScriptManager.RegisterStartupScript(
                                this,
                                this.GetType(),
                                "TalentRegistration",
                                "paymentReceived();",
                                true);

                        }
                        else if (payment == "0")// paypal cancelled or fails
                        {
                            RemoveTalentApplicants(competitionEntries);

                            // trigger the javascript function to go to the payment page
                            ScriptManager.RegisterStartupScript(
                                this,
                                this.GetType(),
                                "TalentRegistration",
                                "paymentCancel();",
                                true);
                        }
                    }
                }
                else if (payment == "2") // mailin redirection
                {
                    List<CECompetitionEntry> competitionEntries = (List<CECompetitionEntry>)Session[CEConstants.CE_FEEWAIVED_COOKIE_ID];
                    if (competitionEntries != null && competitionEntries.Count > 0)
                    {
                        initialized = true;

                        CompetitionDivisionList.SelectedValue = competitionEntries[0].Contact.Division;
                        OnDivisionSelectionChanged(CompetitionDivisionList, null);
                        SetSelectedCategories(categories);
                        SetSelectedClass(competitionEntries);
                        Session[CEConstants.CE_FEEWAIVED_COOKIE_ID] = null;
                    }
                }

                // disable payment buttons when the registration is closed for general users except for site admin
                bool hasCookie = false;
                string ceCookie = CEHelper.GetCookie(CEConstants.CE_ADMIN_COOKIE_ID, CEConstants.CE_ADMIN_COOKIE_NAME);
                if (ceCookie == CEConstants.CE_ADMIN_COOKIE_VALUE) hasCookie = true;

                string regisrationEndDate = CEHelper.GetConfiguration(CEConstants.REGISTRATION_ENDDATE_KEY, "01/01/2000");
                DateTime endDate;
                if (DateTime.TryParse(regisrationEndDate, out endDate) == true)
                {
                    if (endDate < DateTime.Now && !hasCookie)
                    {
                        Response.Redirect(CEConstants.CE_TALENT_COMPETITION_PAGE);
                        return;
                    }
                }

                if (!initialized)
                {
                    Session[CEConstants.CE_PAYMENT_COOKIE_ID] = null;
                    Session[CEConstants.CE_FEEWAIVED_COOKIE_ID] = null;

                    if (Session[CEConstants.CE_CHAMPION_COOKIE_ID] == null)
                        Session[CEConstants.CE_CHAMPION_COOKIE_ID] = (new CEChampionList()).Load();

                    CompetitionDivisionList.Items.Clear();
                    CompetitionDivisionList.DataSource = CompetitionControlData.AllDivisions;
                    CompetitionDivisionList.DataBind();
                     CompetitionDivisionList.SelectedIndex = 0;
                    OnDivisionSelectionChanged(CompetitionDivisionList, null); // this will initialize division list

                    CompetitionClassOptions.Items.Clear();
                    CompetitionClassOptions.DataSource = CompetitionControlData.ValidClasses;
                    CompetitionClassOptions.DataBind();
                    CompetitionClassOptions.Visible = false;

                    ContestantGradeList.DataSource = CompetitionControlData.K12Grades;
                    ContestantGradeList.DataBind();
                    ContestantGradeList.SelectedIndex = 0;

                    // if form data cookie presents, we initialize category/subcategory/class if they have values
                    HttpCookie cookie = Request.Cookies[CEConstants.CE_INDIVIDUAL_ENTRY_COOKIE_ID];
                    if (cookie != null)
                    {
                        string savedForm = cookie.Value;
                        savedForm = HttpUtility.UrlDecode(savedForm);
                        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                        IDictionary<string, object> savedFormJson = (IDictionary<string, object>)jsonSerializer.DeserializeObject(savedForm);
                        object formDivision = CEHelper.GetSafeDictionary(savedFormJson, "division");
                        object formCategory = CEHelper.GetSafeDictionary(savedFormJson, "category");
                        object formClass = CEHelper.GetSafeDictionary(savedFormJson, "class");

                        if (formCategory != null && ((object[])formCategory).Length > 0)
                        {
                            CompetitionDivisionList.SelectedValue = formDivision.ToString();
                            OnDivisionSelectionChanged(null, null);
                            if (formClass != null && formClass.ToString() != string.Empty) CompetitionClassOptions.Visible = true;
                        }
                    }
                }

                string regisrationDate = CEHelper.GetConfiguration(CEConstants.REGISTRATION_DATE_KEY);
                bool registrationStarted = false;
                DateTime startDate;
                if (DateTime.TryParse(regisrationDate, out startDate) == true) registrationStarted = startDate <= DateTime.Now;
                if (registrationStarted || hasCookie)
                {
                    FeeWaiverButton.Enabled = true;
                    PaypalPaymentButton.Enabled = true;
                    DoneRegistration.Enabled = true;
                    if (!hasCookie) RegistationStartFlag.Value = "1"; // admin can see all pages after registration has closed
                }
                else
                {
                    Response.Redirect(CEConstants.CE_TALENT_COMPETITION_PAGE);
                }
            }
        }

        protected void OnDivisionSelectionChanged(object sender, EventArgs e)
        {
            if (CompetitionDivisionList.SelectedValue == CompetitionControlData.LOWER_ELEMENTARY)
            {
                CompetitionCategoryList.DataSource = CompetitionControlData.LowerElementaryCategories;
                CompetitionCategoryList.DataBind();
                SyncContestantGradeList(CompetitionControlData.LOWER_ELEMENTARY);
            }
            else if (CompetitionDivisionList.SelectedValue == CompetitionControlData.UPPER_ELEMENTARY)
            {
                CompetitionCategoryList.DataSource = CompetitionControlData.UpperElementaryCategories;
                CompetitionCategoryList.DataBind();
                SyncContestantGradeList(CompetitionControlData.UPPER_ELEMENTARY);
            }
            else if (CompetitionDivisionList.SelectedValue == CompetitionControlData.MIDDLE_SCHOOL)
            {
                CompetitionCategoryList.DataSource = CompetitionControlData.MiddleSchoolCategories;
                CompetitionCategoryList.DataBind();
                SyncContestantGradeList(CompetitionControlData.MIDDLE_SCHOOL);
            }
            else if (CompetitionDivisionList.SelectedValue == CompetitionControlData.HIGH_SCHOOL)
            {
                CompetitionCategoryList.DataSource = CompetitionControlData.HighSchoolCategories;
                CompetitionCategoryList.DataBind();
                SyncContestantGradeList(CompetitionControlData.HIGH_SCHOOL);
            }
            else
            {
                CompetitionCategoryList.Items.Clear();
                SyncContestantGradeList(CompetitionControlData.ALL_GRADES);
            }

            CompetitionClassOptions.Visible = false;
            CompetitionClassOptions.ClearSelection();

            TotalCompetitionCost.Visible = false;
            TotalCompetitionCost.Text = string.Empty;

            string birthday = Request.Form[ContestantBirthday.UniqueID];
            if (!string.IsNullOrEmpty(birthday)) ContestantBirthday.Text = birthday;

            RebindJqueryControls(false);
        }

        protected void OnCategorySelectionChanged(object sender, EventArgs e)
        {
            bool showClass = false;
            int count = 0;
            bool talentShowSelected = false;
            foreach (ListItem item in CompetitionCategoryList.Items)
            {
                if (item.Selected)
                {
                    count++;
                    if (item.Value == CompetitionControlData.TEAM_TALENT) talentShowSelected = true;
                    if (item.Value == CompetitionControlData.SPEECH || item.Value == CompetitionControlData.POETRY || item.Value == CompetitionControlData.STORY)
                    {
                        showClass = true;
                    }
                }
            }

            if (showClass)
            {
                if (!CompetitionClassOptions.Visible) CompetitionClassOptions.ClearSelection();
                CompetitionClassOptions.Visible = true;
            }
            else
            {
                CompetitionClassOptions.Visible = false;
                CompetitionClassOptions.ClearSelection();
            }

            if (count == 0)
            {
                TotalCompetitionCost.Visible = false;
                TotalCompetitionCost.Text = string.Empty;
            }
            else
            {
                TotalCompetitionCost.Visible = true;
                TotalCompetitionCost.Text = "Total cost of selected competition categories is $" + (count * 5).ToString();
            }

            string birthday = Request.Form[ContestantBirthday.UniqueID];
            if (!string.IsNullOrEmpty(birthday)) ContestantBirthday.Text = birthday;

            RebindJqueryControls(talentShowSelected);
        }

        protected void OnCheckChampion(object sender, EventArgs e)
        {
            string birthday = Request.Form[ContestantBirthday.UniqueID];
            if (!string.IsNullOrEmpty(ContestantFirstName.Text) &&
                !string.IsNullOrEmpty(ContestantLastName.Text) &&
                !string.IsNullOrEmpty(birthday) &&
                !string.IsNullOrEmpty(CompetitionCategoryList.SelectedValue) &&
                !string.IsNullOrEmpty(CompetitionDivisionList.SelectedValue))
            {
                List<CEChampion> champions = (List<CEChampion>)Session[CEConstants.CE_CHAMPION_COOKIE_ID];
                string id = CEContestant.MakeID(ContestantFirstName.Text, ContestantLastName.Text, birthday);
                string illegalCategory = string.Empty;
                foreach (ListItem item in CompetitionCategoryList.Items)
                {
                    if (item.Selected)
                    {
                        if (CEChampionList.ChampionExist(champions, id, item.Value, CompetitionDivisionList.SelectedValue, ContestantGradeList.SelectedValue) == true)
                        {
                            if (!string.IsNullOrEmpty(illegalCategory)) illegalCategory += ",";
                            illegalCategory += item.Value;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(illegalCategory))
                {
                    string message = "You are not eligible to participate in the category '" + illegalCategory + "' you have won last year.";
                    InvalidRegistration(message);
                }
            }
        }

        protected void OnPaypalPayment(object sender, EventArgs e)
        {
            //Paypal querystring parameters reference: http://www.paypalobjects.com/IntegrationCenter/ic_std-variable-ref-buy-now.html

            string birthday = Request.Form[ContestantBirthday.UniqueID];
            List<CEChampion> champions = (List<CEChampion>)Session[CEConstants.CE_CHAMPION_COOKIE_ID];
            string id = CEContestant.MakeID(ContestantFirstName.Text, ContestantLastName.Text, birthday);

            List<CECompetitionEntry> competitionEntries = PopulateCompetitionEntries();
            bool duplicate = false;
            string existedCategory;
            foreach (CECompetitionEntry competitionEntry in competitionEntries)
            {
                string ceClass = CETalentApplciants.GetCategoryClass(competitionEntry);
                if (IsLastChampion(champions, competitionEntry.Contestants[0].ID, competitionEntry.Contact.Category, competitionEntry.Contact.Division, ceClass) == true) return;

                if (CETalentApplciants.TalentApplicantExist(competitionEntry, out existedCategory, true))
                {
                    ShowDuplicateEntry(existedCategory);
                    duplicate = true;
                    break;
                }
            }

            if (!duplicate)
            {
                Session[CEConstants.CE_PAYMENT_COOKIE_ID] = competitionEntries;
                Session[CEConstants.CE_FEEWAIVED_COOKIE_ID] = null;

                // save the user record first in case the user's Paypal session does not come back to CE site
                ExportAllCompetitionCategories(competitionEntries, 5.0, PaymentType.Pending);

                int categoryCount = 0;
                string responseUrlTemplate = "?payment={0}&team={1}&division={2}&category={3}&id={4}";
                string division = CompetitionDivisionList.SelectedValue;
                string categories = GetSelectedCategories(out categoryCount);

                string redirecturl = CEHelper.GetConfiguration("PaypalUrl", CEConstants.PAYPAL_PAYMENT_URL);
                redirecturl += "&business=" + CEHelper.GetConfiguration("PaypalBusiness", CEConstants.CE_PAYPAL_EMAIL);
                redirecturl += "&item_name=Registration Fee";
                redirecturl += "&item_number=" + id;
                redirecturl += "&amount=" + RegistrationFee.Value; // can't get PaymentAmountList selected value as it is disabled
                redirecturl += "&quantity=1";
                redirecturl += "&currency=USD";
                redirecturl += "&return=" + HttpContext.Current.Server.UrlEncode(CEHelper.GetPageUrl() + string.Format(responseUrlTemplate, "1", "0", division, categories, id));
                redirecturl += "&cancel_return=" + HttpContext.Current.Server.UrlEncode(CEHelper.GetPageUrl() + string.Format(responseUrlTemplate, "0", "0", division, categories, id));
                Response.Redirect(redirecturl);
            }
        }

        protected void OnFeeWaiverPayment(object sender, EventArgs e)
        {
            string birthday = Request.Form[ContestantBirthday.UniqueID];
            List<CEChampion> champions = (List<CEChampion>)Session[CEConstants.CE_CHAMPION_COOKIE_ID];
            string id = CEContestant.MakeID(ContestantFirstName.Text, ContestantLastName.Text, birthday);

            List<CECompetitionEntry> currentApplicants = PopulateCompetitionEntries();
            bool duplicate = false;
            string existedCategory;
            foreach (CECompetitionEntry currentApplicant in currentApplicants)
            {
                string ceClass = CETalentApplciants.GetCategoryClass(currentApplicant);
                if (IsLastChampion(champions, currentApplicant.Contestants[0].ID, currentApplicant.Contact.Category, currentApplicant.Contact.Division, ceClass) == true) return;

                if (CETalentApplciants.TalentApplicantExist(currentApplicant, out existedCategory, true))
                {
                    ShowDuplicateEntry(existedCategory);
                    duplicate = true;
                    break;
                }
            }

            if (!duplicate)
            {
                if (ExportAllCompetitionCategories(currentApplicants, 0.0, PaymentType.FeeWaived) == true)
                {
                    Session[CEConstants.CE_PAYMENT_COOKIE_ID] = null;
                    Session[CEConstants.CE_FEEWAIVED_COOKIE_ID] = currentApplicants;
                    SendConfirmationEmail(currentApplicants, true, true);
                }
            }
        }

        protected void OnCompetitionRegistration(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "TalentRegistration",
                "registrationCompleted();",
                true);
        }

        private void SyncContestantGradeList(string grade)
        {
            if (ContestantGradeList.Items.Count <= 0) return;

            int start = 1, end = ContestantGradeList.Items.Count - 1;
            for (int i = start; i <= end; i++)
            {
                ContestantGradeList.Items[i].Enabled = false;
            }

            string range;
            try
            {
                range = CompetitionControlData.GradeRange[grade];
                if (!string.IsNullOrEmpty(range))
                {
                    string[] tokens = range.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (tokens.Length == 2)
                    {
                        Int32.TryParse(tokens[0], out start);
                        Int32.TryParse(tokens[1], out end);
                    }
                }
            }
            catch
            {
                start = 1;
                end = ContestantGradeList.Items.Count - 1;
            }

            for (int i = start; i <= end; i++)
            {
                ContestantGradeList.Items[i].Enabled = true;
            }
        }

        private bool SendConfirmationEmail(List<CECompetitionEntry> currentApplicants, bool redirect, bool showDialog)
        {
            CECompetitionEntry currentApplicant = currentApplicants[0];

            bool emailSent = false;
            EmailInfo emailInfo = CEHelper.GetEmailConfiguration("talent", currentApplicant.Status.ToString().ToLower());
            if (emailInfo != null && !string.IsNullOrEmpty(emailInfo.Message))
            {
                int categoryCount = 0;
                string formattedCategories = GetSelectedCategories(out categoryCount, currentApplicant.Contact.SubCategory);
                StringBuilder sb = new StringBuilder(emailInfo.Message);
                sb.Replace("{%contact%}", currentApplicant.Contact.ContactName);
                sb.Replace("{%type%}", "Individual competition");
                sb.Replace("{%studentId%}", currentApplicant.Contestants[0].ID);
                sb.Replace("{%division%}", currentApplicant.Contact.Division);
                sb.Replace("{%category%}", formattedCategories);
                sb.Replace("{%class%}", CompetitionClassOptions.Visible ? CompetitionClassOptions.SelectedValue : "n/a");
                sb.Replace("{%contestants%}", MakeCotestantString(currentApplicant.Contestants));
                sb.Replace("{%payment%}", currentApplicant.PaymentMethod.ToString());
                if (currentApplicant.PaymentMethod == PaymentType.Paypal)
                    sb.Replace("{%amount%}", "$" + (currentApplicant.PaymentAmount * categoryCount).ToString());
                //else if (currentApplicant.PaymentMethod == PaymentType.MailIn)
                //    sb.Replace("{%amount%}", "$" + RegistrationFee.Value.ToString() + " (to be paid by mail-in check)");
                else 
                    sb.Replace("{%amount%}", "$0");
                CEHelper.SendEmail(emailInfo.SmtpHost, emailInfo.SmtpPort, emailInfo.Sender, new string[] { currentApplicant.Contact.ContactEmail }, emailInfo.CCs, emailInfo.Subject, sb.ToString(), emailInfo.IsHtml, null);
                emailSent = true;
            }

            if (emailSent && showDialog)
            {
                // trigger the javascript function to acknlowdge the completion of talent registration
                if (!redirect)
                {
                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "TalentRegistration",
                        "registrationSubmitted();",
                        true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "TalentRegistration",
                        "registrationCompleted();",
                        true);
                }
            }

            return emailSent;
        }

        private List<CECompetitionEntry> PopulateCompetitionEntries()
        {
            int categoryCount = 0;
            List<CECompetitionEntry> competitonEntries = new List<CECompetitionEntry>();
            string[] categories = GetSelectedCategories(out categoryCount).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string category in categories)
            {
                CECompetitionEntry competitionEntry = new CECompetitionEntry();
                competitionEntry.AddContact(ContactNameBox.Text, ContactEmailBox.Text, ContactPhoneBox.Text);
                competitionEntry.AddCompetition(category, CompetitionClassOptions.SelectedValue, CompetitionDivisionList.SelectedItem.Value, string.Empty, TalentShowSubCategory.Value);
                competitionEntry.AddStatus(RegistrationStatus.Apply);
                string lastName = ContestantLastName.Text.Trim();
                string firstName = ContestantFirstName.Text.Trim();
                string chineseName = ContestantChineseName.Text.Trim();
                string email = ContestantEmail.Text.Trim();
                string birthday = Request.Form[ContestantBirthday.UniqueID]; // birthday is a readonly field
                string school1 = ContestantSchool1.Text.Trim();
                string school2 = ContestantSchool2.Text.Trim();
                string grade = ContestantGradeList.SelectedValue;
                string lunchProgram = YesLunch.Checked ? "Yes" : "No";
                string id = CEContestant.MakeID(firstName, lastName, birthday);
                competitionEntry.AddContestant(1, id, firstName, lastName, chineseName, birthday, email, school1, school2, grade, lunchProgram);

                competitonEntries.Add(competitionEntry);
            }
            return competitonEntries;
        }

        private void ReloadCompetitionData(List<CECompetitionEntry> competitionEntries, string categories)
        {
            if (competitionEntries != null && competitionEntries.Count> 0)
            {
                CECompetitionEntry competitionEntry = competitionEntries[0];

                if (competitionEntry.Contact != null)
                {
                    ContactNameBox.Text = competitionEntry.Contact.ContactName;
                    ContactEmailBox.Text = competitionEntry.Contact.ContactEmail;
                    ContactPhoneBox.Text = competitionEntry.Contact.ContactPhone;
                    if (competitionEntry.Contestants.Count() > 0)
                    {
                        ContestantFirstName.Text = competitionEntry.Contestants[0].FirstName;
                        ContestantLastName.Text = competitionEntry.Contestants[0].LastName;
                        ContestantBirthday.Text = competitionEntry.Contestants[0].Birthday;
                        ContestantEmail.Text = competitionEntry.Contestants[0].Email;
                        ContestantSchool1.Text = competitionEntry.Contestants[0].School;
                        ContestantSchool2.Text = competitionEntry.Contestants[0].School2;
                        ContestantGradeList.SelectedValue = competitionEntry.Contestants[0].Grade;
                    }
                    CompetitionDivisionList.SelectedValue = competitionEntry.Contact.Division;
                }

                SetSelectedCategories(categories);
                SetSelectedClass(competitionEntries);
            }
        }

        private string MakeCotestantString(List<CEContestant> contestants)
        {
            string entries = string.Empty;
            foreach (CEContestant contestant in contestants)
            {
                if (string.IsNullOrEmpty(contestant.FirstName) && string.IsNullOrEmpty(contestant.LastName)) break;
                if (!string.IsNullOrEmpty(entries)) entries += ", ";
                entries += contestant.FirstName + " " + contestant.LastName;
            }
            return entries;
        }

        protected bool IsLastChampion(List<CEChampion> champions, string id, string category, string division, string ceClass)
        {
            if (CEChampionList.ChampionExist(champions, id, category, division, ceClass) == true)
            {
                string message = "You are not eligible to participate in the category '" + category + "' you have won last year.";
                InvalidRegistration(message);
                return true;
            }
            return false;
        }

        private void ShowDuplicateEntry(string existedCategory)
        {
            DuplicateCategory.Text = "The competition category '" + existedCategory + "' you filled out already exists for the contestant. Please make change and resumit it again.";
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "DuplicateRegistration",
                "duplicatedRegistration();",
                true);
        }

        private void InvalidRegistration(string message)
        {
            InvalidCategory.Text = message;
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "InvalidRegistration",
                "invalidRegistration();",
                true);
        }

        // comma sepetated string
        private string GetSelectedCategories(out int count, string subcategory = null)
        {
            count = 0;
            string selectedCategories = string.Empty;
            foreach (ListItem item in CompetitionCategoryList.Items)
            {
                if (item.Selected)
                {
                    if (selectedCategories != string.Empty) selectedCategories += ",";
                    if (subcategory != null)
                    {
                        selectedCategories += CEHelper.FormatCategory(item.Value, subcategory);
                    }
                    else
                    {
                        selectedCategories += item.Value;
                    }
                    count++;
                }
            }
            return selectedCategories;
        }

        private void SetSelectedCategories(string categories)
        {
            string[] selectedCategories = categories.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string category in selectedCategories)
            {
                if (CompetitionCategoryList.Items.FindByValue(category) != null)
                    CompetitionCategoryList.Items.FindByValue(category).Selected = true;
            }
        }

        private void SetSelectedClass(List<CECompetitionEntry> competitionEntires)
        {
            bool hasSelectedClass = false;
            foreach (CECompetitionEntry entry in competitionEntires)
            {
                if (!string.IsNullOrEmpty(entry.Contact.Class)) {
                    CompetitionClassOptions.SelectedValue = entry.Contact.Class;
                    hasSelectedClass = true;
                    break;
                }
            }
            if (hasSelectedClass)
                CompetitionClassOptions.Visible = true;
            else
                CompetitionClassOptions.Visible = false;
        }

        private bool ExportAllCompetitionCategories(List<CECompetitionEntry> currentApplicants, double amount, PaymentType payType)
        {
            bool exported = true;
            foreach (CECompetitionEntry currentApplicant in currentApplicants)
            {
                if (amount > 0) currentApplicant.PaymentAmount = amount;
                if (payType != PaymentType.None) currentApplicant.PaymentMethod = payType;
                currentApplicant.Contact.Class = CETalentApplciants.GetCategoryClass(currentApplicant);                
                if (currentApplicant.ExportRegistration(payType) != true)
                {
                    exported = false;
                }
            }
            return exported;
        }

        private void RemoveTalentApplicants(List<CECompetitionEntry> competitionEntires)
        {
            foreach (CECompetitionEntry competitionEntry in competitionEntires)
            {
                CETalentApplciants.RemoveTalentApplicant(competitionEntry.ContestantFile);
            }
        }

        private void RebindJqueryControls(bool talentShowSelected)
        {
            ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "RebindJqueryControls",
                        "rebindJqueryControls(" + (talentShowSelected ? "1" : "0") + ");",
                        true);
        }
    }

}