using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace pt5._1
{
    internal class Program
    {
        static void Main()
        {
            SaltedHash passwords = new SaltedHash();
            while (true)
            {
                byte[] password = Encoding.Unicode.GetBytes(Console.ReadLine());
                passwords.GetHash(password);
                SaltedHash nextCounter = new SaltedHash();
                passwords.Next = nextCounter;
            }
        }
        public class SaltedHash
        {
            private byte[] Salt;
            private byte[] SaltedHashPasswd;
            public SaltedHash Next;
            public void GetHash(byte[] Passwd)
            {
                var rng = new RNGCryptoServiceProvider();
                Salt = new byte[32];
                rng.GetBytes(Salt);
                var ret = new byte[Salt.Length + Passwd.Length];
                Buffer.BlockCopy(Salt, 0, ret, 0, Salt.Length);
                Buffer.BlockCopy(Passwd, 0, ret, Salt.Length, Passwd.Length);
                using (var sha256 = SHA256.Create())
                {
                    SaltedHashPasswd = sha256.ComputeHash(ret);
                }
                Console.WriteLine("Salt: " + Convert.ToBase64String(Salt));
                Console.WriteLine("SaltedHash: "+ Convert.ToBase64String(SaltedHashPasswd));
            }
        }
    }
}
