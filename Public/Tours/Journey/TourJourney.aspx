<%@ Page Language="C#" MasterPageFile="~/CEMain.Master" AutoEventWireup="true" CodeBehind="TourJourney.aspx.cs" Inherits="CE.Pages.TourJourneyPage" %>

<%@ Register TagPrefix="CE" Namespace="CE.Pages" Assembly="CE.Application" %>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="PlaceHolderScript" runat="server">
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/jquery-ui-1.10.3.custom.min.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/cearticle.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/cejourney.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/themes/blue/cepage.css")%>" media="all" />
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/jquery/jquery-ui-1.10.3.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/cepages.js")%>"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div>
        <asp:Literal ID="PageTheme" runat="server" /></div>
    <div class="ce-content-container ce-font">
        <table>
            <tr>
                <td class="ce-content-cell">
                    <div id="journey-header" class="ce-h2">
                        <asp:label ID="TourYear" runat="server"></asp:label> Tour Participating Teachers
                    </div>
                    <div id="journey-roaster">
                        <div id="accordion">
                            <asp:Repeater ID="JourneyList" runat="server">
                                <ItemTemplate>
                                    <div class="journey-author"><%# Eval("DisplayName") %> - <%# Eval("School") %>, <%# Eval("Year") %></div>
                                    <div>
                                        <a runat="server" href="~/public/articles/cejourney.aspx?theme=blue&author=<%# Eval("FolderName") %>&year=<%# Eval("Year") %>">
                                            <img class="ce-paragraph-picture-left" src="<%# Eval("Photo") %>" align="left" />
                                        </a>
                                        <%# Eval("About") %> <span class="journey-readmore"><a runat="server" href="~/public/articles/cejourney.aspx?theme=blue&path=/tours/journey/<%# Eval("Year") %>&author=<%# Eval("FolderName") %>">...Read full journey</a></span>
                                        <br />
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>

                </td>
                <td class="ce-divider-cell"></td>
                <td class="ce-sidebar-cell">
                    <div class="ce-sidebar-zone">
                        <asp:Panel ID="CurrentJourneyPanel" runat="server">
                            <div><a runat="server" href="~/public/tours/journey/tourjourney.aspx"><span class="sidebar-header"><asp:label ID="ProgramYear" runat="server"></asp:label> Tour Teachers</span></a></div>
                            <div class="sidebar-divider"></div>
                            <div class="ce-page-sidebar-tile">
                                <table>
                                    <asp:Repeater ID="JourneyParticipants" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="sidebar-smallicon-cell"><img runat="server" class="sidebar-icon-small" src="~/Images/script.png" /></td>
                                                <td class="related-link-cell"><a runat="server" href="~/public/articles/cejourney.aspx?theme=blue&author=<%# Eval("FolderName") %>"><span><%# Eval("DisplayName") %></span></a></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                            <div class="ce-bartile-spacing"></div>
                        </asp:Panel>
                        <div class="ce-bartile-spacing"></div>
                        <asp:Panel ID="PreviousJourneyPanel" runat="server">
                            <div><span class="sidebar-header">Previous Tours</span></div>
                            <div class="sidebar-divider"></div>
                            <div class="ce-page-sidebar-tile">
                                <table>
                                    <asp:Repeater ID="PreviousTourJourney" runat="server">
                                        <ItemTemplate>
                                            <tr id="teacherBios<%# Eval("TourYear") %>">
                                                <td class="sidebar-smallicon-cell"><img runat="server" class="sidebar-icon-small" src="~/Images/script.png" /></td>
                                                <td class="related-link-cell"><a runat="server" href="~/public/tours/journey/tourjourney.aspx?year=<%# Eval("TourYear") %>"><span><%# Eval("TourYear") %> Tour Teachers</span></a></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td class="sidebar-smallicon-cell"><img runat="server" class="sidebar-icon-small" src="~/Images/team-contestants.png" /></td>
                                        <td class="related-link-cell"><a runat="server" href="~/public/tours/plan/2013/tourParticipants.html"><span>2013 Tour Participants</span></a></td>
                                    </tr>
                                    <tr>
                                        <td class="sidebar-smallicon-cell"><img runat="server" class="sidebar-icon-small" src="~/Images/world.png" /></td>
                                        <td class="related-link-cell"><a runat="server" href="~/public/tours/plan/2013/tourplaces.html"><span>2013 Tour Places</span></a></td>
                                    </tr>
                                    <tr>
                                        <td class="sidebar-smallicon-cell"><img runat="server" class="sidebar-icon-small" src="~/Images/pin_blue.png" /></td>
                                        <td class="related-link-cell"><a runat="server" href="~/public/tours/plan/2013/tourschedule.html"><span>2013 Tour Schedule</span></a></td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <script type="text/javascript">
            $(function () {
                $("#accordion").accordion({
                    collapsible: true,
                    active: false
                });
            });

            /*
             * hoverIntent | Copyright 2011 Brian Cherne
             * http://cherne.net/brian/resources/jquery.hoverIntent.html
             * modified by the jQuery UI team
             */
            $.event.special.hoverintent = {
                setup: function () {
                    $(this).bind("mouseover", jQuery.event.special.hoverintent.handler);
                },
                teardown: function () {
                    $(this).unbind("mouseover", jQuery.event.special.hoverintent.handler);
                },
                handler: function (event) {
                    var currentX, currentY, timeout,
                      args = arguments,
                      target = $(event.target),
                      previousX = event.pageX,
                      previousY = event.pageY;

                    function track(event) {
                        currentX = event.pageX;
                        currentY = event.pageY;
                    };

                    function clear() {
                        target
                          .unbind("mousemove", track)
                          .unbind("mouseout", clear);
                        clearTimeout(timeout);
                    }

                    function handler() {
                        var prop,
                          orig = event;

                        if ((Math.abs(previousX - currentX) +
                            Math.abs(previousY - currentY)) < 7) {
                            clear();

                            event = $.Event("hoverintent");
                            for (prop in orig) {
                                if (!(prop in event)) {
                                    event[prop] = orig[prop];
                                }
                            }
                            // Prevent accessing the original event since the new event
                            // is fired asynchronously and the old event is no longer
                            // usable (#6028)
                            delete event.originalEvent;

                            target.trigger(event);
                        } else {
                            previousX = currentX;
                            previousY = currentY;
                            timeout = setTimeout(handler, 100);
                        }
                    }

                    timeout = setTimeout(handler, 100);
                    target.bind({
                        mousemove: track,
                        mouseout: clear
                    });
                }
            };

            if (getQueryString("year") == '2013') $('#tourTeacherBios').hide();
        </script>
    </div>
</asp:Content>
