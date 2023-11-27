using ESWELCOME.DataBase.Procedure.BOL.SCH;
using ESWELCOME.DataBase.Procedure.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.schedule
{
    public partial class schList : System.Web.UI.Page
    {
        #region event

        protected void Page_Load(object sender, EventArgs e)
        {
            SchListBind();
        }

        protected void esnPager1_Command(object sender, CommandEventArgs e)
        {
            SchListBind();
        }

        #endregion


        #region method
        protected void SchListBind()
        {
            esnPager.PageViewCount = Convert.ToInt32(10);

            var param = new iSCH_sd_SCHEDULE_LIST()
            {
                SEARCH_TYPE = SEARCH_TYPE.Value,
                SEARCH_TXT = SEARCH_TXT.Value,
                SCH_TYPE = SCH_TYPE.Value,
                START_DATE = esFromToDate.ValueFrom,
                END_DATE = esFromToDate.ValueTo,
                CURRENTPAGEINDEX = esnPager.CurrentPageIndex + 1,
                PAGINGSIZE = esnPager.PageViewCount
            };

            var schList = SCHFacade.GetInstance.InquirySCHEDULE_LIST(param);

            if (schList.Count > 0)
            {
                esnPager.DataCount = Convert.ToInt32(schList[0].TOTAL_COUNT);
            }

            rptList.DataSource = schList;
            rptList.DataBind();
        }

        protected void searchSCH_Click(object sender, EventArgs e)
        {
            SchListBind();
        }

        #endregion

    }
}