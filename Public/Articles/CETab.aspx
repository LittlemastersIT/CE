<%@ Page Language="C#" MasterPageFile="~/CEMain.Master" AutoEventWireup="true" CodeBehind="CETab.aspx.cs" Inherits="CE.Pages.TabPage" %>

<%@ Register TagPrefix="CE" Namespace="CE.Pages" Assembly="CE.Application" %>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="PlaceHolderScript" runat="server">
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/colorbox.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/cetab.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/ceresults.css")%>" media="all" />
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/jquery/jquery.colorbox-min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/cetab.js")%>"></script>
</asp:Content>

<asp:Content ID="MainContent1" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div><asp:Literal ID="PageTheme" runat="server" /></div>
    <div class="ce-content-container ce-font">
        <div id="ce-tab-content">
            <div id="tab-title-bar">
                <div class="tab-title ce-h2"><asp:Label ID="TabTitle" runat="server" CssClass="dark" Text="" /></div>
                <div class="tab-summary ce-h6"><asp:Label ID="TabTeaser" runat="server" CssClass="dark" Text="" /></div>
            </div>
            <div id="article-tab" class="ce-large">
                <ul>
                    <asp:Repeater ID="ArticleTabs" runat="server">
                        <ItemTemplate>
                            <li class="tab-item" onclick="setArticleTab('#ce-tab-content', '#article-tab', <%# Eval("Index") %>)"><%# Eval("Name") %></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <div id="article-archive" class="ce-large"></div>
            <asp:Repeater ID="TabCollection" runat="server" OnItemDataBound="Tab_ItemDataBound">
                <ItemTemplate>
                    <div id='tab<%# Eval("Index") %>' class="tab-articles-item <%# Eval("Display") %>">
                        <asp:Repeater ID="PageArticles" runat="server" OnItemDataBound="Article_ItemDataBound">
                            <ItemTemplate>
                                <div class="ce-article-title ce-h3"><%# Eval("Title") %></div>
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
                </ItemTemplate>
            </asp:Repeater>

            <div id='drawing-album' class="album-item hide">
                <div id='drawing-gallery'>
                    <asp:Repeater ID="AlbumViews" runat="server" OnItemDataBound="Photo_ItemDataBound">
                        <ItemTemplate>
                            <div class="ce-photo-view">
                                <ul>
                                    <asp:Repeater ID="AlbumPhotos" runat="server">
                                        <ItemTemplate>
                                            <li class="photo-tile"><img src="<%# Eval("ImageUrl") %>"/></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div id='drawing-paging'>
                    <div class="ce-paging-view">
                        <div class="photo-paging-list center">
                            <div class="button-left"><img runat="server" class="photo-paging-icon" src="~/Images/back.png" /></div>
                            <asp:Repeater ID="AlbumPages" runat="server">
                                <ItemTemplate>
                                    <div class="photo-paging-item"></div>
                                    <div class="photo-paging-spacing"></div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="button-right"><img runat="server" class="photo-paging-icon" src="~/Images/forward.png" /></div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <div>
        <script type="text/javascript">
            $(document).ready(function () {
                setArticleTab('#ce-tab-content', '#article-tab', 0);
                if (window.location.href.toLowerCase().indexOf('/results&') > 0) {
                    var pastResults = '<a href="' + baseUrl + '/public/talent/results/2015/ceresults.html" > <span>2015 Results</span></a >&nbsp;&nbsp;&nbsp; <a href="' + baseUrl + '/public/talent/results/2014/ceresults.html" > <span>2014 Results</span></a >&nbsp;&nbsp;&nbsp; <a href="' + baseUrl + '/public/talent/results/2013/ceresults.html" > <span>2013 Results</span></a > ';
                    $('#article-archive').html(pastResults);
                }
                else {
                    var currentResults = '<a href="' + baseUrl + '/public/talent/results/ceresults.html" > <span>2016 Results</span></a > ';
                    $('#article-archive').html(currentResults);
                }
            });
        </script>
    </div>
</asp:Content>
