/*
author : 이주용
version : 09.03.31
comment : 문자열의 null 값 체크
*/
String.prototype.isEmpty  = function(){
	return ( ( this == null ) || ( this.trim().length == 0 ))
};


/*
author : 이주용
version : 09.03.31
comment : 문자열의 왼쪽을 기준으로 length만큼 char로 치환
*/
String.prototype.padLeft = function ( length, char ){	
	if ( ( (! length ) || ( length < 1 ) ) || ( this.isEmpty() ) )
		return this;

	var padding = '                                                                                                    ';

	if ( char )
	{	
		padding = padding.replace( / /g, char );
	}
	return ( padding.substr ( 0, length ) + this );
};

/*
 * author  : 이주용
 * version : 09.03.20
 * comment : 문자열에서 공백을 제거한다. 
 */
String.prototype.trim = function(){
	return this.replace(/^\s*|\s*$|/gi, '');
};

/*
 * author  : 이주용
 * version : 09.03.20
 * comment : 문자열을 추가한다.
 */
String.prototype.append = function(str){
	return this + str;
};


/*
 * author  : 이주용
 * version : 09.03.23
 * comment : 문자열을 추가하고 줄내림(\n)을 추가한다. 
 */
String.prototype.appendLine = function(str){

	if(!str.isEmpty())
		return this + str + "\n";
	return this;
};

/*
 * author : 이주용
 * version : 09.03.25
 * comment : 대소문자를 구분하지 않고 특정 문자열의 위치 검색(indexOf 와 비교)
 */
String.prototype.searchString = function(str){
	var thisTemp = this.toLowerCase();
	var strTemp = str.toLowerCase();
	
	return thisTemp.indexOf(strTemp);
};

String.prototype.isContain = function(str){
	return this.searchString(str) != -1;
};


/*
 * author : 이주용
 * version : 09.03.25
 * comment : 문자열에서 특정 문자(str)이 반복되어 나오는 횟수반환
 */
String.prototype.countString = function(str){
	var count = 0;
	var pos = this.indexOf(str, 0);
	
	while(pos != -1){
		count++;
		pos = this.indexOf(str, pos+1);
	}	
	return count;
};

/*
 * author : 이주용
 * version : 09.03.25
 * comment : 경로를 가지는 문자열에서 확장자 부분을 반환한다.
 */
String.prototype.getExtension = function(){
	var pos = this.lastIndexOf(".");
	
	return this.substring(pos+1);
};

/*
 * author : 이주용
 * version : 09.03.25
 * comment : 파일명을 반환한다.
 */
String.prototype.getFileName = function(){
	var pos = this.lastIndexOf("/");
	return this.substring(pos+1);
};

String.prototype.isForbid = function(forbiden){
	// 모든 태그 제거
	var source = this.replace(/(<[//]?[^>]+>)/gi,'');		
	var pattern = eval('/' + forbiden + '/gi');
	
	return pattern.test(source);
};

/*
 * author : 이주용
 * version : 09.06.15
 * comment : 숫자만 반환한다.
 */
String.prototype.num = function(){
	return (this.trim().replace(/[^0-9]/g,""));
};


Array.prototype.contains = function(element){
	for(var i=0; i < this.length; i++){
		if(this[i] == element){
			return true;
		}
	}
	return false;
};

Array.prototype.removeVal = function(val){
	for(var i=0; i < this.length; i++){
		if(this[i] == val){
			this.splice(i,1);
			return i;
		}
	}
	return -1;
};

Array.prototype.remove = function(index)
{
	var temp = new Array();
	var i = this.length;
	
	while(i > index){
		var item = this.pop();
		temp.push(item);
		i--;
	}
	
	for(var i=temp.length-2; i >= 0;i--){
		this.push(temp[i]);
	}
};


ES = {
    /*키보드 이벤트방지*/
    PreventKeyEvent: function() {
        var evt = event;

        var node = (evt.target != null) ? evt.target : ((evt.srcElement != null) ? evt.srcElement : null);

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
    GetCutTitle : function(s,max){
        var checkByte = 0;
        var subStrLength = 0;
        
        var hangul = max / 2;
        var english = max;
        var ret = false;
        
        var returnString = "";
        
        for(var i = 0; i < s.length; i++)
        {
            var lastByte = 0;
            var oneChar = s.charAt(i);
            
            returnString += oneChar + "";
            
            if(escape(oneChar).length > 4)
            {
                checkByte += 2;
                lastByte = 2;
            }
            else
            {
                checkByte++;
                lastByte = 1;
            }
            
            if(checkByte <= max)            
            {
                subStrLength = i + 1;
            }
            else
            {
                checkByte = checkByte - lastByte;
                ret = true;
                returnString += "...";
                break;
            }
        }
    
        return returnString;
    },
    MaxLengthCheck : function(s,max){
        var checkByte = 0;
        var subStrLength = 0;
        
        var hangul = max / 2;
        var english = max;
        var ret = false;
        
        for(var i = 0; i < s.length; i++)
        {
            var lastByte = 0;
            var oneChar = s.charAt(i);
            
            if(escape(oneChar).length > 4)
            {
                checkByte += 2;
                lastByte = 2;
            }
            else
            {
                checkByte++;
                lastByte = 1;
            }
            
            if(checkByte <= max)            
            {
                subStrLength = i + 1;
            }
            else
            {
                checkByte = checkByte - lastByte;
                ret = true;
                break;
            }
        }
    
        return {success : ret, current: checkByte}; 
    },
    
    Element : {
        Show : function(c){
            jQuery(c).show();
        },
        Hide : function(c){
            jQuery(c).hide();
        }
    }
};

document.onkeydown = ES.PreventKeyEvent; 


function MM_swapImgRestore() { //v3.0

  var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;

}

function MM_preloadImages() { //v3.0

  var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();

    var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)

    if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}

}



function MM_findObj(n, d) { //v4.01

  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {

    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}

  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];

  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);

  if(!x && d.getElementById) x=d.getElementById(n); return x;

}



