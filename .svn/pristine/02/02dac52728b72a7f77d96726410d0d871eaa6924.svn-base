using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Web;
using CE.Data;

namespace CE.Data
{
    #region CE Site Users Class
    public static class SiteUsers
    {
        private static List<CEUser> _ceusers = null;

        private static void AddUser(CEUser account)
        {
            _ceusers.Add(account);
        }

        private static void EnsureSiteUsers()
        {
            if (_ceusers == null || _ceusers.Count == 0)
            {
                _ceusers = new List<CEUser>();
                try
                {
                    //string physicalPath = HttpContext.Current.Request.PhysicalApplicationPath + @"admin\users\siteusers.xml";
                    string physicalPath = CEHelper.GetDataPath() + @"\users\siteusers.xml";
                    XDocument xdoc = XDocument.Load(physicalPath);
                    if (xdoc != null)
                    {
                        IEnumerable<XElement> users = xdoc.Element("ce").Element("users").Elements("user");
                        foreach (XElement userItem in users)
                        {
                            string name = CEHelper.GetSafeAttribute(userItem, "name");
                            string displayName = CEHelper.GetSafeAttribute(userItem, "displayName");
                            string password = CEHelper.GetSafeAttribute(userItem, "password");
                            string role = CEHelper.GetSafeAttribute(userItem, "role");
                            string expired = CEHelper.GetSafeAttribute(userItem, "expired");

                            if (expired != "true")
                            {
                                CEUser user = new CEUser(name, password, role, displayName);
                                _ceusers.Add(user);
                            }
                        }
                    }
                }
                catch // there will be no menu if we get here
                {

                }
            }
        }

        public static List<CEUser> Users
        {
            get { return _ceusers; }
        }

        public static CEUser FindUser(string username, string password)
        {
            EnsureSiteUsers();
            foreach (CEUser user in _ceusers)
            {
                if (string.Compare(user.UserName, username, true) == 0 && user.Password == password)
                    return user;
            }
            return null;
        }

        public static bool IsSiteUser(string username, string password, string role, string displayName)
        {
            EnsureSiteUsers();
            return _ceusers.Exists(MatchUser(new CEUser(username, password, role, displayName)));
        }

        public static bool IsSiteUser(string username, string password)
        {
            return IsSiteUser(username, password, CEConstants.CE_GENERIC_ROLE, username);
        }

        public static Predicate<CEUser> MatchUser(CEUser aUser)
        {
            return delegate(CEUser user)
            {
                return string.Compare(user.UserName, aUser.UserName, true) == 0 && user.Password == aUser.Password;
            };
        }

        public static List<CEUser> FindUsersWithRole(string role)
        {
            EnsureSiteUsers();
            List<CEUser> users = new List<CEUser>();
            foreach (CEUser user in _ceusers)
            {
                if (user.Role.Contains(role)) users.Add(user);
            }
            return users;
        }
    }

    [Serializable]
    public class CEUser
    {
        public CEUser(string user, string password, string role, string displayName)
        {
            UserName = user;
            DisplayName = displayName;
            Password = password;
            Role = role;
        }

        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public static bool HasTourRole(string role)
        {
            return string.Compare(role, CEConstants.CE_TOUR_ROLE, false) == 0 || string.Compare(role, CEConstants.CE_ADMIN_ROLE, false) == 0;
        }

        public static bool HasTalentRole(string role)
        {
            return string.Compare(role, CEConstants.CE_TALENT_ROLE, false) == 0 || string.Compare(role, CEConstants.CE_ADMIN_ROLE, false) == 0;
        }

        public static bool HasAdminRole(string role)
        {
            return string.Compare(role, CEConstants.CE_ADMIN_ROLE, false) == 0;
        }
    }
    #endregion

    #region User Account Retrieval
    /// <summary>
    /// User Account Retrieval from xml Content Source
    /// </summary>
    public class UserAccountRetriever
    {
        #region Menu Content Retrieval
        #endregion
    }
    #endregion
}
