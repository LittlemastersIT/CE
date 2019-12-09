using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Net.Mail;
using System.IO;

namespace CE.Data
{
    public static class CompetitionControlData
    {
        public const string LOWER_ELEMENTARY = "K2";
        public const string UPPER_ELEMENTARY = "Elementary";
        public const string MIDDLE_SCHOOL = "MiddleSchool";
        public const string HIGH_SCHOOL = "HighSchool";
        public const string ALL_GRADES = "All";
        public const string CHESS = "Chess";
        public const string DRAWING = "Drawing";
        public const string STORY = "Story";
        public const string SPEECH = "Speech";
        public const string POETRY = "Poetry";
        public const string BRIDGE = "ChineseBridge";
        public const string TEAM_KNOWLEGE_BOWL = "TeamBowl";
        public const string TEAM_POETRY = "TeamPoetry";
        public const string TEAM_TALENT = "TeamTalent";
        public const string TEAM_SINGING = "TeamSing";
        public const string TEAM_LANGUAGE = "TeamLanguage";
        public const string CLASS_A = "ClassA";
        public const string CLASS_B = "ClassB";
        public const string CLASS_C = "ClassC";

        public static Dictionary<string, string> CategoryEnglishNames = new Dictionary<string, string>()
        {
            {TEAM_TALENT, "Group Talent Show" },
            {TEAM_SINGING, "Group Chinese Singing" },
            {TEAM_LANGUAGE, "Group Chinese Language Arts" },
            {TEAM_POETRY, "Group Poetry Recitation" },
            {TEAM_KNOWLEGE_BOWL, "China Knowledge Bowl" },
            {DRAWING, "Individual Drawing Contest" },
            {STORY, "Individual Story Telling" },
            {SPEECH, "Individual Public Speaking" },
            {POETRY, "Individual Poetry Recitation" },
            {CHESS, "Individual Chinese Chess " },
            {BRIDGE, "Individual Chinese Bridge" }
        };

        public static Dictionary<string, string> CategoryChineseNames = new Dictionary<string, string>()
        {
            {TEAM_TALENT, "才 藝 競 賽" },
            {TEAM_SINGING, "歌 唱 比 賽" },
            {TEAM_LANGUAGE, "口 語 才 藝 競 賽" },
            {TEAM_POETRY, "團 體 詩 歌 朗 誦" },
            {TEAM_KNOWLEGE_BOWL, "中 國 常 識 競 答" },
            {DRAWING, "繪 畫 比 賽" },
            {STORY, "說 故 事" },
            {SPEECH, "演 講" },
            {POETRY, "個 人 詩 歌 朗 誦" },
            {CHESS, "中 國 象 棋" },
            {BRIDGE, "汉 语 桥" }
        };

        public static Dictionary<string, string> CategoryReportNames = new Dictionary<string, string>()
        {
            {TEAM_TALENT, "Group Talent Show 才藝競賽" },
            {TEAM_SINGING, "Group Chinese Singing 歌唱比賽" },
            {TEAM_LANGUAGE, "Group Chinese Language Arts 口語才藝競賽" },
            {TEAM_POETRY, "Group Poetry Recitation 團體詩歌朗誦" },
            {TEAM_KNOWLEGE_BOWL, "China Knowledge Bowl 中國常識競答" },
            {DRAWING, "Individual Drawing Contest 繪畫比賽" },
            {STORY, "Individual Story Telling 說故事" },
            {SPEECH, "Individual Public Speaking 演講" },
            {POETRY, "Individual Poetry Recitation 個人詩歌朗誦" },
            {CHESS, "Individual Chinese Chess 中國象棋" },
            {BRIDGE, "Individual Chinese Bridge 中国桥" }
        };

        public static Dictionary<string, string> DivisionReportNames = new Dictionary<string, string>()
        {
            {LOWER_ELEMENTARY, "Lower Elementary Grade K - 2 小學低年級組" },
            {UPPER_ELEMENTARY, "Upper Elementary Grade 3 - 5 小學中高年級組" },
            {MIDDLE_SCHOOL, "Middle School Grade 6 - 8 初中組" },
            {HIGH_SCHOOL, "High School Grade 9 - 12 高中組" }
        };

        public static Dictionary<string, string> ClassReportNames = new Dictionary<string, string>()
        {
            {"", "" },
            {CLASS_A, "Class A" },
            {CLASS_B, "Class B"  },
            {CLASS_C, "Class C"  }
        };

