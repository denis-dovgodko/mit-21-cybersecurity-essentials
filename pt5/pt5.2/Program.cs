using System;
using System.Buffers.Text;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace pt5._2
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Please, enter password");
            string passwordToHash = Console.ReadLine();
            for (int i = 9 * 10000; i < 9 * 10000 + (10 * 50000); i += 50000)
            {
                HashPassword(passwordToHash, i);
            }
            Console.ReadLine();
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
        private static void HashPassword(string passwordToHash, int numberOfRounds)
        {
            var sw = new Stopwatch();
            sw.Start();
            var hashedPassword = PBKDF2.HashPassword(Encoding.UTF8.GetBytes(passwordToHash), PBKDF2.GenerateSalt(), numberOfRounds);
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine("Password to hash : " + passwordToHash);
            Console.WriteLine("Hashed Password : " +
            Convert.ToBase64String(hashedPassword));
            Console.WriteLine("Iterations <" + numberOfRounds + ">Elapsed Time: " + sw.ElapsedMilliseconds + "ms");
        }
    }
}

