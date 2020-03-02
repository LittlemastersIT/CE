<%@ Page Language="C#" MasterPageFile="~/CEMain.Master" AutoEventWireup="true" CodeBehind="ReviewApplications.aspx.cs" Inherits="CE.Pages.ReviewApplicationsPage " %>

<%@ Register TagPrefix="CE" Namespace="CE.Pages" Assembly="CE.Application" %>
<%@ Register TagPrefix="CE" Namespace="CE.Data" Assembly="CE.Application" %>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="PlaceHolderScript" runat="server">
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/jquery-ui-1.10.3.custom.min.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/cearticle.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/ceadmin.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/cetab.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/themes/blue/cepage.css")%>" media="all" />
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/jquery/jquery-ui-1.10.3.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/jquery/jquery.colorbox-min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/cetab.js")%>"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="ce-admin-content" style="padding-top:1px !important;">
        <div id="search-box">
            <table>
                <tr>
                    <td>
                        <div class="search-item-label">&nbsp;School District</div>
                        <div class="search-item-control">
                            <asp:DropDownList ID="SchoolDistrictList" runat="server" AutoPostBack="true" Width="200px"></asp:DropDownList>
                        </div>
                    </td>
                    <td>
                        <div class="search-item-label">Status</div>
                        <div class="search-item-control">
                            <asp:DropDownList ID="StatusList" runat="server" Width="100px">
                                <asp:ListItem Text="All" Value="" />
                                <asp:ListItem Text="Apply" Value="Apply" />
                                <asp:ListItem Text="Withdraw" Value="Withdraw" />
                                <asp:ListItem Text="Review" Value="Review" />
                                <asp:ListItem Text="Interview" Value="Interview" />
                                <asp:ListItem Text="Rejected" Value="Rejected" />
                                <asp:ListItem Text="Awarded" Value="Awarded" />
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td style="padding-left:10px;">
                        <asp:LinkButton ID="SearchButton" runat="server" CssClass="search-button" OnClick="OnSearchApplicants">
                            <img runat="server" src="~/images/search.png" alt="search for registration entries based on criteria given" /><span>Search</span>
                        </asp:LinkButton>
                    </td>
                    <td>
                        <asp:Label ID="SearchResultCount" runat="server" Text="" />
                    </td>
                </tr>
            </table>
        </div>

        <div class="search-result-total">
            <table>
                <tr>
                    <td class="search-note"><asp:Literal ID="SearchResultItems" runat="server" Text=""></asp:Literal></td>
                    <td class="search-export"><asp:LinkButton ID="ExcelExport" runat="server" CssClass="excel-export" Text="Export to Excel" OnClick="OnExcelExport"></asp:LinkButton></td>
                </tr>
            </table>
        </div>

        <div id="search-result">
            <asp:GridView ID="SearchResultGrid" runat="server" 
                            OnRowDataBound="OnGridRowDataBound"
                            AutoGenerateColumns="False" 
                            OnPageIndexChanging="OnApplicationPageIndexChanging"
                            OnSorting="SearchResultSorting"
                            AllowSorting="true"
                            AllowPaging="true"
                            PageSize="20"
                            GridLines="Both"
                            BorderWidth="1px" BackColor="white" BorderStyle="None" BorderColor="#e0e0e0" CellPadding="3" CellSpacing="0">
                <FooterStyle ForeColor="White" BackColor="#360101"></FooterStyle>
                <PagerStyle ForeColor="black" HorizontalAlign="Center" Height="45"></PagerStyle>
                <HeaderStyle ForeColor="White" Font-Bold="True" BackColor="#360101"></HeaderStyle>
                <AlternatingRowStyle ForeColor="#360101" BackColor="#fff2f2" Height="30px" BorderColor="#e0e0e0"></AlternatingRowStyle>
                <RowStyle ForeColor="#360101" BackColor="#eeeeee" Height="30px" BorderColor="#e0e0e0"></RowStyle>
                <SelectedRowStyle ForeColor="Black" BackColor="#e6ffe8"></SelectedRowStyle>
                <Columns>
                    <asp:BoundField ReadOnly="True" HeaderText="Status" InsertVisible="False" DataField="Status">
                        <ItemStyle HorizontalAlign="Center" Width="55px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField ReadOnly="True" HeaderText="Date" InsertVisible="False" DataField="EntryDate" SortExpression="EntryDate">
                        <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField ReadOnly="True" HeaderText="Name" InsertVisible="False" DataField="ApplicantName" SortExpression="ApplicantName">
                        <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField ReadOnly="True" HeaderText="Email" InsertVisible="False" DataField="Email">
                        <ItemStyle HorizontalAlign="Left" Width="180px" ></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField ReadOnly="True" HeaderText="Phone" InsertVisible="False" DataField="Phone">
                        <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField ReadOnly="True" HeaderText="District" InsertVisible="False" DataField="District">
                        <ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField ReadOnly="True" HeaderText="School" InsertVisible="False" DataField="School">
                        <ItemStyle HorizontalAlign="Left" Width="105px" ></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField ReadOnly="True" HeaderText="Grade" InsertVisible="False" DataField="Grade">
                        <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField ReadOnly="True" HeaderText="Subject" InsertVisible="False" DataField="Subject">
                        <ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField ReadOnly="True" HeaderText="Resume" InsertVisible="False" DataField="ResumeFile">
                        <ItemStyle HorizontalAlign="Center" Width="55px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField ReadOnly="True" HeaderText="File" InsertVisible="False" DataField="ApplicantFile">
                        <ItemStyle HorizontalAlign="Center" Width="1px"></ItemStyle>
                    </asp:BoundField>
                </Columns>
                <PagerTemplate>
                    <CE:GridPager ID="SearchResultPager" runat="server"
                        ShowFirstAndLast="True"
                        ShowNextAndPrevious="True"
                        PageLinksToShow="10"
                        NextImageUrl="<img runat='server' src='../images/mewa_rightb.gif'/>"
                        PreviousImageUrl="<img runat='server' src='../images/mewa_leftb.gif'/>"
                        FirstImageUrl="<img runat='server' src='../images/mewa_leftPageb.gif'/>"
                        LastImageUrl="<img runat='server' src='../images/mewa_rightPageb.gif'/>"
                        />
                </PagerTemplate>
                <EmptyDataTemplate>
                    Search result: There are currently no CE Tour application available for display.
                </EmptyDataTemplate>
            </asp:GridView>
        </div>

        <asp:Panel ID="ApplicantDetails" runat="server" Visible="false">
            <div id="application-review-modal">
                <div class="review-application-page">
                    <div class="form-title-bar ce-h3">
                        Review Cultural Tour Application for <asp:Label ID="Applicant" runat="server" />
                        <div class="review-exit-button"><img runat="server" src="~/images/close_gray.png" alt="close dialog" onclick="popoffDialog();" /></div>
                    </div>
                    <div id="review-panel">
                        <table>
                            <tr>
                                <td class="form-section-title ce-h4" colspan="2">Review Recommendation</td>
                            </tr>
                            <tr>
                                <td class="form-span-cell600 ce-h5" colspan="2">
                                    <div class="form-cell-text">
                                        <asp:RadioButton ID="ApplyOption" runat="server" CssClass="cell-button" Text=" Apply" GroupName="reveiw" Checked="true" Width="60px" />
                                        <asp:RadioButton ID="WithdrawOption" runat="server" CssClass="cell-button" Text=" Withdraw" GroupName="reveiw" Checked="false" Width="90px" />
                                        <asp:RadioButton ID="ReviewOption" runat="server" CssClass="cell-button" Text=" Review" GroupName="reveiw" Checked="false" Width="70px" />
                                        <asp:RadioButton ID="InterviewOption" runat="server" CssClass="cell-button" Text=" Interview" GroupName="reveiw" Checked="false" Width="80px" />
                                        <asp:RadioButton ID="RejectOption" runat="server" CssClass="cell-button" Text=" Rejected" GroupName="reveiw" Checked="false" Width="80px" />
                                        <asp:RadioButton ID="AwardOption" runat="server" CssClass="cell-button" Text=" Awarded" GroupName="reveiw" Checked="false" Width="80px" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="form-review-cell" colspan="2">
                                    <asp:Button ID="ReviewButton" runat="server" CssClass="review-button" Text="  Make Recommendation  " OnClientClick="reviewDecision('#form-update-modal'); return false;" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="height:20px;"></div>
                    <div id="applicant-data">
                        <div id="article-tab" class="ce-large">
                            <ul>
                                <li class="tab-item" onclick="setArticleTab('#applicant-data', '#article-tab', 0);">Personal Info</li>
                                <li class="tab-item" onclick="setArticleTab('#applicant-data', '#article-tab', 1);">References</li>
                                <li class="tab-item" onclick="setArticleTab('#applicant-data', '#article-tab', 2);">Questionaire 1</li>
                                <li class="tab-item" onclick="setArticleTab('#applicant-data', '#article-tab', 3);">Questionaire 2</li>
                                <li class="tab-item" onclick="setArticleTab('#applicant-data', '#article-tab', 4);">Attached Files</li>
                                <li class="tab-item" onclick="setArticleTab('#applicant-data', '#article-tab', 5);">Review Comments</li>
                            </ul>
                        </div>
                        <div id="tab1" class="tab-articles-item tab-popup show">
                            <table>
                                <tr>
                                    <td class="form-left-cell600 ce-h5"><div style="width:150px;">Applicant File :</div></td>
                                    <td class="form-right-cell600 ce-h5"><asp:TextBox ID="ApplicantXmlFileBox" runat="server" CssClass="ce-h4" Width="470px" ReadOnly="true" /></td>
                                </tr>
                                <tr>
                                    <td class="form-left-cell600 ce-h5"><div style="width:150px;">First Name :</div></td>
                                    <td class="form-right-cell600 ce-h5"><asp:TextBox ID="FirstNameBox" runat="server" CssClass="ce-h4" Width="470px" ReadOnly="true" /></td>
                                </tr>
                                <tr>
                                    <td class="form-left-cell600 ce-h5"><div style="width:150px;">Last Name :</div></td>
                                    <td class="form-right-cell600 ce-h5"><asp:TextBox ID="LastNameBox" runat="server" CssClass="ce-h4" Width="470px" ReadOnly="true" /></td>
                                </tr>
                                <tr>
                                    <td class="form-left-cell600 ce-h5"><div style="width:150px;">Email :</div></td>
                                    <td class="form-right-cell600 ce-h5"><asp:TextBox ID="EmailBox" runat="server" CssClass="ce-h4" Width="470px" ReadOnly="true" /></td>
                                </tr>
                                <tr>
                                    <td class="form-left-cell600 ce-h5"><div style="width:150px;">Daytime Phone :</div></td>
                                    <td class="form-right-cell600 ce-h5"><asp:TextBox ID="PhoneBox" runat="server" CssClass="ce-h4" Width="470px" ReadOnly="true" /></td>
                                </tr>
                                <tr>
                                    <td class="form-left-cell600 ce-h5"><div style="width:150px;">Cell Phone :</div></td>
                                    <td class="form-right-cell600 ce-h5"><asp:TextBox ID="CellPhoneBox" runat="server" CssClass="ce-h4" Width="470px" ReadOnly="true" /></td>
                                </tr>
                                <tr>
                                    <td class="form-left-cell600 ce-h5"><div style="width:150px;">School District :</div></td>
                                    <td class="form-right-cell600 ce-h5"><asp:TextBox ID="DistrictBox" runat="server" CssClass="ce-h4" Width="470px" ReadOnly="true" /></td>
                                </tr>
                                <tr>
                                    <td class="form-left-cell600 ce-h5"><div style="width:150px;">School Name :</div></td>
                                    <td class="form-right-cell600 ce-h5"><asp:TextBox ID="SchoolBox" runat="server" CssClass="ce-h4" Width="470px" ReadOnly="true" /></td>
                                </tr>
                                <tr>
                                    <td class="form-left-cell600 ce-h5"><div style="width:150px;">Grade Taught :</div></td>
                                    <td class="form-right-cell600 ce-h5"><asp:TextBox ID="GradeBox" runat="server" CssClass="ce-h4" Width="470px" ReadOnly="true" /></td>
                                </tr>
                                <tr>
                                    <td class="form-left-cell600 ce-h5"><div style="width:150px;">Subject Taught :</div></td>
                                    <td class="form-right-cell600 ce-h5"><asp:TextBox ID="SubjectBox" runat="server" CssClass="ce-h4" Width="470px" ReadOnly="true" /></td>
                                </tr>
                                <tr>
                                    <td class="form-left-cell600 ce-h5"><div style="width:150px;">Gender :</div></td>
                                    <td class="form-right-cell600 ce-h5">
                                        <asp:RadioButton ID="MaleGender" runat="server" CssClass="cell-button" Text=" Male" GroupName="gender" Checked="true" Width="60px" Enabled="false" />
                                        <asp:RadioButton ID="FemaleGender" runat="server" CssClass="cell-button" Text=" Female" GroupName="gender" Checked="false" Enabled="false" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="tab2" class="tab-articles-item tab-popup hide">
                            <table>
                                    <tr>
                                    <td class="form-span-cell600 ce-h5" colspan="2">
                                        <div class="form-cell-title"><span class="red">*</span> How did you learn about our program?</div>
                                        <div class="form-cell-text">
                                            <asp:TextBox ID="LearnProgramBox" MaxLength="2048" CssClass="ce-h4" Height="160px" Width="800px" TextMode="MultiLine" ReadOnly="true" runat="server" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-span-cell600 ce-h5" colspan="2">
                                        <div class="form-cell-title"><span class="red">*</span> What is your teaching subject and professional specialty?</div>
                                        <div class="form-cell-text">
                                            <asp:TextBox ID="SpecialtyBox" MaxLength="2048" CssClass="ce-h4" Height="160px" Width="800px" TextMode="MultiLine" ReadOnly="true" runat="server" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-span-cell600 ce-h5" colspan="2">
                                        <div class="form-cell-title"><span class="red">*</span> Please list three references (preferably one each from your school’s administration, a student, and a colleague)</div>
                                        <div class="form-cell-text">
                                            <asp:TextBox ID="Reference1Box" MaxLength="1024" CssClass="form-cell-item ce-h4" Width="800px" runat="server" ReadOnly="true" />
                                            <asp:TextBox ID="Reference2Box" MaxLength="1024" CssClass="form-cell-item ce-h4" Width="800px" runat="server" ReadOnly="true" />
                                            <asp:TextBox ID="Reference3Box" MaxLength="1024" CssClass="form-cell-item ce-h4" Width="800px" runat="server" ReadOnly="true" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="tab3" class="tab-articles-item tab-popup hide">
                            <table>
                                <tr>
                                    <td class="form-span-cell600 ce-h5" colspan="2">
                                        <div class="form-cell-title"><span class="red">*</span> How do you relate your current teaching or work experiences to our program?</div>
                                        <div class="form-cell-text">
                                            <asp:TextBox ID="Questionaire1Box" MaxLength="2048" CssClass="ce-h5" Height="200px" Width="800px" TextMode="MultiLine" ReadOnly="true" runat="server" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-span-cell600 ce-h5" colspan="2">
                                        <div class="form-cell-title"><span class="red">*</span> How have you incorporated a prior experience to augment a lesson plan?</div>
                                        <div class="form-cell-text">
                                            <asp:TextBox ID="Questionaire2Box" MaxLength="2048" CssClass="ce-h5" Height="200px" Width="800px" TextMode="MultiLine" ReadOnly="true" runat="server" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="tab4" class="tab-articles-item tab-popup hide">
                            <table>
                                <tr>
                                    <td class="form-span-cell600 ce-h5" colspan="2">
                                        <div class="form-cell-title"><span class="red">*</span> What are your short-term and long-term teaching goals?</div>
                                        <div class="form-cell-text">
                                            <asp:TextBox ID="Questionaire3Box" MaxLength="2048" CssClass="ce-h5" Height="200px" Width="800px" TextMode="MultiLine" ReadOnly="true" runat="server" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-span-cell600 ce-h5" colspan="2">
                                        <div class="form-cell-title"><span class="red">*</span> How do you expect this travel experience to benefit your students and your teaching?</div>
                                        <div class="form-cell-text">
                                            <asp:TextBox ID="Questionaire4Box" MaxLength="2048" CssClass="ce-h5" Height="200px" Width="800px" TextMode="MultiLine" ReadOnly="true" runat="server" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="tab5" class="tab-articles-item tab-popup hide">
                            <div><asp:LinkButton CssClass="form-cell-text" runat="server" OnClick="OnDownloadResume"><img runat="server" src="~/images/pdf_48x48.png" /><span style="position:relative;top:-20px;"> Download Applicant's Resume File</span></asp:LinkButton>
                                <div id="resumeDownloadError"></div>
                            </div>
                            <div><asp:LinkButton CssClass="form-cell-text" runat="server" OnClick="OnDownloadLessonPlan"><img runat="server" src="~/images/pdf_48x48.png" /><span style="position:relative;top:-20px;"> Download Applicant's Lesson Plan Sample File</span></asp:LinkButton>
                                <div id="lessPlanDownloadError"></div>
                            </div>
                            <div id="ApplicantFileDownload1" runat="server"><asp:LinkButton CssClass="form-cell-text" runat="server" OnClick="OnDownloadApplicantFile1"><img runat="server" src="~/images/pdf_48x48.png" /><span style="position:relative;top:-20px;"> Download Applicant's Additional File #1</span></asp:LinkButton></div>
                            <div id="ApplicantFileDownload2" runat="server"><asp:LinkButton CssClass="form-cell-text" runat="server" OnClick="OnDownloadApplicantFile2"><img runat="server" src="~/images/pdf_48x48.png" /><span style="position:relative;top:-20px;"> Download Applicant's Additional File #2</span></asp:LinkButton></div>
                            <div class="form-cell-title" style="padding-top:40px;">Additional applicant comment:</div>
                            <div class="form-cell-text"><asp:TextBox ID="UserComment" MaxLength="4096" CssClass="ce-h4" Height="200px" Width="800px" TextMode="MultiLine" runat="server" ReadOnly="true" /></div>
                        </div>
                        <div id="tab6" class="tab-articles-item tab-popup hide">
                            <div id="committee-roaster">
                                <div id="accordion">
                                    <asp:Repeater ID="MemberList" runat="server" OnItemDataBound="OnCommitteeCommentDataBound" OnItemCommand="OnSaveReviewComment">
                                        <ItemTemplate>
                                            <div class="committee-member"><%# Eval("DisplayName") %>'s comment and scores, last updated: <%# Eval("UpdateDate") %></div>
                                            <div style="min-height:280px;">
                                                <div class="memeber-score-header ce-h5">Reviewer's Comment:</div>
                                                <div><asp:TextBox ID="MemberComment" runat="server" Text='<%# Eval("Comment") %>' MaxLength="4096" TextMode="MultiLine" Height="100px" Width="780px" ></asp:TextBox></div>
                                                <div class="memeber-score-header">Reviewer Scores (0 - 10 for each score category):</div>
                                                <table>
                                                    <tr>
                                                        <td class="member-score-label ce-h5">Application Material:</td>
                                                        <td class="member-score-text">
                                                            <div><asp:TextBox ID="ApplicationScoreBox" runat="server" Text='<%# Eval("ApplicationScore") %>' Width="120px" ></asp:TextBox></div>
                                                        </td>
                                                        <td class="member-score-label ce-h5">Relevancy:</td>
                                                        <td class="member-score-text">
                                                            <div><asp:TextBox ID="RelevancyScoreBox" runat="server" Text='<%# Eval("RelevancyScore") %>' Width="120px" ></asp:TextBox></div>
                                                        </td>
                                                        <td class="member-score-label ce-h5">Lesson Plan:</td>
                                                        <td class="member-score-text">
                                                            <div><asp:TextBox ID="LessonPlanScoreBox" runat="server" Text='<%# Eval("LessonPlanScore") %>' Width="120px" ></asp:TextBox></div>
                                                        </td>
                                                    </tr>
                                                        <td class="member-score-label ce-h5"> </td>
                                                        <td class="member-score-text">
                                                            <div><asp:RangeValidator ID="ApplicationScoreRangeValidator" runat="server" ControlToValidate="ApplicationScoreBox" Type="Integer" MinimumValue="0" MaximumValue="10" ErrorMessage="Range is 0 - 10" ForeColor="Red" Display="Dynamic"></asp:RangeValidator></div>
                                                        </td>
                                                        <td class="member-score-label ce-h5"> </td>
                                                        <td class="member-score-text">
                                                            <div><asp:RangeValidator ID="RelevancyScoreRangeValidator" runat="server" ControlToValidate="RelevancyScoreBox" Type="Integer" MinimumValue="0" MaximumValue="10" ErrorMessage="Range is 0 - 10" ForeColor="Red" Display="Dynamic"></asp:RangeValidator></div>
                                                        </td>
                                                        <td class="member-score-label ce-h5"> </td>
                                                        <td class="member-score-text">
                                                            <div><asp:RangeValidator ID="LessonPlanScoreRangeValidator" runat="server" ControlToValidate="LessonPlanScoreBox" Type="Integer" MinimumValue="0" MaximumValue="10" ErrorMessage="Range is 0 - 10" ForeColor="Red" Display="Dynamic"></asp:RangeValidator></div>
                                                        </td>
                                                    <tr>

                                                    </tr>
                                                </table>
                                                <div class="memeber-review-action"><asp:Button ID="UpdateReviewComment" runat="server" CssClass="review-button" Text=" Save/Update Comment and Scores " CommandName="save" CommandArgument='<%# Eval("Name") %>' UseSubmitBehavior="false" /></div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <div class="hide">
            <asp:HiddenField ID="PostbackParameters" runat="server" Value="" />
            <asp:Button ID="PostbackButton" runat="server" Text="Postback Action" OnClick="OnPostbackAction" />
            <asp:HiddenField ID="ReviewerName" runat="server" Value="" />
            <asp:HiddenField ID="ReviewerDisplayName" runat="server" Value="" />
            <asp:HiddenField ID="DateSortDirection" runat="server" Value="0" />
            <asp:HiddenField ID="NameSortDirection" runat="server" Value="0" />
        </div>
    </div>

    <div style="display: none;">
        <div id="form-update-modal">
            <div class="application-result-title"><span class="ce-h3">Applicant Review Recommendation</span></div>
            <div class="application-result-text ce-h4">
                <table>
                    <tr>
                        <td style="width: 60px;">
                            <img runat="server" src="~/images/colorpen.png" /></td>
                        <td>
                            <div id="decisionText"></div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="application-result-button">
                <asp:Button ID="ReviewAction" OnClientClick="exitDialog();" OnClick="OnApplicantStatusChanged" UseSubmitBehavior="false" CssClass="action-button ce-h4" Text="  Make the Recommendation  " runat="server" />
                <asp:Button ID="CancelAction" OnClientClick="exitDialog();" OnClick="OnRestoreApplicantStatus" CssClass="action-button ce-h4" Text="  Cancel  " runat="server" />
            </div>
        </div>
    </div>

    <div style="display: none;">
        <div id="form-success-modal">
            <div class="application-result-title"><span class="ce-h3">Applicant Status Changed</span></div>
            <div class="application-result-text ce-h4">
                <table>
                    <tr>
                        <td style="width:60px;"><img runat="server" src="~/images/confirm.png" /></td>
                        <td>
                            <div id="success-message">The applicant status has been changed. An automatic email has been sent to the applicant for the change.</div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="application-result-button"><asp:Button ID="Submitted" OnClientClick="exitDialog();$(PostbackButtonID).click();" CssClass="ce-h4" Text="  Close  " runat="server" /></div>
        </div>
    </div>

    <div style="display: none;">
        <div id="form-error-modal">
            <div class="application-result-title"><span class="ce-h3">File Download Problem</span></div>
            <div class="application-result-text ce-h4">
                <table>
                    <tr>
                        <td style="width:60px;"><img runat="server" src="~/images/error.png" /></td>
                        <td>
                            <div id="form-error-message">There is a problem downloading the file '{0}' you are requesting. Please try again. If this persists, please contact site administrator.</div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="application-result-button"><asp:Button ID="Button1" OnClientClick="return restoreReviewDialog(4);" CssClass="ce-h4" Text="  Close  " runat="server" /></div>
        </div>
    </div>

    <div style="display: none;">
        <div id="operation-error-modal">
            <div class="application-result-title ce-h3" id="operation-error-title">Operation Problem</div>
            <div class="application-result-text ce-h4">
                <table>
                    <tr>
                        <td style="width:60px;"><img runat="server" src="~/images/error.png" /></td>
                        <td>
                            <div id="operation-error-message">There is a problem performing function you are requesting. Please try again. If this persists, please contact site administrator.</div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="application-result-button"><asp:Button ID="Button2" OnClientClick="return restoreReviewDialog(0);" CssClass="ce-h4" Text="  Close  " runat="server" /></div>
        </div>
    </div>

    <div>
        <script type="text/javascript">
            var currentTab = 'tab1';
            var ApplyOptionID = '#<%= ApplyOption.ClientID %>';
            var ReviewOptionID = '#<%= ReviewOption.ClientID %>';
            var AwardOptionID = '#<%= AwardOption.ClientID %>';
            var InterviewOptionID = '#<%= InterviewOption.ClientID %>';
            var RejectOptionID = '#<%= RejectOption.ClientID %>';
            var WithdrawOptionID = '#<%= WithdrawOption.ClientID %>';
            var PostbackParametersID = '#<%= PostbackParameters.ClientID %>';
            var PostbackButtonID = '#<%= PostbackButton.ClientID %>';
            var ReviewerNameID = '#<%= ReviewerName.ClientID %>';
            var ReviewerDisplayNameID = '#<%= ReviewerDisplayName.ClientID %>';
            var ApplicantDetailsID = '#<%= ApplicantDetails.ClientID %>';

            // this is called by clicking grid status icon link that trigger 'PostbackButton' button code-behind code
            function loadApplicantData(index, file) {
                $(PostbackParametersID).val(index + ',' + file);
                $(PostbackButtonID).click();
            }

            function showReviewDialog(tab) {
                installAccordion();
                setArticleTab('#applicant-data', '#article-tab', tab);
                modalPopup('#application-review-modal', 900, 900);
            }

            function fileDownloadError(which, filename) {
                //var $errorMessage = $('#resumeDownloadError');
                //if (which == 'lesson plan') $errorMessage = $('#lessonPlanDownloadError');
                //if (filename == '') {
                //    $errorMessage.text("The " + which + " file is not available. Please make sure the applicant has uploaded the file.");
                //}
                //else {
                //    $errorMessage.text("There is a problem downloading " + which + " file '" + filename + "'. If this problem persists, please contact CE administrator.");
                //}

                if (filename == '') {
                    $('#form-error-message').text("The " + which + " file is not available. Please make sure the applicant has uploaded the file.");
                }
                else {
                    var message = $('#form-error-message').text().replace('{0}', filename);
                    $('#form-error-message').text(message);
                }
                modalDialog('#form-error-modal');
            }

            function statusUpdated(message) {
                if (message != '') $('#success-message').text(message);
                $(ApplicantDetailsID).hide();
                modalDialog('#form-success-modal');
            }

            function operationFail(title, message) {
                $('#operation-error-title').text(title);
                $('#operation-error-message').text(message);
                modalDialog('#operation-error-modal');
            }

            function reviewDecision(id) {
                setConfirmationText();
                modalDialog(id);
                $(ApplicantDetailsID).hide();
            }

            function setConfirmationText() {
                var text = '';
                if ($(ApplyOptionID).is(':checked')) {
                    text = 'Apply';
                }
                else if ($(ReviewOptionID).is(':checked')) {
                    text = 'Review';
                }
                else if ($(AwardOptionID).is(':checked')) {
                    text = 'Awarded';
                }
                else if ($(InterviewOptionID).is(':checked')) {
                    text = 'Interview';
                }
                else if ($(RejectOptionID).is(':checked')) {
                    text = 'Rejected';
                }
                else if ($(WithdrawOptionID).is(':checked')) {
                    text = 'Withdraw';
                }

                $('#decisionText').html("You are about to recommend <b>'" + text + "'</b> status for the applicant. Please confirm this recommendation.");
                return true;
            }

            function modalPopup(id, w, h) {
                $.colorbox.close();
                $.colorbox.remove();
                $.colorbox({
                    scrolling: false,
                    inline: true,
                    escKey: false,
                    overlayClose: false,
                    width: w + 'px',
                    height: h + 'px',
                    href: id,
                    transition: "none",
                    opacity: 0.3,
                    onLoad: function () { $('#cboxClose').remove(); }
                });
            }

            function popoffDialog() {
                $(ApplicantDetailsID).hide();
                exitDialog();
            }

            function restoreReviewDialog(tab) {
                if (tab < 0) tab = 0;
                $.colorbox.close();
                $.colorbox.remove();
                showReviewDialog(tab);
                return true;
            }

            function restoreStatusDialog() {
                $.colorbox.close();
                $.colorbox.remove();
                showReviewDialog(0);
            }

            function installAccordion() {
                var index = 0;
                var foundIndex = 0;
                $('.committee-member').each(function () {
                    if ($(this).text().startsWith($(ReviewerDisplayNameID).val())) {
                        foundIndex = index;
                    }
                    index++;
                });

                $("#accordion").accordion({
                    collapsible: true,
                    active: foundIndex
                });
            }

            function reviewerCommentDone() {
                restoreReviewDialog(5);
                alert('comment and scores are saved/updated.');
            }

            $(document).ready(function () {
                // these calls are required to allow postback from asp.net controls inside colorbox
                $('#colorbox').appendTo('form');
                $('#cboxOverlay, #colorbox').appendTo('form');
            });
        </script>
    </div>
</asp:Content>
