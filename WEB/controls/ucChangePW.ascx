<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucChangePW.ascx.cs" Inherits="WEB.controls.ucChangePW" %>
<script type="text/javascript">

<%--    function fnInit_<%=PopID%>() {

        __doPostBack('<%= lnkDummy.UniqueID%>', '');
    }--%>

</script>

<asp:UpdatePanel ID="upnlList" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="popup-wrap" id="upload-popup" style="display: flex;">
            <!-- display:flex 날리셔야합니다. 보기용 -->
            <div class="popup small">
                <div class="popup-body">
                    <h3 class="popup-title">비밀번호 변경</h3>
                    <div class="body-content">
                        <div class="pw_modify">
                            <div>
                                <p class="label">현재 비밀번호</p>
                                <input id="BEFORE_PW" runat="server" type="password" placeholder="현재 비밀번호를 입력하세요." />
                            </div>
                            <div>
                                <p class="label">변경 비밀번호</p>
                                <input id="AFTER_PW" runat="server" type="password" placeholder="변경 비밀번호를 입력하세요." />
                            </div>
                            <div>
                                <p class="label">비밀번호 확인</p>
                                <input id="AFTER_PWD_CHECK" type="password" />
                            </div>
                        </div>
                        <div class="btns-wrap">
                            <asp:LinkButton ID="lnkChangePwd" OnClick="lnkChangePwd_Click" runat="server" class="btns primary-btn">수정</asp:LinkButton>
                            <a href="javascript:es.pop.disposeLayer({self:$('#<%=PopID %>')});" class="btns secondary-btn  clos"><strong>닫기</strong></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>
<asp:LinkButton ID="lnkDummy" runat="server" OnClick="lnkDummy_Click"></asp:LinkButton>