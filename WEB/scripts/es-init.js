/*
author : jylee
description : loading base scripts
*/

/*사이트 기본설정*/
var es = es || {
    siteRoot: ""
};

/*cosole 출력*/
//var console = {
//    /*obj객체에 대한 모든 속성 값을 context에 출력한다.*/
//    log: function (obj, context) {
//        var msg = '';

//        if (typeof obj == "object") {

//            for (var p in obj) {
//                msg += p + ":" + obj[p] + ', \t\n';
//            }
//        }
//        else {
//            msg = obj.toString();
//        }

//        if (typeof context !== "undefined" && typeof context.mode !== "undefined" && context.mode == "debug") {
//            return msg;
//        }
//        else {
//            alert(msg);
//        }
//    }
//};

/*상속관계를 생성하는 Factory Class*/
var ESClass = function (parent) {
    var cls = function () {
        this.init.apply(this, arguments);
    };

    if (parent) {
        var subClass = function () { };
        subClass.prototype = parent.prototype;
        cls.prototype = new subClass;
    }

    cls.prototype.init = function () { };
    cls.fn = cls.prototype;
    cls.fn.parent = cls;
    cls._super = cls.__proto__;

    /*클래스에 메서드 추가(static 메서드와 비슷)*/
    cls.extend = function (o) {
        var extended = o.extended;

        for (var p in o) {
            cls[p] = o[p];
        }

        if (extended) {
            extended(cls);
        }
    };

    /*prototype에 메서드 추가(instance 메서드와 비슷)*/
    cls.include = function (o) {
        var included = o.included;

        for (var p in o) {
            cls.fn[p] = o[p];
        }

        if (included) {
            included(cls);
        }
    }

    cls.proxy = function (func) {
        var self = this;
        return (function () {
            return func.apply(self.arguments);
        });
    };

    cls.fn.proxy = cls.proxy;

    return cls;
};


/*컨트롤 제어 스크립트*/
es.controls = es.controls || {
    show: function (c) {
        //컨트롤을 화면에 보여준다.
        $(c).show();
    },
    hide: function (c) {
        //c 컨트롤을 화면에 보이지 않게 hidden처리한다.
        $(c).hide();
    },
    checkbox: {
        /* 선택 된 체크박스 중에서 활성화 된 이름이 'name'인 컨트롤의 값을 '@'으로 연결하여 반환한다.*/
        getSelectedValueWithName: function (name) {
            var val = '';
            var delimiter = '@';

            $('input:checkbox:not(:disabled)[name$="' + name + '"]:checked').each(function (i, e) {
                val += $(e).val() + delimiter;
            });

            if (val != '') {
                val = val.substring(0, val.length - 1);
            }

            return val;
        }
    },
    radio: {
        /* 선택 된 라디오버튼 중에서 활성화 된 이름이 'name'인 컨트롤의 값을 반환한다.*/
        getSelectedValueWithName: function (name) {
            var val = '';
            $("input:radio:not(:disabled)[name$='" + name + "']:checked").each(function (i, e) {
                val = $(e).val();
            });

            return val;
        },
        //라디오 버튼 name이 "name"인 컨트롤중 check된 컨트롤의 value를 반환한다.(컨트롤의 활성화,비활성화 여부 상관없음)
        getSelectedValueInRange: function (name) {
            return $('input[name="' + name + '"]:radio:checked').val();
        }
    }, //컨트롤 초기화
    empty: function ($container) {
        $('input:text, input:hidden, textarea, select, span[data]', $container).not('input:text[name$="esnDate"]').each(function (i, e) {

            if (e.type == 'select-one')
                e.selectedIndex = 0;
            else if (e.type == 'span')
                e.innerHTML = '';
            else if (e.type == 'radio' || e.type == 'checkbox')
                ;
            else {
                $(this).val('');
            }
        });
    }
};

