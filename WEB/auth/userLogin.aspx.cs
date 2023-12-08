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
        protected void Page_Load(object sender, EventArgs e)
        {

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
    }
}