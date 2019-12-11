using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CE.Data;

namespace CE.Pages
{
    public partial class TourApplicationPage : System.Web.UI.Page
    {
        private const string FILTENAME_TEMPLATE = @"\application\{0}\{1}\{2}_{3}_{4}_{5}_{6}.xml";

        public string TourProgramYear { get; set; }
        public string TourApplicationStartDate { get; set; }
        public string TourApplicationEndDate { get; set; }
        public string TourProgramStartDate { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // disable payment buttons when the registration is closed for general users except for site admin
                bool hasCookie = false;
                string ceCookie = CEHelper.GetCookie(CEConstants.CE_ADMIN_COOKIE_ID, CEConstants.CE_ADMIN_COOKIE_NAME);
                if (ceCookie == CEConstants.CE_ADMIN_COOKIE_VALUE) hasCookie = true;

                string applicationDate = CEHelper.GetConfiguration(CEConstants.APPLICATION_STARTDATE_KEY, "12/31/2050");
                string applicationEndDate = CEHelper.GetConfiguration(CEConstants.APPLICATION_ENDDATE_KEY, "01/01/2000");
                string tourStartDate = CEHelper.GetConfiguration(CEConstants.TOUR_START_DATE, "01/01/2000");
                TourProgramYear = CEHelper.GetConfiguration(CEConstants.COMPETITION_YEAR_KEY, "2015");

                DateTime startDate, endDate, tourDate;
                bool applicationStarted = false;
                bool applicationEnded = false;
                bool tourEnded = false;
                if (DateTime.TryParse(applicationDate, out startDate) == true) applicationStarted = startDate <= DateTime.Now;
                if (DateTime.TryParse(applicationEndDate, out endDate) == true) applicationEnded = endDate <= DateTime.Now;
                if (DateTime.TryParse(tourStartDate, out tourDate) == true) tourEnded = tourDate <= DateTime.Now;
                TourApplicationStartDate = startDate.ToString("D");
                TourApplicationEndDate = endDate.ToString("D");
                TourProgramStartDate = tourDate.ToString("D");
                TourApplicationStartNote.Visible = false;
                TourApplicationEndNote.Visible = false;
                TourProgramEndNote.Visible = false;
                TourApplicationForm.Visible = false;
                SubmitButton.Enabled = false;
                CancelButton.Enabled = false;
                ApplicationStartFlag.Value = "0";
                if ((applicationStarted && !applicationEnded) || hasCookie)
                {
                    TourApplicationForm.Visible = true;
                    SubmitButton.Enabled = true;
                    CancelButton.Enabled = true;
                    if (!hasCookie) ApplicationStartFlag.Value = "1";
                }
                else if (applicationEnded && !tourEnded)
                {
                    TourApplicationEndNote.Visible = true;
                }
                else if (tourEnded)
                {
                    TourProgramEndNote.Visible = true;
                }
                else
                {
                    TourApplicationStartNote.Visible = true;
                }
            }
        }

        protected void OnCancel(object sender, EventArgs e)
        {
            Response.Redirect(CEHelper.GetSiteRootUrl() + CEConstants.CE_TALENT_COMPETITION_PAGE);
        }

