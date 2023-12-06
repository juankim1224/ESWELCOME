<%@ Page Title="" Language="C#" MasterPageFile="~/master/user.master" AutoEventWireup="true" CodeBehind="schCalendar.aspx.cs" Inherits="WEB.schedule.schCalendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .lgd {
            width: 35px;
            line-height: 35px;
            text-align: center;
            font-size: 13px;
            border-radius: 50%;
            display: inline-block;
            margin-right: 5px;
        }

        .interview {
            color: #7c1bff;
            background: #f1e6ff;
        }

        .meeting {
            color: #ff4800;
            background: #ffe8d5;
        }

        .etc {
            color: #2e82ff;
            background: #e5efff;
        }

        .pclass:hover {
            cursor: pointer;
        }

        .threeUp {
            margin-top: 30px;
        }

            .threeUp:hover {
                cursor: pointer;
            }

            .threeUp + ul {
                padding: 12px;
                background-color: white;
            }

        .trtr {
            height: 40px;
        }

        .sunday {
            color: red;
        }

            .sunday span {
                color: red;
            }

        .saturday {
            color: dodgerblue;
        }

            .saturday span {
                color: dodgerblue;
            }
    </style>

    <script>

        $(function () {

            $('.threeUp').hover(function () {
                var ul = $(this).siblings('ul');
                ul.css('visibility', 'visible');

                var ulParents = $(this).parents();

                ulParents.on('click', function () {
                    var ul = $(this).find('ul');
                    ul.css('visibility', 'hidden');
                })

            });


        });

        function fnschDetail(schId) {
            window.location.href = 'schDetail.aspx?schId=' + schId;
        }

        function fnPrevMonth() {
            __doPostBack('<%=lnkPrevBtn.UniqueID%>', '');
        }

        function fnNextMonth() {
            __doPostBack('<%=lnkNextBtn.UniqueID%>', '');
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <section id="contentWrap">
        <div class="hederSubmenu">
            <!-- nav -->
            <nav>
                <p><a href="schMain.aspx">리스트</a></p>
                <p><a href="schCalendar.aspx" class="active">캘린더</a></p>
            </nav>
            <!-- nav -->
        </div>
        <article class="area mainWrap">
            <div class="mainTitle">
                <h3>캘린더</h3>
                <div>
                    <select id="SEARCH_YEAR" runat="server" align="center">
                        <option value="2023" align="center">2023</option>
                        <option value="2024" align="center">2024</option>
                    </select>
                    <asp:DropDownList ID="SEARCH_MONTH" runat="server"></asp:DropDownList>
                    <asp:LinkButton runat="server" ID="searchSch" OnClick="searchSch_Click">조회</asp:LinkButton>
                </div>
            </div>

            <!-- 게시판 내용 -->
            <div class="mainBoardWrap">
                <div class="calendar-year-month">
                    <a href="javascript:fnPrevMonth();">
                        <img src="../common/images/calendar_icon_prev.png" alt="이전달" /></a>
                    <%-- asp:Label은 css 적용이 안 될 수 있음 --%>
                    <strong>
                        <asp:Literal runat="server" ID="litYear"></asp:Literal>.&nbsp;<asp:Literal runat="server" ID="litMonth"></asp:Literal>
                    </strong>
                    <a href="javascript:fnNextMonth();">
                        <img src="../common/images/calendar_icon_next.png" alt="다음달" /></a>
                </div>
                <div id="remarks">
                    <strong class="lgd interview">면접</strong> 1차·2차 면접 &nbsp;&nbsp;
                    <strong class="lgd meeting">미팅</strong> 미팅 &nbsp;&nbsp;
                    <strong class="lgd etc">기타</strong> 기타
                </div>
                <br />
                <br />

                <!-- 테이블 -->
                <asp:Table ID="tblCalendar" runat="server" CssClass="tableCalendar">
                </asp:Table>
                <!-- //테이블 -->

            </div>
            <!-- //게시판 내용 -->
        </article>
    </section>

    <asp:HiddenField ID="hddYear" runat="server" />
    <input type="hidden" id="hddMonth" runat="server" />
    <asp:LinkButton ID="lnkPrevBtn" OnClick="lnkPrevBtn_Click" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="lnkNextBtn" OnClick="lnkNextBtn_Click" runat="server"></asp:LinkButton>
</asp:Content>
