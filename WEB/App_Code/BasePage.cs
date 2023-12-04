using ESNfx3.Web.Page;
using ESWELCOME.Core.Procedure;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;

namespace WEB.App_Code
{
    /// <summary>
    /// BasePage의 요약 설명입니다.
    /// </summary>
    public abstract class BasePage : SharedBasePage
    {
        public BasePage()
        {

        }

        #region variable
        protected int _totalCount = 0;
        private Dictionary<int, string> _esLists
        {
            get
            {
                var esLists = ViewState["ESLISTS"] as Dictionary<int, string>;

                if (esLists == null)
                    esLists = new Dictionary<int, string>();

                return esLists;
            }
        }
        #endregion

        #region public method
        protected void SetESListsWithIndex(int index, string result)
        {
            var temp = _esLists;
            temp[index] = result;
            ViewState["ESLISTS"] = temp;
        }

        #endregion

        #region Property

        /// <summary>
        /// 공유 ViewState
        /// </summary>
        public StateBag SharedViewState
        {
            get
            {
                return base.ViewState;
            }
        }

        public string LoginPageUrl
        {
            get
            {
                return ResolveClientUrl(base.loginUrl);
            }
        }

        /// <summary>
        /// 리스트 데이터
        /// </summary>
        protected Dictionary<int, string> ESLists
        {
            get
            {
                return _esLists;
            }
        }

        ///// <summary>
        ///// DB 프로시저 호출을 관장한다.
        ///// </summary>
        public ProcManager Procedure
        {
            get
            {
                return ProcManager.Proc;
            }
        }

        //public LoginUser LoginUser
        //{
        //    get
        //    {
        //        LoginUser user = LoginService.GetAdminLoginUser(new LoginTicket()) as LoginUser;
        //        return user;
        //    }
        //}


        #endregion

        #region Override
        //protected override void CheckLogin()
        //{
        //    ConvertToViewData(LoginUser);
        //}

        #endregion

        protected string VisibleSubMenu
        {
            get; set;
        }

        #region static method
        /// <summary>
        /// QueryString에 key가 포함되어 있는지
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsContainQuery(string key)
        {
            var ret = false;
            foreach (var q in HttpContext.Current.Request.QueryString.AllKeys)
            {
                if (q == key)
                {
                    ret = true;
                    break;
                }
            }

            return ret;
        }



        #endregion
    }


    public abstract class UserPage : BasePage
    {
        #region Override
        protected override void SetMasterPage()
        {
            this.MasterPageFile = "~/master/register.master";
        }

        /// <summary>
        /// 로그인 계정을 체크한다.
        /// </summary>
        //protected override void CheckLogin()
        //{
        //    base.CheckLogin();

        //    if (LoginUser == null)
        //    {
        //        RedirectToLoginUrl();
        //    }

        //    CheckUserType();
        //}
        #endregion

        #region virtual method

        /// <summary>
        /// 인증사용자의 인증구분을 체크한다.(사용자/관리자)
        /// </summary>
        //protected virtual void CheckUserType()
        //{
        //    CheckLoginSession();
        //}
        #endregion

        #region method
        public void RedirectToLoginUrl()
        {
            HttpContext.Current.Items["message"] = "로그인 후 이용 가능합니다.";
            Response.Redirect(string.Format("{0}?reUrl={1}", base.loginUrl, Request.RawUrl));
        }

        /// <summary>
        /// 로그인 인증체크
        /// </summary>
        //void CheckLoginSession()
        //{
        //    #region 로그인
        //    // 로그인 체크 안하는 페이지는 페이지내에서 base.isLoginRequired = false;  설정
        //    if (LoginUser.IsLoginSuccess)
        //    {
        //        string pathUrl = Page.Request.Url.AbsolutePath.ToLower();

        //        if (LoginUser != null)
        //        {
        //            if (LoginUser.CPY_TYPE.Equals("A"))
        //            {
        //                string script = @"
        //                        alert('접근할 수 있는 권한이 없습니다');
        //                        if(opener != null)
        //                        {
        //                            self.close();
        //                            parent.history.back();
        //                        }
        //                        else
        //                            history.back();
        //                        ";

        //                Response.Write("<script language=javascript> " + script + "</script>");
        //                Response.Flush();
        //                Response.End();
        //                return;
        //            }
        //            else
        //            {
        //                /*
        //                var items = Procedure.MEM.GetAdInfoUpdateYN(LoginUser.USRID);

        //                if (items.Exist && items.GenericItem.UPT_REQ.ToString() == "Y")
        //                {
        //                    Response.Clear();
        //                    Response.Write("<script language=javascript> alert('서비스 이용 전 광고성 정보 수신동의 여부에 대한 확인이 필요합니다.\\n감사합니다.'); document.location.href='/main/Default.aspx'; </script>");
        //                    Response.Flush();
        //                    Response.End();
        //                    return;
        //                }
        //                */
        //            }
        //        }
        //        else
        //        {
        //            if (Request.Url.AbsolutePath.ToLower().Contains("/admin/"))
        //                Response.Redirect("/eslogin.aspx?ReturnUrl=");
        //            else
        //                Response.Redirect("/eslogin.aspx?ReturnUrl=" + Server.UrlEncode(Page.Request.Url.PathAndQuery));

        //        }
        //    }
        //    #endregion
        //}
        #endregion

        #region webmethod
        #endregion
    }

    public abstract class AdminPage : UserPage
    {
        #region Override

        protected override void SetMasterPage()
        {
            this.MasterPageFile = "~/master/admin.master";
        }

        protected override void CheckLogin()
        {
            base.CheckLogin();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);


            // 관리자 페이지에서 대메뉴 선택 시 서브메뉴 활성화
            if (!string.IsNullOrEmpty(VisibleSubMenu))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "jsVisibleSubmenu"
                        , string.Format("fnShowSubMenu('{0}');", VisibleSubMenu)
                        , true);
            }
        }

        #endregion
    }

}

