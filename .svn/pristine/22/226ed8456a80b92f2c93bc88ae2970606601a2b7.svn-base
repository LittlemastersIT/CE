using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Web;
using CE.Data;

namespace CE.Content
{
    #region CE Menu Class
    public class MenuContent
    {
        private List<CEMenu> _cemenus = null;

        public MenuContent()
        {
            _cemenus = new List<CEMenu>();
        }

        public List<CEMenu> CEMenus
        {
            get { return _cemenus; }
        }

        public void AddMenu(CEMenu menu)
        {
            _cemenus.Add(menu);
        }
    }
    public class CEMenu
    {
        private List<CESubmenu> _ceSubmenus = null;
        public CEMenu(string id, string name, string linkUrl)
        {
            ID = id;
            Name = name;
            LinkUrl = linkUrl;
        }

        public string ID { get; set; }
        public string Name { get; set; }
        public string LinkUrl { get; set; }
        public List<CESubmenu> Submenus
        {
            get { return _ceSubmenus; }
        }

        public void AddSubmenu(CESubmenu submenu)
        {
            if (_ceSubmenus == null) _ceSubmenus = new List<CESubmenu>();
            _ceSubmenus.Add(submenu);
        }
    }
    public class CESubmenu
    {
        public CESubmenu(string name, string linkUrl, string parentId)
        {
            ID = parentId;
            Name = name;
            LinkUrl = linkUrl;
        }

        public string ID { get; set; }
        public string Name { get; set; }
        public string LinkUrl { get; set; }
    }
    #endregion

    #region Page Content Retrieval
    /// <summary>
    /// Page Content Retrieval from xml Content Source
    /// </summary>
    public class MenuContentRetriever
    {
        #region Menu Content Retrieval
        public static MenuContent GetMenuContent(string menuContentXml)
        {
            MenuContent menuContent = new MenuContent();
            try
            {
                string physicalPath = HttpContext.Current.Request.PhysicalApplicationPath + menuContentXml;
                XDocument xdoc = XDocument.Load(physicalPath);
                if (xdoc != null)
                {
                    bool hasCookie = false;
                    string ceCookie = CEHelper.GetCookie(CEConstants.CE_ADMIN_COOKIE_ID, CEConstants.CE_ADMIN_COOKIE_NAME);
                    string userRole = CEHelper.GetCookie(CEConstants.CE_ROLE_COOKIE_ID, CEConstants.CE_ROLE_COOKIE_NAME);
                    if (ceCookie == CEConstants.CE_ADMIN_COOKIE_VALUE) hasCookie = true;
                    IEnumerable<XElement> menus = xdoc.Element("ce").Elements("menu");
                    foreach (XElement menu in menus)
                    {
                        bool internalOnly = CEHelper.GetSafeAttribute(menu, "internal") == "true";
                        if (!internalOnly || hasCookie)
                        {
                            string id = CEHelper.GetSafeAttribute(menu, "id");
                            string name = CEHelper.GetSafeAttribute(menu, "name");
                            string linkUrl = CEHelper.GetSafeAttribute(menu, "linkUrl");

                            CEMenu cemenu = new CEMenu(id, name, linkUrl);
                            IEnumerable<XElement> submenus = menu.Elements("submenu");
                            foreach (XElement submenu in submenus)
                            {
                                string submenuName = CEHelper.GetSafeAttribute(submenu, "name");
                                string submenuLinkUrl = CEHelper.GetSafeAttribute(submenu, "linkUrl");
                                string role = CEHelper.GetSafeAttribute(submenu, "role");
                                if (!internalOnly || (!string.IsNullOrEmpty(userRole) && (userRole.Contains(role) || userRole.Contains(CEConstants.CE_ADMIN_ROLE))))
                                {
                                    cemenu.AddSubmenu(new CESubmenu(submenuName, submenuLinkUrl, cemenu.ID));
                                }
                            }
                            menuContent.AddMenu(cemenu);
                        }
                    }
                }
            }
            catch // there will be no menu if we get here
            {

            }
            return menuContent;
        }
        #endregion
    }
    #endregion
}
