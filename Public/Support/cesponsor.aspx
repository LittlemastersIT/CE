<%@ Page Title="" Language="C#" MasterPageFile="~/CEMain.Master" AutoEventWireup="true" CodeBehind="cesponsor.aspx.cs" Inherits="CE.Pages.SponsorPage" %>

<%@ Register TagPrefix="CE" Namespace="CE.Pages" Assembly="CE.Application" %>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="PlaceHolderScript" runat="server">
    <link type="text/css" rel="stylesheet" href="/CSS/jquery-ui-1.10.3.custom.min.css" media="all" />
    <link type="text/css" rel="stylesheet" href="/CSS/ceadmin.css" media="all" />
    <link type="text/css" rel="stylesheet" href="/CSS/ceArticle.css" media="all" />
    <link type="text/css" rel="stylesheet" href="/CSS/Themes/red/cepage.css" media="all" />
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

                            <div class="ce-article-title ce-h3">Sponsorship</div>
                            <div class="ce-article-title-divider"></div>
                            <div class="ce-article-text">

                                <div class="ce-article-paragraph ">
                                    <div style="padding-top: 5px;">
                                        <div style="font-size: 18px; font-weight: bold; padding-top: 20px;">Our 2014 CE Sponsors:</div>
                                    </div>
                                </div>
				                <div class="ce-article-paragraph ">
                                    <div style="font-size: 18px; font-weight: bold; padding-top: 20px;">CE Founder Sponsor</div>
                                    <div style="height: 1px; width: 960px;"></div>
                                    <div style="padding-bottom: 50px; padding-top: 20px;">
                                        <ul>
                                            <li style="display: inline;">
                                                <img src="/images/support/sponsor5.png" width="300px" alt="su development" /></li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="ce-article-paragraph ">
                                    <div style="font-size: 18px; font-weight: bold; padding-top: 20px;">CE Partner Sponsors</div>
                                    <div style="height: 1px; width: 960px;"></div>
                                    <div style="padding-bottom: 50px; padding-top: 20px;">
                                        <ul>
                                            <li style="display: inline;">
                                                <img src="/images/support/sponsor2.png" width="300px" alt="Breffni House" /></li>
                                            <li style="display: inline;">
                                                <img src="/images/support/sponsor3.jpg" width="300px" alt="Crowley" /></li>
                                            <li style="display: inline;">
                                                <img src="/images/support/sponsor4.jpg" width="300px" alt="The Tina Chang Group" /></li>
                                        </ul>
                                    </div>

                                    <div class="ce-article-paragraph ">
                                        <div style="padding-top: 5px;">
                                            <div style="font-size: 18px; font-weight: bold; padding-bottom: 20px;">We provide three levels of Sponsorship:</div>
                                            <div style="font-size: 14px; font-weight: bold; padding-top: 5px;"><span style="color: black; padding-right: 32px;">Cultural Exploration Founder Level</span> $2000 & Above</div>
                                        </div>
                                    </div>

                                    <div class="ce-article-paragraph ce-indent">
                                        <ul>
                                            <li class="ce-article-item">Special Recognition by Speaker</li>
                                            <li class="ce-article-item">Logo recognition – CE Website</li>
                                            <li class="ce-article-item">Logo recognition – CE Luncheon Event Program</li>
                                            <li class="ce-article-item">Logo recognition – CE Thank-you Letter</li>
                                        </ul>
                                    </div>

                                    <div class="ce-article-paragraph ">
                                        <div style="font-size: 14px; font-weight: bold; padding-top: 15px; padding-bottom: 5px;"><span style="color: black; padding-right: 40px;">Cultural Exploration Partner Level</span> $1000 & Above</div>
                                    </div>

                                    <div class="ce-article-paragraph ce-indent">
                                        <ul>
                                            <li class="ce-article-item">Individual On-screen Recognition</li>
                                            <li class="ce-article-item">Logo recognition – CE Website</li>
                                            <li class="ce-article-item">Logo recognition – CE Luncheon Event Program</li>
                                            <li class="ce-article-item">Logo recognition – CE Thank-you Letter</li>
                                        </ul>
                                    </div>

                                    <div class="ce-article-paragraph ">
                                        <div style="font-size: 14px; font-weight: bold; padding-top: 15px; padding-bottom: 5px;"><span style="color: black; padding-right: 15px;">Cultural Exploration Community Level</span> $500 & Above</div>
                                    </div>
                                </div>

                                <div class="ce-article-paragraph ce-indent">
                                    <ul>
                                        <li class="ce-article-item">Group On-screen Recognition</li>
                                        <li class="ce-article-item">Logo recognition – CE Website</li>
                                        <li class="ce-article-item">Logo recognition – CE Luncheon Event Program</li>
                                        <li class="ce-article-item">Logo recognition – CE Thank-you Letter</li>
                                    </ul>
                                </div>

                            </div>

                        </div>
                    </div>
                    <div class="ce-form">
                        <div class="form-note-text ce-h5" style="margin-top:30px;">If you like to become a Cultural Exploration Corporate Sponsor, please <b>fill out the following form</b> or <a style="color: blue !important; text-decoration: underline;" href="mailto:cesupport@culturalexploration.org?Subject=Apply%20for%20a%20CE%20corporate%20sponsor" target="_top">email us</a>.</div>
                        <table>
                            <tr>
                                <td class="form-section-title ce-h4" colspan="2">Corporate Sponsor Information</td>
                            </tr>
                            <tr>
                                <td class="form-left-cell ce-h5">
                                    <div><span class="red">*</span> Corporate Name :</div>
                                </td>
                                <td class="form-right-cell ce-h5">
                                    <asp:TextBox ID="CorporateNameBox" runat="server" CssClass="ce-h4" Width="480px" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="CorporateNameBox" runat="server" ErrorMessage="The corporate name is required." CssClass="form-error-text" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="form-left-cell ce-h5">
                                    <div><span class="red">*</span> Contact Name :</div>
                                </td>
                                <td class="form-right-cell ce-h5">
                                    <asp:TextBox ID="ContactNameBox" runat="server" CssClass="ce-h4" Width="480px" />
                                    <asp:RequiredFieldValidator ID="ContactNameRequiredValidator" ControlToValidate="ContactNameBox" runat="server" ErrorMessage="The contact name is required." CssClass="form-error-text" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="form-left-cell ce-h5">
                                    <div><span class="red">*</span> Contact Email :</div>
                                </td>
                                <td class="form-right-cell ce-h5">
                                    <asp:TextBox ID="ContactEmailBox" runat="server" CssClass="ce-h4" Width="480px" TextMode="Email" />
                                    <asp:RequiredFieldValidator ID="ContactEmailRequiredValidator" ControlToValidate="ContactEmailBox" runat="server" ErrorMessage="The contact email is required." CssClass="form-error-text" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="form-left-cell ce-h5">
                                    <div><span class="red">*</span> Contact Phone :</div>
                                </td>
                                <td class="form-right-cell ce-h5">
                                    <asp:TextBox ID="ContactPhoneBox" runat="server" CssClass="ce-h4" Width="480px" />
                                    <asp:RequiredFieldValidator ID="ContactPhoneRequiredValidator" ControlToValidate="ContactPhoneBox" runat="server" ErrorMessage="The contact phone is required." CssClass="form-error-text" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div class="form-section-divider"></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="form-button-cell" colspan="2">
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
            <div class="application-result-title"><span class="ce-h3">Volunteer Acknowledgement</span></div>
            <div class="application-result-text ce-h4">
                <table>
                    <tr>
                        <td style="width: 60px;"><img src="/images/confirm.png" /></td>
                        <td>Thank you for filling out the Corporate Sponsor form to become a supporting member of our oganization. We will contact you soon to share our passion and goals with you.</td>
                    </tr>
                </table>
            </div>
            <div class="application-result-button">
                <asp:Button ID="DoneButton" OnClientClick="return exitDialog();" CssClass="ce-h4" Text="  Close & Back to Corporeate Sponsor Page  " runat="server" />
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
