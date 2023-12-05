
var f = new Format();
var m = new Message();

/*
* author  : 이주용
* version : 09.03.20
* comment : 입력 값의 형태를 지정한다.
*/
function Format() {
    this.PHONENO = 1;
    this.BIZNO = 2;
    this.JUMIN = 3;
    this.INCOR = 4;
    this.EMAIL = 5;
    this.TIME = 6;
    this.ZIPNO = 7;
}

/*
* author  : 이주용
* version : 09.03.20
* comment : 메시지를 정의한다.
*/
function Message() {
    this.message = new Array(
			'-전화번호 형식 오류\n', '-사업자번호 형식 오류\n', '-법인번호 형식 오류\n', '-주민번호 형식 오류\n', '-이메일 형식 오류\n', '-시간 형식 오류\n', '-우편번호 형식 오류\n'

	);
}

Message.prototype.getMessage = function (index) {
    return this.message[index - 1];
};


/*
author : 이주용
version : 09.03.31
comment : 문자열의 null 값 체크
*/
String.prototype.isEmpty = function () {
    return ((this == null) || (this.trim().length == 0))
};


/*
author : 이주용
version : 09.03.31
comment : 문자열의 왼쪽을 기준으로 length만큼 char로 치환
*/
String.prototype.padLeft = function (length, char) {
    if (((!length) || (length < 1)) || (this.isEmpty()))
        return this;

    var padding = '                                                                                                    ';

    if (char) {
        padding = padding.replace(/ /g, char);
    }
    return (padding.substr(0, length) + this);
};

/*
* author  : 이주용
* version : 09.03.20
* comment : 문자열에서 공백을 제거한다. 
*/
String.prototype.trim = function () {
    return this.replace(/^\s*|\s*$|/gi, '');
};

/*
* author  : 이주용
* version : 09.03.20
* comment : 문자열을 추가한다.
*/
String.prototype.append = function (str) {
    return this + str;
};


/*
* author  : 이주용
* version : 09.03.23
* comment : 문자열을 추가하고 줄내림(\n)을 추가한다. 
*/
String.prototype.appendLine = function (str) {

    if (!str.isEmpty())
        return this + str + "\n";
    return this;
};

/*
* author : 이주용
* version : 09.03.25
* comment : 대소문자를 구분하지 않고 특정 문자열의 위치 검색(indexOf 와 비교)
*/
String.prototype.searchString = function (str) {
    var thisTemp = this.toLowerCase();
    var strTemp = str.toLowerCase();

    return thisTemp.indexOf(strTemp);
};

String.prototype.isContain = function (str) {
    return this.searchString(str) != -1;
};


/*
* author : 이주용
* version : 09.03.25
* comment : 문자열에서 특정 문자(str)이 반복되어 나오는 횟수반환
*/
String.prototype.countString = function (str) {
    var count = 0;
    var pos = this.indexOf(str, 0);

    while (pos != -1) {
        count++;
        pos = this.indexOf(str, pos + 1);
    }
    return count;
};

/*
* author : 이주용
* version : 09.03.25
* comment : 경로를 가지는 문자열에서 확장자 부분을 반환한다.
*/
String.prototype.getExtension = function () {
    var pos = this.lastIndexOf(".");

    return this.substring(pos + 1);
};

/*
* author : 이주용
* version : 09.03.25
* comment : 파일명을 반환한다.
*/
String.prototype.getFileName = function () {
    var pos = this.lastIndexOf("/");
    return this.substring(pos + 1);
};

String.prototype.isContainPlainText = function (forbiden) {
    // 모든 태그 제거
    var source = this.replace(es.regex.Tag, '');
    var pattern = eval('/' + forbiden + '/gi');

    return pattern.test(source);
};

/*
* author : 이주용
* version : 09.06.15
* comment : 숫자만 반환한다.
*/
String.prototype.num = function () {
    return (this.trim().replace(/[^0-9]/g, ""));
};


/*
* author  : 이주용
* version : 09.03.20
* comment : 전화번호 형식을 체크한다.
*/
String.prototype.checkPhoneNo = function () {
    return this.checkFormat(f.PHONENO);
};


/*
* author  : 이주용
* version : 09.03.20
* comment : 사업자번호 형식을 체크한다.
*/
String.prototype.checkBizNo = function () {
    return this.checkFormat(f.BIZNO);
};

/*
* author  : 이주용
* version : 09.03.20
* comment : 주민번호 형식을 체크한다.
*/
String.prototype.checkJumin = function () {
    return this.checkFormat(f.JUMIN);
};

String.prototype.Jumin = function () {
    if (this.trim().length == 13) {
        var temp = this.substring(0, 6) + "-" + this.substr(6, 7);
        return temp;
    }
    else {
        return "-";
    }
};

