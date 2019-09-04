using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Nokia.AssessmentMange.Domain.Common
{
    public class CryptographyHelp
    {

        public static string GetMD5(string str)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }
    }
}
