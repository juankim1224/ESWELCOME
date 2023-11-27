using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WEB.App_Code;
using WEB.net.e_sang.api2;

namespace WEB.controls
{
    public partial class ucFromToDate : BaseUC
    {
        #region ## property ###

        public string ValueFrom
        {
            get
            {
                return esnDate.ValueFrom;
            }
            set
            {
                esnDate.TextFrom = value;
            }
        }

        public string ValueTo
        {
            get
            {
                return esnDate.ValueTo;
            }
            set
            {
                esnDate.TextTo = value;
            }
        }

        /// <summary>
        /// From date컨트롤 ClientID
        /// </summary>
        public string FromDateClientID
        {
            get
            {
                return esnDate.FromClientID;
            }
        }

        /// <summary>
        /// To date컨트롤 ClientID
        /// </summary>
        public string ToDateClientID
        {
            get
            {
                return esnDate.ToClientID;
            }
        }

        #endregion

        #region override
        protected override void FirstSetting()
        {
            if (!IsPostBack)
            {
                esnDate.TextFrom = GetPrevNoneHoliday_ucFromToDate(DateTime.Now.AddMonths(-1));
                esnDate.TextTo = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }

        public string GetPrevNoneHoliday_ucFromToDate(DateTime dateTime)
        {
            EsnAPISoapClient service = new EsnAPISoapClient();

            var isHoliday = CheckHoliday(dateTime);

            while (isHoliday)
            {
                dateTime = dateTime.AddDays(-1);
                isHoliday = CheckHoliday(dateTime);
            }

            return dateTime.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 입력받은 날짜가 토요일, 일요일인지 확인한다.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        bool CheckHoliday(DateTime dt)
        {
            return dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday;

            //var isHoliday = service.IsHoliday(dateTime.ToString("yyyyMMdd"));

            //while (isHoliday)
            //{
            //    dateTime = dateTime.AddDays(-1);
            //    isHoliday = service.IsHoliday(dateTime.ToString("yyyyMMdd"));
            //}
        }


        #endregion
    }

}