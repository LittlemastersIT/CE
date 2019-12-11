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
    public partial class VideoPage : System.Web.UI.Page
    {
        private const string SIDEBAR_SECTION_HEADER = "<span class=\"sidebar-header\">{0}</span>";

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
                    if (string.IsNullOrEmpty(pageTag)) pageTag = CEConstants.CE_DEFAULT_VIDEO_TAG; // use default is it is not defined
                    if (string.IsNullOrEmpty(theme)) theme = "blue";
                    if (string.IsNullOrEmpty(path)) path = string.Empty;
                    PageTheme.Text = "<link rel=\"stylesheet\" type=\"text/css\" href=\"/CSS/Themes/" + theme + "/cepage.css\" media=\"all\" />"; ;

                    string contentUrl = CEHelper.GetContentUrl(path, contentFile);
                    VideoContent videoContent = VideoContentRetriever.GetVideoContent(contentUrl);

                    // hook up the data source to ASP:Repeater object
                    VideoTitles.DataSource = videoContent.Videos;
                    VideoTitles.DataBind();
                    VideoTabs.DataSource = videoContent.Videos;
                    VideoTabs.DataBind();
                    VideoGallery.DataSource = videoContent.Videos;
                    VideoGallery.DataBind();

                    if (videoContent.SideBar.BarTiles != null && videoContent.SideBar.BarTiles.Count > 0)
                    {
                        SideBarTiles.DataSource = videoContent.SideBar.BarTiles;
                        SideBarTiles.DataBind();
                    }
                    else
                        SideBarTiles.Visible = false;

                    if (videoContent.SideBar.RelatedLinksList != null && videoContent.SideBar.RelatedLinksList.Count > 0)
                    {
                        RelatedLinksList.DataSource = videoContent.SideBar.RelatedLinksList;
                        RelatedLinksList.DataBind();
                    }
                    else
                        RelatedLinksPanel.Visible = false;

                    if (videoContent.SideBar.Testimonies != null && videoContent.SideBar.Testimonies.Count > 0)
                    {
                        TestimoniesHeader.Text = string.Format(SIDEBAR_SECTION_HEADER, videoContent.SideBar.TestimonyHeader);
                        SideBarTestimonies.DataSource = videoContent.SideBar.Testimonies;
                        SideBarTestimonies.DataBind();
                    }
                    else
                        TestimonyPanel.Visible = false;

                    if (videoContent.SideBar.VideoClips != null && videoContent.SideBar.VideoClips.Count > 0)
                    {
                        VideoClipHeader.Text = string.Format(SIDEBAR_SECTION_HEADER, videoContent.SideBar.VidepClipsHeader);
                        SideBarVideoClips.DataSource = videoContent.SideBar.VideoClips;
                        SideBarVideoClips.DataBind();
                    }
                    else
                        VideoClipPanel.Visible = false;
                }
            }
        }

        protected void Video_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // current data item is an Video type
            CEVideo video = (CEVideo)e.Item.DataItem;

            Repeater videoClipList = (Repeater)e.Item.FindControl("VideoClipList");
            if (videoClipList != null)
            {
                videoClipList.DataSource = video.VideoClips;
                videoClipList.DataBind();
            }
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
    }
}