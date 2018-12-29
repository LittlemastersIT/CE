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
    #region CE Album Class
    public class AlbumContent
    {
        private List<CEAlbum> _cealbums = null;

        public AlbumContent() : this(string.Empty, "/Album")
        {
        }
        public AlbumContent(string title, string rootUrl)
        {
            DefaultTitle = title;
            RootUrl = rootUrl;
            _cealbums = new List<CEAlbum>();
        }

        public string DefaultTitle { get; set; }
        public string RootUrl { get; set; }

        public List<CEAlbum> CEAlbums
        {
            get { return _cealbums; }
        }

        public void AddAlbum(CEAlbum album)
        {
            _cealbums.Add(album);
        }
   }

    public class CEAlbum
    {
        private List<CEImage> _imageUrls;

        public CEAlbum(string folder, string tab, string title, string align, string summary, int index, string show)
        {
            Folder = folder;
            Tab = tab;
            Title = title;
            Align = align;
            Summary = summary;
            Index = index.ToString();
            Display = show;
            _imageUrls = new List<CEImage>();
        }

        public string Folder { get; set; }
        public string Tab { get; set; }
        public string Title { get; set; }
        public string Align {get; set; }
        public string Summary { get; set; }
        public string Index { get; set; }
        public string Display { get; set; }
        public List<CEImage> Images
        {
            get { return _imageUrls; }
        }

        public void AddImage(string imageUrl)
        {
            _imageUrls.Add(new CEImage(imageUrl));
        }
    }

    public class CEImage
    {
        public CEImage(string imageUrl)
        {
            ImageUrl = imageUrl;
        }
        public string ImageUrl { get; set; }
    }

    public class AlbumSwipeableView
    {
        private List<CEImage> _viewImages = null;
        public AlbumSwipeableView(int sizePerView)
        {
            SizePerView = sizePerView;
            _viewImages = new List<CEImage>();
        }
        public int SizePerView { get; set; }
        public List<CEImage> Images
        {
            get { return _viewImages; }
        }

        public bool AddImage(CEImage ceImage)
        {
            if (_viewImages.Count < SizePerView) _viewImages.Add(ceImage);
            return _viewImages.Count < SizePerView;
        }
    }
    #endregion

    #region Page Content Retrieval
    /// <summary>
    /// Page Content Retrieval from xml Content Source
    /// </summary>
    public class AlbumContentRetriever
    {
        private const string ALLOW_IMAGE_TYPS = ".jpg,.png,.gif,.bmp,.jpeg";

        #region Album Content Retrieval
        public static AlbumContent GetAlbumContent(string albumContentXml, string pageTag)
        {
            AlbumContent albumContent = new AlbumContent();
            try
            {
                if (albumContentXml.StartsWith("/")) albumContentXml = albumContentXml.Substring(1);
                string physicalPath = HttpContext.Current.Request.PhysicalApplicationPath + albumContentXml;
                XDocument xdoc = XDocument.Load(physicalPath);
                if (xdoc != null)
                {
                    XElement journey = xdoc.Element("ce").Element(pageTag);
                    albumContent.DefaultTitle = CEHelper.GetSafeAttribute(journey, "title");
                    albumContent.RootUrl = CEHelper.GetSafeAttribute(journey, "rootUrl");
                    string albumRootFolder = (HttpContext.Current.Request.PhysicalApplicationPath + albumContent.RootUrl).Replace('/', '\\').Replace("\\\\", "\\") + "\\";
                    string imageRiootUrl = !albumContent.RootUrl.EndsWith("/") ? albumContent.RootUrl + "/" : albumContent.RootUrl;

                    IEnumerable<XElement> albumItems = journey.Elements("album");
                    int index = 0;
                    string display = "show";
                    foreach (XElement album in albumItems)
                    {
                        string tab = CEHelper.GetSafeAttribute(album, "tab");
                        string title = CEHelper.GetSafeAttribute(album, "title");
                        string folder = CEHelper.GetSafeAttribute(album, "folder");
                        string align = CEHelper.GetSafeAttribute(album, "align");
                        string summary = CEHelper.GetSafeElementText(album.Element("summary"));
                        CEAlbum cealbum = new CEAlbum(folder, tab, title, align, summary, index, display);
                        index++;
                        display = "hide";

                        string photoPath = albumRootFolder + folder;
                        string[] files = System.IO.Directory.GetFiles(photoPath, "*.*", System.IO.SearchOption.TopDirectoryOnly);
                        foreach (string file in files)
                        {
                            if (ALLOW_IMAGE_TYPS.Contains(System.IO.Path.GetExtension(file).ToLower()))
                            {
                                string imageUrl = imageRiootUrl + folder + "/" + System.IO.Path.GetFileName(file);
                                cealbum.AddImage(imageUrl);
                            }
                        }

                        albumContent.AddAlbum(cealbum);
                    }
                }
            }
            catch // there will be no content if we get here
            {
                albumContent = new AlbumContent();
            }
            return albumContent;
        }
        #endregion
    }
    #endregion
}
