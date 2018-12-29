using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CE.Data;
using CE.Content;

namespace CE.Pages
{
    public partial class ReviewRegistrationPage : System.Web.UI.Page
    {
        private const string PAGER_HTML_BEGIN_TEMPLATE = "<span>Page </span><ul>";
        private const string PAGER_HTML_ITEM_TEMPLATE = "<li>{0}</li>";
        private const string PAGER_HTML_END_TEMPLATE = "</ul>";
        private const string IMAGE_LINK_TEMPLATE = "<a href=\"javascript:loadApplicantData({0},'{1}');\"><img class=\"search-grid-icon\" src=\"{2}\" /></a>";
        private const int GRID_STATUS_COLUMN = 0;
        private const int GRID_DATE_COLUMN = 1;
        private const int GRID_DIVISION_COLUMN = 5;
        private const int GRID_TEAM_NAME_COLUMN = 6;
        private const int GRID_METHOD_COLUMN = 8;
        private const int GRID_PAYMENT_COLUMN = 9;
        private const int GRID_LUNCH_COLUMN = 10;
        private const int GRID_ID_COLUMN = 11; // this is the File cell
        private const int GRID_STUDENT_COLUMN = 12; // this is the indiviudal contestant name column
        private const int GRID_CLASS_COLUMN = 13;
        private const int GRID_PAGE_SIZE = 20;
        private const string ALL_SELECTION_TEXT = "All";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CategoryList.DataTextField = "Key";
                CategoryList.DataValueField = "Value";
                DivisionList.DataTextField = "Key";
                DivisionList.DataValueField = "Value";
                ClassList.DataTextField = "Key";
                ClassList.DataValueField = "Value";

                Session[CEConstants.CE_REVIEW_MASTER_RESULT_ID] = null; // start over
                CETalentApplciants talentApplications = new CETalentApplciants(false);
                List<RegisteredApplicant> registeredApplicants = talentApplications.CreateRegisteredApplicants();
                registeredApplicants = registeredApplicants.OrderByDescending(s => s.EntryDate).ToList();
                Session[CEConstants.CE_REVIEW_MASTER_RESULT_ID] = registeredApplicants;
                Session[CEConstants.CE_REVIEW_SEARCH_RESULT_ID] = registeredApplicants;
                RebindGrid(registeredApplicants);

