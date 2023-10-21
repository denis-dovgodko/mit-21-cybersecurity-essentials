using System;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

namespace pt3._2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Guid guid = new Guid("564c8da6-0440-88ec-d453-0bbad57c6036");
            string password = indexStep(guid);
            Console.WriteLine(password);
        }
        static string indexStep(Guid guid)
        {
            byte[] md5Hashed;
            for (int i = 00000000; i < 100000000; i++)
            {
                string index = i.ToString();
                md5Hashed = ComputeHashMd5(Encoding.Unicode.GetBytes(index));
                Guid guid1 = new Guid(md5Hashed);
                if (guid1 == guid)
                {
                    return index;
                }
            }
            return "not founded";
        }
        static byte[] ComputeHashMd5(byte[] dataForHash)
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(dataForHash);
            }
        }
    }
}
