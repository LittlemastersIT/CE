using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CE.Data;


namespace CE.Admin
{
    public partial class CreatePage : System.Web.UI.Page
    {
        private const string TEMPLATE_BEGIN_BLOCK = "<!DOCTYPE html><html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n\t<title>{0}</title>";
        private const string WEB_META_NO_CACHE_1 = "\t<meta http-equiv=\"Cache-Control\" content=\"no-cache, no-store, private, must-revalidate, max-stale=0, post-check=0, pre-check=0\" />";
        private const string WEB_META_NO_CACHE_2 = "\t<meta http-equiv=\"Pragma\" content=\"no-cache\" />";
        private const string WEB_META_NO_CACHE_3 = "\t<meta http-equiv=\"Expires\" content=\"-1\" />";
        private const string WEB_PAGE_META_TEMPLATE = "\t<meta http-equiv=\"refresh\" content=\"0; url=/public/articles/ce{0}.aspx?theme={1}&path=/{2}/{3}&content={4}\" />";
        private const string WEB_JOURNEY_META_TEMPLATE = "\t<meta http-equiv=\"refresh\" content=\"0; url=/public/articles/ce{0}.aspx?theme={1}&path=/{2}/{3}&author={4}\" />";
        private const string WEB_PAGE_URL_TEMPLATE = "/public/{0}/{1}/{2}";
        private const string TEMPLATE_END_BLOCK = "</head>\r\n<body></body>\r\n</html>";
        private const string SITEMAP_PAGE_TEMPLATE = "<page url=\"{0}\" display=\"{1}\" />\r\n";
       
