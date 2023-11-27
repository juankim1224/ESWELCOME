using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using ESNfx.DataBase;

namespace ESWELCOME.DataBase.Procedure.BOL.MSG
{
    public class MSG_sr_SCHSTAFF
    {
        #region 생성자
        public MSG_sr_SCHSTAFF()
        {
            this._schStaffHp = null;
        }
        #endregion

        #region 프로퍼티

        private string _schStaffHp;

        [ESBind("schStaffHp", "")]
        [ESNfx.Attributes.ExcelBind("schStaffHp")]
        public string schStaffHp
        {
            get { return _schStaffHp; }
            set { _schStaffHp = value; }
        }

        #endregion
    }

    public class MSG_sr_STAFFHP
    {
        #region 생성자
        public MSG_sr_STAFFHP()
        {
            this._schStaffHp = null;
        }
        #endregion

        #region 프로퍼티

        private string _schStaffHp;

        [ESBind("schStaffHp", "")]
        [ESNfx.Attributes.ExcelBind("schStaffHp")]
        public string schStaffHp
        {
            get { return _schStaffHp; }
            set { _schStaffHp = value; }
        }

        #endregion
    }


    public class iMSG_iu_MESSAGE
    {
        #region 생성자
        public iMSG_iu_MESSAGE()
        {
            this._MSG_ID = null;
            this._SCH_ID = null;
            this._MSG_GUBUN = null;
            this._MSG_TO = null;
            this._MSG_FROM = null;
            this._MSG_CONTENT = null;
            this._MSG_YEARMD = null;
            this._MSG_HOUR = null;
            this._MSG_MIN = null;
            this._TND_CHECK = null;
            this._CRE_MEMID = null;
        }
        #endregion

        #region 프로퍼티
        private int? _MSG_ID;

        public int? MSG_ID
        {
            get { return _MSG_ID; }
            set { _MSG_ID = value; }
        }

        private int? _SCH_ID;

        public int? SCH_ID
        {
            get { return _SCH_ID; }
            set { _SCH_ID = value; }
        }

        private int? _MSG_GUBUN;

        public int? MSG_GUBUN
        {
            get { return _MSG_GUBUN; }
            set { _MSG_GUBUN = value; }
        }

        private string _MSG_TO;

        public string MSG_TO
        {
            get { return _MSG_TO; }
            set { _MSG_TO = value; }
        }

        private string _MSG_FROM;

        public string MSG_FROM
        {
            get { return _MSG_FROM; }
            set { _MSG_FROM = value; }
        }

        private string _MSG_CONTENT;

        public string MSG_CONTENT
        {
            get { return _MSG_CONTENT; }
            set { _MSG_CONTENT = value; }
        }

        private string _MSG_YEARMD;

        public string MSG_YEARMD
        {
            get { return _MSG_YEARMD; }
            set { _MSG_YEARMD = value; }
        }

        private string _MSG_HOUR;

        public string MSG_HOUR
        {
            get { return _MSG_HOUR; }
            set { _MSG_HOUR = value; }
        }

        private string _MSG_MIN;

        public string MSG_MIN
        {
            get { return _MSG_MIN; }
            set { _MSG_MIN = value; }
        }

        private string _TND_CHECK;

        public string TND_CHECK
        {
            get { return _TND_CHECK; }
            set { _TND_CHECK = value; }
        }

        private int? _CRE_MEMID;

        public int? CRE_MEMID
        {
            get { return _CRE_MEMID; }
            set { _CRE_MEMID = value; }
        }

        #endregion
    }


}
