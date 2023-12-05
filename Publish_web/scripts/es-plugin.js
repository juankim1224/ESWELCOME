var g_isShowPop = false;
var g_openPopCount = 0;
var g_openArrPop = [];

/*
작성자 : 이주용
작성일 : 2012-12-22
용  도 : 마우스 오른쪽 클릭 방지
*/
if (jQuery) (function () { $.extend($.fn, { rightClick: function (handler) { $(this).each(function () { $(this).mousedown(function (e) { var evt = e; $(this).mouseup(function () { $(this).unbind('mouseup'); if (evt.button == 2) { handler.call($(this), evt); return false; } else { return true; } }); }); $(this)[0].oncontextmenu = function () { return false; } }); return $(this); }, rightMouseDown: function (handler) { $(this).each(function () { $(this).mousedown(function (e) { if (e.button == 2) { handler.call($(this), e); return false; } else { return true; } }); $(this)[0].oncontextmenu = function () { return false; } }); return $(this); }, rightMouseUp: function (handler) { $(this).each(function () { $(this).mouseup(function (e) { if (e.button == 2) { handler.call($(this), e); return false; } else { return true; } }); $(this)[0].oncontextmenu = function () { return false; } }); return $(this); }, noContext: function () { $(this).each(function () { $(this)[0].oncontextmenu = function () { return false; } }); return $(this); } }); })(jQuery);
/*****************************************************************************/


/*
작성자 : 이주용
작성일 : 2011-08-12
용  도 : 상세pop div를 화면의 중앙에 배치
*/
; (function ($) {
    $.fn.MoveCenter = function (options) {

        var _w = $(this).width();
        var _h = $(this).height();

        var left = ($(window).scrollLeft() + ($(window).width() - _w) / 2) + 'px';
        var top = window.innerHeight / 2 - _h / 2 + $(window).scrollTop();
        left = $(window).width() < _w ? 0 : ($(window).width() - _w) / 2;
        top = $(window).height() < _h ? $(window).scrollTop() : top;        
        //var top = Math.abs((($(window).height() - $(this).height()) / 2)) + 'px';
        //top = $(window).height() < _h ? $(window).scrollTop() : $(window).scrollTop() + ($(window).height() - _h) / 2;
        
        $.fn.MoveCenter.defaults.owidth = _w;
        $.fn.MoveCenter.defaults.oheight = _h;
        $.fn.MoveCenter.defaults.left = left;
        $.fn.MoveCenter.defaults.top = top;

        var opts = $.extend({}, $.fn.MoveCenter.defaults, options);

        return this.each(function () {
            var w = $(this).width();

            $(this).css({
                'position': opts.position,
                'z-index': opts.zIndex,
                'background-color': opts.backgroundColor,
                'left': opts.left,
                'top': opts.animationOn ? 0 : opts.top
                //'width'                 : 0,
                //'height'                : 0
            });

            if (opts.animationOn) {
                $(this).animate({
                    left: opts.left,
                    top: opts.top,
                    width: opts.owidth,
                    height: opts.oheight
                }, "slow");
            }
        });
    };

    $.fn.MoveCenter.defaults = {
        zIndex: 1000,
        //position: 'fixed',
        position: 'absolute',
        backgroundColor: '#fff',
        left: 0,
        top: 0,
        owidth: 0,
        oheight: 0,
        animationOn: false
    };
})(jQuery);

(function ($) {

    $.fn.rowspan = function (colIdx) {

        return this.each(function () {
            var that;
            $('tr', this).each(function (row) {

                $('td:eq(' + colIdx + ')', this).each(function (col) {

                    if ($.trim($(this).text()) == $.trim($(that).text())) {
                        rowspan = $(that).attr("rowSpan");
                        if (rowspan == undefined) {

                            $(that).attr("rowSpan", 1);
                            rowspan = $(that).attr("rowSpan");
                        }
                        rowspan = Number(rowspan) + 1;
                        $(that).attr("rowSpan", rowspan); // do your action for the colspan cell here
                        $(this).hide(); // .remove(); // do your action for the old cell here
                    } else {
                        that = this;
                    }
                    that = (that == null) ? this : that; // set the that if not already set
                });
            });

        });
    };

})(jQuery);