function MM_swapImage() { //v3.0

  var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)

   if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}

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

/*
* author : 이주용
* version : 09.03.23
* comment : onkeypress, onkeyup 시 숫자 값 체크
*/
function fn_OnkeyNumber() {
    if ((event.keyCode < 48) || (event.keyCode > 57)) {
        event.returnValue = false;
    }
}

function fn_OnkeyNumberAndMinus() {
    if (event.keyCode != 45 && (event.keyCode < 48) || (event.keyCode > 57)) {
        event.returnValue = false;
    }
}

/*
* author : 이주용
* version : 09.03.23
* comment : obj컨트롤 값에 ',' 적용하여 숫자형태로 보여줌
*/
function fn_ApplyComma(obj) {

    var match = false;
    var original = '';
    
    if(typeof obj == 'object'){
        if(obj != null)
            original = obj.value;
    }
    else if(typeof obj == 'number'){
        original = obj.toString();
    }
    else
        original = obj;

    if(original == '')
        return '';
        
    var flag = '';
    var number = original;

    if (original.indexOf('-') > -1) {

        for (i = 0; i < original.length; i++) {
            if (original.charAt(0) == "-") {
                match = true;
            }
        }

        original = original.replace(/-/gi, '');

        if (match) 
        {
            flag = "-";
        }

        number = original;
    }
    

    if (number.indexOf(',') > -1) {
        number = number.split(',').join('');
    }

    var result = flag + '' + fn_ApplyCommaString(number); 
    
    if(typeof obj == 'object')
        obj.value = result;
        
    return result;
}

/*
* author : 이주용
* version : 09.03.23
* comment : 문자열(str)에서 ',' 문자 제거하여 숫자값으로 반환
*/
function fn_RemoveCommaValue(str) {
    var sum = '';
    var c = '';

    for (var i = 0; i < str.length; i++) {
        c = str.charAt(i);
        if (c != ',') {
            sum = sum + c;
        }
        else {
            c = '';
            sum = sum + c;
        }
    }

    return sum;
}

/*
* author : 이주용
* version : 09.03.23
* comment : 3자리 수마다 ',' 문자를 삽입하여 콤마를 가지는 문자열 형태로 반환
*/
function fn_ApplyCommaString(n) {
    var origin = n.toString();
    var num1, num2 = '';

    if (origin.indexOf(".") > -1) {
        num1 = origin.split('.')[0];
        num2 = origin.split('.')[1];
    }
    else {
        num1 = origin;
    }

    if (num1 == '')
        return '';

    n = Number(num1);
    var x = '';
    var sum = '';
    str = n + '';
    for (var i = str.length; i > 0; i--) {
        c = str.charAt(str.length - i);
        if (i % 3 == 0 && (str.length != i)) { sum = sum + ','; }
        sum = sum + c;
    }

    if (num2 == '')
        return sum;
    else
        return sum + '.' + num2;
}


