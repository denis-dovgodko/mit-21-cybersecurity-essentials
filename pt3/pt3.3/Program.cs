using System;
using System.Collections;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace pt3._3
{
    internal class Program
    {
        static void Main()
        {
            Key password = new Key();
            password.GenerateKey();
            while (true) {
                byte[] message = Encoding.Unicode.GetBytes(Console.ReadLine());
                byte[] hmac = HMAC(message, password);
                Console.WriteLine(SendForVerification(hmac, message, password));
            }
        }
        public class Key
        {
            public int Length = 8;
            private byte[] randomNumber;
            public void GenerateKey() 
            {
                var rng = new RNGCryptoServiceProvider();
                this.randomNumber = new byte[this.Length];
                rng.GetBytes(randomNumber);
            }
            public byte[] Get()
            { 
                return randomNumber;
            }
        }
        static byte[] HMAC(byte[] message, Key password)
        {
            byte[] key = password.Get();
            using (var hmac = new HMACSHA1(key))
            { 
                return hmac.ComputeHash(message);
            }
        }
        static bool SendForVerification(byte[] hmac, byte[] message, Key password)
        {
            return hmac.SequenceEqual(HMAC(message, password));
        }
    }
}
