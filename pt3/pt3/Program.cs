using System;
using System.Security.Cryptography;
using System.Text;

namespace pt3
{
    internal class Program
    {
        public class strHash {
            public string strForHash;
            public byte[] md5Hashed;
            public byte[] sha1Hashed;
            public byte[] sha256Hashed;
            public byte[] sha384Hashed;
            public byte[] sha512Hashed;
            public strHash(string value) {
                strForHash = value;
                md5Hashed = ComputeHashMd5(Encoding.Unicode.GetBytes(strForHash));
                Guid guid1 = new Guid(md5Hashed);
                sha1Hashed = ComputeHashSha1(Encoding.Unicode.GetBytes(strForHash));
                sha256Hashed = ComputeHashSha256(Encoding.Unicode.GetBytes(strForHash));
                sha384Hashed = ComputeHashSha384(Encoding.Unicode.GetBytes(strForHash));
                sha512Hashed = ComputeHashSha512(Encoding.Unicode.GetBytes(strForHash));
                Console.WriteLine($"Str:{strForHash}");
                Console.WriteLine($"Hash MD5:{Convert.ToBase64String(md5Hashed)}");
                Console.WriteLine($"GUID:{guid1}");
                Console.WriteLine($"Hash SHA1:{Convert.ToBase64String(sha1Hashed)}");
                Console.WriteLine($"Hash SHA256:{Convert.ToBase64String(sha256Hashed)}");
                Console.WriteLine($"Hash SHA384:{Convert.ToBase64String(sha384Hashed)}");
                Console.WriteLine($"Hash SHA512:{Convert.ToBase64String(sha512Hashed)}\n");
            }
            public strHash Next;
        }
        static void Main(string[] args)
        {
 
             const string strForHash1 = "Hello World!";
            const string strForHash2 = "Hello World!";
            const string strForHash3 = "Hello world!";
            strHash str1 = new strHash(strForHash1);
            strHash str2 = new strHash(strForHash2);
            strHash str3 = new strHash(strForHash3);        
        }
        static byte[] ComputeHashMd5(byte[] dataForHash)
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(dataForHash);
            }
        }
        public static byte[] ComputeHashSha1(byte[] toBeHashed)
        {
            using (var sha256 = SHA1.Create())
            {
                return sha256.ComputeHash(toBeHashed);
            }
        }
        public static byte[] ComputeHashSha256(byte[] toBeHashed)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(toBeHashed);
            }
        }
        public static byte[] ComputeHashSha384(byte[] toBeHashed)
        {
            using (var sha256 = SHA384.Create())
            {
                return sha256.ComputeHash(toBeHashed);
            }
        }
        public static byte[] ComputeHashSha512(byte[] toBeHashed)
        {
            using (var sha256 = SHA512.Create())
            {
                return sha256.ComputeHash(toBeHashed);
            }
        }

    }
}