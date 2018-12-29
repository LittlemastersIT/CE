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
    #region CE Video Class
    public class VideoContent
    {
        private List<CEVideo> _cevideos = null;
        private SideBar _sideBar = null;

        public VideoContent() : this("CE Video Clips", string.Empty)
        {
        }
        public VideoContent(string header, string description)
        {
            Header = header;
            Description = description;
            _cevideos = new List<CEVideo>();
            _sideBar = new SideBar();
        }

        public string Header { get; set; }
        public string Description { get; set; }
        public SideBar SideBar
        {
            get { return _sideBar; }
        }

        public List<CEVideo> Videos
        {
            get { return _cevideos; }
        }

        public void AddVideo(CEVideo video)
        {
            _cevideos.Add(video);
        }
        public void AddBarTile(BarTile tile)
        {
            if (_sideBar != null) _sideBar.AddBarTile(tile);
        }
        public void AddTestimony(Testimony testimony)
        {
            if (_sideBar != null) _sideBar.AddTestimony(testimony);
        }
        public void AddRelatedLinks(RelatedLinks relatedLinks)
        {
            if (_sideBar != null) _sideBar.AddRelatedLinks(relatedLinks);
        }
        public void AddVideoClip(VideoClip videoClip)
        {
            if (_sideBar != null) _sideBar.AddVideoClip(videoClip);
        }
    }

    public class CEVideo
    {
        private List<VideoClip> _videoClips;
        public CEVideo(string title, string subtitle, string tab, string display, int index)
        {
            Tab = tab;
            Title = title;
            SubTitle = subtitle;
            Display = display;
            Index = index;
            _videoClips = new List<VideoClip>();
        }

        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Display { get; set; }
        public string Tab { get; set; }
        public int Index { get; set; }

        public List<VideoClip> VideoClips
        {
            get { return _videoClips; }
        }

        public void AddVideoClip(string title, string clipUrl)
        {
            if (_videoClips == null) _videoClips = new List<VideoClip>();
            VideoClip vc = new VideoClip(title, clipUrl);
            _videoClips.Add(vc);
        }
    }
    #endregion

    #region Page Content Retrieval
    /// <summary>
    /// Page Content Retrieval from xml Content Source
    /// </summary>
    public class VideoContentRetriever
    {
        #region Video Content Retrieval
        public static VideoContent GetVideoContent(string videoContentXml)
        {
            VideoContent videoContent = new VideoContent();
            try
            {
                if (videoContentXml.StartsWith("/")) videoContentXml = videoContentXml.Substring(1);
                string physicalPath = HttpContext.Current.Request.PhysicalApplicationPath + videoContentXml;
                XDocument xdoc = XDocument.Load(physicalPath);
                if (xdoc != null)
                {
                    XElement videos = xdoc.Element("ce").Element("videos");
                    videoContent.Header = CEHelper.GetSafeAttribute(videos, "header");
                    videoContent.Description = CEHelper.GetSafeAttribute(videos, "description");

                    IEnumerable<XElement> videoItems = videos.Elements("video");
                    int index = 0;
                    string display = "show";
                    foreach (XElement video in videoItems)
                    {
                        string tab = CEHelper.GetSafeAttribute(video, "tab");
                        string title = CEHelper.GetSafeAttribute(video, "title");
                        string subtitle = CEHelper.GetSafeAttribute(video, "subtitle");
                        CEVideo cevideo = new CEVideo(title, subtitle, tab, display, index);
                        index++;
                        display = "hide";

                        IEnumerable<XElement> clipItems = video.Elements("clip");
                        foreach (XElement clip in clipItems)
                        {
                            title = CEHelper.GetSafeAttribute(clip, "title");
                            string clipUrl = CEHelper.GetSafeAttribute(clip, "clipUrl");
                            cevideo.AddVideoClip(title, clipUrl);                               
                        }
                        videoContent.AddVideo(cevideo);
                    }
                    ReadSideBar(videoContent, videos);
                }
            }
            catch // there will be no content if we get here
            {
                videoContent = new VideoContent();
            }
            return videoContent;
        }
        private static void ReadSideBar(VideoContent videoContent, XElement page)
        {
            XElement sideBar = page.Element("sideBar");
            XElement barTiles = sideBar.Element("barTiles");
            if (barTiles != null)
            {
                IEnumerable<XElement> barTileElems = barTiles.Elements("barTile");
                if (barTileElems != null)
                {
                    foreach (XElement barTile in barTileElems)
                    {
                        string caption = CEHelper.GetSafeAttribute(barTile, "caption");
                        string imageUrl = CEHelper.GetSafeAttribute(barTile, "imageUrl");
                        string position = CEHelper.GetSafeAttribute(barTile, "captionPosition");
                        string linkUrl = CEHelper.GetSafeAttribute(barTile, "linkUrl");
                        videoContent.AddBarTile(new BarTile(caption, imageUrl, position, linkUrl));
                    }
                }
            }

            XElement testimories = sideBar.Element("testimonies");
            if (testimories != null)
            {
                string header = CEHelper.GetSafeAttribute(testimories, "header");
                if (!string.IsNullOrEmpty(header)) videoContent.SideBar.TestimonyHeader = header;
                IEnumerable<XElement> testimoryElems = testimories.Elements("testimory");
                if (testimoryElems != null)
                {
                    foreach (XElement testimony in testimoryElems)
                    {
                        string caption = CEHelper.GetSafeAttribute(testimony, "caption", string.Empty);
                        string position = CEHelper.GetSafeAttribute(testimony, "captionPosition", "top");
                        string iconUrl = CEHelper.GetSafeAttribute(testimony, "iconUrl");
                        string text = CEHelper.GetSafeAttribute(testimony, "text");
                        string linkUrl = CEHelper.GetSafeAttribute(testimony, "linkUrl");
                        videoContent.AddTestimony(new Testimony(caption, position, iconUrl, text, linkUrl));
                    }
                }
            }

            IEnumerable<XElement> relatedLinksList = sideBar.Elements("relatedLinks");
            if (relatedLinksList != null)
            {
                foreach (XElement relatedLinksElem in relatedLinksList)
                {
                    string header = CEHelper.GetSafeAttribute(relatedLinksElem, "header");
                    if (string.IsNullOrEmpty(header)) header = "Related Links";

                    RelatedLinks relatedLinks = new RelatedLinks(header);
                    IEnumerable<XElement> relatedLinkElems = relatedLinksElem.Elements("relatedLink");
                    if (relatedLinkElems != null)
                    {
                        foreach (XElement relatedLink in relatedLinkElems)
                        {
                            string iconUrl = CEHelper.GetSafeAttribute(relatedLink, "iconUrl");
                            string title = CEHelper.GetSafeAttribute(relatedLink, "title");
                            string linkUrl = CEHelper.GetSafeAttribute(relatedLink, "linkUrl");
                            string target = CEHelper.GetSafeAttribute(relatedLink, "target", "_self");
                            relatedLinks.AddRelatedLink(new RelatedLink(iconUrl, title, linkUrl, target));
                        }
                    }
                    videoContent.AddRelatedLinks(relatedLinks);
                }
            }

            XElement videoClips = sideBar.Element("videoClips");
            if (videoClips != null)
            {
                string header = CEHelper.GetSafeAttribute(videoClips, "header");
                if (string.IsNullOrEmpty(header)) header = "Video Clips";
                videoContent.SideBar.VidepClipsHeader = header;
                IEnumerable<XElement> clips = videoClips.Elements("videoClip");
                foreach (XElement clip in clips)
                {
                    string title = CEHelper.GetSafeAttribute(clip, "title", string.Empty);
                    string clipUrl = CEHelper.GetSafeAttribute(clip, "clipUrl");
                    videoContent.AddVideoClip(new VideoClip(title, clipUrl));
                }
            }
        }
        #endregion
    }
    #endregion
}
