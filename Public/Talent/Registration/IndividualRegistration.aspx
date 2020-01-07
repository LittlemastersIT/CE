<%@ Page Language="C#" MasterPageFile="~/CEMain.Master" AutoEventWireup="true" validateRequest="false" CodeBehind="IndividualRegistration.aspx.cs" Inherits="CE.Pages.IndividualRegistrationPage" %>
<%@ Register TagPrefix="CE" Namespace="CE.Pages" Assembly="CE.Application" %>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="PlaceHolderScript" runat="server">
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/jquery-ui-1.10.3.custom.min.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/ceadmin.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/themes/blue/cepage.css")%>" media="all" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script> 
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/jquery/jquery.colorbox-min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/jquery/jquery-ui-1.10.3.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/jquery/jquery.inputmask.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/cecookie.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/cejson.js")%>"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="ce-admin-content">
        <div class="ce-registration-page">
            <div class="page-title ce-h2" style="display: inline;">Individual Competitions Registration</div>
            <div class="form-input-note"><span class="red">*</span> denotes required input</div>
            <div class="reg-section-divider"></div>
            <div id="ApplicationPage1" class="reg-app-page show">
                <asp:UpdatePanel ID="Page1UpdatePanel" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td class="reg-section-title ce-h4" colspan="2">Contact Information</td>
                            </tr>
                             <tr>
                                <td class="ce-h6" style="padding-left:5px;padding-bottom:20px" colspan="6">
                                    Note: <i>The contact information below will be used by the Competition Administrator to send information (such as confirmation) regarding 
                                             the competition to the contestant. The contact person can a parent, a teacher, a coach or the contestant registered who would 
                                             like to receive the information directly.</i>
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
                            <tr>
                                <td colspan="6">
                                    <div class="reg-section-divider"></div>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td class="reg-section-title ce-h4" colspan="2">Competition Information</td>
                            </tr>
                            <tr>
                                <td class="ce-h6" style="padding-left:5px;padding-bottom:20px" colspan="2">
                                    Note: <i>The cost of each competition category is $10.</i>
                                </td>
                            </tr>
                            <tr>
                                <td class="reg-left-cell ce-h5">
                                    <div><span class="red">*</span> Competition Division :</div>
                                </td>
                                <td class="reg-right-cell ce-h5">
                                    <asp:DropDownList ID="CompetitionDivisionList" runat="server" Width="350px" AutoPostBack="true" OnSelectedIndexChanged="OnDivisionSelectionChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="reg-left-cell ce-h5">
                                    <div><span class="red">*</span> Competition Category :</div>
                                </td>
                                <td class="reg-right-cell ce-h5">
                                    <asp:CheckBoxList ID="CompetitionCategoryList" runat="server" Width="350px" AutoPostBack="true" OnSelectedIndexChanged="OnCategorySelectionChanged">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td> </td>
                                <td class="ce-h5" style="padding-left:20px;">
                                    <div id="competitionClass">
                                        <asp:RadioButtonList ID="CompetitionClassOptions" runat="server" Width="350px" RepeatDirection="Horizontal" />
                                    </div>
                                </td>                            
                            </tr>
                            <tr>
                                <td class="reg-left-cell ce-h5"> </td>
                                <td class="reg-right-cell ce-h5">
                                    <asp:Label ID="TotalCompetitionCost" ForeColor="gray" Font-Italic="true" runat="server" CssClass="ce-h6"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                 <td class="ce-h6" style="padding-left:5px;padding-bottom:20px" colspan="2">
                                    Note 1: <i>The Competition Class is based on the contestant’s individual language ability (not based on the contestant’s family background or birth of origin). Please be sure you read the Competition Information carefully before signing up.</i>
                                </td>
                            </tr>
                            <tr>
                                 <td class="ce-h6" style="padding-left:5px;padding-bottom:20px" colspan="2">
                                    Note 2: <i>If music is required, the contestant who registered for Chinese Singing or Talent Show can bring their own musical instruments (a piano will be available on the stage for use), or upload your music file before the March 2, 2020 registration deadline.
                                    For Talent show, please upload music to <a href="http://www.littlemastersclub.org/music/talentshow">http://www.littlemastersclub.org/music/talentshow </a>.  
                                    And for Chinese Singing, please upload music to  <a href="http://www.littlemastersclub.org/music/Singing">http://www.littlemastersclub.org/music/Singing </i>
                                </td>
                            </tr>
                            <tr>
                                 <td class="ce-h6" style="padding-left:5px;padding-bottom:20px" colspan="2">
                                    Note 3: Each person is limited to participate in a maximum of 3 competitions, including group and individual ones.
                                 </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div class="reg-input-divider"></div>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td class="reg-section-title ce-h4" colspan="4">Contestant Information&nbsp;:&nbsp;<span style="color:#8C6E70;" class="ce-h5"><i><b> (Please enter accurate contestant information)</b></i></span></td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <div class="reg-left-cell1 ce-h5" style="display:inline-block"><div style="width: 95px;"><span class="red">*</span> First Name :</div></div>
                                    <div class="reg-left-cell1 ce-h5" style="display:inline-block"><asp:TextBox ID="ContestantFirstName" runat="server" CssClass="ce-h5" Width="180px" /></div>
                                    <div class="reg-left-cel11 ce-h5" style="display:inline-block"><div style="padding-left:10px;width:90px;"><span class="red">*</span> Last Name :</div></div>
                                    <div class="reg-left-cell1 ce-h5" style="display:inline-block"><asp:TextBox ID="ContestantLastName" runat="server" CssClass="ce-h4" Width="180px" /></div>
                                    <div class="reg-left-cell1 ce-h5" style="display:inline-block"><div style="padding-left:20px;width:105px;"> Chinese Name :</div></div>
                                    <div class="reg-left-cell1 ce-h5" style="display:inline-block"><asp:TextBox ID="ContestantChineseName" runat="server" CssClass="ce-h4" Width="102px" /></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="reg-left-cell1 ce-h5"><div style="width: 95px;"><span class="red">*</span> Birthday :</div></td>
                                <td class="reg-right-cell1 ce-h5"><asp:TextBox ID="ContestantBirthday" runat="server" CssClass="ce-h5" Width="155px" ReadOnly /></td>
                                <td class="reg-left-cell2 ce-h5"><div style="width: 160px;"><span class="red">*</span> Academic School:</div></td>
                                <td class="reg-right-cell2 ce-h5"><asp:TextBox ID="ContestantSchool1" runat="server" CssClass="ce-h5" Width="300px"/></td>
                            </tr>
                            <tr>
                                <td class="reg-left-cell1 ce-h5"><div style="width: 95px;"><span class="red">*</span> Email :</div></td>
                                <td class="reg-right-cell1 ce-h5"><asp:TextBox ID="ContestantEmail" runat="server" CssClass="ce-h5" Width="180px" TextMode="Email" /></td>
                                <td class="reg-left-cell2 ce-h5"><div style="width: 160px;">Extracurricular School :</div></td>
                                <td class="reg-right-cell2 ce-h5"><asp:TextBox ID="ContestantSchool2" runat="server" CssClass="ce-h5" Width="300px" /></td>
                            </tr>
                            <tr>
                                <td class="reg-left-cell1 ce-h5"><div style="width: 95px;"><span class="red">*</span> Grade :</div></td>
                                <td class="reg-right-cell1 ce-h5">
                                    <asp:DropDownList ID="ContestantGradeList" runat="server" CssClass="ce-h5" Width="185px">
                                    </asp:DropDownList>
                                </td>
                                <td class="reg-left-cell1 ce-h5" colspan="2" style="padding-top:10px;padding-left:10px;">Do you participate in Free and Reduced-Price Meal Program?&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton ID="YesLunch" Text="  Yes" runat="server" GroupName="SchoolLunch" />&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton ID="NoLunch" Text="  No" runat="server" GroupName="SchoolLunch" Checked="true" /><br />
                                </td>
                            </tr>
                            <tr>
                                <td class="reg-left-cell1 ce-h5" colspan="2"></td>
                                <td class="reg-left-cell1 ce-h6" colspan="2" style="padding-top:10px;padding-left:10px;">
                                    Note: <i>If the student participates in Free and Reduced-Price Meal Program he/she is eligible for scholarships to cover 
                                             their entrance fee to the competition. If you select Yes, please download the Fee Waiver form from Schedule and Guidelines page to apply.</i>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <div class="reg-input-divider"></div>
                                </td>
                            </tr>
                            <tr id="pageErrorRow1" class="hide">
                                <td id="page1ErrorMessage" class="form-message red" colspan="4">
                                    <asp:Literal ID="Page1ErrorText" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <table id="reg-action-table">
                    <tr>
                        <td class="reg-button-cell">
                            <asp:Button ID="PaymentButton" runat="server" CssClass="action-button" Font-Bold="true" Text="  Next  >>  " OnClientClick="javascript: if(validateInput()) return gotoPage(2); else return false;" />
                            <asp:Button ID="Page1ClearAllButton" runat="server" CssClass="action-button" Text="  Clear Form  " OnClientClick="return clearInputForm();" />
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
                                   if you are eligible for Free and Reduced-Price Meal Program, the <i>Fee Waiver</i> option is available to you.
                                </p>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="reg-left-cell ce-h5" style="width:140px;font-weight:bold;padding-left:10px;">Registration Fee: </td>
                        <td class="reg-right-cell ce-h5">
                            <div class="registration-fee">
                                <asp:DropDownList ID="PaymentAmountList" runat="server" Enabled="false">
                                    <asp:ListItem Text="0 competition category $0" Value="0" />
                                    <asp:ListItem Text="1 competition category $10" Value="10" />
                                    <asp:ListItem Text="2 competition categories $20" Value="20" />
                                    <asp:ListItem Text="3 competition categories $30" Value="30" />
                                    <asp:ListItem Text="4 competition categories $40" Value="40" />
                                    <asp:ListItem Text="5 competition categories $50" Value="50" />
                                    <asp:ListItem Text="6 competition categories $60" Value="60" />
                                </asp:DropDownList>
                            </div>
                        </td>
                    </tr>
                    <tr id="pageErrorRow2" class="hide">
                        <td id="page2ErrorMessage" class="form-message red" colspan="2">
                            <asp:Literal ID="Page2ErrorText" runat="server" EnableViewState="False"></asp:Literal>
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
                            <asp:Button ID="FeeWaiverButton" runat="server" CssClass="action-button" Font-Bold="true" Text="  Fee Waiver  " OnClientClick="return validateInput();" OnClick="OnFeeWaiverPayment" />&nbsp;&nbsp;
                            <asp:LinkButton ID="PaypalPaymentButton" runat="server" CssClass="paypal-button" OnClientClick="return validateInput();" OnClick="OnPaypalPayment"><img runat="server" title="Paypal online payment" src="~/images/paypal_paynow_small.gif" /></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td  class="reg-cell-text ce-h5" colspan="2">
                            <p style="font-weight:bold;padding-bottom:5px;">Notes:</p>
                            <ol style="font-style:italic">
                                <li style="padding-bottom:5px;">If you need to go back to previous input screen, please use the <b>Previous</b> button above instead of browser's Back button.</li>
                                <li style="padding-bottom:5px;">For the student participates in Free and Reduced-Price Meal Program, you are eligible for scholarships to cover your entrance fee 
                                    to the competition.  Please select <u><b>Fee Waiver</b></u> for payment and mail the completed Fee Waiver form within 10 days of registration 
                                    and before the registration deadline. The Fee Waive form can be downloaded from <a style="color:blue !important;text-decoration:underline;" href="../../../documents/talent/2020%20Fee%20Waiver%20Form.pdf" target="_blank">this link</a>.</li>
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
                                    <img runat="server" src="~/images/paypal_payment_logo.gif" border="0" alt="Now Accepting PayPal"/>
                                </a>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>

            <div id="ApplicationPage3" class="reg-app-page hide">
                <table>
                    <tr>
                        <td class="reg-section-title ce-h4">Registration Completion</td>
                    </tr>
                    <tr>
                        <td class="reg-cell-text ce-h4">
                            <div id="PaymentSuccess" class="online-payment-note">
                                <p>Thanks for your payment. Please click the button below to complete your registration.</p><br />
                            </div>
                        </td> 
                    </tr>
                    <tr>
                        <td>
                            <div class="reg-section-divider"></div>
                        </td>
                    </tr>
                    <tr>
                        <td class="reg-button-cell">
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
                <asp:Button ID="MoreRegistrationButton" OnClientClick="return redirectDialog('/public/talent/registration/IndividualRegistration.aspx');" CssClass="ce-h4" Text="  Fill Out Another One?  " runat="server" />
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
                <asp:Button ID="RedirectDoneButton" OnClientClick="return closeDialog();" CssClass="ce-h4" Text="  I Am Done  " runat="server" />
                <asp:Button ID="RedirectNoreButton" OnClientClick="return redirectDialog('/public/talent/registration/TalentRegistration.aspx');" CssClass="ce-h4" Text="  Fill Out Another One?  " runat="server" />
            </div>
        </div>
    </div>

    <div id="modal-error-dialog" class="hide">
        <div id="form-duplicate-modal">
            <div class="application-result-title"><span class="ce-h3">Talent Competition Registration Duplication</span></div>
            <div class="application-result-text ce-h4">
                <table>
                    <tr>
                        <td style="width:60px;"><img runat="server" src="~/images/error.png" /></td>
                        <td>
                            <div id="duplicateCategory">
                                <asp:Label ID="DuplicateCategory" runat="server" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="application-result-button">
                <asp:Button ID="CorrectButton" OnClientClick="return exitDialog();" CssClass="ce-h4" Text="  Correct & Resubmit  " runat="server" />
                <asp:Button ID="ExitButton" OnClientClick="return closeDialog();" CssClass="ce-h4" Text="  Close & Exit  " runat="server" />
            </div>
        </div>
    </div>

    <div id="modal-duplicate-dialog" class="hide">
        <div id="form-invalid-modal">
            <div class="application-result-title"><span class="ce-h3">Invalid Talent Competition Registration</span></div>
            <div class="application-result-text ce-h4">
                <table>
                    <tr>
                        <td style="width:60px;"><img runat="server" src="~/images/error.png" /></td>
                        <td>
                            <div id="invalidCategory">
                                <asp:Label ID="InvalidCategory" runat="server" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="application-result-button">
                <asp:Button ID="CorrectButton1" OnClientClick="return exitDialog();" CssClass="ce-h4" Text="  Correct & Resubmit  " runat="server" />
                <asp:Button ID="ExitButton1" OnClientClick="return closeDialog();" CssClass="ce-h4" Text="  Close & Exit  " runat="server" />
            </div>
        </div>
    </div>

    <div>
        <script type="text/javascript">
            var ApplicationPage1ID = '#ApplicationPage1';
            var ApplicationPage2ID = '#ApplicationPage2';
            var ApplicationPage3ID = '#ApplicationPage3';
            var ContactNameBoxID = '#<%= ContactNameBox.ClientID %>';
            var ContactEmailBoxID = '#<%= ContactEmailBox.ClientID %>';
            var ContactPhoneBoxID = '#<%= ContactPhoneBox.ClientID %>';
            var CompetitionCategoryListID = '#<%= CompetitionCategoryList.ClientID %>';
            var CompetitionDivisionListID = '#<%= CompetitionDivisionList.ClientID %>';
            var CompetitionClassOptionsID = '#<%= CompetitionClassOptions.ClientID %>';
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
            var PaymentAmountListID = '#<%= PaymentAmountList.ClientID %>';
            var FeeWaiverButtonID = '#<%= FeeWaiverButton.ClientID %>';
            var PaypalPaymentButtonID = '#<%= PaypalPaymentButton.ClientID %>';
            var RegistationStartFlagID = '#<%= RegistationStartFlag.ClientID %>';
            var RegistrationFeeID = '#<%= RegistrationFee.ClientID %>';
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
                var paymentStatus = getQueryString('payment');
                if (paymentStatus != null) {
                    if (paymentStatus == 0) { // payment has been cancelled
                        $('#page2ErrorMessage').html('The online payment either was cancelled or did not go through.');
                        $('#pageErrorRow2').show();
                    }
                }
            });

            function rebindJqueryControls(talentShowSelected) {
                bindBirthdayPicker();
                bindPhoneMask();
                bindSchoolAutoComplete();

                isTalentShowSelected = false;

                // 2016 update: inject subdivisions radio buttons for talent show subcategories.
                if (talentShowSelected == 1) {
                    // show sbucategory and existing subcategory selection
                    isTalentShowSelected = true;
                    var $talentShowId = $(CompetitionCategoryListID + ' td').first(); // talent show is the first one in the list
                    var subCategories = '<div style="padding:5px 0px 0px 20px;"><ul>';
                    var values = $(TalentShowSubCategoryListID).val().split(',');
                    for (var i = 0; i < values.length; i++) {
                        subCategories += '<li style="line-height:20px"><input type="radio" name="talentShowSubcategory" value="' + values[i] + '"/> ' + values[i] + '</li>';
                    }

                    subCategories += '<li><input type="checkbox" id="isPianoRequired" name="isPianoRequired" /> Please check this box <mark>if you need a piano for the talent show. </mark> </li ></ul ></div >';
                    $talentShowId.append(subCategories);

                    if ($(TalentShowSubCategoryID).val() != '') {
                        $('input[name ^= talentShowSubcategory]').val([$(TalentShowSubCategoryID).val()]);
                    }

                    $('input[name ^= talentShowSubcategory]').click(function () {
                        $(TalentShowSubCategoryID).val($('input[name ^= talentShowSubcategory]:checked').val());
                    });

                    if ($(TalentShowIsPianoRequiredID).val() == 'true') {
                        $('#isPianoRequired').prop("checked", true);
                    }
                    else {
                        $('#isPianoRequired').prop("checked", false);
                    }
 
                    $('#isPianoRequired').change(function () {
                        if ($(this).is(":checked")) {
                            var returnVal = confirm("Are you sure that you need a Piano for the talent show?");
                            $(this).prop("checked", returnVal);                       
                        }
                        $(TalentShowIsPianoRequiredID).val($(this).is(':checked'));
                        console.log("TalentShowIsPianoRequiredID.val() = : " + $(TalentShowIsPianoRequiredID).val());
                    });

                }
            }

            function bindBirthdayPicker() {
                $(ContestantBirthdayID).datepicker({
                    showOn: 'both',
                    changeYear: true,
                    changeMonth: true,
                    yearRange: '-30:+0',
                    buttonImage: '/images/calendar.png',
                    buttonImageOnly: true
                });
            }

            function bindPhoneMask() {
                $(ContactPhoneBoxID).inputmask("mask", { "mask": "999-999-9999" });
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
                modalDialog('#form-duplicate-modal');
            }

            function invalidRegistration() {
                gotoPage(1);
                modalDialog('#form-invalid-modal');
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

                $(ContestantLastNameID).on('input', function () {
                    $('#page2ErrorMessage').html('');
                });
                $(ContestantFirstNameID).on('input', function () {
                    $('#page2ErrorMessage').html('');
                });
                $(ContestantEmailID).on('input', function () {
                    $('#page2ErrorMessage').html('');
                });
                $(ContestantBirthdayID).on('input', function () {
                    $('#page2ErrorMessage').html('');
                });
                $(ContestantSchool1ID).on('input', function () {
                    $('#page2ErrorMessage').html('');
                });
                $(ContestantSchool2ID).on('input', function () {
                    $('#page2ErrorMessage').html('');
                });
            }

            function clearInputForm() {
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

                $(CompetitionCategoryListID + ' input:checkbox').prop('checked', false);
                $(CompetitionDivisionListID).prop('selectedIndex', 0);
                $(CompetitionClassOptionsID + ' input:radio').prop('checked', false);
                $('input[name="talentShowSubcategory"]').prop('checked', false); // clear subcategory UI
                $(TalentShowIsPianoRequiredID).prop('checked', false);
                $(TalentShowSubCategoryID).val(''); // clear subcategory when postback.
                $(CompetitionClassOptionsID).hide();
                $(CompetitionCategoryListID).hide();

                $(ContactNameBoxID).val('');
                $(ContactEmailBoxID).val('');
                $(ContactPhoneBoxID).val('');

                $('#page1ErrorMessage').html(''); // reset error
                $('#pageErrorRow1').hide();
                $('#page2ErrorMessage').html(''); // reset error
                $('#pageErrorRow2').hide();

                return false;
            }

            function validateInput() {
                //if ($(RegistationStartFlagID).val() == '0') return true; // no validation if the registration is not started yet

                var count = 0;

                count += ($(ContactNameBoxID).val() == '') ? 1 : 0;
                count += ($(ContactEmailBoxID).val() == '') ? 1 : 0;
                count += ($(ContactPhoneBoxID).val() == '') ? 1 : 0;

                count += getSelectedCategories() == 0 ? 1 : 0;;
                count += ($(CompetitionDivisionListID).prop('selectedIndex') == 0) ? 1 : 0;

                count += ($(ContestantLastNameID).val() == '') ? 1 : 0;
                count += ($(ContestantFirstNameID).val() == '') ? 1 : 0;
                count += ($(ContestantEmailID).val() == '') ? 1 : 0;
                count += ($(ContestantBirthdayID).val() == '') ? 1 : 0;
                count += ($(ContestantSchool1ID).val() == '') ? 1 : 0;
                var grade = $(ContestantGradeListID).get(0).selectedIndex;
                count += (grade <= 0) ? 1 : 0;

                if ($('#competitionClass').html().trim() != '') {
                    count += ($(CompetitionClassOptionsID + ' input:checked').length > 0) ? 0 : 1;
                }

                if (count > 0) {
                    if (count == 1)
                        $('#page1ErrorMessage').html('One required input is not provided.');
                    else
                        $('#page1ErrorMessage').html(count + ' required input are not provided.');
                    $('#pageErrorRow1').show();
                }
                else {
                    if (!isValidDate($(ContestantBirthdayID).val())) {
                        $('#page1ErrorMessage').html("The contestant's birthday either is out of range or needs to be in the form of mm/dd/yyyy.");
                        $('#pageErrorRow1').show();
                        count++;
                    }
                    else if (!isValidEmail($(ContestantEmailID).val())) {
                        $('#page1ErrorMessage').html("The contestant's email format is not valid.");
                        $('#pageErrorRow1').show();
                        count++;
                    }
                    else if (!isValidEmail($(ContactEmailBoxID).val())) {
                        $('#page1ErrorMessage').html("The contact's email format is not valid.");
                        $('#pageErrorRow1').show();
                        count++;
                    }
                    else if (!isValidPhone($(ContactPhoneBoxID).val())) {
                        $('#page1ErrorMessage').html("The contact's phone # needs to be all digits with format of xxx-xxx-xxxx.");
                        $('#pageErrorRow1').show();
                        count++;
                    }
                    else if (isTalentShowSelected && $(TalentShowSubCategoryID).val() == '') {
                        $('#page1ErrorMessage').html("Please select a subcategory for talent show.");
                        $('#pageErrorRow1').show();
                        count++;
                    }
                }

                if (!isTalentShowSelected) $(TalentShowSubCategoryID).val('');

                return count == 0;
            }

            function gotoPage(page) {
                if (page != 1) {
                    $('.ce-temp-note').hide();
                }
                else {
                    $('.ce-temp-note').show();
                }

                $('#page1ErrorMessage').html();
                $('#pageErrorRow1').hide();
                $('#page2ErrorMessage').html();
                $('#pageErrorRow2').hide();

                $(ApplicationPage1ID).hide();
                $(ApplicationPage2ID).hide();
                $(ApplicationPage3ID).hide();

                switch (page) {
                    case 1: $(ApplicationPage1ID).show(); break;
                    case 2: setPaymentAmount();
                        $(ApplicationPage2ID).show();
                        break;
                    case 3: $(ApplicationPage3ID).show(); break;
                }
                return false;
            }

            function setPaymentAmount() {
                var count = getSelectedCategories();
                var freeLunch = $(YesLunchID).prop('checked') == true;
                if (count > 0 && !freeLunch) {
                    var fee = '' + (count * 10);
                    $(PaymentAmountListID).val(fee);
                    $(RegistrationFeeID).val(fee);
                    $(PaypalPaymentButtonID).show();
                    $(FeeWaiverButtonID).hide();
                }
                else {
                    if (count <= 1)
                        $(PaymentAmountListID + " option[value='0']").text(count + ' competition category $0');
                    else
                        $(PaymentAmountListID + " option[value='0']").text(count + ' competition categories $0');
                    $(PaymentAmountListID).val("0");
                    $(RegistrationFeeID).val("0");
                    $(FeeWaiverButtonID).show();
                    $(PaypalPaymentButtonID).hide();
                }
                return true;
            }

            function getSelectedCategories() {
                var count = $(CompetitionCategoryListID + ' input:checkbox:checked').length;
                if ($(TalentShowIsPianoRequiredID).val() == 'true') count--;
                return count;
            }

            function paymentCancel() {
                gotoPage(2);
            }

            function paymentReceived() {
                gotoPage(3);
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
                    source: availableSchools,
                    minLength: 0
                }).on("focus", function () {
                    $(this).autocomplete('search', $(this).val());
                });

                $(ContestantSchool2ID).autocomplete({
                    source: availableSchools,
                    minLength: 0
                }).on("focus", function () {
                    $(this).autocomplete('search', $(this).val());
                });

                // this one hidden help text is causing the autocomplete box to shift downward; so we remove it as we don't need it.
                $('span.ui-helper-hidden-accessible').remove();
            }

            bindFormDataFromCookie = function () {
                var savedForm = CEApp.Cookie.getCookie({ 'cookieName': 'CE_INDIVIDUAL_ENTRY' });
                if (savedForm != null) {
                    var formDataJson = JSON.parse(savedForm);
                    $(ContactNameBoxID).val(formDataJson.contactName);
                    $(ContactEmailBoxID).val(formDataJson.contactEmail);
                    $(ContactPhoneBoxID).val(formDataJson.contactPhone);
                    $.each(formDataJson.category, function(i, cat) {
                        $(CompetitionCategoryListID + ' [type=checkbox][value=' + cat + ']').prop('checked', true);
                    });
                    $(CompetitionDivisionListID).val(formDataJson.division);
                    $(CompetitionClassOptionsID + ' input:radio[value=' + formDataJson.class + ']').prop('checked', true);
                    $(TalentShowSubCategoryID).val(formDataJson.subcategory);
                    $(TalentShowIsPianoRequiredID).val(formDataJson.ispianorequired);
                    $(ContestantLastNameID).val(formDataJson.lastName);
                    $(ContestantFirstNameID).val(formDataJson.firstName);
                    $(ContestantChineseNameID).val(formDataJson.chineseName);
                    $(ContestantBirthdayID).val(formDataJson.birthday);
                    $(ContestantSchool1ID).val(formDataJson.school);
                    $(ContestantSchool2ID).val(formDataJson.otherSchool);
                    $(ContestantEmailID).val(formDataJson.email);
                    $(ContestantGradeListID).val(formDataJson.grade);
                    $(YesLunchID).prop('checked', formDataJson.lunchProgram == '0' ? false : true);
                    $(NoLunchID).prop('checked', formDataJson.lunchProgram == '0' ? true : false);
                }
            }

            function saveFormData() {
                var categories = [];
                // get the list of values for selected categories
                var checkedCategories = $(CompetitionCategoryListID + ' input:checkbox:checked');
                if (checkedCategories.length > 0) {
                    categories = checkedCategories.map(function () {
                        return this.value;
                    }).get();
                }

                var formJson = CEApp.Json.individualEntryJson(
                    {
                        'contactName': $(ContactNameBoxID).val(),
                        'contactEmail': $(ContactEmailBoxID).val(),
                        'contactPhone': $(ContactPhoneBoxID).val(),
                        'category': categories,
                        'division': $(CompetitionDivisionListID).val(),
                        'subcategory': $(TalentShowSubCategoryID).val(),
                        'ispianorequired': $(TalentShowIsPianoRequiredID.val()),
                        'class': $(CompetitionClassOptionsID + ' input:checked').val(),
                        'lastName': $(ContestantLastNameID).val(),
                        'firstName': $(ContestantFirstNameID).val(),
                        'chineseName': $(ContestantChineseNameID).val(),
                        'birthday': $(ContestantBirthdayID).val(),
                        'school': $(ContestantSchool1ID).val(),
                        'otherSchool': $(ContestantSchool2ID).val(),
                        'email': $(ContestantEmailID).val(),
                        'grade': $(ContestantGradeListID).val(),
                        'lunchProgram': $(YesLunchID).prop('checked')
                    });
                if (formJson != null) CEApp.Cookie.setCookie({ 'cookieName': 'CE_INDIVIDUAL_ENTRY' }, JSON.stringify(formJson));

                return false;
            }

        </script>

        <asp:HiddenField ID="TalentShowSubCategory" runat="Server" Value="" />
        <asp:HiddenField ID="TalentShowSubCategoryList" runat="Server" Value="" />
        <asp:HiddenField ID="TalentShowIsPianoRequired" runat="server" />
    </div>
</asp:Content>
