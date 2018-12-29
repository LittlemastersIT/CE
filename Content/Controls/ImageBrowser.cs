using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CE.Application.Controls
{
    [DefaultProperty("FolderUrl")]
    [ToolboxData("<{0}:ImageBrowser runat=server></{0}:ImageBrowser>")]
    public class ImageBrowser : WebControl
    {
        private readonly string SlideContainerBegin = "<div id=\"slideContainer\"><div id=\"slideGallery\">";
        private readonly string SlideView = "<div class=\"slide-view\"><img src=\"{0}\" alt=\"\"/></div>";
        private readonly string SlideContainerEnd = "</div></div>";
        private readonly string slidePagingBegin = "<div id=\"slidePaging\"><div class=\"slide-paging-view\"><div class=\"slide-paging-list center\">";
        private readonly string slidePagingItem = "<div class=\"slide-paging-item\"></div>";
        private readonly string slidePagingEnd = "</div></div></div>";

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Folder
        {
            get
            {
                String s = (String)ViewState["Folder"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["Folder"] = value;
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string FolderUrl
        {
            get
            {
                String s = (String)ViewState["FolderUrl"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["FolderUrl"] = value;
            }
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            string imageDirectory = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, Folder);
            string[] imageFiles = System.IO.Directory.GetFiles(imageDirectory);

            StringBuilder sb = new StringBuilder();

            sb.Append(SlideContainerBegin);

            foreach (string imageFile in imageFiles)
            {
                sb.Append(string.Format(SlideView, FolderUrl + Path.GetFileName(imageFile)));
            }

            sb.Append(slidePagingBegin);
            for (int i = 0; i < imageFiles.Length; i++)
            {
                sb.Append(slidePagingItem);
            }
            sb.Append(slidePagingEnd);

            sb.AppendLine(SlideContainerEnd);
        }
    }
}
