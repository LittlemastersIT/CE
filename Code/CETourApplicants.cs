using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Web;
using CE.Data;

namespace CE.Data
{
    public class CETourApplicants
    {
        private List<CETourApplicant> _tourApplicants = null;

        public CETourApplicants()
        {
            RetrieveTourApplicants();
        }

        public List<CETourApplicant> TourApplicants
        {
            get { return _tourApplicants; }
        }

        public List<ReviewedApplicant> CreateReviewedApplicants()
        {
            List<ReviewedApplicant> reviewApplicants = new List<ReviewedApplicant>();
            foreach (CETourApplicant applicant in _tourApplicants)
            {
                ReviewedApplicant reviewApplicant = new ReviewedApplicant();
                reviewApplicant.ResumeFile = applicant.ResumeFile;
                reviewApplicant.LessonPlanFile = applicant.LessonPlanFile;
                reviewApplicant.UserFile1 = applicant.UserFile1;
                reviewApplicant.UserFile2 = applicant.UserFile2;
                reviewApplicant.UserComment = applicant.UserComment;
                reviewApplicant.ApplicantFile = applicant.ApplicantFile;
                reviewApplicant.ApplicantName = applicant.ApplicantName;
                reviewApplicant.Email = applicant.Email;
                reviewApplicant.Phone = string.IsNullOrEmpty(applicant.Phone) ? applicant.CellPhone : applicant.Phone;
                reviewApplicant.District = applicant.District;
                reviewApplicant.School = applicant.School;
                reviewApplicant.Grade = applicant.Grade;
                reviewApplicant.Subject = applicant.Subject;
                reviewApplicant.Gender = applicant.Gender;
                reviewApplicant.EntryDate = DateTime.Parse(applicant.EntryDate).ToString("MM/dd/yy");
                reviewApplicant.Status = applicant.Status;
                //reviewApplicant.ReviewComments = applicant.ReviewComments;
                reviewApplicants.Add(reviewApplicant);
            }
            return reviewApplicants;
        }

        public static string MakeApplicationFolder()
        {
            string folder = CEHelper.GetDataPath() + CEConstants.CE_TOUR_APPLICANT_FOLDER + CEHelper.GetCompetitionYear();
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            return folder;
        }

        private void RetrieveTourApplicants()
        {
            // ensure a fresh positive start
            if (_tourApplicants == null)
                _tourApplicants = new List<CETourApplicant>();
            else
                _tourApplicants.Clear();

            // get the list of applicant data files from \data\application\tours folder
            string tourApplicantFolder = CETourApplicants.MakeApplicationFolder();
            if (!Directory.Exists(tourApplicantFolder))
            {
                Directory.CreateDirectory(tourApplicantFolder);
            }

            string[] applicantFiles = Directory.GetFiles(tourApplicantFolder, "*.xml", SearchOption.TopDirectoryOnly);

            // read each one to assemble the applicant list
            for (int i = 0; i < applicantFiles.Length; i++)
            {
                _tourApplicants.Add(new CETourApplicant(applicantFiles[i]));
            }
        }
    }

    [Serializable]
    public class ReviewedApplicant
    {
        public string ResumeFile { get; set; }
        public string LessonPlanFile { get; set; }
        public string UserFile1 { get; set; }
        public string UserFile2 { get; set; }
        public string UserComment { get; set; }
        public string ApplicantFile { get; set; }
        public string ApplicantName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string District { get; set; }
        public string School { get; set; }
        public string Grade { get; set; }
        public string Subject { get; set; }
        public string Gender { get; set; }
        public string EntryDate { get; set; }
        public ReviewStatus Status { get; set; }
    }

    [Serializable]
    public class CETourApplicant
    {
        public CETourApplicant()
        {
            EntryDate = string.Empty;
            ReviewDate = string.Empty;
        }

        public CETourApplicant(string applicantFile)
        {
            RetrieveTourApplicantData(applicantFile);
        }

        public string ResumeFile { get; set; }
        public string LessonPlanFile { get; set; }
        public string UserFile1 { get; set; }
        public string UserFile2 { get; set; }
        public string UserComment { get; set; }
        public string ApplicantFile { get; set; }
        public string ApplicantName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CellPhone { get; set; }
        public string District { get; set; }
        public string School { get; set; }
        public string Grade { get; set; }
        public string Subject { get; set; }
        public string Gender { get; set; }
        public string ProgramQuestion { get; set; }
        public string Program { get; set; }
        public string SpecialtyQuestion { get; set; }
        public string Specialty { get; set; }
        public string Reference1 { get; set; }
        public string Reference2 { get; set; }
        public string Reference3 { get; set; }
        public QuestionaireType Questionaire1 { get; set; }
        public QuestionaireType Questionaire2 { get; set; }
        public QuestionaireType Questionaire3 { get; set; }
        public QuestionaireType Questionaire4 { get; set; }
        public string EntryDate { get; set; }
        public string ReviewDate { get; set; }
        public ReviewStatus Status { get; set; }
        public List<CommitteeComment>  ReviewComments { get; set; }

