using System;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Services;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using CE.Data;

namespace CE.Application.Public.Talent.Registration
{
    public partial class schools : System.Web.UI.Page
    {
        private const string SchoolFilePathTemplate = "{0}\\Schools\\{1}";

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod]
        public static string GetSchoolsList()
        {
            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
            List<string> schoolsList = new List<string>();

            // read data from schools List.xslx
            string physicalPath = string.Format(SchoolFilePathTemplate, CEHelper.GetDataPath(), CEConstants.CE_SCHOOL_LIST_FILE);
            var schoolListFile = new FileInfo(physicalPath);
            using (var package = new ExcelPackage(schoolListFile))
            {
                ExcelWorkbook workBook = package.Workbook;
                if (workBook != null)
                {
                    if (workBook.Worksheets.Count > 0)
                    {
                        const int startRow = 2; // row 1 is header that we skip
                        ExcelWorksheet currentWorksheet = workBook.Worksheets[1];
                        for (int row = startRow; row <= currentWorksheet.Dimension.End.Row; row++)
                        {
                            if (currentWorksheet.Cells[row, 1].Value == null) break;
                            string school = currentWorksheet.Cells[row, 1].Value.ToString();
                            if (string.IsNullOrEmpty(school)) break;
                            schoolsList.Add(school);
                        }
                    }
                }
            }
            var jsonData = TheSerializer.Serialize(schoolsList.ToArray());
            return jsonData;
        }
    }
}