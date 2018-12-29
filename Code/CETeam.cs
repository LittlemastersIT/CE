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
    [Serializable]
    public static class CETeams
    {
        private const string TEAM_XML_BEGIN_TEMPLATE = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n<ce>\r\n";
        private const string TEAM_XML_TEMPLATE = "\t<team name=\"{0}\" category=\"{1}\" division=\"{2}\" />\r\n";
        private const string TEAM_XML_END_TEMPLATE = "</ce>\r\n";

        private static List<CETeam> _teams = null;

        private static void EnsureTeams()
        {
            if (_teams == null || _teams.Count <= 0)
            {
                string applicantFolder = CEHelper.GetDataPath() + CEConstants.CE_REVIEW_FOLDER;
                string physicalPath = System.IO.Path.Combine(applicantFolder, CEConstants.CE_TEAM_XML);
                XDocument xdoc = XDocument.Load(physicalPath);
                if (xdoc != null)
                {
                    IEnumerable<XElement> teams = xdoc.Element("ce").Elements("team");
                    _teams = new List<CETeam>();
                    foreach (XElement team in teams)
                    {
                        CETeam ceteam = new CETeam();
                        ceteam.Name = CEHelper.GetSafeAttribute(team, "name");
                        ceteam.Category = CEHelper.GetSafeAttribute(team, "category");
                        ceteam.Division = CEHelper.GetSafeAttribute(team, "division");
                        _teams.Add(ceteam);
                    }
                }
            }
        }

        public static bool TeamExist(string name, string category, string division)
        {
            bool exist = false;
            EnsureTeams();
            foreach (CETeam team in _teams)
            {
                if (string.Compare(team.Name, name, true) == 0 && string.Compare(team.Category, category, true) == 0 && string.Compare(team.Division, division, true) == 0)
                {
                    exist = true;
                    break;
                }
            }
            return exist;
        }

        public static void InsertTeam(string name, string category, string division)
        {
            EnsureTeams();
            if (!TeamExist(name, category, division))
            {
                CETeam team = new CETeam(name, category, division);
                _teams.Add(team);
                ExportTeams();
            }
        }

        public static void RemoveTeam(string name, string category, string division)
        {
            EnsureTeams();
            foreach (CETeam team in _teams)
            {
                if (string.Compare(team.Name, name, true) == 0 && string.Compare(team.Category, category, true) == 0 && string.Compare(team.Division, division, true) == 0)
                {
                    _teams.Remove(team);
                    break;
                }
            }
        }

        private static bool ExportTeams()
        {
            EnsureTeams();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine(TEAM_XML_BEGIN_TEMPLATE);
            foreach (CETeam team in _teams)
            {
                sb.AppendFormat(TEAM_XML_TEMPLATE, new string[] { team.Name, team.Category, team.Division });
            }
            sb.AppendLine(TEAM_XML_END_TEMPLATE);

            string applicantFolder = CEHelper.GetDataPath() + CEConstants.CE_REVIEW_FOLDER;
            string xmlFile = System.IO.Path.Combine(applicantFolder, CEConstants.CE_TEAM_XML);
            return CEHelper.WaitAndWrite(xmlFile, sb.ToString(), false, true);
        }
    }

    [Serializable]
    public class CETeam
    {
        public CETeam()
        {
        }

        public CETeam(string name, string category, string division)
        {
            Name = name;
            Category = category;
            Division = division;
        }

        public string Name { get; set; }
        public string Category { get; set; }
        public string Division { get; set; }
    }
}
