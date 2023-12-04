using ESNfx.Web.Controls;
using ESNfx3.Web.Page;
using ESWELCOME.Core.Procedure;
using ESWELCOME.DataBase.Procedure.BOL.ADM;
using ESWELCOME.DataBase.Procedure.BOL.ES;
using ESWELCOME.DataBase.Procedure.Facade;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.schedule;

namespace WEB.admin
{
    public partial class memList : System.Web.UI.Page
    {
        #region method
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MemBinding();
            }

        }

        protected void MemBinding()
        {
            esnPager.PageViewCount = Convert.ToInt32(10);

            var param = new iADM_sd_MEMBER()
            {
                MEMBER_CPY = MEMBER_CPY.Value,
                SEARCH_TYPE = SEARCH_TYPE.Value,
                SEARCH_TEXT = SEARCH_TEXT.Value,
                CURRENTPAGEINDEX = esnPager.CurrentPageIndex + 1,
                PAGINGSIZE = esnPager.PageViewCount
            };
            var memList = ADMFacade.GetInstance.InquiryMEMBER(param);

            if (memList.Count > 0)
            {
                esnPager.DataCount = Convert.ToInt32(memList[0].TOTAL_COUNT);
            }

            rptList.DataSource = memList;
            rptList.DataBind();

        }
        #endregion

        #region event
        protected void searchEmp_Click(object sender, EventArgs e)
        {
            MemBinding();

        }

        protected void esnPager_Command(object sender, CommandEventArgs e)
        {
            MemBinding();
        }

        /// <summary>
        /// 임직원 삭제
        /// </summary>
        [WebMethod]
        public static string DeleteMem(string usrids)
        {

            string[] data = usrids.TrimEnd(',').Split(',');

            var Result = "False";

            foreach (string item in data)
            {
                var ret = ADMFacade.GetInstance.UpdateDeleteMEM(Convert.ToInt32(item));
                //    if (ret["@ERR_CD"].ToString() == "1")
                //    Result = "True";
                //else
                //    Result = "False";

                Result =  "Y";
            }

            return Result;
        }


        /// <summary>
        /// 임직원 비밀번호 초기화
        /// </summary>
        [WebMethod]
        public static string ResetPwd(string memId)
        {
            var Result = "F";

            iES_un_LOGIN_PW member = new iES_un_LOGIN_PW()
            {
                MEM_TYPE = 1,
                MEM_ID = Convert.ToInt32(memId),
                BEFORE_PW = null,
                AFTER_PW = null
            };
            
            var PwdResult = ESFacade.GetInstance.UpdateLOGIN_PW(member);

            if(PwdResult.Result)
            {
                Result = "Y";
            } else
            {
                Result = "F";
            }

            return Result;

        }

        #endregion

        protected void lnkDummy_Click(object sender, EventArgs e)
        {
            MemBinding();

        }

        // 멤버 바인딩 (임시)
        protected void MemBinding2(ObjectStateFormatter sender)
        {
            MemBinding();

        }
    }
}