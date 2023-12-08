<%@ Page Title="" Language="C#" MasterPageFile="~/master/Base.Master" AutoEventWireup="true" CodeBehind="userLogin.aspx.cs" Inherits="WEB.auth.userLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

        function fnCheckLogin() {
            var msg = es.valid.isRequired($('div.loginBox'));

            if (msg != '') {
                alert('다음은 필수사항입니다.\n' + msg);
                return false;
            }
            else {
                __doPostBack('<%=lnkLogin.UniqueID%>', '');
            }
        }


    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

        <asp:UpdatePanel ID="upnlLogin" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkLogin" EventName="Click" />
    </Triggers>
    <ContentTemplate>


    <section id="login-contentWrap">
        <article class="area">
            <h1><strong>ES</strong>WELCOME</h1>
            <h2>ES그룹 방문예약 서비스</h2>
            <div class="loginBox">
                <h3>ES그룹 방문을 환영합니다.</h3>
                <h4>아이디와 비밀번호를 입력해 주세요.</h4>
                <div class="vstCheckInInner">
                    <div>
                        <p class="vst-title">
                            ID
                        </p>
                        <div>
                            <input name="txtLoginID"  id="txtLoginID"  placeholder="아이디를 입력해 주세요.">
                        </div>
                    </div>

                    <div>
                        <p class="vst-title">
                            PASSWORD
                        </p>
                        <div>
                            <input name="txtLoginPwd"  id="txtLoginPwd"   placeholder="비밀번호를 입력해 주세요.">
                        </div>
                    </div>

                    <div class="mt10">
                        <input type="checkbox" name="saveloginid"  id="saveloginid" value="save">
                        <label>아이디 저장</label>
                    </div>
                </div>
                <div class="btns-wrap mt40">
                    <a href="javascript:fnCheckLogin();" class="btns secondary-btn btn-big">로그인</a>
                </div>
            </div>
        </article>
    </section>

    <asp:LinkButton ID="lnkLogin" runat="server" OnClick="lnkLogin_Click"></asp:LinkButton>

    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>