/* 쿠키관리 설정*/
es.cookie = es.cookie || {
    //쿠키값 등록 (name : 등록할 쿠키 명칭, value : 등록할 쿠키value, expiredays: 쿠키등록기간(1=1일,2=2일...)
    set: function (name, value, expiredays) {
        var todayDate = new Date();
        todayDate.setDate(todayDate.getDate() + expiredays);
        document.cookie = name + "=" + escape(value) + "; path=/; expires=" + todayDate.toGMTString() + ";"
    },
    //"name" 명칭의 쿠키값 얻기
    get: function (name) {
        var nameOfCookie = name + "=";
        var x = 0;
        while (x <= document.cookie.length) { //현재 웹브라우저에 등록된 쿠키값이 있을경우
            var y = (x + nameOfCookie.length);
            if (document.cookie.substring(x, y) == nameOfCookie) { //x부터 y만큼 자른 문자열이 nameOfCookie와 같을 경우 조건문 실행
                if ((endOfCookie = document.cookie.indexOf(";", y)) == -1)
                    endOfCookie = document.cookie.length;
                return unescape(document.cookie.substring(y, endOfCookie));  //name = 제외한 value값만 return
            }
            x = document.cookie.indexOf(" ", x) + 1;
            if (x == 0)
                break;
        }
        return "";
    }
};


/* markup 관련 Utility*/
es.dom = es.dom || {
    //마우스로 드레그한 문자열을 반환
    getSelectionString: function () {
        var txt = '';
        if (window.getSelection) {
            txt = window.getSelection();
        }
        else if (document.getSelection) {
            txt = document.getSelection();
        }
        else if (document.selection) {
            txt = document.selection.createRange().text;
        }

        return txt;
    },

    /*
    * author : 이주용
    * version : 09.09.04
    * comment : cellIdx 값을 기준으로 수직으로 merge
    param
    $table : 테이블 selector
    startRowIdx : 머지를 실행할 row의 Index
    cellIdx : 머지 대상 cell Index
    */
    mergeVerticalCell: function ($table, startRowIdx, cellIdx) {

        if (typeof ($table) != 'undefined') {

            var table = $table.get(0);

            var rows = table.getElementsByTagName('tr');
            var numRows = rows.length;
            var numRowSpan = 1;
            var currentRow = null;
            var currentCell = null;
            var currentCellData = null;
            var nextRow = null;
            var nextCell = null;
            var nextCellData = null;

            for (var i = startRowIdx; i < (numRows - 1); i++) {
                if (numRowSpan <= 1) {
                    currentRow = table.getElementsByTagName('tr')[i];
                    currentCell = currentRow.getElementsByTagName('td')[cellIdx];
                    currentCellData = es.dom.getCellValue(currentCell);
                }

                if (i < numRows - 1) {
                    if (table.getElementsByTagName('tr')[i + 1]) {
                        nextRow = table.getElementsByTagName('tr')[i + 1];
                        nextCell = nextRow.getElementsByTagName('td')[cellIdx];
                        nextCellData = es.dom.getCellValue(nextCell);
                    }

                    if (currentCellData == nextCellData) {
                        numRowSpan += 1;
                        currentCell.rowSpan = numRowSpan;
                        nextCell.style.display = 'none';
                    } else {
                        numRowSpan = 1;
                    }
                }
            }
        }
    },

    /*
    * author : 이주용
    * version : 09.09.04
    * comment : basicCellIdx 기준으로 머지되어 있는 리스트에서 특정 cellIdx의 cell을 다시 merge한다.
    param
    table : 리스트 객체, 또는 ID
    startRowIdx : 머지를 실행할 row의 Index
    basicCellIdx : 기준 머지 cell의 Index
    cellIdx : 머지 대상 cell Index
    */
    mergeDependentVerticalCell: function ($table, startRowIdx, basicCellIdx, cellIdx) {
        if (typeof ($table) != 'undefined') {
            var table = $table.get(0);

            var rows = table.getElementsByTagName('tr');
            var numRows = rows.length;
            var numRowSpan = 1;
            var countLoopInBasicMerge = 1;
            var currentRow = null;
            var currentCell = null;
            var currentCellData = null;
            var nextRow = null;
            var nextCell = null;
            var nextCellData = null;
            var basicRowSpan = null;

            for (var i = startRowIdx; i < (numRows - 1); i++) {

                if (i == startRowIdx || (countLoopInBasicMerge == 1)) {
                    basicRowSpan = table.getElementsByTagName('tr')[i].getElementsByTagName('td')[basicCellIdx].rowSpan;
                }
                if (numRowSpan <= 1) {
                    currentRow = table.getElementsByTagName('tr')[i];
                    currentCell = currentRow.getElementsByTagName('td')[cellIdx];
                    currentCellData = es.dom.getCellValue(currentCell);
                }

                if (i < numRows - 1) {
                    if (countLoopInBasicMerge < basicRowSpan) {
                        if (table.getElementsByTagName('tr')[i + 1]) {
                            nextRow = table.getElementsByTagName('tr')[i + 1];
                            nextCell = nextRow.getElementsByTagName('td')[cellIdx];
                            nextCellData = es.dom.getCellValue(nextCell);

                            if (currentCellData == nextCellData) {
                                numRowSpan += 1;
                                currentCell.rowSpan = numRowSpan;
                                nextCell.style.display = 'none';
                            }
                            else {
                                numRowSpan = 1;
                            }
                        }

                        countLoopInBasicMerge++;
                    }
                    else {
                        countLoopInBasicMerge = 1;
                        numRowSpan = 1;
                    }
                }
            }
        }
    },

    getCellValue: function (cell) {
        var cellText = '';

        if (typeof (cell) == 'undefined')
            return cellText;

        if (typeof (cell.tagName) == 'undefined') {
            cellText = cell.data;
        }
        else {
            cellText = cell.innerText;
        }
        return cellText;
    }
};


