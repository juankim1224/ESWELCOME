using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using ESNfx.DataBase;

namespace ESWELCOME.DataBase.Procedure.BOL.SCH
{
    public class SCH_sr_SCHEDULE
    {
        #region 생성자
        public SCH_sr_SCHEDULE()
        {
            this._SCH_ID = null;
            this._GST_CPY = null;
            this._GST_PST = null;
            this._GST_NAME = null;
            this._GST_MOBILE_NO = null;
            this._SCH_YEARMD = null;
            this._SCH_HOUR = null;
            this._SCH_MIN = null;
            this._SCH_TYPE = null;
            this._SCH_MONITER = null;
            this._SCH_STATUS = null;
            this._MSG_CODE = null;
            this._MSG_GUBUN = null;
            this._MSG_YEARMD = null;
            this._MSG_HOUR = null;
            this._MSG_MIN = null;
            this._TND_CHECK = null;
            this._MSG_CONTENT = null;
            this._MSG_STATUS = null;
            this._MSG_VALIDITY = null;
            this._STF_ID = null;
            this._STAFF_NAME = null;
            this._STAFF_ID = null;
        }
        #endregion

        #region 프로퍼티

        private int? _SCH_ID;

        [ESBind("SCH_ID", "")]
        [ESNfx.Attributes.ExcelBind("SCH_ID")]
        public int? SCH_ID
        {
            get { return _SCH_ID; }
            set { _SCH_ID = value; }
        }

        private string _GST_CPY;

        [ESBind("GST_CPY", "")]
        [ESNfx.Attributes.ExcelBind("GST_CPY")]
        public string GST_CPY
        {
            get { return _GST_CPY; }
            set { _GST_CPY = value; }
        }

        private string _GST_PST;

        [ESBind("GST_PST", "")]
        [ESNfx.Attributes.ExcelBind("GST_PST")]
        public string GST_PST
        {
            get { return _GST_PST; }
            set { _GST_PST = value; }
        }

        private string _GST_NAME;

        [ESBind("GST_NAME", "")]
        [ESNfx.Attributes.ExcelBind("GST_NAME")]
        public string GST_NAME
        {
            get { return _GST_NAME; }
            set { _GST_NAME = value; }
        }

        private string _GST_MOBILE_NO;

        [ESBind("GST_MOBILE_NO", "")]
        [ESNfx.Attributes.ExcelBind("GST_MOBILE_NO")]
        public string GST_MOBILE_NO
        {
            get { return _GST_MOBILE_NO; }
            set { _GST_MOBILE_NO = value; }
        }

        private string _SCH_YEARMD;

        [ESBind("SCH_YEARMD", "")]
        [ESNfx.Attributes.ExcelBind("SCH_YEARMD")]
        public string SCH_YEARMD
        {
            get { return _SCH_YEARMD; }
            set { _SCH_YEARMD = value; }
        }

        private string _SCH_HOUR;

        [ESBind("SCH_HOUR", "")]
        [ESNfx.Attributes.ExcelBind("SCH_HOUR")]
        public string SCH_HOUR
        {
            get { return _SCH_HOUR; }
            set { _SCH_HOUR = value; }
        }

        private string _SCH_MIN;

        [ESBind("SCH_MIN", "")]
        [ESNfx.Attributes.ExcelBind("SCH_MIN")]
        public string SCH_MIN
        {
            get { return _SCH_MIN; }
            set { _SCH_MIN = value; }
        }

        private string _SCH_TYPE;

        [ESBind("SCH_TYPE", "")]
        [ESNfx.Attributes.ExcelBind("SCH_TYPE")]
        public string SCH_TYPE
        {
            get { return _SCH_TYPE; }
            set { _SCH_TYPE = value; }
        }

        private string _SCH_MONITER;

        [ESBind("SCH_MONITER", "")]
        [ESNfx.Attributes.ExcelBind("SCH_MONITER")]
        public string SCH_MONITER
        {
            get { return _SCH_MONITER; }
            set { _SCH_MONITER = value; }
        }

        private string _SCH_STATUS;

