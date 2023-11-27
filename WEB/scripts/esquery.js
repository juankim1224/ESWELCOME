var __globalFuc = {
    items: [],
    add: function (func) {
        this.items[this.items.length] = func;
    },

    run: function () {
        for (var a = 0; a < this.items.length; a++) {
            this.items[a]();
        }
    }
};



/* 모든페이지에 공통적으로 적용되는 스크립트입니다.*/
function fnGlobal() {
    document.onfocusin = es.event.removeFocus;

    __globalFuc.run();

    $("input[placeholder],textarea[placeholder]").css({
        'padding': '2px',
        'color': '#ccc',
        'width' : '90%'
    });

    //공통 작업처리
    $('input.v-number').keypress(es.event.onlyNumber).css({
        'ime-mode': 'disabled',
        '-webkit-ime-mode': 'disabled',
        'text-align': 'right',
        'padding': '0 2px'
    });

    $('input.v-number-l').keypress(es.event.onlyNumber).css({
        'ime-mode': 'disabled',
        '-webkit-ime-mode': 'disabled',
        'text-align': 'left',
        'padding': '0 2px'
    });

    $('input.v-tel').keypress(es.event.onlyInteger).css({
        'ime-mode': 'disabled',
        'padding': '1px 2px'
    }).blur(function () {
        var val = $(this).val();
        if (val.trim() != '' && !val.checkPhoneNo()) {
            alert('형식이 잘못되었습니다.');
            $(this).val('');
        }
    });

    $('input.v-email').css({
        'ime-mode': 'disabled',
        'padding': '0 2px'
    }).blur(function () {
        var val = $(this).val();
        if (val.trim() != '' && !val.checkEmail()) {
            alert('이메일주소가 유효하지 않습니다.');
            $(this).val('');
        }
    });

    $('input.v-corpono').blur(function () {
        var val = $(this).val();
        if (val.trim() != '' && !es.regex.IncorpoDigit.test(val.trim())) {
            alert('법인번호(주민번호)가 유효하지 않습니다.');
            $(this).val('');
        }
    });

    $('input.v-bizno').blur(function () {
        var val = $(this).val();
        if (val.trim() != '' && !val.checkBizNo()) {
            alert('사업자번호가 유효하지 않습니다.');
            $(this).val('');
        }
    });

    $('input.v-id').keyup(function (event) {
        if (!es.regex.EnglishAndDigit.test($(this).val())) {
            $(this).val('');
            event.returnValue = false;
        }
    });

    $('input.v-email-front').blur(function (event) {

        var regFrontLoginID = /^[\dA-Za-z||^_||^-||.-]{1,100}$/i;
        if ($(this).val() != "" && !regFrontLoginID.test($(this).val())) {
            $(this).val('');
            $(this).focus();
            event.returnValue = false;
            alert("이메일 주소는 영문과 숫자만 가능합니다.");
        }
    });

    $('input.v-loginid').keyup(function (event) {
        if (!es.regex.EnglishAndDigit.test($(this).val())) {
            $(this).val('');
            event.returnValue = false;
        }
    }).blur(function () {
        var val = $(this).val().trim();
        var regLoginID = /^.*(?=.{5,24})(?=.*[0-9a-zA-Z]).*$/

        if (val != '' && !regLoginID.test(val)) {
            alert('로그인 아이디는 영문, 숫자를 사용하여 5~24자 이내여야 합니다.');
            $(this).val('');
        }

        /*
        if (val != '' && !es.regex.EnglishAndDigit.test(val)) {
        alert('로그인 아이디는 숫자 또는 영문자 조합이어야 합니다.');
        $(this).val('');
        }
        

        if (val.length > 50) {
        alert('로그인 아이디는 50자 이하여야 합니다.');
        $(this).val('');
        }

        */
    }).css({
        'ime-mode': 'disabled'
    });

    $('input.v-loginpwd').blur(function () {
        var val = $(this).val().trim();

        var regPwd = /^.*(?=.{10,12})(?=.*[0-9])(?=.*[a-zA-Z]).*$/

        if (val != '' && !regPwd.test(val)) {
            alert('비밀번호는 영문,숫자를 혼합하여 10~12자 이내여야 합니다.');
            $(this).val('');
            $(this).focus();
        }

        /*
        if (val != '' && !es.regex.NoneKorean.test(val)) {
        alert('비밀번호는 한글을 제외한 문자여야 합니다.');
        $(this).val('');
        }

        if (val != '' && (val.length < 6 || val.length > 16)) {
        alert('비밀번호는 6자이상 16자 이하여야 합니다.');
        $(this).val('');
        }
        */

    });

    $('input.v-password').blur(function () {
        var val = $(this).val();
        if (val.trim() != '' && val.trim().length < 8) {
            alert('비밀번호는 8자리 이상이어야 합니다.');
            $(this).val('');
        }
    }).css({
        'ime-mode': 'disabled'
    });

    $('input.v-number2').keypress(es.event.onlyInteger).css({
        'ime-mode': 'disabled',
        'text-align': 'right',
        'padding': '0 2px'
    });

    $('input.v-money').keypress(function () {

        var cVal = $(this).val();

        var clip = es.dom.getSelectionString();
        cVal = cVal.replace(clip, '');
        $(this).val(cVal);

        if (event.keyCode == 45 && cVal.indexOf('-') == -1) {
            $(this).val('-' + '' + cVal);
            event.returnValue = false;
        }

        if (event.keyCode == 45 && $.trim(cVal) == '') {
            es.event.onlyInteger();
        }
        else {
            es.event.onlyNumber();
        }
    }).css({
        'ime-mode': 'disabled',
        'text-align': 'right',
        'padding': '0 2px'
    }).blur(function () {
        var $this = $(this);

        var regex = /[^\d|^,]/g;
        if ($this.hasClass("minus")) {
            regex = /[^\d|^,^-]/g;
        }

        if (regex.test($this.val())) {
            $this.val($this.val().replace(regex, ''));
        }
    }).keyup(function () {
        var cVal = $(this).val();
        cVal = cVal.replace(/,/gi, '');

        if (/^(-)?[\d]*$/.test(cVal)) {

            if (/-/.test(cVal)) {
                cVal = cVal.substring(1);
                $(this).val('-' + es.format.getNumberFormat(cVal));
            }
            else {
                $(this).val(es.format.getNumberFormat(cVal));
            }
        }
        else {
            //$(this).val('');
        }
    });

    $('input.v-pointnumber').css({
        'ime-mode': 'disabled',
        'text-align': 'right'
    }).NumberLimitDigitPoint();

    $('input.v-pointnumber2').css({
        'ime-mode': 'disabled',
        'text-align': 'right'
    }).NumberLimitDigitPoint(2);

    $('input.v-pointnumber3').css({
        'ime-mode': 'disabled',
        'text-align': 'right'
    }).NumberLimitDigitPoint(3);

    $('input.v-pointnumber4').css({
        'ime-mode': 'disabled',
        'text-align': 'right'
    }).NumberLimitDigitPoint(4);

    $('input.v-pointnumber5').css({
        'ime-mode': 'disabled',
        'text-align': 'right'
    }).NumberLimitDigitPoint(5);

    $('input[maxlength]').keyup(function () {
        var $input = $(this);

        if ($input.val().length == $input.attr('maxlength')) {
            var $next = $input.next();

            if (typeof ($next) != 'undefined' && $input.attr('group') == $next.attr('group')) {
                $next.focus();
            }

            //            var notfocus = $input.attr('notfocus');

            //            if (typeof (notfocus) == 'undefined') {
            //                $input.next().focus();
            //            }
        }
    });

    $('div.pop_wrap, div.pop_wrap2, div.event').draggable({
        containment: "window",
        handle: "div.pop_header",
        cursor: "move"
        //cursorAt: {top:10, left:10}
    });

    $('.es-no').keypress(function (e) {
        if (e.keyCode < 48 || e.keyCode > 57) {
            e.preventDefault();
            //return false;
        }
    }).blur(function () {
        var $this = $(this);

        if (/[^\d]/.test($this.val())) {
            $this.val($this.val().replace(/[^\d]/g, ''));
        }
    }).keyup(function (e) {
        var $this = $(this);
        $this.val($this.val().replace(/[\ㄱ-ㅎ가-힣]/g, ''));
    });


    //$('input.v-email-domain').keypress(function (e) {
    //    var $this = $(this);
    //    var email_reg = /\W||^.||^-/gi;
    //    if (!email_reg.test($(this).val())) {
    //        e.preventDefault();
    //        return false;
    //    }
    //}).blur(function () {
    //    var $this = $(this);
    //    //alert(/[A-Za-z0-9_-]+[.]+[A-Za-z]+/.test($this.val()));
    //    var regexp = /[A-Za-z0-9_-]+[.]+[A-Za-z]+/;
    //    var match = regexp.exec($this.val());
    //    if ($this.val() != "" && !(match != null && match[0] == $this.val())) {
    //        $this.val('');
    //        $this.focus();
    //        alert('이메일 도메인 주소를 올바르게 입력해주시기 바랍니다.');
    //    }
    //});


    $('input.v-email-domain').keypress(function (e) {
        var $this = $(this);
        var email_reg = /\W||^.||^-/gi;
        //if (!email_reg.test($(this).val())) {
        //    e.preventDefault();
        //    return false;
        //}
    }).blur(function () {
        var $this = $(this);
        //alert(/[A-Za-z0-9_-]+[.]+[A-Za-z]+/.test($this.val()));
        // regexp = /[A-Za-z0-9_-]+[.]+[A-Za-z]+/;
        var regexp = /([A-Za-z0-9\_\-\.]+)+[a-zA-Z]{1,}/g;
        var match = regexp.exec($this.val());
        if ($this.val() != "" && !(match != null && match[0] == $this.val())) {
            $this.val('');
            $this.focus();
            alert('이메일 도메인 주소를 올바르게 입력해주시기 바랍니다.');
        }
    });


    $('input.es-kr').css('ime-mode', 'active');


    if (typeof (window["fnAuth"]) == "function") {
        window["fnAuth"]();
    }

    __fnTrMouseOverAndOut();
}

