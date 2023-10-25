using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace pt3._4
{
    internal class Program
    {
        static void Main()
        {
            data list = new data();
            list = null;
            data cdata = list;
            while (true)
            {
                string s;
                Console.WriteLine("Please, choose necessary command");
                s = Console.ReadLine();
                switch (s[0])
                {
                    case 's':
                        SignUp(cdata);
                        break;
                    case 'l':
                        LogIn();
                        break;
                    case 'e':
                        LogOut();
                        break;
                }
            }
        }
        public class data
        {
            public string login;
            private byte[] hash_password;
            public bool logged = false;
            public void AddData(string Login, byte[] password)
            {
                login = Login;
                var hash = SHA256.Create();
                hash_password = hash.ComputeHash(password);
            }
            public data Next;
        }
        static void SignUp(data cdata)
        {
            Console.Clear();
            Console.WriteLine("Signing up");
            Console.WriteLine("Please, enter desirable login");
            string login = Console.ReadLine();
            Console.WriteLine("Please, enter password");
            byte[] password = Encoding.Unicode.GetBytes(Console.ReadLine());
            cdata.AddData(login, password);
            data nextCounter = new data();
            cdata.Next = nextCounter;
        }
        public data logged_link;
        public bool logged = false;
        static void LogIn(data currentUser)
        {
            LogOut();
            data logged_link = currentUser;
        }
        static void LogOut()
        {
            logged_link = null;
        }
        
    }
}
