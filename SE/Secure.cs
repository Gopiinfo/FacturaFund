using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SE
{
    public class Secure
    {
        private const string strLogic = "HmacSHA256";
        private const string KeyPasswordEncrypt = "dehdfvGh5nchejkwuernShsfdiWstrSYubsfersoeGwrnkkIkptasnD";
        private const string strbriny = "wxdc45dgfdfgdfg65sdfsdfsfFgvfdJjksleorTcmqtszDjhedsoiyA";

        public static string GetHashedPassword(string password, string sbriny)
        {
            try
            {
                string key = string.Join(":", new string[] { password, sbriny });
                using (HMAC hmac = HMACSHA256.Create(strLogic))
                {
                    hmac.Key = Encoding.UTF8.GetBytes(sbriny + strbriny);
                    hmac.ComputeHash(Encoding.UTF8.GetBytes(key));
                    return Convert.ToBase64String(hmac.Hash);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string PAConceal(string sText, string sSalt)
        {
            try
            {
                byte[] clearBytes = Encoding.Unicode.GetBytes(sText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb;

                    if (sSalt == "N")
                    {
                        pdb = new Rfc2898DeriveBytes(KeyPasswordEncrypt, new byte[] { 0x65, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x49, 0x65, 0x64, 0x76, 0x64, 0x65, 0x76 });
                    }
                    else
                    {
                        byte[] bArr = Encoding.ASCII.GetBytes(sSalt);
                        pdb = new Rfc2898DeriveBytes(KeyPasswordEncrypt, bArr);
                    }

                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        sText = Convert.ToBase64String(ms.ToArray());
                    }
                }
                return sText;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string PAreveal(string cipherText, string sSalt)
        {
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText.Replace(' ', '+'));
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb;
                    if (sSalt == "N")
                    {
                        pdb = new Rfc2898DeriveBytes(KeyPasswordEncrypt, new byte[] { 0x65, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x49, 0x65, 0x64, 0x76, 0x64, 0x65, 0x76 });
                    }
                    else
                    {
                        byte[] bArr = Encoding.ASCII.GetBytes(sSalt);
                        pdb = new Rfc2898DeriveBytes(KeyPasswordEncrypt, bArr);
                    }
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
                return cipherText;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string EnCrypt(string text)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string DeCrypt(string text)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(text);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

        }

        public static string PasswordHash(string Password)
        {
            string HashedPassword = string.Empty;

            Byte[] inputBytes = Encoding.UTF8.GetBytes(Password);

            HashAlgorithm algorithm = new MD5CryptoServiceProvider();

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            HashedPassword = BitConverter.ToString(hashedBytes);
            HashedPassword = HashedPassword.Replace(@"-", string.Empty);

            return HashedPassword;
        }

        public static string EncryptText(string TextToBeEncrypted)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            string Password = "cscE@12#";
            byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(TextToBeEncrypted);
            byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(PlainText, 0, PlainText.Length);
            cryptoStream.FlushFinalBlock();
            byte[] CipherBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string EncryptedData = Convert.ToBase64String(CipherBytes);

            return EncryptedData;
        }


        public static string DecryptText(string TextToBeDecrypted)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            string Password = "cscE@12#";
            string DecryptedData;

            try
            {
                byte[] EncryptedData = Convert.FromBase64String(TextToBeDecrypted);

                byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
                PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
                ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
                MemoryStream memoryStream = new MemoryStream(EncryptedData);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

                byte[] PlainText = new byte[EncryptedData.Length];
                int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
                memoryStream.Close();
                cryptoStream.Close();
                DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
            }
            catch
            {
                DecryptedData = string.Empty;
            }
            return DecryptedData;
        }

        public static string EncryptURLText(string TextToBeEncrypted)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            string Password = "cscE@12#";
            byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(TextToBeEncrypted);
            byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(PlainText, 0, PlainText.Length);
            cryptoStream.FlushFinalBlock();
            byte[] CipherBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string EncryptedData = Convert.ToBase64String(CipherBytes).Replace('/', '*');

            return EncryptedData;
        }

        public static string DecryptURLText(string TextToBeDecrypted)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            string Password = "cscE@12#";
            string DecryptedData;

            try
            {
                byte[] EncryptedData = Convert.FromBase64String(TextToBeDecrypted.Replace('*', '/'));

                byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
                PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
                ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
                MemoryStream memoryStream = new MemoryStream(EncryptedData);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

                byte[] PlainText = new byte[EncryptedData.Length];
                int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
                memoryStream.Close();
                cryptoStream.Close();
                DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
            }
            catch
            {
                DecryptedData = string.Empty;
            }
            return DecryptedData;
        }
    }
}