        [ESBind("SCH_STATUS", "")]
        [ESNfx.Attributes.ExcelBind("SCH_STATUS")]
        public string SCH_STATUS
        {
            get { return _SCH_STATUS; }
            set { _SCH_STATUS = value; }
        }

        private string _MSG_CODE;

        [ESBind("MSG_CODE", "")]
        [ESNfx.Attributes.ExcelBind("MSG_CODE")]
        public string MSG_CODE
        {
            get { return _MSG_CODE; }
            set { _MSG_CODE = value; }
        }

        private int? _MSG_GUBUN;

        [ESBind("MSG_GUBUN", "")]
        [ESNfx.Attributes.ExcelBind("MSG_GUBUN")]
        public int? MSG_GUBUN
        {
            get { return _MSG_GUBUN; }
            set { _MSG_GUBUN = value; }
        }

        private string _MSG_YEARMD;

        [ESBind("MSG_YEARMD", "")]
        [ESNfx.Attributes.ExcelBind("MSG_YEARMD")]
        public string MSG_YEARMD
        {
            get { return _MSG_YEARMD; }
            set { _MSG_YEARMD = value; }
        }

        private string _MSG_HOUR;

        [ESBind("MSG_HOUR", "")]
        [ESNfx.Attributes.ExcelBind("MSG_HOUR")]
        public string MSG_HOUR
        {
            get { return _MSG_HOUR; }
            set { _MSG_HOUR = value; }
        }

        private string _MSG_MIN;

        [ESBind("MSG_MIN", "")]
        [ESNfx.Attributes.ExcelBind("MSG_MIN")]
        public string MSG_MIN
        {
            get { return _MSG_MIN; }
            set { _MSG_MIN = value; }
        }

        private string _TND_CHECK;

        [ESBind("TND_CHECK", "")]
        [ESNfx.Attributes.ExcelBind("TND_CHECK")]
        public string TND_CHECK
        {
            get { return _TND_CHECK; }
            set { _TND_CHECK = value; }
        }

        private string _MSG_CONTENT;

        [ESBind("MSG_CONTENT", "")]
        [ESNfx.Attributes.ExcelBind("MSG_CONTENT")]
        public string MSG_CONTENT
        {
            get { return _MSG_CONTENT; }
            set { _MSG_CONTENT = value; }
        }

        private string _MSG_STATUS;

        [ESBind("MSG_STATUS", "")]
        [ESNfx.Attributes.ExcelBind("MSG_STATUS")]
        public string MSG_STATUS
        {
            get { return _MSG_STATUS; }
            set { _MSG_STATUS = value; }
        }

        private string _MSG_VALIDITY;

        [ESBind("MSG_VALIDITY", "")]
        [ESNfx.Attributes.ExcelBind("MSG_VALIDITY")]
        public string MSG_VALIDITY
        {
            get { return _MSG_VALIDITY; }
            set { _MSG_VALIDITY = value; }
        }

        private int? _STF_ID;

        [ESBind("STF_ID", "")]
        [ESNfx.Attributes.ExcelBind("STF_ID")]
        public int? STF_ID
        {
            get { return _STF_ID; }
            set { _STF_ID = value; }
        }

        private string _STAFF_NAME;

        [ESBind("STAFF_NAME", "")]
        [ESNfx.Attributes.ExcelBind("STAFF_NAME")]
        public string STAFF_NAME
        {
            get { return _STAFF_NAME; }
            set { _STAFF_NAME = value; }
        }

        private string _STAFF_ID;

        [ESBind("STAFF_ID", "")]
        [ESNfx.Attributes.ExcelBind("STAFF_ID")]
        public string STAFF_ID
        {
            get { return _STAFF_ID; }
            set { _STAFF_ID = value; }
        }

        #endregion
    }

    public class SCH_sd_StaffForSchId
    {
        #region 생성자
        public SCH_sd_StaffForSchId()
        {
            this._COMPANY = null;
            this._DEPT = null;
            this._TEAM = null;
            this._MEM_ID = null;
            this._MEM_FULLNAME = null;
            this._STAFF_GUBUN = null;
            this._ID_FULLNAME = null;
        }
        #endregion

        #region 프로퍼티

        private string _COMPANY;

