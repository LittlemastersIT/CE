<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AnnouncementControl.ascx.cs" Inherits="CE.Controls.AnnouncementControl" %>

<div class="news-pane">
    <div class="news-pane-header">
        <span><asp:Literal ID="NewsHeader" runat="server" /></span>
    </div>
    <div id="news-content-pane" data-role="content" data-transition="slide">
        <div id="news-all-content">
            <asp:Repeater ID="NewsView" runat="server">
                <ItemTemplate>
                    <div class="news-view">
                        <div class="news-tile">
                            <div class="news-title"><span><%# Eval("Title") %></span></div>
                            <div class="news-summary"><span><%# Eval("Summary") %></span></div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div id="news-paging-content">
            <div id="news-paging-left" class="news-paging-button ce-left"><span>left</span></div>
            <div class="news-paging-view">
                <ul class="news-paging-list">
                    <asp:Literal ID="NewsPagingItems" runat="server" />
                </ul>
            </div>
            <div id="news-paging-right" class="news-paging-button ce-right"><span>right</span></div>
        </div>
    </div>
</div>