/* 문자열 포맷 관련 */
es.format = es.format || {

    /*소수점을 포함한 숫자형 문자열을 반환한다.*/
    getOnlyNumeric: function (s) {
        s += '';
        return s.replace(/[^\d\.-]/g, '');
    },

    /*정수 반환*/
    getOnlyInteger: function (s) {
        s += '';
        var num = this.getOnlyNumeric(s);

        if (num == '')
            num = 0;

        return parseInt(num, 10);
    },

    /*소수 반환*/
    getOnlyFloat: function (s) {
        s += '';
        var num = this.getOnlyNumeric(s);

        if (num == '')
            num = 0;

        return parseFloat(num);
    },

    /* s 문자열을 maxlength 만큼 자른 후 appendString 으로 말줄임 표시 하여 문자열을 반환한다.
    ex)
    s : 1234556
    maxlength : 3
    appendString : --
    return : 123--
    */
    getCutContent: function (s, maxlength, appendString) {

        var checkByte = 0;
        var subStrLength = 0;

        if (typeof (appendString) == 'undefined') {
            appendString = "...";
        }

        var hangul = maxlength / 2;
        var english = maxlength;
        var ret = false;

        var returnString = "";

        for (var i = 0; i < s.length; i++) {
            var lastByte = 0;
            var oneChar = s.charAt(i);

            returnString += oneChar + "";

            if (escape(oneChar).length > 4) {
                checkByte += 2;
                lastByte = 2;
            }
            else {
                checkByte++;
                lastByte = 1;
            }

            if (checkByte <= maxlength) {
                subStrLength = i + 1;
            }
            else {
                checkByte = checkByte - lastByte;
                ret = true;
                returnString += appendString;
                break;
            }
        }

        return returnString;
    },
    /*지역번호를 포함한 9-11자리의 전화번호 형태로 변환한다.*/
    getLocalPhoneNumber: function (phoneNo) {

        var ret = "형식이 잘못되었습니다.";

        if (es.regex.PhoneDigit2.test(phoneNo)) {
            ret = phoneNo.replace(es.regex.PhoneDigit2, "$1-$2-$3");
        }
        return ret;
    },

    getYYYY_MM_DD: function (s) {
        s = s + '';

        if (s.length == 8) {
            s = s.substr(0, 4) + '-' + s.substr(4, 2) + '-' + s.substr(6, 2);
        }

        return s;
    },

    /* 사업자번호 형태를 반환한다.*/
    getBusinessNo: function (s) {
        s = s.replace(/[^\d]/g, '');

        if (es.valid.isValidBusinessNo(s)) {
            s = s.replace(/([\d]{3})([\d]{2})([\d]{5})/, '$1-$2-$3');
        }
        else {
            s = "잘못된 사업자번호 입니다.";
        }
        return s;
    },

    /* 법인번호 형태를 반환한다.*/
    getCorNo: function (s) {
        s = s.replace(/[^\d]/g, '');

        if (es.valid.isValidCorporationNo(s)) {
            s = s.replace(/([\d]{6})([\d]{7})/, '$1-$2');
        }
        else {
            s = "잘못된 법인번호 입니다.";
        }
        return s;
    },
    /* 문자열을 3자리로 잘라 ","로 구분한다 */
    getNumberFormat: function (o) {

        if (typeof (o) == 'undefined')
            o = '';

        if (typeof (o) == 'object' && o != null) {
            o = o.value;
        } else if (typeof (o) == 'number') {
            o = o.toString();
        }

        if (o.trim() != '') {
            var origin = o.toString().replace(/,/g, '');
            var num1, num2 = '';
            var minus = false;

            if (/^-/.test(origin)) {
                minus = true;

                origin = origin.replace(/-/,'');
            }

            if (origin.indexOf(".") > -1) {
                num1 = origin.split('.')[0];
                num2 = origin.split('.')[1];
            }
            else {
                num1 = origin;
            }

            if (num1 == '')
                return '';

            o = Number(num1);

            var x = '';
            var sum = '';
            str = o + '';
            for (var i = str.length; i > 0; i--) {
                var c = str.charAt(str.length - i);
                if (i % 3 == 0 && (str.length != i)) { sum = sum + ','; }
                sum = sum + c;
            }

            if (num2 == '')
                return (minus ? "-" : "") + sum;
            else
                return (minus ? "-" : "") + sum + '.' + num2;
        }
        else
            return '';
    },

    getTwoDigit: function (num) {
        num = parseInt(num, 10);
        return num < 10 && num >= 0 ? '0' + num : '' + num;
    },

    yyyyMMddConvertToDate: function (yyyyMMdd) {

        yyyyMMdd += '';
        yyyyMMdd = yyyyMMdd.replace(/[^\d]/g, '');

        if (yyyyMMdd.length == 8) {
            var year = Number(yyyyMMdd.substr(0, 4));
            var month = Number(yyyyMMdd.substr(4, 2));
            var day = Number(yyyyMMdd.substr(6, 2));

            return new Date(year, month - 1, day);
        }

        return null;
    },

    /*구분자를 기준으로 전화번호를 구분하여 배열로 반환한다.*/
    getPhoneNo_Hipen: function (s) {

        var arrPhoneNo = ['', '', ''];

        s = s.replace(/[^\d]/g, '');

        if (s.length >= 9) {
            var middleNumberLength;

            if (/^02[\d]+/.test(s)) {
                middleNumberLength = s.length - 6;
                arrPhoneNo[0] = "02";
                arrPhoneNo[1] = s.substr(2, middleNumberLength);
                arrPhoneNo[2] = s.substr(s.length - 4, 4);
            }
            else {
                middleNumberLength = s.length - 7;

                arrPhoneNo[0] = s.substr(0, 3);
                arrPhoneNo[1] = s.substr(3, middleNumberLength);
                arrPhoneNo[2] = s.substr(s.length - 4, 4);
            }
        }

        return arrPhoneNo;
    }
};

