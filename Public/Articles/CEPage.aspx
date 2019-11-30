<%@ Page Title="" Language="C#" MasterPageFile="~/CEMain.Master" AutoEventWireup="true" CodeBehind="cepage.aspx.cs" Inherits="CE.Pages.GenericPage" %>

<%@ Register TagPrefix="CE" Namespace="CE.Pages" Assembly="CE.Application" %>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="PlaceHolderScript" runat="server">
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/ceArticle.css")%>" media="all" />
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/cepages.js")%>"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div>
        <asp:Literal ID="PageTheme" runat="server" /></div>
    <div class="ce-content-container ce-font">
        <table>
            <tr>
                <td class="ce-content-cell">
                    <div id="ce-content-zone">
                        <div class="ce-page-articles">
                            <asp:Repeater ID="PageArticles" runat="server" OnItemDataBound="Article_ItemDataBound">
                                <ItemTemplate>
                                    <div class="ce-article-title ce-h3"><%# Eval("Title") %></div>
                                    <div class="ce-article-title-divider"></div>
                                    <div class="ce-article-text">
                                        <asp:Repeater ID="Paragraphs" runat="server">
                                            <ItemTemplate>
                                                <div class="ce-article-paragraph <%# Eval("Class") %>"><%# Eval("PictureUrl") %><%# Eval("TextBlock") %></div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