        [ESBind("COMPANY", "")]
        [ESNfx.Attributes.ExcelBind("COMPANY")]
        public string COMPANY
        {
            get { return _COMPANY; }
            set { _COMPANY = value; }
        }

        private string _DEPT;

        [ESBind("DEPT", "")]
        [ESNfx.Attributes.ExcelBind("DEPT")]
        public string DEPT
        {
            get { return _DEPT; }
            set { _DEPT = value; }
        }

        private string _TEAM;

        [ESBind("TEAM", "")]
        [ESNfx.Attributes.ExcelBind("TEAM")]
        public string TEAM
        {
            get { return _TEAM; }
            set { _TEAM = value; }
        }

        private int? _MEM_ID;

        [ESBind("MEM_ID", "")]
        [ESNfx.Attributes.ExcelBind("MEM_ID")]
        public int? MEM_ID
        {
            get { return _MEM_ID; }
            set { _MEM_ID = value; }
        }

        private string _MEM_FULLNAME;

        [ESBind("MEM_FULLNAME", "")]
        [ESNfx.Attributes.ExcelBind("MEM_FULLNAME")]
        public string MEM_FULLNAME
        {
            get { return _MEM_FULLNAME; }
            set { _MEM_FULLNAME = value; }
        }

        private short? _STAFF_GUBUN;

        [ESBind("STAFF_GUBUN", "")]
        [ESNfx.Attributes.ExcelBind("STAFF_GUBUN")]
        public short? STAFF_GUBUN
        {
            get { return _STAFF_GUBUN; }
            set { _STAFF_GUBUN = value; }
        }


        private string _ID_FULLNAME;

        [ESBind("ID_FULLNAME", "")]
        [ESNfx.Attributes.ExcelBind("ID_FULLNAME")]
        public string ID_FULLNAME
        {
            get { return _ID_FULLNAME; }
            set { _ID_FULLNAME = value; }
        }

        #endregion
    }

    public class SCH_sd_STAFF
    {
        #region 생성자
        public SCH_sd_STAFF()
        {
            this._DEPT = null;
            this._TEAM = null;
            this._MEM_ID = null;
            this._MEM_FULLNAME = null;
            this._ID_FULLNAME = null;
        }
        #endregion

        #region 프로퍼티

        private string _DEPT;

        [ESBind("DEPT", "")]
        [ESNfx.Attributes.ExcelBind("DEPT")]
        public string DEPT
        {
            get { return _DEPT; }
            set { _DEPT = value; }
        }

        private string _TEAM;

        [ESBind("TEAM", "")]
        [ESNfx.Attributes.ExcelBind("TEAM")]
        public string TEAM
        {
            get { return _TEAM; }
            set { _TEAM = value; }
        }

        private int? _MEM_ID;

        [ESBind("MEM_ID", "")]
        [ESNfx.Attributes.ExcelBind("MEM_ID")]
        public int? MEM_ID
        {
            get { return _MEM_ID; }
            set { _MEM_ID = value; }
        }

        private string _MEM_FULLNAME;

        [ESBind("MEM_FULLNAME", "")]
        [ESNfx.Attributes.ExcelBind("MEM_FULLNAME")]
        public string MEM_FULLNAME
        {
            get { return _MEM_FULLNAME; }
            set { _MEM_FULLNAME = value; }
        }

        private string _ID_FULLNAME;

        [ESBind("ID_FULLNAME", "")]
        [ESNfx.Attributes.ExcelBind("ID_FULLNAME")]
        public string ID_FULLNAME
        {
            get { return _ID_FULLNAME; }
            set { _ID_FULLNAME = value; }
        }

        #endregion
    }

    public class SCH_sd_SCHSTAFF
    {
        #region 생성자
        public SCH_sd_SCHSTAFF()
        {
            this._staffId = null;
            this._staffCompany = null;
            this._staffDept = null;
            this._staffTeam = null;
            this._staffFullName = null;
            this._staffGubun = null;
        }
        #endregion

        #region 프로퍼티

        private int? _staffId;

        [ESBind("staffId", "")]
        [ESNfx.Attributes.ExcelBind("staffId")]
        public int? staffId
        {
            get { return _staffId; }
            set { _staffId = value; }
        }

        private string _staffCompany;

