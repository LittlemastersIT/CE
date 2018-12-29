using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CE.Content;
using CE.Controls;

namespace CE.Pages
{
    public partial class HomePage : System.Web.UI.Page
    {
        private const string HOME_CONTENT_DATA_SOURCE = "Content\\ceHome.xml";
        //private const string HEADLINE_IMAGE_TEMPLAATE = "<img class=\"ce-headline-image\" src=\"{0}\" />";
        //private const string HEADLINE_BANNER_TEMPLAATE = "<div class=\"ce-headline-banner ce-h2 {1}\">{0}</div>";
        private const string PAGING_ITEM_TEMPLATE = "<li class=\"news-paging-item\"></li>";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HomeContent pageContent = HomeContentRetriever.GetHomePageContent(HOME_CONTENT_DATA_SOURCE);
                if (pageContent.Headlines.Count > 0)
                {
                    HeadlineSlides.DataSource = pageContent.Headlines;
                    HeadlineSlides.DataBind();
                }

                // announcements
                if (pageContent.Announcements.Count > 0)
                {
                    NewsAndAnnouncements.Visible = true;
                    NewsHeader.Text = pageContent.AnnouncementHeader;
                    NewsView.DataSource = pageContent.Announcements;
                    NewsView.DataBind();
                    NewsPages.DataSource = pageContent.Announcements;
                    NewsPages.DataBind();
                }
                else
                {
                    NewsAndAnnouncements.Visible = false;
                }

                // tiles
                HomeTiles.DataSource = pageContent.Tiles;
                HomeTiles.DataBind();
            }
        }

        private string GenereatePagingHtml(int count)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < count; i++) sb.Append(PAGING_ITEM_TEMPLATE);
            return sb.ToString();
        }
    }
}