/*마우스 over or out시 배경색 변경 효과 적용*/
function __fnTrMouseOverAndOut() {
    $('.grid_section_1 tr:not(:first):not(.colorfix)').mouseover(function () {
        $('>td', $(this)).css({ 'background-color': '#eee' });
    });

    $('.grid_section_1 tr:not(:first):not(.colorfix)').mouseout(function () 
    {
        $('>td', $(this)).css({ 'background-color': '#fff' });
    });
}


function esSort(sortNM, sortDirection) {
    $('input[name="sortNM"]').val(sortNM);
    $('input[name="sortDirection"]').val(sortDirection);

    if (typeof (window['fnListBind']) != "undefined") {
        fnListBind();
    }
}

function printArea(printHtml) {
    $('div#printArea').html(printHtml);
    window.print();
}


/*모든 페이지에서 사용하는 스크립트 선언*/
$(function () {

    fnGlobal();

    //    $("html").rightClick(function (e) {
    //        if (!g_isShowPop) {
    //            //fnOpen_rightmousepop1();
    //            g_isShowPop = true;
    //        }
    //    }).noContext();
});






/*
dreamweaver 호환 스크립트 
*/
function MM_swapImgRestore() { //v3.0

    var i, x, a = document.MM_sr; for (i = 0; a && i < a.length && (x = a[i]) && x.oSrc; i++) x.src = x.oSrc;

}

