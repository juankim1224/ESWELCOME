using ESNfx;
using ESNfx3.Web.Page;
using ESWELCOME.Core.Procedure;
using ESWELCOME.DataBase.Procedure.BOL.MSG;
using ESWELCOME.DataBase.Procedure.BOL.SCH;
using ESWELCOME.DataBase.Procedure.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
//using WEB.App_Code;

namespace WEB.schedule
{
    public partial class register : System.Web.UI.Page
    {
        #region property

        int schId;
        string gstMobileNo;
        string msgStaff;

        #endregion

        #region method
        protected void Page_Load(object sender, EventArgs e)
        {
            // 등록 or 수정
            if (!IsPostBack)
            {
                if (Request.Params["schId"] == null)
                {
                    newSch();
                }
                else
                {
                    hdd_SchId.Value = Request.Params["schId"];
                    EditSch();
                }
            }
        }

        /// <summary>
        /// HOUR MIN 바인딩
        /// </summary>
        protected void SchTimeBinding()
        {
            string str;

            List<string> hour = new List<string>();
            for (var i = 7; i <= 18; i++)
            {
                str = i + "";
                if (i < 10)
                {
                    str = "0" + str;
                }

                hour.Add(str);
            }

            List<string> min = new List<string>();
            for (var i = 0; i <= 55; i++)
            {
                str = i + "";
                if (i == 0 || i == 5)
                {
                    str = "0" + str;
                    // str.PadLeft(2, '0');
                    // string.Format("{0:D2}", str);
                }

                min.Add(str);
                i += 4;
            }

            SCH_HOUR.Items.Add("선택");
            MSG_HOUR.Items.Add("선택");
            foreach (var h in hour)
            {
                SCH_HOUR.Items.Add(h);
                MSG_HOUR.Items.Add(h);
            }

            SCH_MIN.Items.Add("선택");
            MSG_MIN.Items.Add("선택");
            foreach (var m in min)
            {
                SCH_MIN.Items.Add(m);
                MSG_MIN.Items.Add(m);
            }
        }

        /// <summary>
        /// 스케줄 등록
        /// </summary>
        protected void newSch()
        {
            lnkSave.Text = "등록";
            SchTimeBinding();
        }

        /// <summary>
        /// [수정] 스케줄 바인딩
        /// </summary>
        protected void EditSch()
        {
            lnkSave.Text = "수정";
            SchTimeBinding();
            schId = Convert.ToInt32(hdd_SchId.Value);

            var item = SCHFacade.GetInstance.GetSCHEDULE(schId).GenericItem;

            GST_CPY.Value = item.GST_CPY;
            GST_PST.Value = item.GST_PST;

            string subGstMobileNo = item.GST_MOBILE_NO;
            GST_MOBILE_NO1.Value += subGstMobileNo.Substring(0, 3);
            GST_MOBILE_NO2.Value += subGstMobileNo.Substring(3, 4);
            GST_MOBILE_NO3.Value += subGstMobileNo.Substring(7, 4);

            GST_NAME.Value = item.GST_NAME;

            string subSchYearMd = item.SCH_YEARMD;
            string strSchYearMd = subSchYearMd.Substring(0, 4) + "-" + subSchYearMd.Substring(5, 2) + "-" + subSchYearMd.Substring(6, 2);
            SCH_YEARMD.Text += strSchYearMd;

            SCH_HOUR.Text = item.SCH_HOUR;
            SCH_MIN.Text = item.SCH_MIN;

            SCH_TYPE.Value = item.SCH_TYPE;

            hdd_SCH_MONITER.Value = item.SCH_MONITER;

            // * ********** 접견인 ********** */
            string staffNames = item.STAFF_NAME;

            if (staffNames.Contains(','))
            {
                string[] arrName = staffNames.Split(',');
                foreach (var a in arrName)
                {
                    ltrStaffList.Text += string.Format("<p><input type=\"radio\" value={0} name=\"msgStaff\" /><label>{1}</label><a href=\"#\" class=\"del\"> X </a></p>", a, a);
                    hdd_ARR_STAFF.Value += hdd_ARR_STAFF.Value != "" ? "," + a : a;
                }
            }

            hdd_MSG_GUBUN.Value = item.MSG_GUBUN.ToString();

            if (item.MSG_STATUS == "발송")
            {
                MSG_YEARMD.Visible = false;
                MSG_HOUR.Visible = false;
                MSG_MIN.Visible = false;
            }
            else
            {
                MSG_YEARMD.Text = item.MSG_YEARMD;
                MSG_HOUR.Text = item.MSG_HOUR;
                MSG_MIN.Text = item.MSG_MIN;
            }

            hdd_TND_CHECK.Value = item.TND_CHECK;

        }

