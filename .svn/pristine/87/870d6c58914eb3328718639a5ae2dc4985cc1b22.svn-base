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
    public partial class CETourPage : System.Web.UI.Page
    {
        public const string PAGE_CONTENT_DATA_SOURCE = "Content\\Tours\\cetour.xml"; // tour page data source
        private const string SIDEBAR_SECTION_HEADER = "<span class=\"sidebar-header\">{0}</span>";
        private const string PICTURE_HTML_CDOE_TEMPLATE = "<img class=\"ce-paragraph-picture-{1}\" src=\"{0}\" align=\"{1}\" />";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // can also redirect to the generic article display page as this page uses article page style
                //Response.Redirect("/Public/Articles/cearticle.aspx?theme=blue&tag=tour&path=/tours&content=cetour.xml&page=cetour.aspx");

                ArticleContent pageContent = ArticleContentRetriever.GeArticlePageContent(PAGE_CONTENT_DATA_SOURCE, CEConstants.CE_DEFAULT_PAGE_TAG);
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
                    SideBarTestimonies.Visible = false;
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