using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using ESNfx.DataBase;

namespace ESWELCOME.DataBase.Procedure.BOL.ES
{
    public class ES_sr_MemLoginUser
    {
        #region 생성자
        public ES_sr_MemLoginUser()
        {
            this._MEM_ID = null;
            this._MEM_LOGIN_ID = null;
            this._CPY_NAME = null;
            this._DEPT_NAME = null;
            this._TEAM_NAME = null;
            this._MEM_NAME = null;
            this._MEM_EMAIL = null;
            this._MEM_PST = null;
            this._MEM_MOBILE_NO = null;
            this._MEM_DIRECT_NO = null;
            this._MEM_TYPE = null;
        }
        #endregion

        #region 프로퍼티

        private int? _MEM_ID;

        [ESBind("MEM_ID", "")]
        [ESNfx.Attributes.ExcelBind("MEM_ID")]
        public int? MEM_ID
        {
            get { return _MEM_ID; }
            set { _MEM_ID = value; }
        }

        private string _MEM_LOGIN_ID;

        [ESBind("MEM_LOGIN_ID", "")]
        [ESNfx.Attributes.ExcelBind("MEM_LOGIN_ID")]
        public string MEM_LOGIN_ID
        {
            get { return _MEM_LOGIN_ID; }
            set { _MEM_LOGIN_ID = value; }
        }

        private string _CPY_NAME;

        [ESBind("CPY_NAME", "")]
        [ESNfx.Attributes.ExcelBind("CPY_NAME")]
        public string CPY_NAME
        {
            get { return _CPY_NAME; }
            set { _CPY_NAME = value; }
        }

        private string _DEPT_NAME;

        [ESBind("DEPT_NAME", "")]
        [ESNfx.Attributes.ExcelBind("DEPT_NAME")]
        public string DEPT_NAME
        {
            get { return _DEPT_NAME; }
            set { _DEPT_NAME = value; }
        }

        private string _TEAM_NAME;

        [ESBind("TEAM_NAME", "")]
        [ESNfx.Attributes.ExcelBind("TEAM_NAME")]
        public string TEAM_NAME
        {
            get { return _TEAM_NAME; }
            set { _TEAM_NAME = value; }
        }

        private string _MEM_NAME;

        [ESBind("MEM_NAME", "")]
        [ESNfx.Attributes.ExcelBind("MEM_NAME")]
        public string MEM_NAME
        {
            get { return _MEM_NAME; }
            set { _MEM_NAME = value; }
        }

        private string _MEM_EMAIL;

        [ESBind("MEM_EMAIL", "")]
        [ESNfx.Attributes.ExcelBind("MEM_EMAIL")]
        public string MEM_EMAIL
        {
            get { return _MEM_EMAIL; }
            set { _MEM_EMAIL = value; }
        }

        private string _MEM_PST;

        [ESBind("MEM_PST", "")]
        [ESNfx.Attributes.ExcelBind("MEM_PST")]
        public string MEM_PST
        {
            get { return _MEM_PST; }
            set { _MEM_PST = value; }
        }

        private string _MEM_MOBILE_NO;

        [ESBind("MEM_MOBILE_NO", "")]
        [ESNfx.Attributes.ExcelBind("MEM_MOBILE_NO")]
        public string MEM_MOBILE_NO
        {
            get { return _MEM_MOBILE_NO; }
            set { _MEM_MOBILE_NO = value; }
        }

        private string _MEM_DIRECT_NO;

        [ESBind("MEM_DIRECT_NO", "")]
        [ESNfx.Attributes.ExcelBind("MEM_DIRECT_NO")]
        public string MEM_DIRECT_NO
        {
            get { return _MEM_DIRECT_NO; }
            set { _MEM_DIRECT_NO = value; }
        }

        private short? _MEM_TYPE;

        [ESBind("MEM_TYPE", "")]
        [ESNfx.Attributes.ExcelBind("MEM_TYPE")]
        public short? MEM_TYPE
        {
            get { return _MEM_TYPE; }
            set { _MEM_TYPE = value; }
        }

        #endregion
    }

    public class ES_sr_MEMBER
    {
        #region 생성자
        public ES_sr_MEMBER()
        {
            this._MEM_ID = null;
            this._MEM_LOGIN_ID = null;
            this._CPY_NAME = null;
            this._DEPT_NAME = null;
            this._TEAM_NAME = null;
            this._MEM_NAME = null;
            this._MEM_EMAIL = null;
            this._MEM_PST = null;
            this._MEM_MOBILE_NO = null;
            this._MEM_DIRECT_NO = null;
            this._MEM_TYPE = null;
        }
        #endregion

