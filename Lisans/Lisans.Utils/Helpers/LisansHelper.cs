using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Lisans.Utils.Helpers
{
    public class LisansHelper
    {
        #region Singleton Pattern

        private static readonly Lazy<LisansHelper> _Instance = new Lazy<LisansHelper>(() => new LisansHelper());
        private LisansHelper() { }
        public static LisansHelper Instance { get { return _Instance.Value; } }

        #endregion


        public string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }



        // Verify a hash against a string.
        public bool VerifyMd5Hash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Offline ürün anahtarını oluşturan metod.
        /// </summary>
        /// <param name="hwId"></param>
        /// <returns></returns>
        public string GetOfflineProductKey(string hwId)
        {
            string hash = GetMd5Hash(hwId).ToUpper();
            return hash.Substring(0, 5) + hash.Substring(hash.Length - 5);
        }


        /// <summary>
        /// Base64 formata çeviren Extension method.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetBase64String(string input)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
