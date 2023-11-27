using ESWELCOME.DataBase.Procedure.BOL.MSG;
using ESWELCOME.DataBase.Procedure.BOL.SCH;
using ESWELCOME.DataBase.Procedure.Facade;
using System;
using System.Web.Services;
using System.Web.UI.WebControls;
using WEB.App_Code;

namespace WEB.schedule
{
    public partial class schMain : System.Web.UI.Page
    {
        #region property

        #endregion

        #region event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 현재 월
                DateTime nowDate = DateTime.Now;
                int currentMonth = nowDate.Month;
                SEARCH_MONTH.Items.Add(currentMonth + "월");

                for (int i = 1; i <= 12; i++)
                {
                    if (i != currentMonth)
                    {
                        if (i < 10)
                        {
                            SEARCH_MONTH.Items.Add("0" + i + "월");
                        }
                        else
                        {
                            SEARCH_MONTH.Items.Add(i + "월");
                        }
                    }
                }
            }
            ListBind();
        }

        protected void esnPager1_Command(object sender, CommandEventArgs e)
        {
            ListBind();
        }

        protected void searchSCH_Click(object sender, EventArgs e)
        {
            esnPager1.DataCount = 0;
            ListBind();
        }

        #endregion

        #region method
        /// <summary>
        /// 스케줄 전체 조회
        /// </summary>
        protected void ListBind()
        {
            esnPager1.PageViewCount = Convert.ToInt32(10);

            var param = new iSCH_sd_SCHEDULE_MAIN
            {
                SEARCH_YEAR = SEARCH_YEAR.Value,
                SEARCH_MONTH = "12",
                CURRENTPAGEINDEX = esnPager1.CurrentPageIndex + 1,
                PAGINGSIZE = esnPager1.PageViewCount,
            };

            var schList = SCHFacade.GetInstance.InquirySCHEDULE_MAIN(param);

            if (schList.Count > 0)
            {
                esnPager1.DataCount = Convert.ToInt32(schList[0].TOTAL_COUNT);
            }

            rptList.DataSource = schList;
            rptList.DataBind();

        }

        /// <summary>
        /// 문자 재전송
        /// </summary>
        [WebMethod]
        public static string ResendSMS(string schId, string msgEmpHp)
        {
            var schedule = SCHFacade.GetInstance.GetSCHEDULE(Convert.ToInt32(schId)).GenericItem;
            string result = "문자 전송이 실패하였습니다.";

            if (schedule.SCH_ID != null)
            {
                var msgContent = ESExtension.MakeMsgContent(schedule.SCH_TYPE, schedule.GST_CPY, schedule.GST_PST, schedule.GST_NAME, schedule.SCH_YEARMD, schedule.SCH_HOUR, schedule.SCH_MIN, schedule.MSG_CODE, msgEmpHp);

                var param = new iMSG_iu_MESSAGE()
                {
                    SCH_ID = Convert.ToInt32(schId),
                    MSG_GUBUN = 1,  // 즉시 전송
                    MSG_TO = schedule.GST_MOBILE_NO,
                    MSG_CONTENT = msgContent,
                    TND_CHECK = "N",
                    CRE_MEMID = 2,  // 하드 코딩
                };

                var msgResult = MSGFacade.GetInstance.MSG_iu_MESSAGE(param);

                if (msgResult.Result)
                {
                    result = "재전송이 완료되었습니다.";
                }
            }

            return result;

        }

        #endregion
    }

}