        // all category
        public static Dictionary<string, string> AllCategories = new Dictionary<string, string>()
        {
            {"Select One...", ""},
            {"Talent Show", TEAM_TALENT},
            {"Chinese Singing", TEAM_SINGING},
            {"Chinese Language Arts", TEAM_LANGUAGE},
            {"Knowledge Bowl", TEAM_KNOWLEGE_BOWL},
            {"Group Poetry Recitation", TEAM_POETRY},
            {"Drawing", DRAWING},
            {"Story Telling", STORY},
            {"Public Speaking", SPEECH},
            {"Individual Poetry Recitation", POETRY},
            {"Chinese Chess", CHESS},
            {"Chinese Bridge", BRIDGE }
        };
        // team compeittion category
        public static Dictionary<string, string> TeamCategories = new Dictionary<string, string>()
        {
            {"Select One...", ""},
            {"Talent Show", TEAM_TALENT},
            {"Chinese Singing", TEAM_SINGING},
            {"Chinese Language Arts", TEAM_LANGUAGE},
            {"Knowledge Bowl", TEAM_KNOWLEGE_BOWL},
            {"Group Poetry Recitation", TEAM_POETRY}
        };
        // individual competition category
        public static Dictionary<string, string> IndividualCategories = new Dictionary<string, string>()
        {
            {"Select One...", ""},
            {"Talent Show", TEAM_TALENT},
            {"Chinese Singing", TEAM_SINGING},
            {"Chinese Language Arts", TEAM_LANGUAGE},
            {"Drawing", DRAWING},
            {"Story Telling", STORY},
            {"Public Speaking", SPEECH},
            {"Individual Poetry Recitation", POETRY},
            {"Chinese Chess", CHESS}
        };
        public static Dictionary<string, string> AllDivisions = new Dictionary<string, string>()
        {
            {"Select one...", ""},
            {"Lower Elementary (Grade K - 2)", LOWER_ELEMENTARY},
            {"Upper Elementary (Grade 3 - 5)", UPPER_ELEMENTARY},
            {"Middle School (Grade 6 - 8)", MIDDLE_SCHOOL},
            {"High School (Grade 9 - 12)", HIGH_SCHOOL}
        };
        // for knowledge bowl & public speech
        public static Dictionary<string, string> HighSchoolDivisions = new Dictionary<string, string>()
        {
            {"Select one...", ""},
            {"Middle School (Grade 6 - 8)", MIDDLE_SCHOOL},
            {"High School (Grade 9 - 12)", HIGH_SCHOOL}
        };
        // for individual poetry
        public static Dictionary<string, string> LowerElementaryDivisions = new Dictionary<string, string>()
        {
            {"Select one...", ""},
            {"Lower Elementary (Grade K - 2)", LOWER_ELEMENTARY}
        };
        // for individual story telling
        public static Dictionary<string, string> UpperElementaryDivisions = new Dictionary<string, string>()
        {
            {"Select one...", ""},
            {"Upper Elementary (Grade 3 - 5)", UPPER_ELEMENTARY}
        };
        public static Dictionary<string, string> LowerElementaryCategories = new Dictionary<string, string>()
        {
            {" Talent Show", TEAM_TALENT},
            {" Chinese Singing", TEAM_SINGING},
            {" Chinese Language Arts", TEAM_LANGUAGE},
            {" Drawing", DRAWING},
           // {" Chinese Chess", CHESS},
            {" Individual Poetry Recitation", POETRY}
        };
        public static Dictionary<string, string> UpperElementaryCategories = new Dictionary<string, string>()
        {
            {" Talent Show", TEAM_TALENT},
            {" Chinese Singing", TEAM_SINGING},
            {" Chinese Language Arts", TEAM_LANGUAGE},
            {" Drawing", DRAWING},
           // {" Chinese Chess", CHESS},
            {" Story Telling", STORY}
        };
        public static Dictionary<string, string> MiddleSchoolCategories = new Dictionary<string, string>()
        {
            {" Talent Show", TEAM_TALENT},
            {" Chinese Singing", TEAM_SINGING},
            {" Chinese Language Arts", TEAM_LANGUAGE},
            {" Drawing", DRAWING},
            {" Chinese Chess", CHESS},
            {" Public Speaking", SPEECH}
        };
        public static Dictionary<string, string> HighSchoolCategories = new Dictionary<string, string>()
        {
            {" Talent Show", TEAM_TALENT},
            {" Chinese Singing", TEAM_SINGING},
            {" Chinese Language Arts", TEAM_LANGUAGE},
            {" Drawing", DRAWING},
            {" Chinese Chess", CHESS},
            {" Chinese Bridge -- All About Chinese Test", BRIDGE },
            {" Public Speaking", SPEECH}

        };
        // for all classes
        public static Dictionary<string, string> AllClasses = new Dictionary<string, string>()
        {
            {"Select one...", ""},
            {"Class A", CLASS_A},
            {"Class B", CLASS_B},
            {"Class C", CLASS_C}
        };
        public static Dictionary<string, string> ValidClasses = new Dictionary<string, string>()
        {
            {" Class A", CLASS_A},
            {" Class B", CLASS_B},
            {" Class C", CLASS_C}
        };
        // for no selection
        public static Dictionary<string, string> EmptySelection = new Dictionary<string, string>()
        {
            {"Select one...", ""}
        };
        // for grade indices
        public static Dictionary<string, string> GradeRange = new Dictionary<string, string>()
        {
            {LOWER_ELEMENTARY, "1,4"},
            {UPPER_ELEMENTARY, "1,7"},
            {MIDDLE_SCHOOL, "1,10"},
            {HIGH_SCHOOL, "1,14"},
            {ALL_GRADES, "1,14"}
        };
        public static Dictionary<string, string> K12Grades = new Dictionary<string, string>()
        {
            {"Select one...", ""},
            {"K-1", "K1"},
            {"K-2", "K2"},
            {"1st grade", "1"},
            {"2nd grade", "2"},
            {"3rd grade", "3"},
            {"4th grade", "4"},
            {"5th grade", "5"},
            {"6th grade", "6"},
            {"7th grade", "7"},
            {"8th grade", "8"},
            {"9th grade", "9"},
            {"10th grade", "10"},
            {"11th grade", "11"},
            {"12th grade", "12"}
        };
        // for talent show
        public static List<string> TalentShowTypes = new List<string>()
        {
            "Dance",
            "Folk Arts",
            "Kung-Fu",
            "Music Instruments",
            "Others"
        };
    }

