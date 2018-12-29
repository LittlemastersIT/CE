using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CE.Pages
{
    public partial class PostRequestTestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string value = Request["name"];
                if (!string.IsNullOrEmpty(value))
                {
                    UserName.Text = value;
                }
            }
        }
    }
}