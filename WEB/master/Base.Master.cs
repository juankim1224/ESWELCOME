using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.master
{
    public partial class Base : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void sm_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            sm.AsyncPostBackErrorMessage = e.Exception.Message;
        }
    }
}