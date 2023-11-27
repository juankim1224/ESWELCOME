<%@ Page Title="" Language="C#" MasterPageFile="~/master/Base.Master" AutoEventWireup="true" CodeBehind="userLogin.aspx.cs" Inherits="WEB.auth.userLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <section id="login-contentWrap">
        <article class="area">
            <h1><strong>ES</strong> Welcome</h1>
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
                            <input type="text" placeholder="아이디를 입력해 주세요.">
                        </div>
                    </div>

                    <div>
                        <p class="vst-title">
                            PASSWORD
                        </p>
                        <div>
                            <input type="password" placeholder="비밀번호를 입력해 주세요.">
                        </div>
                    </div>

                    <div class="mt10">
                        <input type="checkbox">
                        <label>아이디 저장</label>
                    </div>
                </div>
                <div class="btns-wrap mt40">
                    <a href="#" class="btns secondary-btn btn-big">로그인</a>
                </div>
            </div>
        </article>
    </section>
</asp:Content>
