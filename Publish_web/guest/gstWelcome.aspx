<%@ Page Title="" Language="C#" MasterPageFile="~/master/Base.Master" AutoEventWireup="true" CodeBehind="gstWelcome.aspx.cs" Inherits="WEB.guest.gstWelcome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <!-- accessibility -->
    <div class="cm-accessibility"><a href="#Wrap">본문바로가기</a> </div>
    <!-- //accessibility -->

    <!-- code -->
    <section id="login-contentWrap">
        <article class="area">
            <h1><strong>ES</strong> Welcome</h1>
            <h2>ES그룹 방문예약 서비스</h2>
            <div class="loginBox">
                <p class="vst-tiem-area"><strong>예약된 방문 시간 :  </strong>
                    <asp:Literal ID="schMDHM" runat="server"></asp:Literal></p>

                <p class="vst-welcome-msg">
                    <span id="GST_NAME" runat="server"></span>님, 환영합니다.<br />
                    <strong>2층</strong>에서 대기부탁드립니다.
                </p>

                <div class="btns-wrap mt40">
                    <a href="guest_CheckIn.aspx" class="btns secondary-btn btn-big">확인</a>
                </div>
            </div>
        </article>
    </section>
    <!-- //code -->
</asp:Content>
