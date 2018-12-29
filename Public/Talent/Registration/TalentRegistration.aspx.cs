using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CE.Data;

namespace CE.Pages
{
    public partial class TalentRegistrationPage : System.Web.UI.Page
    {
        public string CompetitionYear { get; set; }
        public string CompetitonRegisrationStartDate { get; set; }
        public string CompetitonRegistrationEndDate { get; set; }
        public string CompetitonEventDate { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // disable payment buttons when the registration is closed for general users except for site admin
                bool hasCookie = false;
                string ceCookie = CEHelper.GetCookie(CEConstants.CE_ADMIN_COOKIE_ID, CEConstants.CE_ADMIN_COOKIE_NAME);
                if (ceCookie == CEConstants.CE_ADMIN_COOKIE_VALUE) hasCookie = true;

                string regisrationDate = CEHelper.GetConfiguration(CEConstants.REGISTRATION_DATE_KEY, "12/31/2050");
                string regisrationEndDate = CEHelper.GetConfiguration(CEConstants.REGISTRATION_ENDDATE_KEY, "01/01/2000");
                string talentCompetitionDate = CEHelper.GetConfiguration(CEConstants.COMPETITION_DATE_KEY, "01/01/2000");
                CompetitionYear = CEHelper.GetConfiguration(CEConstants.COMPETITION_YEAR_KEY, "2015");

                DateTime startDate, endDate, competitionDate;
                bool registrationStarted = true;
                bool registrationEnded = false;
                bool competitionEnded = false;
                if (DateTime.TryParse(regisrationDate, out startDate) == true) registrationStarted = startDate <= DateTime.Now;
                if (DateTime.TryParse(regisrationEndDate, out endDate) == true) registrationEnded = endDate <= DateTime.Now;
                if (DateTime.TryParse(talentCompetitionDate, out competitionDate) == true) competitionEnded = competitionDate <= DateTime.Now;
                CompetitonRegisrationStartDate = startDate.ToString("D");
                CompetitonRegistrationEndDate = endDate.ToString("D");
                CompetitonEventDate = competitionDate.ToString("D");
                RegistrationCloseNote.Visible = false;
                RegistrationNote.Visible = false;
                RegistrationStartNote.Visible = false;
                RegistationSelection.Visible = false;
                CompetitionEndNote.Visible = false;
                
                if ((registrationStarted && !registrationEnded) || hasCookie)
                {
                    RegistrationNote.Visible = true;
                    RegistationSelection.Visible = true;
                }
                else if (registrationEnded && !competitionEnded)
                {
                    RegistrationCloseNote.Visible = true;
                }
                else if (competitionEnded)
                {
                    CompetitionEndNote.Visible = true;
                }
                else
                {
                    RegistrationStartNote.Visible = true;
                }
            }
        }
    }
}