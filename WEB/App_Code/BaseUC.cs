using ESNfx3.Web.Page;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace WEB.App_Code
{
    /// <summary>
    /// BaseUC의 요약 설명입니다.
    /// </summary>
    public abstract class BaseUC : UserControl
    {
        #region variable
        protected int _totalCount = 0;
        protected string _esListHtml = string.Empty;
        protected bool _isExcel = false;
        //private Dictionary<int, string> _esLists = new Dictionary<int, string>();
        private Dictionary<int, string> _esLists
        {
            get
            {
                var esLists = ViewState["ESLISTS"] as Dictionary<int, string>;

                if (esLists == null)
                    esLists = new Dictionary<int, string>();

                return esLists;
            }
        }

        #endregion

        #region Property
        public BasePage BasePage
        {
            get
            {
                return this.Page as BasePage;
            }
        }

        /// <summary>
        /// 리스트 데이터
        /// </summary>
        protected Dictionary<int, string> ESLists
        {
            get
            {
                return _esLists;
            }
        }

        protected ViewData PageViewData
        {
            get
            {
                return BasePage.PageViewData;
            }
        }


        protected ViewData ViewData
        {
            get
            {
                return BasePage.ViewData;
            }
        }
        #endregion

        #region Constructor
        public BaseUC() { }
        #endregion

        #region Override
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            //CheckLogin();
            NewMovablePopSetting();
            FirstSetting();
        }
        #endregion

        #region Method
        //private void CheckLogin()
        //{
        //    if (BasePage is UserPage && BasePage.LoginUser == null)
        //    {
        //        (BasePage as UserPage).RedirectToLoginUrl();
        //    }
        //}

        protected void SetESListsWithIndex(int index, string result)
        {
            //_esLists[index] = result;
            var temp = _esLists;
            temp[index] = result;
            ViewState["ESLISTS"] = temp;
        }

        /// <summary>
        /// 휴대폰번호 형식 반환
        /// </summary>
        /// <param name="mobileNo"></param>
        /// <returns></returns>
        protected string GetFormatMobileNo(string mobileNo)
        {
            if (mobileNo.Length == 11)
            {
                mobileNo = mobileNo.Substring(0, 3) + "-" + mobileNo.Substring(3, 4) + "-" + mobileNo.Substring(7, 4);
            }
            else if (mobileNo.Length == 10)
            {
                mobileNo = mobileNo.Substring(0, 3) + "-" + mobileNo.Substring(3, 3) + "-" + mobileNo.Substring(6, 4);
            }

            return mobileNo;
        }

        #endregion

        #region Virtual Method
        // 컨트롤 설정 및 초기화 셋팅
        protected virtual void FirstSetting() { }

        // Layer Pop 설정
        protected virtual void NewMovablePopSetting() { }

        // 기본 정보 바인딩
        protected virtual void ListBind() { }
        #endregion
    }

    /// <summary>
    /// es.pop 객체를 사용하기 위한 Server 객체
    /// </summary>
    public abstract class BasePop : BaseUC
    {
        protected string _containerCSS = "pop_wrap";
        protected string _openScript = string.Empty;

        #region property

        protected Dictionary<string, NewMovablePop> NMPops = new Dictionary<string, NewMovablePop>();

        public int? PopWidth { get; set; }

        public string OpenScript
        {
            get
            {
                return _openScript;
            }
        }

        /// <summary>
        /// 식별아이디
        /// </summary>
        protected string PopID
        {
            get;
            set;
        }

        public string PopOption
        {
            get;
            set;
        }

        /// <summary>
        /// 팝업타이틀
        /// </summary>
        public string PopTitle { get; set; }

        /// <summary>
        /// 부모팝업아이디
        /// </summary>
        public string ParentPopID { get; set; }

        /// <summary>
        /// 조회버튼을 클릭여부
        /// </summary>
        public bool IsRealSearch { get; set; }
        #endregion

        public string ContainerCSS
        {
            get
            {
                return _containerCSS;
            }
            set
            {
                _containerCSS = value;
            }
        }
        #region protected method
        /// <summary>
        /// UserControl을 화면에 보여준다.
        /// </summary>
        /// <param name="upnlLIst"></param>
        /// <param name="additionScript"></param>
        protected void UCPopOpenScript(Control upnlLIst, string additionScript)
        {
            additionScript = additionScript.TrimEnd(';');

            if (!string.IsNullOrEmpty(additionScript))
                additionScript += ";";

            if (string.IsNullOrEmpty(ParentPopID))
                ScriptManager.RegisterStartupScript(upnlLIst, upnlLIst.GetType(), string.Format("open{0}", PopID), string.Format("es.pop.coverScreen();es.pop.loadLayer($('#{0}'),{{ {1} }});{2}", PopID, PopOption, additionScript), true);
            else
            {
                PopOption += string.Format("parentPop:'{0}'", ParentPopID);
                ScriptManager.RegisterStartupScript(upnlLIst, upnlLIst.GetType(), string.Format("open{0}", PopID), string.Format("es.pop.coverScreen();es.pop.loadLayer($('#{0}'),{{ {1} }});{2}", PopID, PopOption, additionScript), true);
            }
        }

        /// <summary>
        /// UserControl을 화면에서 제거한다.
        /// </summary>
        /// <param name="upnlLIst"></param>
        /// <param name="additionScript"></param>
        protected void UCPopCloseScript(Control upnlLIst, string additionScript)
        {
            additionScript = additionScript.TrimEnd(';');

            if (!string.IsNullOrEmpty(additionScript))
                additionScript += ";";

            if (string.IsNullOrEmpty(ParentPopID))
                ScriptManager.RegisterStartupScript(upnlLIst, upnlLIst.GetType(), string.Format("close{0}", PopID), string.Format("es.pop.disposeLayer({{self:$('#{0}')}});{1}", PopID, additionScript), true);
            else
                ScriptManager.RegisterStartupScript(upnlLIst, upnlLIst.GetType(), string.Format("close{0}", PopID), string.Format("es.pop.disposeLayer({{self:$('#{0}'),parent:$('#{1}')}});{2}", PopID, ParentPopID, additionScript), true);
        }

        #endregion

        public virtual void AddChild(NewMovablePop childPop) { }
    }

    /// <summary>
    /// es.pop 객체 구현
    /// </summary>
    public abstract class NewMovablePop : BasePop
    {
        #region property
        private string CloseImage
        {
            get
            {
                //관리자 사이트
                if (Request.RawUrl.ToLower().IndexOf("/manage") > -1)
                {
                    return "https://truston.e-sang.net/common/images/ico/pop_close.gif";
                }
                else //사용자 사이트
                {
                    //return "/common/images/btn/pop_close.gif";
                    return "https://truston.e-sang.net/common/images/ico/pop_close.gif";
                }
            }
        }

        protected string CloseScript
        {
            get
            {
                ///
                if (!string.IsNullOrEmpty(ParentPopID))
                {
                    return string.Format("es.pop.disposeLayer({{self:$('#{0}'),parent:$('#{1}')}});", this.PopID, this.ParentPopID);
                }
                else
                {
                    return string.Format("es.pop.disposeLayer({{self:$('#{0}')}});", this.PopID);
                }
            }
        }

        protected string SelfOpenScript
        {
            get
            {
                if (!string.IsNullOrEmpty(ParentPopID))
                {
                    return string.Format("es.pop.coverScreen();es.pop.loadLayer($('#{0}'),{{parentPop:'{1}'}});", this.PopID, this.ParentPopID);
                }
                else
                {
                    return string.Format("es.pop.coverScreen();es.pop.loadLayer($('#{0}'),{{}});", this.PopID);
                }
            }
        }

        #endregion

        #region override

        public override void AddChild(NewMovablePop childPop)
        {
            if (!NMPops.Keys.Contains(childPop.PopID))
            {
                NMPops[childPop.PopID] = childPop;
                childPop.ParentPopID = PopID;
            }
        }

        protected override void NewMovablePopSetting()
        {
            PopID = this.ID;
            _openScript = string.Format("fnOpen_{0}", PopID);
        }

        public override void RenderControl(HtmlTextWriter writer)
        {
            CreateUcPopContainer(writer);
        }

        void CreateUcPopContainer(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(ParentPopID))
            {
                if (!string.IsNullOrEmpty(PopOption))
                {
                    PopOption = PopOption + ",";
                }

                StringBuilder popOptions = new StringBuilder(PopOption);

                popOptions.AppendFormat("parentPop: \"{0}\"", ParentPopID);

                PopOption = popOptions.ToString();
            }

            string scriptPostBack =
                string.Format("es.pop.coverScreen();\n\t\tes.pop.loadLayer($('#{0}'),{{{1}}});", PopID, PopOption);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine(string.Format("<!--###########################################[{0} start]###########################################-->", PopID));


            sb.AppendFormat("<div id='{0}' style='display: none;{2}' class='{1}'>", PopID, ContainerCSS, PopWidth.HasValue ? string.Format("width: {0}px;", PopWidth) : string.Empty)
                    .AppendLine();

            //if (BasePage.IsMobileBrowser)
            //{
            //    sb.AppendFormat("<div id='{0}' style='display: none;{2}' class='{1}'>", PopID, _containerCSS, PopWidth.HasValue ? string.Format("max-width: {0}px;", PopWidth) : string.Empty)
            //        .AppendLine();
            //}
            //else
            //{
            //    sb.AppendFormat("<div id='{0}' style='display: none;{2}' class='{1}'>", PopID, _containerCSS, PopWidth.HasValue ? string.Format("width: {0}px;", PopWidth) : string.Empty)
            //        .AppendLine();
            //}

            //자동 타이틀영역 설정
            sb.Append(ReadNewMovablePopTitleArea());

            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter html = new HtmlTextWriter(sw);
            base.RenderControl(html);

            sw.WriteLine();
            sw.WriteLine("</div>");

            if (!string.IsNullOrEmpty(scriptPostBack))
            {
                sw.WriteLine("");
                sw.WriteLine("<!--[Jylee:MovablePop AutoGenerate Script Area Start]-->");
                sw.WriteLine("");

                //sw.WriteLine("<link href='/common/css/content.css' type='text/css' rel='Stylesheet' />");
                sw.WriteLine("<script type='text/javascript'>");
                sw.WriteLine("//<![CDATA[");

                sw.WriteLine(string.Format("function fnOpen_{0}(param){{\n\tes.pop.init($('#{0}'));\n\n\tif(typeof(window['fnInit_{0}']) == 'undefined' || fnInit_{0}(param)){{\n\t\t{1}\n\t}} \n}}", PopID, scriptPostBack));

                sw.WriteLine("//]]>");
                sw.WriteLine("</script>");

                sw.WriteLine("");
                sw.WriteLine("<!--[Jylee:MovablePop AutoGenerate Script Area End]-->");
                sw.WriteLine("");
            }
            sb.AppendLine(string.Format("<!--###########################################[{0} end]###########################################-->", PopID));

            string htmlContent = sb.ToString();
            writer.Write(htmlContent);
        }
        #endregion

        string ReadNewMovablePopTitleArea()
        {
            StringBuilder sbHeader = new StringBuilder();

            if (!string.IsNullOrEmpty(PopTitle))
            {
                sbHeader.AppendLine("<div class=\"pop_header\">")
                .AppendLine(string.Format("\t<div class=\"pop_header_tit\">{0}</div>", PopTitle));


                if (!string.IsNullOrEmpty(ParentPopID))
                {
                    sbHeader.AppendFormat("\t<div class=\"pop_header_btn\"><a href=\"javascript:es.pop.disposeLayer({{self:$('#{0}'),parent:$('#{1}')}});\"><img src=\"{2}\" alt=\"창닫기\" /></a></div>", PopID, ParentPopID, CloseImage).AppendLine();
                }
                else
                {
                    sbHeader.AppendFormat("\t<div class=\"pop_header_btn\"><a href=\"javascript:es.pop.disposeLayer({{self:$('#{0}')}});\"><img src=\"{1}\" alt=\"창닫기\" /></a></div>", PopID, CloseImage).AppendLine();
                }

                sbHeader.Append("</div>");
            }

            return sbHeader.ToString();
        }
    }

}