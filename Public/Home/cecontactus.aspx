<%@ Page Title="" Language="C#" MasterPageFile="~/CEMain.Master" AutoEventWireup="true" CodeBehind="cecontactus.aspx.cs" Inherits="CE.Pages.ContactUsPage" %>

<%@ Register TagPrefix="CE" Namespace="CE.Pages" Assembly="CE.Application" %>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="PlaceHolderScript" runat="server">
    <link type="text/css" rel="stylesheet" href="/CSS/jquery-ui-1.10.3.custom.min.css" media="all" />
    <link type="text/css" rel="stylesheet" href="/CSS/ceadmin.css" media="all" />
    <link type="text/css" rel="stylesheet" href="/CSS/ceArticle.css" media="all" />
    <link type="text/css" rel="stylesheet" href="/CSS/Themes/black/cepage.css" media="all" />
    <script type="text/javascript" src="/JS/jquery/jquery.colorbox-min.js"></script>
    <script type="text/javascript" src="/JS/jquery/jquery-ui-1.10.3.min.js"></script>
    <script type="text/javascript" src="/JS/jquery/jquery.inputmask.js"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="ce-content-container ce-font">
        <table>
            <tr>
                <td class="ce-content-cell">
                    <div id="ce-content-zone">
                        <div class="ce-page-articles">
                            <div class="ce-article-title ce-h3">Contact Us</div>
                            <div class="ce-article-title-divider"></div>
                            <div class="ce-article-text">

                                <div class="ce-article-paragraph ">
                                    <div style="height: 1px; width: 960px;"></div>
                                    <p>
                                        As a community-based non-profit organization, we like to hear from you and are welcome your comments so that we can get better everyday. 
                                        If you like to contact us, please <b>fill out the following form</b> or <a style="color: blue !important; text-decoration: underline;" href="mailto:cesupport@culturalexploration.org?Subject=Message%20for%20Cultural%20Exploration" target="_top">email us</a>.
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="ce-form">
                        <table>
                            <tr>
                                <td class="form-section-title ce-h4" colspan="2">Contact Information</td>
                            </tr>
                            <tr>
                                <td class="form-left-cell ce-h5">
                                    <div><span class="red">*</span> Contact Name :</div>
                                </td>
                                <td class="form-right-cell1 ce-h5">
                                    <asp:TextBox ID="ContactNameBox" runat="server" CssClass="ce-h4" Width="490px" />
                                    <asp:RequiredFieldValidator ID="ContactNameRequiredValidator" ControlToValidate="ContactNameBox" runat="server" ErrorMessage="The contact name is required." CssClass="form-error-text" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="form-left-cell ce-h5">
                                    <div><span class="red">*</span> Contact Email :</div>
                                </td>
                                <td class="form-right-cell1 ce-h5">
                                    <asp:TextBox ID="ContactEmailBox" runat="server" CssClass="ce-h4" Width="490px" TextMode="Email" />
                                    <asp:RequiredFieldValidator ID="ContactEmailRequiredValidator" ControlToValidate="ContactEmailBox" runat="server" ErrorMessage="The contact email is required." CssClass="form-error-text" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="form-left-cell ce-h5">
                                    <div><span class="red">*</span> Contact Phone :</div>
                                </td>
                                <td class="form-right-cell1 ce-h5">
                                    <asp:TextBox ID="ContactPhoneBox" runat="server" CssClass="ce-h4" Width="490px" />
                                    <asp:RequiredFieldValidator ID="ContactPhoneRequiredValidator" ControlToValidate="ContactPhoneBox" runat="server" ErrorMessage="The contact phone is required."  CssClass="form-error-text" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="form-span-cell600 ce-h5" colspan="2">
                                    <div class="form-section-title ce-h4">Your Message :</div>
                                    <div class="form-cell-text">
                                        <asp:TextBox ID="ContactMessageBox" MaxLength="4096" CssClass="ce-h4" Height="240px" Width="630px" TextMode="MultiLine" runat="server" />
                                    </div>
                                </td>
                            </tr>
                           <tr>
                                <td colspan="2">
                                    <div class="form-section-divider"></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="form-button-cell" colspan="2" style="width:100%">
                                    <asp:Button ID="SubmitButton" runat="server" CssClass="action-button" Font-Bold="true" Text="  Submit  " OnClientClick="" OnClick="Submit_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>

    <div id="modal-dialog" class="hide">
        <div id="form-success-modal">
            <div class="form-result-title"><span class="ce-h3">Contact Us Acknowledgement</span></div>
            <div class="form-result-text ce-h4">
                <table>
                    <tr>
                        <td style="width: 60px;"><img src="/images/confirm.png" /></td>
                        <td>Thank you for filling out the Contact Us form. An email has been sent to you to confirm that we have received your message and is processing it.
                        </td>
                    </tr>
                </table>
            </div>
            <div class="form-result-button">
                <asp:Button ID="DoneButton" OnClientClick="return closeDialog();" CssClass="ce-h4" Text="  Close & Back to CE Home Page  " runat="server" />
            </div>
        </div>
    </div>

    <div>
        <script type="text/javascript">
            var ContactPhoneBoxID = '#<%= ContactPhoneBox.ClientID %>';

            $(document).ready(function () {
                bindPhoneMask();
            });

            function bindPhoneMask() {
                $(ContactPhoneBoxID).inputmask("mask", { "mask": "999-999-9999" });
            }

            function formCompleted() {
                bindPhoneMask();
                modalDialog('#form-success-modal');
            }
        </script>
    </div>
</asp:Content>
