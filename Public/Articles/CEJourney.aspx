<%@ Page Language="C#" MasterPageFile="~/CEMain.Master" AutoEventWireup="true" CodeBehind="CEJOurney.aspx.cs" Inherits="CE.Pages.JourneyPage" %>

<%@ Register TagPrefix="CE" Namespace="CE.Pages" Assembly="CE.Application" %>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="PlaceHolderScript" runat="server">
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/colorbox.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/cearticle.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/cejourney.css")%>" media="all" />
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/jquery/jquery.colorbox-min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/swipe.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/cealbum.js")%>"></script>
</asp:Content>

<asp:Content ID="MainContent1" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div>
        <asp:Literal ID="PageTheme" runat="server" />
    </div>
    <div class="ce-content-container ce-font">
        <div id="ce-journey-content">
            <div id="journey-title-bar">
                <asp:DataList ID="TitleInfo" runat="server">
                    <ItemTemplate>
                        <div class="journey-title ce-h2 <%# Eval("Align") %>">Feature Journey: <%# Eval("Title") %></div>
                        <div class="journey-summary ce-h6 <%# Eval("Align") %>"><%# Eval("Teaser") %></div>
                    </ItemTemplate>
                </asp:DataList>
                <div id="journey-more-link"><a href="<%=ResolveUrl("~/public/tours/journey/tourjourney.aspx")%>"><img runat="server" src="~/images/script.png" /> More journey...</a></div>
            </div>

            <div id="journey-tab" class="ce-large">
                <ul>
                    <li class="tab-item selected" onclick="setJourneyTab(0,5)" id="BioTab" >Journey Author</li>
                    <li class="tab-item" onclick="setJourneyTab(1,5)" id="PhotoTab" >Tour Photos</li>
                    <li class="tab-item" onclick="setJourneyTab(2,5)" id="TeachingPlanTab" runat="server">Lesson Plan</li>
                    <li class="tab-item" onclick="setJourneyTab(3,5)" id="DiaryTab" runat="server">Trip Diary</li>
                    <li class="tab-item" onclick="setJourneyTab(4,5)" id="VideoClipTab" runat="server">Video Clips</li>
                </ul>
            </div>

            <!-- journey bio tab content -->
            <div id="bio" class="journey-tab-content hide">
                <asp:DataList ID="BioInfo" runat="server">
                    <ItemTemplate>
                        <div class="bio-photo"><img src="<%# Eval("Photo") %>" /></div>
                        <div class="bio-info">
                            <ul>
                                <li class="bio-item author">Author: <%# Eval("Name") %></li>
                                <li class="bio-item">School: <%# Eval("School") %> <%# Eval("City") %> <%# Eval("State") %></li>
                                <li class="bio-item">Subject: <%# Eval("Subject") %></li>
                                <li class="bio-item">Grade: <%# Eval("Grade") %></li>
                                <li class="bio-item">Trip Year: <%# Eval("Year") %></li>
                                <li class="bio-item">Email: <%# Eval("Email") %></li>
                                <li class="bio-item about">About the Author:</li>
                            </ul>
                        </div>
                        <div class="bio-text"><%# Eval("About") %></div>
                    </ItemTemplate>
                </asp:DataList>
            </div>

            <!-- journey photos tab content -->           
            <div id="album0" class="journey-tab-content hide">
                <div id="summary">
                    <asp:Literal ID="AlbumSummary" runat="server"></asp:Literal>
                </div>
                <div id="gallery0">
                    <asp:Repeater ID="AlbumViews" runat="server" OnItemDataBound="Photo_ItemDataBound">
                        <ItemTemplate>
                            <div class="ce-photo-view">
                                <ul>
                                    <asp:Repeater ID="AlbumPhotos" runat="server">
                                        <ItemTemplate>
                                            <li class="photo-tile">
                                                <img src="<%# Eval("ImageUrl") %>" /></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div id="paging0">
                    <div class="ce-paging-view">
                        <div class="photo-paging-list center">
                            <div class="button-left">
                                <img runat="server" class="photo-paging-icon" src="~/Images/back.png" />
                            </div>
                            <asp:Repeater ID="AlbumPages" runat="server">
                                <ItemTemplate>
                                    <div class="photo-paging-item"></div>
                                    <div class="photo-paging-spacing"></div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="button-right">
                                <img runat="server" class="photo-paging-icon" src="~/Images/forward.png" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- journey teaching plan tab content -->
            <div id="curriculum" class="journey-tab-content hide">
               <div class="ce-page-articles">
                    <asp:Repeater ID="TeachingPlan" runat="server" OnItemDataBound="Article_ItemDataBound">
                        <ItemTemplate>
                            <div class="ce-article-title ce-h4"><%# Eval("Title") %></div>
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

            <!-- journey diary tab content -->
            <div id="diary" class="journey-tab-content hide">
                <div class="ce-page-articles">
                    <asp:Repeater ID="PageArticles" runat="server" OnItemDataBound="Article_ItemDataBound">
                        <ItemTemplate>
                            <div class="ce-article-title ce-h4"><%# Eval("Title") %></div>
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

            <!-- video clip tab content -->
            <div id="video" class="journey-tab-content hide">
                <div class="ce-page-articles">
                    <ul class="video-list">
                        <asp:Repeater ID="VideoClips" runat="server">
                            <ItemTemplate>
                                <li class="video-item">
                                    <div class="videoClip"><iframe width="360" height="270" src="<%# Eval("ClipUrl") %>" frameborder="0" allowfullscreen="1"></iframe></div>
                                    <div class="video_caption ce-h4"><%# Eval("Caption") %></div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div>
        <script type="text/javascript">
            $(document).ready(function () {
                SetSwipeHanlders();
                enablePhotoViewer();
                setJourneyTab(0,5);
            });

            function setJourneyTab(index, count) {
                var tabs = $('#journey-tab ul li');
                var tabIndex = tabs.length <= index ? tabs.length - 1 : index;
                tabs.removeClass('selected');
                tabs.addClass('unselected');
                tabs.eq(tabIndex).addClass('selected');

                var tabContent = $('#ce-journey-content .journey-tab-content');
                tabContent.removeClass('show').addClass('hide');
                tabContent.eq(index).removeClass('hide').addClass('show');
            }
        </script>
    </div>
</asp:Content>