function setCookie(name, value, expiredays) {
    var todayDate = new Date();
    todayDate.setDate(todayDate.getDate() + expiredays);
    document.cookie = name + "=" + escape(value) + "; path=/; expires=" + todayDate.toGMTString() + ";"
}

function getCookie(name) {
    var nameOfCookie = name + "=";
    var x = 0;
    while (x <= document.cookie.length) {
        var y = (x + nameOfCookie.length);
        if (document.cookie.substring(x, y) == nameOfCookie) {
            if ((endOfCookie = document.cookie.indexOf(";", y)) == -1)
                endOfCookie = document.cookie.length;
            return unescape(document.cookie.substring(y, endOfCookie));
        }
        x = document.cookie.indexOf(" ", x) + 1;
        if (x == 0)
            break;
    }
    return "";
}



/*
	* author : 이주용
	* version : 09.09.04
	* comment : cellIdx 값을 기준으로 수직으로 merge
	param
	table : 리스트 객체, 또는 ID
	startRowIdx : 머지를 실행할 row의 Index
	cellIdx : 머지 대상 cell Index
	*/
function mergeVerticalCell(table, startRowIdx, cellIdx){

    
	if(typeof(table) == 'String')
		table = document.getElementById(table);

	var rows = table.getElementsByTagName('tr');
	var numRows = rows.length;
	var numRowSpan = 1;
	var currentRow = null;
	var currentCell = null;
	var currentCellData = null;
	var nextRow = null;
	var nextCell = null;
	var nextCellData = null;

	for(var i = startRowIdx; i < (numRows-1); i++){
		if(numRowSpan <= 1){
			currentRow = table.getElementsByTagName('tr')[i];
			currentCell = currentRow.getElementsByTagName('td')[cellIdx];
			currentCellData = getCellValue(currentCell);
		}

		if(i < numRows - 1){
			if(table.getElementsByTagName('tr')[i+1]){
				nextRow = table.getElementsByTagName('tr')[i+1];
				nextCell = nextRow.getElementsByTagName('td')[cellIdx];
				nextCellData = getCellValue(nextCell);
			}

			if(currentCellData == nextCellData){
				numRowSpan += 1;
				currentCell.rowSpan = numRowSpan;
				nextCell.style.display = 'none';
			}else{
				numRowSpan = 1;
			}
		}
	}
}

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
function mergeDependentVerticalCell(table, startRowIdx, basicCellIdx, cellIdx){

	if(typeof(table) == 'String')
		table = document.getElementById(table);
		
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

	for(var i = startRowIdx; i < (numRows - 1); i++){

		if(i == startRowIdx || (countLoopInBasicMerge == 1)){
			basicRowSpan = table.getElementsByTagName('tr')[i].getElementsByTagName('td')[basicCellIdx].rowSpan;
		}
		if(numRowSpan <= 1){
			currentRow = table.getElementsByTagName('tr')[i];
			currentCell = currentRow.getElementsByTagName('td')[cellIdx];
			currentCellData = getCellValue(currentCell);
		}

		if(i < numRows - 1){
			if(countLoopInBasicMerge < basicRowSpan)
			{
				if(table.getElementsByTagName('tr')[i+1]){
					nextRow = table.getElementsByTagName('tr')[i+1];
					nextCell = nextRow.getElementsByTagName('td')[cellIdx];
					nextCellData = getCellValue(nextCell);

					if(currentCellData == nextCellData){
						numRowSpan += 1;
						currentCell.rowSpan = numRowSpan;
						nextCell.style.display = 'none';
					}
					else
					{
						numRowSpan = 1;
					}
				}

				countLoopInBasicMerge++;
			}
			else{
				countLoopInBasicMerge = 1;
				numRowSpan = 1;
			}
		}
	}
}

/*
* author : 이주용
* version : 09.09.04
* comment : cell 값 가져오기
*/
function getCellValue(cell){
	var cellText = '';
	
	if(typeof(cell) == 'undefined')
		return cellText;

	if( typeof(cell.tagName) == 'undefined' ){
		cellText = cell.data;
	}
	else{
		cellText = cell.innerText;
	}
	return cellText;
}



function fnGetValueWithClassName(className, separator, range){
    var val = "";
    $('.'+className, range).each(function(){
        val += $(this).val() + separator;
    });
    return val.substr(0, val.length-1);
}

