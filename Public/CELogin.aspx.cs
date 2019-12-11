using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CE.Data;

namespace CE.Pages
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                errorMessage.Visible = false;
            }
        }

        protected void OnSignIn(object sender, EventArgs e)
        {
            if (SiteUsers.IsSiteUser(UserNameBox.Text, PasswordBox.Text))
            {
                CEUser user = SiteUsers.FindUser(UserNameBox.Text, PasswordBox.Text);
                CEHelper.SetCookie(CEConstants.CE_ADMIN_COOKIE_ID, CEConstants.CE_ADMIN_COOKIE_NAME, CEConstants.CE_ADMIN_COOKIE_VALUE, 10);
                CEHelper.SetCookie(CEConstants.CE_USER_COOKIE_ID, CEConstants.CE_USER_COOKIE_NAME, user.UserName, 10);
                CEHelper.SetCookie(CEConstants.CE_DISPLAYNAME_COOKIE_ID, CEConstants.CE_DISPLAYNAME_COOKIE_NAME, user.DisplayName, 10);
                CEHelper.SetCookie(CEConstants.CE_ROLE_COOKIE_ID, CEConstants.CE_ROLE_COOKIE_NAME, user.Role, 10);
                Response.Redirect(CEHelper.GetSiteRootUrl() + CEConstants.CE_ADMIN_PAGE);
            }
            else
            {
                errorMessage.Visible = true;
                string physicalPath = CEHelper.GetSiteRootUrl();
                ErrorText.Text = physicalPath + "\nYour user name and password do not match. Please try again.";
            }
        }

        protected void OnClearForm(object sender, EventArgs e)
        {
            UserNameBox.Text = string.Empty;
            PasswordBox.Text = string.Empty;
            ErrorText.Text = string.Empty;
            errorMessage.Visible = false;
        }

        protected void OnCancel(object sender, EventArgs e)
        {
            Response.Redirect(CEHelper.GetSiteRootUrl() + CEConstants.CE_TALENT_COMPETITION_PAGE);
        }
    }
}
