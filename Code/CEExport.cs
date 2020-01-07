using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace CE.Data
{
    public class CEExport
    {
        public static bool ExportTalentRegistation(HttpResponse response)
        {
            bool ok = false;
            try
            {
                // copy data to DataTable structure
                DataTable dt = CreateTalentRegistrationTable();

                using (ExcelPackage pck = new ExcelPackage())
                {
                    response.Clear();

                    //Create the worksheet
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("CE Competition Registration");

                    //Load the datatable into the sheet, starting from cell A1. add the column names on row 1
                    ws.Cells["A1"].LoadFromDataTable(dt, true);

                    // Write it back to the client

                    string defautlFilename = CEHelper.GetCompetitionYear() + "TalentRegistration.xlsx";
                    response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    response.AddHeader("content-disposition", "attachment;  filename=" + defautlFilename);
                    response.BinaryWrite(pck.GetAsByteArray());

                    response.End();
                }
                ok = true;
            }
            catch
            {
            }

            return ok;
        }

        private static DataTable CreateTalentRegistrationTable()
        {
            DataTable dt = new DataTable("CompeittionReistration");
            dt.Columns.Add("ContestantID", typeof(string));
            dt.Columns.Add("ContestantLastName", typeof(string));
            dt.Columns.Add("ContestantFirstName", typeof(string));
            dt.Columns.Add("ContestantChineseName", typeof(string));
            dt.Columns.Add("ContestantBirthday", typeof(string));
            dt.Columns.Add("ContestantGrade", typeof(string));
            dt.Columns.Add("ContestantSchool", typeof(string));
            dt.Columns.Add("ContestantSchool2", typeof(string));
            dt.Columns.Add("Category", typeof(string));
            dt.Columns.Add("Division", typeof(string));
            dt.Columns.Add("Class", typeof(string));
            dt.Columns.Add("Award", typeof(string));
            dt.Columns.Add("ContestantLunch", typeof(string));
            dt.Columns.Add("ContestantEmail", typeof(string));
            dt.Columns.Add("ContactName", typeof(string));
            dt.Columns.Add("ContactEmail", typeof(string));
            dt.Columns.Add("ContactPhone", typeof(string));
            dt.Columns.Add("TeamName", typeof(string));
            dt.Columns.Add("TeamHeadCount", typeof(int));
            dt.Columns.Add("PaymentMethod", typeof(string));
            dt.Columns.Add("PaymentAmount", typeof(int));
            dt.Columns.Add("EntryDate", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("Check", typeof(string));
            dt.Columns.Add("Time", typeof(string));
            dt.Columns.Add("Room", typeof(string));
            dt.Columns.Add("EventOrder", typeof(string));

            CETalentApplciants talentApplications = new CETalentApplciants();
            if (talentApplications.TalentApplicants.Count > 0)
            {
                CEEventSchedule eventScheduleList = new CEEventSchedule();
                foreach (CECompetitionEntry applicant in talentApplications.TalentApplicants)
                {
                    EventSchedule eventSchedule = eventScheduleList.GetEventSchedule(applicant);
                    if (eventSchedule == null) eventSchedule = new EventSchedule();
                    foreach (CEContestant contestant in applicant.Contestants)
                    {
                        object[] row = new object[dt.Columns.Count];
                        int i = 0;
                        row[i++] = contestant.ID;
                        row[i++] = contestant.LastName;
                        row[i++] = contestant.FirstName;
                        row[i++] = contestant.ChineseName;
                        row[i++] = contestant.Birthday;
                        row[i++] = contestant.Grade;
                        row[i++] = contestant.School;
                        row[i++] = contestant.School2;
                        row[i++] = applicant.Contact.Category;
                        row[i++] = applicant.Contact.Division;
                        row[i++] = applicant.Contact.Class;
                        row[i++] = (applicant.Award == AwardType.None || applicant.Award == AwardType.Unknown) ? string.Empty : applicant.Award.ToString();
                        row[i++] = contestant.LunchProgram;
                        row[i++] = contestant.Email;
                        row[i++] = applicant.Contact.ContactName;
                        row[i++] = applicant.Contact.ContactEmail;
                        row[i++] = applicant.Contact.ContactPhone;
                        row[i++] = applicant.Contact.TeamName;
                        row[i++] = applicant.Contestants.Count;
                        row[i++] = applicant.PaymentMethod.ToString();
                        row[i++] = (int)applicant.PaymentAmount;
                        row[i++] = applicant.EntryDate;
                        row[i++] = applicant.Status.ToString();
                        row[i++] = applicant.CheckNumber;
                        row[i++] = eventSchedule.EventTime;
                        row[i++] = eventSchedule.EventRoom;
                        row[i++] = eventSchedule.EventOrder;

                        dt.Rows.Add(row);
                    }
                }
            }
            DataView dv = new DataView(dt);
            dv.Sort = "ContestantLastName ASC, ContestantFirstName ASC";
            return dv.ToTable();
        }

        public static bool ExportTourApplication(HttpResponse response)
        {
            bool ok = false;
            try
            {
                List<CEUser> reviewers = SiteUsers.FindUsersWithRole(CEConstants.CE_TOUR_ROLE);
                CETourApplicants tourApplicants = new CETourApplicants();

                using (ExcelPackage pck = new ExcelPackage())
                {
                    response.Clear();

                    DataTable dtFirstCut = CreateTourBaseTable(tourApplicants);
                    ExcelWorksheet wsFirstCut = pck.Workbook.Worksheets.Add("First Cut"); //Create the worksheet
                    wsFirstCut.Cells["A1"].LoadFromDataTable(dtFirstCut, true); //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1

                    List<DataTable> reviewerTables = new List<DataTable>();
                    foreach (CEUser reviewer in reviewers)
                    {
                        DataTable dtReviewer = CreateTourBaseUserTable(dtFirstCut, tourApplicants, reviewer);
                        ExcelWorksheet wsReviewer = pck.Workbook.Worksheets.Add(reviewer.DisplayName); //Create the worksheet
                        wsReviewer.Cells["A1"].LoadFromDataTable(dtReviewer, true); //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
                        reviewerTables.Add(dtReviewer);
                    }

                    DataTable dtSummary = CreateTourSummaryTable(tourApplicants, reviewers, reviewerTables);
                    ExcelWorksheet wsSummary = pck.Workbook.Worksheets.Add("Summary"); //Create the worksheet
                    wsSummary.Cells["A1"].LoadFromDataTable(dtSummary, true); //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
                    

                    // example: how to format the header for column 1-3
                    //using (ExcelRange rng = ws.Cells["A1:C1"])
                    //{
                    //    rng.Style.Font.Bold = true;
                    //    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    //    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));  //Set color to dark blue
                    //    rng.Style.Font.Color.SetColor(Color.White);
                    //}

                    // example: how to Format Column 1 as numeric 
                    //using (ExcelRange col = ws.Cells[2, 1, 2 + dt.Rows.Count, 1])
                    //{
                    //    col.Style.Numberformat.Format = "#,##0.00";
                    //    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    //}

                    // Write it back to the client
                    string defautlFilename = CEHelper.GetCompetitionYear() + "TourApplication.xlsx";
                    response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    response.AddHeader("content-disposition", "attachment;  filename=" + defautlFilename);
                    response.BinaryWrite(pck.GetAsByteArray());

                    response.End();
                }

                ok = true;
            }
            catch
            {
            }

            return ok;
        }

        private static DataTable CreateTourBaseTable(CETourApplicants tourApplicants)
        {
            DataTable dt = new DataTable("TourApplicationBase");

            // -------------------------------------------------------
            // these are for matching existing Excel sheet columns
            // -------------------------------------------------------
            dt.Columns.Add("ApplicationID", typeof(int));
            dt.Columns.Add("NAME", typeof(string));
            dt.Columns.Add("DISTRICT", typeof(string));
            dt.Columns.Add("SCHOOL", typeof(string));
            dt.Columns.Add("EMAIL", typeof(string));
            dt.Columns.Add("PHONE", typeof(string));
            dt.Columns.Add("Application Material", typeof(int));
            dt.Columns.Add("Relevancy", typeof(int));
            dt.Columns.Add("Lesson Plan", typeof(int));
            dt.Columns.Add("TotalScores", typeof(int));
            dt.Columns.Add("Rank", typeof(int));
            dt.Columns.Add("Recommand for Interview?", typeof(string));
            dt.Columns.Add("Comments", typeof(string));
            dt.Columns.Add("NormalizedScore", typeof(double));
            
            if (tourApplicants.TourApplicants.Count > 0)
            {
                int applicationNo = 1;
                foreach (CETourApplicant applicant in tourApplicants.TourApplicants)
                {
                    if (applicant.Status == ReviewStatus.Rejected || applicant.Status == ReviewStatus.Apply) continue;

                    object[] row = new object[dt.Columns.Count];
                    int count = 0;
                    row[count++] = applicationNo++;
                    row[count++] = applicant.FirstName + " " + applicant.LastName;
                    row[count++] = applicant.District;
                    row[count++] = applicant.School;
                    row[count++] = applicant.Email;
                    row[count++] = string.IsNullOrEmpty(applicant.Phone) ? applicant.CellPhone : applicant.Phone;

                    dt.Rows.Add(row);
                }
            }
            return dt;
        }

        private static DataTable CreateTourBaseUserTable(DataTable dt, CETourApplicants tourApplicants, CEUser user)
        {
            dt.Rows.Clear();
            if (tourApplicants.TourApplicants.Count > 0)
            {
                int applicationNo = 1;
                foreach (CETourApplicant applicant in tourApplicants.TourApplicants)
                {
                    if (applicant.Status == ReviewStatus.Rejected || applicant.Status == ReviewStatus.Apply) continue;

                    object[] row = new object[dt.Columns.Count];
                    int count = 0;
                    row[count++] = applicationNo++;
                    row[count++] = applicant.FirstName + " " + applicant.LastName;
                    row[count++] = applicant.District;
                    row[count++] = applicant.School;
                    row[count++] = applicant.Email;
                    row[count++] = string.IsNullOrEmpty(applicant.Phone) ? applicant.CellPhone : applicant.Phone;

                    foreach (CommitteeComment comment in applicant.ReviewComments)
                    {
                        if (string.Compare(comment.Name, user.UserName, true) == 0)
                        {
                            row[count++] = comment.ApplicationScore;
                            row[count++] = comment.RelevancyScore;
                            row[count++] = comment.LessonPlanScore;
                            row[count++] = comment.TotalScore;
                            row[count++] = 0; // rank
                            row[count++] = string.Empty; // recommended for interview
                            row[count++] = comment.Comment;
                            break;
                        }
                    }
                    dt.Rows.Add(row);
                }
            }

            // this would sort the datarow into descending order based on TotalScores
            DataView dv = new DataView(dt);
            dv.Sort = "TotalScores DESC";
            dt = dv.ToTable();
            int rankIndex = 10; // this is the index of 'Rank' column
            int rank = 0;
            int lastTotal = -1;
            foreach (DataRow row in dt.Rows)
            {
                if (row[rankIndex - 1] is System.DBNull)
                    break;
                else if (lastTotal != (int)row[rankIndex - 1])
                {
                    row[rankIndex] = ++rank;
                    lastTotal = (int)row[rankIndex - 1];
                }
                else
                    row[rankIndex] = rank;
            }

            if (rank  > 0) // this would be the highest rank
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[rankIndex] is System.DBNull) continue;
                    SetNormalizedScore(row, rank);
                }
            }

            // restore the original datarow order
            dv = new DataView(dt);
            dv.Sort = "ApplicationID ASC";
            dt = dv.ToTable();

            return dt;
        }

        private static DataTable CreateTourSummaryTable(CETourApplicants tourApplicants, List<CEUser> users, List<DataTable> reviewerTables)
        {
            DataTable dt = new DataTable("TourApplicationBase");

            // -------------------------------------------------------
            // these are for matching existing Excel sheet columns
            // -------------------------------------------------------
            dt.Columns.Add("ApplicationID", typeof(int));
            dt.Columns.Add("NAME", typeof(string));
            dt.Columns.Add("DISTRICT", typeof(string));
            dt.Columns.Add("SCHOOL", typeof(string));
            dt.Columns.Add("EMAIL", typeof(string));
            dt.Columns.Add("PHONE", typeof(string));
            foreach (CEUser user in users) dt.Columns.Add(user.DisplayName, typeof(double)); // these are the tour reviewers' scores
            dt.Columns.Add("SUBTotal", typeof(double));
            dt.Columns.Add("ScoreRank", typeof(int));
            dt.Columns.Add("STD", typeof(double));
            dt.Columns.Add("Recommand for Interview?", typeof(string));
            dt.Columns.Add("Comments", typeof(string));

            int rankIndex = 0;
            int startScoreIndex = 6;
            int normalizedScoreIndex = 13;
            if (tourApplicants.TourApplicants.Count > 0)
            {
                int applicationID = 1;
                double totalScores = 0.0;
                foreach (CETourApplicant applicant in tourApplicants.TourApplicants)
                {
                    if (applicant.Status == ReviewStatus.Rejected || applicant.Status == ReviewStatus.Apply) continue;

                    object[] row = new object[dt.Columns.Count];
                    int count = 0;
                    row[count++] = applicationID++;
                    row[count++] = applicant.FirstName + " " + applicant.LastName;
                    row[count++] = applicant.District;
                    row[count++] = applicant.School;
                    row[count++] = applicant.Email;
                    row[count++] = string.IsNullOrEmpty(applicant.Phone) ? applicant.CellPhone : applicant.Phone;
                    double subtotal = 0.0;
                    int reviewerIndex = -1;
                    foreach (CEUser user in users)
                    {
                        reviewerIndex++;
                        bool found = false;
                        foreach (CommitteeComment comment in applicant.ReviewComments)
                        {
                            if (string.Compare(comment.Name, user.UserName, true) == 0)
                            {
                                DataRow reviewerRow = reviewerTables[reviewerIndex].Rows[dt.Rows.Count];
                                double normalizedScore = (double)reviewerRow[normalizedScoreIndex];
                                row[count++] = normalizedScore;
                                subtotal += normalizedScore;
                                found = true;
                                break;
                            }
                        }
                        if (found == false)
                        {
                            row[count++] = 0.0;
                        }
                    }
                    totalScores += subtotal;
                    row[count++] = subtotal;
                    rankIndex = count;
                    row[count++] = 0;   // score rank placeholder
                    if (users.Count > 1) row[count++] = GetSTD(row, startScoreIndex, users.Count); // STD
                    row[count++] = applicant.Status == ReviewStatus.Interview ? "YES" : string.Empty;
                    row[count++] = string.Empty; // comment

                    dt.Rows.Add(row);
                }
            }

            // set the rank
            DataView dv = new DataView(dt);
            dv.Sort = "SUBTotal DESC";
            dt = dv.ToTable();
            int rank = 0;
            double lastTotal = -1.0;
            foreach (DataRow row in dt.Rows)
            {
                if (row[rankIndex - 1] is System.DBNull) 
                    break;
                else if (lastTotal != (double)row[rankIndex - 1])
                {
                    row[rankIndex] = ++rank;
                    lastTotal = (double)row[rankIndex - 1];
                }
                else
                    row[rankIndex] = rank;
            }

            // restore the original datarow order
            dv = new DataView(dt);
            dv.Sort = "ApplicationID ASC";
            dt = dv.ToTable();

            return dt;
        }

        private static DataTable CreateTourApplicationTable(CETourApplicants tourApplicants, List<CEUser> users)
        {
            DataTable dt = new DataTable("TourApplicationBase");

            // these are for exporting the data from C# classes
            dt.Columns.Add("FistName", typeof(string));
            dt.Columns.Add("LastName", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("DaytimePhone", typeof(string));
            dt.Columns.Add("Cellphone", typeof(string));
            dt.Columns.Add("District", typeof(string));
            dt.Columns.Add("School", typeof(string));
            dt.Columns.Add("GradeTaught", typeof(string));
            dt.Columns.Add("SubjectTaught", typeof(string));
            dt.Columns.Add("Gender", typeof(string));
            dt.Columns.Add("LearnProgram", typeof(string));
            dt.Columns.Add("Specialty", typeof(string));
            dt.Columns.Add("Reference1", typeof(string));
            dt.Columns.Add("Reference2", typeof(string));
            dt.Columns.Add("Reference3", typeof(string));
            dt.Columns.Add("Questionaire1", typeof(string));
            dt.Columns.Add("Questionaire2", typeof(string));
            dt.Columns.Add("Questionaire3", typeof(string));
            dt.Columns.Add("Questionaire4", typeof(string));
            dt.Columns.Add("Comment", typeof(string));
            dt.Columns.Add("ResumeFile", typeof(string));
            dt.Columns.Add("LessonPlanFile", typeof(string));
            dt.Columns.Add("UserFile1", typeof(string));
            dt.Columns.Add("UserFile2", typeof(string));
            dt.Columns.Add("ApplyDate", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("ApplicationFile", typeof(string));

            if (tourApplicants.TourApplicants.Count > 0)
            {
                foreach (CETourApplicant applicant in tourApplicants.TourApplicants)
                {
                    object[] row = new object[27];
                    row[0] = applicant.FirstName;
                    row[1] = applicant.LastName;
                    row[2] = applicant.Email;
                    row[3] = applicant.Phone;
                    row[4] = applicant.CellPhone;
                    row[5] = applicant.District;
                    row[6] = applicant.School;
                    row[7] = applicant.Grade;
                    row[8] = applicant.Subject;
                    row[9] = applicant.Gender;
                    row[10] = HttpUtility.HtmlDecode(applicant.Program);
                    row[11] = HttpUtility.HtmlDecode(applicant.Specialty);
                    row[12] = applicant.Reference1;
                    row[13] = applicant.Reference2;
                    row[14] = applicant.Reference3;
                    row[15] = HttpUtility.HtmlDecode(applicant.Questionaire1.Answer);
                    row[16] = HttpUtility.HtmlDecode(applicant.Questionaire2.Answer);
                    row[17] = HttpUtility.HtmlDecode(applicant.Questionaire3.Answer);
                    row[18] = HttpUtility.HtmlDecode(applicant.Questionaire4.Answer);
                    row[19] = HttpUtility.HtmlDecode(applicant.UserComment);
                    row[20] = (string.IsNullOrEmpty(applicant.ResumeFile) ? string.Empty : Path.GetFileName(applicant.ResumeFile));
                    row[21] = (string.IsNullOrEmpty(applicant.LessonPlanFile) ? string.Empty : Path.GetFileName(applicant.LessonPlanFile));
                    row[22] = (string.IsNullOrEmpty(applicant.UserFile1) ? string.Empty : Path.GetFileName(applicant.UserFile1));
                    row[23] = (string.IsNullOrEmpty(applicant.UserFile2) ? string.Empty : Path.GetFileName(applicant.UserFile2));
                    row[24] = applicant.EntryDate;
                    row[25] = applicant.Status.ToString();
                    row[26] = (string.IsNullOrEmpty(applicant.ApplicantFile) ? string.Empty : Path.GetFileName(applicant.ApplicantFile));

                    dt.Rows.Add(row);
                }
            }
            return dt;
        }

        private static int SetRanks(DataRowCollection rows, int scoreIndex)
        {
            List<int> scores = new List<int>();
            foreach (DataRow row in rows)
            {
                if (row[scoreIndex] is System.DBNull)
                    scores.Add(-1);
                else
                    scores.Add((int)row[scoreIndex]);
            }
            scores.Sort();
            int rank = 0;
            int lastScore = -1;
            foreach (int score in scores)
            {
                if (score == -1) continue;

                if (lastScore != score)
                {
                    ++rank;
                    lastScore = score;
                }
            }

            foreach (DataRow row in rows)
            {
            }

            return rank;
        }

        private static void SetNormalizedScore(DataRow row, int highestRank)
        {
            if (highestRank <= 0) return;

            int rankIndex = 10; // this is the index of 'Rank' column
            int normalizedScoreIndex = 13;  // this is the index of 'NoromalizedScore' column

            // the formula to calculate the normalized score is: 5 - rank/highestrank * 5
            double score = 5.0 - (double)((int)row[rankIndex]) / (double)highestRank * 5.0;
            score = Math.Truncate((score + 0.005) * 100) / 100; // limited to 2 decimal points with rounding
            row[normalizedScoreIndex] = score;
        }

        private static double GetSTD(object[] row, int startScoreIndex, int totalSamples)
        {
            // formula = sqrt((x - x-avg) * (x - x-avg) / (count - 1)
            int endScoreidnex = startScoreIndex + totalSamples - 1;
            double sum = 0.0;
            for (int i = startScoreIndex; i <= endScoreidnex; i++) sum += (double)row[i];
            double avg = sum / totalSamples;
            sum = 0.0;
            for (int i = startScoreIndex; i <= endScoreidnex; i++)
            {
                double diff = (double)row[i] - avg;
                sum += diff * diff;
            }

            double std = Math.Sqrt(sum / (totalSamples - 1));
            return Math.Truncate((std + 0.005) * 100) / 100; // limited to 2 decimal points with rounding
        }
    }

    internal class EventScore
    {
        public EventScore()
        {
            EnglishName = string.Empty;
            ChineseName = string.Empty;
            Percentage = string.Empty;
            Best = string.Empty;
            Better = string.Empty;
            Good = string.Empty;
            Fair = string.Empty;
        }

        public EventScore(string englishName, string chineseName, string percentage, string best, string better, string good, string fair)
        {
            EnglishName = englishName;
            ChineseName = chineseName;
            Percentage = percentage;
            Best = best;
            Better = better;
            Good = good;
            Fair = fair;
        }

        public string EnglishName { get; set; }
        public string ChineseName { get; set; }
        public string Percentage { get; set; }
        public string Best { get; set; }
        public string Better { get; set; }
        public string Good { get; set; }
        public string Fair { get; set; }
    }

    internal class EventSchedule
    {
        public EventSchedule()
        {
            Category = string.Empty;
            Division = string.Empty;
            Class = string.Empty;
            EventTime = string.Empty;
            EventRoom = string.Empty;
            EventOrder = string.Empty;
            Facilitator = string.Empty;
            Referee = string.Empty;
            EventScores = new List<EventScore>();
        }

        public string Category { get; set; }
        public string Division { get; set; }
        public string Class { get; set; }
        public string EventTime { get; set; }
        public string EventRoom { get; set; }
        public string EventOrder { get; set; }
        public string Facilitator { get; set; }
        public string Referee { get; set; }
        public List<EventScore> EventScores { get; set; }

        public void AddEventScore(EventScore eventScore)
        {
            EventScores.Add(eventScore);
        }

        public void AddEventScore(string englishName, string chineseName, string percentage, string best, string better, string good, string fair)
        {
            EventScores.Add(new EventScore(englishName, chineseName, percentage, best, better, good, fair));
        }
    }

    internal class CEEventSchedule
    {
        private List<EventSchedule> _eventScheduleList = null;

        public CEEventSchedule()
        {
            Load();
        }

        public void Load()
        {
            string applicantFolder = Path.Combine(CEHelper.GetDataPath() + CEConstants.CE_REPORT_FOLDER, CEHelper.GetCompetitionYear());
            if (!Directory.Exists(applicantFolder))
            {
                Directory.CreateDirectory(applicantFolder);
            }

            string physicalPath = Path.Combine(applicantFolder, CEConstants.CE_SCHEDULE_XLSX);
            _eventScheduleList = new List<EventSchedule>();
            var eventScheduleFile = new FileInfo(physicalPath);
            using (var package = new ExcelPackage(eventScheduleFile))
            {
                ExcelWorkbook workBook = package.Workbook;
                if (workBook != null)
                {
                    if (workBook.Worksheets.Count > 0)
                    {
                        const int startRow = 2; // row 1 is header that we skip
                        ExcelWorksheet currentWorksheet = workBook.Worksheets[1]; // first sheet index starts from 1
                        for (int row = startRow; row <= currentWorksheet.Dimension.End.Row; row++)
                        {
                            EventSchedule eventSchedule = new EventSchedule();
                            eventSchedule.Division = CEHelper.GetSafeCellString(currentWorksheet.Cells[row, 5].Value);
                            eventSchedule.Category = CEHelper.GetSafeCellString(currentWorksheet.Cells[row, 6].Value);
                            eventSchedule.Class = CEHelper.GetSafeCellString(currentWorksheet.Cells[row, 7].Value);
                            eventSchedule.EventTime = CEHelper.GetSafeCellString(currentWorksheet.Cells[row, 9].Value);
                            eventSchedule.EventRoom = CEHelper.GetSafeCellString(currentWorksheet.Cells[row, 10].Value);
                            eventSchedule.EventOrder = CEHelper.GetSafeCellString(currentWorksheet.Cells[row, 11].Value);
                            eventSchedule.Facilitator = CEHelper.GetSafeCellString(currentWorksheet.Cells[row, 12].Value);
                            eventSchedule.Referee = CEHelper.GetSafeCellString(currentWorksheet.Cells[row, 13].Value);

                            int ScoreStart = 14;
                            int ScoreSet = 7;
                            int ScoreCount = 5;
                            for (int i = ScoreStart; i < ScoreStart + ScoreSet * ScoreCount; i = i + ScoreSet)
                            {
                                if (currentWorksheet.Cells[row, i].Value == null || currentWorksheet.Cells[row, i].Value.ToString() == string.Empty) break;
                                EventScore eventScore = new EventScore();
                                eventScore.EnglishName = CEHelper.GetSafeCellString(currentWorksheet.Cells[row, i].Value);
                                eventScore.ChineseName = CEHelper.GetSafeCellString(currentWorksheet.Cells[row, i+1].Value);
                                eventScore.Percentage = CEHelper.GetSafeCellString(currentWorksheet.Cells[row, i+2].Value);
                                eventScore.Best = CEHelper.GetSafeCellString(currentWorksheet.Cells[row, i+3].Value);
                                eventScore.Better = CEHelper.GetSafeCellString(currentWorksheet.Cells[row, i+4].Value);
                                eventScore.Good = CEHelper.GetSafeCellString(currentWorksheet.Cells[row, i+5].Value);
                                eventScore.Fair = CEHelper.GetSafeCellString(currentWorksheet.Cells[row, i + 6].Value);
                                eventSchedule.AddEventScore(eventScore);
                            }

                            _eventScheduleList.Add(eventSchedule);
                        }
                    }
                }
            }
        }

        public int GetScoreSetCount(string category, string division, string ceClass)
        {
            int scoreSetCount = 0;
            EventSchedule schedule = _eventScheduleList.Find(i => i.Category == category && i.Division == division && i.Class == ceClass);
            if (schedule != null) scoreSetCount = schedule.EventScores.Count;
            return scoreSetCount;
        }

        public EventSchedule GetEventSchedule(CECompetitionEntry applicant)
        {
            if (_eventScheduleList == null) return null;
            //string ceClass = CETalentApplciants.GetCategoryClass(applicant);
            //return _eventScheduleList.Find(i => i.Category == applicant.Contact.Category && i.Division == applicant.Contact.Division && i.Class == ceClass);
            return _eventScheduleList.Find(i => i.Category == applicant.Contact.Category && i.Division == applicant.Contact.Division && i.Class == applicant.Contact.Class);
        }
    }
}