function fnGetSelectedRadioButtonValue(name, range){
    return $('input[name="'+name+'"]:checked',range).val();
}


/*
    작성자 : 이주용
    작성일 : 2011-08-16
    용  도 : 동적 DB페이징에 사용되는 Pager
*/
; (function($) {
    $.fn.HNSSPager = function(options) {

        var defaults = {
            currentPage: 1,
            totalRow: 10,
            showButtonCount: 10,
            rowPerPage: 10
        };

        var settings = $.extend({}, defaults, options);
        var numPages = Math.ceil(settings.totalRow / settings.rowPerPage);

        var av = settings.showButtonCount / 2;

        var start = 1;
        var end = numPages;

        if (numPages > settings.showButtonCount) {
            if (settings.currentPage - av > 0) {
                start = settings.currentPage - av;
                end = start + settings.showButtonCount - 1;
            }
            else {
                start = 1;
                end = settings.showButtonCount;
            }
        }

        return this.each(function() {
            generatePager($(this));
            $('li a').click(function() {
                settings.callback($(this).attr('num'));
            });
        });

        function generatePager($obj) {
            $('ul.pager').remove();

            var $pager = $("<ul class='pager'/>");
            
            if(settings.currentPage > 1){
                $('<li><a href="#" num=1>first</a></li>').appendTo($pager);
                $('<li><a href="#" num='+(Number(settings.currentPage) - 1)+'>prev</a></li>').appendTo($pager);
            }

            for (var page = start; page <= numPages && page <= end; page++) {
                if (settings.currentPage == page) {
                    $('<li>' + page + '</li>').appendTo($pager);
                }
                else {
                    $('<li><a href="#" num='+ page+'>' + page + '</a></li>').appendTo($pager);
                }
            }
            
            if(settings.currentPage < numPages){
                $('<li><a href="#" num='+(Number(settings.currentPage) + 1)+'>next</a></li>').appendTo($pager);
                $('<li><a href="#" num=' + numPages+ '>last</a></li>').appendTo($pager);
            }

            $obj.append($pager);
        }
    };
})(jQuery); 


/*
    작성자 : 이주용
    작성일 : 2011-08-12
    용  도 : 상세pop div를 화면의 중앙에 배치
*/
;(function($){
    $.fn.MoveCenter = function(options){
    
        var left = ( $(window).scrollLeft() + ($(window).width() - $(this).width()) / 2) + 'px';
        //var top = Math.abs( ( $(window).scrollTop() + ($(window).height() - $(this).height()) / 2)) + 'px';
	    var top = Math.abs( ( ($(window).height() - $(this).height()) / 2)) + 'px';

        $.fn.MoveCenter.defaults.left = left;
        $.fn.MoveCenter.defaults.top = top;
        
        var opts = $.extend({}, $.fn.MoveCenter.defaults, options);
        
        return this.each(function(){
            $(this).css({
                'position'              : opts.position,
                'z-index'               : opts.zIndex,
                'background-color'      : opts.backgroundColor,
                'left'                  : opts.left,
                'top'                   : opts.top
            }); 

            //$('body').append($(this)); // IE7의 경우 div의 위치에 따라 position:fix속성 문제 발생
        });
    };
    
    $.fn.MoveCenter.defaults = {
        zIndex: 1000,
        position: 'fixed',
        backgroundColor: '#fff',
        left: 0,
        top: 0
    };
})(jQuery);

;(function($){
    
    $.fn.ValidRequired = function(options){
        var msg = "";
        this.each(function(){
             msg = __fnChkRequired( $(this) );
        });

        return msg;
    };

})(jQuery);

