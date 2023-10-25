using System;
using System.Security.Cryptography;
using System.Text;

namespace pt3._4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string s;
                Console.WriteLine("Please, choose necessary command");
                s = Console.ReadLine();
                switch (s[0])
                {
                    case 's':
                        signUp();
                        break;
                    case 'l':
                        logIn();
                        break;
                    case 'e':
                        logOut();
                        break;
                }
            }
        }
        public class data
        {
            private string login;
            private byte[] password;
            public void addData(string Login, byte[] Password)
            {
                login = Login;
                var hash = SHA256.Create();
                password = hash.ComputeHash(Password);
            }
        }
        static void signUp()
        {
            Console.Clear();
            Console.WriteLine("Signing up");
            Console.WriteLine("Please, enter desirable login");
            string login = Console.ReadLine();
            Console.WriteLine("Please, enter password");
            byte[] password = Encoding.Unicode.GetBytes(Console.ReadLine());
            data data = new data();

        }
        static void logOut()
        {

        }
        static void logIn() {
        }
    }
}
