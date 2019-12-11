<%@ Page Title="" Language="C#" MasterPageFile="~/CEMain.Master" AutoEventWireup="true" CodeBehind="cearticle.aspx.cs" Inherits="CE.Pages.ArticlePage" %>

<%@ Register TagPrefix="CE" Namespace="CE.Pages" Assembly="CE.Application" %>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="PlaceHolderScript" runat="server">
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/ceArticle.css")%>" media="all" />
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/cepages.js")%>"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div>
        <asp:Literal ID="PageTheme" runat="server" /></div>
    <div class="ce-content-container ce-font">
        <table>
            <tr>
                <td class="ce-content-cell">
                    <div id="ce-language-link"><asp:Literal ID="LanguageLink" runat="server"></asp:Literal></div>
                    <div id="ce-content-zone">
                        <div class="ce-page-articles">
                            <asp:Repeater ID="PageArticles" runat="server" OnItemDataBound="Article_ItemDataBound">
                                <ItemTemplate>
                                    <div class="ce-article-title ce-h3"><%# Eval("Title") %></div>
                                    <div class="ce-article-title-divider"></div>
                                    <div class="ce-article-text">
                                        <asp:Repeater ID="Paragraphs" runat="server">
                                            <ItemTemplate>
                                                <div class="ce-article-paragraph <%# Eval("Class") %>"><%# Eval("PictureUrl") %><%# Eval("TextBlock") %></div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </td>
                <td class="ce-divider-cell"></td>
                <td class="ce-sidebar-cell">
                    <div class="ce-sidebar-zone">
                        <asp:Panel ID="RelatedLinksPanel" runat="server">
                            <asp:Repeater ID="RelatedLinksList" runat="server" OnItemDataBound="RelatedLinksList_DataBound">
                                <ItemTemplate>
                                    <div class="related-link-header"><span class="sidebar-header"><%# Eval("Header") %></span></div>
                                    <div class="sidebar-divider"></div>
                                    <div class="ce-page-sidebar-tile">
                                        <table>
                                            <asp:Repeater ID="RelatedLinks" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="sidebar-smallicon-cell">
                                                            <img class="sidebar-icon-small" src="<%# Eval("IconUrl") %>" /></td>
                                                        <td class="related-link-cell"><a href="<%# Eval("LinkUrl") %>" target="<%# Eval("Target") %>"><span><%# Eval("Title") %></span></a></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="ce-bartile-spacing"></div>
                        </asp:Panel>
                        <asp:Repeater ID="SideBarTiles" runat="server">
                            <ItemTemplate>
                                <div class="ce-page-sidebar-tile">
                                    <div class="ce-sidebar-caption <%# Eval("CaptionTop") %>"><%# Eval("Caption") %></div>
                                    <a href="<%# Eval("LinkUrl") %>">
                                        <img class="ce-sidebar-image" src="<%# Eval("ImageUrl") %>" /></a>
                                    <div class="ce-sidebar-caption <%# Eval("CaptionBottom") %>"><%# Eval("Caption") %></div>
                                </div>
                                <div class="ce-bartile-spacing"></div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Panel ID="TestimonyPanel" runat="server">
                            <asp:Literal ID="TestimoniesHeader" runat="server" />
                            <div class="sidebar-divider"></div>
                            <asp:Repeater ID="SideBarTestimonies" runat="server">
                                <ItemTemplate>
                                    <div class="ce-page-sidebar-tile">
                                        <table>
                                            <tr>
                                                <td class="sidebar-icon-cell">
                                                    <img class="sidebar-icon" src="<%# Eval("IconUrl") %>" /></td>
                                                <td class="sidebar-caption-cell"><span><%# Eval("Caption") %></span></td>
                                            </tr>
                                        </table>
                                        <div class="sidebar-testimony-text"><a href="<%# Eval("LinkUrl") %>"><%# Eval("Text") %></a></div>
                                    </div>
                                    <div class="ce-bartile-spacing"></div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </asp:Panel>
                        <asp:Panel ID="VideoClipPanel" runat="server">
                            <asp:Literal ID="VideoClipHeader" runat="server" />
                            <div class="sidebar-divider"></div>
                            <asp:Repeater ID="SideBarVideoClips" runat="server">
                                <ItemTemplate>
                                   <div class="ce-page-sidebar-tile">
                                        <iframe width="180" height="135" src="<%# Eval("ClipUrl") %>" frameborder="0" allowfullscreen="1"></iframe>
                                        <div class="ce-sidebar-caption"><%# Eval("Caption") %></div>
                                   </div>
                                   <div class="ce-bartile-spacing"></div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </asp:Panel>
                        <div class="ce-bartile-spacing"></div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
