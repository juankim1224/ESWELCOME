using ESNfx3.Web.User;
using ESWELCOME.Core;
using ESWELCOME.Core.Procedure;
using ESWELCOME.DataBase.Procedure.BOL.ES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WEB.App_Code.auth
{
    public class LoginUser : SiteUser
    {
        public LoginUser() { }

        #region property

        /// <summary>
        /// 사용자 탈퇴여부
        /// </summary>
        public string PRS_Validity
        {
            get; set;
        }

        /// <summary>
        /// 회원사 구분
        /// </summary>
        public string CPY_TYPE
        {
            get; set;
        }

        /// <summary>
        /// 로그인 아이디
        /// </summary>
        public string PRS_LGN_ID { get; set; }

        private string _prs_pw;
        /// <summary>
        /// 로그인 비밀번호
        /// </summary>
        public string PRS_PW
        {
            get
            {
                return _prs_pw;
            }
            private set { _prs_pw = value; }
        }

        /// <summary>
        /// 비밀번호 원문
        /// </summary>
        public string OriginPRS_PW { get; set; }

        /// <summary>
        /// 로그인 사용자 PK(USRID)
        /// </summary>
        public long? PRS_ID
        {
            get; set;
        }

        /// <summary>
        /// 비밀번호설정
        /// </summary>
        /// <param name="pwd"></param>
        public void SetLoginPWD(string pwd)
        {
            _prs_pw = pwd;
        }


        #endregion
    }

    /// <summary>
    ///  ID/PW 로그인인증 처리 클래스
    /// </summary>
    public class LoginTicket : ISBILoginTicket
    {
        private LoginUser _loginUser;

        public LoginTicket() { }

        #region ILoginTicket 멤버

        /// <summary>
        /// 로그인계정 유효성 확인
        /// </summary>
        /// <returns></returns>
        public bool IsValidAccount()
        {
            _errorMessage = new StringBuilder();

            var ret = false;
            var loginpwd = this.LoginPWD;
            string sOriginPwd = this.LoginPWD;

            /*
            if (this.SHAPWD != true)
            {
                loginpwd = ESNfx.Encryption.EncryptToHashedText(ESNfx.Encryption.ENCTYPE.SHA512, this.LoginPWD);
            }
            */

            var entry = ProcManager.Proc.ESFacade.GetMemLoginUser(this.LoginID, loginpwd);

            if (entry.Exist)
            {
                var user = entry.GenericItem;

                ////          승인 사용중지 탈퇴요청
                //          승인 탈퇴요청
                if (user.MEM_ID  > 1)
                {

                    _loginUser = new LoginUser
                    {
                        PRS_LGN_ID = user.MEM_LOGIN_ID,
                        UserName = user.MEM_NAME,
                        IsLoginSuccess = true,
                        PRS_ID = user.MEM_ID
                    };

                    _loginUser.OriginPRS_PW = sOriginPwd;
                    _loginUser.SetLoginPWD(loginpwd);

                    ret = true;

                }
                else
                {

                    ret = false;
                }
            }
            else
            {
                _errorMessage.AppendLine("alert('아이디 또는 비밀번호가 잘못되었습니다.');");
            }


            return ret;
        }

        /// <summary>
        /// 인증된 사용자 반환
        /// </summary>
        /// <returns></returns>
        public SiteUser GetUser()
        {
            return _loginUser;
        }

        private bool _isUseCookieLogin = false;

        /// <summary>
        /// 쿠키로그인 사용 여부
        /// </summary>
        public bool IsUseCookieLogin
        {
            get
            {
                return _isUseCookieLogin;
            }
            set
            {
                _isUseCookieLogin = value;
            }
        }

        /// <summary>
        /// 로그인아이디
        /// </summary>
        public string LoginID
        {
            get;
            set;
        }

        /// <summary>
        /// 로그인 비밀번호
        /// </summary>
        public string LoginPWD
        {
            get;
            set;
        }

        /// <summary>
        /// 로그인 IP
        /// </summary>
        public string LoginIP
        {
            get;
            set;
        }

        /// <summary>
        /// 로그인 타입(A:관리자, M:사용자)
        /// </summary>
        public string LoginType
        {
            get;
            set;
        }
        /// <summary>
        /// SHAPWD == Y : SHA 암호화된 패스워드
        /// </summary>
        private bool _shapwd = false;
        public bool SHAPWD
        {
            get { return _shapwd; }
            set { _shapwd = value; }
        }

        /// <summary>
        /// 브라우저 UserAgent
        /// </summary>
        public string UserAgent { get; set; }

        #endregion

        private StringBuilder _errorMessage = new StringBuilder();

        public string ErrorMessage
        {
            get
            {
                return _errorMessage.ToString();
            }
        }

        #region ISBILoginTicket 멤버

        private string _PWDTempYN = "N";

        public string PWDTempYN
        {
            get { return _PWDTempYN; }
        }


        /// <summary>
        /// 사업자번호
        /// </summary>
        public string BusinessNO { get; set; }

        /// <summary>
        /// 메뉴접속권한
        /// </summary>
        public string AuthID { get; set; }

        #endregion
    }
}