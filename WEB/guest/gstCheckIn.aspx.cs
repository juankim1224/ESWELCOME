using ESWELCOME.DataBase.Procedure.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.guest
{
    public partial class gstCheckIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region event
        /// <summary>
        /// 방문객 체크인
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkCheckIn_Click(object sender, EventArgs e)
        {
            var GST_HP = GST_HP1.Value + GST_HP2.Value + GST_HP3.Value;
            var checkInResult = SCHFacade.GetInstance.SCH_iu_CHECKIN(GST_HP, MSG_CODE.Value);

            switch (Convert.ToInt32(checkInResult["@ERR_CD"]))
            {
                case -3:
                    ESNfx3.Web.Page.WebHelper.AjaxMessageAlert(Page, "체크인이 완료된 방문입니다.", true);
                    break;
                case -2:
                    ESNfx3.Web.Page.WebHelper.AjaxMessageAlert(Page, "해당 방문내역이 없습니다.", true);
                    break;
                case 1:
                    var schId = Convert.ToInt32(checkInResult["@SCH_ID"]);
                    ESNfx3.Web.Page.WebHelper.AjaxMoveLocation(Page, String.Format("gstWelcome.aspx?schId={0}", schId), true);
                    break;
            }
        }
        #endregion
    }
}