using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CE.Content;

namespace CE.Controls
{
    [ToolboxData("<{0}:MenuControl runat=server></{0}:MenuControl>")]
    public class MenuControl : WebControl
    {
        private const string MENU_CONTENT_FILENAME = @"Content\cemenu.xml";

        private const string MENU_BLOCK_BEGIN = "<div id=\"ce-top-nav\"><ul>";
        private const string MENU_ITEM_TEMPLATE = "<li id=\"ce-top-nav-{0}\"><div class=\"ce-nav-item\" nodeurl=\"{2}\" onmouseover=\"showSubNav('{0}',{1})\" onmouseout=\"hideSubNav('{0}',true)\" onclick=\"linkMenu('{0}')\">{3}</div></li>";
        private const string MENU_ITEM_DIVIDER = "<li class=\"dim\">|</li>";
        private const string MENU_BLOCK_END = "</ul></div>";

        private const string SUBMENU_BLOCK_BEGIN = "<div id=\"ce-sub-nav\" class=\"hide\" onmouseover=\"displaySubNav()\" onmouseout=\"closeSubNav(0)\" onclick=\"closeSubNav(1)\">";
        private const string SUBMENU_BLOCK_TEMPLATE = "<div id=\"ce-sub-nav-{0}\" class=\"ce-sub-nav hide\" onmouseover=\"showSubNav('{0}',{1})\" onclick=\"closeSubNav(1);\"><ul>";
        private const string SUBMENU_ITEM_TEMPLATE = "<li><a href=\"{0}\">{1}</a></li>";
        private const string SUBMENU_ITEM_DIVIDER = "<li class=\"dark\">|</li>";
        private const string SUBMENU_BLOCK_TEMPLATE_END = "</ul></div>";
        private const string SUBMENU_BLOCK_END = "</div>";

