using System;
using System.Data.SqlTypes;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace pt7._2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            _currentPath = "./keys/Dovgodko_RSAPublicKey.xml";
            AssignNewKey();
            while (true)
            {
                Console.WriteLine("Choose command: ");
                Console.WriteLine("Enter 'u' char if you wanna change data's destination user(public key)");
                Console.WriteLine("Otherwise, just continue with some data to encrypt/decrypt using current user's key");
                var a = Console.ReadKey();
                switch (a.KeyChar)
                {
                    case'u':
                        Console.Clear();
                        Console.WriteLine("Available keys: ");
                        string[] files = Directory.GetFiles("./keys");
                        foreach (string b in files)
                        {
                            Console.WriteLine(b);
                        }
                        Console.WriteLine("Please, enter surname of chosen key's owner");
                        _currentPath = "./keys/"+ Console.ReadLine()+"_RSAPublicKey.xml";
                        try
                        {
                            ChangeUser(File.ReadAllText(_currentPath));
                        }
                        catch{
                            Console.WriteLine("Mistake: Incorrect surname(chosen key unreachable)");
                        }
                        break;
                    default:
                        Console.WriteLine("Enter data to encrypt/decrypt: ");
                        string data = Console.ReadLine();
                        byte[] encrypted = EncryptData(Encoding.Unicode.GetBytes(data));
                        Console.WriteLine(Convert.ToBase64String(encrypted));
                        if(_currentPath == "./keys/Dovgodko_RSAPublicKey.xml")
                            Console.WriteLine(Encoding.Unicode.GetString(DecryptData(encrypted)));
                        break;
                }
            }
        } 
        public static string _currentPath;
        private static RSAParameters _privateKey, _currentPublicKey;
        public static void AssignNewKey()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                File.WriteAllText(_currentPath, rsa.ToXmlString(false));
                _privateKey = rsa.ExportParameters(true);
                _currentPublicKey = rsa.ExportParameters(false);
            }
        }
        public static void ChangeUser(string key)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(key);
                _currentPublicKey = rsa.ExportParameters(false);
            }
        }
        public static byte[] EncryptData(byte[] dataToEncrypt)
        {
            byte[] cypherBytes;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.ImportParameters(_currentPublicKey);
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
