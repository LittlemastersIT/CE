using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Web;
using CE.Data;

namespace CE.Data
{
    public class CETalentApplciants
    {
        private List<CECompetitionEntry> _talentApplicants = null;

        public CETalentApplciants(bool excludeRejected = true)
        {
            RetrieveTourApplicants(excludeRejected);
        }

        public List<CECompetitionEntry> TalentApplicants
        {
            get { return _talentApplicants; }
        }

        private void RetrieveTourApplicants(bool excludeRejected)  //shall be TalentApplicants too
        {
            // ensure a fresh positive start
            if (_talentApplicants == null)
                _talentApplicants = new List<CECompetitionEntry>();
            else
                _talentApplicants.Clear();

            // get the list of applicant data files from \data\application\talent folder
            string talentApplicantFolder = CETalentApplciants.MakeRegistrationFolder();
            string[] applicantFiles = Directory.GetFiles(talentApplicantFolder, "*.xml", SearchOption.TopDirectoryOnly);

            // read each one to assemble the applicant list
            for (int i = 0; i < applicantFiles.Length; i++)
            {
                CECompetitionEntry entry = new CECompetitionEntry(applicantFiles[i]);
                if (excludeRejected && entry.Status == RegistrationStatus.Rejected) continue;
                _talentApplicants.Add(entry);
            }
        }

        public List<RegisteredApplicant> CreateRegisteredApplicants()
        {
            List<RegisteredApplicant> registeredApplicants = new List<RegisteredApplicant>();
            foreach (CECompetitionEntry entry in _talentApplicants)
            {
                RegisteredApplicant registeredApplicant = new RegisteredApplicant();
                registeredApplicant.File = entry.ContestantFile;
                registeredApplicant.EntryDate = entry.EntryDate;
                registeredApplicant.Status = entry.Status;
                registeredApplicant.ContestantCount = entry.Contestants.Count;
                registeredApplicant.Payment = entry.PaymentAmount;
                registeredApplicant.PaymentMethod = entry.PaymentMethod;
                registeredApplicant.ContactName = entry.Contact.ContactName;
                registeredApplicant.ContactPhone = entry.Contact.ContactPhone;
                registeredApplicant.ContactEmail = entry.Contact.ContactEmail;
                registeredApplicant.Category = entry.Contact.Category;
                registeredApplicant.SubCategory = entry.Contact.SubCategory;
                registeredApplicant.Division = entry.Contact.Division;
                registeredApplicant.IsTeam = entry.Contact.IsTeam;
                registeredApplicant.TeamName = entry.Contact.TeamName;
                registeredApplicant.Class = entry.Contact.Class;
                if (entry.Contact.IsTeam || entry.Contestants.Count == 0)
                {
                    registeredApplicant.Student = string.Empty;
                    foreach (CEContestant contestant in entry.Contestants)
                    {
                        registeredApplicant.LunchCount += (contestant.LunchProgram.ToLower() == "yes" ? 1 : 0);
                    }
                }
                else
                {
                    registeredApplicant.Student = entry.Contestants[0].ID; // entry.Contestants[0].FirstName + " " + entry.Contestants[0].LastName;
                    registeredApplicant.LunchCount = (entry.Contestants[0].LunchProgram.ToLower() == "yes" ? 1 : 0);
                }

                registeredApplicants.Add(registeredApplicant);
            }
            return registeredApplicants;
        }

