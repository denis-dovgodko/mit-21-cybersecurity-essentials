using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace lab2._1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte[] inputData = File.ReadAllBytes("../../../data.txt").ToArray();
            byte[] encryptMes = new byte[inputData.Length];
            var rndNumberGenerator = new RNGCryptoServiceProvider();
            var key = new byte[inputData.Length];
            rndNumberGenerator.GetBytes(key);
            for (int i = 0; i < inputData.Length; i++)
            {
                encryptMes[i] = (byte)(inputData[i] ^ key[i]);
            }
            File.WriteAllBytes("../../../data.dat", encryptMes);
            Console.WriteLine("encrypted");
            decrypt(key);
        }
        static void decrypt(byte[] key)
        {
            byte[] encryptMes = File.ReadAllBytes("../../../data.dat").ToArray();
            byte[] outputData = new byte[encryptMes.Length];
            for (int i = 0; i < encryptMes.Length; i++)
            {
                outputData[i] = (byte)(encryptMes[i] ^ key[i]);
            }
            string decrypted = Encoding.UTF8.GetString(outputData);
            System.Console.WriteLine(decrypted);
        }
        
    }
}