/*
* author  : 이주용
* version : 09.03.20
* comment : 메일주소 형식을 체크한다.
*/
String.prototype.checkEmail = function () {
    return this.checkFormat(f.EMAIL);
};

/*
* author  : 이주용
* version : 09.03.20
* comment : 법인번호 형식을 체크한다.
*/
String.prototype.checkInCor = function () {
    return this.checkFormat(f.INCOR);
};

/*
* author  : 이주용
* version : 09.03.20
* comment : 법인번호 형식을 체크한다.
*/
String.prototype.checkZipNo = function () {
    return this.checkFormat(f.ZIPNO);
};



String.prototype.checkFormat = function (num) {
    var regexp;
    var ret = false;

    switch (num) {
        case 1: // 전화번호 [000-0000-0000]		
            regexp = es.regex.PhoneDigit;
            ret = regexp.test(this);
            break;
        case 2: // 사업자번호 000-00-00000
            regexp = es.regex.BizDigit;
            ret = (regexp.test(this) && es.valid.isValidBusinessNo(this));
            break;
        case 3: // 주민번호
            regexp = /^\d{6}-?\d{7}$/;
            ret = (regexp.test(this) && es.valid.isValidJumin(this));
            break;
        case 4: // 법인번호
            regexp = /^\d{6}-?\d{7}$/;
            ret = (regexp.test(this) && es.valid.isValidCorporationNo(this));

            break;
        case 5: // 이메일주소
            regexp = es.regex.Email;
            ret = regexp.test(this);

            if (!ret) {
                regexp = /[^@]+@[A-Za-z0-9_-]+[.]+[A-Za-z0-9_-]+[.]+[A-Za-z]+/;
                ret = regexp.test(this);
            }

            if (!ret) {
                regexp = /[^@]+@[A-Za-z0-9_-]+[.]+[A-Za-z0-9_-]+[.]+[A-Za-z0-9_-]+[.]+[A-Za-z]+/;
                ret = regexp.test(this);
            }
            break;

        case 6: // 시간 (hh:mm:ss)
            regexp = /([0-1][0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])/;
            ret = regexp.test(this);
            break;

        case 7: // 우편번호 (hh:mm:ss)
            regexp = es.regex.ZipCode;
            ret = regexp.test(this);
            break;
    }
    return ret;
};

String.prototype.convertToDateFormat = function (fmt) {
    if (!/^\/Date\([\d]+\)\/$/.test(this))
        return "";

    var date = new Date();
    date.setTime(this.replace(/[^\d]/gi, ''));
    return date.toFormatString(fmt)
};

String.prototype.convertTextToHtml = function () {
    return this.replace(/\n/gi, '<br/>').replace(/\t/gi, '&nbsp;&nbsp;');
};

Array.prototype.contains = function (element) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] == element) {
            return true;
        }
    }
    return false;
};

Array.prototype.removeVal = function (val) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] == val) {
            this.splice(i, 1);
            return i;
        }
    }
    return -1;
};

Array.prototype.remove = function (index) {
    var temp = new Array();
    var i = this.length;

    while (i > index) {
        var item = this.pop();
        temp.push(item);
        i--;
    }

    for (var i = temp.length - 2; i >= 0; i--) {
        this.push(temp[i]);
    }
};

Date.prototype.toFormatString = function (fmt) {

    if (!this.valueOf()) return "";

    var dt = this;
    return fmt.replace(/(yyyy|yy|mm|dd|hh24|hh|mi|ss|ms|am|pm)/gi,
        function ($1) {

            function NN(num) {
                num = parseInt(num, 10);
                if (num > 9)
                    return num;
                else
                    return '0' + '' + num;
            }

            switch ($1) {
                case 'yyyy': return dt.getFullYear();
                case 'yy': return dt.getFullYear().toString().substr(2);
                case 'mm':
                    var mm = '';
                    var month = dt.getMonth() + 1;

                    if (month < 10)
                        mm = '0' + month;
                    else
                        mm = month;
                    return mm;
                case 'dd': return dt.getDate() < 10 ? '0' + dt.getDate() : dt.getDate();
                case 'hh': return (h = dt.getHours() % 12) ? h : 12;
                case 'hh24': return dt.getHours();
                case 'mi': return NN(dt.getMinutes());
                case 'ss': return NN(dt.getSeconds());
                case "ms": return dt.getMilliseconds();
                case 'am': return dt.getHours() < 12 ? 'am' : 'pm';
                case 'pm': return dt.getHours() < 12 ? 'am' : 'pm';
            }
        }
    );
};

$(document).ready(function () {
    $('input[disabled="disabled"][type="checkbox"]').parent().css({
        //backgroundColor: "#e8e8e8",
        cursor: "no-drop"
    });
});