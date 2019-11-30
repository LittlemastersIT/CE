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
    public partial class ReviewApplicationsPage : System.Web.UI.Page
    {
        private const string APPLICANTS_VIEWSTATE = "TourApplicants";
        private const string RELATEDLINKS_VIEWSTATE = "RelatedLinks";

        private const string PAGER_HTML_BEGIN_TEMPLATE = "<span>Page </span><ul>";
        private const string PAGER_HTML_ITEM_TEMPLATE = "<li>{0}</li>";
        private const string PAGER_HTML_END_TEMPLATE = "</ul>";
        private const string STATUS_LINK_TEMPLATE = "<a href=\"javascript:loadApplicantData({0},'{1}');\"><img class=\"search-grid-icon\" src=\"{2}\" /></a>";
        private const string RESUME_LINK_TEMPLATE = "<a href=\"javascript:void();\" target=\"_blank\" OnClick=\"OnDownloadResume\"><img class=\"search-grid-icon\" src=\"{0}\" /></a>";
        private const int GRID_STATUS_COLUMN = 0;
        private const int GRID_RESUME_COLUMN = 9;
        private const int GRID_ID_COLUMN = 10; // this is the applicant File cell; hidden
        private const int GRID_PAGE_SIZE = 10;
        private const string ALL_SELECTION_TEXT = "All";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ReviewerName.Value = CEHelper.GetCookie(CEConstants.CE_USER_COOKIE_ID, CEConstants.CE_USER_COOKIE_NAME);
                ReviewerDisplayName.Value = CEHelper.GetCookie(CEConstants.CE_DISPLAYNAME_COOKIE_ID, CEConstants.CE_DISPLAYNAME_COOKIE_NAME);
                SetSession(CEConstants.CE_REVIEW_MASTER_RESULT_ID, (List<ReviewedApplicant>)null);
                SetSession(CEConstants.CE_REVIEW_SEARCH_RESULT_ID, (List<ReviewedApplicant>)null);
                SetSession(CEConstants.CE_ROLE_USERS_ID, (List<CEUser>)null);
                CETourApplicants tourApplicants = new CETourApplicants();
                if (tourApplicants.TourApplicants.Count > 0)
                {
                    List<ReviewedApplicant> reviewedApplicants = tourApplicants.CreateReviewedApplicants();
                    reviewedApplicants = reviewedApplicants.OrderByDescending(s => s.EntryDate).ToList();
                    SetSession(CEConstants.CE_REVIEW_MASTER_RESULT_ID, reviewedApplicants);
                    SetSession(CEConstants.CE_REVIEW_SEARCH_RESULT_ID, reviewedApplicants);
                    RebindGrid(reviewedApplicants);
                    SchoolDistrictList.DataSource = GetDistinctDistricts(reviewedApplicants);
                    SchoolDistrictList.DataBind();
                }
                List<CEUser> users = SiteUsers.FindUsersWithRole(CEConstants.CE_TOUR_ROLE);
                SetSession(CEConstants.CE_ROLE_USERS_ID, users);
            }
        }

        protected void OnSearchApplicants(object sender, EventArgs e)
        {
            List<ReviewedApplicant> allApplicants = (List<ReviewedApplicant>)GetSession(CEConstants.CE_REVIEW_MASTER_RESULT_ID);
            if (allApplicants != null && allApplicants.Count > 0)
            {
                string district = SchoolDistrictList.SelectedValue;
                if (district == "All") district = string.Empty;

                ReviewStatus status = ReviewStatus.Unknown;
                if (!string.IsNullOrEmpty(StatusList.SelectedValue))
                {
                    status = (ReviewStatus)Enum.Parse(typeof(ReviewStatus), StatusList.SelectedValue, true);
                }

                List<ReviewedApplicant> foundApplicants = new List<ReviewedApplicant>();

                foreach (ReviewedApplicant applicant in allApplicants)
                {
                    if ((string.IsNullOrEmpty(district) || string.Compare(applicant.District, district, true) == 0) &&
                        (status == ReviewStatus.Unknown || status == applicant.Status))
                    {
                        foundApplicants.Add(applicant);
                    }
                }

                // bidn the search result
                SetSession(CEConstants.CE_REVIEW_SEARCH_RESULT_ID, foundApplicants);
                RebindGrid(foundApplicants);
                ApplicantDetails.Visible = false;
            }
        }

        protected void OnExcelExport(object sender, EventArgs e)
        {
            if (CEExport.ExportTourApplication(Response) == false)
            {
                string title = "Excel Export Problem";
                string message = "There is problem exporting tour application to Excel file. Please try again later. If the problem persists, please contact site administrator.";
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
                string status = e.Row.Cells[GRID_STATUS_COLUMN].Text;
                if (!string.IsNullOrEmpty(status))
                {
                    string imageUrl = GetImage(status);
                    e.Row.Cells[GRID_STATUS_COLUMN].Text = string.Format(STATUS_LINK_TEMPLATE, e.Row.RowIndex.ToString(), e.Row.Cells[GRID_ID_COLUMN].Text, imageUrl);
                }
                string resume = e.Row.Cells[GRID_RESUME_COLUMN].Text;
                if (!string.IsNullOrEmpty(resume))
                {
                    string imageUrl = "/images/pin_blue.png";
                    e.Row.Cells[GRID_RESUME_COLUMN].Text = string.Format(RESUME_LINK_TEMPLATE, imageUrl, resume);
                }
            }
        }

        protected void SearchResultSorting(object sender, GridViewSortEventArgs e)
        {
            List<ReviewedApplicant> applicants = (List<ReviewedApplicant>)Session[CEConstants.CE_REVIEW_SEARCH_RESULT_ID];

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
                else if (e.SortExpression == "ApplicantName")
                {
                    if (NameSortDirection.Value == "1")
                        applicants = applicants.OrderByDescending(s => s.ApplicantName).ToList();
                    else
                        applicants = applicants.OrderBy(s => s.ApplicantName).ToList();
                    NameSortDirection.Value = NameSortDirection.Value == "0" ? "1" : "0";
                }
                else
                    return;

                Session[CEConstants.CE_REVIEW_SEARCH_RESULT_ID] = applicants;
                RebindGrid();
                ApplicantDetails.Visible = false;
            }
        }

        // this event handle pagination
        protected void OnApplicationPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SearchResultGrid.PageIndex = e.NewPageIndex;
            RebindGrid();
        }

        // trigger when status icon is clicked
        protected void OnPostbackAction(object sender, EventArgs e)
        {
            string filename = GetFilenameFromPostbackAction();
            CETourApplicant tourApplicant = new CETourApplicant(MakeFullXmlPath(filename));
            BindApplicantData(tourApplicant);

            // trigger the javascript function to bring up the review modal dialog
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "reviewDialog",
                "showReviewDialog(0);",
                true);
        }

        private void RebindGrid()
        {
            RebindGrid((List<ReviewedApplicant>)GetSession(CEConstants.CE_REVIEW_SEARCH_RESULT_ID));
        }

        private void RebindGrid(List<ReviewedApplicant> reviewedApplicants)
        {
            SearchResultGrid.Columns[GRID_RESUME_COLUMN].Visible = true; // enable it so that the data is available while binding
            SearchResultGrid.Columns[GRID_ID_COLUMN].Visible = true; // enable it so that the data is available while binding
            SearchResultGrid.DataSource = reviewedApplicants;
            SearchResultGrid.DataBind();
            SearchResultGrid.Columns[GRID_RESUME_COLUMN].Visible = false; // enable it so that the data is available while binding
            SearchResultGrid.Columns[GRID_ID_COLUMN].Visible = false; // do this after data binding so that we can easily get to the hidden column in RowDataBound event

            // get the tour application simple statistices
            int districtCount = reviewedApplicants.Select(r => r.District).Distinct().Count();
            int schoolCount = reviewedApplicants.Select(r => r.School).Distinct().Count();
            SearchResultItems.Text = string.Format("{0} applicants, {1} school districts, {2} schools", reviewedApplicants.Count(), districtCount, schoolCount);
        }

        private string GetImage(ReviewStatus status)
        {
            string imageUrl = "/images/script.png";
            switch (status)
            {
                case ReviewStatus.Withdraw: imageUrl = "/images/no.png"; break;
                case ReviewStatus.Review: imageUrl = "/images/modify.png"; break;
                case ReviewStatus.Rejected: imageUrl = "/images/thumbdown.png"; break;
                case ReviewStatus.Interview: imageUrl = "/images/yes.png"; break;
                case ReviewStatus.Awarded: imageUrl = "/images/thumbup.png"; break;
                case ReviewStatus.Apply: imageUrl = "/images/script.png"; break;
            }
            return imageUrl;
        }

        private string GetImage(string statustext)
        {
            ReviewStatus status = (ReviewStatus)Enum.Parse(typeof(ReviewStatus), statustext, true);
            return GetImage(status);
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
            string talentApplicantFolder = CETourApplicants.MakeApplicationFolder();
            if (!Directory.Exists(talentApplicantFolder))
            {
                Directory.CreateDirectory(talentApplicantFolder);
            }

            return Path.Combine(talentApplicantFolder, xmlFile);
        }

        private void BindApplicantData(CETourApplicant applicant)
        {
            Applicant.Text = applicant.ApplicantName;
            ApplicantXmlFileBox.Text = applicant.ApplicantFile;
            FirstNameBox.Text = applicant.FirstName;
            LastNameBox.Text = applicant.LastName;
            EmailBox.Text = applicant.Email;
            PhoneBox.Text = applicant.Phone;
            CellPhoneBox.Text = applicant.CellPhone;
            DistrictBox.Text = applicant.District;
            SchoolBox.Text = applicant.School;
            GradeBox.Text = applicant.Grade;
            SubjectBox.Text = applicant.Subject;
            FemaleGender.Checked = applicant.Gender == "Male" ? false : true;
            MaleGender.Checked = applicant.Gender == "Male" ? true : false;
            LearnProgramBox.Text = HttpUtility.HtmlDecode(applicant.Program);
            SpecialtyBox.Text = HttpUtility.HtmlDecode(applicant.Specialty);
            Reference1Box.Text = applicant.Reference1;
            Reference2Box.Text = applicant.Reference2;
            Reference3Box.Text = applicant.Reference3;
            Questionaire1Box.Text = HttpUtility.HtmlDecode(applicant.Questionaire1.Answer);
            Questionaire2Box.Text = HttpUtility.HtmlDecode(applicant.Questionaire2.Answer);
            Questionaire3Box.Text = HttpUtility.HtmlDecode(applicant.Questionaire3.Answer);
            Questionaire4Box.Text = HttpUtility.HtmlDecode(applicant.Questionaire4.Answer);
            UserComment.Text = HttpUtility.HtmlDecode(applicant.UserComment);

            MemberList.DataSource = EnsureAllCommitteeComments(applicant.ReviewComments);
            MemberList.DataBind();

            ApplicantFileDownload1.Visible = !string.IsNullOrEmpty(applicant.UserFile1);
            ApplicantFileDownload2.Visible = !string.IsNullOrEmpty(applicant.UserFile2);

            InitReviewStatus(applicant.Status);
            SetReviewStatus(applicant.Status);

            ApplicantDetails.Visible = true;
        }

        private void InitReviewStatus(ReviewStatus status)
        {
            ApplyOption.Enabled = true;
            ReviewOption.Enabled = true;
            InterviewOption.Enabled = true;
            AwardOption.Enabled = true;
            RejectOption.Enabled = true;
            WithdrawOption.Enabled = true;
            ApplyOption.CssClass = "cell-button";
            ReviewOption.CssClass = "cell-button";
            InterviewOption.CssClass = "cell-button";
            AwardOption.CssClass = "cell-button";
            RejectOption.CssClass = "cell-button";
            WithdrawOption.CssClass = "cell-button";

            if (CEHelper.GetConfiguration(CEConstants.REVIEW_BACKFLOW_KEY, "0") == "0")
            {
                switch (status)
                {
                    case ReviewStatus.Apply:
                        break;
                    case ReviewStatus.Review:
                        ApplyOption.Enabled = false;
                        ApplyOption.CssClass = "cell-button inactive";
                        break;
                    case ReviewStatus.Awarded:
                        ApplyOption.Enabled = false;
                        ReviewOption.Enabled = false;
                        InterviewOption.Enabled = false;
                        RejectOption.Enabled = false;
                        ApplyOption.CssClass = "cell-button inactive";
                        ReviewOption.CssClass = "cell-button inactive";
                        InterviewOption.CssClass = "cell-button inactive";
                        RejectOption.CssClass = "cell-button inactive";
                        break;
                    case ReviewStatus.Interview:
                        ApplyOption.Enabled = false;
                        ReviewOption.Enabled = false;
                        ApplyOption.CssClass = "cell-button inactive";
                        ReviewOption.CssClass = "cell-button inactive";
                        break;
                    case ReviewStatus.Rejected:
                        ApplyOption.Enabled = false;
                        ReviewOption.Enabled = false;
                        InterviewOption.Enabled = false;
                        AwardOption.Enabled = false;
                        ApplyOption.CssClass = "cell-button inactive";
                        ReviewOption.CssClass = "cell-button inactive";
                        InterviewOption.CssClass = "cell-button inactive";
                        AwardOption.CssClass = "cell-button inactive";
                        break;
                    case ReviewStatus.Withdraw:
                        ApplyOption.Enabled = false;
                        ReviewOption.Enabled = false;
                        InterviewOption.Enabled = false;
                        AwardOption.Enabled = false;
                        RejectOption.Enabled = false;
                        ApplyOption.CssClass = "cell-button inactive";
                        ReviewOption.CssClass = "cell-button inactive";
                        InterviewOption.CssClass = "cell-button inactive";
                        AwardOption.CssClass = "cell-button inactive";
                        RejectOption.CssClass = "cell-button inactive";
                        break;
                    default:
                        break;
                }
            }
        }

        private void SetReviewStatus(ReviewStatus status)
        {
            ApplyOption.Checked = false;
            ReviewOption.Checked = false;
            AwardOption.Checked = false;
            InterviewOption.Checked = false;
            RejectOption.Checked = false;
            WithdrawOption.Checked = false;
            switch (status)
            {
                case ReviewStatus.Apply:
                    ApplyOption.Checked = true;
                    break;
                case ReviewStatus.Review:
                    ReviewOption.Checked = true;
                    break;
                case ReviewStatus.Awarded:
                    AwardOption.Checked = true;
                    break;
                case ReviewStatus.Interview:
                    InterviewOption.Checked = true;
                    break;
                case ReviewStatus.Rejected:
                    RejectOption.Checked = true;
                    break;
                case ReviewStatus.Withdraw:
                    WithdrawOption.Checked = true;
                    break;
                default:
                    ApplyOption.Checked = true;
                    break;
            }
        }

        private object GetSession(string id)
        {
            return Session[id];
        }

        private void SetSession(string id, List<ReviewedApplicant> applicants)
        {
            Session[id] = applicants;
        }

        private void SetSession(string id, List<CEUser> users)
        {
            Session[id] = users;
        }

        protected void OnCommitteeCommentDataBound(object sender, RepeaterItemEventArgs e)
        {
            CommitteeComment ceComment = (CommitteeComment)e.Item.DataItem;
            Control commentControl = e.Item.FindControl("MemberComment");
            Control applicationControl = e.Item.FindControl("ApplicationScoreBox");
            Control relevancyControl = e.Item.FindControl("RelevancyScoreBox");
            Control lessonPlanControl = e.Item.FindControl("LessonPlanScoreBox");
            Control saveControl = e.Item.FindControl("UpdateReviewComment");
            if (commentControl != null && saveControl != null && applicationControl != null && relevancyControl != null && lessonPlanControl != null)
            {
                if (string.Compare(ceComment.Name, ReviewerName.Value, true) == 0)
                {
                    ((TextBox)commentControl).Enabled = true;
                    ((TextBox)applicationControl).Enabled = true;
                    ((TextBox)relevancyControl).Enabled = true;
                    ((TextBox)lessonPlanControl).Enabled = true;
                    ((Button)saveControl).Visible = true;
                }
                else
                {
                    ((TextBox)commentControl).Enabled = false;
                    ((TextBox)applicationControl).Enabled = false;
                    ((TextBox)relevancyControl).Enabled = false;
                    ((TextBox)lessonPlanControl).Enabled = false;
                    ((Button)saveControl).Visible = false;
                }
            }
        }

        protected void OnSaveReviewComment(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "save" && string.Compare(e.CommandArgument.ToString(), ReviewerName.Value, true) == 0)
            {
                TextBox commentBox = (TextBox)(e.Item.FindControl("MemberComment"));
                TextBox applicationScoreBox = (TextBox)(e.Item.FindControl("ApplicationScoreBox"));
                TextBox relevancyScoreBox = (TextBox)(e.Item.FindControl("RelevancyScoreBox"));
                TextBox lessonPlanScoreBox = (TextBox)(e.Item.FindControl("LessonPlanScoreBox"));
                if (commentBox != null && applicationScoreBox != null && relevancyScoreBox != null && lessonPlanScoreBox != null)
                {
                    // rebind review comments
                    string filename = GetFilenameFromPostbackAction();
                    int applicationScore = 0, relevancyScore = 0, lessonPlanScore = 0;
                    if (int.TryParse(applicationScoreBox.Text, out applicationScore) == false) applicationScore = 0;
                    if (int.TryParse(relevancyScoreBox.Text, out relevancyScore) == false) relevancyScore = 0;
                    if (int.TryParse(lessonPlanScoreBox.Text, out lessonPlanScore) == false) lessonPlanScore = 0;
                    CETourApplicant tourApplicant = new CETourApplicant(MakeFullXmlPath(filename));
                    tourApplicant = tourApplicant.UpdateComment(ReviewerName.Value, ReviewerDisplayName.Value, commentBox.Text.Trim(), applicationScore, relevancyScore, lessonPlanScore);
                    tourApplicant.UpdateTourApplicationFile();
                    MemberList.DataSource = EnsureAllCommitteeComments(tourApplicant.ReviewComments);
                    MemberList.DataBind();
                }
                // return to the review comment tab
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "reviewerCommentDone",
                    "reviewerCommentDone();",
                    true);
            }
            else
            {
                // trigger the javascript function to acknowledge the change of the applicant status
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "restorePopup",
                    "restoreReviewDialog(5)",
                    true);
            }
        }

        protected void OnDownloadResume(object sender, EventArgs e)
        {
            // ship the resume from repository to client machine for download
            ReviewedApplicant currentApplicant = GetCurrentApplicant(ApplicantXmlFileBox.Text);
            string downloadFilename = MakeTourApplicantionFullPath(currentApplicant.ResumeFile);
            if (currentApplicant != null && !string.IsNullOrEmpty(currentApplicant.ResumeFile) && System.IO.File.Exists(downloadFilename))
            {
                DownloadFile(downloadFilename, "Resume");
            }
            else
            {
                // trigger the javascript function to acknowledge the change of the applicant status
                string filename = (currentApplicant.ResumeFile == null ? string.Empty : currentApplicant.ResumeFile);
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "resumeDownload",
                    "fileDownloadError('resume', '" + filename + "');",
                    true);
            }
        }

        protected void OnDownloadLessonPlan(object sender, EventArgs e)
        {
            // ship the resume from repository to client machine for download
            ReviewedApplicant currentApplicant = GetCurrentApplicant(ApplicantXmlFileBox.Text);
            string downloadFilename = MakeTourApplicantionFullPath(currentApplicant.LessonPlanFile);
            if (currentApplicant != null && !string.IsNullOrEmpty(currentApplicant.LessonPlanFile) && System.IO.File.Exists(downloadFilename))
            {
                DownloadFile(downloadFilename, "LessonPlan");
            }
            else
            {
                // trigger the javascript function to acknowledge the change of the applicant status
                string filename = (currentApplicant.LessonPlanFile == null ? string.Empty : currentApplicant.LessonPlanFile);
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "lessonPlanDownload",
                    "fileDownloadError('lesson plan', '" + filename + "');",
                    true);
            }
        }

        protected void OnDownloadApplicantFile1(object sender, EventArgs e)
        {
            // ship the resume from repository to client machine for download
            ReviewedApplicant currentApplicant = GetCurrentApplicant(ApplicantXmlFileBox.Text);
            string downloadFilename = MakeTourApplicantionFullPath(currentApplicant.UserFile1);
            if (currentApplicant != null && !string.IsNullOrEmpty(currentApplicant.UserFile1) && System.IO.File.Exists(downloadFilename))
            {
                DownloadFile(downloadFilename, "file1");
            }
            else
            {
                // trigger the javascript function to acknowledge the change of the applicant status
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "file1Download",
                    "fileDownloadError('file1');",
                    true);
            }
        }

        protected void OnDownloadApplicantFile2(object sender, EventArgs e)
        {
            // ship the resume from repository to client machine for download
            ReviewedApplicant currentApplicant = GetCurrentApplicant(ApplicantXmlFileBox.Text);
            string downloadFilename = MakeTourApplicantionFullPath(currentApplicant.UserFile2);
            if (currentApplicant != null && !string.IsNullOrEmpty(currentApplicant.UserFile2) && System.IO.File.Exists(downloadFilename))
            {
                DownloadFile(downloadFilename, "file2");
            }
            else
            {
                // trigger the javascript function to acknowledge the change of the applicant status
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "file2Download",
                    "fileDownloadError('file2');",
                    true);
            }
        }

        protected void DownloadFile(string filename, string which)
        {
            try
            {
                if (Path.GetExtension(filename).ToLower() == ".pdf")
                    Context.Response.ContentType = "application/pdf";
                else
                    Context.Response.ContentType = "application/octet-stream";

                Context.Response.BufferOutput = false;
                Context.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(filename));
                Context.Response.WriteFile(filename);
                Context.Response.End();
                Context.Response.Flush();
            }
            catch
            {
                // trigger the javascript function to acknowledge the change of the applicant status
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "fileDownload",
                    "fileDownloadError('" + which + "');",
                    true);
            }
        }

        private ReviewedApplicant GetCurrentApplicant(string applcantFile)
        {
            List<ReviewedApplicant> applicants = (List<ReviewedApplicant>)GetSession(CEConstants.CE_REVIEW_MASTER_RESULT_ID);
            if (applicants != null)
            {
                foreach (ReviewedApplicant applicant in applicants)
                {
                    if (string.Compare(applicant.ApplicantFile, applcantFile, true) == 0)
                        return applicant;
                }
            }
            return null;
        }

        protected void OnCancel(object sender, EventArgs e)
        {
            Response.Redirect(CEConstants.CE_ADMIN_PAGE);
        }

        protected void OnApplicantStatusChanged(object sender, EventArgs e)
        {
            int rowIndex = GetRowIndexFromPostackAction();
            int actualRowIndex = GetAbsoluteRowIndex(rowIndex);
            ReviewStatus status = GetReviewStatus();

            // change the session cache
            List<ReviewedApplicant> reviewedApplicants = (List<ReviewedApplicant>)GetSession(CEConstants.CE_REVIEW_SEARCH_RESULT_ID);
            if (reviewedApplicants != null && actualRowIndex >= 0 && actualRowIndex < reviewedApplicants.Count<ReviewedApplicant>())
            {
                ReviewedApplicant reviewedApplicant = reviewedApplicants[actualRowIndex];

                // update xml file
                CETourApplicant currentApplicant = new CETourApplicant(MakeFullXmlPath(reviewedApplicant.ApplicantFile));
                ReviewStatus oldStatus = currentApplicant.Status;
                bool needEmail = false; // currentApplicant.Status != ReviewStatus.Apply && currentApplicant.Status != ReviewStatus.Review && currentApplicant.Status != ReviewStatus.Withdraw;
                if (currentApplicant.UpdateApplicantStatus(status) == true)
                {
                    // send an email to applicant
                    if (needEmail)
                    {
                        bool emailSent = false;
                        EmailInfo emailInfo = CEHelper.GetEmailConfiguration("tour", currentApplicant.Status.ToString().ToLower());
                        if (emailInfo != null && !string.IsNullOrEmpty(emailInfo.Message))
                        {
                            StringBuilder sb = new StringBuilder(emailInfo.Message);
                            sb.Replace("{%contact%}", currentApplicant.ApplicantName);
                            CEHelper.SendEmail(emailInfo.SmtpHost, emailInfo.SmtpPort, emailInfo.Sender, new string[] { currentApplicant.Email }, emailInfo.CCs, emailInfo.Subject, sb.ToString(), emailInfo.IsHtml, null);
                            emailSent = true;
                        }

                        if (emailSent)
                        {
                            // update session cache
                            reviewedApplicant.Status = status;
                            SetSession(CEConstants.CE_REVIEW_SEARCH_RESULT_ID, reviewedApplicants);
                            RebindGrid();

                            // update master cache
                            UpdateMasterCache(reviewedApplicant);

                            string message = "The applicant status has been changed and applciant is notified.";
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
                            currentApplicant.UpdateApplicantStatus(oldStatus); // revert the change if email has error
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
                    else
                    {
                        // update session cache
                        reviewedApplicant.Status = status;
                        SetSession(CEConstants.CE_REVIEW_SEARCH_RESULT_ID, reviewedApplicants);
                        RebindGrid();

                        // update master cache
                        UpdateMasterCache(reviewedApplicant);
                        string message = "The applicant status has been changed.";
                        // trigger the javascript function to acknowledge the change of the applicant status
                        ScriptManager.RegisterStartupScript(
                            this,
                            this.GetType(),
                            "statusUpdated",
                            "statusUpdated('" + message + "');",
                            true);
                    }
                }
            }
        }

        protected void OnRestoreApplicantStatus(object sender, EventArgs e)
        {
            int rowIndex = GetRowIndexFromPostackAction();
            int actualRowIndex = GetAbsoluteRowIndex(rowIndex);

            // change the session cache
            List<ReviewedApplicant> reviewedApplicants = (List<ReviewedApplicant>)GetSession(CEConstants.CE_REVIEW_SEARCH_RESULT_ID);
            if (reviewedApplicants != null && actualRowIndex >= 0 && actualRowIndex < reviewedApplicants.Count<ReviewedApplicant>())
            {
                ReviewedApplicant reviewedApplicant = reviewedApplicants[actualRowIndex];
                CETourApplicant currentApplicant = new CETourApplicant(MakeFullXmlPath(reviewedApplicant.ApplicantFile));
                ReviewStatus oldStatus = currentApplicant.Status;
                SetReviewStatus(oldStatus);

                // update master cache
                // trigger the javascript function to acknowledge the change of the applicant status
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "restoreStatusDialog",
                    "restoreStatusDialog();",
                    true);
            }
        }

        private void UpdateMasterCache(ReviewedApplicant reviewedApplicant)
        {
            List<ReviewedApplicant> reviewedApplicants = (List<ReviewedApplicant>)GetSession(CEConstants.CE_REVIEW_MASTER_RESULT_ID);
            if (reviewedApplicants != null)
            {
                foreach (ReviewedApplicant applicant in reviewedApplicants)
                {
                    if (string.Compare(applicant.ApplicantFile, reviewedApplicant.ApplicantFile, true) == 0)
                    {
                        applicant.Status = reviewedApplicant.Status;
                        SetSession(CEConstants.CE_REVIEW_MASTER_RESULT_ID, reviewedApplicants);
                        break;
                    }
                }
            }
        }

        private ReviewStatus GetReviewStatus()
        {
            ReviewStatus status = ReviewStatus.Apply;
            if (AwardOption.Checked)
                status = ReviewStatus.Awarded;
            else if (InterviewOption.Checked)
                status = ReviewStatus.Interview;
            else if (RejectOption.Checked)
                status = ReviewStatus.Rejected;
            else if (WithdrawOption.Checked)
                status = ReviewStatus.Withdraw;
            else if (ReviewOption.Checked)
                status = ReviewStatus.Review;
            else
                status = ReviewStatus.Apply;

            return status;
        }

        private List<CommitteeComment> EnsureAllCommitteeComments(List<CommitteeComment> comments)
        {
            List<CEUser> users = (List<CEUser>)GetSession(CEConstants.CE_ROLE_USERS_ID);
            if (users != null && users.Count > 0)
            {
                string today = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                foreach (CEUser user in users)
                {
                    if (!UserHasComment(comments, user.UserName))
                    {
                        comments.Add(new CommitteeComment(user.UserName, user.DisplayName, string.Empty, string.Empty, 0, 0, 0));
                    }
                }
            }
            return comments;
        }

        private bool UserHasComment(List<CommitteeComment> comments, string username)
        {
            foreach (CommitteeComment comment in comments)
            {
                if (string.Compare(comment.Name, username, true) == 0) return true;
            }
            return false;
        }

        private string MakeTourApplicantionFullPath(string filename)
        {
            string tourApplicantFolder = CETourApplicants.MakeApplicationFolder();
            if (!Directory.Exists(tourApplicantFolder))
            {
                Directory.CreateDirectory(tourApplicantFolder);
            }

            return Path.Combine(tourApplicantFolder, Path.GetFileName(filename));
        }

        private List<string> GetDistinctDistricts(List<ReviewedApplicant> applicants)
        {
            StringCaseInsensiveComparer districtComaparer = new StringCaseInsensiveComparer();
            List<string> districts = new List<string>();
            districts.Add("All");
            foreach (ReviewedApplicant applicant in applicants)
            {
                if (!districts.Contains(applicant.District, districtComaparer)) districts.Add(applicant.District);
            }
            return districts;
        }
    }

    internal class StringCaseInsensiveComparer : IEqualityComparer<string>
    {
        public bool Equals(string s1, string s2)
        {
            return string.Compare(s1, s2, true) == 0;
        }

        public int GetHashCode(string s)
        {
            return 0; // not implemented
        }
    }
}