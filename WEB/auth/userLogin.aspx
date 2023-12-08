<%@ Page Title="" Language="C#" MasterPageFile="~/master/Base.Master" AutoEventWireup="true" CodeBehind="userLogin.aspx.cs" Inherits="WEB.auth.userLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(function () {
            var init = function () {

                $('#txtPassword').keyup(function (e) {
                    if (e.keyCode == 13) {
                        __doPostBack('<%=lnkLogin.UniqueID %>', '');
                    }
                });

                //-- 로그인아이디
                var ckLoginID = es.cookie.get("ckSaveID");
                if (ckLoginID != '') {
                    $('input[name="txtLoginID"]').val(ckLoginID);
                    $('input[name="saveloginid"]').attr('checked', true);
                }
                $('#txtLoginID').focus();
            };

            init();

            __globalFuc.add(init);
        });

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
                            <input name="txtLoginID" type="text" placeholder="아이디를 입력해 주세요.">
                        </div>
                    </div>

                    <div>
                        <p class="vst-title">
                            PASSWORD
                       
                        </p>
                        <div>
                            <input type="password" name="txtPassword" placeholder="비밀번호를 입력해 주세요.">
                        </div>
                    </div>

                    <div class="mt10">
                        <input name="saveloginid" id="saveloginid" type="checkbox" value="save">
                        <label>아이디 저장</label>
                    </div>
                </div>
                <div class="btns-wrap mt40">
                    <asp:LinkButton ID="lnkLogin" CssClass="btns secondary-btn btn-big" runat="server" OnClick="lnkLogin_Click">
                    로그인
                        </asp:LinkButton>
                </div>
            </div>
        </article>
    </section>

    <asp:LinkButton ID="lnkLogin" runat="server" OnClick="lnkLogin_Click"></asp:LinkButton>

    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>