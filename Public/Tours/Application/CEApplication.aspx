<%@ Page Language="C#" MasterPageFile="~/CEMain.Master" AutoEventWireup="True" CodeBehind="CEApplication.aspx.cs" Inherits="CE.Pages.TourApplicationPage" %>

<%@ Register TagPrefix="CE" Namespace="CE.Pages" Assembly="CE.Application" %>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="PlaceHolderScript" runat="server">
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/ceadmin.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/themes/blue/cepage.css")%>" media="all" />
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/jquery/jquery.colorbox-min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/jquery/jquery.inputmask.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/cecookie.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/cejson.js")%>"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="ce-admin-content">
        <div class="ce-application-page">
            <asp:Panel ID="TourApplicationStartNote" runat="server">
                <div class="ce-temp-note ce-h4" style="color:#050143;border-color:red;">
                    <asp:Label ID="TourApplicationStartNoteText" runat="server">
                        Please refer to <a href="<%=ResolveUrl("~/Public/Articles/cearticle.aspx?theme=black&path=/home/about&content=ceabout")%>" style="font-size:16px;text-decoration:underline;color:black !important;">CE Status August 2016</a> for CE Tour current status.
                    </asp:Label>
                    <br /><br />
                </div>
            </asp:Panel>
            <asp:Panel ID="TourApplicationEndNote" runat="server">
                <div class="ce-temp-note ce-h4" style="color:#050143;border-color:red;">
                    <asp:Label ID="TourApplicationEndNoteText" runat="server">
                        <%= TourProgramYear %> CE Tour application is closed. We will start our first round of selection process and will notify selected applicants in the next few weeks to scehdule for an in-person interview.
                    </asp:Label>
                    <br /><br />
                </div>
            </asp:Panel>
            <asp:Panel ID="TourProgramEndNote" runat="server">
                <div class="ce-temp-note ce-h4" style="color:#050143;border-color:red;">
                    <asp:Label ID="TourProgramEndNoteText" runat="server">
                        <%= TourProgramYear %> CE Tour is currently under way. If you are interested in our program, 
                        please check back with us toward the end of the year for next year's application date. 
                        We wish you to have a wonderful year.
                    </asp:Label>
                    <br /><br />
                </div>
            </asp:Panel>

            <asp:Panel ID="TourApplicationForm" runat="server">
                <div class="page-title ce-h2" style="display: inline;">Cultural Tour Application</div>
                <div class="form-input-note"><span class="red">*</span> denotes required input</div>
                <div class="page-section-divider600"></div>
                <div id="applicationPage1" style="display:block;">
                    <table>
                        <tr>
                            <td class="form-section-title ce-h4" colspan="2">Personal Information</td>
                        </tr>
                        <tr>
                            <td class="form-left-cell600 ce-h5"><div style="width:150px;"><span class="red">*</span> First Name :</div></td>
                            <td class="form-right-cell600 ce-h5">
                                <asp:TextBox ID="FirstNameBox" runat="server" CssClass="ce-h4" Width="470px" />
                                <asp:RequiredFieldValidator ID="FirstNameRequiredFieldValidator" runat="server" ControlToValidate="FirstNameBox" ErrorMessage="First Name field is required." CssClass="form-error-text" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-left-cell600 ce-h5"><span class="red">*</span> Last Name :</td>
                            <td class="form-right-cell600 ce-h5">
                                <asp:TextBox ID="LastNameBox" runat="server" CssClass="ce-h4" Width="470px" />
                                <asp:RequiredFieldValidator ID="LastNameRequiredFieldValidator" runat="server" ControlToValidate="LastNameBox" ErrorMessage="Last Name field is required." CssClass="form-error-text" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-left-cell600 ce-h5"><span class="red">*</span> Email :</td>
                            <td class="form-right-cell600 ce-h5">
                                <asp:TextBox ID="EmailBox" runat="server" CssClass="ce-h4" Width="470px" TextMode="Email" />
                                <asp:RequiredFieldValidator ID="EmailRequiredFieldValidator" runat="server" ControlToValidate="EmailBox" ErrorMessage="Email field is required." CssClass="form-error-text" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-left-cell600 ce-h5">Daytime Phone :</td>
                            <td class="form-right-cell600 ce-h5">
                                <asp:TextBox ID="PhoneBox" runat="server" CssClass="ce-h4" Width="470px" /></td>
                        </tr>
                        <tr>
                            <td class="form-left-cell600 ce-h5">Mobile Phone :</td>
                            <td class="form-right-cell600 ce-h5">
                                <asp:TextBox ID="CellPhoneBox" runat="server" CssClass="ce-h4" Width="470px" /></td>
                        </tr>
                        <tr>
                            <td class="form-left-cell600 ce-h5"><span class="red">*</span> School District :</td>
                            <td class="form-right-cell600 ce-h5">
                                <asp:TextBox ID="DistrictBox" runat="server" CssClass="ce-h4" Width="470px" />
                                <asp:RequiredFieldValidator ID="DistrictRequiredFieldValidator" runat="server" ControlToValidate="DistrictBox" ErrorMessage="School District field is required." CssClass="form-error-text" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-left-cell600 ce-h5"><span class="red">*</span> School Name :</td>
                            <td class="form-right-cell600 ce-h5">
                                <asp:TextBox ID="SchoolBox" runat="server" CssClass="ce-h4" Width="470px" />
                                <asp:RequiredFieldValidator ID="SchoolRequiredFieldValidator" runat="server" ControlToValidate="SchoolBox" ErrorMessage="School Name field is required." CssClass="form-error-text" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-left-cell600 ce-h5"><span class="red">*</span> Grade Taught :</td>
                            <td class="form-right-cell600 ce-h5">
                                <asp:TextBox ID="GradeBox" runat="server" CssClass="ce-h4" Width="470px" />
                                <asp:RequiredFieldValidator ID="GradeRequiredFieldValidator" runat="server" ControlToValidate="GradeBox" ErrorMessage="Grade Taught field is required." CssClass="form-error-text" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-left-cell600 ce-h5"><span class="red">*</span> Subject Taught :</td>
                            <td class="form-right-cell600 ce-h5">
                                <asp:TextBox ID="SubjectBox" runat="server" CssClass="ce-h4" Width="470px" />
                                <asp:RequiredFieldValidator ID="SubjectRequiredFieldValidator" runat="server" ControlToValidate="SubjectBox" ErrorMessage="Subject Taught field is required." CssClass="form-error-text" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-left-cell600 ce-h5"><span class="red">*</span> Gender:</td>
                            <td class="form-right-cell600 ce-h5">
                                <asp:RadioButton ID="MaleGender" runat="server" CssClass="cell-button" Text=" Male" GroupName="gender" Checked="true" Width="60px" />
                                <asp:RadioButton ID="FemaleGender" runat="server" CssClass="cell-button" Text=" Female" GroupName="gender" Checked="false" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div class="page-section-divider600"></div>
                            </td>
                        </tr>
                        <tr id="pageErrorRow1" class="hide">
                            <td id="page1ErrorMessage" class="form-message red" colspan="2">
                                <asp:Literal ID="Page1ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-button-cell" colspan="2">
                                <asp:Button ID="Page1NextButton" runat="server" CssClass="action-button" Font-Bold="true" Text="  Next  >>  " OnClientClick="return ensureInput(1,2);" />
                                <asp:Button ID="Page1ClearButton" runat="server" CssClass="action-button" Text="  Clear Form  " OnClientClick="return clearPage(1);" />
                                <asp:Button ID="Page1SaveButton" runat="server" CssClass="action-button" Text="  Save Form Data " OnClientClick="return saveFormData();" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="applicationPage2" style="display:none;">
                    <table>
                        <tr>
                            <td class="form-section-title ce-h4">Please answer the following questions (4000 characters or less for each question)</td>
                        </tr>
                        <tr>
                            <td class="form-span-cell600 ce-h5">
                                <div class="form-cell-title"><span class="red">*</span> How did you learn about our program?</div>
                                <div class="form-cell-text">
                                    <asp:TextBox ID="LearnProgramBox" MaxLength="4096" CssClass="ce-h4" Height="120px" Width="630px" TextMode="MultiLine" runat="server" />
                                    <asp:RequiredFieldValidator ID="LearnProgramRequiredFieldValidator" runat="server" ControlToValidate="LearnProgramBox" ErrorMessage="This field is required." CssClass="form-error-text" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-span-cell600 ce-h5">
                                <div class="form-cell-title"><span class="red">*</span> What is your teaching subject and professional specialty?</div>
                                <div class="form-cell-text">
                                    <asp:TextBox ID="SpecialtyBox" MaxLength="4096" CssClass="ce-h4" Height="120px" Width="630px" TextMode="MultiLine" runat="server" />
                                    <asp:RequiredFieldValidator ID="SpecialtyRequiredFieldValidator" runat="server" ControlToValidate="SpecialtyBox" ErrorMessage="This field is required." CssClass="form-error-text" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-span-cell600 ce-h5">
                                <div class="form-cell-title">
                                    <span class="red">*</span> Please list three references
                                    <div style="padding-left:8px;">(<i>Preferably one each from your school’s administration, a student, and a colleague. You will be asked to provide reference letters when selected for interview.</i>)</div>
                                </div>
                                <div class="form-cell-text">
                                    <div class="form-cell-text">
                                        <asp:TextBox ID="Reference1Box" MaxLength="1024" CssClass="ce-h4" Width="630px" runat="server" />
                                        <asp:RequiredFieldValidator ID="Reference1RequiredFieldValidator" runat="server" ControlToValidate="Reference1Box" ErrorMessage="Reference 1 field is required." CssClass="form-error-text" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-cell-text">
                                        <asp:TextBox ID="Reference2Box" MaxLength="1024" CssClass="ce-h4" Width="630px" runat="server" />
                                        <asp:RequiredFieldValidator ID="Reference2RequiredFieldValidator" runat="server" ControlToValidate="Reference2Box" ErrorMessage="Reference 2 field is required." CssClass="form-error-text" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-cell-text">
                                        <asp:TextBox ID="Reference3Box" MaxLength="1024" CssClass="ce-h4" Width="630px" runat="server" />
                                        <asp:RequiredFieldValidator ID="Reference3RequiredFieldValidator" runat="server" ControlToValidate="Reference3Box" ErrorMessage="Reference 3 field is required." CssClass="form-error-text" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="page-section-divider600"></div>
                            </td>
                        </tr>
                        <tr id="pageErrorRow2">
                            <td id="page2ErrorMessage" class="form-message red">
                                <asp:Literal ID="Page2ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-button-cell">
                                <asp:Button ID="Page2PreviousButton" runat="server" CssClass="action-button" Text="  <<  Previous  " OnClientClick="return gotoNextPage(1);" />
                                <asp:Button ID="Page2NextButton" runat="server" CssClass="action-button" Font-Bold="true" Text="  Next  >>  " OnClientClick="return ensureInput(2,3);" />
                                <asp:Button ID="Page2ClearButton" runat="server" CssClass="action-button" Text="  Clear Form  " OnClientClick="return clearPage(2);" />
                                <asp:Button ID="Page2SaveButton" runat="server" CssClass="action-button" Text="  Save Form Data " OnClientClick="return saveFormData();" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="applicationPage3" style="display:none;">
                    <table>
                        <tr>
                            <td class="form-section-title ce-h4">Questionnaire (4000 characters or less for each question)</td>
                        </tr>
                        <tr>
                            <td class="form-span-cell600 ce-h4">
                                <div class="form-cell-title"><span class="red">*</span> How do you relate your current teaching or work experiences to our program?</div>
                                <div class="form-cell-text">
                                    <asp:TextBox ID="Questionaire1Box" MaxLength="4096" CssClass="ce-h4" Height="120px" Width="630px" TextMode="MultiLine" runat="server" />
                                    <asp:RequiredFieldValidator ID="Questionaire1RequiredFieldValidator" runat="server" ControlToValidate="Questionaire1Box" ErrorMessage="This field is required." CssClass="form-error-text" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-span-cell600 ce-h4">
                                <div class="form-cell-title">
                                    <span class="red">*</span>
                                        How have you incorporated a prior experience to augment a lesson plan?
                                        <div style="padding-left:8px;">(<i>Please provide a sample lesson plan developed by you in the next screen.</i>)</div>
                                </div>
                                <div class="form-cell-text">
                                    <asp:TextBox ID="Questionaire2Box" MaxLength="4096" CssClass="ce-h4" Height="120px" Width="630px" TextMode="MultiLine" runat="server" />
                                    <asp:RequiredFieldValidator ID="Questionaire2RequiredFieldValidator" runat="server" ControlToValidate="Questionaire2Box" ErrorMessage="This field is required." CssClass="form-error-text" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-span-cell600 ce-h4">
                                <div class="form-cell-title"><span class="red">*</span> What are your short-term and long-term teaching goals?</div>
                                <div class="form-cell-text">
                                    <asp:TextBox ID="Questionaire3Box" MaxLength="4096" CssClass="ce-h4" Height="120px" Width="630px" TextMode="MultiLine" runat="server" />
                                    <asp:RequiredFieldValidator ID="Questionaire3RequiredFieldValidator" runat="server" ControlToValidate="Questionaire3Box" ErrorMessage="This field is required." CssClass="form-error-text" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-span-cell600 ce-h4">
                                <div class="form-cell-title">
                                    <span class="red">*</span> 
                                    How do you expect this travel experience to benefit your students and your teaching?
                                    <div style="padding-left:8px;">(<i>If you are selected for interview, you will be asked to present a lesson plan you will develop as a result of your trip.</i>)</div>
                                </div>
                                <div class="form-cell-text">
                                    <asp:TextBox ID="Questionaire4Box" MaxLength="4096" CssClass="ce-h4" Height="120px" Width="630px" TextMode="MultiLine" runat="server" />
                                    <asp:RequiredFieldValidator ID="Questionaire4RequiredFieldValidator" runat="server" ControlToValidate="Questionaire4Box" ErrorMessage="This field is required." CssClass="form-error-text" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="page-section-divider600"></div>
                            </td>
                        </tr>
                        <tr id="pageErrorRow3" class="hide">
                            <td id="page3ErrorMessage" class="form-message red">
                                <asp:Literal ID="Page3ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-button-cell">
                                <asp:Button ID="Page3PreviousButton" runat="server" CssClass="action-button" Text="  <<  Previous  " OnClientClick="return gotoNextPage(2);" />
                                <asp:Button ID="Page3NextButton" runat="server" CssClass="action-button" Font-Bold="true" Text="  Next  >>  " OnClientClick="return ensureInput(3,4);" />
                                <asp:Button ID="Page3ClearButton" runat="server" CssClass="action-button" Text="  Clear Form  " OnClientClick="return clearPage(3);" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="applicationPage4" style="display:none;">
                    <table>
                        <tr>
                            <td class="form-section-title ce-h4"><span class="red">*</span> Resume</td>
                        </tr>
                        <tr>
                            <td>
                                <div class="form-cell-text">Attach your resume for the application (10Mb or less, PDF please):</div>
                                <div>
                                    <asp:TextBox ID="ApplicantResume" runat="server" CssClass="form-upload-file" Enabled="false" />
                                    <div class="form-upload">
                                        <img runat="server" id="resumeImg" src="~/images/pin_blue.png" />
                                        <img runat="server" id="resumeRemoveImg" src="~/images/eraser_small.png" onclick="clearFilename(1);" />
                                    </div>
                                </div>
                                <div id="resumeSizeError"></div>
                                <div class="hide"><input id="AttachResumeFile" type="file" accept="application/pdf" runat="server" /></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-section-title ce-h4"><span class="red">*</span> References</td>
                        </tr>
                        <tr>
                            <td>
                                <div class="form-cell-text">Attach your lesson plan example (10Mb or less, PDF please):</div>
                                <div>
                                    <asp:TextBox ID="ApplicantTeachingPlan" runat="server" CssClass="form-upload-file" Enabled="false" />
                                    <div class="form-upload">
                                        <img runat="server" id="teachingPlanImg" src="~/images/pin_blue.png" />
                                        <img runat="server" id="teachingPlanRemoveImg" src="~/images/eraser_small.png" onclick="clearFilename(2);" />
                                    </div>
                                </div>
                                <div id="teachingPlanSizeError"></div>
                                <div class="hide"><input id="AttachLessonPlan" type="file" accept="application/pdf" runat="server" /></div>
                            </td>
                        </tr>
                        <tr>
                            <td><div class="form-row-spacing"></div></td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <asp:CheckBox ID="SkipUpload" runat="server" Text=" " />&nbsp;&nbsp;Check this box if you can't upload files. 
                                    Please <a style="color:blue !important;text-decoration:underline;" href="mailto:ceadmin@culturalexploration.org">mail the files to us</a> after you submit the application.
                                </div>
                            </td>
                        </tr>
                            <tr>
                            <td><div class="form-row-spacing"></div></td>
                        </tr>
                        <tr>
                            <td class="form-section-title ce-h4">Supporting Materials</td>
                        </tr>
                        <tr>
                            <td>
                                <div class="form-cell-text">Attach additional materials you might like to share with us (10Mb or less, PDF please):</div>
                                <div>
                                    <asp:TextBox ID="ApplicantFile1" runat="server" CssClass="form-upload-file" Enabled="false" />
                                    <div class="form-upload">
                                        <img runat="server" id="applicantFile1Img" src="~/images/pin_blue.png" />
                                        <img runat="server" id="userFile1RemoveImg" src="~/images/eraser_small.png" onclick="clearFilename(3);" />
                                    </div>
                                    <div id="userFile1SizeError"></div>
                                </div>
                                <div class="hide"><input id="AttachApplicantFile1" type="file" accept="application/pdf" runat="server" /></div>
                                    <div>
                                    <asp:TextBox ID="ApplicantFile2" runat="server" CssClass="form-upload-file" Enabled="false" />
                                    <div class="form-upload">
                                        <img runat="server" id="applicantFile2Img" src="~/images/pin_blue.png" />
                                        <img runat="server" id="userFile2RemoveImg" src="~/images/eraser_small.png" onclick="clearFilename(4);" />
                                    </div>
                                    <div id="userFile2SizeError"></div>
                                </div>
                                <div class="hide"><input id="AttachApplicantFile2" type="file" accept="application/pdf" runat="server" /></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-span-cell600 ce-h5">
                                <div class="form-cell-title">Additional comment you might have:</div>
                                <div class="form-cell-text"><asp:TextBox ID="UserComment" MaxLength="4096" CssClass="ce-h4" Height="120px" Width="630px" TextMode="MultiLine" runat="server" /></div>
                            </td>
                        </tr>
                        <tr>
                            <td><div class="form-row-spacing"></div></td>
                        </tr>
                        <tr>
                            <td>
                                <div class="page-section-divider600"></div>
                            </td>
                        </tr>
                        <tr id="pageErrorRow4" class="hide">
                            <td id="page4ErrorMessage" class="form-message red">
                                <asp:Literal ID="Page4ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-button-cell">
                                <asp:Button ID="Page4PreviousButton" runat="server" CssClass="action-button" Text="  <<  Previous  " OnClientClick="return gotoNextPage(3);" />
                                <asp:Button ID="SubmitButton" runat="server" Enabled="false" CssClass="action-button" Font-Bold="true" Text="  Submit  " OnClientClick="return validateApplication();" OnClick="OnApplciationSubmit" />
                                <asp:Button ID="Page4ClearButton" runat="server" CssClass="action-button" Text="  Clear Form  " OnClientClick="return clearPage(4);" />
                                <asp:Button ID="CancelButton" runat="server" CssClass="action-button" Text="  Cancel  " OnClick="OnCancel" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </div>
    </div>
    <div class="hide">
        <div id="form-success-modal">
            <div class="application-result-title"><span class="ce-h3">Application Submission Acknowledgement</span></div>
            <div class="application-result-text ce-h4">
                <table>
                    <tr>
                        <td style="width:60px;"><img runat="server" src="~/images/confirm.png" /></td>
                        <td>
                            Thanks for fill out the cultural tour application with us. We have sent an email to confirm that we have received your application.
                            Please check your email for further instruction about our process and how to proceed going forward.
                        </td>
                    </tr>
                </table>
            </div>
            <div class="application-result-button"><asp:Button ID="Submitted" OnClientClick="return closeDialog();" CssClass="ce-h4" Text="  Close  " runat="server" /></div>
        </div>
    </div>
    <div class="hide">
        <div id="form-error-modal">
            <div class="application-result-title"><span class="ce-h3">Resume/Lesson Plan Upload Problem</span></div>
            <div class="application-result-text ce-h4">
                <table>
                    <tr>
                        <td style="width:60px;"><img runat="server" src="~/images/error.png" /></td>
                        <td>
                            There is a problem saving your attached file(s) to our server. Please ensure the filename is valid and try again. 
                            If the problem persists, please send all your input and attachments to <a href="mailto:ceadmin@culturalExploration.org">CE Administrator</a>.
                        </td>
                    </tr>
                </table>
            </div>
            <div class="application-result-button"><asp:Button ID="Closed" OnClientClick="return exitDialog();" CssClass="ce-h4" Text="  Close  " runat="server" /></div>
        </div>
    </div>
    
    <div>
        <asp:HiddenField ID="ApplicationStartFlag" runat="server" Value="0" />
        <asp:HiddenField ID="ApplincantFile1Removed" runat="server" Value="0" />
        <asp:HiddenField ID="ApplincantFile2Removed" runat="server" Value="0" />
    </div>

    <div>
        <script type="text/javascript">
            // page 1
            var FirstNameID = '#<%= FirstNameBox.ClientID %>';
            var LastNameID = '#<%= LastNameBox.ClientID %>';
            var EmailID = '#<%= EmailBox.ClientID %>';
            var PhoneID = '#<%= PhoneBox.ClientID %>';
            var CellPhoneID = '#<%= CellPhoneBox.ClientID %>';
            var DistrictID = '#<%= DistrictBox.ClientID %>';
            var SchoolID = '#<%= SchoolBox.ClientID %>';
            var GradeID = '#<%= GradeBox.ClientID %>';
            var SubjectID = '#<%= SubjectBox.ClientID %>';
            var MaleGenderID = '#<%= MaleGender.ClientID %>';
            var FemaleGenderID = '#<%= FemaleGender.ClientID %>';
            var Page1ErrorMessageID = '#<%= Page1ErrorMessage.ClientID %>';
            // page 2
            var LearnProgramID = '#<%= LearnProgramBox.ClientID %>';
            var SpecialtyID = '#<%= SpecialtyBox.ClientID %>';
            var Reference1ID = '#<%= Reference1Box.ClientID %>';
            var Reference2ID = '#<%= Reference2Box.ClientID %>';
            var Reference3ID = '#<%= Reference3Box.ClientID %>';
            var Page2ErrorMessageID = '#<%= Page2ErrorMessage.ClientID %>';
            // page 3
            var Questionaire1ID = '#<%= Questionaire1Box.ClientID %>';
            var Questionaire2ID = '#<%= Questionaire2Box.ClientID %>';
            var Questionaire3ID = '#<%= Questionaire3Box.ClientID %>';
            var Questionaire4ID = '#<%= Questionaire4Box.ClientID %>';
            var Page3ErrorMessageID = '#<%= Page3ErrorMessage.ClientID %>';
            // page 4
            var Page4ErrorMessageID = '#<%= Page4ErrorMessage.ClientID %>';
            var ApplicantResumeID = '#<%= ApplicantResume.ClientID %>';
            var SkipUploadID = '#<%= SkipUpload.ClientID %>';
            var ApplicantTeachingPlanID = '#<%= ApplicantTeachingPlan.ClientID %>';
            var ApplicantFile1ID = '#<%= ApplicantFile1.ClientID %>';
            var ApplicantFile2ID = '#<%= ApplicantFile2.ClientID %>';
            var AttachResumeFileID = '#<%= AttachResumeFile.ClientID %>';
            var AttachLessonPlanID = '#<%= AttachLessonPlan.ClientID %>';
            var AttachApplicantFile1ID = '#<%= AttachApplicantFile1.ClientID %>';
            var AttachApplicantFile2ID = '#<%= AttachApplicantFile2.ClientID %>';
            var UserCommentID = '#<%= UserComment.ClientID %>';

            var ApplicationStartFlagID = '#<%= ApplicationStartFlag.ClientID %>';
            var ApplincantFile1RemovedID = '#<%= ApplincantFile1Removed.ClientID %>';
            var ApplincantFile2RemovedID = '#<%= ApplincantFile2Removed.ClientID %>';

            var tourEntryCookie = 'CE_TOUR_ENTRY';

            $(document).ready(function () {
                installErrorResetEvents();
                bindPhoneMask();
                bindFormDataFromCookie();
                clearErrorMessages();

                $('.form-upload #resumeImg').bind('click', function () {
                    $(AttachResumeFileID).click();
                });

                $('.form-upload #teachingPlanImg').bind('click', function () {
                    $(AttachLessonPlanID).click();
                });

                $('.form-upload #applicantFile1Img').bind('click', function () {
                    $(AttachApplicantFile1ID).click();
                });

                $('.form-upload #applicantFile2Img').bind('click', function () {
                    $(AttachApplicantFile2ID).click();
                });

                $(AttachResumeFileID).bind('change', function () {
                    var filename = '';
                    filename = $(this).val();
                    if (filename != null && filename.toLocaleLowerCase().endsWith('.pdf')) {
                        $(ApplicantResumeID).val(filename);
                        $('#page4ErrorMessage').html('');
                    }
                    else if (filename != null && filename != '') {
                        $(ApplicantResumeID).val('');
                        alert('Please attach a PDF file.');
                    }
                }).change();

                $(AttachLessonPlanID).bind('change', function () {
                    var filename = '';
                    filename = $(this).val();
                    if (filename != null && filename.toLocaleLowerCase().endsWith('.pdf')) {
                        $(ApplicantTeachingPlanID).val(filename);
                        $('#page4ErrorMessage').html('');
                    }
                    else if (filename != null && filename != '') {
                        $(ApplicantTeachingPlanID).val('');
                        alert('Please attach a PDF file.');
                    }
                }).change();

                $(AttachApplicantFile1ID).bind('change', function () {
                    var filename = '';
                    filename = $(this).val();
                    if (filename != null && filename.toLocaleLowerCase().endsWith('.pdf')) {
                        $(ApplicantFile1ID).val(filename);
                        $('#page4ErrorMessage').html('');
                        $(ApplincantFile1RemovedID).val('0');
                    }
                    else if (filename != null && filename != '') {
                        $(ApplicantFile1ID).val('');
                        $(ApplincantFile1RemovedID).val('1');
                        alert('Please attach a PDF file.');
                    }
                }).change();

                $(AttachApplicantFile2ID).bind('change', function () {
                    var filename = '';
                    filename = $(this).val();
                    if (filename != null && filename.toLocaleLowerCase().endsWith('.pdf')) {
                        $(ApplicantFile2ID).val(filename);
                        $('#page4ErrorMessage').html('');
                        $(ApplincantFile2RemovedID).val('0');
                    }
                    else if (filename != null && filename != '') {
                        $(ApplicantFile2ID).val('');
                        $(ApplincantFile2RemovedID).val('1');
                        alert('Please attach a PDF file.');
                    }
                }).change();
            });

            function bindPhoneMask() {
                $(PhoneID).inputmask("mask", { "mask": "999-999-9999" });
                $(CellPhoneID).inputmask("mask", { "mask": "999-999-9999" });
            }

            function applicationSubmitted() {
                bindPhoneMask();
                modalDialog('#form-success-modal');
            }

            function saveUploadFileError(resume, lessonPlan) {
                bindPhoneMask();
                gotoNextPage(4);
                modalDialog('#form-error-modal');
            }

            function installErrorResetEvents() {
                // page 1
                $(FirstNameID).on('input', function () {
                    $('#page1ErrorMessage').html('');
                });
                $(LastNameID).on('input', function () {
                    $('#page1ErrorMessage').html('');
                });
                $(EmailID).on('input', function () {
                    $('#page1ErrorMessage').html('');
                });
                $(PhoneID).on('input', function () {
                    $('#page1ErrorMessage').html('');
                });
                $(CellPhoneID).on('input', function () {
                    $('#page1ErrorMessage').html('');
                });
                $(DistrictID).on('input', function () {
                    $('#page1ErrorMessage').html('');
                });
                $(SchoolID).on('input', function () {
                    $('#page1ErrorMessage').html('');
                });
                $(GradeID).on('input', function () {
                    $('#page1ErrorMessage').html('');
                });
                $(SubjectID).on('input', function () {
                    $('#page1ErrorMessage').html('');
                });
                // page 2
                $(LearnProgramID).on('input', function () {
                    $('#page2ErrorMessage').html('');
                });
                $(SpecialtyID).on('input', function () {
                    $('#page2ErrorMessage').html('');
                });
                $(Reference1ID).on('input', function () {
                    $('#page2ErrorMessage').html('');
                });
                $(Reference2ID).on('input', function () {
                    $('#page2ErrorMessage').html('');
                });
                $(Reference3ID).on('input', function () {
                    $('#page2ErrorMessage').html('');
                });
                // page 3
                $(Questionaire1ID).on('input', function () {
                    $('#page3ErrorMessage').html('');
                });
                $(Questionaire2ID).on('input', function () {
                    $('#page3ErrorMessage').html('');
                });
                $(Questionaire3ID).on('input', function () {
                    $('#page3ErrorMessage').html('');
                });
                $(Questionaire4ID).on('input', function () {
                    $('#page3ErrorMessage').html('');
                });
                // page 4
                $(ApplicantResumeID).on('input', function () {
                    $('#page4ErrorMessage').html('');
                });
                $(ApplicantTeachingPlanID).on('input', function () {
                    $('#page4ErrorMessage').html('');
                });
                $(ApplicantFile1ID).on('input', function () {
                    $('#page4ErrorMessage').html('');
                });
                $(ApplicantFile2ID).on('input', function () {
                    $('#page4ErrorMessage').html('');
                });
            }

            function clearPage(page) {
                if (page == 1) {
                    $(FirstNameID).val('');
                    $(LastNameID).val('');
                    $(EmailID).val('');
                    $(PhoneID).val('');
                    $(CellPhoneID).val('');
                    $(DistrictID).val('');
                    $(SchoolID).val('');
                    $(GradeID).val('');
                    $(SubjectID).val('');
                    $('#page1ErrorMessage').html('');
                    $('#pageErrorRow1').hide();
                }
                else if (page == 2) {
                    $(LearnProgramID).val('');
                    $(SpecialtyID).val('');
                    $(Reference1ID).val('');
                    $(Reference2ID).val('');
                    $(Reference3ID).val('');
                    $('#page2ErrorMessage').html('');
                    $('#pageErrorRow2').hide();
                }
                else if (page == 3) {
                    $(Questionaire1ID).val('');
                    $(Questionaire2ID).val('');
                    $(Questionaire3ID).val('');
                    $(Questionaire4ID).val('');
                    $('#page3ErrorMessage').html('');
                    $('#pageErrorRow3').hide();
                }
                else if (page == 4) {
                    $(ApplicantResumeID).val('');
                    $(ApplicantTeachingPlanID).val('');
                    $(ApplicantFile1ID).val('');
                    $(ApplicantFile2ID).val('');
                    $('#page4ErrorMessage').html('');
                    $('#pageErrorRow4').hide();
                }
                //gotoNextPage(page);
                return false;
            }

            function ensureInput(page, nextPage) {
                var next = false;
                if ($(ApplicationStartFlagID).val() == '0') {
                    next = true;
                }
                else {
                    var count = pageValidation(page);
                    if (count > 0) {
                        switch (page) {
                            case 1:
                                $('#page1ErrorMessage').html('Not all required input are filled.');
                                $('#pageErrorRow1').show();
                                break;
                            case 2:
                                $('#page2ErrorMessage').html('Not all required input are filled.');
                                $('#pageErrorRow2').show();
                                break;
                            case 3:
                                $('#page3ErrorMessage').html('Not all required input are filled.');
                                $('#pageErrorRow3').show();
                                break;
                            case 4:
                                $('#page4ErrorMessage').html('A resume and lesson plan sample are required to complete the application.');
                                $('#pageErrorRow4').show();
                                break;
                        }
                    }
                    else {
                        if (page == 1 && !isValidEmail($(EmailID).val())) {
                            $('#page1ErrorMessage').html('The email you have entered contains invalid character(s).');
                            $('#pageErrorRow1').show();
                        }
                        else {
                            next = true;
                        }
                    }
                }

                if (next) {
                    $('#applicationPage' + page).hide();
                    $('#applicationPage' + nextPage).show();
                }

                return false;
            }

            function pageValidation(page) {
                if ($(ApplicationStartFlagID).val() == '0') return 0; // no validation if the application is not started yet

                var count = 0;
                if (page == 1) {
                    count += ($(FirstNameID).val().trim() == '') ? 1 : 0;
                    count += ($(LastNameID).val().trim() == '') ? 1 : 0;
                    count += ($(EmailID).val().trim() == '') ? 1 : 0;
                    count += ($(DistrictID).val().trim() == '') ? 1 : 0;
                    count += ($(SchoolID).val().trim() == '') ? 1 : 0;
                    count += ($(GradeID).val().trim() == '') ? 1 : 0;
                    count += ($(SubjectID).val().trim() == '') ? 1 : 0;
                }
                else if (page == 2) {
                    count += ($(Reference1ID).val().trim() == '') ? 1 : 0;
                    count += ($(Reference2ID).val().trim() == '') ? 1 : 0;
                    count += ($(Reference3ID).val().trim() == '') ? 1 : 0;
                }
                else if (page == 3) {
                    count += ($(Questionaire1ID).val().trim() == '') ? 1 : 0;
                    count += ($(Questionaire2ID).val().trim() == '') ? 1 : 0;
                    count += ($(Questionaire3ID).val().trim() == '') ? 1 : 0;
                    count += ($(Questionaire4ID).val().trim() == '') ? 1 : 0;
                }
                else if (page == 4) {
                    if (!$(SkipUploadID).is(':checked')) {
                        count += ($(ApplicantResumeID).val().trim() == '') ? 1 : 0;
                        count += ($(ApplicantTeachingPlanID).val().trim() == '') ? 1 : 0;
                    }
                }
                return count;
            }

            function validateApplication() {
                if ($(ApplicationStartFlagID).val() == '0') return true; // no validation if the application is not started yet

                clearErrorMessages();

                var validated = false;
                var count = pageValidation(1);
                if (count > 0) {
                    if (count == 1)
                        $('#page1ErrorMessage').html('1 required input is not filled.');
                    else
                        $('#page1ErrorMessage').html(count + ' required input are not filled.');
                    gotoNextPage(1);
                    $('#pageErrorRow1').show();
                }
                else if (!isValidEmail($(EmailID).val())) {
                    gotoNextPage(1);
                    $('#page1ErrorMessage').html('The email you have entered contains invalid character(s).');
                    $('#pageErrorRow1').show();
                }
                else if (pageValidation(2)) {
                    gotoNextPage(2);
                    $('#page2ErrorMessage').html('Not all required input are filled.');
                    $('#pageErrorRow2').show();
                }
                else if (pageValidation(3)) {
                    gotoNextPage(3);
                    $('#page3ErrorMessage').html('Not all required input are filled.');
                    $('#pageErrorRow3').show();
                }
                else if (pageValidation(4)) {
                    gotoNextPage(4);
                    $('#page4ErrorMessage').html('A resume and lesson plan sample are required to complete the application.');
                    $('#pageErrorRow4').show();
                }
                else {
                    clearErrorMessages();
                    validated = true;
                }

                return validated;
            }

            function clearErrorMessages() {
                $('#Page1ErrorMessage').html('');
                $('#Page2ErrorMessage').html('');
                $('#Page3ErrorMessage').html('');
                $('#Page4ErrorMessage').html('');
                $('#pageErrorRow1').hide();
                $('#pageErrorRow2').hide();
                $('#pageErrorRow3').hide();
                $('#pageErrorRow4').hide();

                $('.form-error-text').hide();
            }

            function clearFilename(which) {
                if (which == 1) {
                    $(ApplicantResumeID).val('');
                }
                else if (which == 2) {
                    $(ApplicantTeachingPlanID).val('');
                }
                else if (which == 3) {
                    $(ApplicantFile1ID).val('');
                    $(ApplincantFile1RemovedID).val('1');
                }
                else if (which == 4) {
                    $(ApplicantFile2ID).val('');
                    $(ApplincantFile1RemovedID).val('1');
                }
            }

            function uploadError(resuneFile, lessonPlanFile, userFile1, userFile2) {
                alert('One or more files are too large to upload. The maximum upload file size is 10Mb. Please make file size smaller and try again.');
                //if (resumeFile == 1)
                //    $('#resumeSizeError').text('Resume file size is too large to upload.');
                //else
                //    $('#resumeSizeError').text('');

                //if (lessonPlanFile == 1)
                //    $('#teachingPlanSizeError').text('Lesson Plan file size is too large to upload.');
                //else
                //    $('#teachingPlanSizeError').text('');

                //if (userFile1 == 1)
                //    $('#userFile1SizeError').text('User file 1 size is too large to upload.');
                //else
                //    $('#userFile1SizeError').text('');

                //if (userFile2 == 1)
                //    $('#userFile2SizeError').text('User file 2 size is too large to upload.');
                //else
                //    $('#userFile2SizeError').text('');

                $('#applicationPage1').hide();
                $('#applicationPage2').hide();
                $('#applicationPage3').hide();
                $('#applicationPage4').show();
            }

            bindFormDataFromCookie = function () {
                var savedForm = CEApp.Cookie.getCookie({ 'cookieName': tourEntryCookie });
                if (savedForm != null) {
                    var formDataJson = JSON.parse(savedForm);
                    // page 1
                    $(FirstNameID).val(formDataJson.firstName);
                    $(LastNameID).val(formDataJson.lastName);
                    $(EmailID).val(formDataJson.email);
                    $(PhoneID).val(formDataJson.phone);
                    $(CellPhoneID).val(formDataJson.cellphone);
                    $(DistrictID).val(formDataJson.disctrict);
                    $(SchoolID).val(formDataJson.school);
                    $(GradeID).val(formDataJson.grade);
                    $(SubjectID).val(formDataJson.subject);
                    if (formDataJson.gender == 'male')
                        $(MaleGenderID).attr('checked', 'checked');
                    else
                        $(FemaleGenderID).attr('checked', 'checked');
                    // page 2
                    $(LearnProgramID).val(formDataJson.learnProgram);
                    $(SpecialtyID).val(formDataJson.specialty);
                    $(Reference1ID).val(formDataJson.reference1);
                    $(Reference2ID).val(formDataJson.reference2);
                    $(Reference3ID).val(formDataJson.reference3);
                    // page 3 - potentially too large to saved to cookie
                }
            }

            function saveFormData() {
                var formJson = CEApp.Json.tourEntryJson(
                    {
                        // page 1
                        'firstName': $(FirstNameID).val(),
                        'lastName': $(LastNameID).val(),
                        'email': $(EmailID).val(),
                        'phone': $(PhoneID).val(),
                        'cellphone': $(CellPhoneID).val(),
                        'district': $(DistrictID).val(),
                        'school': $(SchoolID).val(),
                        'grade': $(GradeID).val(),
                        'subject': $(SubjectID).val(),
                        'gender': $(MaleGenderID).is(':checked') ? 'male' : 'female',
                        // page 2
                        'learnProgram': $(LearnProgramID).val(),
                        'specialty': $(SpecialtyID).val(),
                        'reference1': $(Reference1ID).val(),
                        'reference2': $(Reference2ID).val(),
                        'reference3': $(Reference3ID).val()
                        // page 3 - potentially too large to save to cookie
                    });

                // separate inot a few cookies to observe 4K bytes cookie limit
                if (formJson != null) CEApp.Cookie.setCookie({ 'cookieName': tourEntryCookie }, JSON.stringify(formJson));

                return false;
            }
        </script>
    </div>
</asp:Content>
