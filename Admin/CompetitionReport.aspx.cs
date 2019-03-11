using System;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;
using DocumentFormat.OpenXml.Packaging;
using Word = Microsoft.Office.Interop.Word;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CE.Data;

namespace CE.Admin
{
    public partial class CompetitionReport : System.Web.UI.Page
    {
        private const string ACTION = "action";
        private const string SIGNIN = "signin";
        private const string HEADCOUNT = "headcount";
        private const string TROPHY = "trophy";
        private const string SCORE = "score";
        private const string CONTESTANT = "contestant";
        private const string CONFLICT = "conflict";
        private const string AWARDS = "awards";
        private const string WINNERS = "winners";
        private const string CERTIFICATE = "certificate";
        private const string GROUP = "group";
        private const string INDIVIDUAL = "individual";
        private const string TEAM = "team";
        private const string REPORT_FILE_FORMAT = "{0}_{1}_{2}_{3}.xlsx";
        private const string CONTESTANT_FILE_FORMAT = "{0}_{1}.xlsx";
        private const string HEADCOUNT_FILE_FORMAT = "{0} Competition Contestant Head Count.xlsx";
        private const string TROPHYCOUNT_FILE_FORMAT = "{0} Competition Trophy Count.xlsx";
        private const string COMPETITION_AWARD_FILE_FORMAT = "{0} Talent Competition Award Presentation.xlsx";
        private const string COMPETITION_WINNER_FILE_FORMAT = "{0} Talent Competition Winner List.xlsx";
        private const string CONFLICT_FILE_FORMAT = "{0} Contestant Conflict Event Schedule.xlsx";

        private bool sendEmailFromReport = false;
        public string CompetitionYear { get; set; }
        public string ConflictFileFormat { get; set; }
        public string TrophyFileFormat { get; set; }
        public string HeadCountFileFormat { get; set; }
        public string CompetitionAwardFormat { get; set; }
        public string CompetitionWinnerFormat { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ConflictFileFormat = CONFLICT_FILE_FORMAT;
                TrophyFileFormat = TROPHYCOUNT_FILE_FORMAT;
                HeadCountFileFormat = HEADCOUNT_FILE_FORMAT;
                CompetitionAwardFormat = COMPETITION_AWARD_FILE_FORMAT;
                CompetitionWinnerFormat = COMPETITION_WINNER_FILE_FORMAT;
                CompetitionYear = CEHelper.GetConfiguration(CEConstants.COMPETITION_YEAR_KEY, "2015");

                string action = Request.QueryString[ACTION];
                string group = Request.QueryString[GROUP];
                string reportYear = CEHelper.GetCompetitionYear();
                string rootFolder = Path.Combine(CEHelper.GetDataPath() + CEConstants.CE_REPORT_FOLDER, reportYear);
                if (((action == SIGNIN || action == SCORE) && (group == INDIVIDUAL || group == TEAM)) ||
                      action == HEADCOUNT || action == CONTESTANT || action == CONFLICT ||
                      action == TROPHY || action == AWARDS || action == WINNERS || action == CERTIFICATE)
                {
                    int index = 0;
                    int totalSheets = 0;
                    string reportFolder = string.Empty;

                    if (action == SIGNIN)
                    {
                        string physicalPath = Path.Combine(rootFolder, CEConstants.CE_SIGNIN_TEMPLATE_XLSX);
                        FileInfo templateFile = new FileInfo(physicalPath);
                        totalSheets = GenerateSignInSheets(rootFolder, templateFile, group);
                        reportFolder = Path.Combine(rootFolder, CEConstants.CE_SIGNIN_SHEET_FOLDER, group);
                        index = reportFolder.IndexOf(CEConstants.CE_DATA_FOLDER) - 1;
                    }
                    else if (action == SCORE)
                    {
                        totalSheets = GenerateScoreSheets(rootFolder, group);
                        reportFolder = Path.Combine(rootFolder, CEConstants.CE_SCORE_SHEET_FOLDER, group);
                        index = reportFolder.IndexOf(CEConstants.CE_DATA_FOLDER) - 1;
                    }
                    else if (action == CONTESTANT)
                    {
                        string physicalPath = Path.Combine(rootFolder, CEConstants.CE_CONTESTANT_TEMPLATE_XLSX);
                        FileInfo templateFile = new FileInfo(physicalPath);
                        totalSheets = GenerateContestantSheets(rootFolder, templateFile);
                        reportFolder = Path.Combine(rootFolder, CEConstants.CE_CONTESTANT_SHEET_FOLDER);
                        index = reportFolder.IndexOf(CEConstants.CE_DATA_FOLDER) - 1;
                    }
                    else if (action == CONFLICT)
                    {
                        string physicalPath = Path.Combine(rootFolder, CEConstants.CE_CONFLICT_TEMPLATE_XLSX);
                        FileInfo templateFile = new FileInfo(physicalPath);
                        totalSheets = GenerateConflictSheet(rootFolder, templateFile);
                        reportFolder = rootFolder;
                        index = reportFolder.IndexOf(CEConstants.CE_DATA_FOLDER) - 1;
                    }
                    else if (action == TROPHY)
                    {
                        string physicalPath = Path.Combine(rootFolder, CEConstants.CE_TROPHY_TEMPLATE_XLSX);
                        FileInfo templateFile = new FileInfo(physicalPath);
                        GenerateTrophySheet(rootFolder, templateFile);
                        reportFolder = rootFolder;
                        index = reportFolder.IndexOf(CEConstants.CE_DATA_FOLDER) - 1;
                    }
                    else if (action == HEADCOUNT)
                    {
                        string physicalPath = Path.Combine(rootFolder, CEConstants.CE_HEADCOUNT_TEMPLATE_XLSX);
                        FileInfo templateFile = new FileInfo(physicalPath);
                        GenerateHeadCountSheet(rootFolder, templateFile);
                    }
                    else if (action == AWARDS)
                    {
                        string physicalPath = Path.Combine(rootFolder, CEConstants.CE_AWARD_PRESENTATION_TEMPLATE_XLSX);
                        FileInfo templateFile = new FileInfo(physicalPath);
                        totalSheets = GenerateAwardPresentationSheet(rootFolder, templateFile);
                    }
                    else if (action == WINNERS)
                    {
                        string physicalPath = Path.Combine(rootFolder, CEConstants.CE_WINNER_TEMPLATE_XLSX);
                        FileInfo templateFile = new FileInfo(physicalPath);
                        totalSheets = GenerateWinnersSheet(rootFolder, templateFile);
                    }
                    else if (action == CERTIFICATE)
                    {
                        string physicalPath = Path.Combine(rootFolder, CEConstants.CE_CERTIFICATE_TEMPLATE_DOCX);
                        FileInfo templateFile = new FileInfo(physicalPath);
                        totalSheets = GenerateCertificatesSheet(rootFolder, templateFile);
                        reportFolder = Path.Combine(rootFolder, CEConstants.CE_CERTIFICATE_FOLDER);
                        index = reportFolder.IndexOf(CEConstants.CE_DATA_FOLDER) - 1;
                    }

                    // post-process message
                    index = index < 0 ? 0 : index;
                    string message;
                    if (action == HEADCOUNT)
                    {
                        string baseFolder = rootFolder;
                        string headCountFile = string.Format(HEADCOUNT_FILE_FORMAT, reportYear);
                        string path = Path.Combine(baseFolder, headCountFile);
                        message = string.Format("Head Count sheet for {0} competition is avaiable for reporting in file {1}", reportYear, path);
                    }
                    else if (action == TROPHY)
                    {
                        string baseFolder = rootFolder;
                        string trophyCountFile = string.Format(TROPHYCOUNT_FILE_FORMAT, reportYear);
                        string path = Path.Combine(baseFolder, trophyCountFile);
                        message = string.Format("Trophy Count sheet for {0} competition is avaiable for reporting in file {1}", reportYear, path);
                    }
                    else if (action == AWARDS)
                    {
                        if (totalSheets == 0)
                        {
                            message = string.Format("There is no competition winner recorded for {0} talent competition.", reportYear);
                        }
                        else
                        {
                            string baseFolder = rootFolder;
                            string winnerFile = string.Format(COMPETITION_AWARD_FILE_FORMAT, reportYear);
                            string path = Path.Combine(baseFolder, winnerFile);
                            message = string.Format("Competition award presentation sheet for {0} is avaiable in file {1}", reportYear, path);
                        }
                    }
                    else if (action == WINNERS)
                    {
                        if (totalSheets == 0)
                        {
                            message = string.Format("There is no competition winner recorded for {0} talent competition certification.", reportYear);
                        }
                        else
                        {
                            string baseFolder = rootFolder;
                            string winnerFile = string.Format(COMPETITION_WINNER_FILE_FORMAT, reportYear);
                            string path = Path.Combine(baseFolder, winnerFile);
                            message = string.Format("Competition winners list for {0} is avaiable in file {1}", reportYear, path);
                        }
                    }
                    else if (action == CERTIFICATE)
                    {
                        if (totalSheets == 0)
                        {
                            message = string.Format("There is no competition winner or all winner certificates have been generated for {0} talent competition certification.", reportYear);
                        }
                        else
                        {
                            message = string.Format("Total of {0} Competition winners certificates for {1} are avaiable in folder {2}", totalSheets.ToString(), reportYear, reportFolder.Substring(index));
                        }
                    }
                    else if (action == CONTESTANT)
                    {
                        message = string.Format("Total of {0} {1} sheets are generated in folder {2}.", totalSheets.ToString(), action, reportFolder.Substring(index));
                    }
                    else if (action == TROPHY)
                    {
                        message = string.Format("Total of {0} contestants with three consecutive top 3 finishes is generated in folder {1}.", totalSheets.ToString(), reportFolder.Substring(index));
                    }
                    else if (action != CONFLICT)
                    {
                        if (totalSheets == 1)
                            message = string.Format("Total of {0} {1} sheet for {2} competition is generated in folder {3}.", totalSheets.ToString(), action, group, reportFolder.Substring(index));
                        else if (totalSheets > 1)
                            message = string.Format("Total of {0} {1} sheets for {2} competition are generated in folder {3}.", totalSheets.ToString(), action, group, reportFolder.Substring(index));
                        else
                            message = string.Format("No {0} sheet for {1} competition is avaiable for reporting.", action, group);
                    }
                    else
                    {
                        string baseFolder = rootFolder;
                        string conflictFile = string.Format(CONFLICT_FILE_FORMAT, reportYear);
                        string path = Path.Combine(baseFolder, conflictFile);
                        message = string.Format("Total of {0} contestants with conflict competition event schedule is generated in file {1}.", totalSheets.ToString(), path);
                    }

                    ShowReportDialog(HttpUtility.JavaScriptStringEncode(message));
                }
                else if (!string.IsNullOrEmpty(action))
                {
                    ShowReportDialog(HttpUtility.JavaScriptStringEncode("URL query paremeters are not recognized."));
                    return;
                }
            }
        }