                // initialize dropdown list to default selection
                TeamCompetition.Checked = false;
                IndivisualCompetition.Checked = false;
                CategoryList.DataSource = CompetitionControlData.EmptySelection;
                CategoryList.DataBind();
                CategoryList.Items[0].Text = ALL_SELECTION_TEXT;
                DivisionList.DataSource = CompetitionControlData.EmptySelection;
                DivisionList.DataBind();
                DivisionList.Items[0].Text = ALL_SELECTION_TEXT;
                ClassList.DataSource = CompetitionControlData.EmptySelection;
                ClassList.DataBind();
                ClassList.Items[0].Text = ALL_SELECTION_TEXT;
            }
        }

        protected void OnTeamCompetition(object sender, EventArgs e)
        {
            CategoryList.DataSource = CompetitionControlData.TeamCategories;
            CategoryList.DataBind();
            CategoryList.Items[0].Text = ALL_SELECTION_TEXT;
            CategoryList.SelectedIndex = 0;
            DivisionList.DataSource = CompetitionControlData.EmptySelection;
            DivisionList.DataBind();
            DivisionList.Items[0].Text = ALL_SELECTION_TEXT;
            ClassList.DataSource = CompetitionControlData.EmptySelection;
            ClassList.DataBind();
            ClassList.Items[0].Text = ALL_SELECTION_TEXT;
        }

        protected void OnIndividualCompetition(object sender, EventArgs e)
        {
            CategoryList.DataSource = CompetitionControlData.IndividualCategories;
            CategoryList.DataBind();
            CategoryList.Items[0].Text = ALL_SELECTION_TEXT;
            CategoryList.SelectedIndex = 0;
            DivisionList.DataSource = CompetitionControlData.EmptySelection;
            DivisionList.DataBind();
            DivisionList.Items[0].Text = ALL_SELECTION_TEXT;
            ClassList.DataSource = CompetitionControlData.EmptySelection;
            ClassList.DataBind();
            ClassList.Items[0].Text = ALL_SELECTION_TEXT;
        }

        protected void OnCategorySelectionChanged(object sender, EventArgs e)
        {
            // default to all division
            DivisionList.DataSource = CompetitionControlData.AllDivisions;
            DivisionList.DataBind();
            ClassList.DataSource = CompetitionControlData.EmptySelection;
            ClassList.DataBind();

            if (CategoryList.SelectedValue == "TeamBowl")
            {
                DivisionList.DataSource = CompetitionControlData.HighSchoolDivisions;
                DivisionList.DataBind();
                DivisionList.SelectedIndex = 0;
            }
            else if (CategoryList.SelectedValue == "TeamPoetry")
            {
                DivisionList.SelectedIndex = 0;
            }
            else if (CategoryList.SelectedValue == "Poetry") // individual poetry
            {
                DivisionList.DataSource = CompetitionControlData.LowerElementaryDivisions;
                DivisionList.DataBind();
                DivisionList.SelectedIndex = 0;
                ClassList.DataSource = CompetitionControlData.AllClasses;
                ClassList.DataBind();
                ClassList.SelectedIndex = 0;
            }
            else if (CategoryList.SelectedValue == "Story") // individual story telling
            {
                DivisionList.DataSource = CompetitionControlData.UpperElementaryDivisions;
                DivisionList.DataBind();
                DivisionList.SelectedIndex = 0;
                ClassList.DataSource = CompetitionControlData.AllClasses;
                ClassList.DataBind();
                ClassList.SelectedIndex = 0;
            }
            else if (CategoryList.SelectedValue == "Speech") // individual public speech
            {
                DivisionList.DataSource = CompetitionControlData.HighSchoolDivisions;
                DivisionList.DataBind();
                DivisionList.SelectedIndex = 0;
                ClassList.DataSource = CompetitionControlData.AllClasses;
                ClassList.DataBind();
                ClassList.SelectedIndex = 0;
            }
            DivisionList.Items[0].Text = ALL_SELECTION_TEXT;
            ClassList.Items[0].Text = ALL_SELECTION_TEXT;
        }

        protected void OnSearchApplicants(object sender, EventArgs e)
        {
            List<RegisteredApplicant> allApplicants = (List<RegisteredApplicant>)Session[CEConstants.CE_REVIEW_MASTER_RESULT_ID];
            if (allApplicants != null && allApplicants.Count > 0)
            {
                bool isTeam = TeamCompetition.Checked ? true : false;
                bool isIndividual = IndivisualCompetition.Checked ? true : false;
                string category = CategoryList.SelectedValue;
                string division = DivisionList.SelectedValue;
                string ceClass = ClassList.SelectedValue;

                RegistrationStatus status = RegistrationStatus.Unknown;
                if (!string.IsNullOrEmpty(StatusList.SelectedValue))
                {
                    status = (RegistrationStatus)Enum.Parse(typeof(RegistrationStatus), StatusList.SelectedValue, true);
                }

                List<RegisteredApplicant> foundApplicants = new List<RegisteredApplicant>();

                foreach (RegisteredApplicant applicant in allApplicants)
                {
                    if ((string.IsNullOrEmpty(category) || string.Compare(applicant.Category, category, true) == 0) &&
                        (string.IsNullOrEmpty(division) || string.Compare(applicant.Division, division, true) == 0) &&
                        (string.IsNullOrEmpty(ceClass) || string.Compare(applicant.Class, ceClass, true) == 0) &&
                        ((!isTeam && !isIndividual) || applicant.IsTeam == isTeam) &&
                        (status == RegistrationStatus.Unknown || status == applicant.Status))
                    {
                        foundApplicants.Add(applicant);
                    }
                }

                // bidn the search result
                foundApplicants = foundApplicants.OrderByDescending(s => s.EntryDate).ToList();
                Session[CEConstants.CE_REVIEW_SEARCH_RESULT_ID] = foundApplicants;
                RebindGrid(foundApplicants);
            }
        }

        protected void OnExcelExport(object sender, EventArgs e)
        {
            if (CEExport.ExportTalentRegistation(Response) == false)
            {
                string title = "Excel Export Problem";
                string message = "There is problem exporting talent competition registration to Excel file. Please try again later. If the problem persists, please contact site administrator.";
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "exportFail",
                    "operationFail('" + title + "', '" + message + "');",
                    true);
            }
        }

        // this event style the cell dislay to whatever we want it to be. we can put any html code in it
        protected void OnGridRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string method = e.Row.Cells[GRID_METHOD_COLUMN].Text;
                string date = e.Row.Cells[GRID_DATE_COLUMN].Text;
                if (date.Length < 8)
                    e.Row.Cells[GRID_DATE_COLUMN].Text = "";
                else
                    e.Row.Cells[GRID_DATE_COLUMN].Text = date.Substring(0, date.LastIndexOf('/'));
                string status = e.Row.Cells[GRID_STATUS_COLUMN].Text;
                if (!string.IsNullOrEmpty(status))
                {
                    string imageUrl = GetImage(status, method);
                    e.Row.Cells[GRID_STATUS_COLUMN].Text = string.Format(IMAGE_LINK_TEMPLATE, e.Row.RowIndex.ToString(), e.Row.Cells[GRID_ID_COLUMN].Text, imageUrl);
                }

                // append class to division
                if (!IsCellTextEmpty(e.Row.Cells[GRID_CLASS_COLUMN].Text))
                {
                    e.Row.Cells[GRID_DIVISION_COLUMN].Text += " (" + e.Row.Cells[GRID_CLASS_COLUMN].Text + ")";
                    e.Row.Cells[GRID_DIVISION_COLUMN].Text = e.Row.Cells[GRID_DIVISION_COLUMN].Text.Replace("(Class", "(");
                }

                // use teamname column for individual contestant name if it is a individual competition
                if (IsCellTextEmpty(e.Row.Cells[GRID_TEAM_NAME_COLUMN].Text))
                {
                    e.Row.Cells[GRID_TEAM_NAME_COLUMN].Text = e.Row.Cells[GRID_STUDENT_COLUMN].Text;
                }

                // add $ to payment column
                e.Row.Cells[GRID_PAYMENT_COLUMN].Text = "$" + e.Row.Cells[GRID_PAYMENT_COLUMN].Text;
                e.Row.Cells[GRID_PAYMENT_COLUMN].HorizontalAlign = HorizontalAlign.Right;
            }
        }

        protected void SearchResultSorting(object sender, GridViewSortEventArgs e)
        {
            List<RegisteredApplicant> applicants = (List<RegisteredApplicant>)Session[CEConstants.CE_REVIEW_SEARCH_RESULT_ID];

            if (applicants != null)
            {
                if (e.SortExpression == "EntryDate")
                {
                    if (DateSortDirection.Value == "1")
                        applicants = applicants.OrderByDescending(s => s.EntryDate).ToList();
                    else
                        applicants = applicants.OrderBy(s => s.EntryDate).ToList();
                    DateSortDirection.Value = DateSortDirection.Value == "0" ? "1" : "0";
                }
                else if (e.SortExpression == "ContactName")
                {
                    if (ContactSortDirection.Value == "1")
                        applicants = applicants.OrderByDescending(s => s.ContactName).ToList();
                    else
                        applicants = applicants.OrderBy(s => s.ContactName).ToList();
                    ContactSortDirection.Value = ContactSortDirection.Value == "0" ? "1" : "0";
                }
                else if (e.SortExpression == "Division")
                {
                    if (DivisionSortDirection.Value == "1")
                        applicants = applicants.OrderByDescending(s => s.Division + s.Class).ToList();
                    else
                        applicants = applicants.OrderBy(s => s.Division + s.Class).ToList();
                    DivisionSortDirection.Value = DivisionSortDirection.Value == "0" ? "1" : "0";
                }
                else
                    return;

                Session[CEConstants.CE_REVIEW_SEARCH_RESULT_ID] = applicants;
                RebindGrid();
            }
        }

        // thsi event can create any control for pager; not use it right now
        protected void OnCreatePagerRow(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
            }
        }

        // this event handle pagination
        protected void OnApplicationPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SearchResultGrid.PageIndex = e.NewPageIndex;
            RebindGrid();
        }

        protected void OnPostbackAction(object sender, EventArgs e)
        {
            string filename = GetFilenameFromPostbackAction();
            CECompetitionEntry talentApplicant = new CECompetitionEntry(MakeFullXmlPath(filename));
            BindApplicantData(talentApplicant);

            // trigger the javascript function to bring up the review modal dialog
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "reviewDialog",
                "showReviewDialog();",
                true);

        }

        protected void OnCancel(object sender, EventArgs e)
        {
            Response.Redirect(CEConstants.CE_ADMIN_PAGE);
        }

        protected void OnStatusChanged(object sender, EventArgs e)
        {
            int rowIndex = GetRowIndexFromPostackAction();
            int actualRowIndex = GetAbsoluteRowIndex(rowIndex);
            RegistrationStatus status = GetRegistrationStatus();

            if (status == RegistrationStatus.Approved)
            {
                OnAwardTrophy(sender, e);
                return;
            }

            // change the session cache
            List<RegisteredApplicant> registeredApplicants = (List<RegisteredApplicant>)Session[CEConstants.CE_REVIEW_SEARCH_RESULT_ID];
            if (registeredApplicants != null && actualRowIndex >= 0 && actualRowIndex < registeredApplicants.Count<RegisteredApplicant>())
            {
                RegisteredApplicant registeredApplicant = registeredApplicants[actualRowIndex];

                // update applicant if mail-in payment is received
                bool paymentReceived = false;
                bool paymentVerified = false;
                if (MailInCheckBox.Checked && !string.IsNullOrEmpty(MailInBox.Text))
                {
                    double payment = 0;
                    if (double.TryParse(MailInBox.Text, out payment) == true && payment > 0)
                    {
                        //registeredApplicant.PaymentMethod = PaymentType.MailIn;
                        registeredApplicant.Payment += payment;
                        paymentReceived = true;
                    }
                    registeredApplicant.CheckNumber = CheckNumberBox.Text;
                }
                else if (PaypalCheckBox.Checked)
                {
                    registeredApplicant.PaymentMethod = PaymentType.Paypal;
                    paymentVerified = true;
                }

                // update xml file
                CECompetitionEntry currentApplicant = new CECompetitionEntry(MakeFullXmlPath(registeredApplicant.File));
                if (paymentReceived)
                {
                    //currentApplicant.PaymentMethod = PaymentType.MailIn;
                    currentApplicant.PaymentAmount = registeredApplicant.Payment;
                    currentApplicant.CheckNumber = registeredApplicant.CheckNumber;

                }
                else if (paymentVerified)
                {
                    currentApplicant.PaymentMethod = PaymentType.Paypal;
                }

                RegistrationStatus oldStatus = currentApplicant.Status;
                if (status == RegistrationStatus.Reminder)
                {
                    // change payment type to Mial-in for late payment
                    //currentApplicant.PaymentMethod = PaymentType.MailIn;
                    currentApplicant.PaymentAmount = 0;
                    currentApplicant.CheckNumber = string.Empty;
                }
                if (currentApplicant.ExportRegistration(status) == true)
                {
                    // send an email to applicant if it is not review, pending, or apply status
                    bool emailSent = true;
                    bool needEmail = false;
                    if (paymentVerified)
                    {
                        emailSent = false;
                        EmailInfo emailInfo = CEHelper.GetEmailConfiguration("talent", RegistrationStatus.Apply.ToString().ToLower());
                        if (emailInfo != null && !string.IsNullOrEmpty(emailInfo.Message))
                        {
                            StringBuilder sb = new StringBuilder(emailInfo.Message);
                            sb.Replace("{%contact%}", currentApplicant.Contact.ContactName);
                            sb.Replace("{%type%}", currentApplicant.Contact.IsTeam ? ("Team competition; name = " + currentApplicant.Contact.TeamName) : "Individual competition");
                            sb.Replace("{%studentId%}", (currentApplicant.Contact.IsTeam ? currentApplicant.Contact.TeamName : currentApplicant.Contestants[0].ID));
                            sb.Replace("{%category%}", GetDictionaryKeyFromValue(CompetitionControlData.AllCategories, currentApplicant.Contact.Category));
                            sb.Replace("{%division%}", GetDictionaryKeyFromValue(CompetitionControlData.AllDivisions, currentApplicant.Contact.Division));
                            sb.Replace("{%class%}", string.IsNullOrEmpty(currentApplicant.Contact.Class) ? "n/a" : currentApplicant.Contact.Class);
                            sb.Replace("{%contestants%}", MakeCotestantString(currentApplicant.Contestants));
                            sb.Replace("{%payment%}", currentApplicant.PaymentMethod.ToString());
                            sb.Replace("{%amount%}", "$" + currentApplicant.PaymentAmount.ToString());
                            CEHelper.SendEmail(emailInfo.SmtpHost, emailInfo.SmtpPort, emailInfo.Sender, new string[] { currentApplicant.Contact.ContactEmail }, emailInfo.CCs, emailInfo.Subject, sb.ToString(), emailInfo.IsHtml, null);
                            emailSent = true;
                        }
                    }
                    else if (currentApplicant.Status == RegistrationStatus.Reminder)
                    {
                        emailSent = false;
                        EmailInfo emailInfo = CEHelper.GetEmailConfiguration("talent", RegistrationStatus.Reminder.ToString().ToLower());
                        if (emailInfo != null && !string.IsNullOrEmpty(emailInfo.Message))
                        {
                            StringBuilder sb = new StringBuilder(emailInfo.Message);
                            sb.Replace("{%contact%}", currentApplicant.Contact.ContactName);
                            sb.Replace("{%type%}", currentApplicant.Contact.IsTeam ? ("Team competition; name = " + currentApplicant.Contact.TeamName) : "Individual competition");
                            sb.Replace("{%studentId%}", (currentApplicant.Contact.IsTeam ? currentApplicant.Contact.TeamName : currentApplicant.Contestants[0].ID));
                            sb.Replace("{%category%}", GetDictionaryKeyFromValue(CompetitionControlData.AllCategories, currentApplicant.Contact.Category));
                            sb.Replace("{%division%}", GetDictionaryKeyFromValue(CompetitionControlData.AllDivisions, currentApplicant.Contact.Division));
                            sb.Replace("{%class%}", string.IsNullOrEmpty(currentApplicant.Contact.Class) ? "n/a" : currentApplicant.Contact.Class);
                            sb.Replace("{%contestants%}", MakeCotestantString(currentApplicant.Contestants));
                            CEHelper.SendEmail(emailInfo.SmtpHost, emailInfo.SmtpPort, emailInfo.Sender, new string[] { currentApplicant.Contact.ContactEmail }, emailInfo.CCs, emailInfo.Subject, sb.ToString(), emailInfo.IsHtml, null);
                            emailSent = true;
                        }
                    }
                    else if (currentApplicant.Status == RegistrationStatus.Approved || currentApplicant.Status == RegistrationStatus.Rejected)
                    {
                        needEmail = true;
                        emailSent = false;
                        EmailInfo emailInfo = null;
                        emailInfo = CEHelper.GetEmailConfiguration("talent", currentApplicant.Status.ToString().ToLower());

                        if (emailInfo != null && !string.IsNullOrEmpty(emailInfo.Message))
                        {
                            StringBuilder sb = new StringBuilder(emailInfo.Message);
                            sb.Replace("{%contact%}", currentApplicant.Contact.ContactName);
                            sb.Replace("{%type%}", currentApplicant.Contact.IsTeam ? ("Team competition; name = " + currentApplicant.Contact.TeamName) : "Individual competition");
                            sb.Replace("{%studentId%}", (currentApplicant.Contact.IsTeam ? currentApplicant.Contact.TeamName : currentApplicant.Contestants[0].ID));
                            sb.Replace("{%category%}", GetDictionaryKeyFromValue(CompetitionControlData.AllCategories, currentApplicant.Contact.Category));
                            sb.Replace("{%division%}", GetDictionaryKeyFromValue(CompetitionControlData.AllDivisions, currentApplicant.Contact.Division));
                            sb.Replace("{%class%}", string.IsNullOrEmpty(currentApplicant.Contact.Class) ? "n/a" : GetDictionaryKeyFromValue(CompetitionControlData.AllClasses, currentApplicant.Contact.Class));
                            sb.Replace("{%contestants%}", MakeCotestantString(currentApplicant.Contestants));
                            CEHelper.SendEmail(emailInfo.SmtpHost, emailInfo.SmtpPort, emailInfo.Sender, new string[] { currentApplicant.Contact.ContactEmail }, emailInfo.CCs, emailInfo.Subject, sb.ToString(), emailInfo.IsHtml, null);
                            emailSent = true;
                        }
                    }

                    if (emailSent)
                    {
                        // update session cache
                        registeredApplicant.Status = status;
                        registeredApplicant.Payment = currentApplicant.PaymentAmount;
                        registeredApplicant.PaymentMethod = currentApplicant.PaymentMethod;
                        Session[CEConstants.CE_REVIEW_SEARCH_RESULT_ID] = registeredApplicants;
                        RebindGrid();

                        // update master cache
                        UpdateMasterCache(registeredApplicant);

                        string message = string.Empty;
                        if (!needEmail) message = "The applicant status has been changed.";
                        // trigger the javascript function to acknowledge the change of the applicant status
                        ScriptManager.RegisterStartupScript(
                            this,
                            this.GetType(),
                            "statusUpdated",
                            "statusUpdated('" + message + "');",
                            true);
                    }
                    else
                    {
                        currentApplicant.ExportRegistration(oldStatus); // revert the change if email has error
                        string title = "Email Sending Problem";
                        string message = "There is problem sending email to the application contact. Please try again later. If the problem persists, please contact site administrator.";
                        ScriptManager.RegisterStartupScript(
                            this,
                            this.GetType(),
                            "emailFail",
                            "operationFail('" + title + "', '" + message + "');",
                            true);
                    }
                }
            }
        }

        protected void OnAwardTrophy(object sender, EventArgs e)
        {
            int rowIndex = GetRowIndexFromPostackAction();
            int actualRowIndex = GetAbsoluteRowIndex(rowIndex);
            AwardType award = GetAwardPlace();

            if (award == AwardType.Unknown) return;

            // change the session cache
            List<RegisteredApplicant> registeredApplicants = (List<RegisteredApplicant>)Session[CEConstants.CE_REVIEW_SEARCH_RESULT_ID];
            if (registeredApplicants != null && actualRowIndex >= 0 && actualRowIndex < registeredApplicants.Count<RegisteredApplicant>())
            {
                RegisteredApplicant registeredApplicant = registeredApplicants[actualRowIndex];
                registeredApplicant.Award = award;
                CECompetitionEntry currentApplicant = new CECompetitionEntry(MakeFullXmlPath(registeredApplicant.File));
                currentApplicant.ExportRegistration(RegistrationStatus.Unknown, award);
            }
        }

        private RegistrationStatus GetRegistrationStatus()
        {
            RegistrationStatus status = RegistrationStatus.Apply;
            if (ApprovedOption.Checked)
                status = RegistrationStatus.Approved;
            else if (RejectOption.Checked)
                status = RegistrationStatus.Rejected;
            else if (PendingOption.Checked)
                status = RegistrationStatus.Pending;
            else if (ReviewOption.Checked)
                status = RegistrationStatus.Review;
            else if (ReminderOption.Checked)
                status = RegistrationStatus.Reminder;
            else
                status = RegistrationStatus.Apply;

            return status;
        }

        private AwardType GetAwardPlace()
        {
            AwardType award = AwardType.None;
            if (FirstPlace.Checked)
                award = AwardType.FirstPlace;
            else if (SecondPlace.Checked)
                award = AwardType.SecondPlace;
            else if (ThirdPlace.Checked)
                award = AwardType.ThirdPlace;
            else if (HonorableMentioned.Checked)
                award = AwardType.HonorableMentioned;
            else if (RemoveTrophy.Checked)
                award = AwardType.None;
            else
                award = AwardType.Unknown;

            return award;
        }

        private void BindApplicantData(CECompetitionEntry applicant)
        {
            EntryDateBox.Text = "Registration entry timestamp: " + applicant.EntryDate;
            Applicant.Text = applicant.Contact.ContactName;
            ContactNameBox.Text = applicant.Contact.ContactName;
            ContactEmailBox.Text = applicant.Contact.ContactEmail;
            ContactPhoneBox.Text = applicant.Contact.ContactPhone;
            CompetitionCategory.Text = applicant.Contact.Category;
            CompetitionClass.Text = applicant.Contact.Class;
            CompetitionDivision.Text = applicant.Contact.Division;
            if (!string.IsNullOrEmpty(applicant.Contact.TeamName))
            {
                CompetitionType.Text = "(Team; name = " + applicant.Contact.TeamName + ")";
            }
            else
            {
                CompetitionType.Text = "(Individual)";
            }

            Award.Text = applicant.Award.ToString();

            PaymentMethodBox.Text = applicant.PaymentMethod.ToString();
            CheckNumberBox.Text = applicant.CheckNumber;
            MailInCheckBox.Checked = false;
            PaypalCheckBox.Checked = false;
            PaypalPaymentCheck.Attributes["style"] = "display:none;";
            MailInPayment.Attributes["style"] = "display:none;";
            double requiredAmount = GetRequiredPayment(applicant);
            if (applicant.PaymentMethod == PaymentType.Pending)
            {
                PaymentMethodBox.Text += "  (amount of $" + requiredAmount.ToString() + " to be verified with Paypal)";
                PaypalPaymentCheck.Attributes["style"] = "display:block;";
            }
            else
            {
                if (requiredAmount == 0)
                {
                    PaymentMethodBox.Text += "  (registration fee is waived.)";
                }
                else if (applicant.PaymentAmount > 0)
                {
                    if (applicant.PaymentAmount < requiredAmount)
                    {
                        MailInPayment.Attributes["style"] = "display:block;";
                        PaymentMethodBox.Text += "  (amount of $" + applicant.PaymentAmount.ToString() + " was received; required amount is $" + requiredAmount.ToString() + ")";
                    }
                    else if (string.IsNullOrEmpty(applicant.CheckNumber))
                    {
                        PaymentMethodBox.Text += "  (amount of $" + applicant.PaymentAmount.ToString() + " was received; required amount is $" + requiredAmount.ToString() + ")";
                    }
                    else
                    {
                        PaymentMethodBox.Text += "  (check #" + applicant.CheckNumber + " for $" + applicant.PaymentAmount.ToString() + " was received; required amount is $" + requiredAmount.ToString() + ")";
                    }
                }
                else
                {
                    PaymentMethodBox.Text += "  (no payment was received; required amount is $" + requiredAmount.ToString() + ")";
                    MailInPayment.Attributes["style"] = "display:block;";
                }
            }

            TalentParticipants.DataSource = applicant.Contestants;
            TalentParticipants.DataBind();

            InitReviewStatus(applicant);
            SetReviewStatus(applicant.Status, applicant.Award);
        }

        private void InitReviewStatus(CECompetitionEntry applicant)
        {
            ApplyOption.Enabled = true;
            ReviewOption.Enabled = true;
            ApprovedOption.Enabled = true;
            RejectOption.Enabled = true;
            PendingOption.Enabled = true;
            ReminderOption.Enabled = true;
            ApplyOption.CssClass = "cell-button";
            ReviewOption.CssClass = "cell-button";
            ApprovedOption.CssClass = "cell-button";
            RejectOption.CssClass = "cell-button";
            PendingOption.CssClass = "cell-button";
            ReminderOption.CssClass = "cell-button";


            if (CEHelper.GetConfiguration(CEConstants.REVIEW_BACKFLOW_KEY, "0") == "0")
            {
                if (applicant.PaymentAmount > 0)
                {
                    PendingOption.Enabled = false;
                    PendingOption.CssClass = "cell-button inactive";
                }
                else
                {
                    PendingOption.Enabled = true;
                    PendingOption.CssClass = "cell-button";
                }

                switch (applicant.Status)
                {
                    case RegistrationStatus.Pending:
                        break;
                    case RegistrationStatus.Apply:
                        PendingOption.Enabled = false;
                        PendingOption.CssClass = "cell-button inactive";
                        break;
                    case RegistrationStatus.Reminder:
                        ApplyOption.Enabled = false;
                        PendingOption.Enabled = false;
                        ApplyOption.CssClass = "cell-button inactive";
                        PendingOption.CssClass = "cell-button inactive";
                        break;
                    case RegistrationStatus.Review:
                        ApplyOption.Enabled = false;
                        PendingOption.Enabled = false;
                        ReminderOption.Enabled = false;
                        ApplyOption.CssClass = "cell-button inactive";
                        PendingOption.CssClass = "cell-button inactive";
                        ReminderOption.CssClass = "cell-button inactive";
                        break;
                    case RegistrationStatus.Approved:
                        ApplyOption.Enabled = false;
                        PendingOption.Enabled = false;
                        ReminderOption.Enabled = false;
                        ReviewOption.Enabled = false;
                        RejectOption.Enabled = false;
                        ApplyOption.CssClass = "cell-button inactive";
                        PendingOption.CssClass = "cell-button inactive";
                        ReminderOption.CssClass = "cell-button inactive";
                        ReviewOption.CssClass = "cell-button inactive";
                        RejectOption.CssClass = "cell-button inactive";
                        break;
                    case RegistrationStatus.Rejected:
                        ApplyOption.Enabled = false;
                        PendingOption.Enabled = false;
                        ReminderOption.Enabled = false;
                        ReviewOption.Enabled = false;
                        ApprovedOption.Enabled = false;
                        ApplyOption.CssClass = "cell-button inactive";
                        PendingOption.CssClass = "cell-button inactive";
                        ReminderOption.CssClass = "cell-button inactive";
                        ReviewOption.CssClass = "cell-button inactive";
                        ApprovedOption.CssClass = "cell-button inactive";
                        break;
                    default:
                        break;
                }
            }
        }

        private void SetReviewStatus(RegistrationStatus status, AwardType award = AwardType.None)
        {
            ApplyOption.Checked = false;
            PendingOption.Checked = false;
            ReminderOption.Checked = false;
            ReviewOption.Checked = false;
            RejectOption.Checked = false;
            ApprovedOption.Checked = false;
            switch (status)
            {
                case RegistrationStatus.Apply:
                    ApplyOption.Checked = true;
                    break;
                case RegistrationStatus.Review:
                    ReviewOption.Checked = true;
                    break;
                case RegistrationStatus.Approved:
                    ApprovedOption.Checked = true;
                    break;
                case RegistrationStatus.Rejected:
                    RejectOption.Checked = true;
                    break;
                case RegistrationStatus.Pending:
                    PendingOption.Checked = true;
                    break;
                case RegistrationStatus.Reminder:
                    ReminderOption.Checked = true;
                    break;
                default:
                    ApplyOption.Checked = true;
                    break;
            }

            FirstPlace.Checked = false;
            SecondPlace.Checked = false;
            ThirdPlace.Checked = false;
            HonorableMentioned.Checked = false;
            RemoveTrophy.Checked = false;
            switch (award)
            {
                case AwardType.FirstPlace:
                    FirstPlace.Checked = true;
                    break;
                case AwardType.SecondPlace:
                    SecondPlace.Checked = true;
                    break;
                case AwardType.ThirdPlace:
                    ThirdPlace.Checked = true;
                    break;
                case AwardType.HonorableMentioned:
                    HonorableMentioned.Checked = true;
                    break;
                default:
                    break;
            }
        }

        private string GetImage(RegistrationStatus status, PaymentType method)
        {
            string imageUrl = "/images/script.png";
            switch (status)
            {
                case RegistrationStatus.Pending: imageUrl = "/images/pending.png"; break;
                case RegistrationStatus.Review: imageUrl = "/images/modify.png"; break;
                case RegistrationStatus.Rejected: imageUrl = "/images/thumbdown.png"; break;
                case RegistrationStatus.Approved: imageUrl = "/images/thumbup.png"; break;
                case RegistrationStatus.Reminder: imageUrl = "/images/reminder.png"; break;
                case RegistrationStatus.Apply: imageUrl = (method == PaymentType.FeeWaived ? "/images/mailin.png" : "/images/script.png"); break;
            }
            return imageUrl;
        }

        private string GetImage(string statustext, string methodText)
        {
            RegistrationStatus status = (RegistrationStatus)Enum.Parse(typeof(RegistrationStatus), statustext, true);
            PaymentType method = (PaymentType)Enum.Parse(typeof(PaymentType), methodText, true);
            return GetImage(status, method);
        }

        private int GetRowIndexFromPostackAction()
        {
            int rowIndex = -1;
            string[] tokens = PostbackParameters.Value.Split(new char[] { ',' });
            if (tokens.Length > 1 && int.TryParse(tokens[0], out rowIndex) == false) rowIndex = -1;
            return rowIndex;
        }

        private int GetAbsoluteRowIndex(int relativeRowIndex)
        {
            return relativeRowIndex + SearchResultGrid.PageSize * SearchResultGrid.PageIndex;
        }

        private string GetFilenameFromPostbackAction()
        {
            string filename = string.Empty;
            string[] tokens = PostbackParameters.Value.Split(new char[] { ',' });
            if (tokens.Length > 1)
            {
                filename = tokens[1];
            }
            else
            {
                filename = PostbackParameters.Value;
            }

            return filename;
        }

        private string MakeFullXmlPath(string xmlFile)
        {
            string talentApplicantFolder = CETalentApplciants.MakeRegistrationFolder();
            return Path.Combine(talentApplicantFolder, xmlFile);
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

        private string GetDictionaryKeyFromValue(Dictionary<string,string> dic, string value)
        {
            var result = dic.Where(c => string.Compare(c.Value, value, true) == 0);
            if (result != null)
            {
                foreach (var item in result)
                {
                    value = item.Key;
                    break; // deal with unique match
                }
            }
            return value;
        }

        private void RebindGrid()
        {
            RebindGrid((List<RegisteredApplicant>)Session[CEConstants.CE_REVIEW_SEARCH_RESULT_ID]);
        }

        private void RebindGrid(List<RegisteredApplicant> registeredApplicants)
        {
            SearchResultGrid.Columns[GRID_ID_COLUMN].Visible = true; // enable it so that the data is available while binding
            SearchResultGrid.Columns[GRID_STUDENT_COLUMN].Visible = true;
            SearchResultGrid.Columns[GRID_CLASS_COLUMN].Visible = true;
            SearchResultGrid.DataSource = registeredApplicants;
            SearchResultGrid.DataBind();
            // do this after data binding so that we can easily get to the hidden column in RowDataBound event
            SearchResultGrid.Columns[GRID_ID_COLUMN].Visible = false;
            SearchResultGrid.Columns[GRID_STUDENT_COLUMN].Visible = false;
            SearchResultGrid.Columns[GRID_CLASS_COLUMN].Visible = false; 

            // get the registration application simple statistices
            int contestanCount = registeredApplicants.Sum(r => r.ContestantCount);
            int categoryCount = registeredApplicants.Select(r => r.Category).Distinct().Count();
            int divisionCount = registeredApplicants.Select(r => r.Division).Distinct().Count();
            SearchResultItems.Text = string.Format("{0} applicants, {1} contestants, {2} categories, {3} divisions",
                registeredApplicants.Count(), contestanCount, categoryCount, divisionCount);
        }

        private void UpdateMasterCache(RegisteredApplicant registeredApplicant)
        {
            List<RegisteredApplicant> registeredApplicants = (List<RegisteredApplicant>)Session[CEConstants.CE_REVIEW_MASTER_RESULT_ID];
            foreach (RegisteredApplicant applicant in registeredApplicants)
            {
                if (string.Compare(applicant.File, registeredApplicant.File, true) == 0)
                {
                    applicant.Status = registeredApplicant.Status;
                    applicant.Payment = registeredApplicant.Payment;
                    applicant.PaymentMethod = registeredApplicant.PaymentMethod;
                    applicant.CheckNumber = registeredApplicant.CheckNumber;
                    Session[CEConstants.CE_REVIEW_MASTER_RESULT_ID] = registeredApplicants;
                    break;
                }
            }
        }

        private bool IsCellTextEmpty(string cellText)
        {
            return string.IsNullOrEmpty(cellText) || cellText == "&nbsp;";
        }

        private double GetRequiredPayment(CECompetitionEntry applicant)
        {
            int waiverCount = 0;
            foreach (CEContestant contestant in applicant.Contestants)
            {
                if (string.Compare(contestant.LunchProgram, "yes", true) == 0) waiverCount++;
            }
            return 5.0 * (applicant.Contestants.Count - waiverCount);
        }
    }
}