        [ESBind("staffCompany", "")]
        [ESNfx.Attributes.ExcelBind("staffCompany")]
        public string staffCompany
        {
            get { return _staffCompany; }
            set { _staffCompany = value; }
        }

        private string _staffDept;

        [ESBind("staffDept", "")]
        [ESNfx.Attributes.ExcelBind("staffDept")]
        public string staffDept
        {
            get { return _staffDept; }
            set { _staffDept = value; }
        }

        private string _staffTeam;

        [ESBind("staffTeam", "")]
        [ESNfx.Attributes.ExcelBind("staffTeam")]
        public string staffTeam
        {
            get { return _staffTeam; }
            set { _staffTeam = value; }
        }

        private string _staffFullName;

        [ESBind("staffFullName", "")]
        [ESNfx.Attributes.ExcelBind("staffFullName")]
        public string staffFullName
        {
            get { return _staffFullName; }
            set { _staffFullName = value; }
        }

        private short? _staffGubun;

        [ESBind("staffGubun", "")]
        [ESNfx.Attributes.ExcelBind("staffGubun")]
        public short? staffGubun
        {
            get { return _staffGubun; }
            set { _staffGubun = value; }
        }

        #endregion
    }

    public class SCH_sd_SCHEDULE_MAIN
    {
        #region 생성자
        public SCH_sd_SCHEDULE_MAIN()
        {
            this._TOTAL_COUNT = null;
            this._NO = null;
            this._SCH_ID = null;
            this._GST_CPY = null;
            this._GST_PST = null;
            this._GST_NAME = null;
            this._SCH_YEARMD = null;
            this._SCH_HOUR = null;
            this._SCH_MIN = null;
            this._SCH_TYPE = null;
            this._SCH_STATUS = null;
            this._SCH_VALIDITY = null;
            this._STAFF_NAME = null;
        }
        #endregion

        #region 프로퍼티

        private int? _TOTAL_COUNT;

        [ESBind("TOTAL_COUNT", "")]
        [ESNfx.Attributes.ExcelBind("TOTAL_COUNT")]
        public int? TOTAL_COUNT
        {
            get { return _TOTAL_COUNT; }
            set { _TOTAL_COUNT = value; }
        }

        private long? _NO;

        [ESBind("NO", "")]
        [ESNfx.Attributes.ExcelBind("NO")]
        public long? NO
        {
            get { return _NO; }
            set { _NO = value; }
        }

        private int? _SCH_ID;

        [ESBind("SCH_ID", "")]
        [ESNfx.Attributes.ExcelBind("SCH_ID")]
        public int? SCH_ID
        {
            get { return _SCH_ID; }
            set { _SCH_ID = value; }
        }

        private string _GST_CPY;

        [ESBind("GST_CPY", "")]
        [ESNfx.Attributes.ExcelBind("GST_CPY")]
        public string GST_CPY
        {
            get { return _GST_CPY; }
            set { _GST_CPY = value; }
        }

        private string _GST_PST;

        [ESBind("GST_PST", "")]
        [ESNfx.Attributes.ExcelBind("GST_PST")]
        public string GST_PST
        {
            get { return _GST_PST; }
            set { _GST_PST = value; }
        }

        private string _GST_NAME;

        [ESBind("GST_NAME", "")]
        [ESNfx.Attributes.ExcelBind("GST_NAME")]
        public string GST_NAME
        {
            get { return _GST_NAME; }
            set { _GST_NAME = value; }
        }

        private string _SCH_YEARMD;

        [ESBind("SCH_YEARMD", "")]
        [ESNfx.Attributes.ExcelBind("SCH_YEARMD")]
        public string SCH_YEARMD
        {
            get { return _SCH_YEARMD; }
            set { _SCH_YEARMD = value; }
        }

        private string _SCH_HOUR;

        [ESBind("SCH_HOUR", "")]
        [ESNfx.Attributes.ExcelBind("SCH_HOUR")]
        public string SCH_HOUR
        {
            get { return _SCH_HOUR; }
            set { _SCH_HOUR = value; }
        }

        private string _SCH_MIN;