;(function($){
  
   $.fn.rowspan = function(colIdx) {
    
   return this.each(function(){
     var that;
     $('tr', this).each(function(row) {

      $('td:eq('+colIdx+')', this).each(function(col) {
        
          if ( $.trim($(this).text()) == $.trim($(that).text())) {
            rowspan = $(that).attr("rowSpan");
            if (rowspan == undefined) {
       
              $(that).attr("rowSpan",1);
              rowspan = $(that).attr("rowSpan");
            }
            rowspan = Number(rowspan)+1;
            $(that).attr("rowSpan",rowspan); // do your action for the colspan cell here
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



/*
    작성자 : 이주용
    작성일 : 2013-02-07
    용  도 : 갤러리

    사용법
    $('#gallery').gallery({enableCount:3, data:['http://zerotips.com/wp-content/uploads/2012/02/Mixed-Fruits-15-AC996402V2-1600x1200.jpg',
						'https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQGfTjkvbpR7Wz_RXnmsBdxx5VIDpTmBdvcc4PZNwEXD2lk5Acuow',
						'http://www.edgarsfruit.co.uk/pages/about_2_176868501.jpg',
						'http://cfs7.tistory.com/upload_control/download.blog?fhandle=YmxvZzU1OTQ5QGZzNy50aXN0b3J5LmNvbTovYXR0YWNoLzAvMi5qcGc%3D']});
*/
;(function($){
	$.fn.gallery = function(opts){
		var o = $.extend(
			{
				enable: true,
				width: 100,
				height: 100,
				data : [],
				text : 'test',
				current : 0,
				marginLeft : 0,
				totalCount : function(){
					return this.data.length;
				}
			},opts);

		return this.each(function(){
			var $this = $(this);
			var $wrap = $('<div/>');

			$.map( o.data, function(n,i){
				$wrap.append('<img src="' + n + '"/>') ;
			} );

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
				containment : 'window',
				cursor: 'move'
			});


			if(o.enableCount < o.totalCount()){

				setInterval(function(){
					$wrap.animate({
						marginLeft: o.width * -1
					},1000, function(){

						if(!o.enable)
							return;

						/*
						o.current++;
						o.current %= o.totalCount() + 1;
						o.marginLeft = o.current * o.width * -1;
						*/

						$wrap.css({marginLeft : 0});

						var $img = $wrap.find('img:eq(0)');
						$wrap.find('img:eq(0)').remove();
						$wrap.append($img);

						$wrap.find('img').css({
							cursor: 'pointer'
						}).click(function(){
							//alert( $(this).attr('src') );
							//var w = open('about:blank');
							//w.location.href = $(this).attr('src');

							event.preventDefault();
						});
					});
				}, 3000);

			}
		});
	};	
})(jQuery);


/*
    작성자 : 이주용
    작성일 : 2011-08-11
    용  도 : 상세팝업 생성 및 감추기
*/
$(document).ready(function(){
    
    $('span[rel^="#op-"]').click(function() {
        var openPanelID = $(this).attr('rel').replace(/#op-/gi, '');
        var $panel = $('#' + openPanelID);        
        
        __fnAppendScreenBlock();
        __fnShowPop($panel);
    });
    
    $('span[rel^="#cl-"]').click(function() {        
        var openPanelID = $(this).attr('rel').replace(/#cl-/gi, '');
        var $panel = $('#' + openPanelID);
                
        __fnHidePop($panel);
    });
    
    $('span[rel]').css('cursor','hand');
    
    $('div[id^="ucPop"]').hide();
});

/*client screen영역 전체를 덮는 div를 생성한다.*/
function __fnAppendScreenBlock()
{
    $('div.screen3').remove();    
    $('body').append('<div class="screen"></div>');
    $('div.screen').show();
}

function __fnAppendScreenBlock2()
{
    $('div.block').remove();
    $('body').append('<div id="block" class="screen block"></div><div id="block-msg" class="block"><img src="/common/images/img/loading_bar.gif" title="조회중" alt="조회중"/></div>');
    $('div#block').show();
}

function __fnES_Begin(){
    __fnAppendScreenBlock2();
    __fnShowPop($('#block-msg'));
}

function __fnES_End(){
    $('div.block').remove();
}


/*$pop을 화면의 정중앙에 위치시킨다.*/
//function __fnShowPop($pop){
//    $pop.MoveCenter({backgroundColor:"#fff"}).show();
//}

function __fnShowPop($pop,opts){
    var settings = $.extend({}, {backgroundColor:"#fff"}, opts);
    $pop.MoveCenter(settings).show();
}


/*$pop요소를 화면에서 감춘다.*/
function __fnHidePop($pop){
    $('div.screen').hide();
    $pop.hide();
}

function __fnInitValue($container){
    $('input, textarea, select, span[data]', $container).each(function(i, e) {
        if (e.type == 'select-one')
            e.selectedIndex = 0;
        else if(e.type == 'span')
            e.innerHTML = '';
        else
        {   
            $(this).val('');
        }
    });
}

/*체크박스 중 name요소가 name요소를 가지면 체크되어 있는 모든 element의 값을 '@'로 연결하여 반환한다.*/
function __fnSelectedCheckedValue(name){
    var val = '';
    var delimiter = '@';

    $('input:checkbox:not(:disabled)[name$="'+name+'"]:checked').each(function (i, e) {
        val += $(e).val() + delimiter;
    });

    if(val != ''){
        val = val.substring(0, val.length - 1);
    }

    return val;
}

function __fnSelectedRadioValue(name){
    var val = '';
    $("input:radio:not(:disabled)[name$='"+name+"']:checked").each(function(i,e){
        val = $(e).val();    
    });
    
    return val;
}


function __fnChkRequired($container) {
    var info = [];
    var msg = '';

    $('input.required, textarea.required, select.required', $container).each(function(i, e) {
        var $ctl = $(this);
        
        if(e.type == 'select-one' && e.value == ''){
            info[info.length] = {
                'msg': '- ' + $ctl.attr('vmsg') + "\n"
            };
        }
        else
        {
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


// 숫자타입 입력
$.fn.NumberInput = function() {
    $(this).bind("keydown", function(e) {
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
$.fn.NumberLimitDigitPoint = function(digit) {
    $(this).bind("keyup", function(e){
        var i;
        var t = $(this).val();
        var k = '0123456789.';
        var flag = false;
        
        if(digit == undefined)
            digit = $(this).attr('digit');
            
        digit = Number(digit);
        
        for(i=0; i < t.length; i++){
            if(k.indexOf(t.substring(i,i+1)) < 0){
                flag = true;
            }
        }
        
        if(flag){
            alert('숫자만 입력가능합니다.');
            $(this).val('');
            return;
        }
        
        if(t.indexOf('.') != -1){
            var t_length = t.substring(t.indexOf('.') + 1);
            if(t_length.length > digit){
                alert('소수점 '+digit+'자리까지만 입력됩니다.');
                $(this).val(t.substring(0, t.indexOf('.') + 1 + digit));
                return false;
            }
        }
        
        return flag;
    });
};

/// 문자열 바이트 길이 제한
$.fn.CheckByte = function(maxByteLength) {
    $(this).blur(function(e) {
        if (ES.MaxLengthCheck($(this).val(), maxByteLength).success) {
            $(this).val($(this).val().substring(0, maxByteLength));
        }
    });
};


// 테이블 헤더고정
$.fn.fixTableHeader = function(opts) {

    var defaults = {
            tableClass : "tb3"
        };

    var settings = $.extend({}, defaults, opts);

    
    var $div = $(this);
    var $header = $('table tr:first', $div);
    var $headerDiv = $('<div/>');
    var $headerTable = $('<table class="'+settings.tableClass+'"/>');
    $headerTable.append($header.clone());

    $('td', $headerTable).each(function(i) {
        $(this).css('width', $('th:eq(' + i + ')', $header).width());
    });

    $headerDiv.append($headerTable);
    $headerDiv.insertBefore($div);

    $headerTable.css('width', $('table', $div).width());
    $headerDiv.css('width', $div.width());
    $header.hide();
};

Date.prototype.toFormatString = function(fmt) {

    if (!this.valueOf()) return "";
 
    var dt = this;
    return fmt.replace(/(yyyy|yy|mm|dd|hh|hh24|mi|ss|am|pm)/gi,
        function($1){
            switch ($1){
                case 'yyyy': return dt.getFullYear();
                case 'yy':   return dt.getFullYear().toString().substr(2);
                case 'mm': 
                        var mm = '';
                        if( dt.getMonth() < 10)
                        mm  = '0' +  (dt.getMonth()+1) ;
                            
                  return mm;
                case 'dd':   return dt.getDate();
                case 'hh':   return (h = dt.getHours() % 12) ? h : 12;
                case 'hh24': return dt.getHours();
                case 'mi':   return dt.getMinutes();
                case 'ss':   return dt.getSeconds();
                case 'am':   return dt.getHours() < 12 ? 'am' : 'pm';
                case 'pm':   return dt.getHours() < 12 ? 'am' : 'pm';
            }
        } 
    );
}


function setCookie(name, value, expiredays) {
            var todayDate = new Date();
            todayDate.setDate(todayDate.getDate() + expiredays);
            document.cookie = name + "=" + escape(value) + "; path=/; expires=" + todayDate.toGMTString() + ";"
}

function fnClosePopupToday(objDiv) {
            setCookie(objDiv, "done", 1); 		//마지막 파라미터 1을 2로 고치면 2일 동안 안보임.
            fnClosePopup(objDiv);
}

function fnClosePopup(objDiv) {
            jQuery("#" + objDiv).hide();
}

function fnSetPopup() {
var jPopupContainer = jQuery("#divPopupContainer");

if(jPopupContainer.parent().offset() != null)
{
    var left = jPopupContainer.parent().offset().left;
    jPopupContainer.css({
        position: "absolute",
        top: 0,
        left: left + 120
    });

    var jPopupList = jPopupContainer.children("div");
    var cookiedata = document.cookie;
    var popupCnt = 0;

    jPopupList.each(function() {
        if (cookiedata.indexOf(jQuery(this).attr("id") + "=done") < 0) {
            jQuery(this).show();
            jQuery(this).css({
                position: "absolute",
                left: popupCnt * 100,
                "float": "left"
            })
            popupCnt++;
//                    jQuery(this).click(function() {
//                        jPopupList.css("z-index", 1);
//                        jQuery(this).css("z-index", jPopupList.length);

//                    });
        }
        else {
            jQuery(this).hide();
        }
    });
}
}


function SelectAllChildNodes() 
{
    var obj = window.event.srcElement; 
    var treeNodeFound = false;

    var checkedState; 
    if (obj.tagName == "INPUT" && obj.type == "checkbox") 
    {
        var treeNode = obj; 
        checkedState = treeNode.checked;
        do
        {
            obj = obj.parentElement;
        } while (obj.tagName != "TABLE") 
        
        var parentTreeLevel = obj.rows[0].cells.length;            
        var parentTreeNode = obj.rows[0].cells[0]; 
        var tables = obj.parentElement.getElementsByTagName("TABLE");
        var numTables = tables.length;
        if (numTables >= 1) 
        {
            for (iCount=0; iCount < numTables; iCount++) 
            {
                if (tables[iCount] == obj) 
                {
                    treeNodeFound = true; 
                    iCount++;
                    if (iCount == numTables) 
                    {
                        return; 
                    }
                }
                if (treeNodeFound == true) 
                {
                    var childTreeLevel = tables[iCount].rows[0].cells.length;
                    if (childTreeLevel > parentTreeLevel) 
                    {
                        var cell = tables[iCount].rows[0].cells[childTreeLevel - 1];
                        var inputs = cell.getElementsByTagName("INPUT"); 
                        inputs[0].checked = checkedState;
                    }
                    else
                    {
                        return; 
                    }
                }
            }
        }
    }
}
    
    
// 날짜 형식인지 체크 한다.
String.prototype.isDate = function(){
	var dateRegEx = /^(\d{4})(\/|-)(\d{1,2})(\/|-)(\d{1,2})$/;
    var matchArray = dateRegEx.exec(this); // is the format ok?
    if (matchArray == null) return false;
	var year = matchArray[1]; 
    var month = matchArray[3];
    if(month.charAt(0)==0) month = month.charAt(1);
    var day = matchArray[5];
    if(day.charAt(0)==0) day = day.charAt(1);
    if (month < 1 || month > 12) return false; // check month range
    if (day < 1 || day > 31) return false; //check day range
    if ((month==4 || month==6 || month==9 || month==11) && day==31) {
        return false;
    }
    if (month == 2) { // check for february 29th
        var isleap = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));
        if (day>29 || (day==29 && !isleap)) {        
            return false;
        }
    }
    return true;
}
	
// 값이 널 값인지 체크 한다.
function NullCheck(value) {

    if (value != undefined && value != '') {
        return true;
    }
    else {
        return false;
    }
}


function fnGetQueryString(uri,key) {
    var q = '', val='';

    if(uri.indexOf('?') > -1)
        q = uri.split('?')[1];

    if(q.indexOf('#') > -1)
        q = q.split('#')[0];

    var items = q.split('&');

    for (i = 0; i < items.length; i++) {
        if (items[i].indexOf(key) > -1) {
            var arr = items[i].split('=');
            val = arr[1];
            break;
        }
    }
    return val;
}

// 시작페이지 설정
function fnSetStart(){
    document.body.style.behavior = 'url(#default#homepage)';
    document.body.setHomePage('http://' + document.location.host);
}

// 즐겨찾기 페이지 설정
function fnAddFavorite(){
    window.external.AddFavorite('http://' + document.location.host, '건축자재포탈 페어몰');
}

function fnOpenWin(url, name, left, top, width, height) {
    //open(url, name, 'scrollbars=yes,toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,left=' + left + ',top=' + top + ',width=' + width + ',height=' + height);
    open(url, name, 'scrollbars=yes,toolbar=yes,resizable=yes,left=' + left + ',top=' + top + ',width=' + width + ',height=' + height);
}



var es = es || {};

es.globalpop = es.globalpop || {
    coverScreen: function() {
        $('div.block').remove();
        $('body').append('<div id="block" class="block es-screen"></div><div id="block-msg" class="block"><img src="/common/images/img/loading_bar.gif" alt="" /></div>')
        $('div#block').show();
    },
    coverScreen2: function () {
        $('div.block').remove();
        $('body').append('<div id="block" class="screen block"></div><div id="block-msg" class="block"><img src="/loading_bar.gif" title="조회중" alt="조회중"/></div>');
        $('div#block').show();
    },
    start: function() {
        this.coverScreen2();
        this.show($('#block-msg'), { animationOn: false });
    },
    end: function() {
        $('div.block').remove();
    },

    show: function($pop, opts) {
        var settings = $.extend({}, { backgroundColor: "#fff" }, opts);
        $pop.MoveCenter(settings).show();

        if (typeof (settings.parentPop) != "undefined") {
            var $parentPop = $('#' + settings.parentPop);
            $parentPop.css('z-index', 999);
        }
    }
};

es.fn = es.fn || {
    stack: [],
    add: function(f) {
        this.stack[this.stack.length] = f;
    },
    run: function() {
        for (var i = 0; i < this.stack.length; i++) {
            this.stack[i]();
        }
    }
};






/* 입력 값 체크 */
es.valid = es.valid || {

    isOverLength: function(s, max) {
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

    getOverAndLength: function(s, max) {
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
    isValidLocalPhone: function(phoneNo) {
        return es.regex.PhoneDigit2.test(phoneNo);
    },

    /*주민번호 체크*/
    isValidJumin: function(juminNo) {
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
    isValidBusinessNo: function(bizNo) {
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
    isValidCorporationNo: function(corpoNo) {
        var re = /-/g;
        sRegNo = corpoNo.replace(re, '');

        if (sRegNo.length != 13) {
            return false;
        }

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

        return true
    },

    /*$selector에 포함된 input,textarea,select 요소 중 클래스명으로 required를 가지는 element의 값을 체크*/
    isRequired: function($selector) {
        var info = [];
        var msg = '';

        $('input.required, textarea.required, select.required', $selector).each(function(i, e) {
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




/* 문자열 포맷 관련 */
es.format = es.format || {

    /*소수점을 포함한 숫자형 문자열을 반환한다.*/
    getOnlyNumeric: function(s) {
        s += '';
        return s.replace(/[^\d\.-]/g, '');
    },

    /*정수 반환*/
    getOnlyInteger: function(s) {
        s += '';
        var num = this.getOnlyNumeric(s);

        if (num == '')
            num = 0;

        return parseInt(num, 10);
    },

    /*소수 반환*/
    getOnlyFloat: function(s) {
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
    getCutContent: function(s, maxlength, appendString) {

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
    getLocalPhoneNumber: function(phoneNo) {

        var ret = "형식이 잘못되었습니다.";

        if (es.regex.PhoneDigit2.test(phoneNo)) {
            ret = phoneNo.replace(es.regex.PhoneDigit2, "$1-$2-$3");
        }
        return ret;
    },

    getYYYY_MM_DD: function(s) {
        s = s + '';

        if (s.length == 8) {
            s = s.substr(0, 4) + '-' + s.substr(4, 2) + '-' + s.substr(6, 2);
        }

        return s;
    },

    /* 사업자번호 형태를 반환한다.*/
    getBusinessNo: function(s) {
    
        s = s.replace(/[^\d]/g, '');
        if (es.valid.isValidBusinessNo(s)) {
            s = s.replace(/([\d]{3})([\d]{2})([\d]{5})/, '$1-$2-$3');
        }
        //        else {
        //            s = "잘못된 사업자번호 입니다.";
        //        }
        
        return s;
    },
    /* 문자열을 3자리로 잘라 ","로 구분한다 */
    getNumberFormat: function(o) {

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

                origin = origin.replace(/-/, '');
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

    getTwoDigit: function(num) {
        num = parseInt(num, 10);
        return num < 10 && num >= 0 ? '0' + num : '' + num;
    },

    yyyyMMddConvertToDate: function(yyyyMMdd) {

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

    getPhoneNo_Hipen: function(s) {

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