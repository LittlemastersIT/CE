<%@ Page Title="Little Master Club Chinese Language and Talent Competition Error" Language="C#" MasterPageFile="~/CESimple.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="CE.Pages.ErrorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="ce-simple-content">
        <div class="ce-message">
            <table style="width:100%">
                <tr>
                    <td class="ce-icon-cell">
                        <img runat="server" class="ce-icon" src="~/Images/error.jpg" />
                    </td>
                    <td class="ce-text-cell">
                        <div class="ce-text">
                            We are sorry that the page you requested cannot be displayed or located. We apologize for the inconvenience. Please visit our site again later.<br/><br/>
                            <a runat="server" href="~/public/home.aspx">Return to home page</a>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
