<%@ Page Title="" Language="C#" MasterPageFile="~/CEMain.Master" AutoEventWireup="true" CodeBehind="CEResources.aspx.cs" Inherits="CE.Pages.ResourcesPage" %>
<%@ Register TagPrefix="CE" Namespace="CE.Pages" Assembly="CE.Application" %>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="PlaceHolderScript" runat="server">
    <link type="text/css" rel="stylesheet" href="/CSS/ceArticle.css" media="all" />
    <link type="text/css" rel="stylesheet" href="/CSS/Themes/green/cepage.css" media="all" />
    <script type="text/javascript" src="/JS/cepages.js"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="ce-simple-content">
        <div class="ce-message">
            <table style="width:100%">
                <tr>
                    <td class="ce-icon-cell">
                        <img class="ce-icon" src="/Images/coding.jpg" />
                    </td>
                    <td class="ce-text-cell">
                        <div class="ce-text">
                            Thank you for visiting. This page is currently under construction and will be available by November 1, 2013.
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>