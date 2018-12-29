using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CE.Data;

namespace CE.Pages
{
    public partial class SponsorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            SendConfirmationEmail("sponsor", "apply");
        }

        private bool SendConfirmationEmail(string emailKey, string emailType)
        {
            bool emailSent = false;
            EmailInfo emailInfo = CEHelper.GetEmailConfiguration(emailKey, emailType);
            if (emailInfo != null && !string.IsNullOrEmpty(emailInfo.Message))
            {
                StringBuilder sb = new StringBuilder(emailInfo.Message);
                sb.Replace("{%company%}", CorporateNameBox.Text);
                sb.Replace("{%contact%}", ContactNameBox.Text);
                sb.Replace("{%email%}", ContactEmailBox.Text);
                sb.Replace("{%phone%}", ContactPhoneBox.Text);
                CEHelper.SendEmail(emailInfo.SmtpHost, emailInfo.SmtpPort, emailInfo.Sender, new string[] { ContactEmailBox.Text }, emailInfo.CCs, emailInfo.Subject, sb.ToString(), emailInfo.IsHtml, null);
                emailSent = true;
            }

            if (emailSent)
            {
                ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "FormCompleted",
                "formCompleted();",
                true);
            }

            return emailSent;
        }
    }
}