<%@ Page Language="C#" MasterPageFile="~/CEMain.Master" AutoEventWireup="true" CodeBehind="Howto.aspx.cs" Inherits="CE.Pages.HowToPage " %>

<%@ Register TagPrefix="CE" Namespace="CE.Pages" Assembly="CE.Application" %>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="PlaceHolderScript" runat="server">
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/ceadmin.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/themes/black/cepage.css")%>" media="all" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="ce-admin-content">
        <div id="admin-howto-page">
            <div class="howto-header">
                <p class="ce-h2">Cutural Exploration Site Content Authoring References</p>
                <div class="ce-h5">
                </div>
            </div>
            <div class="howto-divider"> </div>
            <div class="howto-title">
                <img runat="server" src="~/images/pdf.png" /><span><a class="ce-h5" href="<%=ResolveUrl("~/admin/references/CE Web Site Content Update Tool.pdf")%>" target="_blank">Content Upload/Download Tool</a></span>
                <div class="ce-h6">
                    Filezilla is a popular FTP tool widely used by open soruce community for downloading and uploading files from and to between local computer and hosting server. 
                    The hosting server's CE site directly supports this tool.
                </div>
            </div><br />
            <div class="howto-title">
                <img runat="server" src="~/images/pdf.png" /><span><a class="ce-h5" href="<%=ResolveUrl("~/admin/references/CE Web Hosting Server Connection.pdf")%>" target="_blank">Connect to CE Hosting Server</a></span>
                <div class="ce-h6">
                    CE web site hosting server can be connected using Filezilla FTP tool. If you don't already have an account for the hosting server, pleasd request one from site administrator.
                </div>
            </div><br />
            <div class="howto-title">
                <img runat="server" src="~/images/pdf.png" /><a class="ce-h5" href="<%=ResolveUrl("~/admin/references/CE Web Site Content File Structure.pdf")%>" target="_blank">Content Folder Structure</a>
                <div class="ce-h6">
                    CE web site content is organized into 6 folders: content, album, journey, images, documents, and page. All of them has the same subfolder structure for
                    easy cross-reference. In addition, internal users can also see the Admin folder for site content management.
                </div>
            </div><br />
            <div class="howto-title">
                <img runat="server" src="~/images/pdf.png" /><a class="ce-h5" href="<%=ResolveUrl("~/admin/references/CE Web Site Content Creation Guide.pdf")%>" target="_blank">Content Creation/Modification Guide</a>
                <div class="ce-h6">
                    CE web site content creation/modification is designed to allow non-technical personnel to be able to create/modify content for CE site.
                    There are 3 cotnent tempplates available for CE content authors to use to create page content in different style.
                </div>
            </div><br />
            <div class="howto-title">
                <img runat="server" src="~/images/pdf.png" /><a class="ce-h5" href="<%=ResolveUrl("~/admin/references/CE Web Site Article Content Authoring.pdf")%>" target="_blank">Authoring CE Article</a>
                <div class="ce-h6">
                    CE article is a two-column layout with main content on the left and complimentary content on the right. Main content can be any html-compliant content.
                    Complimentary content support related links, image tiles, and testimonies style of display.
                </div>
            </div><br />
            <div class="howto-title">
                <img runat="server" src="~/images/pdf.png" /><a class="ce-h5" href="<%=ResolveUrl("~/admin/references/CE Web Site Album Content Authoring.pdf")%>" target="_blank">Authoring CE Album</a>
                <div class="ce-h6">
                    CE album is a photo gallery for visualizing the activity happened around CE functions. Album can be grouped into categories and view spearated.
                    Photos in a album are orgnized into 12 shots per view and swipeable left and right along with browsing buttons.
                </div>
            </div><br />
            <div class="howto-title">
                <img runat="server" src="~/images/pdf.png" /><a class="ce-h5" href="<%=ResolveUrl("~/admin/references/CE Web Site Journey Content Authoring.pdf")%>" target="_blank">Authoring CE Journey</a>
                <div class="ce-h6">
                    CE journey is a tour report from our cultural exploration tour participants to share their expierence with our users. A journey consists of 4 parts:
                    The participant prefessional information, the tour photos, the trip diary, and the oversea curriculum.
                </div>
            </div><br />
            <div class="howto-title">
                <img runat="server" src="~/images/pdf.png" /><a class="ce-h5" href="<%=ResolveUrl("~/admin/references/CE Web Page Creation for Content.pdf")%>" target="_blank">Generating CE Web Page for Content</a>
                <div class="ce-h6">
                    Each content created in CE site has a corresponding web page. The web page creation process is automated and available from <i>Create Web Page</i> submenu under <i>Admin</i> menu.
                </div>
            </div><br />
            <div class="howto-title">
                <img runat="server" src="~/images/pdf.png" /><a class="ce-h5" href="<%=ResolveUrl("~/admin/references/CE Tour Application Online Process.pdf")%>" target="_blank">CE Tour Application Online Process</a>
                <div class="ce-h6">
                    CE cultural tour application and review process can be conducted online. The participants fill out the online application form.
                    CE site administrator/review committee can then review/interview/award/reject/score an application online.
                </div>
            </div><br />
            <div class="howto-title">
                <img runat="server" src="~/images/pdf.png" /><a class="ce-h5" href="<%=ResolveUrl("~/admin/references/CE talent Competition Online Process.pdf")%>" target="_blank">CE Talent Competition Online Process</a>
                <div class="ce-h6">
                    CE talent competition registration and review process can be conducted online. The participants fill out the online registration form.
                    CE site administrator/review committee can then review/approve/reject a registration online. Automatic email is sent as a result of the review process.
                </div>
            </div><br />
            <div class="howto-title">
                <img runat="server" src="~/images/pdf.png" /><a class="ce-h5" href="<%=ResolveUrl("~/admin/references/How to Update Tour Journey for New Program Year.pdf")%>" target="_blank">Update Program Year for CE Tour</a>
                <div class="ce-h6">
                    CE Tour page content and links need to be updated after teachers are selected for the tour year. This document describes the procedure 
                    for updating the page content and link.
                </div>
            </div><br />
            <div class="howto-title">
                <img runat="server" src="~/images/pdf.png" /><a class="ce-h5" href="<%=ResolveUrl("~/admin/references/Yearly CE Web Application Update Guidelines.pdf")%>" target="_blank">Yearly CE Web Application Update Guidelines</a>
                <div class="ce-h6">
                    This document describes the guidelines for how to make changes to CE web site for a new program year and tips for maintaining the web site's ASP.Net code.
                </div>
            </div><br />
        </div>
    </div>
</asp:Content>
