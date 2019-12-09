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
    public partial class AlbumPage : System.Web.UI.Page
    {
        private const int SWIPEABLE_VIEW_SIZE = 12;
        private const int MAX_ALBUM_SIZE = 10;

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
                    if (string.IsNullOrEmpty(pageTag)) pageTag = CEConstants.CE_DEFAULT_ALBUM_TAG; // use default is it is not defined
                    if (string.IsNullOrEmpty(theme)) theme = "blue";
                    if (string.IsNullOrEmpty(path)) path = string.Empty;
                    PageTheme.Text = "<link rel=\"stylesheet\" type=\"text/css\" href=\"" + CEHelper.GetSiteRootUrl() + "/CSS/Themes/" + theme + "/cepage.css\" media=\"all\" />"; ;

                    string contentUrl = CEHelper.GetContentUrl(path, contentFile);
                    AlbumContent pageContent = AlbumContentRetriever.GetAlbumContent(contentUrl, pageTag);

                    // hook up the data source to ASP:Repeater object
                    AlbumTitles.DataSource = pageContent.CEAlbums;
                    AlbumTitles.DataBind();
                    AlbumTabs.DataSource = pageContent.CEAlbums;
                    AlbumTabs.DataBind();
                    AlbumGallery.DataSource = pageContent.CEAlbums;
                    AlbumGallery.DataBind();
                }
            }
        }

        protected void Album_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // current data item is an Album type
            CEAlbum album = (CEAlbum)e.Item.DataItem;

            // break up albums images into viewable chunks
            List<AlbumSwipeableView> swipeableViews = new List<AlbumSwipeableView>();
            List<int> viewPages = new List<int>();
            AlbumSwipeableView view = new AlbumSwipeableView(SWIPEABLE_VIEW_SIZE);
            bool added = false;
            foreach (CEImage img in album.Images)
            {
                added = false;
                if (view.AddImage(img) == false) 
                {
                    swipeableViews.Add(view);
                    viewPages.Add(1);
                    added = true;
                    if (swipeableViews.Count == MAX_ALBUM_SIZE) break;
                    view = new AlbumSwipeableView(SWIPEABLE_VIEW_SIZE);
                }
            }
            if (!added) // this takes care of last view that does not have full images
            {
                swipeableViews.Add(view);
                viewPages.Add(1);
            }

            // find the child repeater and bind it with the paragraph list
            Repeater albumViewRepeater = (Repeater)e.Item.FindControl("AlbumViews");
            albumViewRepeater.DataSource = swipeableViews;
            albumViewRepeater.DataBind();

            Repeater viewPageRepeater = (Repeater)e.Item.FindControl("AlbumPages");
            viewPageRepeater.DataSource = viewPages;
            viewPageRepeater.DataBind();
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
    }
}