        public static bool TalentApplicantExist(CECompetitionEntry applicant, out string categoryExist, bool isIndividual=false)
        {
            categoryExist = string.Empty;
            bool exist = false;
            try
            {
                string talentApplicantFolder = CETalentApplciants.MakeRegistrationFolder();
                string[] applicantFiles = Directory.GetFiles(talentApplicantFolder, "*.xml", SearchOption.TopDirectoryOnly);
                if (isIndividual)
                {
                    for (int i = 0; i < applicantFiles.Length; i++)
                    {
                        // registration filename format: contactname_phone_category_class_division_first_last_birthday_competitionyear.xml
                        string[] tokens = applicantFiles[i].Split(new char[] { '_' });
                        if (tokens.Count() == 9) // individual compeititon
                        {
                            string category = tokens[2];
                            string ceClass = tokens[3] == "0" ? string.Empty : tokens[3];
                            string division = tokens[4];
                            string id = CEContestant.MakeID(tokens[5], tokens[6], tokens[7]);
                            string categoryClass = CETalentApplciants.GetCategoryClass(applicant);
                            if (id == applicant.Contestants[0].ID && category == applicant.Contact.Category && division == applicant.Contact.Division && ceClass == categoryClass)
                            {
                                if (categoryExist != string.Empty) categoryExist += ",";
                                categoryExist += category;
                                exist = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    string filename = applicant.MakeRegistrationFilename().ToLower();
                    for (int i = 0; i < applicantFiles.Length; i++)
                    {
                        if (applicantFiles[i].ToLower().EndsWith(filename))
                        {
                            exist = true;
                            break;
                        }
                    }
                }
            }
            catch
            {
            }
            return exist;
        }

        public static void RemoveTalentApplicant(string filename)
        {
            try
            {
                string talentApplicantFolder = CETalentApplciants.MakeRegistrationFolder();
                if (!string.IsNullOrEmpty(filename))
                {
                    string xmlPath = Path.Combine(talentApplicantFolder, filename);
                    File.Delete(xmlPath);
                }
            }
            catch
            {
            }
        }

        public static string GetCategoryClass(CECompetitionEntry applicant)
        {
            if (applicant.Contact.Category == CompetitionControlData.CHESS || applicant.Contact.Category == CompetitionControlData.DRAWING || applicant.Contact.Category.StartsWith("Team"))
                return string.Empty;
            else
                return applicant.Contact.Class;
        }

        public static string MakeRegistrationFolder()
        {
            return CEHelper.GetDataPath() + CEConstants.CE_TALENT_REGISTRATION_FOLDER + CEHelper.GetCompetitionYear();
        }
    }

    [Serializable]
    public class RegisteredApplicant : CECompetitionContact
    {
        public RegisteredApplicant() : base()
        {
            File = string.Empty;
            Status = 0;
            ContestantCount = 0;
            Payment = 0;
            PaymentMethod = PaymentType.None;
            LunchCount = 0;
        }

        public RegisteredApplicant(string xmlFile)
        {
            File = string.Empty;
            Status = 0;
            ContestantCount = 0;
            Payment = 0;
            PaymentMethod = PaymentType.None;
            CheckNumber = string.Empty;
            LunchCount = 0;
            Award = AwardType.None;

            RetrieveApplicantData(xmlFile);
        }

        public string File { get; set; }
        public RegistrationStatus Status { get; set; }
        public string EntryDate { get; set; }
        public string Student { get; set; }
        public int ContestantCount { get; set; }
        public double Payment { get; set; }
        public PaymentType PaymentMethod { get; set; }
        public string CheckNumber { get; set; }
        public int LunchCount { get; set; }
        public AwardType Award { get; set; }

        private bool RetrieveApplicantData(string xmlFile)
        {
            bool ok = false;
            try
            {
                XDocument xdoc = XDocument.Load(xmlFile);
                if (xdoc != null)
                {
                    this.File = Path.GetFileName(xmlFile);
                    XElement registration = xdoc.Element("ce").Element("registration");

                    double amount;
                    if (double.TryParse(CEHelper.GetSafeElementText(registration.Element("paymentAmount")), out amount) == true)
                        this.Payment = amount;

                    string status = CEHelper.GetSafeElementText(registration.Element("status"));
                    if (string.IsNullOrEmpty(status))
                        this.Status = (int)RegistrationStatus.Apply;
                    else
                        this.Status = (RegistrationStatus)Enum.Parse(typeof(RegistrationStatus), status, true);

                    this.EntryDate = CEHelper.GetSafeElementText(registration.Element("entryDate"));
                    string paymentMethod = CEHelper.GetSafeElementText(registration.Element("paymentMethod"));
                    if (string.IsNullOrEmpty(status))
                        this.Status = (int)RegistrationStatus.Apply;
                    else
                        this.PaymentMethod = (PaymentType)Enum.Parse(typeof(PaymentType), paymentMethod, true);

                    XElement contact = registration.Element("contact");
                    this.ContactName = CEHelper.GetSafeElementText(contact.Element("name"));
                    this.ContactEmail = CEHelper.GetSafeElementText(contact.Element("email"));
                    this.ContactPhone = CEHelper.GetSafeElementText(contact.Element("phone"));
                    XElement competition = registration.Element("competition");
                    this.TeamName = CEHelper.GetSafeAttribute(competition, "teamName");
                    this.IsTeam = string.IsNullOrEmpty(this.TeamName) ? false : true;
                    this.Category = CEHelper.GetSafeElementText(competition.Element("category"));
                    this.Class = CEHelper.GetSafeElementText(competition.Element("class"));
                    this.Division = CEHelper.GetSafeElementText(competition.Element("division"));

                    IEnumerable<XElement> contestants = registration.Element("contestants").Elements("contestant");
                    this.ContestantCount = contestants.Count<XElement>();
                    foreach (XElement contestant in contestants)
                    {
                        LunchCount += CEHelper.GetSafeElementText(contestant.Element("lunchProgram")).ToLower() == "yes" ? 1 : 0;
                    }

                    ok = true;
                }
            }
            catch
            {
                ok = false;
            }
            return ok;
        }
    }

    [Serializable]
    public class CECompetitionEntry
    {
        private const string FILTENAME_TEMPLATE = "{0}_{1}_{2}_{3}_{4}_{5}_{6}_{7}.xml";
        private const string APPLICATION_XML_BEGIN_TEMPLATE = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n<ce>\r\n\t<registration>";
        private const string TROPHY_TEMPLATE = "\t\t<award>{0}</award>\r\n";
        private const string PAYMENT_TEMPLATE = "\t\t<paymentMethod>{0}</paymentMethod>\r\n\t\t<paymentAmount>{1}</paymentAmount>\r\n\t\t<checkNumber>{2}</checkNumber>\r\n";
        private const string STATUS_TEMPLATE = "\t\t<status>{0}</status>\r\n\t\t<entryDate>{1}</entryDate>\r\n\t\t<processDate>{2}</processDate>\r\n";
        private const string CONTACT_TEMPLATE = "\t\t<contact>\r\n\t\t\t<name>{0}</name>\r\n\t\t\t<email>{1}</email>\r\n\t\t\t<phone>{2}</phone>\r\n\t\t</contact>\r\n";
        private const string COMPETITION_TEMPLATE = "\t\t<competition teamName=\"{0}\">\r\n\t\t\t<category>{1}</category>\r\n\t\t\t<subcategory>{2}</subcategory>\r\n\t\t\t<class>{3}</class>\r\n\t\t\t<division>{4}</division>\r\n\t\t\t<ispianorequired>{5}</ispianorequired>\r\n\t\t</competition>\r\n";
        private const string CONTESTANT_BLOCK_BEGIN = "\t\t<contestants>";
        private const string CONTESTANT_TEMPLATE = "\t\t\t<contestant>\r\n\t\t\t\t<firstName>{0}</firstName>\r\n\t\t\t\t<lastName>{1}</lastName>\r\n\t\t\t\t<chineseName>{2}</chineseName>\r\n\t\t\t\t<birthday>{3}</birthday>\r\n\t\t\t\t<email>{4}</email>\r\n\t\t\t\t<academicSchool>{5}</academicSchool>\r\n\t\t\t\t<extracurricularSchool>{6}</extracurricularSchool>\r\n\t\t\t\t<grade>{7}</grade>\r\n\t\t\t\t<lunchProgram>{8}</lunchProgram>\r\n\t\t\t\t<studentId>{9}</studentId>\r\n\t\t\t</contestant>\r\n";
        private const string CONTESTANT_BLOCK_END = "\t\t</contestants>";
        private const string APPLICATION_XML_END_TEMPLATE = "\t</registration>\r\n</ce>";

        private CECompetitionContact _contact;
        private List<CEContestant> _contestants;

        public CECompetitionEntry()
        {
            _contestants = new List<CEContestant>();
            PaymentMethod = PaymentType.None;
            PaymentAmount = 0;
            CheckNumber = string.Empty;
            Award = AwardType.None;
        }

        public CECompetitionEntry(string xmlFile)
        {
            RetrieveTalentCompetitionEntry(xmlFile);
        }

        public string ContestantFile { get; set; }
        public string EntryDate { get; set; }
        public string ProcessDate { get; set; }
        public double PaymentAmount { get; set; }
        public PaymentType PaymentMethod { get; set; }
        public string CheckNumber { get; set; }
        public RegistrationStatus Status { get; set; }
        public AwardType Award { get; set; }
        public CECompetitionContact Contact 
        {
            get { return _contact; }
        }
        public List<CEContestant> Contestants 
        {
            get { return _contestants; }
        }

        public void AddStatus(RegistrationStatus status)
        {
            this.Status = status;
            this.EntryDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            this.ProcessDate = string.Empty;
        }

        public void AddContact(string name, string email, string phone)
        {
            if (_contact == null) _contact = new CECompetitionContact();
            _contact.ContactName = name.Trim();
            _contact.ContactEmail = email.Trim();
            _contact.ContactPhone = phone.Trim();
        }

        public void AddCompetition(string category, string classType, string division, string teamName, string subCategory, string isPianoRequired = "false")
        {
            if (_contact == null) _contact = new CECompetitionContact();
            _contact.Category = category;

            if (category == CompetitionControlData.TEAM_TALENT)
            {
                _contact.SubCategory = subCategory;
                _contact.IsPianoRequired = isPianoRequired; 
            }
            else
                _contact.SubCategory = string.Empty;

            _contact.Class = classType;
            _contact.Division = division;
            _contact.TeamName = teamName.Trim();
            _contact.IsTeam = string.IsNullOrEmpty(teamName) ? false : true;
        }

        public void AddContestant(CEContestant contestant)
        {
            if (_contestants == null) _contestants = new List<CEContestant>();
            _contestants.Add(contestant);
        }

        public void AddContestant(int row, string id, string firstName, string lastName, string chineseName, string birthday, string email, string school1, string school2, string grade, string lunchProgram)
        {
            CEContestant contestant = new CEContestant(row);
            contestant.FirstName = firstName;
            contestant.LastName = lastName;
            contestant.ChineseName = chineseName;
            contestant.Birthday = birthday;
            contestant.Email = email;
            contestant.School = school1;
            contestant.School2 = school2;
            contestant.Grade = grade;
            contestant.LunchProgram = lunchProgram;
            contestant.ID = id;
            AddContestant(contestant);
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Contact.TeamName))
            {
                if (Contestants.Count<CEContestant>() > 0)
                    return string.Format("{0}{1}-{2}-{3}-{4}", Contestants[0].FirstName, Contestants[0].LastName, Contact.Category, Contact.Class, Contact.Division);
                else
                    return string.Format("{0}-{1}-{2}-{3}", Contact.ContactName, Contact.Category, Contact.Class, Contact.Division);
            }
            else
                return string.Format("{0}-{1}-{2}-{3}", Contact.TeamName, Contact.Category, Contact.Class, Contact.Division);
        }

        public bool ExportRegistration(PaymentType type)
        {
            if (type == PaymentType.Pending)
                return ExportRegistration(RegistrationStatus.Pending);
            else
                return ExportRegistration(RegistrationStatus.Apply);
        }

        public bool ExportRegistration(RegistrationStatus status, AwardType award = AwardType.Unknown)
        {
            if (status != RegistrationStatus.Unknown) this.Status = status;
            if (award != AwardType.Unknown) this.Award = award;

            if (this.Status != RegistrationStatus.Apply && this.Status != RegistrationStatus.Pending) this.ProcessDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            this.ContestantFile = MakeRegistrationFilename();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine(APPLICATION_XML_BEGIN_TEMPLATE);
            if (award != AwardType.Unknown && award != AwardType.None) sb.AppendFormat(TROPHY_TEMPLATE, this.Award.ToString());
            sb.AppendFormat(PAYMENT_TEMPLATE, new string[] { this.PaymentMethod.ToString(), this.PaymentAmount.ToString(), this.CheckNumber });
            sb.AppendFormat(STATUS_TEMPLATE, new string[] { this.Status.ToString(), this.EntryDate, this.ProcessDate });
            sb.AppendFormat(CONTACT_TEMPLATE, new string[] { this._contact.ContactName, this._contact.ContactEmail, this._contact.ContactPhone });
            sb.AppendFormat(COMPETITION_TEMPLATE, new string[] { this._contact.TeamName, this._contact.Category, this._contact.SubCategory, this._contact.Class, this._contact.Division, this._contact.IsPianoRequired });
            sb.AppendLine(CONTESTANT_BLOCK_BEGIN);
            foreach (CEContestant contestant in this._contestants)
            {
                if (!string.IsNullOrEmpty(contestant.FirstName) && !string.IsNullOrEmpty(contestant.LastName) && !string.IsNullOrEmpty(contestant.Birthday))
                    sb.AppendFormat(CONTESTANT_TEMPLATE, new string[] { contestant.FirstName, contestant.LastName, contestant.ChineseName, contestant.Birthday, contestant.Email, HttpUtility.HtmlEncode(contestant.School), HttpUtility.HtmlEncode(contestant.School2), contestant.Grade, contestant.LunchProgram, contestant.ID });
            }
            sb.AppendLine(CONTESTANT_BLOCK_END);
            sb.AppendLine(APPLICATION_XML_END_TEMPLATE);

            bool ok = false;
            string applicantFolder = CETalentApplciants.MakeRegistrationFolder();
            string xmlFile = Path.Combine(applicantFolder, this.ContestantFile);
            ok = CEHelper.WaitAndWrite(xmlFile, sb.ToString(), false, true);
            return ok;
        }

        public string MakeRegistrationFilename()
        {
            string contestantName = ((Contact.IsTeam || Contestants.Count <= 0) ? _contact.TeamName.Replace("-","") : Contestants[0].FirstName + "_" + Contestants[0].LastName);
            string contestantBirthday = ((Contact.IsTeam || Contestants.Count <= 0) ? "0" : Contestants[0].Birthday.Replace("/","-"));
            return string.Format(FILTENAME_TEMPLATE,
                                NeutralizeFilenameText(_contact.ContactName),
                                NeutralizeFilenameText(_contact.ContactPhone),
                                NeutralizeFilenameText(_contact.Category),
                                NeutralizeFilenameText(_contact.Class),
                                NeutralizeFilenameText(_contact.Division),
                                NeutralizeFilenameText(contestantName),
                                contestantBirthday,
                                CEHelper.GetCompetitionYear());
        }

        private bool RetrieveTalentCompetitionEntry(string xmlFile)
        {
            bool ok = false;
            try
            {
                // ensure a fresh positive start
                if (_contact == null)
                {
                    _contact = new CECompetitionContact();
                    _contestants = new List<CEContestant>();
                }
                else
                {
                    _contestants.Clear();
                }

                XDocument xdoc = XDocument.Load(xmlFile);
                if (xdoc != null)
                {
                    this.ContestantFile = Path.GetFileName(xmlFile);
                    XElement registration = xdoc.Element("ce").Element("registration");

                    string award = CEHelper.GetSafeElementText(registration.Element("award"));
                    this.Award = AwardType.None;
                    if (!string.IsNullOrEmpty(award)) this.Award = (AwardType)Enum.Parse(typeof(AwardType), award, true);

                    string paymentMethod = CEHelper.GetSafeElementText(registration.Element("paymentMethod"));
                    if (string.IsNullOrEmpty(paymentMethod))
                        this.PaymentMethod = PaymentType.None;
                    else
                        this.PaymentMethod = (PaymentType)Enum.Parse(typeof(PaymentType), paymentMethod, true);
                    this.PaymentAmount = 0.0;
                    double amount;
                    if (double.TryParse(CEHelper.GetSafeElementText(registration.Element("paymentAmount")), out amount) == true)
                        this.PaymentAmount = amount;

                    this.CheckNumber = CEHelper.GetSafeElementText(registration.Element("checkNumber"));

                    string status = CEHelper.GetSafeElementText(registration.Element("status"));
                    if (string.IsNullOrEmpty(status))
                        this.Status = RegistrationStatus.Apply;
                    else
                        this.Status = (RegistrationStatus)Enum.Parse(typeof(RegistrationStatus), status, true);

                    this.EntryDate = CEHelper.GetSafeElementText(registration.Element("entryDate"));
                    this.ProcessDate = CEHelper.GetSafeElementText(registration.Element("processDate"));

                    XElement contact = registration.Element("contact");
                    _contact.ContactName = CEHelper.GetSafeElementText(contact.Element("name"));
                    _contact.ContactEmail = CEHelper.GetSafeElementText(contact.Element("email"));
                    _contact.ContactPhone = CEHelper.GetSafeElementText(contact.Element("phone"));
                    XElement competition = registration.Element("competition");
                    _contact.TeamName = CEHelper.GetSafeAttribute(competition, "teamName");
                    _contact.IsTeam = string.IsNullOrEmpty(_contact.TeamName) ? false : true;
                    _contact.Category = CEHelper.GetSafeElementText(competition.Element("category"));
                    _contact.SubCategory = CEHelper.GetSafeElementText(competition.Element("subcategory"));
                    _contact.IsPianoRequired = CEHelper.GetSafeElementText(competition.Element("ispianorequired")) ;
                    _contact.Class = CEHelper.GetSafeElementText(competition.Element("class"));
                    _contact.Division = CEHelper.GetSafeElementText(competition.Element("division"));

                    IEnumerable<XElement> contestants = registration.Element("contestants").Elements("contestant");
                    int row = 1;
                    foreach (XElement contestant in contestants)
                    {
                        CEContestant participant = new CEContestant(row);
                        participant.LastName = CEHelper.GetSafeElementText(contestant.Element("lastName"));
                        participant.FirstName = CEHelper.GetSafeElementText(contestant.Element("firstName"));
                        participant.ChineseName = CEHelper.GetSafeElementText(contestant.Element("chineseName"));
                        participant.Birthday = CEHelper.GetSafeElementText(contestant.Element("birthday"));
                        participant.Email = CEHelper.GetSafeElementText(contestant.Element("email"));
                        participant.School = HttpUtility.HtmlDecode(CEHelper.GetSafeElementText(contestant.Element("academicSchool")));
                        participant.School2 = HttpUtility.HtmlDecode(CEHelper.GetSafeElementText(contestant.Element("extracurricularSchool")));
                        participant.Grade = CEHelper.GetSafeElementText(contestant.Element("grade"));
                        participant.LunchProgram = CEHelper.GetSafeElementText(contestant.Element("lunchProgram"));
                        participant.ID = CEHelper.GetSafeElementText(contestant.Element("studentId"));
                        _contestants.Add(participant);
                    }

                    ok = true;
                }
            }
            catch
            {
                ok = false;
            }
            return ok;
        }

        private string NeutralizeFilenameText(string s)
        {
            s = s.Replace(" ", string.Empty).Replace("'", string.Empty).Replace("-", string.Empty);
            if (s == string.Empty) s = "0"; // use '0' for no value input
            return s;
        }

        private int GetActualContestantCount(List<CEContestant> contestants)
        {
            int count = 0;
            foreach (CEContestant contestant in contestants)
            {
                if (string.IsNullOrEmpty(contestant.FirstName) || string.IsNullOrEmpty(contestant.LastName) || string.IsNullOrEmpty(contestant.Birthday)) continue;
                count++;
            }
            return count;
        }
    }

