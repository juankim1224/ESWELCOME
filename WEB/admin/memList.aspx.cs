using ESNfx.Web.Controls;
using ESWELCOME.DataBase.Procedure.BOL.ADM;
using ESWELCOME.DataBase.Procedure.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        #endregion

    }
}