function MM_preloadImages() { //v3.0

    var d = document; if (d.images) {
        if (!d.MM_p) d.MM_p = new Array();

        var i, j = d.MM_p.length, a = MM_preloadImages.arguments; for (i = 0; i < a.length; i++)

            if (a[i].indexOf("#") != 0) { d.MM_p[j] = new Image; d.MM_p[j++].src = a[i]; }
    }

}



function MM_findObj(n, d) { //v4.01

    var p, i, x; if (!d) d = document; if ((p = n.indexOf("?")) > 0 && parent.frames.length) {

        d = parent.frames[n.substring(p + 1)].document; n = n.substring(0, p);
    }

    if (!(x = d[n]) && d.all) x = d.all[n]; for (i = 0; !x && i < d.forms.length; i++) x = d.forms[i][n];

    for (i = 0; !x && d.layers && i < d.layers.length; i++) x = MM_findObj(n, d.layers[i].document);

    if (!x && d.getElementById) x = d.getElementById(n); return x;

}



function MM_swapImage() { //v3.0

    var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2); i += 3)

        if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; }

}

function MM_showHideLayers() { //v9.0
    var i, p, v, obj, args = MM_showHideLayers.arguments;
    for (i = 0; i < (args.length - 2); i += 3)
        with (document) if (getElementById && ((obj = getElementById(args[i])) != null)) {
            v = args[i + 2];
            if (obj.style) { obj = obj.style; v = (v == 'show') ? 'visible' : (v == 'hide') ? 'hidden' : v; }
            obj.visibility = v;
            //obj.display = (v == 'hidden') ? "none" : "";
        }
}

