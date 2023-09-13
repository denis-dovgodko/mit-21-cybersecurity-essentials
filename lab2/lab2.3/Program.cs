using Microsoft.VisualBasic;
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
            Console.OutputEncoding = Encoding.UTF8;
            byte[] decData = File.ReadAllBytes("../../../encfile.dat").ToArray();
            int len = decData.Length;
            string data = "Mit21";
            byte[] search = Encoding.UTF8.GetBytes(data);
            byte[] decrypMes = new byte[len];
            byte[] decryptKey = new byte[len];
            int bytes = 0;
            bytes = Find(search, decData, len, decrypMes,bytes,decryptKey);
            while (bytes != decData.Length)
            {
                bytes = Find(search, decData, len, decrypMes,bytes, decryptKey);
            }
            Console.OutputEncoding = Encoding.UTF8;
            System.Console.WriteLine("Code: ");
            foreach (byte b in decryptKey)
            {
                Console.Write(b.ToString("D") + " ");
            }
            System.Console.WriteLine();
            System.Console.WriteLine("Text of .dat file: ");
            string encrypted = Encoding.UTF8.GetString(decrypMes);
            System.Console.WriteLine(encrypted);
        }

        static int Find(byte[] search, byte[] decData, int len, byte[] decrypMes, int bytes, byte[] decryptKey)
        {
            byte[] key = CreateKey(len);
            for (int i = 0, s = 0; i < 57; i++)
            {
                s = i % 5;
                if ((byte)(decData[i] ^ key[i]) == search[s] && decrypMes[i] ==0 && decryptKey[i] ==0)
                {
                    decrypMes[i] = (byte)(decData[i] ^ key[i]);
                    decryptKey[i] = key[i];
                    bytes++;
                }
            }
            return bytes;
        }

        static byte[] CreateKey(int len)
        {
            var rndNumberGenerator = new RNGCryptoServiceProvider();
            var key = new byte[len];
            rndNumberGenerator.GetBytes(key);
            return key;
        }
    }
}