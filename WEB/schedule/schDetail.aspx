<%@ Page Title="" Language="C#" MasterPageFile="~/master/user.master" AutoEventWireup="true" CodeBehind="schDetail.aspx.cs" Inherits="WEB.schedule.schDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            var schVisit = $("input[id$='hdd_SchStatus']").val();

            if (schVisit === '완료') {
                $('#editBtn').css('visibility', 'hidden');
            }
        });

        function fnEditSch() {
            var schId = $("input[id$='hdd_SchId']").val();
            window.location.href = 'register.aspx?schId=' + schId;
        }

        function fnDeleteCheck() {
            confirm('해당 스케줄을 삭제하시겠습니까?');
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <section id="contentWrap">
        <article class="area mainWrap">
            <div class="mainTitle">
                <h3>상세 조회</h3>
            </div>
            <!-- 게시판 내용 -->
            <div class="mainBoardWrap">
                <h4 class="vst-title-h4">방문객 정보</h4>
                <table class="tableB">
                    <tbody>
                        <tr>
                            <th>회사명</th>
                            <td>
                                <asp:Literal ID="GST_CPY" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>직책</th>
                            <td>
                                <asp:Literal ID="GST_PST" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th>성함</th>
                            <td>
                                <asp:Literal ID="GST_NAME" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>연락처</th>
                            <td>
                                <asp:Literal ID="GST_HP" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>방문 예정일</th>
                            <td>
                                <asp:Literal ID="SCH_YEARMD" runat="server"></asp:Literal>
                                <asp:Literal ID="SCH_HOUR" runat="server"></asp:Literal>
                                <asp:Literal ID="SCH_MIN" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>방문목적</th>
                            <td>
                                <asp:Literal ID="SCH_TYPE" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>모니터 실행</th>
                            <td>
                                <asp:Literal ID="SCH_MONITER" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <h4 class="vst-title-h4 ">접견인 정보 </h4>
                <table class="tableB">
                    <tbody>
                        <tr>
                            <th>회사</th>
                            <th>부서</th>
                            <th>팀</th>
                            <th>성함</th>
                        </tr>
                        <asp:Repeater ID="rptList" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td class="tdClass"><%# Eval("COMPANY") %></td>
                                    <td class="tdClass"><%# Eval("DEPT") %></td>
                                    <td class="tdClass"><%# Eval("TEAM") %></td>
                                    <td class="tdClass"><%# Eval("MEM_FULLNAME") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <h4 class="vst-title-h4 ">문자 발송 </h4>
                <table class="tableB">
                    <tbody>
                        <tr>
                            <th>담당자 번호</th>
                            <td>010-1234-5678</td>
                        </tr>
                        <tr>
                            <th>발송 예정일</th>
                            <td>
                                <asp:Literal ID="MSG_STATUS" runat="server"></asp:Literal>
                                <asp:Literal ID="MSG_GUBUN" runat="server"></asp:Literal>
                                <asp:Literal ID="MSG_YEARMD" runat="server"></asp:Literal>
                                <asp:Literal ID="MSG_HOUR" runat="server"></asp:Literal>
                                <asp:Literal ID="MSG_MIN" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>2차 발송</th>
                            <td>
                                <asp:Literal ID="TND_CHECK" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="btns-wrap">
                    <a id="editBtn" href="javascript:fnEditSch();" class="btns primary-btn">수정</a>
                    <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click" OnClientClick="fnDeleteCheck();" class="btns border-btn">삭제</asp:LinkButton>
                    <a href="/schedule/schList.aspx" class="btns secondary-btn ">목록</a>
                </div>
            </div>
            <!-- //게시판 내용 -->
        </article>
    </section>

    <asp:HiddenField ID="hdd_SchId" runat="server" />
    <asp:HiddenField ID="hdd_SchStatus" runat="server" />

</asp:Content>
