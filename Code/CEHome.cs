using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Web;
using CE.Data;

namespace CE.Content
{
    #region Home Page Content classes
    /// <summary>
    /// Home page content classes
    /// </summary>
    public class HomeContent
    {
        private List<Headline> _headlines = null;
        private List<HomeAnnouncement> _announcements = null;
        private List<HomeTile> _tiles = null;

        public HomeContent()
        {
            _headlines = new List<Headline>();
            _announcements = new List<HomeAnnouncement>();
            _tiles = new List<HomeTile>();
        }
        public string AnnouncementHeader { get; set; }

        public List<Headline> Headlines {
            get { return _headlines; }
            set { _headlines = value; }
        }
        public List<HomeAnnouncement> Announcements
        {
            get { return _announcements; }
            set { _announcements = value; }
        }
        public List<HomeTile> Tiles {
            get { return _tiles; }
            set { _tiles = value; }
        }

        public void AddHeadline(string title, string imageUrl, string position, string expire)
        {
            if (_headlines == null) _headlines = new List<Headline>();
            _headlines.Add(new Headline(title, imageUrl, position, expire));
        }
        public void AddTile(string title, string imageUrl, string text)
        {
            if (_tiles == null) _tiles = new List<HomeTile>();
            _tiles.Add(new HomeTile(title, imageUrl, text));
        }
        public void AddAnnouncement(string title, string iconUrl, string text, string linkUrl, string expire)
        {
            if (_announcements == null) _announcements = new List<HomeAnnouncement>();
            _announcements.Add(new HomeAnnouncement(title, iconUrl, text, linkUrl, expire));
        }
    }
    public class Headline
    {
        public Headline(string title, string imageUrl, string position, string expiration)
        {
            Title = title;
            ImageUrl = imageUrl;
            Position = position;
            if (string.IsNullOrEmpty(expiration)) expiration = "12/31/2050"; // meant to not expired
            ExpiredDate = DateTime.Parse(expiration, System.Globalization.CultureInfo.InvariantCulture);
            Display = string.IsNullOrEmpty(Title) ? "hide" : string.Empty;
        }

        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Position { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string Display { get; set; }
    }
    public class HomeTile
    {
        public HomeTile(string title, string imageUrl, string text)
        {
            Title = title;
            ImageUrl = imageUrl;
            Text = text;
        }

        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Text { get; set; }
    }
    public class HomeAnnouncement
    {
        private const string LEARN_MORE_TEMPLATE = " <a href=\"{0}\" target=\"{1}\">Learn more...</a>";

        public HomeAnnouncement(string title, string iconUrl, string summary, string linkUrl, string expiration)
        {
            Title = title;
            IconUrl = iconUrl;
            LinkUrl = linkUrl;
            string target = "_self";
            if (!string.IsNullOrEmpty(linkUrl))
            {
                if (linkUrl.ToLower().StartsWith("http://")) target = "_blank";
                Summary = summary + string.Format(LEARN_MORE_TEMPLATE, linkUrl, target);
            }
            else
            {
                Summary = summary;
            }
            if (string.IsNullOrEmpty(expiration)) expiration = "12/31/2050"; // meant to not expired
            ExpiredDate = DateTime.Parse(expiration, System.Globalization.CultureInfo.InvariantCulture);
            //ExpiredDate = DateTime.ParseExact(expiration, "mm/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        }
        public string Title { get; set; }
        public string IconUrl { get; set; }
        public string LinkUrl { get; set; }
        public string Summary { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
    #endregion

    #region Page Content Retrieval
    /// <summary>
    /// Page Content Retrieval from xml Content Source
    /// </summary>
    public class HomeContentRetriever　
    {
        #region Home Page Content Rertrieval
        public static HomeContent GetHomePageContent(string pageContentXml)
        {
            HomeContent homeContent = new HomeContent();
            try
            {
                string physicalPath = HttpContext.Current.Request.PhysicalApplicationPath + pageContentXml;
                XDocument xdoc = XDocument.Load(physicalPath);
                if (xdoc != null)
                {
                    XElement home = xdoc.Element("ce").Element("home");
                    ReadHeadlines(homeContent, home);
                    ReadAnnouncements(homeContent, home);
                    ReadTiles(homeContent, home);
                }
            }
            catch // should redirect to the generic error page.
            {

            }
            return homeContent;
        }
        private static void ReadHeadlines(HomeContent homeContent, XElement home)
        {
            XElement headline = home.Element("headline").Element("images");
            if (headline != null)
            {
                IEnumerable<XElement> images = headline.Elements("image");
                if (images != null)
                {
                    foreach (XElement image in images)
                    {
                        string title = CEHelper.GetSafeAttribute(image, "title");
                        string imageUrl = CEHelper.GetSafeAttribute(image, "url");
                        string position = CEHelper.GetSafeAttribute(image, "position");
                        string expire = CEHelper.GetSafeAttribute(image, "expire");
                        if (!CEHelper.IsExpired(expire))
                        {
                            homeContent.AddHeadline(title, imageUrl, position, expire);
                        }
                    }
                }
            }
        }
        private static void ReadAnnouncements(HomeContent homeContent, XElement home)
        {
            XElement announceemnts = home.Element("announcements");
            if (announceemnts != null)
            {
                homeContent.AnnouncementHeader = CEHelper.GetSafeAttribute(announceemnts, "header");
                IEnumerable<XElement> announcementItems = announceemnts.Elements("announcement");
                if (announcementItems != null)
                {
                    foreach (XElement announcement in announcementItems)
                    {
                        string title = CEHelper.GetSafeAttribute(announcement, "title");
                        string iconUrl = CEHelper.GetSafeAttribute(announcement, "iconUrl");
                        string expire = CEHelper.GetSafeAttribute(announcement, "expire");
                        string linkUrl = CEHelper.GetSafeAttribute(announcement, "linkUrl");
                        string summary = CEHelper.GetSafeElementText(announcement.Element("summary"));
                        if (!CEHelper.IsExpired(expire)) homeContent.AddAnnouncement(title, iconUrl, summary, linkUrl, expire);
                    }
                }
            }
        }
        private static void ReadTiles(HomeContent homeContent, XElement home)
        {
            XElement tiles = home.Element("tiles");
            if (tiles != null)
            {
                IEnumerable<XElement>  tileList = tiles.Elements("tile");
                if (tileList != null)
                {
                    foreach (XElement tile in tileList)
                    {
                        string title = CEHelper.GetSafeAttribute(tile, "title");
                        string imageUrl = CEHelper.GetSafeAttribute(tile, "imageUrl");
                        XElement textElem = tile.Element("text");
                        string text = CEHelper.GetSafeElementText(textElem);
                        homeContent.AddTile(title, imageUrl, text);
                    }
                }
            }
        }
        #endregion
    }
    #endregion
}