    public static class CEConstants
    {
        public const string CE_HOME_URL = "https://www.culturalexploration.org";
        public const string CE_HOME_SHORT_NAME = "CE";
        public const string CE_HOME_PAGE = "public/home.aspx";
        public const string CE_TALENT_COMPETITION_PAGE = "Public/Talent/Registration/TalentRegistration.aspx";
        public const string CE_ADMIN_PAGE = "admin/ceadmin.aspx";
        public const string CE_PROBLEM_PAGE = "public/problem.aspx";
        public const string CE_ACCESS_DENIED_PAGE = "public/accessdenied.aspx";
        public const string CE_PAYMENT_COOKIE_ID = "CE_PAYMENT";
        public const string CE_FEEWAIVED_COOKIE_ID = "CE_FEEWAIVED_PAYMENT";
        public const string CE_CHAMPION_COOKIE_ID = "CE_CHAMPION_CACHE";
        public const string CE_REVIEW_SEARCH_RESULT_ID = "CE_FOUND_APPLICANTS";
        public const string CE_REVIEW_MASTER_RESULT_ID = "CE_ALL_APPLICANTS";
        public const string CE_ROLE_USERS_ID = "CE_ROLE_USERS";
        public const string CE_ADMIN_COOKIE_ID = "CE_ADMIN";
        public const string CE_ADMIN_COOKIE_NAME = "CE_ADMIN";
        public const string CE_ADMIN_COOKIE_VALUE = "ce-admin";
        public const string CE_USER_COOKIE_ID = "CE_USER";
        public const string CE_INDIVIDUAL_ENTRY_COOKIE_ID = "CE_INDIVIDUAL_ENTRY";
        public const string CE_TEAM_ENTRY_COOKIE_ID = "CE_TEAM_ENTRY";
        public const string CE_TOUR_ENTRY_COOKIE_ID = "CE_TOUR_ENTRY";
        public const string CE_DISPLAYNAME_COOKIE_ID = "CE_DISPLAYNAME";
        public const string CE_ROLE_COOKIE_ID = "CE_ROLE";
        public const string CE_USER_COOKIE_NAME = "CE_USER";
        public const string CE_DISPLAYNAME_COOKIE_NAME = "CE_DISPLAYNAME";
        public const string CE_ROLE_COOKIE_NAME = "CE_ROLE";
        public const string CE_PUBLIC_COOKIE_ID = "CE_PUBLIC";
        public const string CE_CONTENT_ROOT_URL = "/Content";
        public const string CE_PUBLIC_ROOT_URL = "/public";
        public const string CE_ADMIN_ROOT_URL = "/admin";
        public const string CE_ADMIN_ROOT_FOLDER = "Admin";
        public const string CE_PUBLIC_ROOT_FOLDER = "public";
        public const string CE_CONTENT_ROOT_FOLDER = "Content";
        public const string CE_SITEMAP_FILE = @"content\sitemap.xml";
        public const string CE_GENERIC_ROLE = "user";
        public const string CE_TOUR_ROLE = "tour";
        public const string CE_TALENT_ROLE = "talent";
        public const string CE_ADMIN_ROLE = "admin";
        public const string CE_DATA_FOLDER = "data";
        public const string CE_EMAIL_CONFIGURATION_XML = "ceemail.xml";
        public const string CE_REVIEW_FOLDER = @"\application";
        public const string CE_REPORT_FOLDER = @"\report";
        public const string CE_BACKUP_FOLDER = @"\backup";
        public const string CE_TEAM_XML = "ceteam.xml";
        public const string CE_SCHOOL_LIST_FILE = "CE Schools List.xlsx";
        public const string CE_CHAMPION_XLSX = "CEChampions.xlsx";
        public const string CE_SCHEDULE_XLSX = "TalentCompetitionSchedule.xlsx";
        public const string CE_SIGNIN_TEMPLATE_XLSX = "StudentSignInSheetTemplate.xlsx";
        public const string CE_CONTESTANT_TEMPLATE_XLSX = "ContestantParticipatedEventsTemplate.xlsx";
        public const string CE_CONFLICT_TEMPLATE_XLSX = "ContestantsWithConflictScheduleTemplate.xlsx";
        public const string CE_HEADCOUNT_TEMPLATE_XLSX = "HeadCountTemplate.xlsx";
        public const string CE_TROPHY_TEMPLATE_XLSX = "TrophyTemplate.xlsx";
        public const string CE_AWARD_PRESENTATION_TEMPLATE_XLSX = "AwardPresentationTemplate.xlsx";
        public const string CE_WINNER_TEMPLATE_XLSX = "TalentCompetitionWinnerTemplate.xlsx";
        public const string CE_CERTIFICATE_TEMPLATE_DOCX = "CompetitionWinnerCertificateTemplate.docx";
        public const string CE_PARTICPATING_TEMPLATE_DOCX = "CompetitionParticipatingCertificateTemplate.docx";
        public const string CE_SCORE_TEMPLATE_BASE_XLSX = "ScoreSheetTemplate{0}.xlsx";
        public const string CE_TOUR_APPLICANT_FOLDER = @"\application\tours\";
        public const string CE_TALENT_CHAMPION_FOLDER = @"\application\talent";
        public const string CE_TALENT_REGISTRATION_FOLDER = @"\application\talent\";
        public const string CE_TOUR_JOURNEY_PATH = "/tours/journey/";
        public const string CE_TOUR_JOURNEY_FOLDER = @"content\tours\journey";
        public const string CE_SIGNIN_SHEET_FOLDER = "SignIn Sheets";
        public const string CE_SCORE_SHEET_FOLDER = "Score Sheets";
        public const string CE_CONTESTANT_SHEET_FOLDER = "Contestant Sheets";
        public const string CE_CONFLICT_SHEET_FOLDER = "Conflict Sheets";
        public const string CE_CERTIFICATE_FOLDER = "Winner Certificates";
        public const string CE_PARTICIPATING_FOLDER = "Participant Certificates"; 
        public const string CE_DEFAULT_THEME = "black";
        public const string CE_TOUR_THEME = "blue";
        public const string CE_TALENT_THEME = "maroon";
        public const string CE_RESOURCE_THEME = "green";
        public const string CE_ADMIN_THEME = "black";
        public const string CE_DEFAULT_PAGE_TAG = "page";
        public const string CE_DEFAULT_ALBUM_TAG = "albums";
        public const string CE_DEFAULT_VIDEO_TAG = "videos";
        public const string CE_DEFAULT_JOURNEY_TAG = "journey";
        public const string CE_DEFAULT_TAB_TAG = "tabs";
        public const string CE_DEFAULT_ADMIN_TAG = "admin";
        public const string CE_YES = "yes";
        public const string CE_LANGUAGE_US = "us";
        public const string CE_LANGUAGE_CHINESE = "cn";
        public const string STUDENT_ID_SEPARATOR = ".";
        // configuration settings
        
