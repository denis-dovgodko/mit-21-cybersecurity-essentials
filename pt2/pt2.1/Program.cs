using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace lab2._1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte[] decData = File.ReadAllBytes("../../../data.txt").ToArray();
            byte[] encryptMes = new byte[decData.Length];
            var rndNumberGenerator = new RNGCryptoServiceProvider();
            var key = new byte[decData.Length];
            rndNumberGenerator.GetBytes(key);
            for(int i=0; i<decData.Length; i++)
            {
                encryptMes[i] = (byte)(decData[i] ^ key[i]);
            }
            File.WriteAllBytes("../../../data.dat", encryptMes);
            Console.WriteLine("done");
        }
    }
}
