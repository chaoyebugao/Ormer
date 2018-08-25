using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Ormer.Common.Utilities
{
    public class HashUtil
    {
        public static string Sha1(string content)
        {
            return Sha1(content, Encoding.UTF8);
        }

        public static string Sha1(string content, Encoding encode)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_in = encode.GetBytes(content);
            byte[] bytes_out = sha1.ComputeHash(bytes_in);
            sha1.Dispose();
            string result = BitConverter.ToString(bytes_out);
            result = result.Replace("-", "");
            return result;
        }
    }
}