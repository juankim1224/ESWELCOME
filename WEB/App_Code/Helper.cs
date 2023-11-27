using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Data;
using System.ComponentModel;
using System.Collections;
using ESWELCOME.DataBase.Procedure.Facade;

namespace WEB
    .App_Code
{
    /// <summary>
    /// 그외 설정값
    /// </summary>
    public class ETC : Attribute
    {
        public string Value { get; private set; }

        public ETC(string value)
        {
            this.Value = value;
        }
    }

    public static class Constant
    {
        public const string NO_DATA = "데이터가 존재하지 않습니다.";

        
    }

    /// <summary>
    /// 파일 관리 서비스
    /// </summary>
    public static class FileService
    {
        [DllImport(@"urlmon.dll", CharSet = CharSet.Auto)]
        private extern static System.UInt32 FindMimeFromData(UInt32 pBC, String pwzUrl, byte[] pBuffer, UInt32 cbSize, String pwzMimeProposed, UInt32 dwMimeFlags, out UInt32 ppwzMimeOut, UInt32 dwReserverd);

        /// <summary>
        /// 이진파일에서 파일형식을 체크하여 실제 MimeType을 가져온다.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetMimeFromFile(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException(fileName + "을 찾을 수 없습니다.");

            byte[] buffer = new byte[256];

            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                if (fs.Length >= 256)
                {
                    fs.Read(buffer, 0, 256);
                }
                else
                {
                    fs.Read(buffer, 0, (int)fs.Length);
                }
            }

            try
            {
                UInt32 mimetype;

                FindMimeFromData(0, null, buffer, 256, null, 0, out mimetype, 0);
                IntPtr mimeTypePtr = new IntPtr(mimetype);
                string mime = Marshal.PtrToStringUni(mimeTypePtr);
                Marshal.FreeCoTaskMem(mimeTypePtr);
                return mime;
            }
            catch (Exception e)
            {
                return "unknown/unknown";
            }
        }

        public static string Copy(string sourceFileName, string destFileName, bool isUnique)
        {
            if (File.Exists(sourceFileName))
            {
                if (!isUnique)
                {
                    File.Copy(sourceFileName, destFileName, true);
                }
                else
                {
                    int i = 0;
                    string uniqueFileName = destFileName;
                    do
                    {
                        if (i != 0)
                        {
                            destFileName = Path.GetDirectoryName(uniqueFileName) + "\\" + Path.GetFileNameWithoutExtension(uniqueFileName) + "_" + i.ToString() + Path.GetExtension(uniqueFileName);
                        }
                        i++;
                    } while (File.Exists(destFileName));
                    File.Copy(sourceFileName, destFileName, true);
                }
                return Path.GetFileName(destFileName);
            }
            else
                return string.Empty;
        }
    }

    /// <summary>
    /// 확장클래스
    /// </summary>
    public static class ESExtension
    {

        public static string ToETC(this Enum value)
        {
            var enumType = value.GetType();
            var name = Enum.GetName(enumType, value);
            var data = enumType.GetField(name).GetCustomAttributes(false).OfType<ETC>().SingleOrDefault();
            if (data != null)
                return data.Value;
            else
                return string.Empty;
        }

        #region DropDownList & HtmlInputSelect
        public static string PATH_STR = "@@";

        private static PropertyInfo[] CreatePropertyInfos(Type type, string fields, params char[] separator)
        {
            string[] arrFields = fields.Split(separator);
            PropertyInfo[] arrProperty = new PropertyInfo[arrFields.Length];

            for (int i = 0; i < arrProperty.Length; i++)
            {
                arrProperty[i] = type.GetProperty(arrFields[i]);
            }

            return arrProperty;
        }

        private static void BindListItem<T>(Control ctl, IEnumerable<T> dataSource, string textField, string valueField, params char[] separator)
        {
            if (ctl is HtmlSelect || ctl is DropDownList)
            {
                if (ctl is HtmlSelect)
                    (ctl as HtmlSelect).Items.Clear();


                if (ctl is DropDownList)
                    (ctl as DropDownList).Items.Clear();


                ListItemCollection items = new ListItemCollection();

                Type type = dataSource.GetType().GetGenericArguments()[0];

                PropertyInfo[] arrTextProperty = CreatePropertyInfos(type, textField, separator);
                PropertyInfo[] arrValProperty = CreatePropertyInfos(type, valueField, separator);

                dataSource.ToList<T>().ForEach(delegate (T t)
                {
                    string text = string.Empty;
                    string value = string.Empty;

                    arrTextProperty.ToList().ForEach(p =>
                        text += string.Format("{0}{1}", p.GetValue(t, null), PATH_STR));

                    arrValProperty.ToList().ForEach(p =>
                        value += string.Format("{0}{1}", p.GetValue(t, null), PATH_STR));

                    text = text.TrimEnd(new char[] { '@', ' ', '\n' });
                    value = value.TrimEnd(new char[] { '@', ' ', '\n' });

                    if (ctl is HtmlSelect)
                        (ctl as HtmlSelect).Items.Add(new ListItem(text, value));


                    if (ctl is DropDownList)
                        (ctl as DropDownList).Items.Add(new ListItem(text, value));

                });
            }
        }

        public static decimal? NumericValue(this string str)
        {
            decimal? val = null;

            if (!string.IsNullOrEmpty(str))
            {
                str = Regex.Replace(str, "[,]", string.Empty, RegexOptions.Compiled | RegexOptions.IgnoreCase);

                val = Convert.ToDecimal(str);
            }

            return val;
        }

        public static string NumericValue(this TextBox tb)
        {
            return Regex.Replace(tb.Text, "[,]", "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public static string NumericValue(this HtmlInputText tb)
        {
            return Regex.Replace(tb.Value, "[,]", "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public static void BindSelectBox<T>(this HtmlSelect ctl, IEnumerable<T> dataSource, string textField, string valueField, params char[] separator)
        {
            BindListItem(ctl, dataSource, textField, valueField, separator);
        }

        public static void BindDropDownList<T>(this DropDownList ctl, IEnumerable<T> dataSource, string textField, string valueField, params char[] separator)
        {
            BindListItem(ctl, dataSource, textField, valueField, separator);
        }

        public static List<T> OrderBy2<T>(this IEnumerable<T> list, string sortExpression)
        {
            sortExpression += "";
            string[] parts = sortExpression.Split(' ');
            bool descending = false;
            string property = "";

            if (parts.Length > 0 && parts[0] != "")
            {
                property = parts[0];

                if (parts.Length > 1)
                {
                    descending = parts[1].ToLower().Contains("esc");
                }

                PropertyInfo prop = typeof(T).GetProperty(property);

                if (prop == null)
                {
                    throw new Exception(string.Format("'{0}'클래스는 '{1}'속성을 가지지 않습니다.", typeof(T).FullName, property));
                }

                if (descending)
                    return list.OrderByDescending(x => CheckType(prop.GetValue(x, null))).ToList<T>();
                else
                    return list.OrderBy(x => CheckType(prop.GetValue(x, null))).ToList<T>();
            }

            return list.ToList<T>();
        }

        private static object CheckType(object obj)
        {
            var objValue = obj.ToString();

            if (!string.IsNullOrEmpty(objValue) && Regex.IsMatch(objValue, "^-?[\\d\\.,]+$"))
            {
                return Convert.ToDecimal(Regex.Replace(objValue, ",", string.Empty, RegexOptions.Compiled));
            }

            return obj;
        }

        public static string EscapeXml(this string s)
        {
            string xml = s;
            if (!string.IsNullOrEmpty(xml))
            {
                // replace literal values with entities
                xml = xml.Replace("&", "&amp;");
                xml = xml.Replace("&lt;", "&lt;");
                xml = xml.Replace("&gt;", "&gt;");
                xml = xml.Replace("\"", "&quot;");
                xml = xml.Replace("'", "&apos;");
            }
            return xml;
        }

        public static string UnescapeXml(this string s)
        {
            string unxml = s;
            if (!string.IsNullOrEmpty(unxml))
            {
                // replace entities with literal values
                unxml = unxml.Replace("&apos;", "'");
                unxml = unxml.Replace("&quot;", "\"");
                unxml = unxml.Replace("&gt;", "&gt;");
                unxml = unxml.Replace("&lt;", "&lt;");
                unxml = unxml.Replace("&amp;", "&");
            }
            return unxml;
        }


        #endregion

        public static string ReplaceVal(this object obj, string replaceString)
        {
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {
                return obj.ToString();
            }

            return replaceString;
        }

        /// <summary>
        /// 큰따옴표, 작은따옴표를 인코딩된 문자열로 변환
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        public static string EncodeQuotation(string origin)
        {
            origin = Regex.Replace(origin, "\"", "&quot;", RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
            origin = Regex.Replace(origin, "'", "&apos;", RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

            return origin;
        }

        public static string DecodeQuotation(string origin)
        {
            origin = Regex.Replace(origin, "&quot;", "\"", RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
            origin = Regex.Replace(origin, "&apos;", "'", RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

            return origin;
        }


        public static string base64DecodeReplace(string sText)
        {
            string str = sText;

            str = str.Replace("@", "/");
            str = str.Replace("$", "+");

            str = ESNfx.Encryption.base64Decode(str);

            return str;
        }

        public static string base64EncodeReplace(string sText)
        {
            string str = "";
            str = ESNfx.Encryption.base64Encode(sText);
            str = str.Replace("/", "@");
            str = str.Replace("+", "$");

            return str;
        }

        /// <summary>
        /// 핸드폰번호 체크
        /// </summary>
        /// <param name="phoneNum"></param>
        /// <returns></returns>
        public static bool phoneNumCheck(string phoneNum)
        {
            bool isPhoneNumCheck = true;

            if (phoneNum.Length == 10 || phoneNum.Length == 11)
            {
                Regex regex = new Regex(@"01{1}[016789]{1}[0-9]{7,8}");

                Match m = regex.Match(phoneNum);

                if (m.Success)
                {
                    isPhoneNumCheck = true;
                }
                else
                {
                    isPhoneNumCheck = false;
                }
            }
            else
            {
                isPhoneNumCheck = false;
            }

            return isPhoneNumCheck;
        }

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