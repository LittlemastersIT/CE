using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CE.Content;
using CE.Data;

namespace CE.Controls
{
    public partial class AnnouncementControl : UserControl
    {
        private const string PAGING_ITEM_TEMPLATE = "<li class=\"news-paging-item\"></li>";

        public AnnouncementControl()
        {
        }

        public string AnnouncementHeader { get; set; }
        public List<HomeAnnouncement> NewAnnouncements { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string pageTag = Request.QueryString["page"];
                string contentSource = Request.QueryString["content"];
                if (string.IsNullOrEmpty(pageTag) || string.IsNullOrEmpty(contentSource))
                {
                    Response.Redirect(CEConstants.CE_PROBLEM_PAGE);
                }
                else
                {
                    if (contentSource.IndexOf("\\") < 0) contentSource = "Content\\" + contentSource;
                    HomeContent pageContent = HomeContentRetriever.GetHomePageContent(contentSource);
                    NewsHeader.Text = pageContent.AnnouncementHeader;
                    NewsPagingItems.Text = GenereatePagingHtml(pageContent.Announcements.Count);
                    NewsView.DataSource = pageContent.Announcements;
                    NewsView.DataBind();
                }
            }
        }

        public override void RenderControl(HtmlTextWriter writer)
        {
 	         base.RenderControl(writer);
        }

        private string GenereatePagingHtml(int count)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++) sb.Append(PAGING_ITEM_TEMPLATE);
            return sb.ToString();
        }
    }
}