/*이벤트관련 설정*/
es.event = es.event || {
    preventKeyEvent: function () {
        var evt = event;
        var node = (evt.target != null) ? evt.target : ((evt.srcElement != null) ? evt.srcElement : null);

        if (node.type == "text" && node.readOnly == true) {
            event.returnValue = false;
            window.focus();
        }

        if ((event.ctrlKey == true && (event.keyCode == 78 || event.keyCode == 82)) || (event.keyCode >= 112 && event.keyCode <= 123) || (event.keyCode == 8 && node.type != "text" && node.type != "textarea" && node.type != "password")) {
            event.keyCode = 0;
            event.cancelBubble = true;
            event.returnValue = false;
        }
        if ((evt.keyCode == 13) && ((node.type == "text") || (node.type == "password"))) { return false; }

        if ((evt.keyCode == 13) && node.type == null) {
            event.returnValue = false;
        }
    },

    /*keypress, keyup시 숫자만 입력 가능하도록*/
    onlyNumber: function () {
        if ((event.keyCode < 48) || (event.keyCode > 57)) {
            event.returnValue = false;
        }
    },

    /*keypress, keyup시 음수, 양수만 입력 가능하도록*/
    onlyInteger: function () {
        if (event.keyCode != 45 && (event.keyCode < 48) || (event.keyCode > 57)) {
            event.returnValue = false;
        }
    },
    /*A태그와 IMG태그 선택시 Focus가 out되도록 한다.*/
    //removeFocus: function () {
    //    if (event.srcElement.tagName == "A" || event.srcElement.tagName == "IMG")
    //        document.body.focus();
    //}
};