        [ESBind("SCH_MIN", "")]
        [ESNfx.Attributes.ExcelBind("SCH_MIN")]
        public string SCH_MIN
        {
            get { return _SCH_MIN; }
            set { _SCH_MIN = value; }
        }

        private string _SCH_TYPE;

        [ESBind("SCH_TYPE", "")]
        [ESNfx.Attributes.ExcelBind("SCH_TYPE")]
        public string SCH_TYPE
        {
            get { return _SCH_TYPE; }
            set { _SCH_TYPE = value; }
        }

        private string _SCH_STATUS;

        [ESBind("SCH_STATUS", "")]
        [ESNfx.Attributes.ExcelBind("SCH_STATUS")]
        public string SCH_STATUS
        {
            get { return _SCH_STATUS; }
            set { _SCH_STATUS = value; }
        }

        private string _SCH_VALIDITY;

        [ESBind("SCH_VALIDITY", "")]
        [ESNfx.Attributes.ExcelBind("SCH_VALIDITY")]
        public string SCH_VALIDITY
        {
            get { return _SCH_VALIDITY; }
            set { _SCH_VALIDITY = value; }
        }

        private string _STAFF_NAME;

        [ESBind("STAFF_NAME", "")]
        [ESNfx.Attributes.ExcelBind("STAFF_NAME")]
        public string STAFF_NAME
        {
            get { return _STAFF_NAME; }
            set { _STAFF_NAME = value; }
        }

        #endregion
    }

    public class SCH_sd_SCHEDULE_LIST
    {
        #region 생성자
        public SCH_sd_SCHEDULE_LIST()
        {
            this._TOTAL_COUNT = null;
            this._NO = null;
            this._SCH_ID = null;
            this._GST_CPY = null;
            this._GST_PST = null;
            this._GST_NAME = null;
            this._SCH_YEARMD = null;
            this._SCH_HOUR = null;
            this._SCH_MIN = null;
            this._SCH_TYPE = null;
            this._SCH_STATUS = null;
            this._SCH_VALIDITY = null;
            this._STAFF_NAME = null;
        }
        #endregion

        #region 프로퍼티

        private int? _TOTAL_COUNT;

        [ESBind("TOTAL_COUNT", "")]
        [ESNfx.Attributes.ExcelBind("TOTAL_COUNT")]
        public int? TOTAL_COUNT
        {
            get { return _TOTAL_COUNT; }
            set { _TOTAL_COUNT = value; }
        }

        private long? _NO;

        [ESBind("NO", "")]
        [ESNfx.Attributes.ExcelBind("NO")]
        public long? NO
        {
            get { return _NO; }
            set { _NO = value; }
        }

        private int? _SCH_ID;

        [ESBind("SCH_ID", "")]
        [ESNfx.Attributes.ExcelBind("SCH_ID")]
        public int? SCH_ID
        {
            get { return _SCH_ID; }
            set { _SCH_ID = value; }
        }

        private string _GST_CPY;

        [ESBind("GST_CPY", "")]
        [ESNfx.Attributes.ExcelBind("GST_CPY")]
        public string GST_CPY
        {
            get { return _GST_CPY; }
            set { _GST_CPY = value; }
        }

        private string _GST_PST;

        [ESBind("GST_PST", "")]
        [ESNfx.Attributes.ExcelBind("GST_PST")]
        public string GST_PST
        {
            get { return _GST_PST; }
            set { _GST_PST = value; }
        }

        private string _GST_NAME;

        [ESBind("GST_NAME", "")]
        [ESNfx.Attributes.ExcelBind("GST_NAME")]
        public string GST_NAME
        {
            get { return _GST_NAME; }
            set { _GST_NAME = value; }
        }

        private string _SCH_YEARMD;

        [ESBind("SCH_YEARMD", "")]
        [ESNfx.Attributes.ExcelBind("SCH_YEARMD")]
        public string SCH_YEARMD
        {
            get { return _SCH_YEARMD; }
            set { _SCH_YEARMD = value; }
        }

        private string _SCH_HOUR;

        [ESBind("SCH_HOUR", "")]
        [ESNfx.Attributes.ExcelBind("SCH_HOUR")]
        public string SCH_HOUR
        {
            get { return _SCH_HOUR; }
            set { _SCH_HOUR = value; }
        }

