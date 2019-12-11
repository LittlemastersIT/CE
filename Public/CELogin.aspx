<%@ Page Language="C#" MasterPageFile="~/CEMain.Master" AutoEventWireup="True" CodeBehind="CELogin.aspx.cs" Inherits="CE.Pages.LoginPage" %>
<%@ Register TagPrefix="CE" Namespace="CE.Pages" Assembly="CE.Application" %>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="PlaceHolderScript" runat="server">
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/cemain.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/ceadmin.css")%>" media="all" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="ce-admin-content">
        <div class="ce-login-page">
            <div class="page-title ce-h3">Little Master Club Chinese Language and Talent Competition Site Login</div>
            <div class="page-section-divider2"></div>
            <table>
                <tr>
                    <td class="form-left-cell2 ce-h4"><span class="red">*</span> User Name :</td>
                    <td class="form-right-cell2 ce-h4"><asp:TextBox ID="UserNameBox" runat="server" CssClass="ce-h4" Width="310px" /></td>
                </tr>
                <tr>
                    <td class="form-left-cell2 ce-h4"><span class="red">*</span> Password :</td>
                    <td class="form-right-cell2 ce-h4"><asp:TextBox ID="PasswordBox" runat="server" CssClass="ce-h4" Width="310px" TextMode="Password" /></td>
                </tr>
               <tr>
                    <td colspan="2">
                        <div class="form-cell-divider2"></div>
                    </td>
                </tr>
                <tr>
                    <td id="errorMessage" class="form-message red" colspan="2" runat="server">
                        <asp:Literal ID="ErrorText" runat="server" EnableViewState="False"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="form-button-cell" colspan="2">
                        <asp:Button ID="LogineButton" runat="server" CssClass="action-button" Text=" Sign In " OnClick="OnSignIn" Font-Bold="true" />
                        <asp:Button ID="ClearButton" runat="server" CssClass="hide" Text=" Clear Form " OnClick="OnClearForm" />
                        <asp:Button ID="CancelButton" runat="server" CssClass="action-button" Text=" Cancel " OnClick="OnCancel" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div>
        <script type="text/javascript">
            function clearError() {
                $('#<%= UserNameBox.ClientID %>').on('input', function () {
                    $('#errorMessage').html('');
                });
                $('#<%= PasswordBox.ClientID %>').on('input', function () {
                    $('#errorMessage').html('');
                });
            }
            clearError();
        </script>
    </div>
</asp:Content>
