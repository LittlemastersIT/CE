using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CE.Data;
using CE.Content;

namespace CE.Pages
{
    public partial class TabPage : System.Web.UI.Page
    {
        private const int MAX_TAB_SIZE = 10;
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
                    Response.Redirect(CEConstants.CE_PROBLEM_PAGE);
                }
                else
                {
                    if (!System.IO.Path.HasExtension(contentFile)) contentFile += ".xml";
                    if (string.IsNullOrEmpty(pageTag)) pageTag = CEConstants.CE_DEFAULT_TAB_TAG; // use default is it is not defined
                    if (string.IsNullOrEmpty(theme)) theme = "blue";
                    if (string.IsNullOrEmpty(path)) path = string.Empty;
                    PageTheme.Text = "<link rel=\"stylesheet\" type=\"text/css\" href=\"/CSS/Themes/" + theme + "/cepage.css\" media=\"all\" />"; ;

                    string contentUrl = CEHelper.GetContentUrl(path, contentFile);
                    TabContent pageContent = new TabContent();
                    pageContent.GetTabContent(contentUrl, pageTag);

                    // hook up the data source to ASP.DataList object
                    TabTitle.Text = pageContent.TitleInfo.Title;
                    TabTeaser.Text = pageContent.TitleInfo.Teaser;

                    // hook up the data source to ASP:Repeater object
                    ArticleTabs.DataSource = pageContent.Articles;
                    ArticleTabs.DataBind();
                    TabCollection.DataSource = pageContent.Articles;
                    TabCollection.DataBind();
                }
            }
        }

        protected void Tab_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // current data item is an ArticleContent type
            TabArticles tabArticles = (TabArticles)e.Item.DataItem;

            // find the child repeater and bind it with the paragraph list
            Repeater pageArticlesRepeater = (Repeater)e.Item.FindControl("PageArticles");
            pageArticlesRepeater.DataSource = tabArticles.Articles;
            pageArticlesRepeater.DataBind();
        }

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

        protected void Photo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // current data item is an Article type
            AlbumSwipeableView albumView = (AlbumSwipeableView)e.Item.DataItem;

            // find the child repeater and bind it with the paragraph list
            Repeater photos = (Repeater)e.Item.FindControl("AlbumPhotos");

            photos.DataSource = albumView.Images;
            photos.DataBind();
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