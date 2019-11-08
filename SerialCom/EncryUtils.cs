using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SerialCom
{
    /// <summary>
    /// 加密类
    /// created by wangzh 2019/11/07
    /// </summary>
    public abstract class EncryUtils
    {
        /// <summary>
        /// MD5 32 +Base64
        /// </summary>
        /// <param name="input">待加密字符串</param>
        /// <returns></returns>
        public static String Md5AndBase64(String input)
        {
            String urlInput = UrlEncode(input, Encoding.UTF8);
            MD5CryptoServiceProvider crypto = new MD5CryptoServiceProvider();
            Byte[] bytes = crypto.ComputeHash(Encoding.UTF8.GetBytes(urlInput));
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 同步java java.net.URLEncoder方法字符默认为大写
        /// C# HttpUtility.UrlEncode方法默认为小写
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        private static String UrlEncode(string temp, Encoding encoding)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (Int32 i = 0; i < temp.Length; i++)
            {
                String t = temp[i].ToString();
                String k = HttpUtility.UrlEncode(t, encoding);
                if (t.Equals(k))
                    stringBuilder.Append(t);
                else
                    stringBuilder.Append(k.ToUpper());
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 32位MD5方法2
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String Md5_2(string input)
        {
            String result = String.Empty;
            if (String.IsNullOrEmpty(input)) return result;
            using (MD5 md5 = MD5.Create())
            {
                result = GetMd5Hash(md5, input);
            }
            return result;
        }

        /// <summary>
        /// 获取加密字符串
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string GetMd5Hash(MD5 md5Hash, string input)
        {
            Byte[] bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            foreach (Byte t in bytes)
            {
                sBuilder.Append(t.ToString("x2"));
            }
            return sBuilder.ToString();
        }


        /// <summary>
        /// 32位MD5方法1
        /// </summary>
        /// <param name="input">待加密字符串</param>
        /// <returns></returns>
        public static String Md5_1(String input)
        {
            MD5CryptoServiceProvider crypto = new MD5CryptoServiceProvider();
            Byte[] bytes = crypto.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            foreach (Byte num in bytes)
            {
                sBuilder.AppendFormat("{0:x2}", num);
            }
            return sBuilder.ToString();        //32位
        }
    }
}
