using System;
using System.Security.Cryptography;
using System.Text;

namespace pt7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AssignNewKey();
            while (true)
            {
                Console.WriteLine("Enter data to encrypt/decrypt: ");
                string a = Console.ReadLine();
                byte [] encrypted = EncryptData(Encoding.Unicode.GetBytes(a));
                Console.WriteLine(Convert.ToBase64String(encrypted));
                Console.WriteLine(Encoding.Unicode.GetString(DecryptData(encrypted)));
            }
        }
        private static RSAParameters _publicKey, _privateKey;
        public static void AssignNewKey()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                _publicKey = rsa.ExportParameters(false);
                _privateKey = rsa.ExportParameters(true);
            }
        }
        public static byte[] EncryptData(byte[] dataToEncrypt)
        {
            byte[] cypherBytes;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.ImportParameters(_publicKey);
                cypherBytes = rsa.Encrypt(dataToEncrypt, true);
            }
            return cypherBytes;
        }
        public static byte[] DecryptData(byte[] dataToDecrypt)
        {
            byte[] plainBytes;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.ImportParameters(_privateKey);
                plainBytes = rsa.Decrypt(dataToDecrypt, true);
            }
            return plainBytes;
        }
    }
}

