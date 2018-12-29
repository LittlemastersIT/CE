<%@ Page Language="C#" MasterPageFile="~/CEMain.Master" AutoEventWireup="true" CodeBehind="CEAlbum.aspx.cs" Inherits="CE.Pages.AlbumPage" %>

<%@ Register TagPrefix="CE" Namespace="CE.Pages" Assembly="CE.Application" %>
<%@ Register TagPrefix="JT3" Namespace="JT3Portfolio" Assembly="JT3Portfolio.Application" %>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="PlaceHolderScript" runat="server">
    <link type="text/css" rel="stylesheet" href="/CSS/colorbox.css" media="all" />
    <link type="text/css" rel="stylesheet" href="/CSS/ceAlbum.css" media="all" />
    <script type="text/javascript" src="/JS/jquery/jquery.colorbox-min.js"></script>
    <script type="text/javascript" src="/JS/swipe.js"></script>
    <script type="text/javascript" src="/JS/cealbum.js"></script>
</asp:Content>

<asp:Content ID="MainContent1" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div><asp:Literal ID="PageTheme" runat="server" /></div>
    <div class="ce-content-container ce-font">
        <div id="ce-album-content">
            <div id="album-title-bar">
                <asp:Repeater ID="AlbumTitles" runat="server">
                    <ItemTemplate>
                        <div class="album-title ce-h2 <%# Eval("Align") %> <%# Eval("Display") %>"><%# Eval("Title") %></div>
                        <div class="album-summary ce-h6 <%# Eval("Align") %> <%# Eval("Display") %>"><%# Eval("Summary") %></div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div id="album-tab" class="ce-large">
                <ul>
                    <asp:Repeater ID="AlbumTabs" runat="server">
                        <ItemTemplate>
                            <li class="tab-item" onclick="setAlbumTab('#ce-album-content', '#album-title-bar', '#album-tab', <%# Eval("Index") %>)"><%# Eval("Tab") %></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <jT3:ImageBrowser ID="j13ImageBrowser" Folder="\ImageResources" FolderUl="/ImageResources" />
            <asp:Repeater ID="AlbumGallery" runat="server" OnItemDataBound="Album_ItemDataBound">
                <ItemTemplate>
                    <div id="album<%# Eval("Index") %>" class="album-item <%# Eval("Display") %>">
                        <div id="gallery<%# Eval("Index") %>">
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
                        <div id="paging<%# Eval("Index") %>">
                            <div class="ce-paging-view">
                                <div class="photo-paging-list center">
                                    <div class="button-left"><img class="photo-paging-icon" src="/Images/back.png" /></div>
                                    <asp:Repeater ID="AlbumPages" runat="server">
                                        <ItemTemplate>
                                            <div class="photo-paging-item"></div>
                                            <div class="photo-paging-spacing"></div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <div class="button-right"><img class="photo-paging-icon" src="/Images/forward.png" /></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div>
        <script type="text/javascript">
            $(document).ready(function () {
                SetSwipeHanlders();
                enablePhotoViewer();
                setAlbumTab('#ce-album-content', '#album-title-bar', '#album-tab', 0);
            });
        </script>
    </div>
</asp:Content>