//-- 사업자번호 하이폰 입력
function licenseNum(str)
{
    str = str.replace(/[^0-9]/g, '');
    var tmp = '';
    if (str.length < 4) {
        return str;
    } else if (str.length < 7) {
        tmp += str.substr(0, 3);
        tmp += '-';
        tmp += str.substr(3);
        return tmp;
    } else {
        tmp += str.substr(0, 3);
        tmp += '-';
        tmp += str.substr(3, 2);
        tmp += '-';
        tmp += str.substr(5);
        return tmp;
    }
    return str;
}

var Base64 = {
    keyStr: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=",
    encode: function (input) {
        var output = "";
        var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
        var x = 0;

        input = Base64._utf8_encode(input);

        while (x < input.length) {

            chr1 = input.charCodeAt(x++);
            chr2 = input.charCodeAt(x++);
            chr3 = input.charCodeAt(x++);

            enc1 = chr1 >> 2;
            enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
            enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
            enc4 = chr3 & 63;

            if (isNaN(chr2)) enc3 = enc4 = 64;
            else if (isNaN(chr3)) enc4 = 64;

            output = output + this.keyStr.charAt(enc1) + this.keyStr.charAt(enc2) + this.keyStr.charAt(enc3) +
                        this.keyStr.charAt(enc4);
        }

        return output;
    },
    decode: function (input) {
        var output = "";
        var chr1, chr2, chr3;
        var enc1, enc2, enc3, enc4;
        var x = 0;

        input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");

        while (x < input.length) {

            enc1 = this.keyStr.indexOf(input.charAt(x++));
            enc2 = this.keyStr.indexOf(input.charAt(x++));
            enc3 = this.keyStr.indexOf(input.charAt(x++));
            enc4 = this.keyStr.indexOf(input.charAt(x++));

            chr1 = (enc1 << 2) | (enc2 >> 4);
            chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
            chr3 = ((enc3 & 3) << 6) | enc4;

            output = output + String.fromCharCode(chr1);

            if (enc3 != 64) output = output + String.fromCharCode(chr2);

            if (enc4 != 64) output = output + String.fromCharCode(chr3);
        }

        output = Base64._utf8_decode(output);

        return output;
    },
    _utf8_encode: function (string) {
        string = string.replace(/\r\n/g, "\n");
        var utftext = "";

        for (var n = 0; n < string.length; n++) {

            var c = string.charCodeAt(n);

            if (c < 128) utftext += String.fromCharCode(c);

            else if ((c > 127) && (c < 2048)) {
                utftext += String.fromCharCode((c >> 6) | 192);
                utftext += String.fromCharCode((c & 63) | 128);
            }

            else {
                utftext += String.fromCharCode((c >> 12) | 224);
                utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                utftext += String.fromCharCode((c & 63) | 128);
            }
        }

        return utftext;
    },
    _utf8_decode: function (utftext) {
        var string = "";
        var x = 0;
        var c = c1 = c2 = 0;

        while (x < utftext.length) {

            c = utftext.charCodeAt(x);

            if (c < 128) {
                string += String.fromCharCode(c);
                x++;
            }

            else if ((c > 191) && (c < 224)) {
                c2 = utftext.charCodeAt(x + 1);
                string += String.fromCharCode(((c & 31) << 6) | (c2 & 63));
                x += 2;
            }

            else {
                c2 = utftext.charCodeAt(x + 1);
                c3 = utftext.charCodeAt(x + 2);
                string += String.fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
                x += 3;
            }
        }
        return string;
    }
}