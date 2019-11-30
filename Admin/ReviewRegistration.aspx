<%@ Page Language="C#" MasterPageFile="~/CEMain.Master" AutoEventWireup="true" CodeBehind="ReviewRegistration.aspx.cs" Inherits="CE.Pages.ReviewRegistrationPage " %>

<%@ Register TagPrefix="CE" Namespace="CE.Pages" Assembly="CE.Application" %>
<%@ Register TagPrefix="CE" Namespace="CE.Data" Assembly="CE.Application" %>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="PlaceHolderScript" runat="server">
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/cearticle.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/themes/maroon/cepage.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/ceadmin.css")%>" media="all" />
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/jquery/jquery.colorbox-min.js")%>"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="ce-admin-content" style="padding-top:1px !important;">
        <div id="search-box">
            <table>
                <tr>
                    <td>
                        <div class="search-item-label">&nbsp;Registered type</div>
                        <div class="search-item-control">
                            <asp:RadioButton ID="TeamCompetition" runat="server" AutoPostBack="true" OnCheckedChanged="OnTeamCompetition" CssClass="cell-button" Text=" Team" GroupName="competitionType" Checked="false" Width="60px" />
                            <asp:RadioButton ID="IndivisualCompetition" runat="server" AutoPostBack="true" OnCheckedChanged="OnIndividualCompetition" CssClass="cell-button" Text=" Individual" GroupName="competitionType" Checked="false" Width="80px" />
                        </div>
                    </td>
                    <td>
                        <div class="search-item-label">Category</div>
                        <div class="search-item-control">
                            <asp:DropDownList ID="CategoryList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnCategorySelectionChanged" Width="200px"></asp:DropDownList>
                        </div>
                    </td>
                    <td>
                        <div class="search-item-label">Division</div>
                        <div class="search-item-control">
                            <asp:DropDownList ID="DivisionList" runat="server" Width="210px"></asp:DropDownList>
                        </div>
                    </td>
                    <td>
                        <div class="search-item-label">Class</div>
                        <div class="search-item-control">
                            <asp:DropDownList ID="ClassList" runat="server" Width="100px"></asp:DropDownList>
                        </div>
                    </td>
                    <td>
                        <div class="search-item-label">Status</div>
                        <div class="search-item-control">
                            <asp:DropDownList ID="StatusList" runat="server" Width="80px">
                                <asp:ListItem Text="All" Value="" />
                                <asp:ListItem Text="Apply" Value="Apply" />
                                <asp:ListItem Text="Pending" Value="Pending" />
                                <asp:ListItem Text="Review" Value="Review" />
                                <asp:ListItem Text="Cancel" Value="Rejected" />
                                <asp:ListItem Text="Confirm" Value="Approved" />
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
                            OnRowCreated="OnCreatePagerRow"
                            OnPageIndexChanging="OnApplicationPageIndexChanging"
                            OnSorting="SearchResultSorting"
                            AllowSorting="true"
                            AllowPaging="true"
                            PageSize="20"
                            GridLines="Both"
                            BorderWidth="1px" BackColor="white" BorderStyle="None" BorderColor="#e0e0e0" CellPadding="3" CellSpacing="0">
                <FooterStyle ForeColor="White" BackColor="#360101"></FooterStyle>
                <PagerStyle ForeColor="Black" HorizontalAlign="Center" Height="45"></PagerStyle>
                <HeaderStyle ForeColor="White" Font-Bold="False" Font-Size="12px" BackColor="#360101"></HeaderStyle>
                <SortedAscendingHeaderStyle ForeColor="White" CssClass="ce-grid-header-asc" />
                <SortedDescendingHeaderStyle ForeColor="White" CssClass="ce-grid-header-desc" />
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
                    <asp:BoundField ReadOnly="True" HeaderText="Contact" InsertVisible="False" DataField="ContactName" SortExpression="ContactName">
                        <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField ReadOnly="True" HeaderText="Email" InsertVisible="False" DataField="ContactEmail">
                        <ItemStyle HorizontalAlign="Center" Width="180px" ></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField ReadOnly="True" HeaderText="Category" InsertVisible="False" DataField="Category">
                        <ItemStyle HorizontalAlign="Center" Width="110px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField ReadOnly="True" HeaderText="SubCategory" InsertVisible="False" DataField="SubCategory">
                        <ItemStyle HorizontalAlign="Center" Width="110px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField ReadOnly="True" HeaderText="Division" InsertVisible="False" DataField="Division" SortExpression="Division">
                        <ItemStyle HorizontalAlign="Center" Width="110px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField ReadOnly="True" HeaderText="Team/Contestant" InsertVisible="False" DataField="TeamName">
                        <ItemStyle HorizontalAlign="Center" Width="140px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField ReadOnly="True" HeaderText="#" InsertVisible="False" DataField="ContestantCount">
                        <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField ReadOnly="True" HeaderText="Method" InsertVisible="False" DataField="PaymentMethod">
                        <ItemStyle HorizontalAlign="Center" Width="1px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField ReadOnly="True" HeaderText="$" InsertVisible="False" DataField="Payment">
                        <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField ReadOnly="True" HeaderText="Waive" InsertVisible="False" DataField="LunchCount">
                        <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField ReadOnly="True" HeaderText="File" InsertVisible="False" DataField="File">
                        <ItemStyle HorizontalAlign="Center" Width="1px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField ReadOnly="True" HeaderText="Student" InsertVisible="False" DataField="Student">
                        <ItemStyle HorizontalAlign="Center" Width="1px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField ReadOnly="True" HeaderText="Class" InsertVisible="False" DataField="Class">
                        <ItemStyle HorizontalAlign="Center" Width="1px"></ItemStyle>
                    </asp:BoundField>
                </Columns>
                <PagerTemplate>
                    <CE:GridPager ID="SearchResultPager" runat="server"
                        ShowFirstAndLast="True"
                        ShowNextAndPrevious="True"
                        PageLinksToShow="10"
                        NextImageUrl="<img runat='server' src='~/images/mewa_rightb.gif'/>"
                        PreviousImageUrl="<img runat='server' src='~/images/mewa_leftb.gif'/>"
                        FirstImageUrl="<img runat='server' src='~/images/mewa_leftPageb.gif'/>"
                        LastImageUrl="<img runat='server' src='~/images/mewa_rightPageb.gif'/>"
                        />
                </PagerTemplate>
                <EmptyDataTemplate>
                    Search result: There are currently no Talent Competition application available for display.
                </EmptyDataTemplate>
            </asp:GridView>
        </div>

        <div style="display:none;">
            <div id="application-review-modal">
                <div class="review-application-page">
                    <div class="review-title-bar ce-h3">
                        <div>Review Talent Competition Registration for <asp:Label ID="Applicant" runat="server" /></div>
                        <div class="review-exit-button"><img runat="server" src="~/images/close_gray.png" alt="close dialog" onclick="exitDialog();" /></div>
                    </div>
                    <div id="review-panel">
                        <table>
                            <tr>
                                <td class="form-section-title ce-h4" colspan="2">Review Registration</td>
                            </tr>
                            <tr>
                                <td class="form-span-cell600 ce-h5">
                                    <div class="form-cell-text" style="float:left;">
                                        <asp:RadioButton ID="PendingOption" runat="server" CssClass="cell-button" Text=" Pending Paypal" GroupName="reveiw" Checked="false" Width="130px" />
                                        <asp:RadioButton ID="ApplyOption" runat="server" CssClass="cell-button" Text=" Apply" GroupName="reveiw" Checked="true" Width="70px" />
                                        <asp:RadioButton ID="ReminderOption" runat="server" CssClass="cell-button" Text=" Payment Reminder" GroupName="reveiw" Checked="false" Width="150px" />
                                        <asp:RadioButton ID="ReviewOption" runat="server" CssClass="cell-button" Text=" Review" GroupName="reveiw" Checked="false" Width="80px" />
                                        <asp:RadioButton ID="RejectOption" runat="server" CssClass="cell-button" Text=" Cancel" GroupName="reveiw" Checked="false" Width="80px" />
                                        <asp:RadioButton ID="ApprovedOption" runat="server" CssClass="cell-button" Text=" Confirm" GroupName="reveiw" Checked="false" Width="90px" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="form-span-cell600 ce-h5">
                                    <div class="form-cell-text" style="float:left;">
                                        <asp:RadioButton ID="FirstPlace" runat="server" CssClass="cell-button" Text=" First Place" GroupName="trophy" Width="110px" />
                                        <asp:RadioButton ID="SecondPlace" runat="server" CssClass="cell-button" Text=" Second Place" GroupName="trophy" Width="120px" />
                                        <asp:RadioButton ID="ThirdPlace" runat="server" CssClass="cell-button" Text=" Third Place" GroupName="trophy" Width="110px" />
                                        <asp:RadioButton ID="HonorableMentioned" runat="server" CssClass="cell-button" Text=" Honorable Mentioned" GroupName="trophy"  Width="180px" />
                                        <asp:RadioButton ID="RemoveTrophy" runat="server" CssClass="cell-button" Text=" Remove" GroupName="trophy" Width="80px" />
                                    </div>