        private string _SCH_MIN;

        [ESBind("SCH_MIN", "")]
        [ESNfx.Attributes.ExcelBind("SCH_MIN")]
        public string SCH_MIN
        {
            get { return _SCH_MIN; }
            set { _SCH_MIN = value; }
        }

        private string _SCH_TYPE;

        [ESBind("SCH_TYPE", "")]
        [ESNfx.Attributes.ExcelBind("SCH_TYPE")]
        public string SCH_TYPE
        {
            get { return _SCH_TYPE; }
            set { _SCH_TYPE = value; }
        }

        private string _SCH_STATUS;

        [ESBind("SCH_STATUS", "")]
        [ESNfx.Attributes.ExcelBind("SCH_STATUS")]
        public string SCH_STATUS
        {
            get { return _SCH_STATUS; }
            set { _SCH_STATUS = value; }
        }

        private string _SCH_VALIDITY;

        [ESBind("SCH_VALIDITY", "")]
        [ESNfx.Attributes.ExcelBind("SCH_VALIDITY")]
        public string SCH_VALIDITY
        {
            get { return _SCH_VALIDITY; }
            set { _SCH_VALIDITY = value; }
        }

        private string _STAFF_NAME;

        [ESBind("STAFF_NAME", "")]
        [ESNfx.Attributes.ExcelBind("STAFF_NAME")]
        public string STAFF_NAME
        {
            get { return _STAFF_NAME; }
            set { _STAFF_NAME = value; }
        }

        #endregion
    }

    public class SCH_sd_CALENDAR
    {
        #region 생성자
        public SCH_sd_CALENDAR()
        {
            this._SCH_ID = null;
            this._SCH_TYPE = null;
            this._SCH_DAY = null;
            this._SCH_HOUR = null;
            this._SCH_MIN = null;
            this._GST_CPY = null;
            this._GST_PST = null;
            this._GST_NAME = null;
        }
        #endregion

        #region 프로퍼티

        private int? _SCH_ID;

        [ESBind("SCH_ID", "")]
        [ESNfx.Attributes.ExcelBind("SCH_ID")]
        public int? SCH_ID
        {
            get { return _SCH_ID; }
            set { _SCH_ID = value; }
        }

        private string _SCH_TYPE;

        [ESBind("SCH_TYPE", "")]
        [ESNfx.Attributes.ExcelBind("SCH_TYPE")]
        public string SCH_TYPE
        {
            get { return _SCH_TYPE; }
            set { _SCH_TYPE = value; }
        }

        private string _SCH_DAY;

        [ESBind("SCH_DAY", "")]
        [ESNfx.Attributes.ExcelBind("SCH_DAY")]
        public string SCH_DAY
        {
            get { return _SCH_DAY; }
            set { _SCH_DAY = value; }
        }

        private string _SCH_HOUR;

        [ESBind("SCH_HOUR", "")]
        [ESNfx.Attributes.ExcelBind("SCH_HOUR")]
        public string SCH_HOUR
        {
            get { return _SCH_HOUR; }
            set { _SCH_HOUR = value; }
        }

        private string _SCH_MIN;

        [ESBind("SCH_MIN", "")]
        [ESNfx.Attributes.ExcelBind("SCH_MIN")]
        public string SCH_MIN
        {
            get { return _SCH_MIN; }
            set { _SCH_MIN = value; }
        }

        private string _GST_CPY;

        [ESBind("GST_CPY", "")]
        [ESNfx.Attributes.ExcelBind("GST_CPY")]
        public string GST_CPY
        {
            get { return _GST_CPY; }
            set { _GST_CPY = value; }
        }

        private string _GST_PST;

        [ESBind("GST_PST", "")]
        [ESNfx.Attributes.ExcelBind("GST_PST")]
        public string GST_PST
        {
            get { return _GST_PST; }
            set { _GST_PST = value; }
        }

        private string _GST_NAME;

        [ESBind("GST_NAME", "")]
        [ESNfx.Attributes.ExcelBind("GST_NAME")]
        public string GST_NAME
        {
            get { return _GST_NAME; }
            set { _GST_NAME = value; }
        }

        #endregion
    }