        public const string PAYPAL_PAYMENT_URL = "https://www.paypal.com/cgi-bin/webscr?cmd=_xclick";
        public const string CE_PAYPAL_EMAIL = "culturalexporation@hotmail.com";
        public const string REGISTRATION_MASTER_FILE_KEY = "CreateMasterRegistrationFile";
        public const string REVIEW_BACKFLOW_KEY = "AllowReviewBackflow";
        public const string REGISTRATION_DATE_KEY = "RegistrationStartDate";
        public const string REGISTRATION_ENDDATE_KEY = "RegistrationEndDate";
        public const string COMPETITION_DATE_KEY = "CompetitionDate";
        public const string COMPETITION_YEAR_KEY = "CECompetitionYear";
        public const string APPLICATION_STARTDATE_KEY = "ApplicationDate";
        public const string APPLICATION_ENDDATE_KEY = "ApplicationEndDate";
        public const string TOUR_START_DATE = "CETourStartDate";
        public const string TOUR_PROGRAM_YEAR = "CEProgramYear";
        public const string EMAIL_SENDER_KEY = "EmailSender";
        public const string EMAIL_HOST_KEY = "EmailHost";
        public const string EMAIL_PORT_KEY = "EmailPort";
        public const string EMAIL_CODE_KEY = "EmailCode";