<%--                                    <div style="float:right;"><asp:Button ID="SetResult" CssClass="review-button" Text="  Save Competition Result  " OnClick="OnAwardTrophy" UseSubmitBehavior="false" runat="server" /></div>--%>
                                </td>
                            </tr>
                            <tr>
                                <td class="form-review-cell">
                                    <div><asp:Button ID="ReviewButton" runat="server" CssClass="review-button" Text="  Save Settings  " OnClientClick="setRegistrationStatus('#form-update-modal'); return false;" /></div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="form-application-data">
                        <div id="applicationPage">
                            <div style="float:right;margin-right:10px;"><asp:Label ID="EntryDateBox" runat="server" CssClass="ce-h4" /></div>
                            <div class="reg-section-title ce-h4" style="padding-top:20px !important;">Contact Information</div>
                            <div class="review-item ce-h5">
                                <span>Name: </span><asp:TextBox ID="ContactNameBox" runat="server" CssClass="ce-h4" Width="150px" ReadOnly="true" />
                                <span>Email: </span><asp:TextBox ID="ContactEmailBox" runat="server" CssClass="ce-h4" Width="200px" ReadOnly="true" />
                                <span>Phone: </span><asp:TextBox ID="ContactPhoneBox" runat="server" CssClass="ce-h4" Width="150px" ReadOnly="true" />
                            </div>

                            <div class="review-section-title ce-h4">Competition Information <asp:Label ID="CompetitionType" runat="server" Text="" /></div>
                            <div class="review-item ce-h5">
                                <span>Category: </span><asp:TextBox ID="CompetitionCategory" runat="server" CssClass="ce-h4" Width="200px" ReadOnly="true" />
                                <span>Division: </span><asp:TextBox ID="CompetitionDivision" runat="server" CssClass="ce-h4" Width="200px" ReadOnly="true" />
                                <span>Class: </span><asp:TextBox ID="CompetitionClass" runat="server" CssClass="ce-h4" Width="150px" ReadOnly="true" />
                            </div>

                            <div class="review-section-title ce-h4">Award: <asp:Label ID="Award" runat="server" Text="" />

                            </div>

                            <div class="review-section-title ce-h4">Payment Information</div>
                            <div class="review-item ce-h5" style="padding-bottom:20px;">
                                <div class="payment-method"><span>Payment method: </span><asp:TextBox ID="PaymentMethodBox" runat="server" Text="" BorderStyle="None" Width="700px" Font-Bold="true" /></div>
                                <div id="MailInPayment" runat="server">
                                    <div class="mailin-item">
                                        <asp:CheckBox ID="MailInCheckBox" runat="server" Checked="false" Text=" Main-In Payment" />&nbsp;&nbsp;
                                        $ <asp:TextBox ID="MailInBox" runat="server" Text="" Width="80px" />&nbsp;&nbsp;&nbsp;
                                        Check #: <asp:TextBox ID="CheckNumberBox" runat="server" Text="" Width="150px" />
                                    </div>
                                </div>
                                <div id="PaypalPaymentCheck" runat="server">
                                    <div class="mailin-item">
                                        <asp:CheckBox ID="PaypalCheckBox" runat="server" Checked="false" Text=" Paypal payment received" />&nbsp;&nbsp;
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="review-section-title ce-h4">Contestants Information</div>
                        <div id="reg-participant-table">
                            <table>
                                <tr>
                                    <td class="reg-participant-cell header ce-h5" style="width: 90px;">Name</td>
                                    <td class="reg-participant-cell header ce-h5" style="width: 110px;">Chinese Name</td>
                                    <td class="reg-participant-cell header ce-h5" style="width: 70px;">Birthday</td>
                                    <td class="reg-participant-cell header ce-h5" style="width: 170px;">Email</td>
                                    <td class="reg-participant-cell header ce-h5" style="width: 140px;">Academic School</td>
                                    <td class="reg-participant-cell header ce-h5" style="width: 120px;">Extra School</td>
                                    <td class="reg-participant-cell header ce-h5" style="width: 50px;">Grade</td>
                                    <td class="reg-participant-cell header ce-h5" style="width: 60px;">Lunch?</td>
                                </tr>
                                <asp:Repeater ID="TalentParticipants" runat="server">
                                    <ItemTemplate>
                                            <tr id='reg-participant-row-<%# Eval("Row") %>' class="reg-participant-row">
                                            <td class="reg-participant-cell">
                                                <asp:TextBox ID="LastName" runat="server" Style="width: 90px;" Text='<%# string.Format("{0} {1}",Eval("FirstName "),Eval("LastName"))%>' BorderStyle="None" ReadOnly="true" /></td>
                                            <td class="reg-participant-cell">
                                                <asp:TextBox ID="FirstName" runat="server" Style="width: 110px;" Text='<%# Eval("ChineseName") %>' BorderStyle="None" ReadOnly="true" /></td>
                                            <td class="reg-participant-cell">
                                                <asp:TextBox ID="Birthday" runat="server" Style="width: 70px;" Text='<%# Eval("Birthday") %>' BorderStyle="None" ReadOnly="true" /></td>
                                            <td class="reg-participant-cell">
                                                <asp:TextBox ID="Email" runat="server" Style="width: 170px;" Text='<%# Eval("Email") %>' BorderStyle="None" ReadOnly="true" /></td>
                                            <td class="reg-participant-cell">
                                                <asp:TextBox ID="School1" runat="server" Style="width: 140px;" Text='<%# Eval("School") %>' BorderStyle="None" ReadOnly="true" /></td>
                                            <td class="reg-participant-cell">
                                                <asp:TextBox ID="School2" runat="server" Style="width: 120px;" Text='<%# Eval("School2") %>' BorderStyle="None" ReadOnly="true" /></td>
                                            <td class="reg-participant-cell">
                                                <asp:TextBox ID="Grade" runat="server" Style="width: 50px;" Text='<%# Eval("Grade") %>' BorderStyle="None" ReadOnly="true" /></td>
                                            <td class="reg-participant-cell">
                                                <asp:TextBox ID="LunchProgram" runat="server" Style="width: 60px;" Text='<%# Eval("LunchProgram") %>' BorderStyle="None" ReadOnly="true" /></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="hide">
            <asp:HiddenField ID="PostbackParameters" runat="server" Value="" />
            <asp:Button ID="PostbackButton" runat="server" Text="Postback Action" OnClick="OnPostbackAction" />
            <asp:HiddenField ID="DateSortDirection" runat="server" Value="0" />
            <asp:HiddenField ID="ContactSortDirection" runat="server" Value="0" />
             <asp:HiddenField ID="DivisionSortDirection" runat="server" Value="0" />
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
                <asp:Button ID="ReviewAction" OnClientClick="exitDialog();" OnClick="OnStatusChanged" UseSubmitBehavior="false" CssClass="action-button ce-h4" Text="  Make the Change  " runat="server" />
                <asp:Button ID="CancelAction" OnClientClick="exitDialog();" CssClass="action-button ce-h4" Text="  Cancel  " runat="server" />
            </div>
        </div>
    </div>

    <div style="display: none;">
        <div id="form-success-modal">
            <div class="application-result-title"><span class="ce-h3">Registration Status Changed</span></div>
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
            <div class="application-result-button"><asp:Button ID="Submitted" OnClientClick="return exitDialog();" CssClass="ce-h4" Text="  Close  " runat="server" /></div>
        </div>
    </div>

    <div style="display: none;">
        <div id="form-error-modal">
            <div class="application-result-title ce-h3" id="error-title">Registration Review Problem</div>
            <div class="application-result-text ce-h4">
                <table>
                    <tr>
                        <td style="width:60px;"><img runat="server" src="~/images/error.png" /></td>
                        <td>
                            <div id="error-message">There is problem processing the registration entry. Please try again. If the problem persists, please contact site administrator.</div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="application-result-button"><asp:Button ID="OperationFail" OnClientClick="return exitDialog();" CssClass="ce-h4" Text="  Close  " runat="server" /></div>
        </div>
    </div>

    <div>
        <script type="text/javascript">
            var ApplyOptionID = '#<%= ApplyOption.ClientID %>';
            var ReviewOptionID = '#<%= ReviewOption.ClientID %>';
            var ApprovedOptionID = '#<%= ApprovedOption.ClientID %>';
            var RejectOptionID = '#<%= RejectOption.ClientID %>';
            var PendingOptionID = '#<%= PendingOption.ClientID %>';
            var ReminderOptionID = '#<%= ReminderOption.ClientID %>';
            var PostbackParametersID = '#<%= PostbackParameters.ClientID %>';
            var PostbackButtonID = '#<%= PostbackButton.ClientID %>';
            var MailInPaymentID = '#<%= MailInPayment.ClientID %>';

            function loadApplicantData(index, file) {
                $(PostbackParametersID).val(index + ',' + file);
                $(PostbackButtonID).click();
            }

            function showReviewDialog() {
                modalPopup('#application-review-modal', 900, 1100);
            }

            function statusUpdated(message) {
                if (message != '') $('#success-message').text(message);
                modalDialog('#form-success-modal');
            }

            function operationFail(title, message) {
                $('#error-title').text(title);
                $('#error-message').text(message);
                modalDialog('#form-error-modal');
            }

            function setRegistrationStatus(id) {
                setConfirmationText();
                modalDialog(id);
            }

            function setConfirmationText() {
                var text = '';
                if ($(ApplyOptionID).is(':checked')) {
                    text = 'Apply';
                }
                else if ($(ReviewOptionID).is(':checked')) {
                    text = 'Review';
                }
                else if ($(ApprovedOptionID).is(':checked')) {
                    text = 'Approved';
                }
                else if ($(RejectOptionID).is(':checked')) {
                    text = 'Rejected';
                }
                else if ($(PendingOptionID).is(':checked')) {
                    text = 'Pending Paypal';
                }
                else if ($(ReminderOptionID).is(':checked')) {
                    text = 'Payment Reminder';
                }

                $('#decisionText').html("You are about to change the applicant registration settings. Please confirm this change.");
            }

            function reviewApplicant(file) {
                alert('file = ' + file);
            }

            function allowExport() {
                alert('Excel export is still under development.');
                return false;
            }

            function modalPopup(id, w, h) {
                $.colorbox({
                    scrolling: false,
                    inline: true,
                    escKey: false,
                    overlayClose: false,
                    width: w + 'px',
                    maxHeight: h + 'px',
                    href: id,
                    transition: 'none',
                    opacity: 0.3,
                    outline: 0,
                    onLoad: function () { $('#cboxClose').remove(); }
                });
            }

            $(document).ready(function () {
            });
        </script>
    </div>
</asp:Content>
