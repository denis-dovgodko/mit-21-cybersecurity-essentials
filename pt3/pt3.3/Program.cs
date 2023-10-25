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
        static void Main(string[] args)
        {
            key password = new key();
            password.GenerateKey();
            while (true) {
                byte[] message = Encoding.Unicode.GetBytes(Console.ReadLine());
                byte[] hmac = HMAC(message, password);
                Console.WriteLine(sendForVerification(hmac, message, password));
            }
        }
        public class key
        {
            private int length = 8;
            private byte[] randomNumber;
            public void GenerateKey() 
            {
                var rng = new RNGCryptoServiceProvider();
                this.randomNumber = new byte[this.length];
                rng.GetBytes(randomNumber);
            }
            public byte[] get()
            { 
                return randomNumber;
            }
        }
        static byte[] HMAC(byte[] message, key password)
        {
            byte[] key = password.get();
            using (var hmac = new HMACSHA1(key))
            { 
                return hmac.ComputeHash(message);
            }
        }
        static bool sendForVerification(byte[] hmac, byte[] message, key password)
        {
            return hmac.SequenceEqual(HMAC(message, password));
        }
    }
}
