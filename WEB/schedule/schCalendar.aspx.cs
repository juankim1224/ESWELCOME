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
    public partial class schCalendar : System.Web.UI.Page
    {
        #region property

        public string[] pWeekDay = { "일", "월", "화", "수", "목", "금", "토" };

        private string _CurYear;
        public string pCurYear
        {
            get { return _CurYear; }
            set { _CurYear = value; }
        }

        private string _CurMonth;
        public string pCurMonth
        {
            get { return _CurMonth; }
            set { _CurMonth = value; }
        }

        #endregion


        #region
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setting();
                MakeCalendar();
            }

        }

        /// <summary>
        /// 현재 날짜 설정
        /// </summary>
        protected void Setting()
        {
            litYear.Text = DateTime.Today.Year.ToString();
            litMonth.Text = DateTime.Today.Month.ToString();

            hddYear.Value = DateTime.Today.Year.ToString();
            hddMonth.Value = DateTime.Today.Month.ToString();

            SEARCH_MONTH.Items.Add(litMonth.Text);
            for (var i = 1; i <= 12; i++)
            {
                if (i != Convert.ToInt32(litMonth.Text))
                {
                    SEARCH_MONTH.Items.Add((i < 10 ? "0" + i : i.ToString()));
                }
            }
        }

        /// <summary>
        /// 달력 생성
        /// </summary>
        private void MakeCalendar()
        {
            pCurYear = hddYear.Value.Trim();
            pCurMonth = hddMonth.Value.Trim();

            litYear.Text = pCurYear;
            litMonth.Text = pCurMonth;

            string schYearMd = pCurYear + pCurMonth;
            if (schYearMd.Length == 5) // 1~9월
            {
                schYearMd = string.Format("{0}0{1}", pCurYear, pCurMonth);
            }

            // 스케줄 목록
            var scheduleList = SCHFacade.GetInstance.InquiryCALENDAR(pCurYear, pCurMonth);

            DateTime curDay = new DateTime(int.Parse(pCurYear), int.Parse(pCurMonth), 1);   // 20231101
            int iWeekDay = Convert.ToInt32(curDay.DayOfWeek);   // 월-1, 화-2, 수-3 ...

            DateTime useDay = new DateTime(int.Parse(pCurYear), int.Parse(pCurMonth), 1).AddMonths(1).AddDays(-1);  // 20231031
            int iLastDay = useDay.Day;

            // th 선언
            TableHeaderRow hrow = new TableHeaderRow();


            // 일~토 cell : 일요일은 sunday 클래스 적용
            for (int i = 0; i < pWeekDay.Length; i++)
            {
                // <th>
                TableHeaderCell tcell = new TableHeaderCell();
                tcell.Text = pWeekDay[i];
                if (pWeekDay[i] == "일") tcell.CssClass = "sunday";
                hrow.Cells.Add(tcell);
            }
            // asp:table에 적용된 th 추가
            this.tblCalendar.Rows.Add(hrow);


            for (int i = 0, d = 1, p = 0, n = 0; i < 6; i++)
            {

                TableRow row = new TableRow();

                for (int j = 0; j < 7; j++)
                {
                    TableCell cell = new TableCell();

                    // 전 달의 끝 날짜들  // 10/29, 10/30, 10/31
                    if (i * 7 + j + 1 <= iWeekDay)
                    {
                        int previousMonthLastDay = curDay.AddDays(-1).Day;
                        int previousMonthDay = previousMonthLastDay - (iWeekDay - 1);
                        cell.CssClass = "calendar-in before-date";
                        row.Cells.Add(cell);
                        p++;
                    }
                    // 해당 달의 날짜를 벗어난 빈 셀들 1부터 채워 넣기  // 12/1, 12/2
                    else if (iLastDay < d)
                    {
                        n++;
                        cell.CssClass = "calendar-in before-date";
                        row.Cells.Add(cell);
                    }
                    // 해당 달의 일자 채워넣기
                    else
                    {
                        DateTime curDate = new DateTime(int.Parse(pCurYear), int.Parse(pCurMonth), d);
                        if (Convert.ToInt32(curDate.DayOfWeek) == 0) cell.CssClass = "sunday";
                        StringBuilder sCellText = new StringBuilder(string.Format("<div class='calendar-in' style=\"position: relative;\"><span>{0}</span>", d.ToString()));
                        string Day = d.ToString().Length == 1 ? "0" + d.ToString() : d.ToString();

                        foreach (var schedule in scheduleList)
                        {
                            if (Day == schedule.SCH_DAY)
                            {
                                var equalDayCount = scheduleList.FindAll(x => x.SCH_DAY == Day).Count;
                                
                                if (equalDayCount < 3)
                                {
                                    switch (schedule.SCH_TYPE)
                                    {
                                        case "1차면접":
                                        case "2차면접":
                                            sCellText.AppendFormat("<a href=\"javascript:fnschDetail('{0}');\"><p class=\"pclass\"><strong class=\"interview\">면접</strong>{1}:{2} {3}님</p></a>", schedule.SCH_ID, schedule.SCH_HOUR, schedule.SCH_MIN, schedule.GST_NAME);
                                            break;
                                        case "미팅":
                                            sCellText.AppendFormat("<a href=\"javascript:fnschDetail('{0}');\"><p class=\"pclass\"><strong class=\"meeting\">미팅</strong>{1}:{2} {3}님</p></a>", schedule.SCH_ID, schedule.SCH_HOUR, schedule.SCH_MIN, schedule.GST_NAME);
                                            break;
                                        case "기타":
                                            sCellText.AppendFormat("<a href=\"javascript:fnschDetail('{0}');\"><p class=\"pclass\"><strong class=\"etc\">기타</strong>{1}:{2} {3}님</p></a>", schedule.SCH_ID, schedule.SCH_HOUR, schedule.SCH_MIN, schedule.GST_NAME);
                                            break;
                                    }
                                }
                                else
                                {
                                    sCellText.AppendFormat("<span class=\"threeUp\">총 {0}건</span><ul style=\"visibility: hidden;  width: 185px; height: 200px; overflow-y: scroll; position: absolute; left: 30px; background-color: white; border: 1px solid gray; z-index: 1;\">", scheduleList.Count);
                                    break;
                                }


                            }
                        }

                        foreach(var sch in scheduleList)
                        {
                            if(Day == sch.SCH_DAY)
                            {
                                var equalDayCount = scheduleList.FindAll(x => x.SCH_DAY == Day).Count;
                                if(equalDayCount >= 3)
                                {
                                    switch (sch.SCH_TYPE)
                                    {
                                        case "1차면접":
                                        case "2차면접":
                                            sCellText.AppendFormat("<li><a href=\"javascript:fnschDetail('{0}');\"><p class=\"pclass\"><strong class=\"interview\">면접</strong>{1}:{2} {3}님</p></a><li>", sch.SCH_ID, sch.SCH_HOUR, sch.SCH_MIN, sch.GST_NAME);
                                            break;
                                        case "미팅":
                                            sCellText.AppendFormat("<li><a href=\"javascript:fnschDetail('{0}');\"><p class=\"pclass\"><strong class=\"meeting\">미팅</strong>{1}:{2} {3}님</p></a><li>", sch.SCH_ID, sch.SCH_HOUR, sch.SCH_MIN, sch.GST_NAME);
                                            break;
                                        case "기타":
                                            sCellText.AppendFormat("<li><a href=\"javascript:fnschDetail('{0}');\"><p class=\"pclass\"><strong class=\"etc\">기타</strong>{1}:{2} {3}님</p></a><li>", sch.SCH_ID, sch.SCH_HOUR, sch.SCH_MIN, sch.GST_NAME);
                                            break;
                                    }
                                }
                            }

                        }



                        cell.Text = sCellText.AppendLine("</ul></div>").ToString();

                        d++;
                        row.Cells.Add(cell);
                    }
                }
                this.tblCalendar.Rows.Add(row);
            }
        }

        /// <summary>
        /// 날짜 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void searchSch_Click(object sender, EventArgs e)
        {
            hddYear.Value = SEARCH_YEAR.Value;
            hddMonth.Value = SEARCH_MONTH.Text;
            MakeCalendar();
        }

        protected void lnkPrevBtn_Click(object sender, EventArgs e)
        {
            hddYear.Value = "2023"; // 임시
            hddMonth.Value = (Convert.ToInt32(hddMonth.Value) - 1).ToString();

            MakeCalendar();
        }

        protected void lnkNextBtn_Click(object sender, EventArgs e)
        {
            hddYear.Value = "2023";
            hddMonth.Value = (Convert.ToInt32(hddMonth.Value) + 1).ToString();

            MakeCalendar();
        }
    }

    #endregion
}