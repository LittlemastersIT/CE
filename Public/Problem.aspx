<%@ Page Title="Little Master Club Chinese Language and Talent Competition Error" Language="C#" MasterPageFile="~/CEMain.Master" AutoEventWireup="true" CodeBehind="Problem.aspx.cs" Inherits="CE.Pages.ProblemPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="ce-simple-content">
        <div class="ce-message">
            <table style="width: 100%">
                <tr>
                    <td class="ce-icon-cell">
                        <img runat="server" class="ce-icon" src="~/Images/error.jpg" />
                    </td>
                    <td class="ce-text-cell">
                        <div class="ce-text">
                            We are sorry that the page you requested cannot be displayed. We apologize for the inconvenience. If the problem persists, please contact our <a style="color:blue;text-decoration:underline;" href="mailto:ceadmin@culturalexploration.org" style="text-decoration: none;">site administrator</a>.<br />
                            <br />
                            <a runat="server" href="~/public/home.aspx">Return to home page</a>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
