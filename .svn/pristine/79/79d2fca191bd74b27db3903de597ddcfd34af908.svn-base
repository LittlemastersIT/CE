using System;
using System.IO;
using System.Collections.Generic;
using System.Web;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace CE.Content
{
    public class CERule
    {
    }
    /// <summary>
    /// Page Content Retrieval from xml Content Source
    /// </summary>
    public class RuleContentRetriever
    {
        private const string RuleParagraphTemplate = "<b>{0}</b><br/><br/>{1}<br/></br>";
        private const string OrderListTemplate = "<ul>{0}</ul>";
        private const string OrderItemTemplate = "<li>{0}</li>";

        #region Rule Page Content retrieval
        public static ArticleContent GetRulePageContent(string pageContentXlsx, string sheetName, string language, string pageTag)
        {
            ArticleContent articleContent = new ArticleContent();
            try
            {
                if (pageContentXlsx.StartsWith("/")) pageContentXlsx = pageContentXlsx.Substring(1);
                string physicalPath = HttpContext.Current.Request.PhysicalApplicationPath + pageContentXlsx.Replace('/', '\\');
                physicalPath = physicalPath.Replace("\\cn\\", "\\"); // rules file is language independent
                physicalPath = physicalPath.Replace("\\us\\", "\\"); // rules file is language independent

                // read rule xlsx file
                var ruleFile = new FileInfo(physicalPath);
                using (var package = new ExcelPackage(ruleFile))
                {
                    ExcelWorkbook workBook = package.Workbook;
                    if (workBook != null)
                    {
                        if (workBook.Worksheets.Count > 0)
                        {
                            int startRow = 1; // row 1 is header that we skip
                            if (language == "us") startRow = 2;
                            ExcelWorksheet currentWorksheet = workBook.Worksheets[sheetName];
                            if (currentWorksheet == null) return articleContent; // no content for wrong sheet name
                            string title = GetSafeExcelCellValue(currentWorksheet.Cells[startRow, 2].Value);
                            string description = GetSafeExcelCellValue(currentWorksheet.Cells[startRow + 2, 2].Value);
                            string eligiblityTitle = GetSafeExcelCellValue(currentWorksheet.Cells[startRow + 4, 1].Value);
                            string eligiblity = GetSafeExcelCellValue(currentWorksheet.Cells[startRow + 4, 2].Value);
                            string structureTitle = GetSafeExcelCellValue(currentWorksheet.Cells[startRow + 6, 1].Value);
                            string structure = GetSafeExcelCellValue(currentWorksheet.Cells[startRow + 6, 2].Value);
                            string awardTitle = GetSafeExcelCellValue(currentWorksheet.Cells[startRow + 8, 1].Value);
                            string award = GetSafeExcelCellValue(currentWorksheet.Cells[startRow + 8, 2].Value);
                            string criteriaTitle = GetSafeExcelCellValue(currentWorksheet.Cells[startRow + 10, 1].Value);
                            string criteria = GetSafeExcelCellValue(currentWorksheet.Cells[startRow + 10, 2].Value);
                            string noteTitle = GetSafeExcelCellValue(currentWorksheet.Cells[startRow + 12, 1].Value);
                            string note = GetSafeExcelCellValue(currentWorksheet.Cells[startRow + 12, 2].Value);

                            CEArticle articleItem = new CEArticle(title);

                            articleItem.AddParagraph(newParagraph(string.Empty, description));
                            articleItem.AddParagraph(newParagraph(eligiblityTitle, eligiblity));
                            articleItem.AddParagraph(newParagraph(structureTitle, structure));
                            articleItem.AddParagraph(newParagraph(awardTitle, award));
                            articleItem.AddParagraph(newParagraph(criteriaTitle, criteria));
                            articleItem.AddParagraph(newParagraph(noteTitle, note));

                            articleContent.Language = language;
                            articleContent.Translation = "yes";
                            articleContent.AddArticle(articleItem);


                            // all rule page has the same related links
                            if (language == "us")
                                ArticleContentRetriever.AddRelatedLinks(articleContent, "/content/talent/guidelines/relatedlinks.xml");
                            else
                                ArticleContentRetriever.AddRelatedLinks(articleContent, "/content/talent/guidelines/cn/relatedlinks.xml");

                            return articleContent;
                        }
                    }
                }

            }
            catch // when get here, we show no content
            {
                articleContent = new ArticleContent();
            }
            return articleContent;
        }

        private static Paragraph newParagraph(string title, string text)
        {
            Paragraph paragraphItem = new Paragraph("false");
            if (!string.IsNullOrEmpty(text))
            {
                text = text.Replace("\n", "</br>");
                if (string.IsNullOrEmpty(title))
                    paragraphItem.Text = text;
                else
                    paragraphItem.Text = string.Format(RuleParagraphTemplate, title, text);
            }
            return paragraphItem;
        }

        private static string GetSafeExcelCellValue(object value)
        {
            return value == null ? string.Empty : value.ToString();
        }
        #endregion
    }
}