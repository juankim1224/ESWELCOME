using ESWELCOME.DataBase.Procedure.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.guest
{
    public partial class gstWelcome : System.Web.UI.Page
    {
        #region property
        string schId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                schId = Request.Params["schId"];
                if (schId != null)
                {
                    getSchecule();
                }
            }
        }
        #endregion

        protected void getSchecule()
        {
            var numSchId = Convert.ToInt32(schId);
            var item = SCHFacade.GetInstance.GetSCHEDULE(numSchId).GenericItem;

            if (item != null)
            {
                string md = item.SCH_YEARMD;
                string str;
                str = md.Substring(4, 2) + "월 " + md.Substring(6, 2) + "일 ";
                str += item.SCH_HOUR + "시 " + item.SCH_MIN + "분";
                schMDHM.Text = str;
                GST_NAME.InnerText = item.GST_NAME;
            }

        }


    }
}