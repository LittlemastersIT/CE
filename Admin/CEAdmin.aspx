<%@ Page Title="Little Master Club Chinese Language and Talent Competition Admin" Language="C#" MasterPageFile="~/CEMain.Master" AutoEventWireup="true" CodeBehind="CEAdmin.aspx.cs" Inherits="CE.Admin.ceadmin" %>
<%@ Register TagPrefix="CE" Namespace="CE.Admin" Assembly="CE.Application" %>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="PlaceHolderScript" runat="server">
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/Themes/black/cepage.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/ceadmin.css")%>" media="all" />
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/cepages.js")%>"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="ce-admin-content">
        <div id="admin-home-page">
            <table>
                <tr>
                    <td class="admin-left-cell"><a href='javascript:setAbsoluteUrlHref("/admin/createpage.aspx")'><img src="<%=ResolveUrl("~/images/photoblue.png")%>" /></a></td>
                    <td class="admin-right-cell">
                        <div class="admin-page-title ce-h4"><a href='javascript:setAbsoluteUrlHref("/admin/createpage.aspx")'>Create Site Page from Content Source</a></div>
                        <div class="admin-page-description">
                            This page allows you to create a site html page from the content you created.
                            The site page is a html file that you can use for linking in your content page.
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="admin-left-cell"><a href='javascript:setAbsoluteUrlHref("/admin/reviewapplications.aspx")'><img src="<%=ResolveClientUrl("~/images/travel.png")%>" /></a></td>
                    <td class="admin-right-cell">
                        <div class="admin-page-title ce-h4"><a href='javascript:setAbsoluteUrlHref("/admin/reviewapplications.aspx")'>Tour Review</a></div>
                        <div class="admin-page-description">
                            This page allows you to review cultural tour application online. You can inteview, award, or reject the applicant. 
                            An automatic email will be sent to the applicant.
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="admin-left-cell"><a href='javascript:setAbsoluteUrlHref("/admin/reviewregistration.aspx?generateMasterFile=1")'><img src="<%=ResolveClientUrl("~/images/register.png")%>" /></a></td>
                    <td class="admin-right-cell">
                        <div class="admin-page-title ce-h4"><a href='javascript:setAbsoluteUrlHref("/admin/reviewregistration.aspx?generateMasterFile=1")'>Talent Competition Registration Review</a></div>
                        <div class="admin-page-description">
                            This page allows you to review talent competition registration online. You can eiher approve or reject the registration. 
                            An automatic email will be sent to the contact person on file.
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="admin-left-cell"><a href='javascript:setAbsoluteUrlHref("/admin/howto.aspx")'><img src="<%=ResolveClientUrl("~/images/howto.png")%>" /></a></td>
                    <td class="admin-right-cell">
                        <div class="admin-page-title ce-h4"><a href='javascript:setAbsoluteUrlHref("/admin/howto.aspx")'>Site Content Authoring How-To</a></div>
                        <div class="admin-page-description">
                            This page provides resources in Word document format to illustrate procedure relevant to site content authoring. Many of the documents
                            includes actual screen shots of the this portal to give you a real life visual cue to get to the point quickly.
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>