        #endregion

        /// <summary>
        /// 접견인 바인딩 1. 부서
        /// </summary>
        protected void lnkDummy_Click(object sender, EventArgs e)
        {
            var company = ES_COMPANY.Value;
            var dept = SCHFacade.GetInstance.InquirySTAFF(Convert.ToInt32(company), null, null);
            ES_DEPT.BindSelectBox<SCH_sd_STAFF>(dept, "DEPT", "DEPT");
            ES_DEPT.Items.Insert(0, new ListItem("선택", string.Empty));
        }

        /// <summary>
        /// 접견인 바인딩  2. 팀
        /// </summary>
        protected void lnkDummy2_Click(object sender, EventArgs e)
        {
            var dept = ES_DEPT.Value.Trim();
            var team = SCHFacade.GetInstance.InquirySTAFF(null, dept, null);

            ES_TEAM.BindSelectBox<SCH_sd_STAFF>(team, "TEAM", "TEAM");
            ES_TEAM.Items.Insert(0, new ListItem("선택", string.Empty));
        }

        /// <summary>
        /// 접견인 바인딩  3. 직원
        /// </summary>
        protected void lnkDummy3_Click(object sender, EventArgs e)
        {
            var team = ES_TEAM.Value.Trim();
            var staff = SCHFacade.GetInstance.InquirySTAFF(null, null, team);

            SCH_STAFF.BindSelectBox<SCH_sd_STAFF>(staff, "MEM_FULLNAME", "ID_FULLNAME");
            SCH_STAFF.Items.Insert(0, new ListItem("선택", string.Empty));
        }

        /// <summary>
        /// 접견인 추가
        /// </summary>
        protected void lnkDummy4_Click(object sender, EventArgs e)
        {
            var staffId = SCH_STAFF.Value.Split('&')[0];
            var staffName = SCH_STAFF.Value.Split('&')[1];

            if (ltrStaffList.Text.Contains(staffName) != true)
            {
                ltrStaffList.Text += string.Format("<p id={0}><input type=\"radio\" id={1} value={1} name=\"msgStaff\" /><label>{2}</label><a href=\"javascript:fnDeleteStaff({1})\" class=\"del\"> X </a></p>", "s" + staffId, staffId, staffName);
                hdd_ARR_STAFF.Value += hdd_ARR_STAFF.Value != "" ? "," + staffId : staffId;
            }
            else
            {
                ESNfx3.Web.Page.WebHelper.AjaxMessageAlert(Page, "중복된 선택입니다.", true);
                return;
            }

        }

        /// <summary>
        /// 스케줄 DB 등록 및 수정
        /// </summary>
        protected void lnkSave_Click(object sender, EventArgs e)
        {
            if (lnkSave.Text == "수정")
            {
                Edit_Click();
            }

            else if (lnkSave.Text == "등록")
            {
                gstMobileNo = GST_MOBILE_NO1.Value + GST_MOBILE_NO2.Value + GST_MOBILE_NO3.Value;

                // 스케줄 DB 등록
                iSCH_in_SCHEDULE newSchedule = new iSCH_in_SCHEDULE()
                {
                    GST_CPY = GST_CPY.Value,
                    GST_PST = GST_PST.Value,
                    GST_NAME = GST_NAME.Value,
                    GST_MOBILE_NO = gstMobileNo,
                    SCH_YEARMD = SCH_YEARMD.Value,
                    SCH_HOUR = SCH_HOUR.Text,
                    SCH_MIN = SCH_MIN.Text,
                    SCH_TYPE = SCH_TYPE.Value,
                    SCH_MONITER = hdd_SCH_MONITER.Value,
                    CRE_MEMID = 1,     // 하드코딩
                };

                // 접견인
                var staff = SchStaffRegister();
                var message = MessageRegister();

                string schPk = string.Empty;
                string msgCode = string.Empty;
                string msgStaff = Request.Params["msgStaff"];

                var ret = ProcManager.Proc.SCHFacade.ManageSchedule(newSchedule, staff, message, msgStaff, out schPk);

                if (ret.Result && !string.IsNullOrEmpty(schPk))
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RegisterKey1", "alert('등록이 완료되었습니다.');", true);
                    ESNfx3.Web.Page.WebHelper.AjaxMoveLocation(Page, "schMain.aspx", true);
                }

            }

        }


