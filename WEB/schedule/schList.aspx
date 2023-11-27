<%@ Page Title="" Language="C#" MasterPageFile="~/master/user.master" AutoEventWireup="true" CodeBehind="schList.aspx.cs" Inherits="WEB.schedule.schList" %>

<%@ Register Src="~/controls/ucSMS.ascx" TagName="ucSMS" TagPrefix="MSG" %>
<%@ Register Src="~/controls/ucFromToDate.ascx" TagName="FromToDate" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tdClass:hover {
            cursor: pointer;
        }

        .tr:hover {
            cursor: default;
        }
    </style>

    <script>
        $(function () {
            $('.tdClass').on('click', function () {
                var schId = $(this).siblings('#schId').text();
                window.location.href = 'schDetail.aspx?schId=' + schId;
            });
        });

        function previewSMS(schId) {

            $('#<%=hddSchId.ClientID %>').val(schId);

            fnOpen_ucSMS({ schId: schId });
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <section id="contentWrap">
        <article class="area mainWrap">
            <div class="mainTitle">
                <h3>일정 조회</h3>
                <div class="mainTitle-flex">
                    <select id="SCH_TYPE" runat="server">
                        <option value="" align="center">방문사유</option>
                        <option value="면접" align="center">면접</option>
                        <option value="미팅" align="center">미팅</option>
                        <option value="기타" align="center">기타</option>
                    </select>
                    <%-- 날짜 --%>
                    <div class="datepicker_1">
                        <uc:FromToDate ID="esFromToDate" runat="server" />
                    </div>
                    <select id="SEARCH_TYPE" runat="server">
                        <option value="A" align="center">전체</option>
                        <option value="C" align="center">회사명</option>
                        <option value="N" align="center">방문객명</option>
                    </select>
                    <input id="SEARCH_TXT" runat="server" />
                    <asp:LinkButton runat="server" ID="searchSCH" OnClick="searchSCH_Click">조회</asp:LinkButton>
                </div>
            </div>

            <!-- 게시판 내용 -->
            <asp:UpdatePanel ID="upnlCommand" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="mainBoardWrap">
                        <table class="tableA">
                            <colgroup>
                                <col style="width: 80px" />
                                <col style="width: 120px" />
                                <col style="width: 150px" />
                                <col style="width: 200px" />
                                <col style="width: 100px" />
                                <col style="width: 200px" />
                                <col style="width: 100px" />
                                <col style="width: 100px" />
                            </colgroup>
                            <thead>
                                <tr align="center">
                                    <th>NO</th>
                                    <th>방문객명</th>
                                    <th>소속</th>
                                    <th>방문일시</th>
                                    <th>방문목적</th>
                                    <th>접견인명</th>
                                    <th>문자발송</th>
                                    <th>방문상태</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptList" runat="server">
                                    <ItemTemplate>
                                        <tr class="tr">
                                            <%-- hidden --%>
                                            <td id="schId" style="display: none;"><%# Eval("SCH_ID") %></td>
                                            <td id="gstPst" style="display: none;"><%# Eval("GST_PST") %></td>
                                            <%-- //hidden --%>
                                            <td class="tdClass"><%# Eval("NO") %></td>
                                            <td class="tdClass"><%# Eval("GST_NAME") %></td>
                                            <td class="tdClass"><%# Eval("GST_CPY") %></td>
                                            <td><%# Eval("SCH_YEARMD") %>&nbsp;&nbsp;&nbsp;<%# Eval("SCH_HOUR") %>시&nbsp;<%# Eval("SCH_MIN") %>분</td>
                                            <td><%# Eval("SCH_TYPE") %></td>
                                            <td><%# Eval("STAFF_NAME") %></td>
                                            <td>
                                                <a href="javascript:void(0);" onclick="return fnInit_ucSMS({schId: '<%# Eval("SCH_ID") %>'});">
                                                    <img src="../common/images/icon_mail.png" alt="문자발송" /></a></td>
                                            <td><%# Eval("SCH_STATUS").Equals("완료")? "<span class='state_txt end'>완료</span>" : "<span class='state_txt expected'>예정</span>" %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                        <!--paging -->
                        <div class="paging">
                            <ESNfx:ESNDataPager ID="esnPager" CssClass="paging" runat="server" FirstButtonImageUrl="../common/images/board_btn_pprev.png"
                                LastButtonImageUrl="../common/images/board_btn_nnext.png" NextButtonImageUrl="../common/images/board_btn_next.png"
                                PrevButtonImageUrl="../common/images/board_btn_prev.png" alt="이전" OnCommand="esnPager1_Command" />
                        </div>
                        <!-- //paging-->
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- //게시판 내용 -->
        </article>
    </section>

    <asp:HiddenField ID="hddSchId" runat="server" />
    <MSG:ucSMS ID="ucSMS" runat="server" PopWidth="480" />

    <script type="text/javascript">
        // 달력 Css 수정
        $('#<%= esFromToDate.FromDateClientID %>').attr("style", "ime-mode:disabled;");
        $('#<%= esFromToDate.ToDateClientID %>').attr("style", "ime-mode:disabled;");
        $('#<%= esFromToDate.FromDateClientID %>').attr("placeholder", "YYYY-DD-MM");
        $('#<%= esFromToDate.ToDateClientID %>').attr("placeholder", "YYYY-DD-MM");
        $('#<%= esFromToDate.FromDateClientID %>').removeAttr("onblur");
        $('#<%= esFromToDate.ToDateClientID %>').removeAttr("onblur");
        $('#<%= esFromToDate.FromDateClientID %>').next().andSelf().wrapAll('<p></p>');
        $('#<%= esFromToDate.ToDateClientID %>').next().andSelf().wrapAll('<p></p>');
    </script>

</asp:Content>