    public class iSCH_un_SCHEDULE
    {
        #region 생성자
        public iSCH_un_SCHEDULE()
        {
            this._SCH_ID = null;
            this._EDIT_MODE = null;
            this._GST_CPY = null;
            this._GST_PST = null;
            this._GST_NAME = null;
            this._GST_MOBILE_NO = null;
            this._SCH_YEARMD = null;
            this._SCH_HOUR = null;
            this._SCH_MIN = null;
            this._SCH_TYPE = null;
            this._SCH_MONITER = null;
        }
        #endregion

        #region 프로퍼티
        private int? _SCH_ID;

        public int? SCH_ID
        {
            get { return _SCH_ID; }
            set { _SCH_ID = value; }
        }

        private string _EDIT_MODE;

        public string EDIT_MODE
        {
            get { return _EDIT_MODE; }
            set { _EDIT_MODE = value; }
        }

        private string _GST_CPY;

        public string GST_CPY
        {
            get { return _GST_CPY; }
            set { _GST_CPY = value; }
        }

        private string _GST_PST;

        public string GST_PST
        {
            get { return _GST_PST; }
            set { _GST_PST = value; }
        }

        private string _GST_NAME;

        public string GST_NAME
        {
            get { return _GST_NAME; }
            set { _GST_NAME = value; }
        }

        private string _GST_MOBILE_NO;

        public string GST_MOBILE_NO
        {
            get { return _GST_MOBILE_NO; }
            set { _GST_MOBILE_NO = value; }
        }

        private string _SCH_YEARMD;

        public string SCH_YEARMD
        {
            get { return _SCH_YEARMD; }
            set { _SCH_YEARMD = value; }
        }

        private string _SCH_HOUR;

        public string SCH_HOUR
        {
            get { return _SCH_HOUR; }
            set { _SCH_HOUR = value; }
        }

        private string _SCH_MIN;

        public string SCH_MIN
        {
            get { return _SCH_MIN; }
            set { _SCH_MIN = value; }
        }

        private string _SCH_TYPE;

        public string SCH_TYPE
        {
            get { return _SCH_TYPE; }
            set { _SCH_TYPE = value; }
        }

        private string _SCH_MONITER;

        public string SCH_MONITER
        {
            get { return _SCH_MONITER; }
            set { _SCH_MONITER = value; }
        }

        #endregion
    }

    public class iSCH_sd_SCHEDULE_MAIN
    {
        #region 생성자
        public iSCH_sd_SCHEDULE_MAIN()
        {
            this._SEARCH_YEAR = null;
            this._SEARCH_MONTH = null;
            this._CURRENTPAGEINDEX = null;
            this._PAGINGSIZE = null;
        }
        #endregion

        #region 프로퍼티
        private string _SEARCH_YEAR;

        public string SEARCH_YEAR
        {
            get { return _SEARCH_YEAR; }
            set { _SEARCH_YEAR = value; }
        }

        private string _SEARCH_MONTH;

        public string SEARCH_MONTH
        {
            get { return _SEARCH_MONTH; }
            set { _SEARCH_MONTH = value; }
        }

        private int? _CURRENTPAGEINDEX;

        public int? CURRENTPAGEINDEX
        {
            get { return _CURRENTPAGEINDEX; }
            set { _CURRENTPAGEINDEX = value; }
        }

        private int? _PAGINGSIZE;

        public int? PAGINGSIZE
        {
            get { return _PAGINGSIZE; }
            set { _PAGINGSIZE = value; }
        }

        #endregion
    }

    public class iSCH_sd_SCHEDULE_LIST
    {
        #region 생성자
        public iSCH_sd_SCHEDULE_LIST()
        {
            this._SEARCH_TYPE = null;
            this._SEARCH_TXT = null;
            this._SCH_TYPE = null;
            this._START_DATE = null;
            this._END_DATE = null;
            this._CURRENTPAGEINDEX = null;
            this._PAGINGSIZE = null;
        }
        #endregion

        #region 프로퍼티
        private string _SEARCH_TYPE;

        public string SEARCH_TYPE
        {
            get { return _SEARCH_TYPE; }
            set { _SEARCH_TYPE = value; }
        }

