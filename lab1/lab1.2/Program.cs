using System;
using System.Security.Cryptography;

namespace lab1._2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var rndNumberGenerator = new RNGCryptoServiceProvider();
            int length = 10;
            var randomNumber = new byte[length];
            rndNumberGenerator.GetBytes(randomNumber);
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine(randomNumber[i]);
            }
        }
    }
}