/* 속성 확장 */
es.extend = function (e1, e2) {
    for (var p in e2) {
        e1[p] = e2[p];
    }
};

/* 파일 업로드 관련*/
es.file = es.file || {
    upload: function (opt) {

        this.ifr = '';
        this.idx = this.createUID();
        this.body = document.body;
        this.opts = {
            seq: -1,
            debug: false, /*debugging flag*/
            iframepath: es.siteRoot + '/controls/f.aspx',
            uploadDir: es.siteRoot + '/uploadfiles',
            extraparams: {
                ins: "BIZPAY_fileUploader",
                uploadFileType: "A"  // A:모든파일, I:이미지
            },
            callBack: function () {
                alert('callback을 지정하세요');
            }
        };
        es.extend(this.opts, opt);
        this.createIF();
    }
};

es.file.upload.prototype = {
    createIF: function () {

        var frm = document.getElementById(this.idx);
        
        if( typeof (frm) != 'undefined' && frm != null )
        {
            frm.parentNode.removeChild(frm);
        }

        this.ifr = document.createElement("iframe");
        this.body.appendChild(this.ifr);
        this.ifr.setAttribute('id', this.idx);
        this.ifr.setAttribute('src', this.createIframeSrc());

        if (!this.opts.debug) {
            this.ifr.style.display = 'none';
        }

        setTimeout(function () { }, 500);
    },

    createIframeSrc: function () {
        var src = this.opts.iframepath;
        var query = '';

        for (var p in this.opts.extraparams) {
            var param = p + '=' + encodeURIComponent(this.opts.extraparams[p]) + '&';
            query += param;
        }

        src += '?' + query + 'dir=' + this.opts.uploadDir;
        return src;
    },

    createUID: (function () {
        var id = 0;
        return function () { return '__esIfrUpload_' + (id++); };
    })(),

    run: function () {
        if (typeof (this.ifr) == "object") {
            var btn = this.ifr.contentWindow.document.getElementById('newFile');
            btn.value = "";
            btn.click();
        }
        else {
            this.createIF();
        }
    }
    ,
    onComplete: function (data) {
        if (data.code == "200") {
            this.showData(data);
            this.opts.callBack(data);
            this.createIF();
        }
        else {
            alert(data.message);
        }
        this.createIF();
    },
    showData: function (data) {
        if (this.opts.debug) {
            for (var p in data) {
                alert(p + ":" + data[p]);
            }
        }
    }
};

var eventMethod = window.addEventListener ? "addEventListener" : "attachEvent";
var eventer = window[eventMethod];

var messageEvent = eventMethod == "attachEvent" ? "onmessage" : "message";

eventer(messageEvent, function (e) {
    if (e.data != "") {
        var obj = eval("(" + e.data + ")");
        window[obj.instance].onComplete(obj);
    }
}, false);

function onComplete(data, instance) {

    try {
        window[instance].onComplete(data);
    }
    catch (e) {
        alert(e.message);
    }
}

/*알림메세지/*/
/*확인,취소 형태로 보여지며 "확인" 클릭시 해당url로 이동한다.*/
function callConfirm(msg, url) {
    if (confirm(msg)) {
        location = url;
    }
}


/*결제관련*/
es.inipay = function () {
    this.opts = {
        subject: '',
        auctionno: '',
        price: 0,
        totalorderamt: 0,
        UsedPointAmt: 0,
        orderNo: '',
        ticketno: '',
        productName: '',
        goodname: '',
        buyerID: '',
        buyerName: '',
        buyerEmail: '',
        buyerTel: '',
        ItemCount: 1,
        information: '',
        quantity: '',
        quantityunit: '',
        method: '',
        payclosingtime: '',
        card: false,
        directBank: true,
        vBank: true
    };
};

