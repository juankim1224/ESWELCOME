<%@ Page Title="" Language="C#" MasterPageFile="~/master/user.master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="WEB.schedule.register" %>

<%@ Register Src="~/controls/ucSMS.ascx" TagName="ucSMS" TagPrefix="MSG" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function fnCheckSave() {

            /* 유효성 체크*/
            var gstName = $("input[id$='GST_NAME']").val();
            var schType = $('#<%=SCH_TYPE.ClientID %>').val();

            var moniterY = $("p[id$='SCH_MONITER_Y']").attr('class');
            var moniterN = $("p[id$='SCH_MONITER_N']").attr('class');

            var staff = $('#<%=hdd_ARR_STAFF.ClientID %>').val();

            var gubun1 = $("p[id$='MSG_GUBUN_1']").attr('class');
            var gubun2 = $("p[id$='MSG_GUBUN_2']").attr('class');

            var tndCkY = $("p[id$='TND_CHECK_Y']").attr('class');
            var tndCkN = $("p[id$='TND_CHECK_N']").attr('class');

            var gmn1 = $("input[id$='GST_MOBILE_NO1']").val();
            var gmn2 = $("input[id$='GST_MOBILE_NO2']").val();
            var gmn3 = $("input[id$='GST_MOBILE_NO3']").val();

            var schYMD = $('#<%=SCH_YEARMD.ClientID %>').val();
            var schHH = $('#<%=SCH_HOUR.ClientID %>').val();
            var schMM = $('#<%=SCH_MIN.ClientID %>').val();

            var msgYMD = $('#<%=MSG_YEARMD.ClientID %>').val();
            var msgHH = $('#<%=MSG_HOUR.ClientID %>').val();
            var msgMM = $('#<%=MSG_MIN .ClientID %>').val();

            // 1. 공백 체크
            if (gstName === '' || gmn1 === '' || gmn2 === '' || gmn3 === ''
                || schYMD === '' || schHH === '선택' || schMM === '선택'
                || schType === '' || (moniterY == 'select-btn' && moniterN == 'select-btn')
                || staff === '' || (gubun1 === 'select-btn' && (gubun2 === 'select-btn' || msgYMD === '' || msgHH == '선택' || msgMM == '선택'))
                || (tndCkY === 'select-btn' && tndCkN == 'select-btn')) {
                alert('필수값을 모두 입력해 주세요.');
                return false;
            }

            // 2. 연락처는 숫자만 기입
            var regExpNum = /^[0-9]*$/;

            if (!regExpNum.test(gmn1) || !regExpNum.test(gmn2) || !regExpNum.test(gmn3)) {
                alert('연락처는 숫자만 기입이 가능합니다.');
                $('#<%=GST_MOBILE_NO1.ClientID %>').val('');
                $('#<%=GST_MOBILE_NO2.ClientID %>').val('');
                $('#<%=GST_MOBILE_NO3.ClientID %>').val('');
                return false;
            }

            // 2. 스케줄 날짜는 2시간 이후부터
            var todayDate = new Date();

            var schYMDHM = schYMD + ' ' + schHH + ':' + schMM + ':00';
            var schDate = new Date(schYMDHM);

            var schCk = todayDate.setHours(todayDate.getHours() + 2);

            if (schDate < schCk) {
                alert('방문 일정은 2시간 이후부터 가능합니다.');
                $('#<%=SCH_YEARMD.ClientID %>').val('');
                $('#<%=SCH_HOUR .ClientID %>').val('');
                $('#<%=SCH_MIN.ClientID %>').val('');
                return false;
            }

            // 3. 문자 발송 시간은 스케줄 2시간 전까지
            var msgYMDHM = msgYMD + ' ' + msgHH + ':' + msgMM + ':00';
            var msgDate = new Date(msgYMDHM);
            schDate.setHours(schDate.getHours() - 2);

            if (schDate < msgDate) {
                alert('문자 예약은 방문 2시간 이전까지 가능합니다.');
                $('#<%=MSG_YEARMD.ClientID %>').val('');
                $('#<%=MSG_HOUR.ClientID %>').val('');
                $('#<%=MSG_MIN .ClientID %>').val('');
                return false;
            }

            confirm('방문 등록을 하시겠습니까?');
            return true;
        }

        $(function () {

            var init = function () {
                var schId1 = $('#<%=hdd_SchId.ClientID %>').val();

                // 수정 모드
                if (schId1 != "") {
                    var schMoniter = $("input[id$='hdd_SCH_MONITER']").val();
                    if (schMoniter == 'Y') {
                        $('#SCH_MONITER_Y').addClass('active');
                    } else if (schMoniter == 'N') {
                        $('#SCH_MONITER_N').addClass('active');
                    }

                    var msgGubun = $("input[id$='hdd_MSG_GUBUN']").val();
                    if (msgGubun == '1') {
                        $('#MSG_GUBUN_1').addClass('active');
                    } else if (msgGubun == '2') {
                        $('#MSG_GUBUN_2').addClass('active');
                    }

                    var msgTndCheck = $("input[id$='hdd_TND_CHECK']").val();
                    if (msgTndCheck == 'Y') {
                        $('#TND_CHECK_Y').addClass('active');

                    } else if (msgTndCheck == 'N') {
                        $('#TND_CHECK_N').addClass('active');
                    }
                }

                $('#SCH_MONITER_Y').on('click', function () {
                    $('#SCH_MONITER_Y').addClass('active');
                    $('#SCH_MONITER_N').removeClass('active');
                    $('#<%=hdd_SCH_MONITER.ClientID %>').val('Y');
                });
                $('#SCH_MONITER_N').on('click', function () {
                    $('#SCH_MONITER_N').addClass('active');
                    $('#SCH_MONITER_Y').removeClass('active');
                    $('#<%=hdd_SCH_MONITER.ClientID %>').val('N');
                });

                // 메세지 즉시발송 선택
                $('#MSG_GUBUN_1').on('click', function () {
                    $('#MSG_GUBUN_1').addClass('active');
                    $('#MSG_GUBUN_2').removeClass('active');
                    $('#<%=hdd_MSG_GUBUN.ClientID %>').val('1');
                    $('#viewMsgTimeArea').css('visibility', 'hidden');
                });
                $('#MSG_GUBUN_2').on('click', function () {
                    $('#MSG_GUBUN_2').addClass('active');
                    $('#MSG_GUBUN_1').removeClass('active');
                    $('#<%=hdd_MSG_GUBUN.ClientID %>').val('2');
                    $('#viewMsgTimeArea').css('visibility', 'visible');
                });

                // 메세지 2차발송 선택
                $('#TND_CHECK_Y').on('click', function () {
                    $('#TND_CHECK_Y').addClass('active');
                    $('#TND_CHECK_N').removeClass('active');
                    $('#<%=hdd_TND_CHECK.ClientID %>').val('Y');
                });
                $('#TND_CHECK_N').on('click', function () {
                    $('#TND_CHECK_N').addClass('active');
                    $('#TND_CHECK_Y').removeClass('active');
                    $('#<%=hdd_TND_CHECK.ClientID %>').val('N');
                });
            };
            init();
            __globalFuc.add(init);
        });

        // [접견인] 회사, 부서, 팀, 직원 선택
        function fn_CPYChange() {
            __doPostBack('<%=lnkDummy.UniqueID%>', '');
        };
        function fn_DEPTChange() {
            __doPostBack('<%=lnkDummy2.UniqueID%>', '');
        };
        function fn_TEAMChange() {
            __doPostBack('<%=lnkDummy3.UniqueID%>', '');
        };
        function fn_STAFFChange() {
            __doPostBack('<%=lnkDummy4.UniqueID%>', '');
        };

        // 문자 미리보기
        function fnPreviewSMS() {
            var gstCpy = $('#<%=GST_CPY.ClientID %>').val();
            var gstPst = $('#<%=GST_PST.ClientID %>').val();
            var gstName = $('#<%=GST_NAME.ClientID %>').val();
            var schType = $('#<%=SCH_TYPE.ClientID %>').val();
            var schYearMD = $('#<%=SCH_YEARMD.ClientID %>').val();
            var schHourMin = $('#<%=SCH_HOUR.ClientID %>').val() + "시 " + $('#<%=SCH_MIN.ClientID %>').val() + "분";
            var msgStaff = $(":input:radio[name=msgStaff]:checked").val();  // [ 대표접견인 ] 체크된 접견인 id

            fnOpen_ucSMS({
                gstCpy: gstCpy,
                gstPst: gstPst,
                gstName: gstName,
                schType: schType,
                schYearMD: schYearMD,
                schHourMin: schHourMin,
                msgStaff: msgStaff
            });
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <section id="contentWrap">
        <article class="area mainWrap">
            <div class="mainTitle">
                <h3>방문등록</h3>
            </div>

            <!-- 게시판 내용 -->
            <div class="mainBoardWrap">
                <h4 class="vst-title-h4">방문객 정보</h4>
                <div class="vstCheckInWrap">
                    <div class="vstCheckInInner">
                        <div>
                            <p class="vst-title">
                                회사명
                            </p>
                            <div>
                                <input id="GST_CPY" runat="server" placeholder="회사명을 입력하세요." class="hihi" />
                            </div>
                        </div>
                        <div>
                            <p class="vst-title">
                                직책
                            </p>
                            <div>
                                <input id="GST_PST" runat="server" placeholder="직책을 입력하세요." class="hihi" />
                            </div>
                        </div>
                        <div>
                            <p class="vst-title">
                                성함 <span>*</span>
                            </p>
                            <div>
                                <input id="GST_NAME" runat="server" placeholder="이름을 입력하세요." class="hihi" />
                            </div>
                        </div>
                    </div>
                    <div class="vstCheckInInner">
                        <div>
                            <p class="vst-title">
                                연락처 <span>*</span>
                            </p>
                            <div class="vst-tel">
                                <input id="GST_MOBILE_NO1" runat="server" />
                                <input id="GST_MOBILE_NO2" runat="server" />
                                <input id="GST_MOBILE_NO3" runat="server" />
                            </div>
                        </div>
                        <div>
                            <p class="vst-title">
                                방문 일시 <span>*</span>
                            </p>
                            <div class="vst-tel">
                                <%-- 날짜 --%>
                                <div class="datepicker">
                                    <ESNfx:ESNDatePicker ID="SCH_YEARMD" runat="server" DateButtonImageUrl="~/common/images/icon_calendar.png"></ESNfx:ESNDatePicker>
                                    <asp:DropDownList ID="SCH_HOUR" runat="server"></asp:DropDownList>시
                                    <asp:DropDownList ID="SCH_MIN" runat="server"></asp:DropDownList>분
                                </div>
                                <script type="text/javascript">
                                    $('#<%= SCH_YEARMD.FromClientID %>').attr("style", "ime-mode:disabled;");
                                    $('#<%= SCH_YEARMD.FromClientID %>').attr("placeholder", "YYYY-DD-MM");
                                    $('#<%= SCH_YEARMD.FromClientID %>').removeAttr("onblur");
                                </script>
                            </div>
                        </div>
                        <div>
                            <div class="vst-in-flex">
                                <div class="vst-in-item2">
                                    <p class="vst-title">
                                        방문 목적 <span>*</span>
                                    </p>
                                    <div>
                                        <select id="SCH_TYPE" runat="server">
                                            <option value="" selected="selected">선택</option>
                                            <option value="1차면접">1차 면접</option>
                                            <option value="2차면접">2차 면접</option>
                                            <option value="미팅">미팅</option>
                                            <option value="기타">기타</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="vst-in-item2">
                                    <p class="vst-title">
                                        모니터 실행 <span>*</span>
                                    </p>
                                    <div class="select-btn-flex">
                                        <p class="select-btn" id="SCH_MONITER_Y">희망</p>
                                        <p class="select-btn" id="SCH_MONITER_N">비희망</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%-- ************************* 접견인 ************************* --%>

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="vstCheckInInner mt40">
                                <h4 class="vst-title-h4">접견인 정보</h4>
                                <div>
                                    <p class="vst-title">
                                        소속 회사 <span>*</span>
                                    </p>
                                    <div>
                                        <select id="ES_COMPANY" runat="server" onchange="fn_CPYChange();">
                                            <option value="" selected="selected">회사명</option>
                                            <option value="1">이상네트웍스</option>
                                            <option value="2">메쎄이상</option>
                                        </select>
                                    </div>
                                </div>
                                <div>
                                    <p class="vst-title">
                                        부서 및 팀 <span>*</span>
                                    </p>
                                    <div class="vst-tel">
                                        <%-- 부서 선택 --%>
                                        <select id="ES_DEPT" runat="server" onchange="fn_DEPTChange();"></select>
                                        <select id="ES_TEAM" runat="server" onchange="fn_TEAMChange();"></select>
                                    </div>
                                </div>
                                <div>
                                    <p class="vst-title">
                                        성함 <span>*</span>
                                    </p>
                                    <div class="flex-wrap">
                                        <select id="SCH_STAFF" runat="server" onchange="fn_STAFFChange();"></select>
                                        <div class="select-name-area" id="staffListArea">
                                            <asp:Literal ID="ltrStaffList" runat="server"></asp:Literal>
                                            <asp:HiddenField ID="hdd_ARR_STAFF" runat="server" />
                                        </div>
                                        <p class="help-vst">※ 선택된 접견인은 대표 담당자로 지정됩니다.</p>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lnkDummy" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="lnkDummy2" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="lnkDummy3" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="lnkDummy4" EventName="Click" />

                        </Triggers>
                    </asp:UpdatePanel>
                    <%-- ************************* 접견인 ************************* --%>
                    <div class="vstCheckInInner mt40">
                        <h4 class="vst-title-h4">문자 발송</h4>
                        <div>
                            <div class="vst-in-flex">
                                <div class="vst-in-item2">
                                    <p class="vst-title">
                                        발송 시간 <span>*</span>
                                    </p>
                                    <div class="select-btn-flex">
                                        <p class="select-btn" id="MSG_GUBUN_1">즉시</p>
                                        <p class="select-btn" id="MSG_GUBUN_2">예약</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div>
                            <p class="vst-title">
                                발송 예약 일정
                            </p>
                            <div class="vst-tel" id="viewMsgTimeArea">
                                <asp:Literal ID="ltrEditMsg" runat="server"></asp:Literal>
                                <%-- 날짜 --%>
                                <div class="datepicker" id="datepicker" runat="server">
                                    <ESNfx:ESNDatePicker ID="MSG_YEARMD" runat="server" DateButtonImageUrl="../common/images/icon_calendar.png"></ESNfx:ESNDatePicker>
                                    <asp:DropDownList ID="MSG_HOUR" runat="server"></asp:DropDownList><span id="siTxt" runat="server">시</span>
                                    <asp:DropDownList ID="MSG_MIN" runat="server"></asp:DropDownList><span id="bunTxt" runat="server">분</span>
                                </div>
                                <script type="text/javascript">
                                    $('#<%= MSG_YEARMD.FromClientID %>').attr("style", "ime-mode:disabled;");
                                    $('#<%= MSG_YEARMD.FromClientID %>').attr("placeholder", "YYYY-DD-MM");
                                    $('#<%= MSG_YEARMD.FromClientID %>').removeAttr("onblur");
                                </script>
                            </div>
                        </div>
                        <div>
                            <p class="vst-title">
                                2차 발송 <span>*</span>
                            </p>
                            <div class="vst-in-flex">
                                <div class="vst-in-item2">
                                    <div class="select-btn-flex">
                                        <p id="TND_CHECK_Y" class="select-btn">희망</p>
                                        <p id="TND_CHECK_N" class="select-btn">비희망</p>
                                    </div>
                                </div>
                                <div class="vst-in-item2">
                                    <div>
                                        <a href="javascript:void(0);" onclick="return fnPreviewSMS()" class="sms-btn">SMS 미리보기</a>
                                    </div>
                                </div>
                            </div>
                            <p class="help-vst">※ 일정 하루 전에 문자가 재발송됩니다.</p>
                        </div>
                    </div>
                </div>

                <div class="btns-wrap">
                    <asp:LinkButton ID="lnkSave" runat="server" OnClick="lnkSave_Click" OnClientClick="return fnCheckSave();" CssClass="btns primary-btn"></asp:LinkButton>
                    <a href="schList.aspx" class="btns secondary-btn ">목록</a>
                </div>
            </div>
            <asp:HiddenField ID="hdd_SCH_MONITER" runat="server" />
            <asp:HiddenField ID="hdd_MSG_GUBUN" runat="server" />
            <asp:HiddenField ID="hdd_TND_CHECK" runat="server" />
            <asp:HiddenField ID="hdd_SchId" runat="server" />
            <!-- //게시판 내용 -->

        </article>
    </section>

    <asp:LinkButton ID="lnkDummy" runat="server" OnClick="lnkDummy_Click"></asp:LinkButton>
    <asp:LinkButton ID="lnkDummy2" runat="server" OnClick="lnkDummy2_Click"></asp:LinkButton>
    <asp:LinkButton ID="lnkDummy3" runat="server" OnClick="lnkDummy3_Click"></asp:LinkButton>
    <asp:LinkButton ID="lnkDummy4" runat="server" OnClick="lnkDummy4_Click"></asp:LinkButton>



    <MSG:ucSMS ID="ucSMS" runat="server" PopWidth="480" />

    <script>
        function fnDeleteStaff(radioId) {

            var deleteRadio = $('#' + radioId).val().toString();

            var hddStaffList = $("input[id$='hdd_ARR_STAFF']").val();
            var staff = hddStaffList.split(',');


            for (let i = 0; i <= staff.length; i++) {

                if (staff[i] === deleteRadio) {
                    var index = staff.indexOf(staff[i]);
                    var result = staff.splice(index, 1);
                    $("input[id$='hdd_ARR_STAFF']").val(staff);
                }
            }
            $('#s' + radioId).remove();
        }

    </script>
</asp:Content>