        private string _SEARCH_TXT;

        public string SEARCH_TXT
        {
            get { return _SEARCH_TXT; }
            set { _SEARCH_TXT = value; }
        }

        private string _SCH_TYPE;

        public string SCH_TYPE
        {
            get { return _SCH_TYPE; }
            set { _SCH_TYPE = value; }
        }

        private string _START_DATE;

        public string START_DATE
        {
            get { return _START_DATE; }
            set { _START_DATE = value; }
        }

        private string _END_DATE;

        public string END_DATE
        {
            get { return _END_DATE; }
            set { _END_DATE = value; }
        }

        private int? _CURRENTPAGEINDEX;

        public int? CURRENTPAGEINDEX
        {
            get { return _CURRENTPAGEINDEX; }
            set { _CURRENTPAGEINDEX = value; }
        }

        private int? _PAGINGSIZE;

        public int? PAGINGSIZE
        {
            get { return _PAGINGSIZE; }
            set { _PAGINGSIZE = value; }
        }

        #endregion
    }

    public class iSCH_iu_STAFF
    {
        #region 생성자
        public iSCH_iu_STAFF()
        {
            this._MEM_ID = null;
            this._SCH_ID = null;
            this._STF_GUBUN = null;
            this._IU_GUBUN = null;
            this._CRE_MEMID = null;
        }
        #endregion

        #region 프로퍼티
        private int? _MEM_ID;

        public int? MEM_ID
        {
            get { return _MEM_ID; }
            set { _MEM_ID = value; }
        }

        private int? _SCH_ID;

        public int? SCH_ID
        {
            get { return _SCH_ID; }
            set { _SCH_ID = value; }
        }

        private int? _STF_GUBUN;

        public int? STF_GUBUN
        {
            get { return _STF_GUBUN; }
            set { _STF_GUBUN = value; }
        }

        private string _IU_GUBUN;

        public string IU_GUBUN
        {
            get { return _IU_GUBUN; }
            set { _IU_GUBUN = value; }
        }

        private int? _CRE_MEMID;

        public int? CRE_MEMID
        {
            get { return _CRE_MEMID; }
            set { _CRE_MEMID = value; }
        }

        #endregion
    }

    public class iSCH_in_SCHEDULE
    {
        #region 생성자
        public iSCH_in_SCHEDULE()
        {
            this._GST_CPY = null;
            this._GST_PST = null;
            this._GST_NAME = null;
            this._GST_MOBILE_NO = null;
            this._SCH_YEARMD = null;
            this._SCH_HOUR = null;
            this._SCH_MIN = null;
            this._SCH_TYPE = null;
            this._SCH_MONITER = null;
            this._CRE_MEMID = null;
        }
        #endregion

        #region 프로퍼티
        private string _GST_CPY;

        public string GST_CPY
        {
            get { return _GST_CPY; }
            set { _GST_CPY = value; }
        }

        private string _GST_PST;

        public string GST_PST
        {
            get { return _GST_PST; }
            set { _GST_PST = value; }
        }

        private string _GST_NAME;

        public string GST_NAME
        {
            get { return _GST_NAME; }
            set { _GST_NAME = value; }
        }

        private string _GST_MOBILE_NO;

        public string GST_MOBILE_NO
        {
            get { return _GST_MOBILE_NO; }
            set { _GST_MOBILE_NO = value; }
        }

        private string _SCH_YEARMD;

        public string SCH_YEARMD
        {
            get { return _SCH_YEARMD; }
            set { _SCH_YEARMD = value; }
        }

        private string _SCH_HOUR;

        public string SCH_HOUR
        {
            get { return _SCH_HOUR; }
            set { _SCH_HOUR = value; }
        }

        private string _SCH_MIN;

        public string SCH_MIN
        {
            get { return _SCH_MIN; }
            set { _SCH_MIN = value; }
        }

        private string _SCH_TYPE;

        public string SCH_TYPE
        {
            get { return _SCH_TYPE; }
            set { _SCH_TYPE = value; }
        }

        private string _SCH_MONITER;

        public string SCH_MONITER
        {
            get { return _SCH_MONITER; }
            set { _SCH_MONITER = value; }
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
