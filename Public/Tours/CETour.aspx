<%@ Page Title="Little Master Club Chinese Language and Talent Competition Tours" Language="C#" MasterPageFile="~/CEMain.Master" AutoEventWireup="true" CodeBehind="CETour.aspx.cs" Inherits="CE.Pages.CETourPage" %>

<%@ Register TagPrefix="CE" Namespace="CE.Pages" Assembly="CE.Application" %>

<asp:Content ID="ScriptContent1" ContentPlaceHolderID="PlaceHolderScript" runat="server">
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/ceArticle.css")%>" media="all" />
    <link type="text/css" rel="stylesheet" href="<%=ResolveClientUrl("~/CSS/themes/blue/cepage.css")%>" media="all" />
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/cepages.js")%>"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="ce-content-container">
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
                <td class="ce-divider-cell"></td>
                <td class="ce-sidebar-cell">
                    <div class="ce-sidebar-zone">
                        <asp:Panel ID="RelatedLinksPanel" runat="server">
                            <asp:Repeater ID="RelatedLinksList" runat="server" OnItemDataBound="RelatedLinksList_DataBound">
                                <ItemTemplate>
                                    <div class="related-link-header"><span class="sidebar-header"><%# Eval("Header") %></span></div>
                                    <div class="sidebar-divider"></div>
                                    <div class="ce-page-sidebar-tile">
                                        <table>
                                            <asp:Repeater ID="RelatedLinks" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="sidebar-smallicon-cell">
                                                            <img class="sidebar-icon-small" src="<%# Eval("IconUrl") %>" /></td>
                                                        <td class="related-link-cell"><a href="<%# Eval("LinkUrl") %>"><span><%# Eval("Title") %></span></a></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="ce-bartile-spacing"></div>
                        </asp:Panel>
                        <asp:Repeater ID="SideBarTiles" runat="server">
                            <ItemTemplate>
                                <div class="ce-page-sidebar-tile">
                                    <div class="ce-sidebar-caption <%# Eval("CaptionTop") %>"><%# Eval("Caption") %></div>
                                    <a href="<%# Eval("LinkUrl") %>">
                                        <img class="ce-sidebar-image" src="<%# Eval("ImageUrl") %>" /></a>
                                    <div class="ce-sidebar-caption <%# Eval("CaptionBottom") %>"><%# Eval("Caption") %></div>
                                </div>
                                <div class="ce-bartile-spacing"></div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Panel ID="TestimonyPanel" runat="server">
                            <asp:Literal ID="TestimoniesHeader" runat="server" />
                            <div class="sidebar-divider"></div>
                            <asp:Repeater ID="SideBarTestimonies" runat="server">
                                <ItemTemplate>
                                    <div class="ce-page-sidebar-tile">
                                        <table>
                                            <tr>
                                                <td class="sidebar-icon-cell">
                                                    <img class="sidebar-icon" src="<%# Eval("IconUrl") %>" /></td>
                                                <td class="sidebar-caption-cell"><span><%# Eval("Caption") %></span></td>
                                            </tr>
                                        </table>
                                        <div class="sidebar-testimony-text"><a href="<%# Eval("LinkUrl") %>"><%# Eval("Text") %></a></div>
                                    </div>
                                    <div class="ce-bartile-spacing"></div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </asp:Panel>
                    </div>
                </td>
            </tr>
        </table>
    </div>

    <div class="ce-page-testimonies hide">
        <div class="CETourTestimonyContent">
            <img class="CETourTestimonyImage" src="/Images/Tours/welcome.jpg" />
            <div class="CETourTestimonyInfo">
                <div class="CETourTestimonyName">Caryn Heffernan</div>
                <div class="CETourTestimonyYear">CE Tour 2009</div>
                <div class="CETourTestimonySchool">Newport High School, Bellevue WA</div>
                <div class="CETourTestimonyText">
                    <p>I currently teach AP World History and AP Economics at Newport High School in Bellevue, WA.  When I first applied for the CE Trip I had hoped that I would be a good fit primarily for two reasons:  Chinese history and the Chinese economy are an integral part of my curriculum, and over 40% of the students at Newport are of East Asian ethnicity.  I was looking for a way to both learn things that would make my lessons on China more alive, and for an experience that would help me to better understand my students by seeing something of the background of many of their families.  The experience satisfied both goals and more.</p>
                    <p>
                        Though exhausting, the trip was one of the most fulfilling experiences of my life.  I learned an enormous amount about China and Chinese culture.  But more importantly, I learned about the people.  From our gracious tour guides, to the Chinese Americans in our tour group, to the amazing volunteers in Shang Hai, Hong Kong and Taiwan, as well as the other two teachers on the tour, the people I met during those 3 and a half weeks will be important to me forever.
	This trip allowed me to experience so many aspects of China's politics, economy and lifestyle.  By allowing us to see multiple cities in mainland China, in addition to Hong Kong and Taiwan, the trip allowed us as American teachers to more fully comprehend the diversity in what it means to be "Chinese."  No one could ever return from this trip without a tremendous desire to continue to learn, explore and share such a complex and fascinating culture.
                    </p>
                </div>

            </div>
            <img runat="server" class="CETourTestimonyImage" src="~/Images/Tours/welcome.jpg" />
            <div class="CETourTestimonyInfo">
                <div class="CETourTestimonyName">Tonya Kusak</div>
                <div class="CETourTestimonyYear">CE Tour 2007</div>
                <div class="CETourTestimonySchool">Issaquah School District</div>
            </div>

            <div class="CETourTestimonyText">
                <p>Thank you so much for the tremendous opportunity to travel throughout China this summer. Your hospitality was remarkable and so very memorable. Our entire tour was very educational concerning China's history, art, food, peoples, traditions, and landscape. It was amazing being amongst historical sites and art created thousands of years prior. I now have such a greater understanding for China and its culture. This opportunity to build a greater understanding of China's culture will inevitably impact my teaching and have a positive effect on all of my future students. I now feel that I will be able to have a greater connection to my student's Chinese heritage and teach from a more refined multi-cultural perspective. </p>
            </div>
            <img runat="server" class="CETourTestimonyImage" src="~/Images/Tours/welcome.jpg" />
            <div class="CETourTestimonyInfo">
                <div class="CETourTestimonyName">Jessica Heaton</div>
                <div class="CETourTestimonyYear">CE Tour 2005</div>
                <div class="CETourTestimonySchool">Sammamish School District</div>
            </div>

            <div class="CETourTestimonyText">
                <p>After attending the CE Fund Raise Tea Party on Sunday, it struck me that my participation with the Cultural Exchange program has been a double blessing. Not only was I incredibly lucky to have the opportunity to travel to China, Hong Kong and Taiwan, but what's even better is that I've been welcomed into your community. On Sunday I had a chance to catch up with many of the people who guided me through Taiwan, and it felt like visiting with old friends. I also met many new people, and every single person went out of their way to make me feel comfortable. Thank you so much for your sponsorship, and even more for your friendship - this I hope endures far into the future. </p>
            </div>
            <img runat="server" class="CETourTestimonyImage" src="~/Images/Tours/welcome.jpg" />
            <div class="CETourTestimonyInfo">
                <div class="CETourTestimonyName">Alice Britt</div>
                <div class="CETourTestimonyYear">CE Tour 2002</div>
                <div class="CETourTestimonySchool">Issaquah School District</div>
            </div>

            <div class="CETourTestimonyText">
                <p>As our trip ended in Taipei, we said good-bye to Sam, Susan and their families and headed toward the airport. We were exhausted! This had been no vacation! We had arisen each morning to explore as much as we could on our own even before our tour guide arrived. At night we rarely fell into bed before midnight. We were indeed tired, but we were brimming with enthusiasm about our experiences in China and anxious to share what we learned with our students, parents, and colleagues. </p>
                <p>So now we would like to thank you all for this wonderful opportunity and promise that we will do our best to keep the teaching of Chinese history and culture an integral part of your children's education. This is just the beginning. We look forward to a continued relationship with the Seattle Chinese School on behalf of your children. </p>
            </div>
            <img runat="server" class="CETourTestimonyImage" src="~/Images/Tours/welcome.jpg" />
            <div class="CETourTestimonyInfo">
                <div class="CETourTestimonyName">Susan Brown</div>
                <div class="CETourTestimonyYear">CE Tour 2001</div>
                <div class="CETourTestimonySchool">Everest School District</div>
            </div>

            <div class="CETourTestimonyText">
                <p>I found my China trip to be life-changing. It opened my eyes to an entirely different way of living and that has impacted my interactions with many students and teachers. I now teach high school English and so I encourage students to talk about their cultures and backgrounds and I actively look for literature that offers a non-European view. </p>
            </div>

        </div>
        <div>
            <div class="CETourAboutDivider">
                <img runat="server" class="CETourImage" src="~/Images/Tours/forestLandscape.jpg" />
            </div>
        </div>
    </div>
</asp:Content>