        private int GenerateSignInSheets(string rootFolder, FileInfo templateFile, string group)
        {
            CEEventSchedule eventSchedule = new CEEventSchedule();
            string baseFolder = Path.Combine(rootFolder, CEConstants.CE_SIGNIN_SHEET_FOLDER);
            List<string> multiEventContestants = GetContestantListWithMultiEvents();
            int totalSheets = 0;
            foreach (string category in CompetitionControlData.AllCategories.Keys)
            {
                string categoryValue = CompetitionControlData.AllCategories[category];
                if (categoryValue == string.Empty) continue;
                foreach (string division in CompetitionControlData.AllDivisions.Keys)
                {
                    string divisionValue = CompetitionControlData.AllDivisions[division];
                    if (divisionValue == string.Empty) continue;

                    foreach (string ceClass in CompetitionControlData.AllClasses.Keys)
                    {
                        string classValue = CompetitionControlData.AllClasses[ceClass];
                        string className = classValue == string.Empty ? "0" : classValue;
                        string signInFile = string.Format(REPORT_FILE_FORMAT, SIGNIN, categoryValue, divisionValue, className);

                        FileInfo reportFile = new FileInfo(Path.Combine(baseFolder, group, signInFile));
                        totalSheets += GenerateSignInFile(reportFile, templateFile, group, categoryValue, divisionValue, classValue, eventSchedule, multiEventContestants);
                    }
                }
            }

            return totalSheets;
        }

        private int GenerateScoreSheets(string rootFolder, string group)
        {
            CEEventSchedule eventSchedule = new CEEventSchedule();
            string baseFolder = Path.Combine(rootFolder, CEConstants.CE_SCORE_SHEET_FOLDER);
            List<string> multiEventContestants = GetContestantListWithMultiEvents();
            int totalSheets = 0;
            foreach (string category in CompetitionControlData.AllCategories.Keys)
            {
                string categoryValue = CompetitionControlData.AllCategories[category];
                if (categoryValue == string.Empty) continue;
                foreach (string division in CompetitionControlData.AllDivisions.Keys)
                {
                    string divisionValue = CompetitionControlData.AllDivisions[division];
                    if (divisionValue == string.Empty) continue;

                    foreach (string ceClass in CompetitionControlData.AllClasses.Keys)
                    {
                        string classValue = CompetitionControlData.AllClasses[ceClass];
                        string className = classValue == string.Empty ? "0" : classValue;
                        string scoreFile = string.Format(REPORT_FILE_FORMAT, SCORE, categoryValue, divisionValue, className);

                        int scoreSetCount = eventSchedule.GetScoreSetCount(categoryValue, divisionValue, classValue);
                        if (scoreSetCount > 0)
                        {
                            string physicalPath = Path.Combine(rootFolder, string.Format(CEConstants.CE_SCORE_TEMPLATE_BASE_XLSX, scoreSetCount.ToString()));
                            if (File.Exists(physicalPath))
                            {
                                FileInfo templateFile = new FileInfo(physicalPath);
                                FileInfo reportFile = new FileInfo(Path.Combine(baseFolder, group, scoreFile));
                                totalSheets += GenerateScoreFile(reportFile, templateFile, group, categoryValue, divisionValue, classValue, eventSchedule, multiEventContestants);
                            }
                            else
                            {
                                //ShowReportDialog("Score template '" + physicalPath + "' does not exist.");
                                return 0;
                            }
                        }
                    }
                }
            }

            return totalSheets;
        }

        private int GenerateContestantSheets(string rootFolder, FileInfo templateFile)
        {
            sendEmailFromReport = CEHelper.GetConfiguration("SendEmailFromReport", "false") == "true" ? true : false;

            CEEventSchedule eventSchedule = new CEEventSchedule();
            string baseFolder = Path.Combine(rootFolder, CEConstants.CE_CONTESTANT_SHEET_FOLDER);
            int totalSheets = 0;
            CETalentApplciants allApplicants = new CETalentApplciants();
            ContestantEvents contestantEvents = new ContestantEvents();

            // build up individual contestant list
            foreach (CECompetitionEntry applicant in allApplicants.TalentApplicants)
            {
                if (applicant.Contact.IsTeam) continue;
                EventSchedule ceEvent = eventSchedule.GetEventSchedule(applicant);
                if (ceEvent == null)
                {
                    string message = "No event is found for this applicant file: " + applicant.ContestantFile.ToString();
                    message = message + "Please fix the issue in the xml file and regenerate the reports";
                    ShowReportDialog(HttpUtility.JavaScriptStringEncode(message));
                    continue;
                }
                contestantEvents.Add(applicant.Contestants[0].ID, applicant, ceEvent, 0);
            }

            // add contestants that participate in team competition
            foreach (CECompetitionEntry applicant in allApplicants.TalentApplicants)
            {
                if (!applicant.Contact.IsTeam) continue;
                EventSchedule ceEvent = eventSchedule.GetEventSchedule(applicant);
                if (ceEvent == null)
                {
                    string message = "No event is found for this applicant file: " + applicant.ContestantFile.ToString();
                    message = message + "Please fix the issue in the xml file and regenerate the reports";
                    ShowReportDialog(HttpUtility.JavaScriptStringEncode(message));
                    continue;
                }
                for (int i = 0; i < applicant.Contestants.Count; i++)
                    contestantEvents.Add(applicant.Contestants[i].ID, applicant, ceEvent, i);
            }

            // generate contestant participating events files
            foreach (var contestantEvent in contestantEvents.ContestantList)
            {
                string contentantId = contestantEvent.Key;
                List<ContestantEvent> events = (List<ContestantEvent>)contestantEvent.Value;

                string contestantFile = string.Format(CONTESTANT_FILE_FORMAT, CONTESTANT, contentantId);
                FileInfo reportFile = new FileInfo(Path.Combine(baseFolder, contestantFile));

                totalSheets += GenerateContestantFile(reportFile, templateFile, events);
            }

            return totalSheets;
        }

