<%@ Page Language="C#" MasterPageFile="~/CEMain.Master" AutoEventWireup="true" CodeBehind="CreatePage.aspx.cs" Inherits="CE.Admin.CreatePage" %>

<%@ Register TagPrefix="CE" Namespace="CE.Pages" Assembly="CE.Application" %>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="PlaceHolderScript" runat="server">
    <link type="text/css" rel="stylesheet" href="/CSS/ceadmin.css" media="all" />
</asp:Content>

<asp:Content ID="MainContent1" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="ce-admin-content">
        <div class="ce-admin-page">
            <div class="page-title ce-h2">Create Cultural Exploration Site Page</div>
            <div class="page-section-divider"></div>
            <table>
                <tr class="cell-row">
                    <td class="form-note-cell" colspan="2"><span class="red">*</span> denotes required fields</td>
                </tr>
                <tr class="cell-row">
                    <td class="form-left-cell"><span class="red">*</span> Page Template :</td>
                    <td class="form-right-cell">
                        <asp:DropDownList ID="PageTemplateList" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="OnTemplateChanged">
                            <asp:ListItem Text="Article" Value="article" />
                            <asp:ListItem Text="Album" Value="album" />
                            <asp:ListItem Text="Journey" Value="journey" />
                            <asp:ListItem Text="Video" Value="video" />
                            <asp:ListItem Text="Admin" Value="admin" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="cell-row">
                    <td class="form-left-cell"><span class="red">*</span> Theme :</td>
                    <td class="form-right-cell">
                        <asp:DropDownList ID="ThemeList" runat="server" Width="300px">
                            <asp:ListItem Text="Black" Value="black" />
                            <asp:ListItem Text="Blue" Value="blue" />
                            <asp:ListItem Text="Green" Value="green" />
                            <asp:ListItem Text="Maroon" Value="maroon" />
                            <asp:ListItem Text="Orange" Value="orange" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="cell-row">
                    <td class="form-left-cell"><span class="red">*</span> Top Menu :</td>
                    <td class="form-right-cell">
                        <asp:DropDownList ID="TopfolderList" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="OnTopFolderChanged">
                            <asp:ListItem Text="Select one..." Value="0,Select one" />
                            <asp:ListItem Text="Home" Value="1,Root" />
                            <asp:ListItem Text="Cultural Exploration Tour" Value="2,Tours" />
                            <asp:ListItem Text="Scholastic Competition" Value="3,Talent" />
                            <asp:ListItem Text="Resources" Value="4,Resources" />
                            <asp:ListItem Text="Admin" Value="5,Admin" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="cell-row">
                    <td class="form-left-cell">Sub Menu :</td>
                    <td class="form-right-cell">
                        <asp:DropDownList ID="SubFolderList" runat="server" Width="300px">
                            <asp:ListItem Text="Select one..." Value="0,Select one" />
                            <asp:ListItem Text="Tour Outline" Value="21,Outline" Enabled="false" />
                            <asp:ListItem Text="Application" Value="22,Application" Enabled="false" />
                            <asp:ListItem Text="Testimonials" Value="23,Testimonies" Enabled="false" />
                            <asp:ListItem Text="Album" Value="24,Album" Enabled="false" />
                            <asp:ListItem Text="Journey" Value="25,Journey" Enabled="false" />
                            <asp:ListItem Text="Plan" Value="26,Plan" Enabled="false" />
                            <asp:ListItem Text="Schedule & Guidelines" Value="31,Guidelines" Enabled="false" />
                            <asp:ListItem Text="Registration" Value="32,Registration" Enabled="false" />
                            <asp:ListItem Text="Results" Value="33,Results" Enabled="false" />
                            <asp:ListItem Text="Teaching Curriculum" Value="41,Curriculum" Enabled="false" />
                            <asp:ListItem Text="Forms" Value="42,Forms" Enabled="false" />
                            <asp:ListItem Text="Video" Value="43,Video" Enabled="false" />
                            <asp:ListItem Text="Fundsraising" Value="44,Fundsraising" Enabled="false" />
                            <asp:ListItem Text="Create Page" Value="51,CreatePage" Enabled="false" />
                            <asp:ListItem Text="Review Applications" Value="52,ReviewApplications" Enabled="false" />
                            <asp:ListItem Text="Review Registration" Value="53,ReviewRegistration" Enabled="false" />
                            <asp:ListItem Text="How-To" Value="54,HowTo" Enabled="false" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="cell-row">
                    <td class="form-left-cell"><span class="red">*</span> Content File :</td>
                    <td class="form-right-cell">
                        <asp:TextBox ID="ContentName" runat="server" Width="260px" />
                        .xml
                    </td>
                </tr>
                <tr id="Tr1" class="cell-row" runat="server">
                    <td class="form-left-cell"><span class="red">*</span> Breadcrumb Text :</td>
                    <td class="form-right-cell">
                        <asp:TextBox ID="BreadcrumbLabel" runat="server" Width="292px" />
                    </td>
                </tr>
                <tr id="ContentTagRow" class="cell-row hide" runat="server">
                    <td class="form-left-cell"><span class="red">*</span> File XML Tag :</td>
                    <td class="form-right-cell">
                        <asp:TextBox ID="ContentTag" runat="server" Width="295px" />
                    </td>
                </tr>
                <tr class="cell-row hide">
                    <td class="form-left-cell">Album :</td>
                    <td class="form-right-cell">
                        <div>
                            <asp:RadioButton ID="AlbumYes" CssClass="cell-button" runat="server" Text=" Yes" Width="50px" GroupName="Album" AutoPostBack="true" OnCheckedChanged="OnAlbumYesChecked" />
                            <asp:RadioButton ID="AlbumNo" CssClass="cell-button" runat="server" Text=" No" GroupName="Album" AutoPostBack="true" OnCheckedChanged="OnAlbumNoChecked" />
                        </div>
                    </td>
                </tr>
                <tr id="AlbumPathRow" class="cell-row" runat="server">
                    <td class="form-left-cell option-cell">Album Folder :</td>
                    <td class="form-right-cell option-cell">
                        <asp:TextBox ID="AlbumPath" runat="server" Width="292px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="page-section-divider"></div>
                    </td>
                </tr>
                <tr class="cell-row">
                    <td class="form-button-cell" colspan="2">
                        <asp:Button ID="CreatePageButton" runat="server" CssClass="action-button" Text=" Create Page " OnClientClick="return validateInput();" OnClick="OnCreatePage" Font-Bold="true" />
                        <asp:Button ID="ClearButton" runat="server" CssClass="action-button" Text=" Clear Form " OnClick="OnClearForm" />
                        <asp:Button ID="CancelButton" runat="server" CssClass="action-button" Text=" Cancel " OnClick="OnCancel" />
                    </td>
                </tr>
                <tr id="CreationMessage" class="cell-row" runat="server">
                    <td class="form-right-cell" colspan="2">
                        <asp:Literal ID="ResultMessage" runat="server" /></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
