using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CE.Content;
using CE.Data;

namespace CE.Controls
{
    [DefaultProperty("SiteMap")]
    [ToolboxData("<{0}:SiteMapControl runat=server></{0}:SiteMapControl>")]
    public class SiteMapControl : WebControl
    {
        private const string SITEMAP_NODE_TEMMPLATE = "<a href=\"{0}{1}\">{2}</a>";
        private const string DEFAULT_HOME_NODE_TEMPLATE = "<a href=\"{0}Public/home.aspx\">CE Home</a>";
        private const string SITE_MAP_XML = "\\Content\\SiteMap.xml";
        private const string SITE_MAP_CONNECTOR = "<span> > </span>";
        private const string TOUR_JOURNEY_PAGE = "cejourney.aspx";

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text
        {
            get
            {
                String s = (String)ViewState["SiteMap"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["SiteMap"] = value;
            }
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            // make vs2012 not to create a <span> tag
        }
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            // make vs2012 not to create a </span> tag
        }
        protected override void RenderContents(HtmlTextWriter output)
        {
            string sitemap = string.Format(DEFAULT_HOME_NODE_TEMPLATE, CEHelper.GetSiteRootUrl());
            try
            {
                string currentUrl = HttpContext.Current.Request.Url.AbsoluteUri.ToLower();
                string path = Page.Request.QueryString["path"];
                string pageUrl = Page.Request.QueryString["page"];
                if (path != null && pageUrl != null)
                {
                    pageUrl = CEHelper.GetPageUrl(path, pageUrl);
                    if (pageUrl != null) currentUrl = pageUrl.ToLower();
                }

                string physicalPath = HttpContext.Current.Request.PhysicalApplicationPath + SITE_MAP_XML;
                XDocument xdoc = XDocument.Load(physicalPath);
                if (xdoc != null)
                {
                    XElement root = xdoc.Element("ce").Element("sitemap").Element("site");
                    IEnumerable<XElement> pages = root.Elements("page");
                    if (pages != null)
                    {
                        bool found = false;
                        foreach (XElement page in pages)
                        {
                            if (PageFound(currentUrl, CEHelper.GetSafeAttribute(page, "url")))
                            {
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            XElement matchedPage = GetSiteMapPage(currentUrl, root);
                            if (matchedPage != null)
                            {
                                string url = CEHelper.GetSafeAttribute(matchedPage, "url");
                                if (url.Contains(TOUR_JOURNEY_PAGE)) url = currentUrl;
                                string displayName = CEHelper.GetSafeAttribute(matchedPage, "display");
                                string parentMap = GetSiteMapString(matchedPage.Parent);
                                sitemap = parentMap + string.Format(SITEMAP_NODE_TEMMPLATE, CEHelper.GetSiteRootUrl(), url, displayName);
                            }
                        }
                    }
                }
            }
            catch // should redirect to the generic error page.
            {

            }
            output.Write(sitemap);
        }

        private XElement GetSiteMapPage(string currentUrl, XElement parent)
        {
            IEnumerable<XElement> sites = parent.Elements("site");
            if (sites != null)
            {
                foreach (XElement site in sites)
                {
                    IEnumerable<XElement> pages = site.Elements("page");
                    if (pages != null)
                    {
                        foreach (XElement page in pages)
                        {
                            string pageUrl = CEHelper.GetSafeAttribute(page, "url");
                            if (PageFound(currentUrl, pageUrl))
                            {
                                return page;
                            }
                            else if (currentUrl.Contains(TOUR_JOURNEY_PAGE) && pageUrl.Contains(TOUR_JOURNEY_PAGE)) // special case
                            {
                                return page;
                            }
                        }
                        XElement matchedPage = GetSiteMapPage(currentUrl, site);
                        if (matchedPage != null) return matchedPage;
                    }
                }
            }
            return null;
        }

        private string GetSiteMapString(XElement site)
        {
            XElement parentSite = site.Parent;
            if (parentSite != null)
            {
                string url = CEHelper.GetSafeAttribute(parentSite, "default");
                string displayName = CEHelper.GetSafeAttribute(parentSite, "display");
                if (!string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(displayName))
                {
                    string pageLink = string.Format(SITEMAP_NODE_TEMMPLATE, CEHelper.GetSiteRootUrl(), url, displayName);
                    return GetSiteMapString(parentSite) + pageLink + SITE_MAP_CONNECTOR;
                }
            }
            return string.Empty;
        }

        private bool PageFound(string url, string pageUrl)
        {
            if (url.EndsWith(pageUrl.ToLower())) return true;

            // url could be the redirect url with parameter in the url; the format should end with eith content=xxxxx or author=xxxxxx or album=xxxxxx
            bool found = false;
            int index = url.LastIndexOf('=');
            if (index > 0)
            {
                string pageName = string.Empty;
                int start = pageUrl.LastIndexOf('/');
                if (start > 0) pageName = pageUrl.Substring(start + 1);
                int end = pageName.LastIndexOf('.');
                if (end > 0) pageName = pageName.Substring(0, end);
                found = (string.Compare(url.Substring(index + 1), pageName, true) == 0);
            }
            return found;
        }
    }
}
