<%@ Page Title="" Language="C#" MasterPageFile="~/CEMain.Master" AutoEventWireup="true" CodeBehind="cedonate.aspx.cs" Inherits="CE.Pages.DonatePage" %>
<%@ Register TagPrefix="CE" Namespace="CE.Pages" Assembly="CE.Application" %>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="PlaceHolderScript" runat="server">
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/ceadmin.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/Themes/red/cepage.css")%>" media="all" />
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/jquery/jquery.colorbox-min.js")%>"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="ce-content-container ce-font">
        <div style="padding:20px;">
            <table style="width:100%;">
                <tr>
                    <td class="ce-article-title ce-h2" colspan="2"><p style="padding-bottom:10px;">Donation Online</p></td>
                </tr>
                <tr>
                    <td class="reg-cell-text ce-h4" colspan="2">
                        <div class="ce-article-title-divider"></div><br />
                        <div class="online-payment-note">
                            <p>The funding of our programs is made possible through our community based fundraisers, private donations and corporate sponsorship. 
                                Proceeds will be used on programs that directly benefit teachers and students. Our board of directors and executive team members 
                                are volunteers.</p><br />
                            <p>Cultural Exploration of Greater China Foundation is a 501(c) 3 non-profit organization. The EIN # is #80-0173944. 
                               Your donation is tax deductible.</p><br />
                            <p>Please donate via PayPal by clicking the form below to complete the online donation or, if you pefer, mail check to:</p><br />
                            <p>CE of Greater China Foundation (CE)</p>
                            <p>One Lake Bellevue Drive, Suite 210</p>
                            <p>Bellevue WA 98005</p>
                        </div>
                    </td>
                </tr>
                    <tr>
                    <td class="reg-left-cell ce-h5" style="width:140px;font-weight:bold;padding-left:10px;">Donation Amount: </td>
                    <td class="reg-right-cell ce-h5">
                        <div class="registration-fee">
                            <asp:DropDownList ID="PaymentAmountList" runat="server" Width="200px">
                                <asp:ListItem Text="$50 level" Value="50" />
                                <asp:ListItem Text="$100 level" Value="100" />
                                <asp:ListItem Text="$200 level" Value="200" />
                                <asp:ListItem Text="$500 level" Value="500" />
                                <asp:ListItem Text="$1000 Community level" Value="1000" />
                                <asp:ListItem Text="$2000 Partner level" Value="2000" />
                                <asp:ListItem Text="$4000 Founder level" Value="5000" />
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="reg-section-divider"></div>
                    </td>
                    </tr>
                <tr>
                    <td class="reg-button-cell" colspan="2">
                        <asp:LinkButton ID="PaypalPaymentButton" runat="server" CssClass="paypal-button" OnClick="OnPaypalPayment"><img runat="server" title="Paypal online donation" src="~/images/paypal_donate.gif" /></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="paypal-logo" style="float:right;">
                            <a href="#" onclick="javascript:window.open('https://www.paypal.com/cgi-bin/webscr?cmd=xpt/Marketing/popup/OLCWhatIsPayPal-outside','olcwhatispaypal','toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, width=400, height=350');">
                                <img runat="server" src="~/images/paypal_donate_cc.gif" border="0" alt="Now Accepting PayPal">
                            </a>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <div id="modal-dialog" class="hide">
        <div id="form-success-modal">
            <div class="application-result-title"><span class="ce-h3">Donation Acknowledgement</span></div>
            <div class="application-result-text ce-h4">
                <table>
                    <tr>
                        <td style="width:60px;"><img runat="server" src="~/images/confirm.png" /></td>
                        <td>
                            Thank you note text goes here.
                        </td>
                    </tr>
                </table>
            </div>
            <div class="application-result-button">
                <asp:Button ID="DoneButton" OnClientClick="return closeDialog();" CssClass="ce-h4" Text="  Close & Back to CE Home Page  " runat="server" />
            </div>
        </div>
    </div>

    <div>
        <script type="text/javascript">
            function paymentReceived() {
                modalDialog('#form-success-modal');
            }

            function paymentCancel() {
            }
        </script>
    </div>
</asp:Content>