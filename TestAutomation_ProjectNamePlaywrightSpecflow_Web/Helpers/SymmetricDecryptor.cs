using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace TestAutomationProjectNamePlaywrightSpecflowWeb
{
    public class SymmetricDecryptor
    {
        private static string cryptoKeyPassword = "A9B2c6Y%-=@22";
        public static string DecryptToString(string cipherText)
        {
            try
            {
                byte[] iv = new byte[16];
                byte[] buffer = Convert.FromBase64String(cipherText);
                var key = GetKey(cryptoKeyPassword);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    using (MemoryStream memoryStream = new MemoryStream(buffer))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("DecryptToString() FAILED");
                return String.Empty;
            }
        }

        // converts password to 128 bit hash
        private static byte[] GetKey(string password)
        {
            var keyBytes = Encoding.UTF8.GetBytes(password);
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(keyBytes);
            }
        }
    }
}