        public const string APPLICATION_XML_BEGIN_TEMPLATE = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n<ce>\r\n\t<application>";
        public const string APPLICATION_XML_END_TEMPLATE = "\t</application>\r\n</ce>";
        public const string HOW_YOU_LEARN_OUR_PROGRAM = "How did you learn about our program?";
        public const string TEACHING_SPECIALTY = "What is your teaching subject and professional specialty?";
        public const string QUESTIONAIRE1 = "How do you relate your current teaching or work experiences to our program?";
        public const string QUESTIONAIRE2 = "How have you incorporated a prior experience to augment a lesson plan?";
        public const string QUESTIONAIRE3 = "What are your short-term and long-term teaching goals?";
        public const string QUESTIONAIRE4 = "How do you expect this travel experience to benefit your students and your teaching?";
    }

    public static class CEHelper
    {
        // support only English and Chinese language translationonly
        public static string MakeLanguageUrlLink(string currentLanguage)
        {
            string linkUrl = string.Empty;
            string language = string.Empty;
            string currentUrl = HttpContext.Current.Request.RawUrl;
            if (string.Compare(currentLanguage, CEConstants.CE_LANGUAGE_CHINESE, true) == 0)
            {
                currentUrl = currentUrl.Replace("/" + CEConstants.CE_LANGUAGE_CHINESE + "/&content", "&content");
                currentUrl = currentUrl.Replace("/" + CEConstants.CE_LANGUAGE_CHINESE + "&content", "&content");
                currentUrl = currentUrl.Replace("language=cn", "language=us");
                language = "English Version";
            }
            else if (string.Compare(currentLanguage, CEConstants.CE_LANGUAGE_US, true) == 0 || currentLanguage == string.Empty)
            {
                currentUrl = currentUrl.Replace("&content", "/" + CEConstants.CE_LANGUAGE_CHINESE + "&content");
                currentUrl = currentUrl.Replace("language=us", "language=cn");
                language = "中文";
            }

            if (!string.IsNullOrEmpty(language))
            {
                linkUrl = string.Format("<a href=\"{0}\">{1}</a>", currentUrl, language);
            }

            return linkUrl;
        }

        #region cookies
        public static void SetCookie(string id, string name, string value, int minutes)
        {
            HttpCookie myCookie = new HttpCookie(id);
            myCookie.Values.Add(name, value);
            //myCookie.Expires = DateTime.Now.AddMinutes(minutes); // not setting will make the cookie exist only for the session
            HttpContext.Current.Response.Cookies.Add(myCookie);
        }
        public static string GetCookie(string id, string name)
        {
            HttpCookie myCookie = HttpContext.Current.Request.Cookies[id];
            if (myCookie != null)
            {
                string value = myCookie.Values[name];
                if (value != null) return value;
            }
            return string.Empty;
        }
        #endregion

        #region site path helpers
        public static string GetContentUrl(string path, string file)
        {
            if (!path.StartsWith("/")) path = "/" + path;
            if (!path.EndsWith("/")) path += "/";
            if (file.StartsWith("/")) file = file.Substring(1);
            return CEConstants.CE_CONTENT_ROOT_URL + path + file;
        }
        public static string GetPageUrl(string path, string page)
        {
            if (!path.StartsWith("/")) path = "/" + path;
            if (!path.EndsWith("/")) path += "/";
            if (page.StartsWith("/")) page = page.Substring(1);
            return CEHelper.GetSiteRootUrl() + CEConstants.CE_PUBLIC_ROOT_URL + path + page;
        }
        public static bool IsExpired(string datestr)
        {
            if (string.IsNullOrEmpty(datestr)) return false; // empty string is considered not expired

            bool isExpired = true;
            try
            {
                DateTime dt;
                if (DateTime.TryParse(datestr, out dt) == true)
                {
                    isExpired = DateTime.Now >= dt;
                }
            }
            catch
            {
            }
            return isExpired;
        }
        public static string EnsureTrailingPathChar(string s)
        {
            s = s.Trim();
            if (!s.EndsWith("/")) s += "/";
            return s;
        }
        public static string EnsureTrailingFolderChar(string s)
        {
            s = s.Trim();
            if (!s.EndsWith("/")) s += @"\";
            return s;
        }

