<%@ Page Language="C#" MasterPageFile="~/CEMain.Master" AutoEventWireup="true" validateRequest="false" CodeBehind="TalentRegistration.aspx.cs" Inherits="CE.Pages.TalentRegistrationPage" %>
<%@ Register TagPrefix="CE" Namespace="CE.Pages" Assembly="CE.Application" %>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="PlaceHolderScript" runat="server">
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/ceadmin.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/themes/blue/cepage.css")%>" media="all" />
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/jquery/jquery.colorbox-min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/jquery/jquery-ui-1.10.3.min.js")%>"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="ce-admin-content">
        <div class="ce-registration-page">
            <asp:Panel ID="RegistrationNote" runat="server">
                <div class="ce-temp-note ce-h4" style="color:#050143;border-color:red;">
                    <asp:Label ID="RegistrationNoteText" runat="server">
                       <p style="color:red">
                           Dear students, families and teachers,
                           <br />
                           <br />
                           As you know, we had postponed several times for the 2020 Washington State Chinese Language Art and Talent Competition.  With the recent development and uncertainty of the COVID-19 pandemic, after careful consideration, the competition organization committee had made the decision to cancel this year's competition.  This is not an easy decision, but we think this would allow us to focus our energy to plan for next year's activities.  We would refund all the registration fees for this year's competition.  
                           <br />
                           <br />
                           However, even the competition is canceled, we encourage the students to keep learning Chinese language and culture as this would benefit for lifetime, not just for the preparation of a competition. We hope you understand our decision and wish you and your family stay safe.
                           <br />
                           <br />
                           If you have any questions regarding the competition or need more information, please email cltc@littlemastersclub.org.
                           <br />
                           <br />
                           亲爱的老师，学生及家长，
                           <br />
                           <br />
                           如大家所知, 我们今年几次延期2020华州中文及才艺大赛。鉴于COVID-19发展的不确定性，经过认真考虑，大赛组委会决定取消今年的比赛。虽然我们很艰难做出这个决定，但是这样我们可以集中精力办好下一年的赛事。我们每年从6月就开始启动下一年的准备活动。我们将退还今年的报名费。
                           <br />
                           <br />
                           虽然比赛取消了，我们希望学生在这个特殊时期可以坚持对中国语言和文化的学习，争取明年大赛取得更好成绩。我们希望得到大家的理解，祝愿每个家庭平安，健康！
                           <br />
                           <br />
                           如有任何问题，请联系 cltc@littlemastersclub.org.
                       </p>
                    </asp:Label>
                    <br /><br />
                    <asp:Label ID="Label2" runat="server">
                       We will be offering only Paypal payment service for paying registration fee online going forward. 
                       The registration fee is $10 <u>per person</u> <u>per competition</u> registered.  
                       The registration fee is refundable if you withdraw before the registration deadline (<%= CompetitonRegistrationEndDate %>).  
                       40% of the administration fee will be deducted from the refund.
                    </asp:Label>
                    <br /><br />
                    <p>The event organizers reserve the right to use the photos and videos taken at the event for promotional purpose.</p><br />
                    <p>Please choose the appropriate competition type that applies to you to begin the registration process.</p>
                </div>
            </asp:Panel>
            <asp:Panel ID="RegistrationCloseNote" runat="server">
                <div class="ce-temp-note ce-h4" style="color:#050143;border-color:red;">
                    <asp:Label ID="RegistrationCloseNoteText" runat="server">
                        The registration for <%= CompetitionYear %> Washington State Language and Talent competition is closed. 
                        The competition event will be held at Chief Sealth International High School on <%= CompetitonEventDate %> from 9:00AM to 3:00PM.
                        We are looking forward to your participation.
                    </asp:Label>
                    <br /><br />
                </div>
            </asp:Panel>
            <asp:Panel ID="RegistrationStartNote" runat="server">
                <div class="ce-temp-note ce-h4" style="color:#050143;border-color:red;">
                    <asp:Label ID="RegistrationStartNoteText" runat="server"><%= CompetitionYear %> registration will begin on <%= CompetitonRegisrationStartDate %>.  We look forward to your participation.</asp:Label>
                    <br /><br />
                </div>
            </asp:Panel>
            <asp:Panel ID="CompetitionEndNote" runat="server">
                <div class="ce-temp-note ce-h4" style="color:#050143;border-color:red;">
                    <asp:Label ID="CompetitionEndNoteText" runat="server">
                        <%= CompetitionYear %> talent competition has been held and completed. Have a wonderful <%= CompetitionYear %> and we will see you next year.
                    </asp:Label>
                    <br /><br />
                </div>
            </asp:Panel>
            <asp:Panel ID="CompetitionTermiateNote" runat="server">
                <div class="ce-temp-note ce-h4" style="color:#050143;border-color:red;display:none">
                    <asp:Label ID="CompetitionTermiateNoteText" runat="server">
                    </asp:Label>
                    <br /><br />
                </div>
            </asp:Panel>
            <asp:Panel ID="RegistationSelection" runat="server">
                <div class="page-title ce-h2" style="display: inline;">
                    <asp:Label ID="Label1" runat="server"><%= CompetitionYear %> Competitions Registration</asp:Label>
                </div>
                <div class="reg-section-divider"></div>
                <div id="competition-selection">
                    <ul>
                        <li class="talent-competition-type">
                            <div class="talent-competition-label ce-h3">Select a competition type: </div>
                        </li>
                        <li class="talent-competition-type">
                            <a href="<%=ResolveUrl("~/public/talent/registration/individualregistration.aspx")%>">
                                <img runat="server" class="talent-competition-icon" src="~/images/single-constestant.png" alt="individual competition regisgtration" /><br /><br />
                                <span class="talent-competition-caption ce-h3">Individual</span>
                            </a>
                        </li>
                        <li>
                            <div class="talent-competition-spacing">&nbsp;</div>
                        </li>
                        <li class="talent-competition-type">
                        <a href="<%=ResolveUrl("~/public/talent/registration/teamregistration.aspx")%>">
                            <img runat="server" class="talent-competition-icon" src="~/images/team-contestants.png" alt="individual competition regisgtration" /><br /><br />
                                <span class="talent-competition-caption ce-h3">Team</span>
                            </a>
                        </li>
                    </ul>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
