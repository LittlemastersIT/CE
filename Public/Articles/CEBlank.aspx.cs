using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CE.Data;

namespace CE.Pages
{
    public partial class BlankPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string pageTag = Request.QueryString["tag"];
                string theme = Request.QueryString["theme"];
                string path = Request.QueryString["path"];
                string contentFile = Request.QueryString["content"];
                if (string.IsNullOrEmpty(pageTag) || string.IsNullOrEmpty(contentFile))
                {
                    Response.Redirect(CEHelper.GetSiteRootUrl() + CEConstants.CE_PROBLEM_PAGE);
                }
                else
                {
                    if (string.IsNullOrEmpty(theme)) theme = "blue";
                    if (string.IsNullOrEmpty(path)) path = "";
                    PageTheme.Text = "<link rel=\"stylesheet\" type=\"text/css\" href=\"/CSS/Themes/" + theme + "/cepage.css\" media=\"all\" />"; ;
                }
            }
        }
    }
}