using System;
using System.IO;
using System.Security.Cryptography;
using static pt6.Program;
using System.Text;

namespace pt6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DesCipher desCipher = new DesCipher();
            TripleDesCipher tripleDesCipher = new TripleDesCipher();
            AesCipher aesCipher = new AesCipher();
            desCipher.Key = GenerateRandomNumber(8);
            tripleDesCipher.Key = GenerateRandomNumber(16);
            aesCipher.Key = GenerateRandomNumber(32);
            while (true)
            {
                Console.WriteLine("Please, enter data to encrypt / decrypt");
                string data = Console.ReadLine();
                var DesIV = GenerateRandomNumber(8);
                var TripleDesIV = GenerateRandomNumber(8);
                var AesIV = GenerateRandomNumber(16);
                var des_encrypted = desCipher.Encrypt(Encoding.UTF8.GetBytes(data), desCipher.Key, DesIV);
                var des_decrypted = desCipher.Decrypt(des_encrypted, desCipher.Key, DesIV);
                var des_decryptedMessage = Encoding.UTF8.GetString(des_decrypted);
                var triple_des_encrypted = tripleDesCipher.Encrypt(Encoding.UTF8.GetBytes(data), tripleDesCipher.Key, TripleDesIV);
                var triple_des_decrypted = tripleDesCipher.Decrypt(triple_des_encrypted, tripleDesCipher.Key, TripleDesIV);
                var triple_des_decryptedMessage = Encoding.UTF8.GetString(triple_des_decrypted);
                var aes_encrypted = aesCipher.Encrypt(Encoding.UTF8.GetBytes(data), aesCipher.Key, AesIV);
                var aes_decrypted = aesCipher.Decrypt(aes_encrypted, aesCipher.Key, AesIV);
                var aes_decryptedMessage = Encoding.UTF8.GetString(aes_decrypted);
                Console.WriteLine("----------------------");
                Console.WriteLine("DES Encryption in .NET");
                Console.WriteLine();
                Console.WriteLine("Original Text = " + data);
                Console.WriteLine("Encrypted Text = " +
                Convert.ToBase64String(des_encrypted));
                Console.WriteLine("Decrypted Text = " + des_decryptedMessage);
                Console.WriteLine("----------------------");
                Console.WriteLine("TripleDES Encryption in .NET");
                Console.WriteLine();
                Console.WriteLine("Original Text = " + data);
                Console.WriteLine("Encrypted Text = " +
                Convert.ToBase64String(triple_des_encrypted));
                Console.WriteLine("Decrypted Text = " + triple_des_decryptedMessage);
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
        static byte[] GenerateRandomNumber(int length)
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[length];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
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
    public class DesCipher
    {
        public byte[] Key;
        public byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            using (var des = new DESCryptoServiceProvider())
            {
                des.Mode = CipherMode.CBC;
                des.Padding = PaddingMode.PKCS7;
                des.Key = key;
                des.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
        public byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
        {
            using (var des = new DESCryptoServiceProvider())
            {
                des.Mode = CipherMode.CBC;
                des.Padding = PaddingMode.PKCS7;
                des.Key = key;
                des.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
    }
    public class TripleDesCipher
    {
        public byte[] Key;
        public byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            using (var tripledes = new TripleDESCryptoServiceProvider())
            {
                tripledes.Mode = CipherMode.CBC;
                tripledes.Padding = PaddingMode.PKCS7;
                tripledes.Key = key;
                tripledes.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, tripledes.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
        public byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
        {
            using (var tripledes = new TripleDESCryptoServiceProvider())
            {
                tripledes.Mode = CipherMode.CBC;
                tripledes.Padding = PaddingMode.PKCS7;
                tripledes.Key = key;
                tripledes.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, tripledes.CreateDecryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
