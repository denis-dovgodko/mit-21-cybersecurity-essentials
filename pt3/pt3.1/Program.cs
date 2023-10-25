using System;
using System.Security.Cryptography;
using System.Text;

namespace pt3
{
    internal class Program
    {
        public class StrHash
        {
            private string strForHash;
            private byte[] md5Hashed;
            private byte[] sha1Hashed;
            private byte[] sha256Hashed;
            private byte[] sha384Hashed;
            private byte[] sha512Hashed;
            public void StrHashing(string value)
            {
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
            public StrHash Next;
        }
        static void Main()
        {
            string[] strForHash = new string[3];
            strForHash[0]= "Hello World!";
            strForHash[1] = "Hello World!";
            strForHash[2] = "Hello world!";
            StrHash str = new StrHash();
            for (int i=0; i<strForHash.Length; i++)
            {
                str.StrHashing(strForHash[i]);
                str.Next = new StrHash();
            }
        }
        static byte[] ComputeHashMd5(byte[] dataForHash)
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(dataForHash);
            }
        }
        static byte[] ComputeHashSha1(byte[] toBeHashed)
        {
            using (var sha256 = SHA1.Create())
            {
                return sha256.ComputeHash(toBeHashed);
            }
        }
        static byte[] ComputeHashSha256(byte[] toBeHashed)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(toBeHashed);
            }
        }
        static byte[] ComputeHashSha384(byte[] toBeHashed)
        {
            using (var sha256 = SHA384.Create())
            {
                return sha256.ComputeHash(toBeHashed);
            }
        }
        static byte[] ComputeHashSha512(byte[] toBeHashed)
        {
            using (var sha256 = SHA512.Create())
            {
                return sha256.ComputeHash(toBeHashed);
            }
        }

    }
}