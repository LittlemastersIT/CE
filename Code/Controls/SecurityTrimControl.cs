using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CE.Data;

namespace CE.Controls
{
    [DefaultProperty("Mode")]
    [ToolboxData("<{0}:SecurityTrimControl runat=server></{0}:SecurityTrimControl>")]
    public class SecurityTrimControl : WebControl
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Mode
        {
            get
            {
                String s = (String)ViewState["Mode"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["Mode"] = value;
            }
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            // make vs2012 not to create a <span> tag
        }
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            // make vs2012 not to create a </span> tag
        }
        protected override void RenderContents(HtmlTextWriter output)
        {
            string adminCookie = CEHelper.GetCookie(CEConstants.CE_ADMIN_COOKIE_ID, CEConstants.CE_ADMIN_COOKIE_NAME);
            bool showAdmin = Mode == "dev" || adminCookie == CEConstants.CE_ADMIN_COOKIE_VALUE;
            if (showAdmin == true) // restart the cookie timer
            {
                CEHelper.SetCookie(CEConstants.CE_ADMIN_COOKIE_ID, CEConstants.CE_ADMIN_COOKIE_NAME, CEConstants.CE_ADMIN_COOKIE_VALUE, 10);
            }
            else if (!HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Contains(CEConstants.CE_PUBLIC_ROOT_URL))
            {
                HttpContext.Current.Response.Redirect(CEHelper.GetSiteRootUrl() + CEConstants.CE_ACCESS_DENIED_PAGE);
            }
        }
    }
}
