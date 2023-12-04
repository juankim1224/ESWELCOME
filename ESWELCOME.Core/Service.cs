using ESNfx3.Web.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ESWELCOME.Core
{

    // 비밀번호 암호화
    public interface ISBILoginTicket : ILoginTicket
    {
        string ErrorMessage { get; }
        string PWDTempYN { get; }

        // SHA512 비밀번호 암호화 진행여부
        bool SHAPWD { get; set; }
    }


    public class LoginService
    {
        private static string SES_ADMIN = "SESSION_ADMIN";
        private static string PRE_SES_ADMIN = "PRE_SESSION_ADMIN";

        private static string CK_KEY = "__eswelcome";
        private static string CK_KEY_INFO = "__eswelcome_i";

        private static string ENC_PASSWORD = "ESANGNETWORKSESWELCOME";

        public static readonly string CK_KEY_PREV = "__eswelcomeakp";
        public static readonly string CK_KEY_INFO_PREV = "__eswelcomeakp_i";

        public static string SES_PREV_KEY = "SES_LOGIN_KEY";


        /// <summary>
        /// 로그인 활성화 여부 체크
        /// </summary>
        /// <returns></returns>
        public static SiteUser GetAdminLoginUser(ISBILoginTicket ticket)
        {
            var user = HttpContext.Current.Session[SES_ADMIN] as SiteUser;

            //if (user == null)
            //{
            //    HttpCookie ck = HttpContext.Current.Request.Cookies[CK_KEY];

            //    if (ck != null && ck[CK_KEY_INFO] != null && !string.IsNullOrEmpty(ck[CK_KEY_INFO]))
            //    {
            //        string[] cookieInfo = Utilities.DecryptString(ck[CK_KEY_INFO], ENC_PASSWORD).Split('@');
            //        if (cookieInfo != null && cookieInfo.Length == 2)
            //        {
            //            ticket.LoginID = cookieInfo[0];
            //            ticket.LoginPWD = cookieInfo[1];
            //            ticket.SHAPWD = true;

            //            user = SiteUser.LogIn(ticket);
            //            SaveSession(user);
            //        }
            //    }
            //}
            return user;
        }

        public static void UpdateUserInfo(SiteUser loginUser)
        {
            SaveAdminSession(loginUser);
        }

        public static void UpdateUserInfo(SiteUser loginUser, string loginId, string encryptPassword)
        {
            UpdateUserInfo(loginUser);
            SaveAdminCookie(loginId, encryptPassword);
        }

        /// <summary>
        /// 로그인정보 저장(세션)
        /// </summary>
        /// <param name="loginUser"></param>
        private static void SaveAdminSession(SiteUser loginUser)
        {
            HttpContext.Current.Session[SES_ADMIN] = loginUser;
        }


        /// <summary>
        /// 이전 사용자 정보 세션 저장
        /// </summary>
        public static void SavePrevSessionInfo()
        {
            if (HttpContext.Current.Session[SES_ADMIN] != null)
            {
                SiteUser PreUserInfo = HttpContext.Current.Session[SES_ADMIN] as SiteUser;

                HttpContext.Current.Session[PRE_SES_ADMIN] = PreUserInfo;
            }

        }

        public static void AbandonSession()
        {
            if (HttpContext.Current.Session[PRE_SES_ADMIN] != null)
            {
                HttpContext.Current.Session[SES_ADMIN] = HttpContext.Current.Session[PRE_SES_ADMIN];
                HttpContext.Current.Session[PRE_SES_ADMIN] = null;

                var preLoginInfo = HttpContext.Current.Session[SES_ADMIN] as SiteUser;

                SaveAdminSession(preLoginInfo);
            }
            else
            {
                HttpContext.Current.Session[SES_ADMIN] = null;
            }
        }

        /// <summary>
        /// 로그인정보 저장(쿠키)
        /// </summary>
        /// <param name="loginUser"></param>
        private static void SaveAdminCookie(string loginID, string loginPassword)
        {
            HttpCookie hc = new HttpCookie(CK_KEY);

            string encodeUserInfo = loginID + "@" + loginPassword;

            //hc.Values.Add(CK_KEY_INFO, bizpay.Core.Utilities.EncryptString(encodeUserInfo, ENC_PASSWORD));
            //hc.Domain = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
            //hc.Path = "/";

            if (IsCookieContains(CK_KEY))
            {
                HttpContext.Current.Response.Cookies.Set(hc);
            }
            else
            {
                HttpContext.Current.Response.Cookies.Add(hc);
            }
        }

        /// <summary>
        /// 로그아웃
        /// </summary>
        public static void LogOut()
        {
            SessionOut();
            CookieSessionOut();

            HttpContext.Current.Response.Redirect("~/");
        }

        /// <summary>
        /// 로그아웃
        /// </summary>
        /// <param name="returnUrl"></param>
        public static void LogOut(string returnUrl)
        {
            SessionOut();
            CookieSessionOut();

            HttpContext.Current.Response.Redirect(returnUrl);
        }

        /// <summary>
        /// 세션아웃
        /// </summary>
        private static void SessionOut()
        {
            HttpContext.Current.Session[SES_ADMIN] = null;
        }

        /// <summary>
        /// 쿠키세션아웃
        /// </summary>
        private static void CookieSessionOut()
        {
            HttpContext.Current.Response.Cookies[CK_KEY].Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies[CK_KEY].Domain = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
        }

        /// <summary>
        /// 로그인 서비스
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="returnUrl"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string Login(ILoginTicket ticket, string returnUrl)
        {
            string message = string.Empty;

            var reUrl = HttpContext.Current.Request.QueryString["reUrl"];

            var grtno = HttpContext.Current.Request.QueryString["grtno"];

            // 트러스트온에서 넘오온 보증번호를 받아서 넘기도록 JAKIM 20211203
            if (grtno != null)
                reUrl = reUrl + "&grtno=" + grtno;

            //var reUrl = returnUrl;
            var user = SiteUser.LogIn(ticket);

            //var user = SiteUser.LogIn(ticket) as LoginUser;

            if (user != null && user.IsLoginSuccess)
            {
                string saveID = HttpContext.Current.Request.Form["saveloginid"];
                HttpCookie ckSaveID = new HttpCookie("ckSaveID");
                ckSaveID.Domain = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
                //ckSaveID.Expires = DateTime.Now.AddHours(1);
                ckSaveID.Expires = DateTime.Now.AddYears(1);

                if (saveID == "save")
                {
                    ckSaveID.Value = ticket.LoginID;
                }
                else
                {
                    ckSaveID.Value = string.Empty;
                }

                HttpContext.Current.Response.Cookies.Add(ckSaveID);

                SaveAdminSession(user);

                if (returnUrl != "/members/home.aspx")
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                        reUrl = returnUrl;
                    else if (string.IsNullOrEmpty(reUrl))
                    {
                        reUrl = "/default.aspx";
                    }

                    if (!string.IsNullOrEmpty((ticket as ISBILoginTicket).ErrorMessage))
                    {
                        message = (ticket as ISBILoginTicket).ErrorMessage;
                        HttpContext.Current.Session["ErrorMessage"] = message;
                    }

                    HttpContext.Current.Response.Redirect(reUrl);
                }
            }
            else
            {
                message = (ticket as ISBILoginTicket).ErrorMessage;
            }

            return message;
        }

        /// <summary>
        /// Cookie 존재여부 체크
        /// </summary>
        /// <param name="cookieKey"></param>
        /// <returns></returns>
        private static bool IsCookieContains(string cookieKey)
        {
            bool ret = false;

            foreach (string key in HttpContext.Current.Request.Cookies.AllKeys)
            {
                if (cookieKey == key)
                {
                    ret = true;
                    break;
                }
            }

            return ret;
        }
    }



}
