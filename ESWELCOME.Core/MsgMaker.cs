using ESWELCOME.DataBase.Procedure.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESWELCOME.Core
{
    public static class MsgMaker
    {

        #region 현주
        /// <summary>
        /// 문자 내용
        /// </summary>
        /// <returns></returns>
        public static string MakeMsgContent(string schType, string gstCpy, string gstPst, string gstName, string schYearMd, string schHour, string schMin, string msgCode, string msgStaff)
        {
            string msgContent = string.Empty;

            // 회사 및 직급 없을 시
            string gstCpy2 = gstCpy != null && gstCpy != "-" ? gstCpy + " " : "";
            string gstPst2 = gstPst != null && gstPst != "-" ? " " + gstPst : "";

            // 년도 변환
            string schYearMd2 = schYearMd;
            StringBuilder sbYearMd = new StringBuilder();
            sbYearMd.Append(schYearMd2.Substring(0, 4));
            sbYearMd.Append("년 ");
            sbYearMd.Append(schYearMd2.Substring(4, 2));
            sbYearMd.Append("월 ");
            sbYearMd.Append(schYearMd2.Substring(6, 2));
            sbYearMd.Append("일 ");
            schYearMd2 = sbYearMd.ToString();

            // 시분 합치기
            string schHourMin = schHour + "시 " + schMin + "분";

            // 문자 담당자 번호
            string num = msgStaff;
            if (msgStaff.Length < 5)
            {
                num = ESFacade.GetInstance.GetMEMBER(Convert.ToInt32(num)).GenericItem.MEM_MOBILE_NO;
            }

            StringBuilder sbMsg = new StringBuilder();
            sbMsg.Append("\n[ES타워 방문안내]\n\n");
            sbMsg.Append("\"" + gstCpy2 + gstName + gstPst2 + "님\"");
            sbMsg.Append("\n\n안녕하세요.");
            sbMsg.Append("\n" + schType + " 일정 안내 드립니다.");
            sbMsg.Append("\n\n날 짜 : " + schYearMd2);
            sbMsg.Append("\n시 간 : " + schHourMin);
            sbMsg.Append("\n인증번호 : [" + msgCode + "]");
            sbMsg.Append("\n담당자 번호 : " + num);
            sbMsg.Append("\n\n도착 시 접수 부탁드립니다.");
            sbMsg.Append("\n감사합니다.");
            sbMsg.Append("\n\n▶ 오시는 길");
            sbMsg.Append("\nhttps://kko.to/E47ZooGW3i");
            sbMsg.Append("\n서울 마포구 월드컵북로58길 9 ES타워");
            msgContent = sbMsg.ToString();

            return msgContent;

        }
        #endregion







    }
}