        public bool UpdateTourApplicationFile()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine(CEConstants.APPLICATION_XML_BEGIN_TEMPLATE);

            // page 1: personal information
            sb.AppendLine("\t\t<firstName>" + this.FirstName + "</firstName>");
            sb.AppendLine("\t\t<lastName>" + this.LastName + "</lastName>");
            sb.AppendLine("\t\t<email>" + this.Email + "</email>");
            sb.AppendLine("\t\t<phone>" + this.Phone + "</phone>");
            sb.AppendLine("\t\t<cellphone>" + this.CellPhone + "</cellphone>");
            sb.AppendLine("\t\t<district>" + HttpUtility.HtmlEncode(this.District) + "</district>");
            sb.AppendLine("\t\t<school>" + HttpUtility.HtmlEncode(this.School) + "</school>");
            sb.AppendLine("\t\t<grade>" + HttpUtility.HtmlEncode(this.Grade) + "</grade>");
            sb.AppendLine("\t\t<subject>" + HttpUtility.HtmlEncode(this.Subject) + "</subject>");
            sb.AppendLine("\t\t<gender>" + this.Gender + "</gender>");
            sb.AppendLine("\t\t<entryDate>" + this.EntryDate + "</entryDate>");

            // page 2: references
            sb.AppendLine("\t\t<learnProgram question=\"" + CEConstants.HOW_YOU_LEARN_OUR_PROGRAM + "\">" + HttpUtility.HtmlEncode(this.Program) + "</learnProgram>");
            sb.AppendLine("\t\t<specialty question=\"" + CEConstants.TEACHING_SPECIALTY + "\">" + HttpUtility.HtmlEncode(this.Specialty) + "</specialty>");
            sb.AppendLine("\t\t<reference1>" + HttpUtility.HtmlEncode(this.Reference1) + "</reference1>");
            sb.AppendLine("\t\t<reference2>" + HttpUtility.HtmlEncode(this.Reference2) + "</reference2>");
            sb.AppendLine("\t\t<reference3>" + HttpUtility.HtmlEncode(this.Reference3) + "</reference3>");

            // page 3: questionaire
            sb.AppendLine("\t\t<questionaire1 question=\"" + this.Questionaire1.Questionaire + "\">" + HttpUtility.HtmlEncode(this.Questionaire1.Answer) + "</questionaire1>");
            sb.AppendLine("\t\t<questionaire2 question=\"" + this.Questionaire2.Questionaire + "\">" + HttpUtility.HtmlEncode(this.Questionaire2.Answer) + "</questionaire2>");
            sb.AppendLine("\t\t<questionaire3 question=\"" + this.Questionaire3.Questionaire + "\">" + HttpUtility.HtmlEncode(this.Questionaire3.Answer) + "</questionaire3>");
            sb.AppendLine("\t\t<questionaire4 question=\"" + this.Questionaire4.Questionaire + "\">" + HttpUtility.HtmlEncode(this.Questionaire4.Answer) + "</questionaire4>");

            // page 4: uploaded files
            sb.AppendLine("\t\t<resume>" + this.ResumeFile + "</resume>");
            sb.AppendLine("\t\t<lessonPlan>" + this.LessonPlanFile + "</lessonPlan>");
            sb.AppendLine("\t\t<userFile1>" + this.UserFile1 + "</userFile1>");
            sb.AppendLine("\t\t<userFile2>" + this.UserFile2 + "</userFile2>");
            sb.AppendLine("\t\t<userComment>" + HttpUtility.HtmlEncode(this.UserComment) + "</userComment>");

            // page 5: review status
            sb.AppendLine("\t\t<status>" + this.Status.ToString() + "</status>");
            sb.AppendLine("\t\t<reviewDate>" + this.ReviewDate + "</reviewDate>");

