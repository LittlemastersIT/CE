<%@ Page Language="C#" MasterPageFile="~/CEMain.Master" AutoEventWireup="true" validateRequest="false" CodeBehind="TeamRegistration.aspx.cs" Inherits="CE.Pages.TeamRegistrationPage" %>
<%@ Register TagPrefix="CE" Namespace="CE.Pages" Assembly="CE.Application" %>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="PlaceHolderScript" runat="server">
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/jquery-ui-1.10.3.custom.min.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/ceadmin.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/themes/maroon/cepage.css")%>" media="all" />
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/jquery/jquery.colorbox-min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/jquery/jquery-ui-1.10.3.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/jquery/jquery.inputmask.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/cecookie.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/cejson.js")%>"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="ce-admin-content">
        <div class="ce-registration-page">
            <div class="page-title ce-h2" style="display: inline;">Team Competitions Registration</div>
            <div class="form-input-note"><span class="red">*</span> denotes required input</div>
            <div class="reg-section-divider"></div>
            <div id="ApplicationPage1" class="reg-app-page show">
                <asp:UpdatePanel ID="Page1UpdatePanel" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td class="reg-section-title ce-h4" colspan="6">Contact Information</td>
                            </tr>
                            <tr>
                                <td class="ce-h6" style="padding-left:5px;padding-bottom:20px" colspan="6">
                                    <b>Note</b>: <i>The contact information below will be used by the Competition Administrator to send information (such as confirmation) 
                                    regarding the competition to the team. The contact person can be a parent, a teacher, a coach, or one of the representing members of the team.</i>
                                </td>
                            </tr>
                            <tr>
                                <td class="reg-left-cell1 ce-h5"><div><span class="red">*</span> Name :</div></td>
                                <td class="reg-right-cell1 ce-h5"><asp:TextBox ID="ContactNameBox" runat="server" CssClass="ce-h4" Width="180px" /></td>
                                <td class="reg-left-cell1 ce-h5"><div><span class="red">*</span> Email :</div></td>
                                <td class="reg-right-cell1 ce-h5"><asp:TextBox ID="ContactEmailBox" runat="server" CssClass="ce-h4" TextMode="Email"  Width="220px" /></td>
                                <td class="reg-left-cell1 ce-h5"><div><span class="red">*</span> Phone :</div></td>
                                <td class="reg-right-cell1 ce-h5"><asp:TextBox ID="ContactPhoneBox" runat="server" CssClass="ce-h4" Width="140px" /></td>
                            </tr>
                            <tr id="pageErrorRow1" class="hide">
                                <td id="page1ErrorMessage" class="form-message red" colspan="6">
                                    <asp:Literal ID="Page1ErrorText" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <div class="reg-input-divider"></div>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td class="reg-section-title ce-h4" colspan="2">Competition Information</td>
                            </tr>
                            <tr id="TeamNameRow" runat="server">
                                <td class="reg-left-cell ce-h5">
                                    <div><span class="red">*</span> Team Name :</div>
                                </td>
                                <td class="reg-right-cell ce-h5">
                                    <asp:TextBox ID="TeamNameBox" runat="server" CssClass="ce-h4" Width="300px" ToolTip="Team name can contain alphabets, digits or _ character" /></td>
                            </tr>
                            <tr>
                                <td class="reg-left-cell ce-h5">
                                    <div><span class="red">*</span> Competition Category :</div>
                                </td>
                                <td class="reg-right-cell ce-h5">
                                    <asp:DropDownList ID="CompetitionCategoryList" runat="server" Width="305px" AutoPostBack="true" CssClass="ce-ddbox ce-margin8" OnSelectedIndexChanged="OnCategorySelectionChanged">
                                    </asp:DropDownList>
                                    <div id="talentShowSubCategoryButtons" style="padding:10px 0px 0px 5px;"></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="reg-left-cell ce-h5">
                                    <div><span class="red">*</span> Competition Division :</div>
                                </td>
                                <td class="reg-right-cell ce-h5">
                                    <asp:DropDownList ID="CompetitionDivisionList" runat="server" Width="305px" AutoPostBack="true" CssClass="ce-ddbox" OnSelectedIndexChanged="OnDivisionSelectionChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="pageErrorRow2" class="hide">
                                <td id="page2ErrorMessage" class="form-message red" colspan="2">
                                    <asp:Literal ID="Page2ErrorText" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                            <tr></tr>
                             <tr>
                                 <td class="ce-h6" style="padding-left:5px;padding-bottom:20px" colspan="2">
                                    Note: <i>For Chinese Singing or Talent Show Competition. If music is required, The participants can bring their own musical instruments (a piano will be available on the stage for use), or upload your music file before the March 2, 2020 registration deadline.
                                    For Talent show, please upload music to <a href="http://www.littlemastersclub.org/music/talentshow">http://www.littlemastersclub.org/music/talentshow </a>.  
                                    And for Chinese Singing, please upload music to  <a href="http://www.littlemastersclub.org/music/Singing">http://www.littlemastersclub.org/music/Singing </i>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div class="reg-section-divider" style="background-color:#8C6E70;height:4px;"></div>
                                </td>
                            </tr>
                        </table>
                        <div>
                            <asp:HiddenField ID="MinimumContestantProxy" runat="server" Value="1" />
                            <asp:HiddenField ID="MaximumContestantProxy" runat="server" Value="0" />
                        </div>
                        <div id="reg-contestant-entry">
                            <table>
                                <tr>
                                    <td class="reg-section-title reg-competition-type ce-h4" colspan="4">Contestant Information&nbsp;:&nbsp;<span style="color:#8C6E70;" class="ce-h5"><i><b> (Every member of the team must all be listed in this registration <u>ACCURATELY</u>)</b></i></span></td>
                                </tr>
                                <tr>
                                    <td class="ce-h6" style="padding-left:5px;padding-bottom:20px" colspan="4">
                                        <b>Note</b>: <i>Please fill out the following information and select "Add Contestant" to enter an entry to "Added Contestant List".
                                                 To remove a contestant from "Added Contestant List", please enter "First Name", "Last Name", and "Birthday" and then select "Remove Contestant".</i>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <div class="reg-left-cell1 ce-h5" style="display:inline-block"><div style="width: 95px;"><span class="red">*</span> First Name :</div></div>
                                        <div class="reg-left-cell1 ce-h5" style="display:inline-block"><asp:TextBox ID="ContestantFirstName" runat="server" CssClass="ce-h5" Width="200px" /></div>
                                        <div class="reg-left-cel11 ce-h5" style="display:inline-block"><div style="padding-left:10px;width:90px;"><span class="red">*</span> Last Name :</div></div>
                                        <div class="reg-left-cell1 ce-h5" style="display:inline-block"><asp:TextBox ID="ContestantLastName" runat="server" CssClass="ce-h4" Width="180px" /></div>
                                        <div class="reg-left-cell1 ce-h5" style="display:inline-block"><div style="padding-left:20px;width:105px;"> Chinese Name :</div></div>
                                        <div class="reg-left-cell1 ce-h5" style="display:inline-block"><asp:TextBox ID="ContestantChineseName" runat="server" CssClass="ce-h4" Width="102px" /></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="reg-left-cell1 ce-h5"><div style="width: 95px;"><span class="red">*</span> Birthday :</div></td>
                                    <td class="reg-right-cell1 ce-h5"><asp:TextBox ID="ContestantBirthday" runat="server" CssClass="ce-h5" Width="175px" ToolTip="Date format is mm/dd/yyyy" ReadOnly/></td>
                                    <td class="reg-left-cell2 ce-h5"><div style="width: 160px;"><span class="red">*</span> Academic School :</div></td>
                                    <td class="reg-right-cell2 ce-h5"><asp:TextBox ID="ContestantSchool1" runat="server" CssClass="ce-h5" Width="340px" /></td>
                                </tr>
                                <tr>
                                    <td class="reg-left-cell1 ce-h5"><div style="width: 95px;"><span class="red">*</span> Email :</div></td>
                                    <td class="reg-right-cell1 ce-h5"><asp:TextBox ID="ContestantEmail" runat="server" CssClass="ce-h4" Width="200px" TextMode="Email" /></td>
                                    <td class="reg-left-cell2 ce-h5"><div style="width: 160px;">Extracurricular School :</div></td>
                                    <td class="reg-right-cell2 ce-h5"><asp:TextBox ID="ContestantSchool2" runat="server" CssClass="ce-h5" Width="340px" /></td>
                                </tr>
                                <tr>
                                    <td class="reg-left-cell1 ce-h5"><div style="width:95px;"><span class="red">*</span> Grade :</div></td>
                                    <td class="reg-right-cell1 ce-h5">
                                        <asp:DropDownList ID="ContestantGradeList" runat="server" CssClass="ce-h4" Width="205px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="reg-left-cell1 ce-h5" colspan="2" style="padding-top:10px;padding-left:10px;">Do you participate in Free and Reduced-Price Meal Program?&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="YesLunch" Text="  Yes" runat="server" GroupName="SchoolLunch" />&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="NoLunch" Text="  No" runat="server" GroupName="SchoolLunch" Checked="true" /><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="reg-left-cell1 ce-h5" colspan="2"></td>
                                    <td class="reg-left-cell1 ce-h6" colspan="2" style="padding-top:10px;padding-left:10px;">
                                        <b>Note</b>: <i>If the student participates in Free and Reduced-Price Meal Program he/she is eligible for scholarships to cover 
                                                    their entrance fee to the competition. If you select Yes, please download the Fee Waiver form from Schedule and Guidelines page to apply.</i>
                                    </td>
                                </tr>
                                <tr id="pageErrorRow3" class="hide">
                                    <td id="page3ErrorMessage" class="form-message red" colspan="4">
                                        <asp:Literal ID="Page3ErrorText" runat="server" EnableViewState="False"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <div class="reg-section-divider"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="reg-button-cell" colspan="4">
                                        <asp:LinkButton ID="AddButton" runat="server" CssClass="action-button" OnClientClick="return addContestant();">
                                            <img runat="server" src="~/images/edit_add.png" alt="Add Contestant" />&nbsp;&nbsp;<span>Add Contestant</span>
                                        </asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton ID="RemoveButton" runat="server" CssClass="action-button" OnClientClick="return removeContestant();">
                                            <img runat="server" src="~/images/erase.png" alt="Remove Contestant" />&nbsp;&nbsp;<span>Remove Contestant</span>
                                        </asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
        <%--                                <asp:LinkButton ID="Page2ClearButton" runat="server" CssClass="action-button" OnClientClick="return clearContestantInput();">
                                            <img runat="server" src="~/images/eraser_big.png" alt="Erase Contestant" />&nbsp;&nbsp;<span>Clear Contestant</span>
                                        </asp:LinkButton>--%>
                                    </td>
                                </tr>
                            </table>
                        </div> 
                        <div class="reg-constant-list ce-h4">Added Contestant List : <asp:Label ID="MinimumContestants" runat="server"></asp:Label></div>
                        <div id="reg-participant-table">
                            <table>
                                <tr>
                                    <td class="reg-participant-cell header ce-h5" style="width: 90px;">Last Name</td>
                                    <td class="reg-participant-cell header ce-h5" style="width: 90px;">First Name</td>
                                    <td class="reg-participant-cell header ce-h5" style="width: 80px;">Birthday</td>
                                    <td class="reg-participant-cell header ce-h5" style="width: 170px;">Email</td>
                                    <td class="reg-participant-cell header ce-h5" style="width: 150px;">Academic School</td>
                                    <td class="reg-participant-cell header ce-h5" style="width: 150px;">Extra School</td>
                                    <td class="reg-participant-cell header ce-h5" style="width: 50px;">Grade</td>
                                    <td class="reg-participant-cell header ce-h5" style="width: 55px;">Lunch?</td>
                                    <td class="reg-participant-cell header ce-h5 hide" style="width: 1px;">ID</td>
                                </tr>
                                <asp:Repeater ID="ContestantList" runat="server">
                                    <ItemTemplate>
                                        <tr id='reg-participant-row-<%# Eval("Row") %>' class="reg-participant-row hide">
                                            <td class="reg-participant-cell">
                                                <asp:TextBox ID="LastName" runat="server" Style="width: 90px;" Text='<%# Eval("LastName") %>' BorderStyle="None" /></td>
                                            <td class="reg-participant-cell">
                                                <asp:TextBox ID="FirstName" runat="server" Style="width: 90px;" Text='<%# Eval("FirstName") %>' BorderStyle="None" /></td>
                                            <td class="reg-participant-cell">
                                                <asp:TextBox ID="Birthday" runat="server" Style="width: 80px;" Text='<%# Eval("Birthday") %>' BorderStyle="None" /></td>
                                            <td class="reg-participant-cell">
                                                <asp:TextBox ID="Email" runat="server" Style="width: 170px;" Text='<%# Eval("Email") %>' BorderStyle="None" /></td>
                                            <td class="reg-participant-cell">
                                                <asp:TextBox ID="School1" runat="server" Style="width: 150px;" Text='<%# Eval("School") %>' BorderStyle="None" /></td>
                                            <td class="reg-participant-cell">
                                                <asp:TextBox ID="School2" runat="server" Style="width: 150px;" Text='<%# Eval("School2") %>' BorderStyle="None" /></td>
                                            <td class="reg-participant-cell">
                                                <asp:TextBox ID="Grade" runat="server" Style="width: 50px;" Text='<%# Eval("Grade") %>' BorderStyle="None" /></td>
                                            <td class="reg-participant-cell">
                                                <asp:TextBox ID="LunchProgram" runat="server" Style="width: 55px;" Text='<%# Eval("LunchProgram") %>' BorderStyle="None" /></td>
                                            <td class="reg-participant-cell hide">
                                                <asp:TextBox ID="ChineseName" runat="server" Style="width: 1px;" Text='<%# Eval("ChineseName") %>' BorderStyle="None" /></td>
                                            <td class="reg-participant-cell hide">
                                                <asp:TextBox ID="ID" runat="server" Style="width: 1px;" Text='<%# Eval("ID") %>' BorderStyle="None" /></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <table id="reg-action-table">
                    <tr>
                        <td class="reg-button-cell">
                            <asp:Button ID="PaymentButton" runat="server" CssClass="action-button" Font-Bold="true" Text="  Next  >>  " OnClientClick="javascript: if(validateInput()) return gotoPage(2); else return false;" />
                            <asp:Button ID="Page2ClearAllButton" runat="server" CssClass="action-button" Text="  Clear Added Contestants  " OnClientClick="return clearAddedContestants();" />
                            <asp:Button ID="SaveDataButton" runat="server" CssClass="action-button" Font-Bold="true" Text="  Save Form Data  " OnClientClick="javascript: return saveFormData();" />
                        </td>
                    </tr>
                </table>
            </div>

            <div id="ApplicationPage2" class="reg-app-page hide">
                <table>
                    <tr>
                        <td class="reg-section-title ce-h4" colspan="2">Registration Payment</td>
                    </tr>
                    <tr>
                        <td class="reg-cell-text ce-h4" colspan="2">
                            <div class="online-payment-note">
                                <p>Starting from 2015 going forward, we will only be providing Paypal payment option for paying registragtion fee online. However
                                   if your entire team members are eligible for Free and Reduced-Price Meal Program, the <i>Fee Waiver</i> option is available to you.
                                </p>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="reg-left-cell ce-h5" style="width:140px;font-weight:bold;padding-left:10px;">Registration Fee: </td>
                        <td class="reg-right-cell ce-h5">
                            <div class="registration-fee">
                                <asp:DropDownList ID="PaymentAmountList" runat="server" Enabled="false">
                                    <asp:ListItem Text="0 paid contestant $0" Value="0" />
                                    <asp:ListItem Text="1 paid contestant $10" Value="10" />
                                    <asp:ListItem Text="2 paid contestants $20" Value="20" />
                                    <asp:ListItem Text="3 paid contestants $30" Value="30" />
                                    <asp:ListItem Text="4 paid contestants $40" Value="40" />
                                    <asp:ListItem Text="5 paid contestants $50" Value="50" />
                                    <asp:ListItem Text="6 paid contestants $60" Value="60" />
                                    <asp:ListItem Text="7 paid contestants $70" Value="70" />
                                    <asp:ListItem Text="8 paid contestants $80" Value="80" />
                                    <asp:ListItem Text="9 paid contestants $90" Value="90" />
                                    <asp:ListItem Text="10 paid contestants $100" Value="100" />
                                    <asp:ListItem Text="11 paid contestants $110" Value="110" />
                                    <asp:ListItem Text="12 paid contestants $120" Value="120" />
                                    <asp:ListItem Text="13 paid contestants $130" Value="130" />
                                    <asp:ListItem Text="14 paid contestants $140" Value="140" />
                                    <asp:ListItem Text="15 paid contestants $150" Value="150" />
                                    <asp:ListItem Text="16 paid contestants $160" Value="160" />
                                    <asp:ListItem Text="17 paid contestants $170" Value="170" />
                                    <asp:ListItem Text="18 paid contestants $180" Value="180" />
                                    <asp:ListItem Text="19 paid contestants $190" Value="190" />
                                    <asp:ListItem Text="20 paid contestants $200" Value="200" />
                                </asp:DropDownList>
                            </div>
                        </td>
                    </tr>
                    <tr id="pageErrorRow4" class="hide">
                        <td id="page4ErrorMessage" class="form-message red" colspan="2">
                            <asp:Literal ID="Page4ErrorText" runat="server" EnableViewState="False"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="reg-section-divider"></div>
                        </td>
                        </tr>
                    <tr>
                        <td class="reg-button-cell" colspan="2">
                            <asp:Button ID="Page2PreviousButton" runat="server" CssClass="action-button" Font-Bold="true" Text="  <<  Previous  " OnClientClick="return gotoPage(1);" />
                            <asp:Button ID="FeeWaiverButton" runat="server" CssClass="action-button" Font-Bold="true" Text="  Fee Waiver  " OnClick="OnFeeWaiverPayment" />&nbsp;&nbsp;
                            <asp:LinkButton ID="PaypalPaymentButton" runat="server" CssClass="paypal-button" OnClick="OnPaypalPayment"><img runat="server" title="Paypal online payment" src="~/images/paypal_paynow_small.gif" /></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td  class="reg-cell-text ce-h5" colspan="2">
                            <p style="font-weight:bold;padding-bottom:5px;">Notes:</p>
                            <ol style="font-style:italic">
                                <li style="padding-bottom:5px;">If you need to go back to previous input screen, please use the <b>Previous</b> button above instead of browser's Back button.</li>
                                <li style="padding-bottom:5px;">The registration fee shown above excludes those students who are participating in the Free and Reduced-Price Meal Program.
                                    Please be sure to remind them to mail in the Fee Waive form, which can be downloaded from <a style="color:blue !important;text-decoration:underline;" href="../../../documents/talent/2020%20Fee%20Waiver%20Form.pdf" target="_blank">this link</a>.</li>
                                <li style="padding-bottom:5px;color:red;">Please Return to Little Masters Club after you have completed Paypal transaction. A confirmation email from ce@culturalexploration.org will be sent to your registered email and please verify all information in it. If you don’t receive it, please check your junk or spam mail first. If you notice any errors on the list,  please contact  cltc@littlemastersclub.org as soon as possible. Please don't reply to ce@culturalexploration.org  because this email is not monitored. 
                                    <br>
                                    在完成Paypal付款后，请回到小大师报名网页。从ce@culturalexploration.org 发出的报名确认函将发到您注册的邮箱。如果收件箱没有收到，请先检查垃圾邮件文件夹。如果您发现确认函上有任何信息不准确，请尽快联系cltc@littlemastersclub.org. 请不要回复ce@culturalexploration.org 因为这个邮箱没有人工管理。</li>
                            </ol>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="paypal-logo" style="float:right;">
                                <a href="#" onclick="javascript:window.open('https://www.paypal.com/cgi-bin/webscr?cmd=xpt/Marketing/popup/OLCWhatIsPayPal-outside','olcwhatispaypal','toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, width=400, height=350');">
                                    <img runat="server" src="~/images/paypal_payment_logo.gif" border="0" alt="Now Accepting PayPal">
                                </a>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>

            <div id="ApplicationPage3" class="reg-app-page hide">
                <table>
                    <tr>
                        <td class="reg-section-title ce-h4" colspan="2">Registration Completion</td>
                    </tr>
                    <tr>
                        <td class="reg-cell-text ce-h4" colspan="2">
                            <div id="PaymentSuccess" class="online-payment-note">
                                <p>Thanks for your payment. Please click the button below to complete your registration.</p><br />
                            </div>
                        </td> 
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="reg-section-divider"></div>
                        </td>
                        </tr>
                    <tr>
                        <td class="reg-button-cell" colspan="2">
                            <asp:Button ID="DoneRegistration" runat="server" CssClass="action-button" Font-Bold="true" Text="  Complete this Registration  " OnClick="OnCompetitionRegistration" />
                        </td>
                    </tr>
                </table>
            </div>
        </div> 
    </div>

    <div>
        <asp:HiddenField ID="RegistationStartFlag" runat="server" Value="0" />
        <asp:HiddenField ID="RegistrationFee" runat="server" Value="0" />
        <asp:HiddenField ID="TalentShowSubCategory" runat="Server" Value="" />
        <asp:HiddenField ID="TalentShowSubCategoryList" runat="Server" Value="" />
        <asp:HiddenField ID="TalentShowIsPianoRequired" runat="server" />
    </div>

    <div id="modal-dialog" class="hide">
        <div id="form-success-modal">
            <div class="application-result-title"><span class="ce-h3">Talent Competition Registration Acknowledgement</span></div>
            <div class="application-result-text ce-h4">
                <table>
                    <tr>
                        <td style="width:60px;"><img runat="server" src="~/images/confirm.png" /></td>
                        <td>
                            Thanks for filling out the talent competition registration form with us. We have sent an email to confirm that we have received your registration.
                            Please check your email for further instruction about our process and how to proceed going forward.
                        </td>
                    </tr>
                </table>
            </div>
            <div class="application-result-button">
                <asp:Button ID="Submitted" OnClientClick="return closeDialog();" CssClass="ce-h4" Text="  I Am Done  " runat="server" />
                <asp:Button ID="MoreRegistrationButton" OnClientClick="return redirectDialog('/public/talent/registration/TeamRegistration.aspx');" CssClass="ce-h4" Text="  Fill Out Another One?  " runat="server" />
            </div>
        </div>
    </div>

    <div id="modal-redirect-dialog" class="hide">
        <div id="form-redirect-modal">
            <div class="application-result-title"><span class="ce-h3">Talent Competition Registration Acknowledgement</span></div>
            <div class="application-result-text ce-h4">
                <table>
                    <tr>
                        <td style="width:60px;"><img runat="server" src="~/images/confirm.png" /></td>
                        <td>
                            Thanks for filling out the talent competition registration form with us. We have sent an email to confirm that we have received your registration.
                            Please check your email for further instruction about our process and how to proceed going forward.
                        </td>
                    </tr>
                </table>
            </div>
            <div class="application-result-button">
                <asp:Button ID="IAmDoneButton" OnClientClick="return closeDialog();" CssClass="ce-h4" Text="  I Am Done  " runat="server" />
                <asp:Button ID="AnotherRegistrationButton" OnClientClick="return redirectDialog('/public/talent/registration/TalentRegistration.aspx');" CssClass="ce-h4" Text="  Fill out Another One?  " runat="server" />
            </div>
        </div>
    </div>

    <div id="modal-error-dialog" class="hide">
        <div id="form-error-modal">
            <div class="application-result-title"><span class="ce-h3">Talent Competition Registration Duplication</span></div>
            <div class="application-result-text ce-h4">
                <table>
                    <tr>
                        <td style="width:60px;"><img runat="server" src="~/images/error.png" /></td>
                        <td>
                            The registration you filled out already exists. Please change one of the following input data and resumit it again: <br />
                            contact name, phone, category, division, and team name.
                        </td>
                    </tr>
                </table>
            </div>
            <div class="application-result-button">
                <asp:Button ID="ExitButton" OnClientClick="return closeDialog();" CssClass="ce-h4" Text="  Close & Exit  " runat="server" />
                <asp:Button ID="CorrectButton" OnClientClick="return exitDialog();" CssClass="ce-h4" Text="  Correct & Resubmit  " runat="server" />
            </div>
        </div>
    </div>

    <div id="modal-team-dialog" class="hide">
        <div id="team-error-modal">
            <div class="application-result-title"><span class="ce-h3">Talent Competition Registration Team Name Duplication</span></div>
            <div class="application-result-text ce-h4">
                <table>
                    <tr>
                        <td style="width:60px;"><img runat="server" src="~/images/error.png" /></td>
                        <td>
                            The team name you specified already exists. Please use a different name. <br />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="application-result-button">
                <asp:Button ID="Button1" OnClientClick="return closeDialog();" CssClass="ce-h4" Text="  Close & Exit  " runat="server" />
                <asp:Button ID="Button2" OnClientClick="return exitDialog();" CssClass="ce-h4" Text="  Correct & Resubmit  " runat="server" />
            </div>
        </div>
    </div>

    <div>
        <script type="text/javascript">
            var maxContestantsAllowed = 20;
            var contestantRowId = 1;
            var deletingRowIndex = -1;
            var ApplicationPage1ID = '#ApplicationPage1';
            var ApplicationPage2ID = '#ApplicationPage2';
            var ApplicationPage3ID = '#ApplicationPage3';
            var ContactNameBoxID = '#<%= ContactNameBox.ClientID %>';
            var ContactEmailBoxID = '#<%= ContactEmailBox.ClientID %>';
            var ContactPhoneBoxID = '#<%= ContactPhoneBox.ClientID %>';
            var TeamNameBoxID = '#<%= TeamNameBox.ClientID %>';
            var CompetitionCategoryListID = '#<%= CompetitionCategoryList.ClientID %>';
            var CompetitionDivisionListID = '#<%= CompetitionDivisionList.ClientID %>';
            var ContestantFirstNameID = '#<%= ContestantFirstName.ClientID %>';
            var ContestantLastNameID = '#<%= ContestantLastName.ClientID %>';
            var ContestantChineseNameID = '#<%= ContestantChineseName.ClientID %>';
            var ContestantEmailID = '#<%= ContestantEmail.ClientID %>';
            var ContestantBirthdayID = '#<%= ContestantBirthday.ClientID %>';
            var ContestantSchool1ID = '#<%= ContestantSchool1.ClientID %>';
            var ContestantSchool2ID = '#<%= ContestantSchool2.ClientID %>';
            var ContestantGradeListID = '#<%= ContestantGradeList.ClientID %>';
            var YesLunchID = '#<%= YesLunch.ClientID %>';
            var NoLunchID = '#<%= NoLunch.ClientID %>';
            var AddButtonID = '#<%= AddButton.ClientID %>';
            var RemoveButtonID = '#<%= RemoveButton.ClientID %>';
            var PaymentAmountListID = '#<%= PaymentAmountList.ClientID %>';
            var MinimumContestantProxyID = '#<%= MinimumContestantProxy.ClientID %>';
            var MaximumContestantProxyID = '#<%= MaximumContestantProxy.ClientID %>';
            var RegistationStartFlagID = '#<%= RegistationStartFlag.ClientID %>';
            var MinimumContestantsID = '#<%= MinimumContestants.ClientID %>';
            var PaymentButtonID = '#<%= PaymentButton.ClientID %>';
            var RegistrationFeeID = '#<%= RegistrationFee.ClientID %>';
            var FeeWaiverButtonID = '#<%= FeeWaiverButton.ClientID %>';
            var PaypalPaymentButtonID = '#<%= PaypalPaymentButton.ClientID %>';
            // these are hidden fields; not UI fields
            var TalentShowSubCategoryID = '#<%= TalentShowSubCategory.ClientID %>';
            var TalentShowSubCategoryListID = '#<%= TalentShowSubCategoryList.ClientID %>';
            var TalentShowIsPianoRequiredID = '#<%= TalentShowIsPianoRequired.ClientID %>';
            var isTalentShowSelected = false;

            $(document).ready(function () {
                getSchools();
                resetErrortext();
                bindFormDataFromCookie();
                rebindJqueryControls($(TalentShowSubCategoryID).val() == '' ? 0 : 1);
                makeReadonly(); // this only affects client side
                setContestantRowId();

                var paymentStatus = getQueryString('payment');
                if (paymentStatus != null) {
                    setContestantRowId();
                    if (paymentStatus == 0) { // payment has been cancelled
                        $('#page4ErrorMessage').html('The online payment either was cancelled or did not go through.');
                        $('#pageErrorRow4').show();
                    }
                }
            });

            function rebindJqueryControls(talentShowSelected) {
                bindBirthdayPicker();
                bindPhoneMask();
                bindSchoolAutoComplete();
                setContestantRange();
                makeReadonly(); // this will show added contestants after postback

                isTalentShowSelected = talentShowSelected == 1 ? true : false;

                // 2016 update: inject subdivisions radio buttons for talent show subcategories.
                if (talentShowSelected == 1) {
                    // show sbucategory and existing subcategory selection
                    var $talentShowId = $('#talentShowSubCategoryButtons'); // talent show subcategory placeholder
                    var subCategories = '<ul>';
                    var values = $(TalentShowSubCategoryListID).val().split(',');
                    for (var i = 0; i < values.length; i++) {
                        subCategories += '<li style="line-height:20px"><input type="radio" name="talentShowSubcategory" value="' + values[i] + '"/> ' + values[i] + '</li>';
                    }
                    subCategories += '<li><input type="checkbox" id="isPianoRequired" name="isPianoRequired" /> Please check the box if you need Piano for the talent show. </li ></ul ></div >';
                    $talentShowId.append(subCategories);

                    if ($(TalentShowSubCategoryID).val() != '') {
                        $('input[name ^= talentShowSubcategory]').val([$(TalentShowSubCategoryID).val()]);
                    }

                    $('input[name ^= talentShowSubcategory]').click(function () {
                        $(TalentShowSubCategoryID).val($('input[name ^= talentShowSubcategory]:checked').val());
                    });

                    $('#isPianoRequired').change(function () {
                        if ($(this).is(":checked")) {
                            var returnVal = confirm("Are you sure that you need a Piano for the talent show?");
                            $(this).prop("checked", returnVal);

                        }

                        $(TalentShowIsPianoRequiredID).val($(this).is(':checked'));
                        // alert($(TalentShowIsPianoRequiredID).val());  only needed for debugging purpose 
                    });
                }
                else {
                    $(TalentShowSubCategoryID).val('');
                }
            }

            function bindBirthdayPicker() {
                $(ContestantBirthdayID).datepicker({
                    showOn: 'both',
                    changeYear: true,
                    changeMonth: true,
                    yearRange: '-20:+0',
                    buttonImage: '/images/calendar.png',
                    buttonImageOnly: true
                });

                $(ContestantBirthdayID).datepicker('setDate', $(ContestantBirthdayID).val());
            }

            function bindPhoneMask() {
                $(ContactPhoneBoxID).inputmask("mask", { "mask": "999-999-9999" });
            }

            function makeReadonly() {
                $('#reg-participant-table td > input[type=text]').each(function (i, row) {
                    $(this).attr('readonly', 'redonly');
                });

                var rows = $('.reg-participant-row');
                for (var i = 0; i < rows.length; i++) {
                    var currentCells = $(rows[i]).find("input[type=text]");
                    if ($(currentCells).eq(0).val() != '') {
                        $(rows[i]).removeClass('hide');
                    }
                }
            }

            function registrationSubmitted() {
                gotoPage(1);
                modalDialog('#form-success-modal');
            }

            function registrationCompleted() {
                modalDialog('#form-redirect-modal');
            }

            function duplicatedRegistration() {
                gotoPage(1);
                modalDialog('#form-error-modal');
            }

            function DuplicateTeamName() {
                gotoPage(1);
                modalDialog('#team-error-modal');
                $(TeamNameBoxID).val('');
            }

            function resetErrortext() {
                $(ContactNameBoxID).on('input', function () {
                    $('#page1ErrorMessage').html('');
                });
                $(ContactEmailBoxID).on('input', function () {
                    $('#page1ErrorMessage').html('');
                });
                $(ContactPhoneBoxID).on('input', function () {
                    $('#page1ErrorMessage').html('');
                });
                $('#pageErrorRow1').hide();

                $(TeamNameBoxID).on('input', function () {
                    $('#page2ErrorMessage').html('');
                });
                $('#pageErrorRow2').hide();

                $(ContestantFirstNameID).on('input', function () {
                    $('#page3ErrorMessage').html('');
                });
                $(ContestantLastNameID).on('input', function () {
                    $('#page3ErrorMessage').html('');
                });
                $(ContestantEmailID).on('input', function () {
                    $('#page3ErrorMessage').html('');
                });
                $(ContestantBirthdayID).on('input', function () {
                    $('#page3ErrorMessage').html('');
                });
                $(ContestantSchool1ID).on('input', function () {
                    $('#page3ErrorMessage').html('');
                });
                $('#pageErrorRow3').hide();

                $('#pageErrorRow4').hide();
            }

            function clearContestantInput() {
                $(ContestantLastNameID).val('');
                $(ContestantFirstNameID).val('');
                $(ContestantChineseNameID).val('');
                $(ContestantEmailID).val('');
                $(ContestantBirthdayID).val('');
                $(ContestantSchool1ID).val('');
                $(ContestantSchool2ID).val('');
                $(ContestantGradeListID).prop('selectedIndex', 0);
                $(YesLunchID).prop('checked', false);
                $(NoLunchID).prop('checked', true);
            }

            function clearAddedContestants() {
                $('.reg-participant-cell input[type=text]').val('');
                $('.reg-participant-row').hide();
                $(AddButtonID).attr('disabled', false);
                $(RemoveButtonID).attr('disabled', 'disabled');

                $('#page3ErrorMessage').html(''); // reset error
                $('#pageErrorRow3').hide();

                contestantRowId = 1; // reset the contestant table index

                return false;
            }

            function validateInput() {
                //if ($(RegistationStartFlagID).val() == '0') return true; // no validation if the registration is not started yet

                $('#pageErrorRow1').hide();
                $('#pageErrorRow2').hide();
                $('#pageErrorRow3').hide();

                var email = $(ContactEmailBoxID).val();
                var phone = $(ContactPhoneBoxID).val();

                var count1 = 0;

                count1 += ($(ContactNameBoxID).val() == '') ? 1 : 0;
                count1 += (email == '') ? 1 : 0;
                count1 += (phone == '') ? 1 : 0;

                if (count1 > 0) {
                    if (count1 == 1)
                        $('#page1ErrorMessage').html('One required input is not provided.');
                    else
                        $('#page1ErrorMessage').html(count1 + ' required input are not provided.');
                    $('#pageErrorRow1').show();
                }
                else if (!isValidEmail(email)) {
                    $('#page1ErrorMessage').html("The contact's email format is not valid.");
                    $('#pageErrorRow1').show();
                    count1 = -1;
                }
                else if (!isValidPhone(phone)) {
                    $('#page1ErrorMessage').html('The phone # needs to be all digits with format of xxxxxxxxxx or xxx-xxx-xxxx.');
                    $('#pageErrorRow1').show();
                    count1 = -1;
                }

                var count2 = 0;
                count2 += ($(TeamNameBoxID).val() == '') ? 1 : 0;
                count2 += ($(CompetitionCategoryListID).prop('selectedIndex') <= 0) ? 1 : 0;
                count2 += ($(CompetitionDivisionListID).prop('selectedIndex') <= 0) ? 1 : 0;
                if (count2 > 0) {
                    if (count2 == 1)
                        $('#page2ErrorMessage').html('One required competition input is not provided.');
                    else
                        $('#page2ErrorMessage').html(count2 + ' required competition input are not provided.');
                    $('#pageErrorRow2').show();
                }
                else if (!isValidName($(TeamNameBoxID).val())) {
                    $('#page2ErrorMessage').html('The team name can only contain alphabets, digits, and _ character.');
                    $('#pageErrorRow2').show();
                    count2 = -1;
                }

                var contestantOK = true;
                if (count1 == 0 && count2 == 0) {
                    var minContestants = $(MinimumContestantProxyID).val();
                    var maxContestants = $(MaximumContestantProxyID).val();
                    if (contestantRowId <= minContestants) {
                        $('#page3ErrorMessage').html('At least ' + minContestants + ' contestants are requried to register for selected team competition.');
                        $('#pageErrorRow3').show();
                        contestantOK = false;
                    }
                    else if (maxContestants > 0 && contestantRowId - 1 > maxContestants) {
                        $('#page3ErrorMessage').html('At most ' + maxContestants + ' contestants are allowed to register for selected team competition.');
                        $('#pageErrorRow3').show();
                        contestantOK = false;
                    }
                    else if (isTalentShowSelected && $(TalentShowSubCategoryID).val() == '') {
                        $('#page1ErrorMessage').html('Please select a subcategory for talent show.');
                        $('#pageErrorRow1').show();
                        contestantOK = false;
                    }
                }         

                if (!isTalentShowSelected) $(TalentShowSubCategoryID).val('');

                return count1 == 0 && count2 == 0 && contestantOK;
            }

            function validateContestant() {
                $('#page3ErrorMessage').html('');
                $('#pageErrorRow3').hide();

                if ($(CompetitionCategoryListID).prop('selectedIndex') <= 0 || $(CompetitionDivisionListID).prop('selectedIndex') <= 0) {
                    $('#page3ErrorMessage').html('Please selecte a competition category and division first.');
                    $('#pageErrorRow3').show();
                    return false;
                }

                var ln = $(ContestantLastNameID).val();
                var fn = $(ContestantFirstNameID).val();
                var bd = $(ContestantBirthdayID).val();
                var email = $(ContestantEmailID).val();
                var grade = $(ContestantGradeListID).get(0).selectedIndex;

                var count = 0;
                count += (ln == '') ? 1 : 0;
                count += (fn == '') ? 1 : 0;
                count += (bd == '') ? 1 : 0;
                count += (email == '') ? 1 : 0;
                count += ($(ContestantSchool1ID).val() == '') ? 1 : 0;
                count += (grade <= 0) ? 1 : 0;

                if (count > 0) {
                    if (count == 1)
                        $('#page3ErrorMessage').html('One required input for contenstant is not provided.');
                    else
                        $('#page3ErrorMessage').html(count + ' required input for contenstant are not provided.');
                    $('#pageErrorRow3').show();
                }
                else {
                    if (!isValidEmail(email)) {
                        $('#page3ErrorMessage').html("The contestant's email format is not valid.");
                        $('#pageErrorRow3').show();
                        count = -1;
                    }
                    if (!isValidDate(bd)) {
                        $('#page3ErrorMessage').html("The contestant's birthday either is out of range or needs to be in the form of mm/dd/yyyy.");
                        $('#pageErrorRow3').show();
                        count = -1;
                    }
                    else if (contestantExist(ln, fn, bd)) {
                        $('#page3ErrorMessage').html('Contestant already exists.');
                        $('#pageErrorRow3').show();
                        count = -1;
                    }
                }

                return count == 0;
            }

            function addContestant() {
                if (validateContestant() == false) return false;

                var maxContestants = $(MaximumContestantProxyID).val();
                if (maxContestants > 0 && contestantRowId > maxContestants) {
                    $('#page3ErrorMessage').html('At most ' + maxContestants + ' contestants are allowed to register for selected team competition.');
                    $('#pageErrorRow3').show();
                    return false;
                }

                var ln = $(ContestantLastNameID).val().trim();
                var fn = $(ContestantFirstNameID).val().trim();
                var bd = $(ContestantBirthdayID).val().trim();
                var em = $(ContestantEmailID).val().trim();
                var s1 = $(ContestantSchool1ID).val().trim();
                var s2 = $(ContestantSchool2ID).val().trim();
                var gd = $(ContestantGradeListID).val();
                var lp = ($(YesLunchID).prop('checked') == true ? 'Yes' : 'No');
                var cn = $(ContestantChineseNameID).val().trim();
                var id = fn.toLowerCase() + '.' + ln.toLowerCase() + '.' + bd.replace(/[\/]/g, '-');

                $('#reg-participant-row-' + contestantRowId).show();
                var cells = $('#reg-participant-row-' + contestantRowId + ' > td > input[type=text]'); // get all <td> input cells
                setContestantRowText(cells, ln, fn, bd, em, s1, s2, gd, lp, cn, id);

                $(ContestantLastNameID).val('');
                $(ContestantFirstNameID).val('');
                $(ContestantChineseNameID).val('');
                $(ContestantBirthdayID).val('');

                if ((maxContestants > 0 && contestantRowId >= maxContestants) || contestantRowId >= maxContestantsAllowed) {
                    $(AddButtonID).attr('disabled', 'disabled');
                }
                if (contestantRowId > 1) {
                    $(RemoveButtonID).attr('disabled', false);
                }

                contestantRowId++;

                return false;
            }

            function removeContestant() {
                $('#page3ErrorMessage').html(''); // reset error
                $('#pageErrorRow3').hide();

                if (contestantRowId == 1) {
                    $('#page3ErrorMessage').html('There is no contestant to remove.');
                    $('#pageErrorRow3').show();
                    return false;
                }

                deletingRowIndex = -1;
                var ln = $(ContestantLastNameID).val().trim().toLowerCase();
                var fn = $(ContestantFirstNameID).val().trim().toLowerCase();
                var bd = $(ContestantBirthdayID).val().trim().toLowerCase();
                if (ln == '' || fn == '' || bd == '') {
                    $('#page3ErrorMessage').html('Please specify a first name, last name, and birthday to remove added constatnt.');
                    $('#pageErrorRow3').show();
                    return false;
                }
                $('.reg-participant-row').each(function (i, row) {
                    if ($(this).is(':hidden') || deletingRowIndex >= 0) return false;
                    var cells = $(this).find("input[type=text]");
                    if (ln == $(cells).eq(0).val().toLowerCase() && fn == $(cells).eq(1).val().toLowerCase() && bd == $(cells).eq(2).val().toLowerCase()) {
                        deletingRowIndex = i;
                        compactContestantRows(deletingRowIndex);
                        contestantRowId--;
                        $('#reg-participant-row-' + contestantRowId).hide();// row id start from 1 instead of 0
                    }
                });

                if (deletingRowIndex == -1) {
                    $('#page3ErrorMessage').html('Contestant does not exist.');
                    $('#pageErrorRow3').show();
                }
                else {
                    var maxContestants = $(MaximumContestantProxyID).val();
                    if ((maxContestants > 0 && contestantRowId <= maxContestants) || contestantRowId <= maxContestantsAllowed) {
                        $(AddButtonID).attr('disabled', false);
                    }
                    if (contestantRowId == 1) {
                        $(RemoveButtonID).attr('disabled', 'disabled');
                    }
                }

                return false;
            }

            function contestantExist(lastname, firstname, birthday) {
                var ln = lastname.toLowerCase();
                var fn = firstname.toLowerCase();
                var bd = birthday.toLowerCase();
                var rows = $('.reg-participant-row');
                for (var i = 0; i < rows.length; i++) {
                    if ($(currentCells).eq(0).val() == '') break;
                    var currentCells = $(rows[i]).find("input[type=text]");
                    if ($(currentCells).eq(0).val().toLowerCase() == ln &&
                        $(currentCells).eq(1).val().toLowerCase() == fn &&
                        $(currentCells).eq(2).val().toLowerCase() == bd) {
                        return true;
                    }
                }
                return false;
            }

            function compactContestantRows(rowIndex) {
                var rows = $('.reg-participant-row');
                for (var i = rowIndex; i < maxContestantsAllowed; i++) {
                    var j = i + 1;
                    var currentCells = $(rows[i]).find("input[type=text]");
                    if ($(currentCells).eq(0).val() == '') break;
                    // move row up
                    if (j < maxContestantsAllowed) {
                        var nextCells = $(rows[j]).find("input[type=text]");
                        setContestantRowText(currentCells,
                                             $(nextCells).eq(0).val(),
                                             $(nextCells).eq(1).val(),
                                             $(nextCells).eq(2).val(),
                                             $(nextCells).eq(3).val(),
                                             $(nextCells).eq(4).val(),
                                             $(nextCells).eq(5).val(),
                                             $(nextCells).eq(6).val(),
                                             $(nextCells).eq(7).val(),
                                             $(nextCells).eq(8).val(),
                                             $(nextCells).eq(9).val());
                    }
                    else {
                        setContestantRowText(currentCells, '', '', '', '', '', '', '', '', '', '');
                    }
                }
            }

            function setContestantRowText(cells, ln, fn, bd, em, s1, s2, gd, lp, cn, id) {
                $(cells).eq(0).val(ln);
                $(cells).eq(1).val(fn);
                $(cells).eq(2).val(bd);
                $(cells).eq(3).val(em);
                $(cells).eq(4).val(s1);
                $(cells).eq(5).val(s2);
                $(cells).eq(6).val(gd);
                $(cells).eq(7).val(lp);
                $(cells).eq(8).val(cn);
                $(cells).eq(9).val(id);
            }

            function setPaidContestants() {
                var count = 0;
                var rows = $('.reg-participant-row');
                for (var i = 0; i < rows.length; i++) {
                    var currentCells = $(rows[i]).find("input[type=text]");
                    if ($(currentCells).eq(0).val() != '') {
                        $(rows[i]).removeClass('hide');
                        if ($(currentCells).eq(7).val() == 'No') count++;
                    }
                }
                if (count > 0) {
                    var fee = '' + (count * 10);
                    $(PaymentAmountListID).val(fee);
                    $(RegistrationFeeID).val(fee);
                    $(PaypalPaymentButtonID).show();
                    $(FeeWaiverButtonID).hide();
                }
                else {
                    $(PaymentAmountListID).val("0");
                    $(RegistrationFeeID).val("0");
                    $(PaypalPaymentButtonID).hide();
                    $(FeeWaiverButtonID).show();
                }
                return true;
            }

            function setContestantRange() {
                var minCount = $(MinimumContestantProxyID).val();
                var maxCount = $(MaximumContestantProxyID).val();
                if (minCount == 1) {
                    $(MinimumContestantsID).text('(minimum contestant: 1)');
                    $(AddButtonID).attr('disabled', false);
                }
                else if (maxCount <= 0) {
                    $(MinimumContestantsID).text('(minimum contestants: ' + minCount + ')');
                    $(AddButtonID).attr('disabled', false);
                }
                else {
                    $(MinimumContestantsID).text('(minimum contestants: ' + minCount + ', maximum contestants: ' + maxCount + ')');
                    $(AddButtonID).attr('disabled', false);
                }
            }

            function gotoPage(page) {
                if (page != 1) {
                    $('.ce-temp-note').hide();
                }
                else {
                    $('.ce-temp-note').show();
                }

                $('#page3ErrorMessage').html();
                $('#pageErrorRow3').hide();
                $('#page4ErrorMessage').html();
                $('#pageErrorRow4').hide();

                $(ApplicationPage1ID).hide();
                $(ApplicationPage2ID).hide();
                $(ApplicationPage3ID).hide();

                switch (page) {
                    case 1: $(ApplicationPage1ID).show(); break;
                    case 2: setPaidContestants();
                        $(ApplicationPage2ID).show();
                        break;
                    case 3: $(ApplicationPage3ID).show(); break;
                }
                return false;
            }

            function paymentCancel() {
                setContestantRowId();
                gotoPage(2);
            }

            function paymentReceived() {
                setContestantRowId();
                gotoPage(3);
            }

            function setContestantRowId() {
                var rows = $('.reg-participant-row');
                contestantRowId = 1;
                for (var i = 0; i < rows.length; i++) {
                    var currentCells = $(rows[i]).find("input[type=text]");
                    if ($(currentCells).eq(0).val() != '') contestantRowId++;
                }

                $(AddButtonID).attr('disabled', false);
            }

            var availableSchools = [];
            function getSchools() {
                var url = 'Schools.aspx/getSchoolsList'

                var options = {
                    type: 'POST',
                    url: url,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                };

                $.ajax(options).done(function (data) {
                    availableSchools = $.parseJSON(data.d);
                    bindSchoolAutoComplete();
                });
            }

            function bindSchoolAutoComplete() {
                $(ContestantSchool1ID).autocomplete({
                    source: availableSchools
                });

                $(ContestantSchool2ID).autocomplete({
                    source: availableSchools
                });

                // this one hidden help text is causing the autocomplete box to shift downward; so we remove it as we don't need it.
                $('span.ui-helper-hidden-accessible').remove();
            }

            bindFormDataFromCookie = function () {
                var savedForm = CEApp.Cookie.getCookie({ 'cookieName': 'CE_TEAM_ENTRY' });
                if (savedForm != null) {
                    var formDataJson = JSON.parse(savedForm);
                    $(ContactNameBoxID).val(formDataJson.contactName);
                    $(ContactEmailBoxID).val(formDataJson.contactEmail);
                    $(ContactPhoneBoxID).val(formDataJson.contactPhone);
                    $(TeamNameBoxID).val(formDataJson.team);
                    $(CompetitionCategoryListID).val(formDataJson.category);
                    $(CompetitionDivisionListID).val(formDataJson.division);
                    $(TalentShowSubCategoryID).val(formDataJson.subcategory);
                    $(TalentShowIsPianoRequiredID).val(formDataJson.ispianorequired);
                    populateTeammates(formDataJson.teammates);
                    setContestantRowId();

                    $(ContestantLastNameID).val('');
                    $(ContestantFirstNameID).val('');
                    $(ContestantChineseNameID).val('');
                    $(ContestantBirthdayID).val('');
                    $(ContestantSchool1ID).val('');
                    $(ContestantSchool2ID).val('');
                    $(ContestantEmailID).val('');
                    $(ContestantGradeListID).val('');
                    $(YesLunchID).prop('checked', false);
                    $(NoLunchID).prop('checked', false);
                }
            }

            function saveFormData() {
                var formJson = CEApp.Json.teamEntryJson(
                    {
                        'contactName': $(ContactNameBoxID).val(),
                        'contactEmail': $(ContactEmailBoxID).val(),
                        'contactPhone': $(ContactPhoneBoxID).val(),
                        'team': $(TeamNameBoxID).val(),
                        'category': $(CompetitionCategoryListID).val(),
                        'division': $(CompetitionDivisionListID).val(),
                        'subcategory': $(TalentShowSubCategoryID).val(),
                        'ispianorequired': $(TalentShowIsPianoRequiredID.val()),
                        'teammates': getTeammates()
                    });

                if (formJson != null) CEApp.Cookie.setCookie({ 'cookieName': 'CE_TEAM_ENTRY' }, JSON.stringify(formJson));

                return false;
            }

            function getTeammates() {
                var teammates = [];
                $('.reg-participant-row').each(function (i, row) {
                    if (!$(this).is(':hidden')) {
                        var cells = $(this).find("input[type=text]");
                        var contestant = {
                            'lastName': $(cells).eq(0).val(),
                            'firstName': $(cells).eq(1).val(),
                            'birthday': $(cells).eq(2).val(),
                            'email': $(cells).eq(3).val(),
                            'school': $(cells).eq(4).val(),
                            'otherSchool': $(cells).eq(5).val(),
                            'grade': $(cells).eq(6).val(),
                            'lunchProgram': $(cells).eq(7).val(),
                            'chineseName': $(cells).eq(8).val(),
                            'id': $(cells).eq(9).val()
                        };
                        teammates.push(contestant);
                    }
                });
                return teammates;
            }

            function populateTeammates(teammates) {
                $.each(teammates, function (i, contestant) {
                    $('#reg-participant-row-' + contestantRowId).show();
                    var row = i + 1;
                    var cells = $('#reg-participant-row-' + row + ' > td > input[type=text]'); // get all <td> input cells
                    setContestantRowText(cells,
                                         contestant.lastName,
                                         contestant.firstName,
                                         contestant.birthday,
                                         contestant.email,
                                         contestant.school,
                                         contestant.otherSchool,
                                         contestant.grade,
                                         contestant.lunchProgram,
                                         contestant.chineseName,
                                         contestant.id);
                });
            }

       </script>
    </div>
</asp:Content>