    [Serializable]
    public class CECompetitionContact
    {
        public CECompetitionContact()
        {
            ContactName = string.Empty;
            ContactEmail = string.Empty;
            ContactPhone = string.Empty;
            IsTeam = false;
            Category = string.Empty;
            SubCategory = string.Empty;
            IsPianoRequired = string.Empty;  
            Class = string.Empty;
            Division = string.Empty;
            TeamName = string.Empty;
        }

        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public bool IsTeam { get; set; }
        public string TeamName { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string IsPianoRequired { get; set; }
        public string Class { get; set; }
        public string Division { get; set; }
    }

    [Serializable]
    public class CEContestant
    {
        public CEContestant()
        {
            ID = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            ChineseName = string.Empty;
            Birthday = string.Empty;
            Email = string.Empty;
            School = string.Empty;
            School2 = string.Empty;
            Grade = string.Empty;
            LunchProgram = "No";
            Row = 0;
        }

        public CEContestant(int row) : base()
        {
            Row = row;
        }

        public int Row { get; set; }
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ChineseName { get; set; }
        public string Birthday { get; set; }
        public string Email { get; set; }
        public string School { get; set; }
        public string School2 { get; set; }
        public string Grade { get; set; }
        public string LunchProgram { get; set; }

        public string MakeID()
        {
            return ID = MakeID(FirstName, LastName, Birthday);
        }

