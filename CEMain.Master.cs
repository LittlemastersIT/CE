using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CE.Data;

namespace CE.Pages
{
    public partial class CEMain : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // this is to simulate show/hide admin menu until ASP.Net membership provider is in place.
                string showAdmin = Request.QueryString["showAdmin"];
                if (!string.IsNullOrEmpty(showAdmin) && showAdmin == "1")
                {
                    CEHelper.SetCookie(CEConstants.CE_ADMIN_COOKIE_ID, CEConstants.CE_ADMIN_COOKIE_ID, "ce-admin", 30);
                }
            }
        }
    }
}