        public static string GetSiteRootUrl()
        {
            string url = System.Web.VirtualPathUtility.ToAbsolute("~/");
            if (!url.EndsWith("/"))
            {
                return url + "/";
            }

            return url;
        }

        public static string GetSiteRootPath()
        {
            return HttpContext.Current.Request.PhysicalApplicationPath;
        }
        public static string GetDataPath()
        {
            string folder = HttpContext.Current.Request.PhysicalApplicationPath + CEConstants.CE_DATA_FOLDER;
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            return folder;
        }
        public static string GetContentPath()
        {
            string webHomeFolder = HttpContext.Current.Request.PhysicalApplicationPath;
            return webHomeFolder + CEConstants.CE_CONTENT_ROOT_FOLDER;
        }
        public static string GetPageUrl()
        {
            //HttpContext.Current.Request.Url.Host
            //HttpContext.Current.Request.Url.Authority
            //HttpContext.Current.Request.Url.AbsolutePath
            //HttpContext.Current.Request.ApplicationPath
            //HttpContext.Current.Request.Url.Host
            //HttpContext.Current.Request.Url.AbsoluteUri
            //HttpContext.Current.Request.Url.PathAndQuery

            string uri = HttpContext.Current.Request.Url.AbsoluteUri;
            int index = uri.IndexOf("?");
            if (index > 0)
                return uri.Substring(0, index);
            else
                return HttpContext.Current.Request.Url.AbsoluteUri;
        }
        public static string GetProgramYear()
        {
            return CEHelper.GetConfiguration(CEConstants.TOUR_PROGRAM_YEAR, "2013");
            /*
            string mostRecentYear = "2013"; // this is the first year when online registration is available
            string tourJourneyFolder = System.IO.Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, CEConstants.CE_TOUR_JOURNEY_FOLDER);
            string[] journeyFolderList = System.IO.Directory.GetDirectories(tourJourneyFolder, "*.*", System.IO.SearchOption.TopDirectoryOnly);
            // locate the most recent year
            for (int i = 0; i < journeyFolderList.Length; i++)
            {
                string journeyFolder = new System.IO.DirectoryInfo(journeyFolderList[i]).Name;
                if (mostRecentYear.CompareTo(journeyFolder) < 0) mostRecentYear = journeyFolder;
            }
            return mostRecentYear;
            */
        }
        public static string GetCompetitionYear()
        {
            return CEHelper.GetConfiguration(CEConstants.COMPETITION_YEAR_KEY, "2015");
        }
        #endregion

