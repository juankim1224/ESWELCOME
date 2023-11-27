<%@ Page Title="" Language="C#" MasterPageFile="~/master/Base.Master" AutoEventWireup="true" CodeBehind="gstCheckIn.aspx.cs" Inherits="WEB.guest.gstCheckIn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <section id="login-contentWrap">
        <article class="area">
            <h1><strong>ES</strong> WELCOME</h1>
            <h2>ES그룹 방문예약 서비스</h2>
            <div class="loginBox">
                <h3>ES그룹 방문을 환영합니다.</h3>
                <h4>방문자 정보를 입력해 주세요.</h4>
                <div class="vstCheckInInner">
                    <div>
                        <p class="vst-title">
                            휴대폰 번호
                        </p>
                        <div class="vst-tel">
                            <input id="GST_HP1" runat="server" maxlength="3">
                            <input id="GST_HP2" runat="server" maxlength="4">
                            <input id="GST_HP3" runat="server" maxlength="4">
                        </div>
                    </div>

                    <div>
                        <p class="vst-title">
                            인증 번호
                        </p>
                        <div>
                            <input id="MSG_CODE" runat="server" maxlength="4" placeholder="인증번호 4자리를 입력해 주세요.">
                        </div>
                    </div>
                </div>
                <div class="btns-wrap mt40">
                    <asp:LinkButton ID="lnkCheckIn" OnClick="lnkCheckIn_Click" runat="server" CssClass="btns secondary-btn btn-big">등록</asp:LinkButton>
                </div>
            </div>
        </article>
    </section>

</asp:Content>