        public override void RenderBeginTag(HtmlTextWriter writer)
        { 
            // make vs2012 not to create a <span> tag
        }
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            // make vs2012 not to create a </span> tag
        }
        protected override void RenderContents(HtmlTextWriter output)
        {
            base.RenderContents(output);
            output.Flush();

            MenuContent menuContent = MenuContentRetriever.GetMenuContent(MENU_CONTENT_FILENAME);
            StringBuilder sb = new StringBuilder();

            sb.Append(MENU_BLOCK_BEGIN);
            int menuCount = 0;
            foreach (CEMenu cemenu in menuContent.CEMenus)
            {
                menuCount++;
                string hasSubmenu = (cemenu.Submenus != null && cemenu.Submenus.Count > 0) ? "true" : "false";
                sb.Append(string.Format(MENU_ITEM_TEMPLATE, menuCount, hasSubmenu, cemenu.LinkUrl, cemenu.Name));
                if (menuCount < menuContent.CEMenus.Count) sb.Append(string.Format(MENU_ITEM_DIVIDER));
            }
            sb.Append(MENU_BLOCK_END);

            menuCount = 0;
            sb.Append(SUBMENU_BLOCK_BEGIN);
            foreach (CEMenu cemenu in menuContent.CEMenus)
            {
                menuCount++;
                string hasSubmenu = (cemenu.Submenus != null && cemenu.Submenus.Count > 0) ? "true" : "false";
                sb.Append(string.Format(SUBMENU_BLOCK_TEMPLATE, menuCount, hasSubmenu));
                if (cemenu.Submenus != null && cemenu.Submenus.Count > 0)
                {
                    int submenuCount = 0;
                    foreach (CESubmenu cesubmenu in cemenu.Submenus)
                    {
                        submenuCount++;
                        sb.Append(string.Format(SUBMENU_ITEM_TEMPLATE, cesubmenu.LinkUrl, cesubmenu.Name));
                        if (submenuCount < cemenu.Submenus.Count) sb.Append(string.Format(SUBMENU_ITEM_DIVIDER));
                    }
                }
                sb.Append(SUBMENU_BLOCK_TEMPLATE_END);
            }
            sb.Append(SUBMENU_BLOCK_END);


            //// main menu
            ////sb.Append("<div class=\"ce-nav-container\">");
            //sb.Append("<div id=\"ce-top-nav\">");
            //sb.Append("<ul>");
            //sb.Append("<li id=\"ce-top-nav-1\">");
            //sb.Append("<div class=\"ce-nav-item nosubmenu\" nodeurl=\"/public/home.aspx\" onmouseover=\"showSubNav('1',false)\" onmouseout=\"hideSubNav('1',true)\" onclick=\"linkMenu('1')\">");
            //sb.Append("Home");
            //sb.Append("</div>");
            //sb.Append("</li>");
            //sb.Append("<li class=\"dim\">|</li>");
            //sb.Append("<li id=\"ce-top-nav-2\">");
            //sb.Append("<div class=\"ce-nav-item hassubmenu\" nodeurl=\"/public/tours/cetour.aspx\" onmouseover=\"showSubNav('2',true)\" onmouseout=\"hideSubNav('2',true)\" onclick=\"linkMenu('2')\">");
            //sb.Append("Cultural Expploration Tour");
            //sb.Append("</div>");
            //sb.Append("</li>");
            //sb.Append("<li class=\"dim\">|</li>");
            //sb.Append("<li id=\"ce-top-nav-3\">");
            //sb.Append("<div class=\"ce-nav-item hassubmenu\" nodeurl=\"/public/talent/cetalent.html\" onmouseover=\"showSubNav('3',true)\" onmouseout=\"hideSubNav('3',true)\" onclick=\"linkMenu('3')\">");
            //sb.Append("Scholastic Competition");
            //sb.Append("</div>");
            //sb.Append("</li>");
            //sb.Append("<li class=\"dim\">|</li>");
            //sb.Append("<li id=\"ce-top-nav-4\">");
            //sb.Append("<div class=\"ce-nav-item hassubmenu\" nodeurl=\"/public/resources/ceresources.html\" onmouseover=\"showSubNav('4',true)\" onmouseout=\"hideSubNav('4',true)\" onclick=\"linkMenu('4')\">");
            //sb.Append("Resources");
            //sb.Append("</div>");
            //sb.Append("</li>");
            //sb.Append("<li class=\"dim\">|</li>");
            //sb.Append("<li id=\"ce-top-nav-5\">");
            //sb.Append("<div class=\"ce-nav-item hassubmenu\" nodeurl=\"/admin/ceadmin.html\" onmouseover=\"showSubNav('5',true)\" onmouseout=\"hideSubNav('5',true)\" onclick=\"linkMenu('5')\">");
            //sb.Append("Admin");
            //sb.Append("</div>");
            //sb.Append("</li>");
            //sb.Append("</ul>");
            //sb.Append("</div>");

            //// submenu
            //sb.Append("<div id=\"ce-sub-nav\" class=\"hide\" onmouseover=\"displaySubNav()\" onmouseout=\"closeSubNav()\" onclick=\"closeSubNav()\">");
            //// home submenu
            //sb.Append("<div id=\"ce-sub-nav-1\" class=\"ce-sub-nav hide\" onmouseover=\"showSubNav('1',false)\" onclick=\"closeSubNav();\">");
            //sb.Append("</div>");
            //// tour submenu
            //sb.Append("<div id=\"ce-sub-nav-2\" class=\"ce-sub-nav hide\" onmouseover=\"showSubNav('2',true)\" onclick=\"closeSubNav();\">");
            //sb.Append("<ul>");
            //sb.Append("<li><a href=\"/public/tours/outline/ceoutline.html\">Overview</a></li>");
            //sb.Append("<li class=\"dim\">|</li>");
            //sb.Append("<li><a href=\"/public/tours/application/ceapplication.html\">Application</a></li>");
            //sb.Append("<li class=\"dim\">|</li>");
            //sb.Append("<li><a href=\"/public/tours/testimonies/cetestimonies.html\">Testimonials</a></li>");
            //sb.Append("<li class=\"dim\">|</li>");
            //sb.Append("<li><a href=\"/public/tours/album/touralbum.html\">Album</a></li>");
            //sb.Append("<li class=\"dim\">|</li>");
            //sb.Append("<li><a href=\"/public/tours/journey/cejourney.html\">Journey</a></li>");
            //sb.Append("</ul>");
            //sb.Append("</div>");
            //// talent submenu
            //sb.Append("<div id=\"ce-sub-nav-3\" class=\"ce-sub-nav hide\" onmouseover=\"showSubNav('3',true)\" onclick=\"closeSubNav();\">");
            //sb.Append("<ul>");
            //sb.Append("<li><a href=\"/public/talent/guidelines/ceoutline.html\">Introduction</a></li>");
            //sb.Append("<li class=\"dim\">|</li>");
            //sb.Append("<li><a href=\"/public/talent/registration/ceregistration.html\">Registration</a></li>");
            //sb.Append("<li class=\"dim\">|</li>");
            //sb.Append("<li><a href=\"/public/talent/results/ceresults.html\">Results</a></li>");
            //sb.Append("</ul>");
            //sb.Append("</div>");
            //// resources submenu
            //sb.Append("<div id=\"ce-sub-nav-4\" class=\"ce-sub-nav hide\" onmouseover=\"showSubNav('4',true)\" onclick=\"closeSubNav();\">");
            //sb.Append("<ul>");
            //sb.Append("<li><a href=\"/public/resources/curriculum/cecurriculum.html\">Teaching Curriculum</a></li>");
            //sb.Append("<li class=\"dim\">|</li>");
            //sb.Append("<li><a href=\"/public/resources/forms/ceforms.html\">Forms</a></li>");
            //sb.Append("<li class=\"dim\">|</li>");
            //sb.Append("<li><a href=\"/public/resources/fundraising/cefundraising.html\">Fundraising</a></li>");
            //sb.Append("</ul>");
            //sb.Append("</div>");
            //// admin submenu
            //sb.Append("<div id=\"ce-sub-nav-5\" class=\"ce-sub-nav hide\" onmouseover=\"showSubNav('5',true)\" onclick=\"closeSubNav();\">");
            //sb.Append("<ul>");
            //sb.Append("<li><a href=\"/admin/tools/createpage.html\">Create Page</a></li>");
            //sb.Append("</ul>");
            //sb.Append("</div>");
            //sb.Append("</div>"); // end of submenu
            ////sb.Append("</div>"); // end of menu container

            output.Write(sb.ToString());
        }
    }
}
