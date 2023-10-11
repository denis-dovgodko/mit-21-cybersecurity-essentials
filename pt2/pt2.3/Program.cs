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
            for (int begin = 0; begin < decData.Length-search.Length; begin++)
            {
                byte[] decrypMes = new byte[len];
                byte[] decryptKey = new byte[len];
                int bytes = 0;
                bytes = Find(search, decData, len, begin, decrypMes, bytes, decryptKey);
                while (bytes != 5)
                {
                    bytes = Find(search, decData, len, begin, decrypMes, bytes, decryptKey);
                }
                for (int i = begin + 5; i < 57; i++)
                {
                    int s = i - 5;
                    decrypMes[i] = (byte)(decData[i] ^ decryptKey[s]);
                    decryptKey[i] = decryptKey[s];
                }
                if(begin!=0) {
                    for (int i = begin - 1; i >= 0; i--)
                    {
                        int s = i + 5;
                        decrypMes[i] = (byte)(decData[i] ^ decryptKey[s]);
                        decryptKey[i] = decryptKey[s];
                    }
                }
                System.Console.WriteLine("Code: ");
                foreach (byte b in decryptKey)
                {
                    Console.Write(b.ToString("D") + " ");
                }
                System.Console.WriteLine();
                System.Console.WriteLine();
                System.Console.WriteLine("Text of .dat file: ");
                string encrypted = Encoding.UTF8.GetString(decrypMes);
                System.Console.WriteLine(encrypted);
                System.Console.WriteLine();
            }
        }

        static int Find(byte[] search, byte[] decData, int len, int begin, byte[] decrypMes, int bytes, byte[] decryptKey)
        {
            byte[] key = CreateKey(len);
            for (int i = begin, s = 0; i < begin+5; i++)
            {
                s = i-begin;
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