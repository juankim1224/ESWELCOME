using ESWELCOME.DataBase.Procedure.BOL.ES;
using ESWELCOME.DataBase.Procedure.Facade;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.App_Code;

namespace WEB.controls
{
    public partial class ucChangePW : NewMovablePop
    {
        #region event
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 비밀번호 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkChangePwd_Click(object sender, EventArgs e)
        {
            // *임시 (하드코딩)
            var memId = 7;
            var memType = 2;  // 직원구분 (슈퍼관리자: 0, 일반관리자: 1, 일반: 2)	

            var param = new iES_un_LOGIN_PW ()
            {
                MEM_TYPE = Convert.ToInt16(memType),  
                MEM_ID = memId, 
                BEFORE_PW = BEFORE_PW.Value,
                AFTER_PW = AFTER_PW.Value
            };

            var changeResult = ESFacade.GetInstance.UpdateLOGIN_PW(param);

            switch (Convert.ToInt32(changeResult["@ERR_CD"]))
            {
                case -1:
                    ESNfx3.Web.Page.WebHelper.AjaxMessageAlert(Page, "비밀번호 변경이 실패되었습니다.", true);
                    break;
                case 1:
                    UCPopCloseScript(upnlList, "alert('비밀번호 변경이 완료되었습니다.');");
                    break;
            }
        }

        /// <summary>
        /// 창 띄우기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkDummy_Click(object sender, EventArgs e)
        {
            UCPopOpenScript(upnlList, "");
        }

        #endregion
    }
}