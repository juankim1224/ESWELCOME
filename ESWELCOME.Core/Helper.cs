using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web;
using System.ComponentModel;

namespace ESWELCOME.Core
{
    /// <summary>
    /// 확장클래스
    /// </summary>
    public static class ESExtension
    {

        public static string ToDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
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
        /// 첫번째로 검색 되는 문자열만 Replace
        /// </summary>
        /// <param name="text"></param>
        /// <param name="search"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        /// <summary>
        /// 임시비밀번호생성
        /// </summary>
        /// <returns></returns>
        public static string CreatePassword()
        {
            var password = new StringBuilder();
            var alphabet = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            var number = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            var special = new string[] { "!", "@", "#", "$", "%", "^", "&", "*" };

            var charType = new string[3][];

            charType[0] = alphabet;
            charType[1] = number;
            //charType[2] = special;

            Random r1 = new Random();
            Random r2 = new Random();

            int prevIndex = -1;

            for (int i = 0; i < 11; i++)
            {
                var upperDemension = r1.Next(0, 2);

                while (upperDemension == prevIndex)
                {
                    upperDemension = r1.Next(0, 2);
                }

                var demensionMaxLength = charType[upperDemension].Length;

                password.Append(charType[upperDemension][r2.Next(0, demensionMaxLength)]);

                prevIndex = upperDemension;
            }

            return password.ToString();
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


    }
}