        public static string MakeID(string firstName, string lastName, string birthday)
        {
            string id = string.Empty;
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(birthday))
            {
                id = firstName + CEConstants.STUDENT_ID_SEPARATOR + lastName + CEConstants.STUDENT_ID_SEPARATOR + birthday.Replace("/", "-");
            }
            return id.ToLower();
        }
    }

    [Serializable]
    public enum RegistrationStatus
    {
        Apply = 0,
        Pending = 1,
        Review = 2,
        Rejected = 3,
        Approved = 4,
        Reminder = 5,
        Unknown = 10
    }

    [Serializable]
    public enum PaymentType
    {
        Paypal = 1,
        MailIn = 2,
        FeeWaived = 3,
        None = 4,
        Pending = 5
    }

    public enum AwardType
    {
        FirstPlace = 1,
        SecondPlace,
        ThirdPlace,
        HonorableMentioned,
        None,
        Unknown
    }

    public enum DivisionType
    {
        K2 = 1,
        Elementary,
        MiddleSchool,
        HighSchool
    }

    public enum CategoryType
    {
        Chess = 1,
        Drawing,
        Poetry,
        Speech,
        Story,
        TeamBowl,
        TeamLanguage,
        TeamPoetry,
        TeamSing,
        TeamTalent,
        ChineseBridge
    }

    public enum IndividualCategoryType
    {
        Chess = 1,
        Drawing,
        Poetry,
        Speech,
        Story
    }

    public enum TeamCategoryType
    {
        TeamBowl = 1,
        TeamLanguage,
        TeamPoetry,
        TeamSing,
        TeamTalent
    }
}