        #region ASP.Net Email
        public static bool SendEmail(string type, string id, string toEmails)
        {
            if (string.IsNullOrEmpty(toEmails)) return true;
            EmailInfo emailInfo = GetEmailConfiguration(type, id);
            if (emailInfo != null)
            {
                emailInfo.Recipients = toEmails.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                SendEmail(emailInfo.SmtpHost, emailInfo.SmtpPort, emailInfo.Sender, emailInfo.Recipients, emailInfo.CCs, emailInfo.Subject, emailInfo.Message, emailInfo.IsHtml, null);
            }
            return emailInfo != null;
        }
        public static bool SendEmail(string type, string id, string toEmails, string receiver)
        {
            if (string.IsNullOrEmpty(toEmails) || string.IsNullOrEmpty(receiver)) return false;
            EmailInfo emailInfo = GetEmailConfiguration(type, id);
            if (emailInfo != null)
            {
                emailInfo.Message = Greeting(receiver) + emailInfo.Message + "<br/><br/>";
                emailInfo.Recipients = toEmails.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                SendEmail(emailInfo.SmtpHost, emailInfo.SmtpPort, emailInfo.Sender, emailInfo.Recipients, emailInfo.CCs, emailInfo.Subject, emailInfo.Message, emailInfo.IsHtml, null);
            }
            return emailInfo != null;
        }
        public static bool SendEmail(string sender, string[] recipients, string subject, string message)
        {
            return SendEmail(string.Empty, 0, sender, recipients, new List<string>(), subject, message, true, null);
        }
        public static bool SendEmail(string sender, string[] recipients, string subject, string message, bool isHtml)
        {
            return SendEmail(string.Empty, 0, sender, recipients, new List<string>(), subject, message, isHtml, null);
        }
        public static bool SendEmail(EmailInfo emailInfo, List<string> attachments)
        {
            if (emailInfo.AllowAttachment)
                return SendEmail(emailInfo.SmtpHost, emailInfo.SmtpPort, emailInfo.Sender, emailInfo.Recipients, emailInfo.CCs, emailInfo.Subject, emailInfo.Message, emailInfo.IsHtml, attachments);
            else
                return SendEmail(emailInfo.SmtpHost, emailInfo.SmtpPort, emailInfo.Sender, emailInfo.Recipients, emailInfo.CCs, emailInfo.Subject, emailInfo.Message, emailInfo.IsHtml, null);
        }
        public static bool SendEmail(string host, int port, string sender, string[] recipients, List<string> CCs, string subject, string message, bool isHtml, List<string> attachedFiles)
        {
            if (recipients == null || recipients.Length == 0) return false;

            bool sent = false;
            try
            {
                if (string.IsNullOrEmpty(sender))
                {
                    sender = CEHelper.GetConfiguration(CEConstants.EMAIL_SENDER_KEY, "ce2019competition@littlemastersclub.org");  //used to be ce@culturalexploration.org
                }

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(sender);
                foreach (string recipient in recipients) mail.To.Add(new MailAddress(recipient));
                if (CCs != null) foreach (string email in CCs) mail.CC.Add(email);
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = isHtml;
                if (attachedFiles != null)
                {
                    foreach (string file in attachedFiles)
                    {
                        Attachment attachment = new Attachment(file);
                        if (file.Contains("/")) attachment = new Attachment(HttpContext.Current.Server.MapPath(file));
                        mail.Attachments.Add(attachment);
                    }
                }
                System.Net.Mail.SmtpClient smtp = new SmtpClient(); // use configuration settings
                if (string.IsNullOrEmpty(host))
                {
                    host = CEHelper.GetConfiguration(CEConstants.EMAIL_HOST_KEY, "snare.arvixe.com");
                    port = int.Parse(CEHelper.GetConfiguration(CEConstants.EMAIL_PORT_KEY, "25"));
                }
                smtp.Host = host;
                smtp.Port = port <= 0 ? 25 : port;
                string passCode = CEHelper.GetConfiguration(CEConstants.EMAIL_CODE_KEY, "Unknown");

                // arvixe host mail server needs to pass in credential to send mail
                System.Net.NetworkCredential nc = new System.Net.NetworkCredential(sender, passCode);
                smtp.Credentials = nc;

                smtp.Send(mail);
                sent = true;
            }
            catch
            {
                sent = false;
            }
            finally
            {
            }
            return sent;
        }
        public static EmailInfo GetEmailConfiguration(string messageType, string messageId)
        {
            EmailInfo emailInfo = null;
            try
            {
                string applicantFolder = CEHelper.GetDataPath() + CEConstants.CE_REVIEW_FOLDER;
                if (!Directory.Exists(applicantFolder))
                {
                    Directory.CreateDirectory(applicantFolder);
                }

                string physicalPath = System.IO.Path.Combine(applicantFolder, CEConstants.CE_EMAIL_CONFIGURATION_XML);
                XDocument xdoc = XDocument.Load(physicalPath);
                if (xdoc != null)
                {
                    emailInfo = new EmailInfo();
                    XElement emailTag = xdoc.Element("ce").Element("email");
                    emailInfo.SmtpHost = CEHelper.GetSafeAttribute(emailTag, "host");
                    emailInfo.SmtpPort = CEHelper.GetSafeAttributeInt(emailTag, "port");
                    IEnumerable<XElement> messageTypes = emailTag.Elements(messageType);
                    if (messageTypes != null)
                    {
                        bool found = false;
                        foreach (XElement messageTag in messageTypes)
                        {
                            if (GetSafeAttribute(messageTag, "id") == messageId)
                            {
                                // get cc list if any
                                XElement cc = messageTag.Element("cc");
                                if (cc != null)
                                {
                                    IEnumerable<XElement> emails = cc.Elements("email");
                                    if (emails != null)
                                    {
                                        foreach (XElement email in emails)
                                        {
                                            if (!string.IsNullOrEmpty(CEHelper.GetSafeElementText(email)))
                                                emailInfo.CCs.Add(CEHelper.GetSafeElementText(email));
                                        }
                                    }
                                }
                                emailInfo.Sender = CEHelper.GetSafeAttribute(messageTag, "sender");
                                emailInfo.Subject = CEHelper.GetSafeAttribute(messageTag, "subject");
                                emailInfo.IsHtml = CEHelper.GetSafeAttribute(messageTag, "format").ToLower() == "text" ? false : true;
                                emailInfo.AllowAttachment = CEHelper.GetSafeAttributeBool(messageTag, "attachment");
                                emailInfo.Message = CEHelper.GetSafeElementText(messageTag.Element("message"));
                                found = true;
                                break;
                            }
                        }
                        if (!found) emailInfo = null;
                    }
                }
            }
            catch
            {
                emailInfo = null;
            }
            return emailInfo;
        }
        private static string Greeting(string receiver)
        {
            return string.Format("Dear {0},<br/><br/>", receiver);
        }
        #endregion

