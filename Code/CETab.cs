using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Web;
using CE.Data;

namespace CE.Content
{
    #region CE Album Class
    public class TabContent
    {
        private List<TabArticles> _ceaTabArticles = null;

        public TabContent() : this(string.Empty, "left")
        {
        }

        public TabContent(string title, string align)
        {
            TitleInfo = new CETitleInfo(title, align, string.Empty);
            _ceaTabArticles = new List<TabArticles>();
        }

        public CETitleInfo TitleInfo { get; set; }
        public List<TabArticles> Articles { get { return _ceaTabArticles; } }

        public void GetTabContent(string contentXml, string pageTag)
        {
            try
            {
                string physicalPath;
                if (!contentXml.Contains("/"))
                {
                    physicalPath = contentXml; // we assume the folder form is a compelete file path starting from drive letter
                }
                else
                {
                    if (contentXml.StartsWith("/")) contentXml = contentXml.Substring(1);
                    physicalPath = HttpContext.Current.Request.PhysicalApplicationPath + contentXml.Replace('/', '\\');
                }
                XDocument xdoc = XDocument.Load(physicalPath);
                if (xdoc != null)
                {
                    XElement tabs = xdoc.Element("ce").Element(pageTag);
                    TitleInfo.Title = CEHelper.GetSafeAttribute(tabs, "title");
                    TitleInfo.Align = CEHelper.GetSafeAttribute(tabs, "align");
                    TitleInfo.Teaser = CEHelper.GetSafeAttribute(tabs, "summary");

                    // retrieve tab element branch
                    IEnumerable<XElement> tabCollection = tabs.Elements("tab");
                    int index = 0;
                    foreach (XElement tab in tabCollection)
                    {
                        TabArticles tabArticles = new TabArticles();
                        tabArticles.Name = CEHelper.GetSafeAttribute(tab, "name");
                        tabArticles.Index = index.ToString();
                        tabArticles.Display = "hide";
                        ArticleContentRetriever.ReadArticles(tabArticles, tab);
                        _ceaTabArticles.Add(tabArticles);
                        index++;
                    }
                }
            }
            catch // there will be no menu if we get here
            {
            }
        }
    }

    public class TabArticles : ArticleContent
    {
        public TabArticles() : base() {}
        public string Index { get; set; }
        public string Name { get; set; }
        public string Display { get; set; }
    }

    #endregion
}
