<%@ Page Title="Little Master Club Chinese Language and Talent Competition Admin" Language="C#" MasterPageFile="~/CEMain.Master" AutoEventWireup="true" CodeBehind="CompetitionReport.aspx.cs" Inherits="CE.Admin.CompetitionReport" %>
<%@ Register TagPrefix="CE" Namespace="CE.Admin" Assembly="CE.Application" %>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="PlaceHolderScript" runat="server">
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/Themes/black/cepage.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/ceadmin.css")%>" media="all" />
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/cepages.js")%>"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="ce-admin-content">
        <div id="admin-howto-page">
            <div class="howto-header">
                <p class="ce-h2">Cutural Exploration Talent Competition Report Generator</p>
                <div class="ce-h5">
                </div>
            </div>
            <div class="howto-divider"> </div>
            <div class="howto-title">
                <img src="<%=ResolveUrl("~/images/excel-48.png")%>" style="margin-right:10px;" /><span><a class="ce-h5" href="<%=ResolveUrl("~/admin/CompetitionReport.aspx?action=signin&group=individual")%>">Create Individual Talent Competition Sign-In Sheets</a></span>
                <div class="ce-h6">
                    <asp:Label ID="Report1" runat="server">
                        Generate Sign-in Sheet for each contestant participating in talent competition. Each sign-in sheet is an Excel file in '/data/report/<%= CompetitionYear %>/SignIn Sheets/Individual' folder
                        created from Excel template file 'SignInSheetTemplate.xlsx'.
                    </asp:Label>
                </div>
            </div>
            <div class="howto-title">
                <img src="<%=ResolveUrl("~/images/excel-48.png")%>" style="margin-right:10px;" /><span><a class="ce-h5" href="<%=ResolveUrl("~/admin/CompetitionReport.aspx?action=signin&group=team")%>">Create Team Talent Competition Sign-In Sheets</a></span>
                <div class="ce-h6">
                    <asp:Label ID="Report2" runat="server">
                        Generate Sign-in Sheet for each team participating in talent competition. Each sign-in sheet is an Excel file in '/data/report/<%= CompetitionYear %>/SignIn Sheets/Team' folder
                        created from Excel template file 'SignInSheetTemplate.xlsx'.
                    </asp:Label> 
                </div>
            </div>
            <div class="howto-title">
                <img src="<%=ResolveUrl("~/images/excel-48.png")%>" style="margin-right:10px;" /><span><a class="ce-h5" href="<%=ResolveUrl("~/admin/CompetitionReport.aspx?action=score&group=individual")%>">Create Individual Talent Competition Score Sheets</a></span>
                <div class="ce-h6">
                    <asp:Label ID="Report3" runat="server">
                        Generate Score Sheet for each contestant participating in talent competition event. Each score sheet is an Excel file in '/data/report/<%= CompetitionYear %>/Score Sheets' folder
                        uniquely identified by division, category, and class from Excel template file.
                    </asp:Label>
                </div>
            </div>
            <div class="howto-title">
                <img src="<%=ResolveUrl("~/images/excel-48.png")%>" style="margin-right:10px;" /><span><a class="ce-h5" href="<%=ResolveUrl("~/admin/CompetitionReport.aspx?action=score&group=team")%>">Create Team Talent Competition Score Sheets</a></span>
                <div class="ce-h6">
                    <asp:Label ID="Report4" runat="server">
                        Generate Score Sheet for each team participating in talent competition event. Each score sheet is an Excel file in '/data/report/<%= CompetitionYear %>/Score Sheets' folder
                        uniquely identified by division, category, and class from Excel template file.
                    </asp:Label> 
                </div>
            </div>
            <div class="howto-title">
                <img src="<%=ResolveUrl("~/images/excel-48.png")%>" style="margin-right:10px;" /><span><a class="ce-h5" href="<%=ResolveUrl("~/admin/CompetitionReport.aspx?action=contestant&group=both")%>">Create Participating Competitions Per Contestant Report</a></span>
                <div class="ce-h6">
                    <asp:Label ID="Report6" runat="server">
                        Generate competition list Sheet for each participant. The sheet is an Excel file in '/data/report/<%= CompetitionYear %>/Contestant Sheets' folder.
                    </asp:Label>
                </div>
            </div>
            <div class="howto-title">
                <img src="<%=ResolveUrl("~/images/excel-48.png")%>" style="margin-right:10px;" /><span><a class="ce-h5" href="<%=ResolveUrl("~/admin/CompetitionReport.aspx?action=headcount&group=both")%>">Create Talent Competition Head Count Report</a></span>
                <div class="ce-h6">
                    <asp:Label ID="Report5" runat="server">
                        Generate Head and Team Count Sheet for talent competition event. The Excel file is '/data/report/<%= CompetitionYear %>/<%= string.Format(HeadCountFileFormat, CompetitionYear) %>'.
                    </asp:Label>
                </div>
            </div>
            <div class="howto-title">
                <img src="<%=ResolveUrl("~/images/excel-48.png")%>" style="margin-right:10px;" /><span><a class="ce-h5" href="<%=ResolveUrl("~/admin/CompetitionReport.aspx?action=conflict&group=individual")%>">Create Conflicting Competitions Report</a></span>
                <div class="ce-h6">
                    <asp:Label ID="Report7" runat="server">
                        Generate an Excel file for all contestants who have conflict competition schedule. The Excel file is '/data/report/<%= CompetitionYear %>/<%= string.Format(ConflictFileFormat, CompetitionYear) %>'.
                    </asp:Label>
                </div>
            </div>
            <div class="howto-title">
                <img src="<%=ResolveUrl("~/images/excel-48.png")%>" style="margin-right:10px;" /><span><a class="ce-h5" href="<%=ResolveUrl("~/admin/CompetitionReport.aspx?action=trophy&group=both")%>">Create Throphy Count Report</a></span>
                <div class="ce-h6">
                    <asp:Label ID="Report8" runat="server">
                        Generate an Excel file for trophy counts. The Excel file is '/data/report/<%= CompetitionYear %>/<%= string.Format(TrophyFileFormat, CompetitionYear) %>'.
                    </asp:Label>
                </div>
            </div>
            <div class="howto-title">
                <img src="<%=ResolveUrl("~/images/excel-48.png")%>" style="margin-right:10px;" /><span><a class="ce-h5" href="<%=ResolveUrl("~/admin/CompetitionReport.aspx?action=awards&group=both")%>">Create Competition Award Presentation Report</a></span>
                <div class="ce-h6">
                    <asp:Label ID="Label1" runat="server">
                        Generate an Excel file for competition award presentation. The Excel file is '/data/report/<%= CompetitionYear %>/<%= string.Format(CompetitionAwardFormat, CompetitionYear) %>'.
                    </asp:Label>
                </div>
            </div>
            <div class="howto-title">
                <img src="<%=ResolveUrl("~/images/excel-48.png")%>" style="margin-right:10px;" /><span><a class="ce-h5" href="<%=ResolveUrl("~/admin/CompetitionReport.aspx?action=winners&group=both")%>">Create Competition Winners List</a></span>
                <div class="ce-h6">
                    <asp:Label ID="Label2" runat="server">
                        Generate an Excel file for competition winners. The Excel file is '/data/report/<%= CompetitionYear %>/<%= string.Format(CompetitionWinnerFormat, CompetitionYear) %>'.
                    </asp:Label>
                </div>
            </div>
            <div class="howto-title">
                <img src="<%=ResolveUrl("~/images/excel-48.png")%>" style="margin-right:10px;" /><span><a class="ce-h5" href="<%=ResolveUrl("~/admin/CompetitionReport.aspx?action=certificate&group=both")%>">Create Competition Winner Certificates</a></span>
                <div class="ce-h6">
                    <asp:Label ID="Label3" runat="server">
                        Generate official certificate for each competition winner. The certificate files are in '/data/report/<%= CompetitionYear %>/Winner Certificates' folder.
                    </asp:Label>
                </div>
            </div>
            <div class="howto-title">
                <img src="<%=ResolveUrl("~/images/excel-48.png")%>" style="margin-right:10px;" /><span><a class="ce-h5" href="<%=ResolveUrl("~/admin/CompetitionReport.aspx?action=participating&group=both")%>">Create Competition Participating Certificates</a></span>
                <div class="ce-h6">
                    <asp:Label ID="Label4" runat="server">
                        Generate official certificate for each competition winner. The certificate files are in '/data/report/<%= CompetitionYear %>/Participating Certificates' folder.
                    </asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>