            // page 6: reviewer comments
            sb.AppendLine("\t\t<committeeComments>");
            string datestr = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            foreach (CommitteeComment comment in this.ReviewComments)
            {
                sb.AppendLine(string.Format("\t\t\t<comment user=\"{0}\" displayName=\"{1}\" applicationScore=\"{2}\" relevancyScore=\"{3}\" lessonPlanScore=\"{4}\" totalScore=\"{5}\" updateDate=\"{6}\">\r\n<![CDATA[{7}]]>\r\n\t\t\t</comment>", 
                    comment.Name, comment.DisplayName, comment.ApplicationScore.ToString(), comment.RelevancyScore.ToString(), comment.LessonPlanScore.ToString(), comment.TotalScore.ToString(), comment.UpdateDate, HttpUtility.HtmlEncode(comment.Comment)));
            }
            sb.AppendLine("\t\t</committeeComments>");

            sb.AppendLine(CEConstants.APPLICATION_XML_END_TEMPLATE);

            string tourApplicantFolder = CETourApplicants.MakeApplicationFolder();
            if (!Directory.Exists(tourApplicantFolder))
            {
                Directory.CreateDirectory(tourApplicantFolder);
            }

            string xmlFile = Path.Combine(tourApplicantFolder, ApplicantFile);
            return CEHelper.WaitAndWrite(xmlFile, sb.ToString(), false, true);
        }

        public bool UpdateApplicantStatus(ReviewStatus status)
        {
            Status = status; // new status
            return UpdateTourApplicationFile();
        }

        public bool RetrieveTourApplicantData(string xmlFile)
        {
            bool ok = false;
            try
            {
                XDocument xdoc = XDocument.Load(xmlFile);
                if (xdoc != null)
                {
                    ApplicantFile = Path.GetFileName(xmlFile);
                    XElement application = xdoc.Element("ce").Element("application");
                    ResumeFile = CEHelper.GetSafeElementText(application.Element("resume"));
                    LessonPlanFile = CEHelper.GetSafeElementText(application.Element("lessonPlan"));
                    UserFile1 = CEHelper.GetSafeElementText(application.Element("userFile1"));
                    UserFile2 = CEHelper.GetSafeElementText(application.Element("userFile2"));
                    UserComment = CEHelper.GetSafeElementText(application.Element("userComment"));
                    FirstName = CEHelper.GetSafeElementText(application.Element("firstName"));
                    LastName = CEHelper.GetSafeElementText(application.Element("lastName"));
                    ApplicantName = FirstName + " " + LastName;
                    Email = CEHelper.GetSafeElementText(application.Element("email"));
                    Phone = CEHelper.GetSafeElementText(application.Element("phone"));
                    CellPhone = CEHelper.GetSafeElementText(application.Element("cellphone"));
                    District = CEHelper.GetSafeElementText(application.Element("district"));
                    School = HttpUtility.HtmlDecode(CEHelper.GetSafeElementText(application.Element("school")));
                    Grade = HttpUtility.HtmlDecode(CEHelper.GetSafeElementText(application.Element("grade")));
                    Subject = HttpUtility.HtmlDecode(CEHelper.GetSafeElementText(application.Element("subject")));
                    Gender = CEHelper.GetSafeElementText(application.Element("gender"));
                    ProgramQuestion = CEHelper.GetSafeAttribute(application.Element("learnProgram"), "question");
                    Program = HttpUtility.HtmlDecode(CEHelper.GetSafeElementText(application.Element("learnProgram")));
                    SpecialtyQuestion = CEHelper.GetSafeAttribute(application.Element("specialty"), "question");
                    Specialty = HttpUtility.HtmlDecode(CEHelper.GetSafeElementText(application.Element("specialty")));
                    Reference1 = CEHelper.GetSafeElementText(application.Element("reference1"));
                    Reference2 = CEHelper.GetSafeElementText(application.Element("reference2"));
                    Reference3 = CEHelper.GetSafeElementText(application.Element("reference3"));
                    Questionaire1 = new QuestionaireType(CEHelper.GetSafeAttribute(application.Element("questionaire1"), "question"), HttpUtility.HtmlDecode(CEHelper.GetSafeElementText(application.Element("questionaire1"))));
                    Questionaire2 = new QuestionaireType(CEHelper.GetSafeAttribute(application.Element("questionaire2"), "question"), HttpUtility.HtmlDecode(CEHelper.GetSafeElementText(application.Element("questionaire2"))));
                    Questionaire3 = new QuestionaireType(CEHelper.GetSafeAttribute(application.Element("questionaire3"), "question"), HttpUtility.HtmlDecode(CEHelper.GetSafeElementText(application.Element("questionaire3"))));
                    Questionaire4 = new QuestionaireType(CEHelper.GetSafeAttribute(application.Element("questionaire4"), "question"), HttpUtility.HtmlDecode(CEHelper.GetSafeElementText(application.Element("questionaire4"))));
                    EntryDate = CEHelper.GetSafeElementText(application.Element("entryDate"));
                    ReviewDate = CEHelper.GetSafeElementText(application.Element("reviewDate"));
                    string status = CEHelper.GetSafeElementText(application.Element("status"));
                    if (string.IsNullOrEmpty(status))
                        Status = ReviewStatus.Review;
                    else
                        Status = (ReviewStatus)Enum.Parse(typeof(ReviewStatus), status, true);

                    // committee comments
                    ReviewComments = RetrieveReviewComments(application.Element("committeeComments"));

                    ok = true;
                }
            }
            catch
            {
            }
            return ok;
        }