        protected void OnApplciationSubmit(object sender, EventArgs e)
        {
            const int MAX_UPLOAD_SIZE = 10000000;

            // save uploaded file
            string resumeUploaded = "0";
            string lessonPlanUploaded = "0";
            string resumeFile = string.Empty;
            string lessonPlanFile = string.Empty;
            string userFile1 = string.Empty;
            string userFile2 = string.Empty;
            string userComment = string.Empty;

            try
            {
                int resumeError = 0;
                int lessonPlanError = 0;
                int userFile1Error = 0;
                int userFile2Error = 0;
                if (AttachResumeFile.PostedFile != null && AttachResumeFile.PostedFile.ContentLength > MAX_UPLOAD_SIZE) resumeError = 1;
                if (AttachLessonPlan.PostedFile != null && AttachLessonPlan.PostedFile.ContentLength > MAX_UPLOAD_SIZE) lessonPlanError = 1;
                if (AttachApplicantFile1.PostedFile != null && AttachApplicantFile1.PostedFile.ContentLength > MAX_UPLOAD_SIZE) userFile1Error = 1;
                if (AttachApplicantFile2.PostedFile != null && AttachApplicantFile2.PostedFile.ContentLength > MAX_UPLOAD_SIZE) userFile2Error = 1;

                // show error message for file that is > 10Mb. Otherwise unhandle web site error will appear.
                if (resumeError != 0 || lessonPlanError != 0 || userFile1Error != 0 || userFile2Error != 0)
                {
                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "UploadError",
                        "uploadError(" + resumeError.ToString() + "," + lessonPlanError.ToString() + "," + userFile1Error.ToString() + "," + userFile2Error.ToString() + ");",
                        true);

                    return;
                }

                if (AttachResumeFile.PostedFile != null && AttachResumeFile.PostedFile.ContentLength > 0)
                {
                    resumeFile = Path.ChangeExtension(GetUploadFullFileName("resume"), Path.GetExtension(AttachResumeFile.PostedFile.FileName));
                    AttachResumeFile.PostedFile.SaveAs(resumeFile);
                    resumeUploaded = "1";
                }
                if (AttachLessonPlan.PostedFile != null && AttachLessonPlan.PostedFile.ContentLength > 0)
                {
                    lessonPlanFile = Path.ChangeExtension(GetUploadFullFileName("lessonPlan"), Path.GetExtension(AttachLessonPlan.PostedFile.FileName));
                    AttachLessonPlan.PostedFile.SaveAs(lessonPlanFile);
                    lessonPlanUploaded = "1";
                }
                string userFile = "userFile";
                int count = 0;
                if (ApplincantFile1Removed.Value == "0" && AttachApplicantFile1.PostedFile != null && AttachApplicantFile1.PostedFile.ContentLength > 0)
                {
                    count++;
                    userFile1 = Path.ChangeExtension(GetUploadFullFileName(userFile + count.ToString()), Path.GetExtension(AttachApplicantFile1.PostedFile.FileName));
                    AttachApplicantFile1.PostedFile.SaveAs(userFile1);
                }
                if (ApplincantFile2Removed.Value == "0" && AttachApplicantFile2.PostedFile != null && AttachApplicantFile2.PostedFile.ContentLength > 0)
                {
                    count++;
                    userFile2 = Path.ChangeExtension(GetUploadFullFileName(userFile + count.ToString()), Path.GetExtension(AttachApplicantFile2.PostedFile.FileName));
                    AttachApplicantFile2.PostedFile.SaveAs(userFile2);
                }
            }
            catch
            {
            }

            if (!SkipUpload.Checked && (resumeUploaded == "0" || lessonPlanUploaded == "0"))
            {
                string parameters = resumeUploaded + "," + lessonPlanUploaded;
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "SavingError",
                    "saveUploadFileError(" + parameters + ");",
                    true);

