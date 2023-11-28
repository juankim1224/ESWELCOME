<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucSMS.ascx.cs" Inherits="WEB.controls.ucSMS" %>
<script type="text/javascript">

    function fnInit_<%=PopID %>(d) {

        // 문자 미리보기
        if (d.schId == null) {
            alert('여기까지 왔니?');

            $("#<%=hddGstCpy.ClientID%>").val(d.gstCpy);
            $("#<%=hddGstPst.ClientID%>").val(d.gstPst);
            $("#<%=hddGstName.ClientID%>").val(d.gstName);
            $("#<%=hddSchType.ClientID%>").val(d.schType);
            $("#<%=hddSchYearMD.ClientID%>").val(d.schYearMD);
            $("#<%=hddSchHourMin.ClientID%>").val(d.schHourMin);

            __doPostBack('<%=lnkDummy2.UniqueID%>', '');

            $('#resendMSG').hide();
        }
        else {
            $("#<%=hddSCH_ID.ClientID%>").val(d.schId);
            __doPostBack('<%=lnkDummy.UniqueID%>', '');
        }

    }

    // 문자 재발송
    function fnReSend() {

        var schId = $('#<%= hddSCH_ID.ClientID %>').val();
        var msgStaff = $('#<%= msgStaff.ClientID %>').text();

        PageMethods.ResendSMS(schId, msgStaff, function (result) {
            alert(result);
        });

    }
</script>

<asp:UpdatePanel ID="upnlList" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="popup-body" id="popup-body">
            <h3 class="popup-title">문자 메세지</h3>
            <div class="body-content">
                <asp:UpdatePanel ID="upnList" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="popup-sms-wrap">
                            <p class="popup-sms-title">[ ES타워 방문안내 ]</p>
                            <div>
                                <p>
                                    ❝ <span id="gstCpy" runat="server"></span><span id="gstName" runat="server"></span>&nbsp;<span id="gstPst" runat="server"></span>님 ❞
                                    <br />
                                    <br />
                                    안녕하세요.
                                    <br />
                                    <span id="schType" runat="server"></span>&nbsp;일정 안내 드립니다.
                                </p>
                                <p>
                                    날짜 : <span id="schYearMD" runat="server"></span>
                                    <br />
                                    시간 : <span id="schHourMin" runat="server"></span>
                                    <br />
                                    담당자 번호 : <span id="msgStaff" runat="server"></span>
                                </p>
                                <p>
                                    도착 시 로비에서 접수 부탁드립니다.<br />
                                    감사합니다.
                                </p>
                                <p>
                                    ▶ 오시는 길
                                    <br />
                                    <a href="https://kko.to/E47ZooGW3i">https://kko.to/E47ZooGW3i</a>
                                    <br />
                                    서울 마포구 월드컵북로58길 9 ES타워
                                </p>
                                <div id="foot" style="display: flex; justify-content: center">
                                    <a href="javascript:fnReSend();" class="btns primary-btn" id="resendMSG"><strong>재전송</strong></a>
                                    <a href="javascript:es.pop.disposeLayer({self:$('#<%=PopID %>')});" class="btns secondary-btn  close"><strong>닫기</strong></a>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </ContentTemplate>

    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkDummy" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="lnkDummy2" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>

<asp:LinkButton ID="lnkDummy" runat="server" OnClick="lnkDummy_Click"></asp:LinkButton>
<asp:LinkButton ID="lnkDummy2" runat="server" OnClick="lnkDummy2_Click"></asp:LinkButton>

<input type="hidden" id="hddSCH_ID" runat="server" />

<input type="hidden" id="hddGstCpy" runat="server" />
<input type="hidden" id="hddGstPst" runat="server" />
<input type="hidden" id="hddGstName" runat="server" />
<input type="hidden" id="hddSchType" runat="server" />
<input type="hidden" id="hddSchYearMD" runat="server" />
<input type="hidden" id="hddSchHourMin" runat="server" />

<%--<input type="hidden" id="hddMsgStaff" runat="server" />--%>