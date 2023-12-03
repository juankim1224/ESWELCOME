using ESWELCOME.DataBase.Procedure.BOL.MSG;
using ESWELCOME.DataBase.Procedure.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.App_Code;

namespace WEB.controls
{
    public partial class ucSMS : NewMovablePop
    {
        #region event
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 문자 조회
        /// </summary>
        protected void lnkDummy_Click(object sender, EventArgs e)
        {
            SetDataBind();
            UCPopOpenScript(upnlList, "");
        }
        #endregion


        #region method
        private void SetDataBind()
        {
            var schId = Convert.ToInt32(hddSCH_ID.Value);
            var oneSchedule = SCHFacade.GetInstance.GetSCHEDULE(schId);

            if (oneSchedule.Exist)
            {
                var item = oneSchedule.GenericItem;
                gstCpy.InnerText = item.GST_CPY != "-" ? item.GST_CPY + " " : "";
                gstPst.InnerText = item.GST_PST != "-" ? item.GST_PST : "";
                gstName.InnerText = item.GST_NAME;
                schType.InnerText = item.SCH_TYPE;

                StringBuilder sb = new StringBuilder();
                string ymd = item.SCH_YEARMD;
                sb.Append(ymd.Substring(0, 4));
                sb.Append("년 ");
                sb.Append(ymd.Substring(4, 2));
                sb.Append("월 ");
                sb.Append(ymd.Substring(6, 2));
                sb.Append("일 ");

                schYearMD.InnerText = sb.ToString();
                schHourMin.InnerText = item.SCH_HOUR + "시 " + item.SCH_MIN + "분";

                // 대표 접견인 번호
                msgStaff.InnerText = MSGFacade.GetInstance.GetSTAFFHP(item.STF_ID).GenericItem.schStaffHp;

                if (item.SCH_STATUS == "완료")
                {
                    resendMSG.Visible = false;
                }

            }

        }
        #endregion

        /// <summary>
        /// 문자 미리보기
        /// </summary>
        protected void lnkDummy2_Click(object sender, EventArgs e)
        {
            if(hddSCH_ID.Value == "")
            {
                resendMSG.Visible = false;
            }
            gstCpy.InnerText = hddGstCpy.Value;
            gstPst.InnerText = hddGstPst.Value;
            gstName.InnerText = hddGstName.Value;
            schType.InnerText = hddSchType.Value;

            StringBuilder sb = new StringBuilder();
            string ymd = hddSchYearMD.Value;
            sb.Append(ymd.Substring(0, 4));
            sb.Append("년 ");
            sb.Append(ymd.Substring(5, 2));
            sb.Append("월 ");
            sb.Append(ymd.Substring(8, 2));
            sb.Append("일 ");

            schYearMD.InnerText = sb.ToString();
            schHourMin.InnerText = hddSchHourMin.Value;

            // 대표 접견인 번호
            var memId = Convert.ToInt32(hddmsgStaff.Value);
            msgStaff.InnerText = ESFacade.GetInstance.GetMEMBER(memId).GenericItem.MEM_MOBILE_NO;

            UCPopOpenScript(upnlList, "");
        }
    }

}