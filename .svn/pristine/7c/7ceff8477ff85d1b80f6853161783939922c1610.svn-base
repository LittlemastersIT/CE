using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CE.Data;

namespace CE.Pages
{
    public partial class DonatePage : System.Web.UI.Page
    {
        private const string PAYPAL_PAYMENT_URL = "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("cesupport.html");
            if (!IsPostBack)
            {
                string payment = Request.QueryString["payment"];
                if (payment != null)
                {
                    if (payment == "1") // paypal successes
                    {
                        // trigger the javascript function to go back to donation page
                        ScriptManager.RegisterStartupScript(
                            this,
                            this.GetType(),
                            "CEDonation",
                            "paymentReceived();",
                            true);
                    }
                    else // paypal cancelled or fails
                    {
                        // trigger the javascript function to go to the donation page
                        ScriptManager.RegisterStartupScript(
                            this,
                            this.GetType(),
                            "CEDonation",
                            "paymentCancel();",
                            true);
                    }
                }
            }
        }

        protected void OnPaypalPayment(object sender, EventArgs e)
        {
            //Paypal querystring parameters reference: http://www.paypalobjects.com/IntegrationCenter/ic_std-variable-ref-buy-now.html

            string redirecturl = CEHelper.GetConfiguration("PaypalUrl", CEConstants.PAYPAL_PAYMENT_URL);
            redirecturl += "&business=" + CEHelper.GetConfiguration("PaypalEMail", CEConstants.CE_PAYPAL_EMAIL);
            redirecturl += "&item_name=Cutural Exploration Donation";
            redirecturl += "&amount=" + PaymentAmountList.SelectedValue;
            redirecturl += "&quantity=1";
            redirecturl += "&currency=USD";
            redirecturl += "&return=" + HttpContext.Current.Server.UrlEncode(CEHelper.GetPageUrl() + "?payment=1");
            redirecturl += "&cancel_return=" + HttpContext.Current.Server.UrlEncode(CEHelper.GetPageUrl() + "?payment=0");
            Response.Redirect(redirecturl);
        }
    }
}