es.inipay.prototype.PayMethod = function () {
    /*PG결제 종류 반환 메소드*/
    /*Card=신용카드,DirectBank=실시간 계좌이체,VBank=무통장 입금*/
    /*Card:DirectBank:VBank*/
    var methods = "";

    if (this.opts.card) {
        methods += "Card:";
    }

    if (this.opts.directBank) {
        methods += "DirectBank:";
    }

    if (this.opts.vBank) {
        methods += "VBank:";
    }

    return methods;
};

es.pop = es.pop || {

    /*팝업메세지*/
    message: "",

    /*팝업스타일*/
    style: {        
    },

    /*$container 안의 컨트롤들의 값을 초기화 한다.*/
    init: function ($container) {
        es.controls.empty($container);
    },

    /*client screen영역 전체를 덮는 div를 생성한다.*/
    coverScreen: function () {
        $('div.screen').remove();
        $screen = $('<div class="screen"></div>');
        $('body').append($screen);
        $('div.screen').show();
    },

    /*client screen영역 전체를 덮는 div를 생성한 후 조회중 이미지를 중앙에 보여준다.*/
    coverScreen2: function () {        
        $('div.block').remove();
        $('body').append('<div id="block" class="screen block"></div><div id="block-msg" class="block">' + this.message + '<img src="/loading_bar.gif" title="조회중" alt="조회중"/></div>');
        $('#block-msg').css(this.style);
        $('div#block').show();
    },

    /* UpdatePanel영역 내부에서 비동기로 작업을 시작할때 중앙에 조회 중 이미지를 보여준다.*/
    start: function () {
        this.coverScreen2();
        this.show($('#block-msg'), { animationOn: false });
    },

    /* start() 액션을 종료한다. */
    end: function () {
        this.message = "";
        this.style = {};
        $('div.block').remove();
    },

    /*
    opts 스타일요소를 적용한 $pop 개체를 화면 중앙에 보여준다.
    */
    show: function ($pop, opts) {
        var settings = $.extend({}, { backgroundColor: "#fff" }, opts);
        $pop.MoveCenter(settings).show();

        if (typeof (settings.parentPop) != "undefined") {
            var $parentPop = $('#' + settings.parentPop);
            $parentPop.css('z-index', 999);
        }
    },

    /*
    $pop요소를 화면에서 감춘다.
    */
    hide: function ($pop) {
        $('div.screen').hide();
        $pop.hide();
    },

    /*
    $pop을 화면중앙에 위치시키고 주변을 비활성상태로 전환한다.
    */
    loadLayer: function ($pop, opts) {
        //debugger;
        $('div.screen').remove();
        $screen = $('<div class="screen"></div>');

        if (typeof (opts.parentPop) == "undefined") {
            /* //화면스크린 클릭 시 팝업을 닫으려면 아래 스크립트를 활성화 한다.
            $screen.click(function(){
            $screen.remove();

            if( typeof($pop) != 'undefined' ){
            $pop.hide();
            }
            });
            */
        }

        $('body').append($screen);
        $('div.screen').show();

        this.show($pop, opts);
    },

    /* 팝업에서 팝업이 호출되어 있을 경우 가장 상단의 팝업을 닫을 때 호출한다.*/
    disposeLayer: function (o) {

        if (typeof o == 'object') {
            var $pop = o.self;
            var $parent = o.parent;


            if (/rightmouse/gi.test($pop.attr('id'))) {
                g_isShowPop = false;
            }

            if (typeof ($parent) == 'undefined') {
                $('div.screen').hide();
            }
            else {
                $parent.css('z-index', 1000);
            }

            if (typeof (window["fnHideIframeInNewMovablePop"]) != 'undefined') {
                fnHideIframeInNewMovablePop();
            }

            $pop.hide();
        }
    }
};


