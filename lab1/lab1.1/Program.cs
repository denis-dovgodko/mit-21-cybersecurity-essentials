using System;

namespace lab1._1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random(673);
            for(int i = 0; i < 8; i++)
            {
                Console.WriteLine(random.Next(1,10));
            }
        }
    }
}