        /// <summary>
        /// 접견인 등록
        /// </summary>
        protected iSCH_iu_STAFF SchStaffRegister()
        {
            ESNfx.ReturnValue staffResult = new ReturnValue();
            var hddArrStaff = hdd_ARR_STAFF.Value;
            string[] arrStaff = hddArrStaff.Split(',');

            // 담당자 구분 (담당자: 1, 일반: 2)  *문자기입
            var gubun = 2;
            msgStaff = Request.Params["msgStaff"];

            var staff = new iSCH_iu_STAFF();

            foreach (var a in arrStaff)
            {
                if (a == msgStaff) { gubun = 1; } else { gubun = 2; }
                staff = new iSCH_iu_STAFF
                {
                    MEM_ID = Convert.ToInt32(a),
                    SCH_ID = schId,
                    STF_GUBUN = gubun,
                    IU_GUBUN = "I",
                    CRE_MEMID = 1,      // 하드코딩
                };
            }
            return staff;
        }


        /// <summary>
        /// 메세지 등록
        /// </summary>
        protected iMSG_iu_MESSAGE MessageRegister()
        {
            string msgYearMd;
            string msgHour;
            string msgMin;

            // 즉시 발송 선택
            if (hdd_MSG_GUBUN.Value == "1")
            {
                msgYearMd = null;
                msgHour = null;
                msgMin = null;
            }

            // 예약 발송 선택
            else
            {
                msgYearMd = MSG_YEARMD.Value;
                msgHour = MSG_HOUR.Text;
                msgMin = MSG_MIN.Text;
            }

            iMSG_iu_MESSAGE message = new iMSG_iu_MESSAGE()
            {
                SCH_ID = 0,
                MSG_GUBUN = Convert.ToInt32(hdd_MSG_GUBUN.Value),
                MSG_TO = gstMobileNo,
                MSG_CONTENT = "",
                MSG_YEARMD = msgYearMd,
                MSG_HOUR = msgHour,
                MSG_MIN = msgMin,
                TND_CHECK = hdd_TND_CHECK.Value,
                CRE_MEMID = 1,  // 하드코딩
            };

            return message;

        }


        /// <summary>
        /// 스케줄 수정
        /// </summary>
        protected void Edit_Click()
        {
            var param = new iSCH_un_SCHEDULE()
            {
                SCH_ID = Convert.ToInt32(hdd_SchId.Value),
                EDIT_MODE = "U",  // 수정모드(수정: 'U', 삭제: 'D')
                GST_CPY = GST_CPY.Value,
                GST_PST = GST_PST.Value,
                GST_NAME = GST_NAME.Value,
                GST_MOBILE_NO = GST_MOBILE_NO1.Value + GST_MOBILE_NO2.Value + GST_MOBILE_NO3.Value,
                SCH_YEARMD = SCH_YEARMD.Value,
                SCH_HOUR = SCH_HOUR.Text,
                SCH_MIN = SCH_MIN.Text,
                SCH_TYPE = SCH_TYPE.Value,
                SCH_MONITER = hdd_SCH_MONITER.Value,
            };

            var editResult = SCHFacade.GetInstance.UpdateSCHEDULE(param);

            // 접견인 및 문자 수정

            if (editResult.Result)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key123", "alert('수정이 완료되었습니다.');", true);
                ESNfx3.Web.Page.WebHelper.AjaxMoveLocation(Page, "schList.aspx", true);
            }
        }

    }
}