using ESNfx3.Web.User;
using ESWELCOME.Core;
using ESWELCOME.Core.Procedure;
using ESWELCOME.DataBase.Procedure.BOL.ES;
using ESWELCOME.DataBase.Procedure.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


/// <summary>
/// 로그인 사용자 객체
/// </summary>
public class LoginUser : SiteUser
{
    public LoginUser() { }

    #region property

    /// <summary>
    /// 로그인 사용자 유형
    /// </summary>
    public SiteMemType UserType { get; set; }

    /// <summary>
    /// ** 로그인 사용자 타입 (관리자: 1, 임직원: 2, 방문객: 3 *)
    /// </summary>
    public int? MEM_TYPE { get; set; }

    /// <summary>
    /// 로그인 사용자 식별 PK
    /// </summary>
    public int? MEM_ID { get; set; }


    /// <summary>
    /// 로그인 사용자(직원) 회사명
    /// </summary>
    public string CPY_NAME { get; set; }

    /// <summary>
    /// 로그인 사용자(직원) 부서명
    /// </summary>
    public string DEPT_NAME { get; set; }

    /// <summary>
    /// 로그인 사용자(직원) 팀명
    /// </summary>
    public string TEAM_NAME { get; set; }


    /// <summary>
    /// 로그인 사용자명
    /// </summary>
    public string MEM_NAME { get; set; }


    /// <summary>
    /// 로그인 이메일
    /// </summary>
    public string MEM_EMAIL { get; set; }


    /// <summary>
    /// 로그인 사용자(직원) 직급
    /// </summary>
    public string MEM_PST { get; set; }


    /// <summary>
    /// 로그인 사용자(직원) 휴대번호
    /// </summary>
    public string MEM_MOBILE_NO { get; set; }


    /// <summary>
    /// 로그인 사용자(직원) 내선번호
    /// </summary>
    public string MEM_DIRECT_NO { get; set; }


    /// <summary>
    /// 로그인 아이디
    /// </summary>
    public string LoginID { get; set; }


    private string _loginPwd;
    /// <summary>
    /// 로그인 비밀번호
    /// </summary>
    public string LoginPWD
    {
        get
        {
            return _loginPwd;
        }
        private set { _loginPwd = value; }
    }


    /// <summary>
    /// 비밀번호 원문
    /// </summary>
    public string OriginLoginPWD { get; set; }


    /// <summary>
    /// ***
    /// 로그인 사용자 식별자
    /// </summary>
    public int? ADMID { get; set; }


    /// <summary>
    /// 로그인 사용자 PK(USRID)
    /// </summary>
    public int? USRID
    {
        get; set;
    }


    /// <summary>
    /// 로그인 부서코드
    /// </summary>
    public int? DEPT_CODE
    {
        get; set;
    }

    /// <summary>
    /// 메뉴접속권한
    /// </summary>
    public string AuthID { get; set; }


    /// <summary>
    /// 비밀번호설정
    /// </summary>
    /// <param name="pwd"></param>
    public void SetLoginPWD(string pwd)
    {
        _loginPwd = pwd;
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

        /// SHAPWD == Y일경우 : SHA 암호화된 패스워드로 별도 암호화 작업없이 CPY.GetLogin 호출함.
        if (this.SHAPWD != true)
        {
            loginpwd = ESNfx.Encryption.EncryptToHashedText(ESNfx.Encryption.ENCTYPE.SHA512, this.LoginPWD);
        }

        ES_sr_MemLoginUser param = new ES_sr_MemLoginUser();

        var result = ESFacade.GetInstance.GetMemLoginUser(LoginID, loginpwd);


        if(result.Exist)
        {
            var user = result.GenericItem;
            _loginUser = new LoginUser
            {
                LoginID = user.MEM_LOGIN_ID,
                MEM_NAME = user.MEM_NAME,
                MEM_EMAIL= user.MEM_EMAIL,
                MEM_TYPE = user.MEM_TYPE,
                CPY_NAME = user.CPY_NAME,
                DEPT_NAME = user.DEPT_NAME,
                TEAM_NAME = user.TEAM_NAME,
                MEM_MOBILE_NO = user.MEM_MOBILE_NO,
                MEM_DIRECT_NO = user.MEM_DIRECT_NO,
                MEM_PST = user.MEM_PST,
                DEPT_CODE = user.DEPT_CODE,
                IsLoginSuccess = true,
            };

            _loginUser.OriginLoginPWD = sOriginPwd;
            _loginUser.SetLoginPWD(loginpwd);

            ret = true;
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
    /// 메뉴접속권한
    /// </summary>
    public string AuthID { get; set; }

    #endregion
}
