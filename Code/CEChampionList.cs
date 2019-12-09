using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using CE.Data;

namespace CE.Data
{
    [Serializable]
    public class CEChampionList
    {
        public CEChampionList()
        {
        }

        public List<CEChampion> Load()
        {
            string applicantFolder = Path.Combine(CEHelper.GetDataPath() + CEConstants.CE_TALENT_CHAMPION_FOLDER, CEHelper.GetProgramYear());
            if (!Directory.Exists(applicantFolder))
            {
                Directory.CreateDirectory(applicantFolder);
            }

            string physicalPath = Path.Combine(applicantFolder, CEConstants.CE_CHAMPION_XLSX);
            List<CEChampion> champions = new List<CEChampion>();
            var championFile = new FileInfo(physicalPath);
            using (var package = new ExcelPackage(championFile))
            {
                ExcelWorkbook workBook = package.Workbook;
                if (workBook != null)
                {
                    if (workBook.Worksheets.Count > 0)
                    {
                        const int startRow = 2; // row 1 is headert that we skip
                        string workSheetName = CEHelper.GetProgramYear();
                        ExcelWorksheet currentWorksheet = workBook.Worksheets[workSheetName];
                        if (currentWorksheet == null)
                        {
                            currentWorksheet = workBook.Worksheets[1];
                        }
                        for (int row = startRow; row <= currentWorksheet.Dimension.End.Row; row++)
                        {
                            CEChampion champion = new CEChampion();
                            if (currentWorksheet.Cells[row, 1].Value == null) break;
                            string lastName = currentWorksheet.Cells[row, 1].Value.ToString();
                            string firstName = currentWorksheet.Cells[row, 2].Value.ToString();
                            string birthday = currentWorksheet.Cells[row, 3].Value.ToString();
                            if (birthday.Contains(" ")) // the birthday string comes in as m/d/yyyy hh:mm:ssAM
                            {
                                DateTime bday;
                                if (DateTime.TryParse(birthday, out bday))
                                {
                                    birthday = bday.ToString("MM/dd/yyyy");
                                }
                            }
                            if (string.IsNullOrEmpty(lastName)) break;

                            champion.ID = CEContestant.MakeID(firstName, lastName, birthday);

                            champion.Category = (string)currentWorksheet.Cells[row, 4].Value;
                            champion.Division = (string)currentWorksheet.Cells[row, 5].Value;
                            champion.Class = (string)currentWorksheet.Cells[row, 6].Value;

                            champions.Add(champion);
                        }
                    }
                }
            }

            return champions;
        }

        public static bool ChampionExist(List<CEChampion> champions, string id, string category, string division, string ceClass)
        {
            if (champions != null && champions.Count > 0)
                return champions.Exists(i => i.ID == id && i.Category == category && i.Division == division && i.Class == ceClass);
            else
                return false;
        }
    }

    [Serializable]
    public class CEChampion
    {
        public CEChampion()
        {
        }

        public CEChampion(string id, string category, string division, string ceClass)
        {
            ID = id;
            Category = category;
            Division = division;
            Class = ceClass;
        }

        public string ID { get; set; }
        public string Category { get; set; }
        public string Division { get; set; }
        public string Class { get; set; }
    }
}
