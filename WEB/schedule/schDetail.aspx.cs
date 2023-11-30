using ESWELCOME.DataBase.Procedure.BOL.SCH;
using ESWELCOME.DataBase.Procedure.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.schedule
{
    public partial class schDetail : System.Web.UI.Page
    {

        #region property

        string schId;

        #endregion

        #region method
        protected void Page_Load(object sender, EventArgs e)
        {
            schId = Request.Params["schId"];
            hdd_SchId.Value = schId;

            int nSchId = Convert.ToInt32(schId);

            var item = SCHFacade.GetInstance.GetSCHEDULE(nSchId);

            if (item.Exist)
            {
                var sch = item.GenericItem;
                GST_CPY.Text = sch.GST_CPY;
                GST_PST.Text = sch.GST_PST;

                string subGstHp = sch.GST_MOBILE_NO;
                int length = subGstHp.Length;

                string num1 = subGstHp.Substring(0, 3);
                num1 += "-";
                num1 += subGstHp.Substring(3, 4);
                num1 += "-";
                num1 += subGstHp.Substring(7, 4);

                GST_HP.Text = num1;
                GST_NAME.Text = sch.GST_NAME;

                StringBuilder sb = new StringBuilder();
                string ymd = sch.SCH_YEARMD;
                sb.Append(ymd.Substring(0, 4));
                sb.Append("년 ");
                sb.Append(ymd.Substring(4, 2));
                sb.Append("월 ");
                sb.Append(ymd.Substring(6, 2));
                sb.Append("일 ");
                SCH_YEARMD.Text = sb.ToString();

                SCH_HOUR.Text = sch.SCH_HOUR + "시";
                SCH_MIN.Text = sch.SCH_MIN + "분  ";
                SCH_TYPE.Text = sch.SCH_TYPE;
                SCH_MONITER.Text = sch.SCH_MONITER;
                hdd_SchStatus.Value = sch.SCH_STATUS;

                // 모니터 표시 (희망: 'Y', 비희망: 'N')
                if (sch.SCH_MONITER == "Y")
                {
                    SCH_MONITER.Text = "희망";
                }
                else
                {
                    SCH_MONITER.Text = "비희망";
                }

                /* ********** 문자 ********** */
                // 발송완료시 공백
                if (sch.MSG_STATUS == "발송")
                {
                    MSG_STATUS.Text = "발송완료";
                    MSG_GUBUN.Text = "";
                    MSG_YEARMD.Text = "";
                    MSG_HOUR.Text = "";
                    MSG_MIN.Text = "";
                }
                else
                {
                    StringBuilder sb2 = new StringBuilder();
                    string ymd2 = sch.MSG_YEARMD;
                    sb2.Append(ymd2.Substring(0, 4));
                    sb2.Append("년 ");
                    sb2.Append(ymd2.Substring(4, 2));
                    sb2.Append("월 ");
                    sb2.Append(ymd2.Substring(6, 2));
                    sb2.Append("일 ");
                    MSG_YEARMD.Text = sb2.ToString();

                    MSG_HOUR.Text = sch.MSG_HOUR.ToString() + "시 ";
                    MSG_MIN.Text = sch.MSG_MIN.ToString() + "분  예정";
                }

                // 2차 발송 (희망: 'Y', 비희망: 'N')
                if (sch.TND_CHECK == "Y")
                {
                    TND_CHECK.Text = "희망";
                }
                else
                {
                    TND_CHECK.Text = "비희망";
                }

                // 접견인
                //var staffList = SCHFacade.GetInstance.InquirySCHSTAFF(sch.STAFF_ID);
              //  rptList.DataSource = staffList;
             //   rptList.DataBind();
                //foreach(var staff in staffList)
                //{
                //    STAFF_FULLNAME.Text += string.Format("<p>{0} / {1} / {2} / {3} </p>", staff.staffCompany, staff.staffDept, staff.staffTeam, staff.staffFullName);
                //}

                // 수정
                var staffList = SCHFacade.GetInstance.InquiryStaffForSchId(Convert.ToInt32(schId));
                rptList.DataSource = staffList;
                rptList.DataBind();
            }

        }

        #endregion

        #region event
        /// <summary>
        /// 스케줄 삭제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            var param = new iSCH_un_SCHEDULE()
            {
                SCH_ID = Convert.ToInt32(hdd_SchId.Value),
                EDIT_MODE = "D"
            };

            var result = SCHFacade.GetInstance.UpdateSCHEDULE(param);

            if (result.Result)
            {
                ESNfx3.Web.Page.WebHelper.AjaxMessageAlert(this.Page, "삭제가 완료되었습니다.", true);
                ESNfx3.Web.Page.WebHelper.AjaxMoveLocation(Page, " schlist.aspx", true);
                return;
            }
        }

        #endregion
    }
}