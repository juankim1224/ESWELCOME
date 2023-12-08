using ESWELCOME.DataBase.Procedure.Facade;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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



        public static string EncryptString(string InputText, string Password)
        {

            // Rihndael class를 선언하고, 초기화
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            // 입력받은 문자열을 바이트 배열로 변환
            byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(InputText);

            // 딕셔너리 공격을 대비해서 키를 더 풀기 어렵게 만들기 위해서 
            // Salt를 사용한다.
            byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());

            // PasswordDeriveBytes 클래스를 사용해서 SecretKey를 얻는다.
            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);

            // Create a encryptor from the existing SecretKey bytes.
            // encryptor 객체를 SecretKey로부터 만든다.
            // Secret Key에는 32바이트
            // (Rijndael의 디폴트인 256bit가 바로 32바이트입니다)를 사용하고, 
            // Initialization Vector로 16바이트
            // (역시 디폴트인 128비트가 바로 16바이트입니다)를 사용한다.
            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

            // 메모리스트림 객체를 선언,초기화 
            MemoryStream memoryStream = new MemoryStream();

            // CryptoStream객체를 암호화된 데이터를 쓰기 위한 용도로 선언
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);

            // 암호화 프로세스가 진행된다.
            cryptoStream.Write(PlainText, 0, PlainText.Length);

            // 암호화 종료
            cryptoStream.FlushFinalBlock();

            // 암호화된 데이터를 바이트 배열로 담는다.
            byte[] CipherBytes = memoryStream.ToArray();

            // 스트림 해제
            memoryStream.Close();
            cryptoStream.Close();

            // 암호화된 데이터를 Base64 인코딩된 문자열로 변환한다.
            string EncryptedData = Convert.ToBase64String(CipherBytes);

            // 최종 결과를 리턴
            return EncryptedData;
        }

        public static string DecryptString(string InputText, string Password)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            byte[] EncryptedData = Convert.FromBase64String(InputText);
            byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());

            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);

            // Decryptor 객체를 만든다.
            ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

            MemoryStream memoryStream = new MemoryStream(EncryptedData);

            // 데이터 읽기(복호화이므로) 용도로 cryptoStream객체를 선언, 초기화
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

            // 복호화된 데이터를 담을 바이트 배열을 선언한다.
            // 길이는 알 수 없지만, 일단 복호화되기 전의 데이터의 길이보다는
            // 길지 않을 것이기 때문에 그 길이로 선언한다.
            byte[] PlainText = new byte[EncryptedData.Length];

            // 복호화 시작
            int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);

            memoryStream.Close();
            cryptoStream.Close();

            // 복호화된 데이터를 문자열로 바꾼다.
            string DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);

            // 최종 결과 리턴
            return DecryptedData;
        }



    }
}
