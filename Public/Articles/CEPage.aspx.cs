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
    public partial class GenericPage : System.Web.UI.Page
    {
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
                    if (string.IsNullOrEmpty(pageTag)) pageTag = CEConstants.CE_DEFAULT_PAGE_TAG; // use default is it is not defined
                    if (string.IsNullOrEmpty(theme)) theme = "blue";
                    if (string.IsNullOrEmpty(path)) path = string.Empty;
                    PageTheme.Text = "<link rel=\"stylesheet\" type=\"text/css\" href=\"/CSS/Themes/" + theme + "/cepage.css\" media=\"all\" />"; ;

                    string contentUrl = CEHelper.GetContentUrl(path, contentFile);
                    ArticleContent pageContent = ArticleContentRetriever.GeArticlePageContent(contentUrl, pageTag);

                    // we try the 'default' page tag if it is not becasue browser might cache previous html code
                    if (pageContent.Articles.Count == 0 && pageTag != CEConstants.CE_DEFAULT_PAGE_TAG)
                    {
                        pageContent = ArticleContentRetriever.GeArticlePageContent(contentUrl, CEConstants.CE_DEFAULT_PAGE_TAG);
                    }

                    PageArticles.DataSource = pageContent.Articles;
                    PageArticles.DataBind();
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