        #region 프로퍼티

        private int? _MEM_ID;

        [ESBind("MEM_ID", "")]
        [ESNfx.Attributes.ExcelBind("MEM_ID")]
        public int? MEM_ID
        {
            get { return _MEM_ID; }
            set { _MEM_ID = value; }
        }

        private string _MEM_LOGIN_ID;

        [ESBind("MEM_LOGIN_ID", "")]
        [ESNfx.Attributes.ExcelBind("MEM_LOGIN_ID")]
        public string MEM_LOGIN_ID
        {
            get { return _MEM_LOGIN_ID; }
            set { _MEM_LOGIN_ID = value; }
        }

        private string _CPY_NAME;

        [ESBind("CPY_NAME", "")]
        [ESNfx.Attributes.ExcelBind("CPY_NAME")]
        public string CPY_NAME
        {
            get { return _CPY_NAME; }
            set { _CPY_NAME = value; }
        }

        private string _DEPT_NAME;

        [ESBind("DEPT_NAME", "")]
        [ESNfx.Attributes.ExcelBind("DEPT_NAME")]
        public string DEPT_NAME
        {
            get { return _DEPT_NAME; }
            set { _DEPT_NAME = value; }
        }

        private string _TEAM_NAME;

        [ESBind("TEAM_NAME", "")]
        [ESNfx.Attributes.ExcelBind("TEAM_NAME")]
        public string TEAM_NAME
        {
            get { return _TEAM_NAME; }
            set { _TEAM_NAME = value; }
        }

        private string _MEM_NAME;

        [ESBind("MEM_NAME", "")]
        [ESNfx.Attributes.ExcelBind("MEM_NAME")]
        public string MEM_NAME
        {
            get { return _MEM_NAME; }
            set { _MEM_NAME = value; }
        }

        private string _MEM_EMAIL;

        [ESBind("MEM_EMAIL", "")]
        [ESNfx.Attributes.ExcelBind("MEM_EMAIL")]
        public string MEM_EMAIL
        {
            get { return _MEM_EMAIL; }
            set { _MEM_EMAIL = value; }
        }

        private string _MEM_PST;

        [ESBind("MEM_PST", "")]
        [ESNfx.Attributes.ExcelBind("MEM_PST")]
        public string MEM_PST
        {
            get { return _MEM_PST; }
            set { _MEM_PST = value; }
        }

        private string _MEM_MOBILE_NO;

        [ESBind("MEM_MOBILE_NO", "")]
        [ESNfx.Attributes.ExcelBind("MEM_MOBILE_NO")]
        public string MEM_MOBILE_NO
        {
            get { return _MEM_MOBILE_NO; }
            set { _MEM_MOBILE_NO = value; }
        }

        private string _MEM_DIRECT_NO;

        [ESBind("MEM_DIRECT_NO", "")]
        [ESNfx.Attributes.ExcelBind("MEM_DIRECT_NO")]
        public string MEM_DIRECT_NO
        {
            get { return _MEM_DIRECT_NO; }
            set { _MEM_DIRECT_NO = value; }
        }

        private short? _MEM_TYPE;

        [ESBind("MEM_TYPE", "")]
        [ESNfx.Attributes.ExcelBind("MEM_TYPE")]
        public short? MEM_TYPE
        {
            get { return _MEM_TYPE; }
            set { _MEM_TYPE = value; }
        }

        #endregion
    }


    public class iES_un_LOGIN_PW
    {
        #region 생성자
        public iES_un_LOGIN_PW()
        {
            this._MEM_TYPE = null;
            this._MEM_ID = null;
            this._BEFORE_PW = null;
            this._AFTER_PW = null;
        }
        #endregion

        #region 프로퍼티
        private short? _MEM_TYPE;

        public short? MEM_TYPE
        {
            get { return _MEM_TYPE; }
            set { _MEM_TYPE = value; }
        }

        private int? _MEM_ID;

        public int? MEM_ID
        {
            get { return _MEM_ID; }
            set { _MEM_ID = value; }
        }

        private string _BEFORE_PW;

        public string BEFORE_PW
        {
            get { return _BEFORE_PW; }
            set { _BEFORE_PW = value; }
        }

        private string _AFTER_PW;

        public string AFTER_PW
        {
            get { return _AFTER_PW; }
            set { _AFTER_PW = value; }
        }

        #endregion
    }


}