        private List<CommitteeComment> RetrieveReviewComments(XElement commentsTag)
        {
            CommitteeComments reviewComments = new CommitteeComments();
            if (commentsTag != null && reviewComments != null)
            {
                IEnumerable<XElement> commentTags = commentsTag.Elements("comment");
                foreach (XElement commentTag in commentTags)
                {
                    string user = CEHelper.GetSafeAttribute(commentTag, "user");
                    string displayName = CEHelper.GetSafeAttribute(commentTag, "displayName");
                    string updateDate = CEHelper.GetSafeAttribute(commentTag, "updateDate");
                    int applicationScore = CEHelper.GetSafeAttributeInt(commentTag, "applicationScore", 0);
                    int relevancyScore = CEHelper.GetSafeAttributeInt(commentTag, "relevancyScore", 0);
                    int lessonPlanScore = CEHelper.GetSafeAttributeInt(commentTag, "lessonPlanScore", 0);
                    string comment = HttpUtility.HtmlDecode(CEHelper.GetSafeElementText(commentTag));
                    CommitteeComment committeeComment = new CommitteeComment(user, displayName, updateDate, comment, applicationScore, relevancyScore, lessonPlanScore);
                    reviewComments.AddComment(committeeComment);
                }
            }
            return reviewComments.Comments;
        }

        public CETourApplicant UpdateComment(string username, string displayName, string comment, int applicationScore, int relevancyScore, int lessonPlanScore)
        {
            bool found = false;
            foreach (CommitteeComment c in ReviewComments)
            {
                if (string.Compare(c.Name, username, true) == 0)
                {
                    c.UpdateDate = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                    c.Comment = comment;
                    c.ApplicationScore = applicationScore;
                    c.RelevancyScore = relevancyScore;
                    c.LessonPlanScore = lessonPlanScore;
                    c.TotalScore = applicationScore + relevancyScore + lessonPlanScore;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                string updateDate = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                CommitteeComment committeeComment = new CommitteeComment(username, displayName, updateDate, comment, applicationScore, relevancyScore, lessonPlanScore);
                ReviewComments.Add(committeeComment);
            }

            return this;
        }
    }

    [Serializable]
    public class QuestionaireType
    {
        public QuestionaireType(string q, string a)
        {
            Questionaire = q;
            Answer = a;
        }
        public string Questionaire { get; set; }
        public string Answer { get; set; }
    }

    [Serializable]
    public class CommitteeComments
    {
        private List<CommitteeComment> _comments = null;
        public CommitteeComments()
        {
            _comments = new List<CommitteeComment>();
        }

        public List<CommitteeComment> Comments
        {
            get { return _comments; }
        }

        public int AddComment(CommitteeComment comment)
        {
            if (_comments == null) _comments = new List<CommitteeComment>();
            _comments.Add(comment);
            return _comments.Count;
        }
    }

    [Serializable]
    public class CommitteeComment
    {
        public CommitteeComment(string name, string displayName, string updateDate, string comment, int applicationScore, int relevancyScore, int lessonPlanScore)
        {
            Name = name;
            DisplayName = displayName;
            UpdateDate = updateDate;
            Comment = comment;
            ApplicationScore = applicationScore;
            RelevancyScore = relevancyScore;
            LessonPlanScore = lessonPlanScore;
            TotalScore = applicationScore + relevancyScore + lessonPlanScore;
        }

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string UpdateDate { get; set; }
        public string Comment { get; set; }
        public int ApplicationScore { get; set; }
        public int RelevancyScore { get; set; }
        public int LessonPlanScore { get; set; }
        public int TotalScore { get; set; }
    }

    public enum ReviewStatus
    {
        Apply = 0,
        Withdraw = 1,
        Review = 2,
        Rejected = 3,
        Interview = 4,
        Awarded = 5,
        Unknown = 10
    }
}