using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BL.Secuirty
{
    public static class EncryptANDDecrypt
    {
        private static readonly string encryptionPassword = "RAKEB";
        public static string GenerateKey()
        {
            byte[] bytes = new byte[8];
            byte result = 0;
            byte.TryParse("0", out result);
            bytes[0] = result;
            byte.TryParse("10", out result);
            bytes[1] = result;
            byte.TryParse("20", out result);
            bytes[2] = result;
            byte.TryParse("30", out result);
            bytes[3] = result;
            byte.TryParse("40", out result);
            bytes[4] = result;
            byte.TryParse("50", out result);
            bytes[5] = result;
            byte.TryParse("60", out result);
            bytes[6] = result;
            byte.TryParse("70", out result);
            bytes[7] = result;
            return Encoding.ASCII.GetString(bytes);
        }

        public static string DecryptFile(string sInputFilename, string sKey)
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes(sKey),
                IV = Encoding.ASCII.GetBytes(sKey)
            };
            FileStream stream = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read);
            ICryptoTransform transform = provider.CreateDecryptor();
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
            string str = new StreamReader(stream2).ReadToEnd();
            stream.Close();
            return str;
        }

        public static void EncryptFile(string sInputFilename, string sOutputFilename, string sKey)
        {
            FileStream stream = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read);
            FileStream stream2 = new FileStream(sOutputFilename, FileMode.Create, FileAccess.Write);
            ICryptoTransform transform = new DESCryptoServiceProvider { Key = Encoding.ASCII.GetBytes(sKey), IV = Encoding.ASCII.GetBytes(sKey) }.CreateEncryptor();
            CryptoStream stream3 = new CryptoStream(stream2, transform, CryptoStreamMode.Write);
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            stream3.Write(buffer, 0, buffer.Length);
            stream3.Close();
            stream.Close();
            stream2.Close();
        }

        private static readonly byte[] salt = Encoding.ASCII.GetBytes("Ent3r your oWn S@lt v@lu# h#r3 Ya Mo3alem");

        public static string EncryptText(string textToEncrypt)
        {
            var algorithm = GetAlgorithm(encryptionPassword);

            byte[] encryptedBytes;
            using (ICryptoTransform encryptor = algorithm.CreateEncryptor(algorithm.Key, algorithm.IV))
            {
                byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(textToEncrypt);
                encryptedBytes = InMemoryCrypt(bytesToEncrypt, encryptor);
            }
            return Convert.ToBase64String(encryptedBytes);
        }
        public static string DecryptText(string encryptedText)
        {
            try
            {
                var algorithm = GetAlgorithm(encryptionPassword);

                byte[] descryptedBytes;
                using (ICryptoTransform decryptor = algorithm.CreateDecryptor(algorithm.Key, algorithm.IV))
                {
                    byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                    descryptedBytes = InMemoryCrypt(encryptedBytes, decryptor);
                }
                return Encoding.UTF8.GetString(descryptedBytes);
            }
            catch
            {
                return "#";
            }
        }

        private static byte[] InMemoryCrypt(byte[] data, ICryptoTransform transform)
        {
            try
            {
                MemoryStream memory = new MemoryStream();
                using (Stream stream = new CryptoStream(memory, transform, CryptoStreamMode.Write))
                {
                    stream.Write(data, 0, data.Length);
                }
                return memory.ToArray();
            }
            catch { return new byte[] { }; }
        }
        private static RijndaelManaged GetAlgorithm(string encryptionPassword)
        {
            // Create an encryption key from the encryptionPassword and salt.
            var key = new Rfc2898DeriveBytes(encryptionPassword, salt);

            // Declare that we are going to use the Rijndael algorithm with the key that we've just got.
            var algorithm = new RijndaelManaged();
            int bytesForKey = algorithm.KeySize / 8;
            int bytesForIV = algorithm.BlockSize / 8;
            algorithm.Key = key.GetBytes(bytesForKey);
            algorithm.IV = key.GetBytes(bytesForIV);
            return algorithm;
        }
    }
}