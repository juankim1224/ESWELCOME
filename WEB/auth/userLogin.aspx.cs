using ESNfx3.Web.Page;
using ESNfx3.Web.User;
using ESWELCOME.Core;
using ESWELCOME.Core.Procedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.App_Code;
using WEB.App_Code.auth;

namespace WEB.auth
{
    public partial class userLogin : BasePage
    {
        #region property

        new string ReturnUrl
        {
            get
            {
                return Request.QueryString["reUrl"];
            }
        }

        #endregion


        protected override void FirstSetting()
        {
            if (LoginUser != null)
            {
                if (LoginUser.MEM_TYPE.Equals("1"))     //관리자인 경우
                {
                    Response.Redirect("/admin/memList.aspx");

                }
                else if (LoginUser.MEM_TYPE.Equals("2")) // 임직원인 경우
                {
                    Response.Redirect("/schedule/schMain.aspx");
                }
                else if (LoginUser.MEM_TYPE.Equals("3")) // 방문객인 경우
                {
                    Response.Redirect("/guest/gstCheckIn.aspx");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(ReturnUrl))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "jsAlertLogin", "alert('로그인이 필요합니다.');", true);
                }
            }
        }

        protected void lnkLogin_Click(object sender, EventArgs e)
        {
            var type = (sender as LinkButton).CommandArgument;

            Login();
        }


        /// <summary>
        /// 로그인계정 확인
        /// </summary>
        /// <param name="ticket"></param>
        private void Login()
        {
            ILoginTicket ticket = null;

            ticket = new LoginTicket
            {
                LoginID = Request.Form["txtLoginID"],
                LoginPWD = Request.Form["txtPassword"],
                LoginIP = Request.UserHostAddress,
                UserAgent = Request.UserAgent
            };

            string message = LoginService.Login(ticket, string.Empty);
            //string message = string.Empty;

            if (message != string.Empty)
            {
                WebHelper.AjaxOperation(Page, "jsLogin", "$('input[name=\"txtLoginID\"]').focus();" + message, true);
            }
        }

        /// <summary>
        /// 로그인계정 확인
        /// </summary>
        private void Login(string type)
        {
            ILoginTicket ticket = new LoginTicket
            {
                LoginID = Request.Form["txtLoginID"],
                LoginPWD = Request.Form["txtLoginPwd"],
                LoginIP = Request.UserHostAddress,
                UserAgent = Request.UserAgent
            };

            string message = LoginService.Login(ticket, string.Empty);

            if (message != string.Empty)
            {
                WebHelper.AjaxOperation(upnlLogin, "jsLogin", "$('input[name=\"txtLoginID\"]').focus();" + message, true);
            }

        }

    }

}