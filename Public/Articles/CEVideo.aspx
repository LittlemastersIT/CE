<%@ Page Language="C#" MasterPageFile="~/CEMain.Master" AutoEventWireup="true" CodeBehind="CEVideo.aspx.cs" Inherits="CE.Pages.VideoPage" %>

<%@ Register TagPrefix="CE" Namespace="CE.Pages" Assembly="CE.Application" %>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="PlaceHolderScript" runat="server">
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/colorbox.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/cearticle.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/cevideo.css")%>" media="all" />
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/jquery/jquery.colorbox-min.js")%>"></script>
</asp:Content>

<asp:Content ID="MainContent1" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div><asp:Literal ID="PageTheme" runat="server" /></div>
    <div class="ce-content-container ce-font">
        <table>
            <tr>
                <td class="ce-content-cell">
                    <div id="ce-content-zone">
                        <div id="video-title-bar">
                            <asp:Repeater ID="VideoTitles" runat="server">
                                <ItemTemplate>
                                    <div class="video-title ce-h2 <%# Eval("Display") %>"><%# Eval("Title") %></div>
                                    <div class="video-summary ce-h6 <%# Eval("Display") %>"><%# Eval("SubTitle") %></div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div id="video-tab" class="ce-large">
                            <ul>
                                <asp:Repeater ID="VideoTabs" runat="server">
                                    <ItemTemplate>
                                        <li class="tab-item" onclick="setVideoTab('#video-title-bar', '#video-tab', '.video-item', <%# Eval("Index") %>)"><%# Eval("Tab") %></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                        <div id="video-pane">
                            <asp:Repeater ID="VideoGallery" runat="server" OnItemDataBound="Video_ItemDataBound">
                                <ItemTemplate>
                                    <div id='video<%# Eval("Index") %>' class="video-item <%# Eval("Display") %>">
                                        <ul>
                                            <asp:Repeater ID="VideoClipList" runat="server">
                                                <ItemTemplate>
                                                    <li>
                                                        <div><iframe width="320" height="240" src="<%# Eval("ClipUrl") %>" frameborder="0" allowfullscreen="1"></iframe></div>
                                                        <div class="video-caption ce-h4"><%# Eval("Caption") %></div>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
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
    <div>
        <script type="text/javascript">
            $(document).ready(function () {
                setVideoTab('#video-title-bar', '#video-tab', '.video-item', 0);
            });

            // update the album tab and sync things up
            function setVideoTab(titlebarId, tabId, videoId, i) {
                // update video title and subtitle
                var titles = $(titlebarId + ' .video-title');
                titles.removeClass('show').addClass('hide');
                titles.eq(i).removeClass('hide').addClass('show');
                var subtitle = $(titlebarId + ' .video-summary');
                subtitle.removeClass('show').addClass('hide');
                subtitle.eq(i).removeClass('hide').addClass('show');

                // update video tab
                var videoTabs = $(tabId + ' ul li');
                videoTabs.removeClass('selected');
                videoTabs.eq(i).addClass('selected');

                // update video content
                var videos = $(videoId);
                videos.removeClass('show').addClass('hide');
                videos.eq(i).removeClass('hide').addClass('show');

                // adjust the height of the video display area as youtube 'iframe' does not give <div> a height
                var clips = videos.eq(i).find('ul li');
                var height = 300;
                if (clips != null) height = Math.floor((clips.length + 1) / 2) * 280 + 40;
                $('#video-pane').css('height', height + 'px');
            }
        </script>
    </div>
</asp:Content>
