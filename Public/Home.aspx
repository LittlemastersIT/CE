<%@ Page Title="" Language="C#" MasterPageFile="~/CEMain.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CE.Pages.HomePage" %>

<%@ Register TagPrefix="CE" Namespace="CE.Pages" Assembly="CE.Application" %>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="PlaceHolderScript" runat="server">
    <link type="text/css" rel="stylesheet" href="/CSS/cehome.css" media="all" />
    <script type="text/javascript" src="/JS/cepages.js"></script>
    <script type="text/javascript" src="/JS/swipe.js"></script>
</asp:Content>

<asp:Content ID="MainContent1" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="ce-content">
        <div id="ce-headlines">
            <div id="ce-headline-slides">
                <asp:Repeater ID="HeadlineSlides" runat="server">
                    <ItemTemplate>
                        <div class="ce-headline-item">
                            <img class="ce-headline-image" src="<%# Eval("ImageUrl") %>" />
                            <div class="ce-headline-banner ce-h2 <%# Eval("Position") %> <%# Eval("Display") %>"><%# Eval("Title") %></div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <asp:Panel ID="NewsAndAnnouncements" runat="server">
                <div id="ce-announcements">
                    <div class="news-pane">
                        <div class="news-pane-header">
                            <span class="ce-h3"><asp:Literal ID="NewsHeader" runat="server" /></span>
                            <img src="/Images/crossBlue.png" onclick="closeAnnouncement();" />
                        </div>
                        <div id="news-content-pane" data-role="content" data-transition="slide">
                            <div id="news-all-content">
                                <asp:Repeater ID="NewsView" runat="server">
                                    <ItemTemplate>
                                        <div class="news-view">
                                            <div class="news-tile">
                                                <div class="news-title">
                                                    <table>
                                                        <tr>
                                                            <td class="news-icon-cell">
                                                                <img class="news-icon" src="<%# Eval("IconUrl") %>" /></td>
                                                            <td class="news-title-cell"><span class="ce-h4"><%# Eval("Title") %></span></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="news-summary ce-nromal"><%# Eval("Summary") %></div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <div id="news-paging-content">
                                <div class="news-paging-view">
                                    <div class="news-paging-list center">
                                        <div class="button-left"><img class="news-paging-icon" src="/Images/back.png" /></div>
                                            <asp:Repeater ID="NewsPages" runat="server">
                                                <ItemTemplate>
                                                    <div class="news-paging-item"></div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        <div class="button-right"><img class="news-paging-icon" src="/Images/forward.png" /></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div class="ce-home-tiles">
            <table class="ce-tile-container">
                <tr>
                    <asp:Repeater ID="HomeTiles" runat="server">
                        <ItemTemplate>
                            <td class="ce-tile">
                                <div class="ce-tile-title ce-h2"><%# Eval("Title") %></div>
                                <div class="ce-tile-divider"></div>
                                <div class="ce-tile-image">
                                    <img src="<%# Eval("ImageUrl") %>" />
                                </div>
                                <div class="ce-tile-text">
                                    <%# Eval("Text") %>
                                </div>
                            </td>
                        </ItemTemplate>
                    </asp:Repeater>
                </tr>
            </table>
        </div>
    </div>
    <div>
        <script type="text/javascript">
            var c = 0;
            $(document).ready(function () {
                var ceNewsPane = new Swipeable('#news-content-pane', '#news-all-content', '.news-view', '#news-paging-content', '.news-paging-item', '.button-left', '.button-right');
                ceNewsPane.init();
                adjustHeadlineTitle();
                adjustNewsPagingForIE();

                // play slide show for headline content if it has more than 1
                if ($('#ce-headline-slides .ce-headline-item').length > 1) {
                    $('.ce-headline-item:gt(0)').hide();
                    setInterval(function () {
                        $('#ce-headline-slides > div:first')
                          .fadeOut(5000)
                          .next()
                          .fadeIn(5000)
                          .end()
                          .appendTo('#ce-headline-slides');
                    }, 15000);
                }
            });

            function closeAnnouncement() {
                $('#ce-announcements').hide();
            }

            function toggleNewsPane() {
                var src = $('.news-pane-header img').attr('src');
                if (src != null && src.indexOf('cross') > 0) {
                    $('.news-pane-header img').attr('src', '/Images/plusBlue.png');
                    $('#news-content-pane').hide();
                }
                else {
                    $('.news-pane-header img').attr('src', '/Images/crossBlue.png');
                    $('#news-content-pane').show();
                }
            }

            function adjustHeadlineTitle() {
                var rightTexts = $('.ce-headline-banner.bottom-right');
                if (rightTexts != null) {
                    $('.ce-headline-banner.bottom-right').each(function (i, e) {
                        var font = 'normal 24px ' + $(this).css('font-family');
                        var text = $(this).html();
                        var width = getTextWidth(text, font);
                        $(this).css('left', (970 - width) + 'px');
                    });
                }
            }

            function adjustNewsPagingForIE() {
                var pageItems = $('#news-paging-content .news-paging-list .news-paging-item');
                var isOdd = (pageItems.length % 2);
                var left = 184;
                var half = Math.floor(pageItems.length / 2);
                if (isOdd) left = 178;
                left -= 18 * half;

                $('.news-paging-list > div').css('position', 'absolute');
                for (var i = 0; i < pageItems.length; i++) {
                    pageItems.get(i).style.left = '' + left + 'px';
                    pageItems.get(i).style.top = '10px';
                    left += 18;
                }
            }
        </script>
    </div>
</asp:Content>