// 숫자타입 입력
$.fn.NumberInput = function () {
    $(this).bind("keydown", function (e) {
        if ((e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 96 && e.keyCode <= 105) ||
        e.keyCode == 8 ||    //BackSpace
        e.keyCode == 46 ||    //Delete
        e.keyCode == 110 ||    //소수점(.) : 문자키배열
        e.keyCode == 190 ||    //소수점(.) : 키패드
        e.keyCode == 37 ||    //좌 화살표
        e.keyCode == 39 ||    //우 화살표
        e.keyCode == 35 ||    //End 키
        e.keyCode == 36 ||    //Home 키
        e.keyCode == 9 ||    //Tab 키
        e.keyCode == 13    // Enter 키
    ) {
            // 소수점 입력의 경우
            if (e.keyCode == 110 || e.keyCode == 190) {

                // 최초 소수점 입력 시
                if ($.trim($(this).val()) == '')
                    $(this).val('0');

                // 기존에 소수점이 존재한다면
                if ($(this).val().indexOf('.') > -1) {
                    return false;
                }
            }
            return true;
        }
        else {
            return false;
        }
    });
};

// 소수점 자리수 제한 입력
$.fn.NumberLimitDigitPoint = function (digit) {

    function addCommas(nStr) {

        nStr = nStr.replace(/,/g, '');

        nStr += '';
        var x = nStr.split('.');
        var x1 = x[0];
        var x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, '$1' + ',' + '$2');
        }
        return x1 + x2;
    }

    $(this).bind("keypress", function (e) {
        var $this = $(this);
        var keyCode = e.keyCode;

        if ($this.val() != '' && $this.val().indexOf('.') > -1 && keyCode == 46)
            e.preventDefault();

        if (keyCode != 46 && keyCode != 44 && (keyCode < 48 || keyCode > 57))
            e.preventDefault();

    }).bind("keyup", function (e) {

        var $this = $(this);
        var regExp = /[^0-9,.]/g;

        if (regExp.test($this.val())) {
            $this.val('');
        }

        if (typeof (digit) == "undefined") {
            digit = $(this).attr('digit');
        }

        var arrDigit = $this.val().split('.');

        if (arrDigit.length == 2 && arrDigit[1].length > digit) {
            alert('소수점 ' + digit + '자리까지만 입력됩니다.');
            arrDigit[1] = arrDigit[1].substr(0, digit);

            $this.val(arrDigit.join('.'));
        }

        var num = $this.val();
        $this.val(addCommas(num));
    });
};

/// 문자열 바이트 길이 제한
$.fn.CheckByte = function (maxByteLength) {
    $(this).blur(function (e) {
        if (es.valid.getOverAndLength($(this).val(), maxByteLength).success) {
            $(this).val($(this).val().substring(0, maxByteLength));
        }
    });
};


/*
jQuery.show() override 함수(dom요소의 display속성을 'block'으로 처리)
*/
$.fn.showBlock = function () {

    var $o = $(this);

    if ($o != null && $o.get(0)) {
        var tagName = $o.get(0).tagName;
        $o.css('display', 'block');
    }
}


/// 이미지 슬라이드 갤러리
; (function ($) {
    $.fn.gallery = function (opts) {
        var o = $.extend(
                       {
                           enable: true,
                           width: 100,
                           height: 100,
                           data: [],
                           text: 'test',
                           current: 0,
                           marginLeft: 0,
                           totalCount: function () {
                               return this.data.length;
                           }
                       }, opts);

        return this.each(function () {
            var $this = $(this);
            var $wrap = $('<div/>');

            $.map(o.data, function (n, i) {
                $wrap.append('<img src="' + n + '"/>');
            });

            $wrap.find('img').css({
                width: o.width,
                height: o.height
            }).end().css({
                width: o.totalCount() * o.width
            });

            $this.append($wrap);

            $this.css({
                overflow: 'hidden',
                border: '2px solid #cccccc'
            });

            $this.draggable({
                containment: 'window',
                cursor: 'move'
            });


            if (o.enableCount < o.totalCount()) {

                setInterval(function () {
                    $wrap.animate({
                        marginLeft: o.width * -1
                    }, 1000, function () {

                        if (!o.enable)
                            return;

                        /*
                        o.current++;
                        o.current %= o.totalCount() + 1;
                        o.marginLeft = o.current * o.width * -1;
                        */

                        $wrap.css({ marginLeft: 0 });

                        var $img = $wrap.find('img:eq(0)');
                        $wrap.find('img:eq(0)').remove();
                        $wrap.append($img);

                        $wrap.find('img').css({
                            cursor: 'pointer'
                        }).click(function () {
                            //alert( $(this).attr('src') );                            
                            //w.location.href = $(this).attr('src');

                            event.preventDefault();
                        });
                    });
                }, 3000);

            }
        });
    };
})(jQuery);