        private int GenerateConflictSheet(string rootFolder, FileInfo templateFile)
        {
            CEEventSchedule eventSchedule = new CEEventSchedule();
            string baseFolder = rootFolder;
            CETalentApplciants allApplicants = new CETalentApplciants();
            ContestantEvents contestantEvents = new ContestantEvents();

            // build up individual contestant list
            foreach (CECompetitionEntry applicant in allApplicants.TalentApplicants)
            {
                if (applicant.Contact.IsTeam) continue;
                EventSchedule ceEvent = eventSchedule.GetEventSchedule(applicant);
                if (ceEvent == null)
                {
                    string message = "No event is found for this applicant file: " + applicant.ContestantFile.ToString();
                    message = message + "Please fix the issue in the xml file and regenerate the reports";
                    ShowReportDialog(HttpUtility.JavaScriptStringEncode(message));
                    continue;
                }
                contestantEvents.Add(applicant.Contestants[0].ID, applicant, ceEvent, 0);
            }

            // add contestants that participate in team competition
            foreach (CECompetitionEntry applicant in allApplicants.TalentApplicants)
            {
                if (!applicant.Contact.IsTeam) continue;
                EventSchedule ceEvent = eventSchedule.GetEventSchedule(applicant);
                if (ceEvent == null)
                {
                    string message = "No event is found for this applicant file: " + applicant.ContestantFile.ToString();
                    message = message + "Please fix the issue in the xml file and regenerate the reports";
                    ShowReportDialog(HttpUtility.JavaScriptStringEncode(message));
                    continue;
                }
                for (int i = 0; i < applicant.Contestants.Count; i++)
                    contestantEvents.Add(applicant.Contestants[i].ID, applicant, ceEvent, i);
            }

            // generate contestant event conflict file
            string conflictFile = string.Format(CONFLICT_FILE_FORMAT, CompetitionYear);
            FileInfo reportFile = new FileInfo(Path.Combine(baseFolder, conflictFile));
            int totalCount = 0;

            using (var package = new ExcelPackage(reportFile, templateFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[CONFLICT];
                if (worksheet == null) worksheet = package.Workbook.Worksheets[1];

                // replace competition year placeholder with current year
                string competitionYear = CEHelper.GetCompetitionYear();
                worksheet.Cells[1, 1].Value = worksheet.Cells[1, 1].Value.ToString().Replace("{year}", competitionYear);
                worksheet.Cells[2, 1].Value = worksheet.Cells[2, 1].Value.ToString().Replace("{year}", competitionYear);

                int row = 5; // template start row for data
                foreach (var contestantEvent in contestantEvents.ContestantList)
                {
                    List<ContestantEvent> conflictSchedule = GetConflictSchedule((List<ContestantEvent>)contestantEvent.Value);
                    if (conflictSchedule.Count > 0)
                    {
                        totalCount++;
                        foreach (ContestantEvent e in conflictSchedule)
                        {
                            worksheet.Cells[row, 1].Value = e.ContestantID;
                            worksheet.Cells[row, 2].Value = e.ContestantName;
                            worksheet.Cells[row, 3].Value = e.Category;
                            worksheet.Cells[row, 4].Value = e.Division;
                            worksheet.Cells[row, 5].Value = e.CompetitionClass;
                            worksheet.Cells[row, 6].Value = e.CompetitionTime;
                            worksheet.Cells[row, 7].Value = e.CompetitionRoom;
                            worksheet.Cells[row, 8].Value = e.TeamName;
                            row++;
                        }
                    }
                }
                package.Save();
            }

            return totalCount;
        }

        private int GenerateSignInFile(FileInfo reportFile, FileInfo templateFile, string group, string category, string division, string ceClass, CEEventSchedule eventSchedule, List<string> multiEventContestants)
        {
            using (var package = new ExcelPackage(reportFile, templateFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[SIGNIN];
                if (worksheet == null) worksheet = package.Workbook.Worksheets[1];

                // replace competition year placeholder with current year
                worksheet.Cells[1, 1].Value = worksheet.Cells[1, 1].Value.ToString().Replace("{year}", CEHelper.GetCompetitionYear());

                const int startRow = 8;
                bool foundCategory = false;
                bool foundContestant = false;
                int row = startRow;
                CETalentApplciants allApplicants = new CETalentApplciants();
                foreach (CECompetitionEntry applicant in allApplicants.TalentApplicants)
                {
                    if (applicant.Contact.Category != category || applicant.Contact.Division != division || applicant.Contact.Class != ceClass) continue;

                    bool isTeam = applicant.Contact.IsTeam == true || applicant.Contact.Category.ToLower().StartsWith(TEAM);
                    if ((group == TEAM && !isTeam) || (group == INDIVIDUAL && isTeam)) continue;

                    if (!foundCategory)
                    {
                        EventSchedule ceEvent = eventSchedule.GetEventSchedule(applicant);
                        if(ceEvent == null)
                        {
                            string message = "No event is found for this applicant file: " + applicant.ContestantFile.ToString();
                            message = message + "Please fix the issue in the xml file and regenerate the reports"; 
                            ShowReportDialog(HttpUtility.JavaScriptStringEncode(message)); 
                            continue; 
                        }
                        worksheet.Cells[3, 4].Value = CompetitionControlData.CategoryReportNames[category];
                        worksheet.Cells[4, 4].Value = CompetitionControlData.DivisionReportNames[division] + " " + CompetitionControlData.ClassReportNames[ceClass];
                        worksheet.Cells[5, 4, 5, 5].Value = ceEvent.Facilitator;
                        worksheet.Cells[5, 8].Value = ceEvent.EventRoom;
                        worksheet.Cells[5, 10].Value = ceEvent.EventTime;
                        foundCategory = true;
                    }

                    for (int i = 0; i < applicant.Contestants.Count; i++)
                    {
                        worksheet.Cells[row, 1].Value = (row - startRow + 1).ToString();
                        if (string.IsNullOrEmpty(applicant.Contestants[i].ChineseName))
                        {
                            worksheet.Cells[row, 4].Value = string.Format("{0} {1}", 
                                                                          applicant.Contestants[i].FirstName, 
                                                                          applicant.Contestants[i].LastName);
                        }
                        else 
                        {
                            worksheet.Cells[row, 4].Value = string.Format("{0} {1} ({2})", 
                                                                          applicant.Contestants[i].FirstName, 
                                                                          applicant.Contestants[i].LastName,
                                                                          applicant.Contestants[i].ChineseName);
                        }

                        worksheet.Cells[row, 5, row, 6].Value = applicant.Contestants[i].Email;
                        worksheet.Cells[row, 7, row, 9].Value = applicant.Contestants[i].School;
                        if (group == TEAM && applicant.Contact.IsTeam)
                            worksheet.Cells[row, 10].Value = applicant.Contact.TeamName;
                        else
                        {
                            bool isMultiEvents = (group == INDIVIDUAL) && IsMultiEventsContestant(multiEventContestants, applicant.Contestants[i].ID);
                            worksheet.Cells[row, 10].Value = applicant.Contestants[i].ID + (isMultiEvents ? " *" : "");
                        }

                        // append talent show subcategory to the end of team name / contestant ID
                        if (!string.IsNullOrEmpty(applicant.Contact.SubCategory))
                        {
                            worksheet.Cells[row, 10].Value = string.Format("{0} ({1})", worksheet.Cells[row, 10].Value.ToString(), applicant.Contact.SubCategory);
                        }

                        foundContestant = true;
                        row++;
                    }
                }
                
                if (foundContestant) package.Save();

                return foundContestant ? 1 : 0;
            }
        }

        private int GenerateScoreFile(FileInfo reportFile, FileInfo templateFile, string group, string category, string division, string ceClass, CEEventSchedule eventSchedule, List<string> multiEventContestants)
        {
            using (var package = new ExcelPackage(reportFile, templateFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[SCORE];
                if (worksheet == null) worksheet = package.Workbook.Worksheets[1];

                // replace competition year placeholder with current year
                worksheet.Cells[1, 1].Value = worksheet.Cells[1, 1].Value.ToString().Replace("{year}", CEHelper.GetCompetitionYear());

                const int startRow = 11;
                bool foundCategory = false;
                bool foundContestant = false;
                int row = startRow;
                List<CECompetitionEntry> teamEntires = new List<CECompetitionEntry>();
                CETalentApplciants allApplicants = new CETalentApplciants();
                foreach (CECompetitionEntry applicant in allApplicants.TalentApplicants)
                {
                    if (applicant.Contact.Category != category || applicant.Contact.Division != division || applicant.Contact.Class != ceClass) continue;

                    bool isTeam = applicant.Contact.IsTeam == true || applicant.Contact.Category.ToLower().StartsWith(TEAM);
                    if ((group == TEAM && !isTeam) || (group == INDIVIDUAL && isTeam)) continue;

                    if (!foundCategory)
                    {
                        EventSchedule ceEvent = eventSchedule.GetEventSchedule(applicant);
                        if (ceEvent == null)
                        {
                            string message = "No event is found for this applicant file: " + applicant.ContestantFile.ToString();
                            message = message + "Please fix the issue in the xml file and regenerate the reports";
                            ShowReportDialog(HttpUtility.JavaScriptStringEncode(message));
                            continue;
                        }
                        worksheet.Cells[2, 3].Value = CompetitionControlData.CategoryReportNames[category];
                        worksheet.Cells[3, 3].Value =  CompetitionControlData.DivisionReportNames[division] + " " + CompetitionControlData.ClassReportNames[ceClass];
                        worksheet.Cells[4, 3].Value = ceEvent.Facilitator;
                        worksheet.Cells[5, 3].Value = ceEvent.Referee;
                        worksheet.Cells[4, 10].Value = ceEvent.EventRoom;
                        worksheet.Cells[4, 16].Value = ceEvent.EventTime;

                        int scoreSet = 4;
                        int percentageRow = 6;
                        int englishNameRow = 7;
                        int chineseNameRow = 8;
                        int scoreRow = 10;
                        int scoreColStart = 6;
                        int scoreIndex = 0;
                        for (int col = scoreColStart; col < scoreColStart + ceEvent.EventScores.Count * scoreSet; col = col + scoreSet)
                        {
                            worksheet.Cells[percentageRow, col].Value = ceEvent.EventScores[scoreIndex].Percentage;
                            worksheet.Cells[englishNameRow, col].Value = ceEvent.EventScores[scoreIndex].EnglishName;
                            worksheet.Cells[chineseNameRow, col].Value = ceEvent.EventScores[scoreIndex].ChineseName;
                            worksheet.Cells[scoreRow, col].Value = ceEvent.EventScores[scoreIndex].Best;
                            worksheet.Cells[scoreRow, col + 1].Value = ceEvent.EventScores[scoreIndex].Better;
                            worksheet.Cells[scoreRow, col + 2].Value = ceEvent.EventScores[scoreIndex].Good;
                            worksheet.Cells[scoreRow, col + 3].Value = ceEvent.EventScores[scoreIndex].Fair;
                            scoreIndex++;
                        }
                        foundCategory = true;
                    }

                    if (group == TEAM)
                    {
                        if (applicant.Contact.IsTeam)
                        {
                            teamEntires.Add(applicant);
                            continue;
                        }
                        else
                        {
                            worksheet.Cells[row, 1].Value = (row - startRow + 1).ToString();
                            if (string.IsNullOrEmpty(applicant.Contestants[0].ChineseName))
                                worksheet.Cells[row, 2, row, 3].Value = string.Format("{0} {1} (1)", applicant.Contestants[0].FirstName, applicant.Contestants[0].LastName);
                            else
                                worksheet.Cells[row, 2, row, 3].Value = string.Format("{0} {1} ({2}) (1)", applicant.Contestants[0].FirstName, applicant.Contestants[0].LastName, applicant.Contestants[0].ChineseName);
                            foundContestant = true;
                        }
                    }
                    else
                    {
                        bool isMultiEvents = (group == INDIVIDUAL) && IsMultiEventsContestant(multiEventContestants, applicant.Contestants[0].ID);
                        worksheet.Cells[row, 1].Value = (row - startRow + 1).ToString();
                        if (string.IsNullOrEmpty(applicant.Contestants[0].ChineseName))
                            worksheet.Cells[row, 2, row, 3].Value = string.Format("{0} {1} {2}", applicant.Contestants[0].FirstName, applicant.Contestants[0].LastName, (isMultiEvents ? " *" : ""));
                        else
                            worksheet.Cells[row, 2, row, 3].Value = string.Format("{0} {1} ({2}) {3}", applicant.Contestants[0].FirstName, applicant.Contestants[0].LastName, applicant.Contestants[0].ChineseName, (isMultiEvents ? " *" : ""));
                        foundContestant = true;
                    }
                    row++;
                }


                foreach (CECompetitionEntry e in teamEntires)
                {
                    worksheet.Cells[row, 1].Value = (row - startRow + 1).ToString();
                    worksheet.Cells[row, 2, row, 3].Value = e.Contact.TeamName + " (" + e.Contestants.Count.ToString() + ")";
                    row++;
                    foundContestant = true;
                }

                if (foundContestant) package.Save();

                return foundContestant ? 1 : 0;
            }
        }

        private void GenerateTrophySheet(string rootFolder, FileInfo templateFile)
        {
            string baseFolder = rootFolder;
            string trophyCountFile = string.Format(TROPHYCOUNT_FILE_FORMAT, CEHelper.GetCompetitionYear());
            FileInfo reportFile = new FileInfo(Path.Combine(baseFolder, trophyCountFile));

            // dictionary for competition category, row #
            int individualIndex = 3, teamIndex = 48;
            Dictionary<string, int> competitionMap = new Dictionary<string, int>()
            {
                { CompetitionControlData.DRAWING + CompetitionControlData.LOWER_ELEMENTARY, individualIndex },
                { CompetitionControlData.DRAWING + CompetitionControlData.UPPER_ELEMENTARY, individualIndex+1 },
                { CompetitionControlData.DRAWING + CompetitionControlData.MIDDLE_SCHOOL, individualIndex+2 },
                { CompetitionControlData.DRAWING + CompetitionControlData.HIGH_SCHOOL, individualIndex+3 },

                { CompetitionControlData.SPEECH +  CompetitionControlData.MIDDLE_SCHOOL + CompetitionControlData.CLASS_A, individualIndex+5 },
                { CompetitionControlData.SPEECH +  CompetitionControlData.HIGH_SCHOOL + CompetitionControlData.CLASS_A, individualIndex+6 },
                { CompetitionControlData.SPEECH +  CompetitionControlData.MIDDLE_SCHOOL + CompetitionControlData.CLASS_B, individualIndex+7 },
                { CompetitionControlData.SPEECH +  CompetitionControlData.HIGH_SCHOOL + CompetitionControlData.CLASS_B, individualIndex+8 },
                { CompetitionControlData.SPEECH +  CompetitionControlData.MIDDLE_SCHOOL + CompetitionControlData.CLASS_C, individualIndex+9 },
                { CompetitionControlData.SPEECH +  CompetitionControlData.HIGH_SCHOOL + CompetitionControlData.CLASS_C, individualIndex+10 },

                { CompetitionControlData.STORY + CompetitionControlData.UPPER_ELEMENTARY + CompetitionControlData.CLASS_A, individualIndex+12 },
                { CompetitionControlData.STORY + CompetitionControlData.UPPER_ELEMENTARY + CompetitionControlData.CLASS_B, individualIndex+13 },
                { CompetitionControlData.STORY + CompetitionControlData.UPPER_ELEMENTARY + CompetitionControlData.CLASS_C, individualIndex+14 },

                { CompetitionControlData.POETRY + CompetitionControlData.LOWER_ELEMENTARY + CompetitionControlData.CLASS_A, individualIndex+16 },
                { CompetitionControlData.POETRY + CompetitionControlData.LOWER_ELEMENTARY + CompetitionControlData.CLASS_B, individualIndex+17 },
                { CompetitionControlData.POETRY + CompetitionControlData.LOWER_ELEMENTARY + CompetitionControlData.CLASS_C, individualIndex+18 },

                //there's no Chinese chess for k-2 and 3-5, We start from grade by Emma
                //{ CompetitionControlData.CHESS + CompetitionControlData.LOWER_ELEMENTARY, individualIndex+20 },
                //{ CompetitionControlData.CHESS + CompetitionControlData.UPPER_ELEMENTARY, individualIndex+21 },
                { CompetitionControlData.CHESS + CompetitionControlData.MIDDLE_SCHOOL, individualIndex+22 },
                { CompetitionControlData.CHESS + CompetitionControlData.HIGH_SCHOOL, individualIndex+23 },

                // New competition category for high school students only from 2019
                { CompetitionControlData.BRIDGE + CompetitionControlData.HIGH_SCHOOL, individualIndex + 20 }, 
                
                { CompetitionControlData.TEAM_POETRY + CompetitionControlData.LOWER_ELEMENTARY, teamIndex },
                { CompetitionControlData.TEAM_POETRY + CompetitionControlData.UPPER_ELEMENTARY, teamIndex+1 },
                { CompetitionControlData.TEAM_POETRY + CompetitionControlData.MIDDLE_SCHOOL, teamIndex+2 },
                { CompetitionControlData.TEAM_POETRY + CompetitionControlData.HIGH_SCHOOL, teamIndex+3 },
                
                { CompetitionControlData.TEAM_TALENT + CompetitionControlData.LOWER_ELEMENTARY, teamIndex+5 },
                { CompetitionControlData.TEAM_TALENT + CompetitionControlData.UPPER_ELEMENTARY, teamIndex+6 },
                { CompetitionControlData.TEAM_TALENT + CompetitionControlData.MIDDLE_SCHOOL, teamIndex+7 },
                { CompetitionControlData.TEAM_TALENT + CompetitionControlData.HIGH_SCHOOL, teamIndex+8 },
                
                { CompetitionControlData.TEAM_SINGING + CompetitionControlData.LOWER_ELEMENTARY, teamIndex+10 },
                { CompetitionControlData.TEAM_SINGING + CompetitionControlData.UPPER_ELEMENTARY, teamIndex+11 },
                { CompetitionControlData.TEAM_SINGING + CompetitionControlData.MIDDLE_SCHOOL, teamIndex+12 },
                { CompetitionControlData.TEAM_SINGING + CompetitionControlData.HIGH_SCHOOL, teamIndex+13 },
                
                { CompetitionControlData.TEAM_LANGUAGE + CompetitionControlData.LOWER_ELEMENTARY, teamIndex+15 },
                { CompetitionControlData.TEAM_LANGUAGE + CompetitionControlData.UPPER_ELEMENTARY, teamIndex+16 },
                { CompetitionControlData.TEAM_LANGUAGE + CompetitionControlData.MIDDLE_SCHOOL, teamIndex+17 },
                { CompetitionControlData.TEAM_LANGUAGE + CompetitionControlData.HIGH_SCHOOL, teamIndex+18 },
                
                { CompetitionControlData.TEAM_KNOWLEGE_BOWL + CompetitionControlData.MIDDLE_SCHOOL, teamIndex+20 },
                { CompetitionControlData.TEAM_KNOWLEGE_BOWL + CompetitionControlData.HIGH_SCHOOL, teamIndex+21 },
            };

            int headCountCol = 3;
            int trophyCol = 5;
            int firstPlaceCol = 6;
            int secondPlaceCol = 7;
            int thirdPlaceCol = 8;
            int individualTeamOffset = 25;

            int totalIndividualTrophies = 0;
            int totalIndividualHeadCount = 0;
            int totalIndividualFirst = 0;
            int totalIndividualSecond = 0;
            int totalIndividualThird = 0;
            int totalTeamTrophies = 0;
            int totalTeamHeadCount = 0;
            int totalTeamFirst = 0;
            int totalTeamSecond = 0;
            int totalTeamThird = 0;

            using (var package = new ExcelPackage(reportFile, templateFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[TROPHY];
                if (worksheet == null) worksheet = package.Workbook.Worksheets[1];

                CETalentApplciants allApplicants = new CETalentApplciants();
                foreach (string category in CompetitionControlData.AllCategories.Keys)
                {
                    string categoryValue = CompetitionControlData.AllCategories[category];
                    if (categoryValue == string.Empty) continue;
                    foreach (string division in CompetitionControlData.AllDivisions.Keys)
                    {
                        string divisionValue = CompetitionControlData.AllDivisions[division];
                        if (divisionValue == string.Empty) continue;

                        int count = 0;
                        int row = -1;

                        if (categoryValue == CompetitionControlData.POETRY ||
                            categoryValue == CompetitionControlData.SPEECH ||
                            categoryValue == CompetitionControlData.STORY)
                        {
                            foreach (string ceClass in CompetitionControlData.AllClasses.Keys)
                            {
                                string classValue = CompetitionControlData.AllClasses[ceClass];
                                if (classValue == string.Empty) continue;
                                row = GetSaveMapValue(competitionMap, categoryValue + divisionValue + classValue);
                                if (row > 0)
                                {
                                    count = GetHeadCount(allApplicants, categoryValue, divisionValue, classValue, INDIVIDUAL);
                                    worksheet.Cells[row, headCountCol].Value = count;
                                    worksheet.Cells[row, trophyCol].Value = count > 2 ? "3" : count.ToString();
                                    worksheet.Cells[row, firstPlaceCol].Value = count > 0 ? "1" : "0";
                                    worksheet.Cells[row, secondPlaceCol].Value = count > 1 ? "1" : "0";
                                    worksheet.Cells[row, thirdPlaceCol].Value = count > 2 ? "1" : "0";

                                    if (categoryValue.ToLower().StartsWith(TEAM))
                                    {
                                        totalTeamHeadCount += Convert.ToInt32(worksheet.Cells[row, headCountCol].Value);
                                        totalTeamTrophies += Convert.ToInt32(worksheet.Cells[row, trophyCol].Value);
                                        totalTeamFirst += Convert.ToInt32(worksheet.Cells[row, firstPlaceCol].Value);
                                        totalTeamSecond += Convert.ToInt32(worksheet.Cells[row, secondPlaceCol].Value);
                                        totalTeamThird += Convert.ToInt32(worksheet.Cells[row, thirdPlaceCol].Value);
                                    }
                                    else
                                    {
                                        totalIndividualHeadCount += Convert.ToInt32(worksheet.Cells[row, headCountCol].Value);
                                        totalIndividualTrophies += Convert.ToInt32(worksheet.Cells[row, trophyCol].Value);
                                        totalIndividualFirst += Convert.ToInt32(worksheet.Cells[row, firstPlaceCol].Value);
                                        totalIndividualSecond += Convert.ToInt32(worksheet.Cells[row, secondPlaceCol].Value);
                                        totalIndividualThird += Convert.ToInt32(worksheet.Cells[row, thirdPlaceCol].Value);
                                    }
                                }
                            }
                        }
                        else
                        {
                            row = GetSaveMapValue(competitionMap, categoryValue + divisionValue);
                            if (row > 0)
                            {
                                if (categoryValue.ToLower().StartsWith(TEAM))
                                {
                                    if (categoryValue == CompetitionControlData.TEAM_POETRY || categoryValue == CompetitionControlData.TEAM_KNOWLEGE_BOWL)
                                    {
                                        count = GetHeadCount(allApplicants, categoryValue, divisionValue, string.Empty, true);
                                        worksheet.Cells[row, headCountCol].Value = count;
                                    }
                                    else // render both team and individual category that are competed together
                                    {
                                        // the map only pull team competition category based on team's index. So we render
                                        // rows of individual competition overlapped with team's by the offset from team's
                                        if (categoryValue == CompetitionControlData.TEAM_TALENT ||
                                            categoryValue == CompetitionControlData.TEAM_SINGING ||
                                            categoryValue == CompetitionControlData.TEAM_LANGUAGE)
                                        {
                                            count = GetHeadCount(allApplicants, categoryValue, divisionValue, string.Empty, false);
                                            int individualRow = row - individualTeamOffset;
                                            worksheet.Cells[individualRow, headCountCol].Value = count;
                                            worksheet.Cells[individualRow, trophyCol].Value = count > 2 ? "3" : count.ToString();
                                            worksheet.Cells[individualRow, firstPlaceCol].Value = count > 0 ? "1" : "0";
                                            worksheet.Cells[individualRow, secondPlaceCol].Value = count > 1 ? "1" : "0";
                                            worksheet.Cells[individualRow, thirdPlaceCol].Value = count > 2 ? "1" : "0";
                                            totalIndividualHeadCount += Convert.ToInt32(worksheet.Cells[individualRow, headCountCol].Value);
                                            totalIndividualTrophies += Convert.ToInt32(worksheet.Cells[individualRow, trophyCol].Value);
                                            totalIndividualFirst += Convert.ToInt32(worksheet.Cells[individualRow, firstPlaceCol].Value);
                                            totalIndividualSecond += Convert.ToInt32(worksheet.Cells[individualRow, secondPlaceCol].Value);
                                            totalIndividualThird += Convert.ToInt32(worksheet.Cells[individualRow, thirdPlaceCol].Value);
                                        }

                                        // team
                                        count = GetHeadCount(allApplicants, categoryValue, divisionValue, string.Empty, true);
                                        worksheet.Cells[row, headCountCol].Value = count;
                                    }
                                }
                                else
                                {
                                    count = GetHeadCount(allApplicants, categoryValue, divisionValue, string.Empty, INDIVIDUAL);
                                    worksheet.Cells[row, headCountCol].Value = count;
                                }

                                worksheet.Cells[row, trophyCol].Value = count > 2 ? "3" : count.ToString();
                                worksheet.Cells[row, firstPlaceCol].Value = count > 0 ? "1" : "0";
                                worksheet.Cells[row, secondPlaceCol].Value = count > 1 ? "1" : "0";
                                worksheet.Cells[row, thirdPlaceCol].Value = count > 2 ? "1" : "0";

                                if (categoryValue.ToLower().StartsWith(TEAM))
                                {
                                    totalTeamHeadCount += Convert.ToInt32(worksheet.Cells[row, headCountCol].Value);
                                    totalTeamTrophies += Convert.ToInt32(worksheet.Cells[row, trophyCol].Value);
                                    totalTeamFirst += Convert.ToInt32(worksheet.Cells[row, firstPlaceCol].Value);
                                    totalTeamSecond += Convert.ToInt32(worksheet.Cells[row, secondPlaceCol].Value);
                                    totalTeamThird += Convert.ToInt32(worksheet.Cells[row, thirdPlaceCol].Value);
                                }
                                else
                                {
                                    totalIndividualHeadCount += Convert.ToInt32(worksheet.Cells[row, headCountCol].Value);
                                    totalIndividualTrophies += Convert.ToInt32(worksheet.Cells[row, trophyCol].Value);
                                    totalIndividualFirst += Convert.ToInt32(worksheet.Cells[row, firstPlaceCol].Value);
                                    totalIndividualSecond += Convert.ToInt32(worksheet.Cells[row, secondPlaceCol].Value);
                                    totalIndividualThird += Convert.ToInt32(worksheet.Cells[row, thirdPlaceCol].Value);
                                }
                            }
                        }
                    }
                }

                int individualTotalRow = 43;
                int teamTotalRow = 71;

                worksheet.Cells[individualTotalRow, headCountCol].Value = totalIndividualHeadCount;
                worksheet.Cells[individualTotalRow, trophyCol].Value = totalIndividualTrophies;
                worksheet.Cells[individualTotalRow, firstPlaceCol].Value = totalIndividualFirst;
                worksheet.Cells[individualTotalRow, secondPlaceCol].Value = totalIndividualSecond;
                worksheet.Cells[individualTotalRow, thirdPlaceCol].Value = totalIndividualThird;

                worksheet.Cells[teamTotalRow, headCountCol].Value = totalTeamHeadCount;
                worksheet.Cells[teamTotalRow, trophyCol].Value = totalTeamTrophies;
                worksheet.Cells[teamTotalRow, firstPlaceCol].Value = totalTeamFirst;
                worksheet.Cells[teamTotalRow, secondPlaceCol].Value = totalTeamSecond;
                worksheet.Cells[teamTotalRow, thirdPlaceCol].Value = totalTeamThird;

                package.Save();
            }
        }

        private void GenerateHeadCountSheet(string rootFolder, FileInfo templateFile)
        {
            string baseFolder = rootFolder;
            string headCountFile = string.Format(HEADCOUNT_FILE_FORMAT, CEHelper.GetCompetitionYear());
            FileInfo reportFile = new FileInfo(Path.Combine(baseFolder, headCountFile));

            // dictionary for competition category, row #
            Dictionary<string, int> categoryMap = new Dictionary<string, int>()
            {
                { CompetitionControlData.CHESS, 2 },
                { CompetitionControlData.DRAWING, 3 },
                { CompetitionControlData.POETRY + CompetitionControlData.CLASS_A, 4 },
                { CompetitionControlData.POETRY + CompetitionControlData.CLASS_B, 5 },
                { CompetitionControlData.POETRY + CompetitionControlData.CLASS_C, 6 },
                { CompetitionControlData.STORY + CompetitionControlData.CLASS_A, 7 },
                { CompetitionControlData.STORY + CompetitionControlData.CLASS_B, 8 },
                { CompetitionControlData.STORY + CompetitionControlData.CLASS_C, 9 },
                { CompetitionControlData.SPEECH + CompetitionControlData.CLASS_A, 10 },
                { CompetitionControlData.SPEECH + CompetitionControlData.CLASS_B, 11 },
                { CompetitionControlData.SPEECH + CompetitionControlData.CLASS_C, 12 },
                { CompetitionControlData.TEAM_TALENT + INDIVIDUAL, 17 },
                { CompetitionControlData.TEAM_TALENT, 18 },
                { CompetitionControlData.TEAM_LANGUAGE + INDIVIDUAL, 19 },
                { CompetitionControlData.TEAM_LANGUAGE, 20 },
                { CompetitionControlData.TEAM_SINGING + INDIVIDUAL, 21 },
                { CompetitionControlData.TEAM_SINGING, 22 },
                { CompetitionControlData.TEAM_POETRY, 23 },
                { CompetitionControlData.TEAM_KNOWLEGE_BOWL, 24 },
                { CompetitionControlData.BRIDGE, 25 }
            };

            // dictionary for competition division, column #
            Dictionary<string, int> divisionMap = new Dictionary<string, int>()
            {
                { CompetitionControlData.LOWER_ELEMENTARY, 2 },
                { CompetitionControlData.UPPER_ELEMENTARY, 3 },
                { CompetitionControlData.MIDDLE_SCHOOL, 4 },
                { CompetitionControlData.HIGH_SCHOOL, 5 }
            };

            using (var package = new ExcelPackage(reportFile, templateFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[HEADCOUNT];
                if (worksheet == null) worksheet = package.Workbook.Worksheets[1];

                CETalentApplciants allApplicants = new CETalentApplciants();
                foreach (string category in CompetitionControlData.AllCategories.Keys)
                {
                    string categoryValue = CompetitionControlData.AllCategories[category];
                    if (categoryValue == string.Empty) continue;
                    foreach (string division in CompetitionControlData.AllDivisions.Keys)
                    {
                        string divisionValue = CompetitionControlData.AllDivisions[division];
                        if (divisionValue == string.Empty) continue;

                        if (categoryValue == CompetitionControlData.POETRY ||
                            categoryValue == CompetitionControlData.SPEECH ||
                            categoryValue == CompetitionControlData.STORY)
                        {
                            foreach (string ceClass in CompetitionControlData.AllClasses.Keys)
                            {
                                string classValue = CompetitionControlData.AllClasses[ceClass];
                                if (classValue == string.Empty) continue;
                                string categoryKey = categoryValue + classValue;

                                int row = categoryMap[categoryKey];
                                int col = divisionMap[divisionValue];
                                int count = GetHeadCount(allApplicants, categoryValue, divisionValue, classValue, INDIVIDUAL);
                                worksheet.Cells[row, col].Value = count;
                            }
                        }
                        else
                        {
                            int row = categoryMap[categoryValue];
                            int col = divisionMap[divisionValue];
                            int count = 0;
                            if (categoryValue.ToLower().StartsWith(TEAM))
                            {
                                count = GetHeadCount(allApplicants, categoryValue, divisionValue, string.Empty, true);
                                worksheet.Cells[row, col].Value = count;
                                if (categoryValue != CompetitionControlData.TEAM_POETRY && categoryValue != CompetitionControlData.TEAM_KNOWLEGE_BOWL)
                                {
                                    count = GetHeadCount(allApplicants, categoryValue, divisionValue, string.Empty, false);
                                    row = categoryMap[categoryValue + INDIVIDUAL];
                                    worksheet.Cells[row, col].Value = count;
                                }
                            }
                            else
                            {
                                count = GetHeadCount(allApplicants, categoryValue, divisionValue, string.Empty, INDIVIDUAL);
                                worksheet.Cells[row, col].Value = count;
                            }
                        }
                    }
                }

                int sumCol = 6;
                int headCountStartRow = 2;
                int headCountEndRow = 12;
                int headCountStartCol = 2;
                int headCountEndCol = 5;
                int headCountRow = 13;
                int teamCountStartRow = 17;
                int teanCountEndRow = 24;
                int teamCountRow = 25;

                for (int row = headCountStartRow; row <= headCountEndRow; row++)
                {
                    int sum = 0;
                    for (int col = headCountStartCol; col <= headCountEndCol; col++) sum += (int)worksheet.Cells[row, col].Value;
                    worksheet.Cells[row, sumCol].Value = sum;
                }
                for (int row = teamCountStartRow; row <= teanCountEndRow; row++)
                {
                    int sum = 0;
                    for (int col = headCountStartCol; col <= headCountEndCol; col++) sum += (int)worksheet.Cells[row, col].Value;
                    worksheet.Cells[row, sumCol].Value = sum;
                }

                for (int col = headCountStartCol; col <= sumCol; col++)
                {
                    int sum = 0;
                    for (int row = headCountStartRow; row <= headCountEndRow; row++) sum += (int)worksheet.Cells[row, col].Value;
                    worksheet.Cells[headCountRow, col].Value = sum;
                }
                for (int col = headCountStartCol; col <= sumCol; col++)
                {
                    int sum = 0;
                    for (int row = teamCountStartRow; row <= teanCountEndRow; row++) sum += (int)worksheet.Cells[row, col].Value;
                    worksheet.Cells[teamCountRow, col].Value = sum;
                }

                package.Save();
            }
        }

        private int GenerateAwardPresentationSheet(string rootFolder, FileInfo templateFile)
        {
            string baseFolder = rootFolder;
            string winnerFile = string.Format(COMPETITION_AWARD_FILE_FORMAT, CEHelper.GetCompetitionYear());
            FileInfo reportFile = new FileInfo(Path.Combine(baseFolder, winnerFile));

            CETalentApplciants allApplicants = new CETalentApplciants(); // get all contestants from xml files in registration folder
            List<CECompetitionEntry> awardedApplicants = GetAwardedApplicants(allApplicants);

            if (awardedApplicants != null && awardedApplicants.Count <= 0) return 0;

            using (var package = new ExcelPackage(reportFile, templateFile))
            {
                int firstDivisionRow = 7;
                int nextDivisionRowIncrement = 4;
                int row = firstDivisionRow;
                int placeCol = 1, firstNameCol = 2, lastNameCol = 3, schoolCol = 4;
                string lastCategory = string.Empty;
                string lastDivision = string.Empty;
                string lastClass = string.Empty;
                int lastCount = 0;

                ExcelWorksheet worksheet = null;
                foreach (CECompetitionEntry entry in awardedApplicants)
                {
                    string category = entry.Contact.Category;
                    string division = entry.Contact.Division;
                    string ceClass = entry.Contact.Class;
                    int groupCount = GroupCount(entry.Contestants.Count);
                    if (lastCategory != category)
                    {
                        lastCategory = category;
                        lastDivision = division;
                        lastClass = ceClass;
                        lastCount = groupCount;
                        worksheet = package.Workbook.Worksheets[category];
                        if (worksheet == null) continue;
                        row = firstDivisionRow;
                    }
                    else if (lastDivision != division)
                    {
                        row += nextDivisionRowIncrement;
                        lastDivision = division;
                        lastClass = ceClass;
                        lastCount = groupCount;
                    }
                    else if (lastClass != ceClass)
                    {
                        row += nextDivisionRowIncrement;
                        lastClass = ceClass;
                        lastCount = groupCount;
                    }
                    else if ((category == "TeamSing" || category == "TeamTalent" || category == "TeamLanguage") &&
                             lastCount != groupCount)
                    {
                        row += nextDivisionRowIncrement;
                        lastCount = groupCount;
                    }
                    else
                    {
                        worksheet.InsertRow(row+1, 1, firstDivisionRow);
                        row++;
                    }

                    if (worksheet != null)
                    {
                        worksheet.Cells[row, placeCol].Value = Wordify(entry.Award.ToString());
                        if (category.ToLower().StartsWith("team") && groupCount != 1)
                        {
                            if (worksheet.Cells[row, firstNameCol, row, lastNameCol].Merge != true)
                                worksheet.Cells[row, firstNameCol, row, lastNameCol].Merge = true;
                            worksheet.Cells[row, firstNameCol].Value = entry.Contact.TeamName;
                        }
                        else
                        {
                            worksheet.Cells[row, firstNameCol].Value = entry.Contestants[0].FirstName;
                            worksheet.Cells[row, lastNameCol].Value = entry.Contestants[0].LastName;
                        }
                        worksheet.Cells[row, schoolCol].Value = entry.Contestants[0].School;
                    }
                }

                package.Save();
            }
            return 1;
        }

        private int GenerateWinnersSheet(string rootFolder, FileInfo templateFile)
        {
            string baseFolder = rootFolder;
            string winnerFile = string.Format(COMPETITION_WINNER_FILE_FORMAT, CEHelper.GetCompetitionYear());
            FileInfo reportFile = new FileInfo(Path.Combine(baseFolder, winnerFile));

            CETalentApplciants allApplicants = new CETalentApplciants(); // get all contestants from xml files in registration folder
            List<CECompetitionEntry> awardedApplicants = GetAwardedApplicants(allApplicants, true);

            if (awardedApplicants != null && awardedApplicants.Count <= 0) return 0;

            using (var package = new ExcelPackage(reportFile, templateFile))
            {
                string lastCategory = string.Empty;
                string lastDivision = string.Empty;

                // awarded applicants are sorted by cometition type, category, division, class, then award place
                const int categoryCol = 1, divisionCol = 2, classCol = 3;
                const int placeCol = 3, lastNameCol = 4, firstNameCol = 5, schoolCol = 6, teamCol = 7;
                int colOffset = 0;
                int row = 1;

                ExcelWorksheet worksheet = null;

                worksheet = package.Workbook.Worksheets[INDIVIDUAL];
                if (worksheet == null) worksheet = package.Workbook.Worksheets[1];
                if (worksheet == null) return 0;

                row = 1;
                colOffset = 1;
                foreach (CECompetitionEntry entry in awardedApplicants)
                {
                    string category = entry.Contact.Category;
                    string division = entry.Contact.Division;
                    string ceClass = entry.Contact.Class;

                    if ((RequireClass(category) && string.IsNullOrEmpty(ceClass)) || 
                        (!RequireClass(category) && !string.IsNullOrEmpty(ceClass)) ||
                        NoCompetitionEvent(category, division) || IsTeam(entry))
                        continue;

                    if (lastCategory != category)
                    {
                        lastCategory = category;
                        lastDivision = division;
                    }
                    else if (lastDivision != division)
                    {
                        lastDivision = division;
                    }
                    else
                    {
                        worksheet.InsertRow(row + 1, 1, row);
                        worksheet.Cells[row + 1, categoryCol].Value = worksheet.Cells[row, categoryCol].Value;
                        worksheet.Cells[row + 1, divisionCol].Value = worksheet.Cells[row, divisionCol].Value;
                    }

                    row++;
                    worksheet.Cells[row, classCol].Value = Wordify(ceClass);
                    worksheet.Cells[row, placeCol + colOffset].Value = Wordify(entry.Award.ToString());
                    worksheet.Cells[row, lastNameCol + colOffset].Value = entry.Contestants[0].LastName;
                    worksheet.Cells[row, firstNameCol + colOffset].Value = entry.Contestants[0].FirstName;
                    worksheet.Cells[row, schoolCol + colOffset].Value = entry.Contestants[0].School;
                }

                worksheet = package.Workbook.Worksheets[TEAM];
                if (worksheet == null) worksheet = package.Workbook.Worksheets[2];
                if (worksheet == null) return 0;

                row = 1;
                foreach (CECompetitionEntry entry in awardedApplicants)
                {
                    string category = entry.Contact.Category;
                    string division = entry.Contact.Division;
                    string ceClass = entry.Contact.Class;
                    bool newDivision = true;

                    if ((RequireClass(category) && string.IsNullOrEmpty(ceClass)) ||
                        (!RequireClass(category) && !string.IsNullOrEmpty(ceClass)) ||
                        NoCompetitionEvent(category, division) || !IsTeam(entry))
                        continue;

                    if (lastCategory != category)
                    {
                        lastCategory = category;
                        lastDivision = division;
                        newDivision = true;
                    }
                    else if (lastDivision != division)
                    {
                        lastDivision = division;
                        newDivision = true;
                    }
                    else
                    {
                        newDivision = false;
                    }

                    if (newDivision) row++;

                    string place = Wordify(entry.Award.ToString());
                    string teamName = entry.Contact.TeamName;
                    foreach (CEContestant contentant in entry.Contestants)
                    {
                        if (!newDivision)
                        {
                            worksheet.InsertRow(row + 1, 1, row);
                            worksheet.Cells[row + 1, categoryCol].Value = worksheet.Cells[row, categoryCol].Value;
                            worksheet.Cells[row + 1, divisionCol].Value = worksheet.Cells[row, divisionCol].Value;
                            row++;
                        }

                        worksheet.Cells[row, placeCol].Value = place;
                        worksheet.Cells[row, lastNameCol].Value = contentant.LastName;
                        worksheet.Cells[row, firstNameCol].Value = contentant.FirstName;
                        worksheet.Cells[row, schoolCol].Value = contentant.School;
                        worksheet.Cells[row, teamCol].Value = teamName;
                        newDivision = false;
                    }
                }

                package.Save();
            }
            return 1;
        }

        private int GenerateCertificatesSheet(string rootFolder, FileInfo templateFile)
        {
            string baseFolder = Path.Combine(rootFolder, CEConstants.CE_CERTIFICATE_FOLDER);
            int totalCertificates = 0;

            CETalentApplciants allApplicants = new CETalentApplciants(); // get all contestants from xml files in registration folder
            List<CECompetitionEntry> awardedApplicants = GetAwardedApplicants(allApplicants, true);

            if (awardedApplicants != null && awardedApplicants.Count <= 0) return 0;

            //Word.Application application = null;
            try
            {
                // generate contestant participating events files
                //application = new Word.Application { Visible = false };
                foreach (CECompetitionEntry winner in awardedApplicants)
                {
                    //totalCertificates += GenerateCertificateFile(application, baseFolder, templateFile.FullName, winner);
                    totalCertificates += GenerateCertificateFileWithOpenXml(baseFolder, templateFile.FullName, winner);
                    //if (totalCertificates > 2) break;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                //if (Application != null) ((Word._Application)application).Quit();
            }

            return totalCertificates;
        }

        private int GenerateCertificateFile(Word.Application application, string reportFolder, string templateFile, CECompetitionEntry winner)
        {
            int certificateCount = 0;
            Word.Document certificateDoc = null;
            object replaceAll = Word.WdReplace.wdReplaceAll;
            object fileFormat = Word.WdSaveFormat.wdFormatPDF;

            foreach (CEContestant contestant in winner.Contestants)
            {
                try
                {
                    string certificateFile = Path.Combine(reportFolder, MakeCertificateFileName(winner, contestant, "pdf"));
                    if (File.Exists(certificateFile)) continue;

                    string tempFile = Path.ChangeExtension(certificateFile, "docx");
                    if (File.Exists(tempFile)) File.Delete(tempFile);
                    File.Copy(templateFile, tempFile);
                    certificateDoc = application.Documents.Open(tempFile, ReadOnly: false, Visible: false);
                    if (certificateDoc == null) return certificateCount;

                    certificateDoc.Activate();

                    Word.Find findObject = application.ActiveWindow.Selection.Find;
                    //findObject.Forward = true;
                    //findObject.Wrap = Word.WdFindWrap.wdFindContinue;

                    findObject.ClearFormatting();
                    findObject.Replacement.ClearFormatting();

                    string place = PlaceText(winner.Award, false);
                    string school = contestant.School;
                    string placeChinese = PlaceText(winner.Award, true);
                    string category = CategoryText(winner.Contact);
                    string categoryChinese = CompetitionControlData.CategoryChineseNames[winner.Contact.Category];

                    findObject.Text = "{{Name}}";
                    findObject.Replacement.Text = string.Format("{0} {1}", contestant.FirstName, contestant.LastName);
                    findObject.Execute(Replace: replaceAll);
                    findObject.Text = "{{School}}";
                    findObject.Replacement.Text = school;
                    findObject.Execute(Replace: replaceAll);
                    findObject.Text = "{{Place}}";
                    findObject.Replacement.Text = place + " " + placeChinese;
                    findObject.Execute(Replace: replaceAll);
                    //findObject.Text = "{{Place-Chinese}}";
                    //findObject.Replacement.Text = placeChinese;
                    //findObject.Execute(Replace: replaceAll);
                    findObject.Text = "{{Category}}";
                    findObject.Replacement.Text = category + " " + categoryChinese;
                    findObject.Execute(Replace: replaceAll);
                    //findObject.Text = "{{Category-Chinese}}";
                    //findObject.Replacement.Text = categoryChinese;
                    //findObject.Execute(Replace: replaceAll);

                    certificateDoc.SaveAs2(FileName: certificateFile, FileFormat: fileFormat, ReadOnlyRecommended: false);
                    ((Word._Document)certificateDoc).Close();
                    File.Delete(tempFile);
                    certificateDoc = null;

                    certificateCount++;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (certificateDoc != null) ((Word._Document)certificateDoc).Close();
                }
            }

            return certificateCount;
        }

        private int GenerateCertificateFileWithOpenXml(string reportFolder, string templateFile, CECompetitionEntry winner)
        {
            int certificateCount = 0;

            foreach (CEContestant contestant in winner.Contestants)
            {
                try
                {
                    string certificateFile = Path.Combine(reportFolder, MakeCertificateFileName(winner, contestant, "docx"));
                    if (File.Exists(certificateFile)) continue;


                    string name = string.Format("{0} {1}", contestant.FirstName, contestant.LastName);
                    string school = contestant.School;
                    string place = PlaceText(winner.Award, false);
                    string placeChinese = PlaceText(winner.Award, true);
                    string category = CategoryText(winner.Contact);
                    string categoryChinese = CompetitionControlData.CategoryChineseNames[winner.Contact.Category];

                    File.Copy(templateFile, certificateFile);
                    using (WordprocessingDocument certificateDoc = WordprocessingDocument.Open(certificateFile, true))
                    {
                        string docText = null;
                        using (StreamReader sr = new StreamReader(certificateDoc.MainDocumentPart.GetStream()))
                        {
                            docText = sr.ReadToEnd();
                        }

                        Regex regexText = new Regex("{{Name}}");
                        docText = regexText.Replace(docText, name);
                        regexText = new Regex("{{School}}");
                        docText = regexText.Replace(docText, school);
                        regexText = new Regex("{{Place}}");
                        if (place.Contains("Honorable"))
                            docText = regexText.Replace(docText, place);
                        else
                            docText = regexText.Replace(docText, place + " " + placeChinese);

                        //regexText = new Regex("{{Place-Chinese}}");
                        //docText = regexText.Replace(docText, placeChinese);
                        regexText = new Regex("{{Category}}");
                        docText = regexText.Replace(docText, category + " " + categoryChinese);
                        //regexText = new Regex("{{Category-Chinese}}");
                        //docText = regexText.Replace(docText, categoryChinese);

                        using (StreamWriter sw = new StreamWriter(certificateDoc.MainDocumentPart.GetStream(FileMode.Create)))
                        {
                            sw.Write(docText);
                        } 
                    }

                    certificateCount++;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally 
                {
                }
            }

            return certificateCount;
        }

        private int GenerateContestantFile(FileInfo reportFile, FileInfo templateFile, List<ContestantEvent> events)
        {
            using (var package = new ExcelPackage(reportFile, templateFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[CONTESTANT];
                if (worksheet == null) worksheet = package.Workbook.Worksheets[1];

                // replace competition year placeholder with current year
                string competitionYear = CEHelper.GetCompetitionYear();
                worksheet.Cells[1, 1].Value = worksheet.Cells[1, 1].Value.ToString().Replace("{year}", competitionYear);
                worksheet.Cells[2, 1].Value = worksheet.Cells[2, 1].Value.ToString().Replace("{year}", competitionYear);

                worksheet.Cells[3, 2].Value = events[0].ContestantID;
                worksheet.Cells[4, 2].Value = events[0].ContestantName;
                worksheet.Cells[5, 2].Value = events[0].ContestantBirthday;
                worksheet.Cells[6, 2].Value = events[0].ContestantSchool;
                worksheet.Cells[7, 2].Value = events[0].ContestantEmail;
                worksheet.Cells[8, 2].Value = events[0].ContactName;
                worksheet.Cells[9, 2].Value = events[0].ContactEmail;
                worksheet.Cells[10, 2].Value = events[0].ContactPhone;

                const int startRow = 13;
                int row = startRow;
                foreach (ContestantEvent e in events)
                {
                    worksheet.Cells[row, 1].Value = e.Category;
                    worksheet.Cells[row, 2].Value = CompetitionControlData.DivisionReportNames[e.Division];
                    worksheet.Cells[row, 3].Value = e.CompetitionClass;
                    worksheet.Cells[row, 4].Value = e.CompetitionTime;
                    worksheet.Cells[row, 5].Value = e.CompetitionRoom;
                    worksheet.Cells[row, 6].Value = e.TeamName;
                    row++;
                }

                package.Save();

                if (sendEmailFromReport)
                {
                    string recepient = events[0].ContestantEmail;
                    string cc = events[0].ContactEmail;
                    SendContestantEmail(events[0].ContestantName, recepient, cc, reportFile.FullName);
                }

                return 1;
            }
        }

        private int GetHeadCount(CETalentApplciants allApplicants, string category, string division, string ceClass, string group)
        {
            int count = 0;
            foreach (CECompetitionEntry applicant in allApplicants.TalentApplicants)
            {
                if (applicant.Contact.Category != category || applicant.Contact.Division != division || applicant.Contact.Class != ceClass) continue;
                bool isTeam = applicant.Contact.IsTeam == true || applicant.Contact.Category.ToLower().StartsWith(TEAM);
                if ((group == TEAM && !isTeam) || (group == INDIVIDUAL && isTeam)) continue;
                count++;
            }
            return count;
        }

        private int GetHeadCount(CETalentApplciants allApplicants, string category, string division, string ceClass, bool isTeam)
        {
            int count = 0;
            foreach (CECompetitionEntry applicant in allApplicants.TalentApplicants)
            {
                if (applicant.Contact.Category != category || applicant.Contact.Division != division || applicant.Contact.Class != ceClass) continue;
                if (isTeam && applicant.Contact.IsTeam)
                {
                    count++;
                }
                else if (!isTeam && !applicant.Contact.IsTeam)
                {
                    count++;
                }
            }
            return count;
        }

        private List<string> GetContestantListWithMultiEvents()
        {
            CEEventSchedule eventSchedule = new CEEventSchedule();
            CETalentApplciants allApplicants = new CETalentApplciants();
            ContestantEvents contestantEvents = new ContestantEvents();

            // build up individual contestant list
            foreach (CECompetitionEntry applicant in allApplicants.TalentApplicants)
            {
                if (applicant.Contact.IsTeam) continue;
                EventSchedule ceEvent = eventSchedule.GetEventSchedule(applicant);
                if (ceEvent == null)
                {
                    string message = "No event is found for this applicant file: " + applicant.ContestantFile.ToString();
                    message = message + "Please fix the issue in the xml file and regenerate the reports";
                    ShowReportDialog(HttpUtility.JavaScriptStringEncode(message));
                    continue;
                }
                contestantEvents.Add(applicant.Contestants[0].ID, applicant, ceEvent, 0);
            }

            // add contestants that participate in team competition
            foreach (CECompetitionEntry applicant in allApplicants.TalentApplicants)
            {
                if (!applicant.Contact.IsTeam) continue;
                EventSchedule ceEvent = eventSchedule.GetEventSchedule(applicant);
                if (ceEvent == null)
                {
                    string message = "No event is found for this applicant file: " + applicant.ContestantFile.ToString();
                    message = message + "Please fix the issue in the xml file and regenerate the reports";
                    ShowReportDialog(HttpUtility.JavaScriptStringEncode(message));
                    continue;
                }
                for (int i = 0; i < applicant.Contestants.Count; i++)
                    contestantEvents.Add(applicant.Contestants[i].ID, applicant, ceEvent, i);
            }

            List<string> multiEventContestants = new List<string>();
            foreach (var contestantEvent in contestantEvents.ContestantList)
            {
                List<ContestantEvent> events = (List<ContestantEvent>)contestantEvent.Value;
                if (events.Count > 1) multiEventContestants.Add(contestantEvent.Key);
            }

            return multiEventContestants;
        }

        private bool IsMultiEventsContestant(List<string> multiEventContestants, string contestantId)
        {
            return multiEventContestants.Exists(s => s == contestantId);
        }

        private List<ContestantEvent> GetConflictSchedule(List<ContestantEvent> ceEvents)
        {
            List<ContestantEvent> conflictSchedule = new List<ContestantEvent>();
            ContestantEvent[] sortedEvents = (from e in ceEvents orderby e.StartTime ascending, e.EndTime ascending select e).ToArray();
            for (int i = 0; i < sortedEvents.Length - 1; i++)
            {
                // schedule conflict when the start time of next schedule falls within the current schedule
                if (sortedEvents[i + 1].StartTime < sortedEvents[i].EndTime || sortedEvents[i + 1].StartTime == sortedEvents[i].StartTime)
                {
                    // write to conflict list
                    bool addFirst = true;
                    if (conflictSchedule.Count > 0)
                    {
                        ContestantEvent ce = conflictSchedule.ElementAt(conflictSchedule.Count - 1);
                        addFirst = !(ce.Category == sortedEvents[i].Category && ce.Division == sortedEvents[i].Division && ce.CompetitionClass == sortedEvents[i].CompetitionClass);
                    }
                    
                    if (addFirst) conflictSchedule.Add(sortedEvents[i]);
                    conflictSchedule.Add(sortedEvents[i+1]);
                }
            }

            return conflictSchedule;
        }

        private int GetSaveMapValue(Dictionary<string, int> maps, string key)
        {
            int value = -1;
            try
            {
                value = maps[key];
            }
            catch
            {
                value = -1;
            }

            return value;
        }

        private bool SendContestantEmail(string contestant, string recepient, string cc, string excelFile)
        {
            bool emailSent = false;
            EmailInfo emailInfo = CEHelper.GetEmailConfiguration("report", "contestant");
            if (emailInfo != null && !string.IsNullOrEmpty(emailInfo.Message))
            {
                List<string> attachFiles = new List<string>();
                attachFiles.Add(excelFile);
                emailInfo.CCs.Add(cc);
                CEHelper.SendEmail(emailInfo.SmtpHost, emailInfo.SmtpPort, emailInfo.Sender, new string[] { recepient }, emailInfo.CCs, emailInfo.Subject, emailInfo.Message, emailInfo.IsHtml, attachFiles);
                emailSent = true;
            }
            return emailSent;
        }

        private void ShowReportDialog(string message)
        {
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "ShowReportDialog",
                "alert('" + message + "');",
                true);
        }

        private List<CECompetitionEntry> GetAwardedApplicants(CETalentApplciants allApplicants, bool isCertificate = false)
        {
            List<CECompetitionEntry> awardedApplicants = new List<CECompetitionEntry>();
            foreach (CECompetitionEntry entry in allApplicants.TalentApplicants)
            {
                if (entry.Award != AwardType.Unknown && entry.Award != AwardType.None)
                {
                    awardedApplicants.Add(entry);
                }
            }

            if (isCertificate)
            {
                awardedApplicants = awardedApplicants.Where(c => c.Award <= AwardType.HonorableMentioned)
                                                     .OrderBy(c => GroupCount(c.Contestants.Count))
                                                     .ThenBy(c => Enum.Parse(typeof(CategoryType), c.Contact.Category, true))
                                                     .ThenBy(c => Enum.Parse(typeof(DivisionType), c.Contact.Division, true))
                                                     .ThenBy(c => c.Contact.Class)
                                                     .ThenBy(c => (int)c.Award)
                                                     .ToList();
            }
            else
            {
                awardedApplicants = awardedApplicants.Where(c => c.Award <= AwardType.HonorableMentioned)
                                                     .OrderBy(c => Enum.Parse(typeof(CategoryType), c.Contact.Category, true))
                                                     .ThenBy(c => Enum.Parse(typeof(DivisionType), c.Contact.Division, true))
                                                     .ThenBy(c => c.Contact.Class)
                                                     .ThenBy(c => GroupCount(c.Contestants.Count))
                                                     .ThenBy(c => (int)c.Award)
                                                     .ToList();
            }

            return awardedApplicants;
        }

        private List<CECompetitionEntry> GetteamTalentApplicants(List<CECompetitionEntry> awardedApplicants)
        {
            // sort by category, division, class, places
            List<CECompetitionEntry> teamTalentApplicants = new List<CECompetitionEntry>();
            teamTalentApplicants = awardedApplicants.Where(c => c.Contact.TeamName != string.Empty && (c.Contact.Category == "TeamSing" || c.Contact.Category == "TeamTalent" || c.Contact.Category == "TeamLanguage"))
                                                    .OrderBy(c => Enum.Parse(typeof(CategoryType), c.Contact.Category, true))
                                                    .ThenBy(c => Enum.Parse(typeof(DivisionType), c.Contact.Division, true))
                                                    .ThenBy(c => c.Contact.Class)
                                                    .ThenBy(c => (int)c.Award)
                                                    .ToList();
            return teamTalentApplicants;
        }

        private int GroupCount(int count)
        {
            return count <= 1 ? 1 : 2;
        }

        private string Wordify(string s, string delimiter = " ")
        {
            return System.Text.RegularExpressions.Regex.Replace(s, "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1" + delimiter);
        }

        private bool RequireClass(string category)
        {
            return category == "Story" || category == "Speech" || category == "Poetry";
        }

        private bool NoCompetitionEvent(string category, string division)
        {
            return (category == "Poetry" && division != "K2") ||
                   (category == "Story" && division != "Elementary") ||
                   (category == "Speech" && (division == "K2" || division == "Elementary")) ||
                   (category == "TeamBowl" && (division == "K2" || division == "Elementary"));
        }

        private bool IsTeam(CECompetitionEntry entry)
        {
            return entry.Contestants.Count > 1;
        }

        private string PlaceText(AwardType award, bool isChinese)
        {
            switch (award)
            {
                case AwardType.FirstPlace: return isChinese ? "第 一 名" : "1st Place";
                case AwardType.SecondPlace: return isChinese ? "第 二 名" : "2nd Place";
                case AwardType.ThirdPlace: return isChinese ? "第 三 名" : "3rd Place";
                case AwardType.HonorableMentioned: return isChinese ? string.Empty : "Honorable Mentioned";
                default: return string.Empty;
            }
        }

        private string CategoryText(CECompetitionContact contact)
        {
            string categoryText = CompetitionControlData.CategoryEnglishNames[contact.Category];
            return (contact.IsTeam || !contact.Category.StartsWith("Team")) ? categoryText : categoryText.Replace("Group", "Individual");
        }

        private string MakeCertificateFileName(CECompetitionEntry winner, CEContestant contestant, string extension)
        {
            return string.Format("{0}_{1}{2}_{3}_{4}_{5}_{6}.{7}", 
                                 winner.Contact.Category,
                                 winner.Contact.Division,
                                 string.IsNullOrEmpty(winner.Contact.Class) ? string.Empty : "_" + winner.Contact.Class,
                                 winner.Contact.IsTeam ? "Team" : "Individual",
                                 winner.Award.ToString(),
                                 contestant.FirstName, contestant.LastName,
                                 extension);
        }
    }

    internal class ContestantEvents
    {
        Dictionary<string, object> _contestantList;

        public Dictionary<string, object> ContestantList
        {
            get { return _contestantList; } 
        }

        public ContestantEvents()
        {
            _contestantList = new Dictionary<string, object>();
        }

        public void Add(string contestant, CECompetitionEntry entry, EventSchedule ceEvent, int index)
        {
            List<ContestantEvent> events = null;
            if (!_contestantList.ContainsKey(contestant))
            {
                events = new List<ContestantEvent>();
            }
            else
            {
                events = (List<ContestantEvent>)_contestantList[contestant];
            }

            if (events != null)
            {
                if (events.Count > 0)
                {
                    string eventTime = ceEvent.EventTime;
                    string earliestTime = events[0].CompetitionTime;
                    string latestTime = events[events.Count - 1].CompetitionTime;
                    if (CompareEventTime(earliestTime, eventTime) > 0)
                    {
                        events.Insert(0, new ContestantEvent(entry, ceEvent, index));
                    }
                    else if (CompareEventTime(latestTime, eventTime) > 0)
                    {
                        events.Insert(events.Count - 1, new ContestantEvent(entry, ceEvent, index));
                    }
                    else
                    {
                        events.Add(new ContestantEvent(entry, ceEvent, index));
                    }
                }
                else
                {
                    events.Add(new ContestantEvent(entry, ceEvent, index));
                }
                _contestantList[contestant] = events;
            }
        }

        private int CompareEventTime(string time1, string time2)
        {
            int t1 = Int32.Parse((time1.Split(new char[] { ':' }))[0]);
            int t2 = Int32.Parse((time2.Split(new char[] { ':' }))[0]);
            if (t1 < 6) t1 += 12;
            if (t2 < 6) t2 += 12;
            return t1 - t2;
        }
    }

    internal class ContestantEvent 
    {
        public ContestantEvent(CECompetitionEntry entry, EventSchedule ceEvent, int index)
        {
            ContestantID = entry.Contestants[index].ID;
            ContestantBirthday = entry.Contestants[index].Birthday;
            ContestantSchool = entry.Contestants[index].School;
            if (string.IsNullOrEmpty(entry.Contestants[index].ChineseName))
                ContestantName = string.Format("{0} {1}", entry.Contestants[index].FirstName, entry.Contestants[index].LastName);
            else
                ContestantName = string.Format("{0} {1} ({2})", entry.Contestants[index].FirstName, entry.Contestants[index].LastName, entry.Contestants[index].ChineseName);
            ContestantEmail = entry.Contestants[index].Email;
            ContactName = entry.Contact.ContactName;
            ContactEmail = entry.Contact.ContactEmail;
            ContactPhone = entry.Contact.ContactPhone;
            Category = entry.Contact.Category;
            Division = entry.Contact.Division;
            CompetitionClass = entry.Contact.Class;
            TeamName = entry.Contact.TeamName;
            CompetitionTime = ceEvent.EventTime;
            CompetitionRoom = ceEvent.EventRoom;
            StartTime = 0.0;
            EndTime = 0.0;

            string[] schedules = CompetitionTime.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            if (schedules.Length == 2)
            {
                double start = 0.0, end = 0.0;
                if (Double.TryParse(schedules[0].Replace(':', '.'), out start) && Double.TryParse(schedules[1].Replace(':', '.'), out end))
                {
                    StartTime = start;
                    if (end < start) end += 12.0; // deal with schedule comes in as, say, 12:00 - 1:00; the end time will be changed to 13.0
                    EndTime = end;

                    // ensure 24-hour clock is used by assuming the start time will be after 8am.
                    if (StartTime < 8.0) StartTime += 12.0;
                    if (EndTime < 8.0) EndTime += 12.0;
                }
            }
        }

        public string ContestantID { get; set; }
        public string ContestantBirthday { get; set; }
        public string ContestantSchool { get; set; }
        public string ContestantName { get; set; }
        public string ContestantEmail { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string Category { get; set; }
        public string Division { get; set; }
        public string CompetitionClass { get; set; }
        public string TeamName { get; set; }
        public string CompetitionTime { get; set; }
        public string CompetitionRoom { get; set; }
        public double StartTime { get; set; }
        public double EndTime { get; set; }
    }
}