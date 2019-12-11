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
    #region Article Page Content classes
    /// <summary>
    /// Tour Page Content Classes
    /// </summary>
    public class ArticleContent
    {
        private List<CEArticle> _articles = null;
        private SideBar _sideBar = null;
        private string _language = string.Empty;
        private string _translation = string.Empty;

        public ArticleContent()
        {
            _articles = new List<CEArticle>();
            _sideBar = new SideBar();
        }

        // public properties
        public List<CEArticle> Articles
        {
            get { return _articles; }
        }
        public SideBar SideBar {
            get { return _sideBar; }
        }
        public string Language
        {
            get { return _language; }
            set { _language = value; }
        }
        public string Translation
        {
            get { return _translation; }
            set { _translation = value; }
        }

        public void AddArticle(CEArticle article)
        {
            if (_articles == null) _articles = new List<CEArticle>();
            _articles.Add(article);
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
    public class CEArticle
    {
        List<Paragraph> _paragraphs = null;
        public CEArticle(string title)
        {
            Title = title;
            _paragraphs = new List<Paragraph>();
        }

        // public properties
        public string Title { get; set; }
        public List<Paragraph> Paragraphs
        {
            get { return _paragraphs; }
        }

        public void AddParagraph(Paragraph paragraph)
        {
            if (_paragraphs == null) _paragraphs = new List<Paragraph>();
            _paragraphs.Add(paragraph);
        }

    }
    public class Paragraph
    {
        public const string TEXT_BEG_TEMPLATE = "<ul>";
        public const string TEXT_ITEM_TEMPLATE = "<li class=\"ce-article-item\" >{0}</li>";
        public const string TEXT_END_TEMPLATE = "</ul>";
        private List<string> _texts = null;
        private string _textBlock = string.Empty;
        private string pictureUrl;

        public Paragraph(string indent)
        {
            Indent = (indent.ToLower() == "true") ? true : false;
            _texts = new List<string>();
        }

        // public properties
        public bool Indent { get; set; }
        public string Class
        {
            get { return Indent == true ? "ce-indent" : string.Empty; }
        }
        public string Text { get; set; }
        public string PictureUrl {
            get
            {
                return pictureUrl;
            }
            set
            {
                if (!value.StartsWith("http://") && !value.StartsWith("https://")) pictureUrl = CEHelper.GetSiteRootUrl() + value;
                else pictureUrl = value;
            }
        }
        public string PicturePosition { get; set; }
        public string PictureCaption { get; set; }
        public string TextBlock
        {
            get { return string.IsNullOrEmpty(_textBlock) ? Text : _textBlock; }
        }
        public void AddTextBlock(string text)
        {
            if (!_textBlock.StartsWith(TEXT_BEG_TEMPLATE))
                _textBlock = TEXT_BEG_TEMPLATE;
            else
                _textBlock = _textBlock.Substring(0, _textBlock.Length - TEXT_END_TEMPLATE.Length);

            _textBlock += string.Format(TEXT_ITEM_TEMPLATE, text) + TEXT_END_TEMPLATE;
        }
        public void AddPicture(string url, string position, string caption)
        {
            PictureUrl = url;
            PicturePosition = position;
            PictureCaption = caption;
        }
    }
    public class SideBar
    {
        private List<BarTile> _barTiles = null;
        private List<Testimony> _testimonies = null;
        private List<RelatedLinks> _relatedLinksList = null;
        private List<VideoClip> _videoClips = null;

        public SideBar()
        {
            _barTiles = new List<BarTile>();
            _testimonies = new List<Testimony>();
            _relatedLinksList = new List<RelatedLinks>();
            RelatedLinksHeader = "Related Links"; // this is the default if not set
            TestimonyHeader = "Testimonies"; // this is the default if not set
        }

        public string VidepClipsHeader { get; set; }
        public string RelatedLinksHeader { get; set; }
        public string TestimonyHeader { get; set; }
        public string ImageTileheader { get; set; }
        public List<BarTile> BarTiles
        {
            get { return _barTiles; }
        }
        public List<Testimony> Testimonies
        {
            get { return _testimonies; }
        }
        public List<RelatedLinks> RelatedLinksList
        {
            get { return _relatedLinksList; }
        }
        public List<VideoClip> VideoClips
        {
            get { return _videoClips; }
        }

        public void AddBarTile(BarTile tile)
        {
            if (_barTiles == null) _barTiles = new List<BarTile>();
            _barTiles.Add(tile);
        }
        public void AddTestimony(Testimony testimony)
        {
            if (_testimonies == null) _testimonies = new List<Testimony>();
            _testimonies.Add(testimony);
        }
        public void AddRelatedLinks(RelatedLinks relatedLinks)
        {
            if (_relatedLinksList == null) _relatedLinksList = new List<RelatedLinks>();
            _relatedLinksList.Add(relatedLinks);
        }
        public void AddVideoClip(VideoClip videoClip)
        {
            if (_videoClips == null) _videoClips = new List<VideoClip>();
            _videoClips.Add(videoClip);
        }
    }
    public class BarTile
    {
        private string linkUrl;
        private string imageUrl;

        public BarTile(string caption, string imageUrl, string position, string linkUrl)
        {
            Caption = caption;
            LinkUrl = linkUrl;
            ImageUrl = imageUrl;

            if (string.Compare(position, "bottom", true) == 0)
            {
                CaptionTop = "hide";
                CaptionBottom = "show";
            }
            else
            {
                CaptionTop = "ce-caption-top-show";
                CaptionBottom = "hide";
            }
        }
        public string Caption{ get; set; }
        public string ImageUrl
        {
            get
            {
                return imageUrl;
            }
            set
            {
                if (!value.StartsWith("http://") && !value.StartsWith("https://")) imageUrl = CEHelper.GetSiteRootUrl() + value;
                else imageUrl = value;
            }
        }
        public string CaptionTop { get; set; }
        public string CaptionBottom { get; set; }
        public string LinkUrl
        {
            get
            {
                return linkUrl;
            }
            set
            {
                if (!value.StartsWith("http://") && !value.StartsWith("https://")) linkUrl = CEHelper.GetSiteRootUrl() + value;
                else linkUrl = value;
            }
        }
    }
    public class Testimony
    {
        private string iconUrl;
        private string linkUrl;

        public Testimony(string caption, string position, string iconUrl, string text, string linkUrl)
        {
            Caption = caption;
            IconUrl = iconUrl;
            Text = text;
            LinkUrl = linkUrl;

            if (string.Compare(position, "bottom", true) == 0)
            {
                CaptionTop = "hide";
                CaptionBottom = "show";
            }
            else
            {
                CaptionTop = "ce-caption-top-show";
                CaptionBottom = "hide";
            }
        }
        public string Caption { get; set; }
        public string CaptionTop { get; set; }
        public string CaptionBottom { get; set; }
        public string IconUrl
        {
            get
            {
                return iconUrl;
            }
            set
            {
                if (!value.StartsWith("http://") && !value.StartsWith("https://")) iconUrl = CEHelper.GetSiteRootUrl() + value;
                else iconUrl = value;
            }
        }
        public string Text { get; set; }
        public string LinkUrl
        {
            get
            {
                return linkUrl;
            }
            set
            {
                if (!value.StartsWith("http://") && !value.StartsWith("https://")) linkUrl = CEHelper.GetSiteRootUrl() + value;
                else linkUrl = value;
            }
        }
    }
    public class VideoClip
    {
        private string clipUrl;

        public VideoClip(string caption, string clipUrl)
        {
            Caption = caption;
            ClipUrl = clipUrl;
        }
        public string Caption { get; set; }
        public string ClipUrl
        {
            get
            {
                return clipUrl;
            }
            set
            {
                if (!value.StartsWith("http://") && !value.StartsWith("https://")) clipUrl = CEHelper.GetSiteRootUrl() + value;
                else clipUrl = value;
            }
        }
    }

    [Serializable]
    public class RelatedLinks
    {
        private List<RelatedLink> _relatedLinks = null;
        public RelatedLinks(string header)
        {
            Header = header;
            _relatedLinks = new List<RelatedLink>();
        }
        public string Header { get; set; }
        public List<RelatedLink> RealtedLinks
        {
            get { return _relatedLinks; }
        }

        public void AddRelatedLink(RelatedLink relatedLink)
        {
            _relatedLinks.Add(relatedLink);
        }
    }

    [Serializable]
    public class RelatedLink
    {
        private string iconUrl;
        private string linkUrl;

        public RelatedLink(string iconUrl, string title, string linkUrl, string target)
        {
            IconUrl = iconUrl;
            Title = title;
            LinkUrl = linkUrl;
            Target = target;
        }
        public string IconUrl
        {
            get
            {
                return iconUrl;
            }
            set
            {
                if (!value.StartsWith("http://") && !value.StartsWith("https://")) iconUrl = CEHelper.GetSiteRootUrl() + value;
                else iconUrl = value;
            }
        }
        public string Title { get; set; }
        public string LinkUrl
        {
            get
            {
                return linkUrl;
            }
            set
            {
                if (!value.StartsWith("http://") && !value.StartsWith("https://")) linkUrl = CEHelper.GetSiteRootUrl() + value;
                else linkUrl = value;
            }
        }
        public string Target { get; set; }
    }
    #endregion

    #region Page Content Retrieval
    /// <summary>
    /// Page Content Retrieval from xml Content Source
    /// </summary>
    public class ArticleContentRetriever
    {
        #region Article Page Content retrieval
        public static ArticleContent GeArticlePageContent(string pageContentXml, string pageTag)
        {
            ArticleContent articleContent = new ArticleContent();
            try
            {
                if (pageContentXml.StartsWith("/")) pageContentXml = pageContentXml.Substring(1);
                string physicalPath = HttpContext.Current.Request.PhysicalApplicationPath + pageContentXml.Replace('/', '\\');

                XDocument xdoc = XDocument.Load(physicalPath);
                if (xdoc != null)
                {
                    XElement page = xdoc.Element("ce").Element(pageTag);
                    articleContent.Language = CEHelper.GetSafeAttribute(page, "language", "en");
                    articleContent.Translation = CEHelper.GetSafeAttribute(page, "translation", "no");
                    ReadArticles(articleContent, page);
                    ReadSideBar(articleContent, page);
                }
            }
            catch // when get here, we show no content
            {
                articleContent = new ArticleContent();
            }
            return articleContent;
        }
        public static void ReadArticles(ArticleContent articleContent, XElement page)
        {
            if (page == null) return;
            XElement articles = page.Element("articles");
            IEnumerable<XElement> articleItems = articles.Elements("article");
            foreach (XElement article in articleItems)
            {
                CEArticle articleItem = new CEArticle(CEHelper.GetSafeAttribute(article, "title"));
                foreach (XElement paragraph in article.Elements("paragraph"))
                {
                    string indent = CEHelper.GetSafeAttribute(paragraph, "indent");
                    Paragraph paragraphItem = new Paragraph(indent);
                    XElement textElem = paragraph.Element("text");
                    if (textElem != null)
                    {
                        paragraphItem.Text = CEHelper.GetSafeElementText(textElem);
                    }
                    else
                    {
                        foreach (XElement item in paragraph.Elements("item"))
                        {
                            paragraphItem.AddTextBlock(CEHelper.GetSafeElementText(item));
                        }
                    }
                    XElement pictureElem = paragraph.Element("picture");
                    if (pictureElem != null)
                    {
                        string url = CEHelper.GetSafeAttribute(pictureElem, "url");
                        string position = CEHelper.GetSafeAttribute(pictureElem, "position", "left");
                        string caption = CEHelper.GetSafeAttribute(pictureElem, "caption");
                        paragraphItem.AddPicture(url, position, caption);
                    }

                    articleItem.AddParagraph(paragraphItem);
                }
                articleContent.AddArticle(articleItem);
            }
        }
        private static void ReadSideBar(ArticleContent articleContent, XElement page)
        {
            XElement sideBar = page.Element("sideBar");
            if (sideBar == null) return;

            string includeLinks = CEHelper.GetSafeAttribute(sideBar, "includeLinks");
            string includeTiles = CEHelper.GetSafeAttribute(sideBar, "includeTiles");
            string includeVideos = CEHelper.GetSafeAttribute(sideBar, "includeVideos");
            string includeTestimonies = CEHelper.GetSafeAttribute(sideBar, "includeTestimonies");

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
                        articleContent.AddBarTile(new BarTile(caption, imageUrl, position, linkUrl));
                    }
                }
            }
            if (includeTiles == "true")
            {
                // implement when needed
            }

            AddRelatedLinks(articleContent, sideBar);
            if (includeLinks == "true")
            {
                XElement includedLinks = sideBar.Element("includeLinks");
                if (includedLinks != null)
                {
                    string path = CEHelper.GetSafeAttribute(includedLinks, "path");
                    if (!string.IsNullOrEmpty(path)) AddRelatedLinks(articleContent, path);
                }
            }

            XElement videoClips = sideBar.Element("videoClips");
            if (videoClips != null)
            {
                string header = CEHelper.GetSafeAttribute(videoClips, "header");
                if (string.IsNullOrEmpty(header)) header = "Video Clips";
                articleContent.SideBar.VidepClipsHeader = header;
                IEnumerable<XElement> clips = videoClips.Elements("videoClip");
                foreach (XElement clip in clips)
                {
                    string title = CEHelper.GetSafeAttribute(clip, "title", string.Empty);
                    string clipUrl = CEHelper.GetSafeAttribute(clip, "clipUrl");
                    articleContent.AddVideoClip(new VideoClip(title, clipUrl));
                }
            }
            if (includeVideos == "true")
            {
                // implement when needed
            }

            XElement testimories = sideBar.Element("testimonies");
            if (testimories != null)
            {
                string header = CEHelper.GetSafeAttribute(testimories, "header");
                if (!string.IsNullOrEmpty(header)) articleContent.SideBar.TestimonyHeader = header;
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
                        articleContent.AddTestimony(new Testimony(caption, position, iconUrl, text, linkUrl));
                    }
                }
            }
            if (includeTestimonies == "true")
            {
                // implement when needed
            }
        }

        public static void AddRelatedLinks(ArticleContent articleContent, string path)
        {
            if (path.StartsWith("/")) path = path.Substring(1);
            string physicalPath = HttpContext.Current.Request.PhysicalApplicationPath + path.Replace('/', '\\');
            XDocument xdoc = XDocument.Load(physicalPath);
            if (xdoc != null)
            {
                XElement include = xdoc.Element("ce").Element("include");
                if (include != null) AddRelatedLinks(articleContent, include);
            }
        }

        private static void AddRelatedLinks(ArticleContent articleContent, XElement sideBar)
        {
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
                    articleContent.AddRelatedLinks(relatedLinks);
                }
            }
        }
        #endregion
    }

    #endregion
}
