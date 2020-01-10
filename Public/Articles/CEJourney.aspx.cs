using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CE.Data;
using CE.Content;

namespace CE.Pages
{
    public partial class JourneyPage : System.Web.UI.Page
    {
        private const int SWIPEABLE_VIEW_SIZE = 12;
        private const int MAX_ALBUM_SIZE = 10;
        private const string PICTURE_HTML_CDOE_TEMPLATE = "<img class=\"ce-paragraph-picture-{1}\" src=\"{0}\" align=\"{1}\" />";

        // page bindable properties
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string author = Request.QueryString["author"];
                string theme = Request.QueryString["theme"];
                string path = Request.QueryString["path"];
                string year = Request.QueryString["year"];
                if (string.IsNullOrEmpty(author))
                {
                    Response.Redirect(CEHelper.GetSiteRootUrl() + CEConstants.CE_PROBLEM_PAGE);
                }
                else
                {
                    if (string.IsNullOrEmpty(theme)) theme = CEConstants.CE_TOUR_THEME;
                    if (string.IsNullOrEmpty(year)) year = CEHelper.GetProgramYear();
                    if (string.IsNullOrEmpty(path))
                    {
                        string tourJourneyFolder = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, CEConstants.CE_TOUR_JOURNEY_FOLDER);
                        string[] journeyFolderList = Directory.GetDirectories(tourJourneyFolder, "*.*", SearchOption.TopDirectoryOnly);
                        path = CEConstants.CE_TOUR_JOURNEY_PATH + "/" + year;
                    }

                    path = CEHelper.EnsureTrailingPathChar(path);
                    path += author + "/"; // the journey folder is organize per author
                    PageTheme.Text = "<link rel=\"stylesheet\" type=\"text/css\" href=\"" + CEHelper.GetSiteRootUrl() + "/CSS/Themes/" + theme + "/cepage.css\" media=\"all\" />";

                    string contentFile = author + ".xml"; // each author has a journey file
                    string contentUrl = CEHelper.GetContentUrl(path, contentFile);
                    JourneyContent pageContent = JourneyContentRetriever.GetJourneyContent(contentUrl);

                    // set bindable properties
                    List<CETitleInfo> titles = new List<CETitleInfo>();
                    titles.Add(pageContent.TitleInfo);
                    TitleInfo.DataSource = titles;
                    TitleInfo.DataBind();

                    // hook up bio tab content
                    List<CEBio> bios = new List<CEBio>();
                    bios.Add(pageContent.Bio);
                    BioInfo.DataSource = bios;
                    BioInfo.DataBind();

                    // hook up journey album content
                    AlbumSummary.Text = pageContent.Album.Summary;
                    List<AlbumSwipeableView> swipeableViews = MakeSwipeableViews(pageContent.Album);
                    AlbumViews.DataSource = swipeableViews;
                    AlbumViews.DataBind();
                    AlbumPages.DataSource = swipeableViews;
                    AlbumPages.DataBind();

                    // hook up teaching plan content
                    if (pageContent.TeachingPlan.Articles.Count > 0)
                    {
                        TeachingPlan.DataSource = pageContent.TeachingPlan.Articles;
                        TeachingPlan.DataBind();
                    }
                    //else
                    //{
                    //    TeachingPlanTab.Visible = false;
                    //}

                    // hook up journey diary content
                    if (pageContent.Diary.Articles.Count > 0)
                    {
                        PageArticles.DataSource = pageContent.Diary.Articles;
                        PageArticles.DataBind();
                    }
                    else
                    {
                        DiaryTab.Visible = false;
                    }

                    // hook up video clips content
                    if (pageContent.VideoClips.Clips.Count > 0)
                    {
                        VideoClips.DataSource = pageContent.VideoClips.Clips;
                        VideoClips.DataBind();
                    }
                    else
                    {
                        VideoClipTab.Visible = false;
                    }
                }
            }
        }

        private List<AlbumSwipeableView> MakeSwipeableViews(CEAlbum album)
        {
            // break up albums images into viewable chunks
            List<AlbumSwipeableView> swipeableViews = new List<AlbumSwipeableView>();
            AlbumSwipeableView view = new AlbumSwipeableView(SWIPEABLE_VIEW_SIZE);
            bool added = false;
            foreach (CEImage img in album.Images)
            {
                added = false;
                if (view.AddImage(img) == false)
                {
                    swipeableViews.Add(view);
                    added = true;
                    if (swipeableViews.Count == MAX_ALBUM_SIZE) break;
                    view = new AlbumSwipeableView(SWIPEABLE_VIEW_SIZE);
                }
            }
            if (!added) // this takes care of last view that does not have full images
            {
                swipeableViews.Add(view);
            }

            return swipeableViews;
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