                return;
            }

            string registrationFile = GetApplicationFullFileName();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine(CEConstants.APPLICATION_XML_BEGIN_TEMPLATE);

            // page 1: personal information
            sb.AppendLine("\t\t<firstName>" + FirstNameBox.Text + "</firstName>");
            sb.AppendLine("\t\t<lastName>" + LastNameBox.Text + "</lastName>");
            sb.AppendLine("\t\t<email>" + EmailBox.Text + "</email>");
            sb.AppendLine("\t\t<phone>" + PhoneBox.Text + "</phone>");
            sb.AppendLine("\t\t<cellphone>" + CellPhoneBox.Text + "</cellphone>");
            sb.AppendLine("\t\t<district>" + DistrictBox.Text + "</district>");
            sb.AppendLine("\t\t<school>" + SchoolBox.Text + "</school>");
            sb.AppendLine("\t\t<grade>" + GradeBox.Text + "</grade>");
            sb.AppendLine("\t\t<subject>" + SubjectBox.Text + "</subject>");
            sb.AppendLine("\t\t<gender>" + (MaleGender.Checked ? "Male" : "Female") + "</gender>");
            sb.AppendLine("\t\t<entryDate>" + DateTime.Now.ToString("MM/dd/yyyy") + "</entryDate>");
            // page 2: references
            sb.AppendLine("\t\t<learnProgram question=\"" + CEConstants.HOW_YOU_LEARN_OUR_PROGRAM + "\">" + HttpUtility.HtmlEncode(LearnProgramBox.Text) + "</learnProgram>");
            sb.AppendLine("\t\t<specialty question=\"" + CEConstants.TEACHING_SPECIALTY + "\">" + HttpUtility.HtmlEncode(SpecialtyBox.Text) + "</specialty>");
            sb.AppendLine("\t\t<reference1>" + HttpUtility.HtmlEncode(Reference1Box.Text) + "</reference1>");
            sb.AppendLine("\t\t<reference2>" + HttpUtility.HtmlEncode(Reference2Box.Text) + "</reference2>");
            sb.AppendLine("\t\t<reference3>" + HttpUtility.HtmlEncode(Reference3Box.Text) + "</reference3>");
            // page 3: questionaire
            sb.AppendLine("\t\t<questionaire1 question=\"" + CEConstants.QUESTIONAIRE1 + "\">" + HttpUtility.HtmlEncode(Questionaire1Box.Text) + "</questionaire1>");
            sb.AppendLine("\t\t<questionaire2 question=\"" + CEConstants.QUESTIONAIRE2 + "\">" + HttpUtility.HtmlEncode(Questionaire2Box.Text) + "</questionaire2>");
            sb.AppendLine("\t\t<questionaire3 question=\"" + CEConstants.QUESTIONAIRE3 + "\">" + HttpUtility.HtmlEncode(Questionaire3Box.Text) + "</questionaire3>");
            sb.AppendLine("\t\t<questionaire4 question=\"" + CEConstants.QUESTIONAIRE4 + "\">" + HttpUtility.HtmlEncode(Questionaire4Box.Text) + "</questionaire4>");
            // page 4: uploaded files
            sb.AppendLine("\t\t<resume>" + resumeFile + "</resume>");
            sb.AppendLine("\t\t<lessonPlan>" + lessonPlanFile + "</lessonPlan>");
            sb.AppendLine("\t\t<userFile1>" + userFile1 + "</userFile1>");
            sb.AppendLine("\t\t<userFile2>" + userFile2 + "</userFile2>");
            sb.AppendLine("\t\t<userComment>" + HttpUtility.HtmlEncode(UserComment.Text) + "</userComment>");

            sb.AppendLine(CEConstants.APPLICATION_XML_END_TEMPLATE);

            CEHelper.WaitAndWrite(registrationFile, sb.ToString(), false, true);

            SendConfirmationEmail(EmailBox.Text, FirstNameBox.Text, AttachResumeFile.PostedFile.FileName, AttachLessonPlan.PostedFile.FileName, AttachApplicantFile1.PostedFile.FileName, AttachApplicantFile2.PostedFile.FileName);
        }

        private bool SendConfirmationEmail(string email, string applicantName, string resume, string lessonPlan, string userFile1, string userFile2)
        {
            bool emailSent = false;
            EmailInfo emailInfo = CEHelper.GetEmailConfiguration("tour", "apply");
            if (emailInfo != null && !string.IsNullOrEmpty(emailInfo.Message))
            {
                string attachedFiles = string.Format("<ul><li>Resume: {0}</li><li>Lesson Plan: {1}</li>", resume, lessonPlan);
                if (!string.IsNullOrEmpty(userFile1)) attachedFiles += string.Format("<li>Additional File: {0}</li>", userFile1);
                if (!string.IsNullOrEmpty(userFile2)) attachedFiles += string.Format("<li>Additional File: {0}</li>", userFile2);
                attachedFiles += "</ul>";

                StringBuilder sb = new StringBuilder(emailInfo.Message);
                sb.Replace("{%contact%}", applicantName);
                sb.Replace("{%attachedFiles%}", attachedFiles);
                emailInfo.Message = sb.ToString();
                emailInfo.Recipients = new string[] { email };
                CEHelper.SendEmail(emailInfo, null);
                emailSent = true;
            }

            if (emailSent)
            {
                Session[CEConstants.CE_PAYMENT_COOKIE_ID] = null; // done with registration

                // trigger the javascript function to acknlowdge the completion of talent registration
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "TourApplication",
                    "applicationSubmitted();",
                    true);
            }

            return emailSent;
        }

        // this method returns the data folder that all the applicant application form data is stored under.
        // In Arvixe sever folder structure, the 'data' folder is at the same level as the wwwroot folder.
        // All applicant data is stored in a xml file named 'firstname_lastname_school_grade_year.xml'
        private string GetApplicationFullFileName()
        {
            return GetUploadFullFileName(CEHelper.GetCompetitionYear());
        }

        private string GetUploadFullFileName(string which)
        {
            string webHomeFolder = HttpContext.Current.Request.PhysicalApplicationPath;
            string hostFolder = Directory.GetParent(webHomeFolder).Name;
            string folder = "Tours";
            string applicationYear = CEHelper.GetCompetitionYear();
            string dataFile = string.Format(FILTENAME_TEMPLATE, 
                                            folder,
                                            applicationYear,
                                            NeutralizeFilenameText(FirstNameBox.Text),
                                            NeutralizeFilenameText(LastNameBox.Text),
                                            NeutralizeFilenameText(SchoolBox.Text),
                                            NeutralizeFilenameText(GradeBox.Text),
                                            which);
            return CEHelper.GetDataPath() + dataFile;
        }

        private string NeutralizeFilenameText(string s)
        {
            s = s.Replace(" ", string.Empty).Replace("'", string.Empty).Replace(",", "-");
            if (s == string.Empty) s = "0"; // use '0' for no value input
            return s;
        }
    }
}
