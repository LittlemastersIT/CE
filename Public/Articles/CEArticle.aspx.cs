using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CE.Content;
using CE.Data;

namespace CE.Pages
{
    public partial class ArticlePage : System.Web.UI.Page
    {
        private const string SIDEBAR_SECTION_HEADER = "<span class=\"sidebar-header\">{0}</span>";
        private const string PICTURE_HTML_CDOE_TEMPLATE = "<img class=\"ce-paragraph-picture-{1}\" src=\"{0}\" align=\"{1}\" />";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string pageTag = Request.QueryString["tag"];
                string theme = Request.QueryString["theme"];
                string path = Request.QueryString["path"];
                string contentFile = Request.QueryString["content"];

                if (string.IsNullOrEmpty(contentFile))
                {
                    Response.Redirect(CEHelper.GetSiteRootUrl() + CEConstants.CE_PROBLEM_PAGE);
                }
                else
                {
                    if (!System.IO.Path.HasExtension(contentFile)) contentFile += ".xml";
                    if (string.IsNullOrEmpty(pageTag)) pageTag = CEConstants.CE_DEFAULT_PAGE_TAG; // use default if it is not defined
                    if (string.IsNullOrEmpty(theme)) theme = "blue";
                    if (string.IsNullOrEmpty(path)) 
                        path = string.Empty;
                    else if (path.Contains("{year}"))
                    {
                        path = path.Replace("{year}", CEHelper.GetProgramYear());
                    }

                    PageTheme.Text = "<link rel=\"stylesheet\" type=\"text/css\" href=\"" + CEHelper.GetSiteRootUrl() + "/CSS/Themes/" + theme + "/cepage.css\" media=\"all\" />";

                    string contentUrl = CEHelper.GetContentUrl(path, contentFile);
                    ArticleContent pageContent = ArticleContentRetriever.GeArticlePageContent(contentUrl, pageTag);

                    // we try the 'default' page tag if it is not becasue browser might cache previous html code
                    if (pageContent.Articles.Count == 0 && pageTag != CEConstants.CE_DEFAULT_PAGE_TAG)
                    {
                        pageContent = ArticleContentRetriever.GeArticlePageContent(contentUrl, CEConstants.CE_DEFAULT_PAGE_TAG);
                    }

                    if (string.Compare(pageContent.Translation, CEConstants.CE_YES, true) == 0)
                    {
                        LanguageLink.Text = CEHelper.MakeLanguageUrlLink(pageContent.Language);
                        LanguageLink.Visible = true;
                    }
                    else 
                    {
                        LanguageLink.Visible = false;
                    }

                    PageArticles.DataSource = pageContent.Articles;
                    PageArticles.DataBind();

                    if (pageContent.SideBar.BarTiles != null && pageContent.SideBar.BarTiles.Count > 0)
                    {
                        SideBarTiles.DataSource = pageContent.SideBar.BarTiles;
                        SideBarTiles.DataBind();
                    }
                    else
                        SideBarTiles.Visible = false;

                    if (pageContent.SideBar.RelatedLinksList != null && pageContent.SideBar.RelatedLinksList.Count > 0)
                    {
                        RelatedLinksList.DataSource = pageContent.SideBar.RelatedLinksList;
                        RelatedLinksList.DataBind();
                    }
                    else
                        RelatedLinksPanel.Visible = false;

                    if (pageContent.SideBar.Testimonies != null && pageContent.SideBar.Testimonies.Count > 0)
                    {
                        TestimoniesHeader.Text = string.Format(SIDEBAR_SECTION_HEADER, pageContent.SideBar.TestimonyHeader);
                        SideBarTestimonies.DataSource = pageContent.SideBar.Testimonies;
                        SideBarTestimonies.DataBind();
                    }
                    else
                        TestimonyPanel.Visible = false;

                    if (pageContent.SideBar.VideoClips != null && pageContent.SideBar.VideoClips.Count > 0)
                    {
                        VideoClipHeader.Text = string.Format(SIDEBAR_SECTION_HEADER, pageContent.SideBar.VidepClipsHeader);
                        SideBarVideoClips.DataSource = pageContent.SideBar.VideoClips;
                        SideBarVideoClips.DataBind();
                    }
                    else
                        VideoClipPanel.Visible = false;
                }
            }
        }

        /// <summary>
        /// Bind nexted repeater with data source
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Article_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // current data item is an Article type
            CEArticle article = (CEArticle)e.Item.DataItem;

            // find the child repeater and bind it with the paragraph list
            Repeater paragraphs = (Repeater)e.Item.FindControl("Paragraphs");
            AdjustParagraphPictures(article.Paragraphs);
            paragraphs.DataSource = article.Paragraphs;
            paragraphs.DataBind();
        }

        protected void RelatedLinksList_DataBound(object sender, RepeaterItemEventArgs e)
        {
            // current data item is an RelatedLinks type
            RelatedLinks relatedLinks = (RelatedLinks)e.Item.DataItem;
            if (relatedLinks != null)
            {
                Repeater relatedLinksRepeater = (Repeater)e.Item.FindControl("RelatedLinks");
                if (relatedLinksRepeater != null)
                {
                    relatedLinksRepeater.DataSource = relatedLinks.RealtedLinks;
                    relatedLinksRepeater.DataBind();
                }
            }
        }

        private void AdjustParagraphPictures(List<Paragraph> paragraphs)
        {
            foreach (Paragraph p in paragraphs)
            {
                if (!string.IsNullOrEmpty(p.PictureUrl))
                {
                    p.PictureUrl = string.Format(PICTURE_HTML_CDOE_TEMPLATE, p.PictureUrl, p.PicturePosition);
                }
            }
        }
    }
}