        #region XML Helpers
        public static string GetSafeAttribute(XElement elem, string attrName)
        {
            string value = string.Empty;
            try
            {
                value = elem.Attributes(attrName).ElementAt(0).Value;
            }
            catch
            {
                value = string.Empty;
            }
            return value;
        }
        public static string GetSafeAttribute(XElement elem, string attrName, string defaultValue)
        {
            string value = defaultValue;
            try
            {
                value = elem.Attributes(attrName).ElementAt(0).Value;
            }
            catch
            {
                value = defaultValue;
            }
            return value;
        }
        public static int GetSafeAttributeInt(XElement elem, string attrName)
        {
            int value = -1;
            try
            {
                bool ok = int.TryParse(elem.Attributes(attrName).ElementAt(0).Value, out value);
                if (!ok) value = -1;
            }
            catch
            {
            }
            return value;
        }
        public static int GetSafeAttributeInt(XElement elem, string attrName, int defaultValue)
        {
            int value = defaultValue;
            try
            {
                bool ok = int.TryParse(elem.Attributes(attrName).ElementAt(0).Value, out value);
                if (!ok) value = defaultValue;
            }
            catch
            {
                value = defaultValue;
            }
            return value;
        }
        public static bool GetSafeAttributeBool(XElement elem, string attrName)
        {
            bool value = false;
            try
            {
                string data = elem.Attributes(attrName).ElementAt(0).Value;
                value = data == "1" || data == "yes" || data == "true";
            }
            catch
            {
            }
            return value;
        }
        public static string GetSafeElementText(XElement elem)
        {
            string text = string.Empty;
            try
            {
                if (elem != null) text = elem.Value;
            }
            catch
            {
            }
            return text;
        }
        #endregion

        #region Excel Helpers
        public static string GetSafeCellString(object cellValue)
        {
            return cellValue == null ? string.Empty : cellValue.ToString();
        }
        #endregion

        #region configuration helpers
        public static string GetConfiguration(string key)
        {
            return GetConfiguration(key, string.Empty);
        }
        public static string GetConfiguration(string key, string defaultValue)
        {
            string value = string.Empty;
            try
            {
                value = System.Configuration.ConfigurationManager.AppSettings[key];
                if (string.IsNullOrEmpty(value)) value = defaultValue;
            }
            catch
            {
                value = defaultValue;
            }
            return value;
        }
        #endregion

        #region file read/write
        public static bool WaitAndWrite(string filepath, string content, bool append, bool backup)
        {
            bool ok = false;
            System.IO.StreamWriter sw = null;
            for (int numTries = 0; numTries < 10; numTries++)
            {
                try
                {
                    sw = new System.IO.StreamWriter(filepath, append);
                    if (sw != null)
                    {
                        sw.Write(content);
                        sw.Close();
                        sw = null;
                        ok = true;
                        break;
                    }
                }
                catch
                {
                    System.Threading.Thread.Sleep(50);
                }
                finally
                {
                    if (sw != null) sw.Close();
                    sw = null;
                }
            }

            if (ok && backup)
            {
                string backupPath = filepath.Replace(CEConstants.CE_REVIEW_FOLDER, CEConstants.CE_BACKUP_FOLDER);
                try
                {
                    System.IO.File.Copy(filepath, backupPath);
                }
                catch
                {
                }
            }

            return ok;
        }
        #endregion

        #region Miscelleneous helpers
        public static string FormatCategory(string category, string subCategory)
        {
            if (category == CompetitionControlData.TEAM_TALENT)
            {
                return category + " (" + subCategory + ")";
            }
            else
                return category;
        }

        public static object GetSafeDictionary(IDictionary<string, object> dic, string key, object defaultValue = null)
        {
            object value = defaultValue;
            try
            {
                value = dic[key];
            }
            catch
            {
                value = defaultValue;
            }
            return value;
        }
        #endregion

    }

    public class EmailInfo
    {
        public EmailInfo()
        {
            SmtpHost = string.Empty;
            SmtpPort = 25;
            Sender = string.Empty;
            Recipients = null;
            Subject = string.Empty;
            IsHtml = true;
            AllowAttachment = false;
            Message = string.Empty;
            CCs = new List<string>();
        }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string Sender { get; set; }
        public string[] Recipients { get; set; }
        public List<string> CCs { get; set; }
        public string Subject { get; set; }
        public bool IsHtml { get; set; }
        public bool AllowAttachment { get; set; }
        public string Message { get; set; }
    }
}