        private string WebPageUrl = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Form.DefaultButton = this.CreatePageButton.UniqueID;
                CreationMessage.Visible = false;
                AlbumPathRow.Visible = false;
                AlbumNo.Checked = true;
                TopfolderList.SelectedIndex = 0;
            }
        }

        protected void OnCreatePage(object sender, EventArgs e)
        {
            bool ok = false;
            ResultMessage.Text = string.Empty;
            CreationMessage.Visible = true;

            if (ContentName.Text.Trim() == string.Empty)
            {
                ResultMessage.Text = "<span class=\"form-message red\">Content filename is required.</span>";
                return;
            }

            if (BreadcrumbLabel.Text.Trim() == string.Empty)
            {
                ResultMessage.Text = "<span class=\"form-message red\">Breadcrumb text is required.</span>";
                return;
            }

            string template = PageTemplateList.SelectedItem.Value;
            string theme = ThemeList.SelectedItem.Value;
            string topFolder = NormalizeListItemName(TopfolderList.SelectedItem.Value);
            string subFolder = NormalizeListItemName(SubFolderList.SelectedItem.Value);
            string contentName = ContentName.Text;

            string filepath = string.Empty;
            if (!string.IsNullOrEmpty(topFolder) && !string.IsNullOrEmpty(contentName))
            {
                string homeFolder = topFolder;
                if (string.Compare(topFolder, "root", true) == 0) homeFolder = string.Empty;
                string pageContent = GeneratePageContent(template, theme, homeFolder, subFolder, contentName);
                filepath = MakeFilePath(homeFolder, subFolder, contentName);

                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(filepath))) Directory.CreateDirectory(Path.GetDirectoryName(filepath));

                    ok = CEHelper.WaitAndWrite(filepath, pageContent, false, false);
                    if (ok) UpdateSiteMap(topFolder, subFolder, WebPageUrl, BreadcrumbLabel.Text);
                }
                catch
                {
                    ok = false;
                }
            }

            if (ok)
                ResultMessage.Text = "<span class=\"form-message blue\">Page '" + filepath + "' is created.</span>";
            else
                ResultMessage.Text = "<span class=\"form-message red\">There is problem creating the web site page. Please ensure all required fields contain valid data and try again.</span>";
        }

        protected void OnClearForm(object sender, EventArgs e)
        {
            CreationMessage.Visible = false;
            AlbumPathRow.Visible = false;
            AlbumNo.Checked = true;
            PageTemplateList.SelectedIndex = 0;
            ThemeList.SelectedIndex = 0;
            TopfolderList.SelectedIndex = 0;
            SubFolderList.SelectedIndex = 0;
            ContentName.Text = string.Empty;
            ContentTag.Text = string.Empty;
        }

        protected void OnCancel(object sender, EventArgs e)
        {
            Response.Redirect(CEHelper.GetSiteRootUrl() + CEConstants.CE_TALENT_COMPETITION_PAGE);
        }

        protected void OnTemplateChanged(object sender, EventArgs e)
        {
            if (string.Compare(PageTemplateList.SelectedValue, "journey", true) == 0)
                ContentTagRow.Visible = false;
            else
                ContentTagRow.Visible = true;
        }

        protected void OnTopFolderChanged(object sender, EventArgs e)
        {
            EnableSubfolderItems(int.Parse(GetIdInListItemValue(TopfolderList.SelectedValue)));
        }

        protected void OnAlbumYesChecked(object sender, EventArgs e)
        {
            AlbumPathRow.Visible = true;
        }

        protected void OnAlbumNoChecked(object sender, EventArgs e)
        {
            AlbumPathRow.Visible = false;
        }

        private void EnableSubfolderItems(int topFolderId)
        {
            foreach (ListItem item in SubFolderList.Items) item.Enabled = false;
            SubFolderList.Items[0].Enabled = true; // this is 'select one...' item
            foreach (ListItem item in SubFolderList.Items)
            {
                int id = int.Parse(GetIdInListItemValue(item.Value));
                if ((id / 10) == topFolderId) item.Enabled = true;
            }
        }

        private string GeneratePageContent(string template, string theme, string topFolder, string subFolder, string contentName)
        {
            WebPageUrl = string.Format(WEB_PAGE_URL_TEMPLATE, topFolder, subFolder, contentName + ".html").Replace("//", "/").Replace("//", "/");

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine(string.Format(TEMPLATE_BEGIN_BLOCK, "CE Web Page"));
            sb.AppendLine(WEB_META_NO_CACHE_1);
            sb.AppendLine(WEB_META_NO_CACHE_2);
            sb.AppendLine(WEB_META_NO_CACHE_3);
            if (string.Compare(template, "journey", true) == 0)
            {
                sb.AppendLine(string.Format(WEB_JOURNEY_META_TEMPLATE, template, theme, topFolder, subFolder, contentName).Replace("//","/"));
            }
            else
            {
                sb.AppendLine(string.Format(WEB_PAGE_META_TEMPLATE, template, theme, topFolder, subFolder, contentName).Replace("//", "/"));
            }

            sb.AppendLine(TEMPLATE_END_BLOCK);
            return sb.ToString();
        }

        private void UpdateSiteMap(string topFolder,string subfolder, string url, string breadcrumbText)
        {
            string sitemapFile = HttpContext.Current.Request.PhysicalApplicationPath + CEConstants.CE_SITEMAP_FILE;
            XDocument xSiteMap = XDocument.Load(sitemapFile);
            if (xSiteMap != null)
            {
                XElement page = new XElement("page");
                page.SetAttributeValue("url", url);
                page.SetAttributeValue("display", breadcrumbText);

                bool inserted = false;
                topFolder = topFolder.ToLower();
                subfolder = subfolder.ToLower();
                XElement root = xSiteMap.Element("ce").Element("sitemap").Element("site");
                if (CEHelper.GetSafeAttribute(root, "name") == topFolder)
                {
                    root.AddFirst(page);
                    inserted = true;
                }
                else
                {
                    IEnumerable<XElement> subsites =
                        (from subsite in root.Elements("site")
                         where CEHelper.GetSafeAttribute(subsite, "name") == topFolder
                         select subsite);

                    if (subsites.Count<XElement>() > 0)// should only have one matched
                    {
                        XElement sbusite = subsites.First<XElement>();
                        if (string.IsNullOrEmpty(subfolder))
                        {
                            sbusite.AddFirst(page);
                            inserted = true;
                        }
                        else
                        {
                            IEnumerable<XElement> leafsites =
                                (from subsite in sbusite.Elements("site")
                                 where CEHelper.GetSafeAttribute(subsite, "name") == subfolder
                                 select subsite);

                            if (leafsites.Count<XElement>() > 0)
                            {
                                leafsites.First<XElement>().AddFirst(page);
                                inserted = true;
                            }
                        }
                    }
                }

                if (inserted == true) xSiteMap.Save(sitemapFile, SaveOptions.None);
            }
        }

        private string MakeFilePath(string topFolder, string subFolder, string contentName)
        {
            System.Text.StringBuilder contentrelativePath = new System.Text.StringBuilder();
            contentrelativePath.Append(CEConstants.CE_PUBLIC_ROOT_FOLDER);
            contentrelativePath.Append("\\" + topFolder);
            if (!string.IsNullOrEmpty(subFolder)) contentrelativePath.Append("\\" + subFolder);
            contentrelativePath.Append("\\" + contentName + ".html");

            return HttpContext.Current.Request.PhysicalApplicationPath + contentrelativePath.ToString();
        }

        private string NormalizeListItemName(string value)
        {
            string[] tokens = value.Split(new char[] { ',' });
            if (tokens.Length == 2)
            {
                if (tokens[1].ToLower().StartsWith("select one"))
                    return string.Empty;
                else
                    return tokens[1];
            }
            else
                return value;
        }

        private string GetNameInListItemValue(string item)
        {
            string[] tokens = item.Split(new char[] { ',' });
            if (tokens.Length == 2)
                return tokens[1];
            else
                return item;
        }

        private string GetIdInListItemValue(string item)
        {
            string[] tokens = item.Split(new char[] { ',' });
            if (tokens.Length == 2)
                return tokens[0];
            else
                return "0";
        }
    }
}