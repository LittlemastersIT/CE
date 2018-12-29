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
    public class JourneyContent
    {
        public JourneyContent() : this(string.Empty, "right", "/Tours/Journey")
        {
            Bio = new CEBio();
            Diary = new CEDiary();
            VideoClips = new CEVideoClips();
            Testimony = new CETestimony();
            TeachingPlan = new CETeachingPlan();
        }
        public JourneyContent(string title, string align, string rootUrl)
        {
            TitleInfo = new CETitleInfo(title, align, string.Empty);
            RootUrl = rootUrl;
        }

        public string RootUrl { get; set; }
        public CETitleInfo TitleInfo { get; set; }
        public CEBio Bio { get; set; }
        public CEAlbum Album { get; set; }
        public CEDiary Diary { get; set; }
        public CETeachingPlan TeachingPlan { get; set; }
        public CETestimony Testimony { get; set; }
        public CEVideoClips VideoClips { get; set; }
    }

    public class CETitleInfo
    {
        public CETitleInfo(string title, string align, string teaser)
        {
            Title = title;
            Align = align;
            Teaser = teaser;
        }
        public string Title { get; set; }
        public string Align { get; set; }
        public string Teaser { get; set; }
    }

    public class CEBio
    {
        public string FolderName { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Photo { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string School { get; set; }
        public string Grade { get; set; }
        public string Subject { get; set; }
        public string Year { get; set; }
        public string About { get; set; }
    }

    public class CEYear
    {
        public CEYear(int year)
        {
            TourYear = year;
        }
        public int TourYear { get; set; }
    }

    public class CETestimony
    {
        public string Title { get; set; }
        public string Quote { get; set; }
    }

    public class CEDiary : ArticleContent
    {
        public CEDiary()
        {
        }
    }

    public class CETeachingPlan: ArticleContent
    {
        public CETeachingPlan()
        {
        }
    }

    public class CEVideoClips
    {
        private List<VideoClip> _videoclips;
        public CEVideoClips()
        {
            _videoclips = new List<VideoClip>();
        }

        public List<VideoClip> Clips
        {
            get { return _videoclips; }
        }

        public void AddVideoClip(string title, string clipUrl)
        {
            if (_videoclips == null) _videoclips = new List<VideoClip>();
            VideoClip vc = new VideoClip(title, clipUrl);
            _videoclips.Add(vc);
        }
    }

    #endregion

    #region Page Content Retrieval
    /// <summary>
    /// Page Content Retrieval from xml Content Source
    /// </summary>
    public class JourneyContentRetriever
    {
        private const string ALLOW_IMAGE_TYPS = ".jpg,.png,.gif,.bmp,.jpeg";

        #region Journey Content Retrieval
        public static JourneyContent GetJourneyContent(string contentXml)
        {
            JourneyContent journeyContent = new JourneyContent();
            try
            {
                string physicalPath;
                if (!contentXml.Contains("/"))
                {
                    physicalPath = contentXml; // we assume the folder form is a compelete file path starting from drive letter
                }
                else
                {
                    if (contentXml.StartsWith("/")) contentXml = contentXml.Substring(1);
                    physicalPath = HttpContext.Current.Request.PhysicalApplicationPath + contentXml.Replace('/', '\\');
                }
                XDocument xdoc = XDocument.Load(physicalPath);
                if (xdoc != null)
                {
                    XElement journey = xdoc.Element("ce").Element("journey");
                    journeyContent.TitleInfo.Title = CEHelper.GetSafeAttribute(journey, "title");
                    journeyContent.TitleInfo.Align = CEHelper.GetSafeAttribute(journey, "align");
                    journeyContent.RootUrl = CEHelper.GetSafeAttribute(journey, "rootUrl");
                    string journeyRootFolder = (HttpContext.Current.Request.PhysicalApplicationPath + journeyContent.RootUrl).Replace('/', '\\').Replace("\\\\", "\\") + "\\";
                    string imageRiootUrl = !journeyContent.RootUrl.EndsWith("/") ? journeyContent.RootUrl + "/" : journeyContent.RootUrl;

                    // retrieve bio
                    XElement bio = journey.Element("bio");
                    CEBio cebio = RetrieveBio(bio);
                    cebio.FolderName = journeyContent.RootUrl.Substring(journeyContent.RootUrl.LastIndexOf('/') + 1);
                    journeyContent.Bio = cebio;
                    journeyContent.TitleInfo.Teaser = MakeTeaser(cebio);

                    // retrieve album
                    XElement album = journey.Element("album");
                    string display = "hide";
                    string tab = CEHelper.GetSafeAttribute(album, "tab");
                    string title = CEHelper.GetSafeAttribute(album, "title");
                    string folder = CEHelper.GetSafeAttribute(album, "folder");
                    string align = CEHelper.GetSafeAttribute(album, "align");
                    string summary = CEHelper.GetSafeElementText(album.Element("summary"));
                    CEAlbum cealbum = new CEAlbum(folder, tab, title, align, summary, 0, display);

                    try
                    {
                        string photoPath = journeyRootFolder + folder;
                        string[] files = System.IO.Directory.GetFiles(photoPath, "*.*", System.IO.SearchOption.TopDirectoryOnly);
                        foreach (string file in files)
                        {
                            if (ALLOW_IMAGE_TYPS.Contains(System.IO.Path.GetExtension(file).ToLower()))
                            {
                                if (file.ToLower().Contains(cebio.FolderName.ToLower() + ".")) continue;
                                string imageUrl = imageRiootUrl + folder + "/" + System.IO.Path.GetFileName(file);
                                cealbum.AddImage(imageUrl);
                            }
                        }
                    }
                    catch // we ignore if there is error opening file
                    {
                    }
                    journeyContent.Album = cealbum;

                    // retreive testimony
                    CETestimony cetestimony = new CETestimony();
                    XElement testimony = journey.Element("testimony");
                    if (testimony != null)
                    {
                        cetestimony.Title = CEHelper.GetSafeAttribute(testimony, "title");
                        cetestimony.Quote = CEHelper.GetSafeElementText(testimony.Element("quote"));
                        journeyContent.Testimony = cetestimony;
                    }

                    // retreive diary
                    CEDiary cediary = new CEDiary();
                    XElement diary = journey.Element("diary");
                    ArticleContentRetriever.ReadArticles(cediary, diary);
                    journeyContent.Diary = cediary;

                    // retrive teaching plan
                    CETeachingPlan ceTeachingPlan = new CETeachingPlan();
                    XElement teachingPlan = journey.Element("teachingPlan");
                    ArticleContentRetriever.ReadArticles(ceTeachingPlan, teachingPlan);
                    journeyContent.TeachingPlan = ceTeachingPlan;

                    // retrieve video clips
                    XElement videoClips = journey.Element("videoClips");
                    if (videoClips != null)
                    {
                        CEVideoClips ceVideoClips = new CEVideoClips();
                        IEnumerable<XElement> clips = videoClips.Elements("videoClip");
                        if (clips != null)
                        {
                            foreach (XElement clip in clips)
                            {
                                string cpation = CEHelper.GetSafeAttribute(clip, "title", string.Empty);
                                string clipUrl = CEHelper.GetSafeAttribute(clip, "clipUrl");
                                ceVideoClips.AddVideoClip(cpation, clipUrl);
                            }
                            journeyContent.VideoClips = ceVideoClips;
                        }
                    }

                }
            }
            catch // there will be no menu if we get here
            {
                journeyContent = null;
            }
            return journeyContent;
        }

        public static CEBio GetBio(string author)
        {
            // assemble the bio path
            string contentXml = "/Content/Tours/Journey/" + author + "/" + author + ".xml";
            string physicalPath = HttpContext.Current.Request.PhysicalApplicationPath + contentXml.Replace('/', '\\');

            // retrieve bio
            XDocument xdoc = XDocument.Load(physicalPath);
            if (xdoc != null)
            {
                return RetrieveBio(xdoc.Element("ce").Element("journey").Element("bio"));
            }
            else
                return new CEBio();
        }

        private static CEBio RetrieveBio(XElement bio)
        {
            CEBio cebio = new CEBio();
            XElement xName = bio.Element("name");
            cebio.Name = CEHelper.GetSafeAttribute(xName, "first") + ' ' + CEHelper.GetSafeAttribute(xName, "last");
            cebio.DisplayName = CEHelper.GetSafeAttribute(xName, "display");
            cebio.About = CEHelper.GetSafeElementText(bio.Element("about"));
            cebio.Email = CEHelper.GetSafeElementText(bio.Element("email"));
            cebio.Phone = CEHelper.GetSafeElementText(bio.Element("phone"));
            cebio.Address = CEHelper.GetSafeElementText(bio.Element("address"));
            cebio.City = CEHelper.GetSafeElementText(bio.Element("city"));
            cebio.State = CEHelper.GetSafeElementText(bio.Element("state"));
            cebio.Subject = CEHelper.GetSafeElementText(bio.Element("subject"));
            cebio.School = CEHelper.GetSafeElementText(bio.Element("school"));
            cebio.Grade = CEHelper.GetSafeElementText(bio.Element("grade"));
            cebio.Year = CEHelper.GetSafeElementText(bio.Element("year"));
            cebio.Photo = CEHelper.GetSafeElementText(bio.Element("photo"));

            return cebio;
        }

        private static string MakeTeaser(CEBio bio)
        {
            string teaserTemplate = "{0}, {1}, {2}, Grade {3}, {4}";
            string teaser = string.Format(teaserTemplate, bio.DisplayName, bio.School, bio.Subject, bio.Grade, bio.Year);
            teaser = teaser.Replace(", ,", ", ").Replace("Grade ,", string.Empty);
            return teaser;
        }
        #endregion
    }
    #endregion
}