/* 범용 정규식 */
es.regex = es.regex || {
    Email: /[^@]+@[A-Za-z0-9_-]+[.]+[A-Za-z]+/, //@문자 이후에 "알파벳,숫자,-,_" 문자만 입력되도록 check
    PhoneDigit: /^\d{2,3}-?\d{3,4}-?\d{4}$/,
    PhoneDigit2: /(0(?:2|[0-9]{2}))[-|/]?([0-9]{3,4})[-|/]?([0-9]{4}$)/,
    BizDigit: /^\d{3}-?\d{2}-?\d{5}$/,
    IncorpoDigit: /^\d{6}-?\d{7}$/,
    EnglishAndDigit: /^[\dA-Za-z]{1,100}$/i,
    NoneKorean: /^[\dA-Za-z`~!@#\$%^&\*()_\-\+=\.,\?\/\|<>\\]{1,100}$/i,
    ZipCode: /^\d{3}-\d{3}$/,
    Tag: /(<[\/]?[^>]+>)/gi,
    Number: /^[\d]+$/
};

/* Server Activity */
es.request = function () {
    var xmlHttp;

    if (window.XMLHttpRequest) {
        xmlHttp = new XMLHttpRequest();
    }
    else if (window.ActiveXObject) {
        xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
    }

    return xmlHttp;
};

/* Script 관련*/
es.script = es.script || {
    isjQueryEmbedding: typeof jQuery !== "undefined",

    coreScript: ['es-prototype.js', 'esquery.js', 'es-plugin.js', 'jquery-ui-1.12.1.js'],

    scriptpath: function () {
        var matchPath = "";
        var scripts = document.getElementsByTagName("script");
        var regex = /(.*\/)es-init/i;

        for (var i = 0; i < scripts.length; i++) {
            var currentScriptPath = scripts[i].src;

            if (currentScriptPath.match(regex)) {
                matchPath = currentScriptPath.match(regex)[1];
                break;
            }
        }

        return matchPath;
    },
    load: function () {
        if (!this.isjQueryEmbedding) {
            addScript('jquery-2.2.4.js');
        }

        for (var i = 0; i < this.coreScript.length; i++) {
            addScript(this.coreScript[i]);
        }

        /*Dynamic UI관련 css링크*/
        var cssLink = top.document.createElement("link");
        cssLink.setAttribute("rel", "Stylesheet");
        cssLink.setAttribute("type", "text/css");
        cssLink.setAttribute("href", es.script.scriptpath() + "es.css");
        top.document.getElementsByTagName("head")[0].appendChild(cssLink);

        function addScript(src) {
            var regex = /(http:\/\/)/i;

            if (!src.match(regex)) {
                src = es.script.scriptpath() + src;
            }

            document.write('<scr' + 'ipt type="text/javascript" src="' + src + '"><\/scr' + 'ipt>');
        }
    },

    bookmarksite: function (title, url) {
        if (window.opera && window.print) { // opera
            var elem = document.createElement('a');
            elem.setAttribute('href', url);
            elem.setAttribute('title', title);
            elem.setAttribute('rel', 'sidebar');
            elem.click();
        }
        else if (window.chrome) {
            alert('Ctrl+D키를 누르시면 즐겨찾기에 추가하실 수 있습니다.');
        }
        else if (document.all) // ie
            window.external.AddFavorite(url, title);
        else if (window.sidebar) // firefox
            window.sidebar.addPanel(title, url, "");
    }
};


/* 입력 값 체크 */
es.valid = es.valid || {

    isOverLength: function (s, max) {
        var checkByte = 0;
        var subStrLength = 0;

        var hangul = max / 2;
        var english = max;

        for (var i = 0; i < s.length; i++) {
            var oneChar = s.charAt(i);

            if (escape(oneChar).length > 4) {
                checkByte += 2;
            }
            else {
                checkByte++;
            }

            if (checkByte <= max) {
                subStrLength = i + 1;
            }
            else {
                break;
            }
        }

        return checkByte > max;
    },

    getOverAndLength: function (s, max) {
        var checkByte = 0;
        var subStrLength = 0;

        var hangul = max / 2;
        var english = max;
        var ret = false;

        for (var i = 0; i < s.length; i++) {
            var lastByte = 0;
            var oneChar = s.charAt(i);

            if (escape(oneChar).length > 4) {
                checkByte += 2;
                lastByte = 2;
            }
            else {
                checkByte++;
                lastByte = 1;
            }

            if (checkByte <= max) {
                subStrLength = i + 1;
            }
            else {
                checkByte = checkByte - lastByte;
                ret = true;
                break;
            }
        }

        return { success: ret, current: checkByte };
    },

    /*전화번호 포맷 체크*/
    isValidLocalPhone: function (phoneNo) {
        return es.regex.PhoneDigit2.test(phoneNo);
    },

    /*주민번호 체크*/
    isValidJumin: function (juminNo) {
        var val = juminNo.replace(/[-|.]/gi, '');
        var a = val.substring(6, 7);

        if (a < '0' || a > '2') {
            return false;
        }

        var sum = 0;
        var num = 2;

        for (var i = 0; i < 12; i++) {
            a = val.substring(i, i + 1);
            sum = sum + num * (a - '0');
            num++;
            if (num == 10) num = 2;
        }

        i = (11 - (sum % 11)) % 10;

        a = val.substring(12, 13);
        if (a != i) {
            return false;
        }

        return true;
    },

    /* 사업자번호 체크*/
    isValidBusinessNo: function (bizNo) {
        bizNo = bizNo.replace(/[^\d]/gi, '');

        if (bizNo.length != 10)
            return false;

        var sum = 0;
        var getlist = new Array(10);
        var chkvalue = new Array("1", "3", "7", "1", "3", "7", "1", "3", "5");

        for (var i = 0; i < 10; i++) {
            getlist[i] = bizNo.substring(i, i + 1);
        }
        for (var j = 0; j < 9; j++) {
            sum += getlist[j] * chkvalue[j];
        }

        sum = sum + parseInt((getlist[8] * 5) / 10);
        sidliy = sum % 10;
        sidchk = 0;

        if (sidliy != 0) {
            sidchk = 10 - sidliy;
        }
        else {
            sidchk = 0;
        }
        if (sidchk != getlist[9]) {
            return false;
        }
        return true;
    },

    /* 법인번호 체크*/
    isValidCorporationNo: function (corpoNo) {
        var re = /-/g;
        sRegNo = corpoNo.replace(re, '');

        if (sRegNo.length != 13 || sRegNo === "0000000000000") {
            return false;
        }
        
        /*
        var arr_regno = sRegNo.split("");
        var arr_wt = new Array(1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2);
        var iSum_regno = 0;
        var iCheck_digit = 0;

        for (i = 0; i < 12; i++) {
            iSum_regno += eval(arr_regno[i]) * eval(arr_wt[i]);
        }

        iCheck_digit = 10 - (iSum_regno % 10);

        iCheck_digit = iCheck_digit % 10;

        if (iCheck_digit != arr_regno[12]) {
            return false;
        }                
        */

        return true;

        //var str = sRegNo;

        //var totalNumber = 0;
        //var num = 0;
        //for (i = 0; i < str.length - 1; i++) {
        //    if (((i + 1) % 2) == 0) {
        //        num = parseInt(str.charAt(i)) * 2;
        //    } else {
        //        num = parseInt(str.charAt(i)) * 1;
        //    }

        //    if (num > 0) {
        //        totalNumber = totalNumber + num;
        //    }
        //}
        //totalNumber = (totalNumber % 10 < 10) ? totalNumber % 10 : 0;
        
        //if (totalNumber != str.charAt(str.length - 1)) {
        //    return false;
        //}
        //else{
        //    return true;
        //}
    },

    /*$selector에 포함된 input,textarea,select 요소 중 클래스명으로 required를 가지는 element의 값을 체크*/
    isRequired: function ($selector) {
        var info = [];
        var msg = '';

        $('input.required, textarea.required, select.required', $selector).each(function (i, e) {
            var $ctl = $(this);

            if (e.type == 'select-one' && e.value == '') {
                info[info.length] = {
                    'msg': '- ' + $ctl.attr('vmsg') + "\n"
                };
            }
            else {
                if ($ctl.val().trim() == '' && !$ctl.attr('disabled')) {
                    info[info.length] = {
                        'msg': '- ' + $ctl.attr('vmsg') + "\n"
                    };
                }
            }
        });

        for (i = 0; i < info.length; i++) {
            msg += info[i].msg;
        }

        return msg;
    }
};


/*기본 스크립트 로드*/
es.script.load();

/*textbox에서 Enterkey에 의한 포스트백 방지*/
document.onkeydown = es.event.preventKeyEvent;
