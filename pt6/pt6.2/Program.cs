using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Intrinsics.X86;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
namespace pt6._2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please, enter password");
            string passwordToHash = Console.ReadLine();
            int seed = BitConverter.ToInt32(HashPassword(passwordToHash, 9 * 10000));
            AesCipher aesCipher = new AesCipher();
            Random key = new Random(seed);
            byte[] Key = new byte[32];
            for (int i = 0; i < 32; i++)
            {
                Key[i] = (byte)(key.Next(0, 255));
            }
            aesCipher.Key = Key;
            while (true)
            {
                Console.WriteLine("Enter data to cipher");
                string data = Console.ReadLine();
                Random iv = new Random(seed);
                byte[] IV = new byte[16];
                for (int i = 0; i < 16; i++)
                {
                    IV[i] = (byte)(iv.Next(0, 255));
                }
                var aes_encrypted = aesCipher.Encrypt(Encoding.UTF8.GetBytes(data), aesCipher.Key, IV);
                var aes_decrypted = aesCipher.Decrypt(aes_encrypted, aesCipher.Key, IV);
                var aes_decryptedMessage = Encoding.UTF8.GetString(aes_decrypted);
                Console.WriteLine("----------------------");
                Console.WriteLine("AES Encryption in .NET");
                Console.WriteLine();
                Console.WriteLine("Original Text = " + data);
                Console.WriteLine("Encrypted Text = " +
                Convert.ToBase64String(aes_encrypted));
                Console.WriteLine("Decrypted Text = " + aes_decryptedMessage);
                Console.WriteLine("----------------------");
            }
        }
        public class PBKDF2
        {
            public static byte[] GenerateSalt()
            {
                using (var randomNumberGenerator = new RNGCryptoServiceProvider())
                {
                    var randomNumber = new byte[32];
                    randomNumberGenerator.GetBytes(randomNumber);
                    return randomNumber;
                }
            }
            public static byte[] HashPassword(byte[] toBeHashed, byte[] salt, int numberOfRounds)
            {
                using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds))
                {
                    return rfc2898.GetBytes(20);
                }
            }
        }
        private static byte[] HashPassword(string passwordToHash, int numberOfRounds)
        {
            var hashedPassword = PBKDF2.HashPassword(Encoding.UTF8.GetBytes(passwordToHash), PBKDF2.GenerateSalt(), numberOfRounds);
            Console.WriteLine();
            Console.WriteLine("Password to hash : " + passwordToHash);
            Console.WriteLine("Hashed Password : " +
            Convert.ToBase64String(hashedPassword));
            return hashedPassword;
        }
    }
    public class AesCipher
    {
        public byte[] Key;
        public byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
        public byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
    
