<%@ Page Title="" Language="C#" MasterPageFile="~/master/user.master" AutoEventWireup="true" CodeBehind="schMain.aspx.cs" Inherits="WEB.schedule.schMain" %>

<%@ Register Src="~/controls/ucSMS.ascx" TagName="ucSMS" TagPrefix="MSG" %>

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
            var init = function () {
                $('.tdClass').on('click', function () {
                    var schId = $(this).siblings('#schId').text();
                    window.location.href = 'schDetail.aspx?schId=' + schId;
                });
            };
            init();
            __globalFuc.add(init);

        });

        function previewSMS(schId) {

            $('#<%=hdschId.ClientID %>').val(schId);

            fnOpen_ucSMS({ schId: schId });
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <section id="contentWrap">
        <div class="hederSubmenu">
            <!-- nav -->
            <nav>
                <p><a href="schList.aspx" class="active">리스트</a></p>
                <p><a href="schCalendar.aspx">캘린더</a></p>
            </nav>
            <!-- nav -->
        </div>
        <article class="area mainWrap">
            <div class="mainTitle">
                <h3>리스트</h3>
                <div>
                    <select id="SEARCH_YEAR" runat="server" align="center">
                        <option align="center">2023</option>
                        <option align="center">2024</option>
                    </select>
                    <select id="SEARCH_MONTH" runat="server" align="center">
                    </select>
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
                                                <%--<a href="javascript:void(0);" onclick="return previewSMS('<%# Eval("SCH_ID") %>')">--%>
                                                <a href="javascript:void(0);" onclick="return fnInit_ucsms({schId: '<%# Eval("SCH_ID") %>'});">
                                                    <img src="../common/images/icon_mail.png" alt="문자발송" /></a></td>
                                            <td><%# Eval("SCH_STATUS").Equals("완료")? "<span class='state_txt end'>완료</span>" : "<span class='state_txt expected'>예정</span>" %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                        <!--paging -->
                        <div class="paging">
                            <ESNfx:ESNDataPager ID="esnPager1" CssClass="paging" runat="server" FirstButtonImageUrl="../common/images/board_btn_pprev.png"
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

    <asp:HiddenField ID="hdschId" runat="server" />
    <MSG:ucSMS ID="ucsms" runat="server" PopWidth="480" />

</asp:Content>
