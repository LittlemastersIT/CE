<%@ Page Title="" Language="C#" MasterPageFile="~/CEMain.Master" AutoEventWireup="true" CodeBehind="cevolunteer.aspx.cs" Inherits="CE.Pages.VolunteerPage" %>

<%@ Register TagPrefix="CE" Namespace="CE.Pages" Assembly="CE.Application" %>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="PlaceHolderScript" runat="server">
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/jquery-ui-1.10.3.custom.min.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/ceadmin.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/ceArticle.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/Themes/red/cepage.css")%>" media="all" />
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/jquery/jquery.colorbox-min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/jquery/jquery-ui-1.10.3.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/jquery/jquery.inputmask.js")%>"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="ce-content-container ce-font">
        <table>
            <tr>
                <td class="ce-content-cell">
                    <div id="ce-content-zone">
                        <div class="ce-page-articles">
                            <div class="ce-article-title ce-h3">Become a CE Volunteer</div>
                            <div class="ce-article-title-divider"></div>
                            <div class="ce-article-text">

                                <div class="ce-article-paragraph ">
                                    <div style="height: 1px; width: 960px;"></div>
                                    <p>
                                        As a community-based non-profit organization, the most critical success factor is our People - our team of tireless volunteers. 
                                       Since its inception over 15 years ago, we have been proud of having a great team of volunteers that are fun are hardworking. 
                                       Come join the CE team, let’s connect the communities in the US and Asia to promote a better understanding of greater China, 
                                       Chinese culture and people.
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="ce-form">
                        <div class="form-note-text ce-h5">If you like to become a Cultural Exploration Volunteer, please <b>fill out the following form</b> or <a style="color: blue !important; text-decoration: underline;" href="mailto:cesupport@culturalexploration.org?Subject=Apply%20for%20a%20CE%20volunteer" target="_top">email us</a>.</div>
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
                                    <div class="form-cell-title">Why you are interested in volunteering?</div>
                                    <div class="form-cell-text">
                                        <asp:TextBox ID="VolunteerReasonBox" MaxLength="4096" CssClass="ce-h4" Height="120px" Width="630px" TextMode="MultiLine" runat="server" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="form-span-cell600 ce-h5" colspan="2">
                                    <div class="form-cell-title">What area would you be interested in helping?</div>
                                    <div class="form-cell-text">
                                        <asp:TextBox ID="VolunteerAreaBox" MaxLength="4096" CssClass="ce-h4" Height="120px" Width="630px" TextMode="MultiLine" runat="server" />
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
            <div class="form-result-title"><span class="ce-h3">Volunteering Acknowledgement</span></div>
            <div class="form-result-text ce-h4">
                <table>
                    <tr>
                        <td style="width: 60px;"><img runat="server" src="~/images/confirm.png" /></td>
                        <td>Thank you for filling out the volunteer form to become a member of our oganization. We will contact you soon to share our passion and goals with you.
                        </td>
                    </tr>
                </table>
            </div>
            <div class="form-result-button">
                <asp:Button ID="DoneButton" OnClientClick="return exitDialog();" CssClass="ce-h4" Text="  Close & Back to Volunteer Page  " runat="server" />
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
