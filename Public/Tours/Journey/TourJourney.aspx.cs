using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CE.Data;
using CE.Content;

namespace CE.Pages
{
    public partial class TourJourneyPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string tourJourneyFolder = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, CEConstants.CE_TOUR_JOURNEY_FOLDER);
                string tourYear = Request.QueryString["year"];
                if (string.IsNullOrEmpty(tourYear)) tourYear = CEHelper.GetProgramYear();
                int year;
                if (Int32.TryParse(tourYear, out year) == false) tourYear = CEHelper.GetProgramYear();
                if (!Directory.Exists(tourJourneyFolder + "\\" + tourYear)) tourYear = "2015"; // all invalid year use default year
                TourYear.Text = tourYear;

                string theme = CEConstants.CE_TOUR_THEME;
                PageTheme.Text = "<link rel=\"stylesheet\" type=\"text/css\" href=\"/CSS/Themes/" + theme + "/cepage.css\" media=\"all\" />"; ;

                string[] journeyFolderList = Directory.GetDirectories(tourJourneyFolder, "*.*", SearchOption.TopDirectoryOnly);
                string programYear = CEHelper.GetProgramYear();
                tourJourneyFolder += "\\" + programYear;
                if (!Directory.Exists(tourJourneyFolder)) // all invalid year use default year
                {
                    tourJourneyFolder = tourJourneyFolder.Replace(programYear, "2015");
                    programYear = "2015";
                }
                ProgramYear.Text = programYear;

                journeyFolderList = Directory.GetDirectories(tourJourneyFolder, "*.*", SearchOption.TopDirectoryOnly);

                // read each one to assemble the journey list
                List<CEBio> CurrentJourneyBioList = new List<CEBio>();
                for (int i = 0; i < journeyFolderList.Length; i++)
                {
                    string journeyFolder = new DirectoryInfo(journeyFolderList[i]).Name;
                    string journeyFile = Path.Combine(journeyFolderList[i], journeyFolder + ".xml"); // journey file has the same name as its folder
                    string[] xmlFiles = Directory.GetFiles(journeyFolderList[i], "*.xml", SearchOption.TopDirectoryOnly);
                    for (int j = 0; j < xmlFiles.Length; j++)
                    {
                        if (string.Compare(xmlFiles[j], journeyFile, true) == 0)
                        {
                            JourneyContent journeyContent = JourneyContentRetriever.GetJourneyContent(xmlFiles[j]);
                            if (journeyContent != null)
                            {
                                CurrentJourneyBioList.Add(journeyContent.Bio);
                            }
                            break;
                        }
                    }
                }

                JourneyParticipants.DataSource = CurrentJourneyBioList;
                JourneyParticipants.DataBind();

                List<CEBio> PastJourneyBioList = new List<CEBio>();
                if (tourYear != programYear)
                {
                    tourJourneyFolder = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, CEConstants.CE_TOUR_JOURNEY_FOLDER);
                    journeyFolderList = Directory.GetDirectories(tourJourneyFolder, "*.*", SearchOption.TopDirectoryOnly);
                    tourJourneyFolder += "\\" + tourYear;
                    journeyFolderList = Directory.GetDirectories(tourJourneyFolder, "*.*", SearchOption.TopDirectoryOnly);

                    // read each one to assemble the journey list
                    for (int i = 0; i < journeyFolderList.Length; i++)
                    {
                        string journeyFolder = new DirectoryInfo(journeyFolderList[i]).Name;
                        string journeyFile = Path.Combine(journeyFolderList[i], journeyFolder + ".xml"); // journey file has the same name as its folder
                        string[] xmlFiles = Directory.GetFiles(journeyFolderList[i], "*.xml", SearchOption.TopDirectoryOnly);
                        for (int j = 0; j < xmlFiles.Length; j++)
                        {
                            if (string.Compare(xmlFiles[j], journeyFile, true) == 0)
                            {
                                JourneyContent journeyContent = JourneyContentRetriever.GetJourneyContent(xmlFiles[j]);
                                if (journeyContent != null)
                                {
                                    PastJourneyBioList.Add(journeyContent.Bio);
                                }
                                break;
                            }
                        }
                    }
                }
                else
                {
                    PastJourneyBioList = CurrentJourneyBioList;
                }

                JourneyList.DataSource = PastJourneyBioList;
                JourneyList.DataBind();

                // previous tours
                int startYear = 2013;
                int currentYear;
                if (Int32.TryParse(programYear, out currentYear) == false) currentYear = DateTime.Now.Year;
                List<CEYear> pastYears = new List<CEYear>();
                for (int i = currentYear - 1; i >= startYear; i--) pastYears.Add(new CEYear(i));
                PreviousTourJourney.DataSource = pastYears;
                PreviousTourJourney.DataBind();
            }
        }
    }
}