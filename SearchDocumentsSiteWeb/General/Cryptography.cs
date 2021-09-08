using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SearchDocumentsSiteWeb.General
{
    public class Cryptography
    {
        //The key used for generating the encrypted string
        private const string _CryptoKey = @"d@t@image-pr0";

        //The Initialization Vector for the DES encryption routine
        private static readonly byte[] _Iv = { 241, 0, 34, 19, 23, 32, 44, 151 };


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceString"></param>
        /// <returns></returns>
        public static string Encrypt(string sourceString)
        {
            return Encrypt(sourceString, _CryptoKey);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt(string sourceString, string key)
        {
            Byte[] buffer = Encoding.ASCII.GetBytes(sourceString);
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            des.Key = MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(key));
            des.IV = _Iv;
            string result = Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length));
            des.Clear();
            MD5.Clear();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptedString"></param>
        /// <returns></returns>
        public static string Decrypt(string encryptedString)
        {
            return Decrypt(encryptedString, _CryptoKey);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptedString"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt(string encryptedString, string key)
        {
            try
            {
                Byte[] buffer = Convert.FromBase64String(encryptedString);
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
                des.Key = MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(key));
                des.IV = _Iv;
                string result = Encoding.ASCII.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));
                des.Clear();
                MD5.Clear();
                return result;

            }
            catch (CryptographicException ex)
            {
                throw ex;
            }
        }
    }
}