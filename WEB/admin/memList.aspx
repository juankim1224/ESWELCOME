<%@ Page Title="" Language="C#" MasterPageFile="~/master/admin.master" AutoEventWireup="true" CodeBehind="memList.aspx.cs" Inherits="WEB.admin.memList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <section id="contentWrap">
        <article class="area mainWrap">
            <div class="mainTitle">
                <h3>임직원 관리</h3>
                <div class="mainTitle-flex">
                    <select id="MEMBER_CPY" runat="server">
                        <option value="">회사</option>
                        <option value="E">이상네트웍스</option>
                        <option value="M">메쎄이상</option>
                    </select>
                    <select id="SEARCH_TYPE" runat="server">
                        <option value="ALL">전체</option>
                        <option value="CPY">회사명</option>
                        <option value="DEPT">부서명</option>
                        <option value="TEAM">팀명</option>
                        <option value="NAME">이름</option>
                    </select>
                    <input id="SEARCH_TEXT" runat="server" />
                    <asp:LinkButton runat="server" ID="searchEmp" OnClick="searchEmp_Click">조회</asp:LinkButton>
                </div>
            </div>
            <!-- 게시판 내용 -->
            <div class="mainBoardWrap">
                <a href="#" class="etc-btn">선택 삭제</a>
                <table class="tableA">
                    <colgroup>
                        <col style="width: 80px" />
                        <col style="width: 80px" />
                        <col style="width: 150px" />
                        <col />
                        <col style="width: 200px" />
                        <col style="width: 250px" />
                        <col style="width: 150px" />
                    </colgroup>
                    <thead>
                        <tr>
                            <th>
                                <input type="checkbox" /></th>
                            <th>NO</th>
                            <th>이름</th>
                            <th>회사</th>
                            <th>부서</th>
                            <th>팀</th>
                            <th>아이디</th>
                            <th>비밀번호 초기화</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:UpdatePanel ID="upnlCommand" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Repeater ID="rptList" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <input type="checkbox" /></td>
                                            <td id="empId" style="display: none;"><%# Eval("MEM_ID") %></td>
                                            <td><%# Eval("NO") %></td>
                                            <td><%# Eval("MEM_NAME") %></td>
                                            <td><%# Eval("CPY_NAME") %></td>
                                            <td><%# Eval("DEPT_NAME") %></td>
                                            <td><%# Eval("TEAM_NAME") %></td>
                                            <td><%# Eval("MEM_LOGIN_ID") %></td>
                                            <td><a class="btn-reset" href="#">초기화</a></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </tbody>
                </table>

                <div class="paging">
                    <ESNfx:ESNDataPager ID="esnPager" CssClass="paging" runat="server" FirstButtonImageUrl="../common/images/board_btn_pprev.png"
                        LastButtonImageUrl="../common/images/board_btn_nnext.png" NextButtonImageUrl="../common/images/board_btn_next.png"
                        PrevButtonImageUrl="../common/images/board_btn_prev.png" alt="이전" OnCommand="esnPager_Command" />
                </div>
                <!-- //paging-->
            </div>
            <!-- //게시판 내용 -->
        </article>
    </section>

</asp:Content>
