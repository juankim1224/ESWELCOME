using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using ESNfx.DataBase;

namespace ESWELCOME.DataBase.Procedure.BOL.ADM
{
    public class ADM_sd_MEMBER
    {
        #region 생성자
        public ADM_sd_MEMBER()
        {
            this._TOTAL_COUNT = null;
            this._NO = null;
            this._MEM_ID = null;
            this._MEM_LOGIN_ID = null;
            this._CPY_NAME = null;
            this._DEPT_NAME = null;
            this._TEAM_NAME = null;
            this._MEM_NAME = null;
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

        private string _NO;

        [ESBind("NO", "")]
        [ESNfx.Attributes.ExcelBind("NO")]
        public string NO
        {
            get { return _NO; }
            set { _NO = value; }
        }

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

        #endregion
    }


    public class iADM_sd_MEMBER
    {
        #region 생성자
        public iADM_sd_MEMBER()
        {
            this._MEMBER_CPY = null;
            this._SEARCH_TYPE = null;
            this._SEARCH_TEXT = null;
            this._CURRENTPAGEINDEX = null;
            this._PAGINGSIZE = null;
        }
        #endregion

        #region 프로퍼티
        private string _MEMBER_CPY;

        public string MEMBER_CPY
        {
            get { return _MEMBER_CPY; }
            set { _MEMBER_CPY = value; }
        }

        private string _SEARCH_TYPE;

        public string SEARCH_TYPE
        {
            get { return _SEARCH_TYPE; }
            set { _SEARCH_TYPE = value; }
        }

        private string _SEARCH_TEXT;

        public string SEARCH_TEXT
        {
            get { return _SEARCH_TEXT; }
            set { _SEARCH_TEXT = value; }
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


}
