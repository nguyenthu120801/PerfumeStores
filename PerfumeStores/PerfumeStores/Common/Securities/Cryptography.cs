using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Common.Securities
{
    public class Cryptography
    {
        public static string MD5Hash(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            // Compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
            // Get hash result after compute it
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("X2"));
            }

            return strBuilder.ToString();
        }

        public static bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            var encryptedPass = MD5Hash(enteredPassword);
            return encryptedPass == storedPassword;
        }
    }
}
