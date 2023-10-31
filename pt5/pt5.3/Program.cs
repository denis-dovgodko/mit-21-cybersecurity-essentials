using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace pt5._3
{
    internal class Program
    {
        static void Main()
        {
            data RootList = new data(); // Корінь(початок) списку
            data cdata = RootList; // Поточний кінець списку зареєстрованих
            data logged = null; // Залогінений користувач
            while (true)
            {
                string s;
                if (logged != null)
                {
                    Console.WriteLine("Now logged: " + logged.login);
                }
                Console.WriteLine("Available commands:");
                Console.WriteLine("s - Sign up");
                Console.WriteLine("l - Log in");
                Console.WriteLine("e - Log out");
                Console.WriteLine("Please, choose necessary command");
                s = Console.ReadLine();
                switch (s[0])
                {
                    case 's':
                        data newcdata = SignUp(cdata);
                        if (logged != null)
                        {
                            logged = LogOut(logged);
                        }
                        logged = LogIn(cdata);
                        Console.WriteLine("Success");
                        cdata = newcdata;
                        break;
                    case 'e':
                        if (logged != null)
                        {
                            logged = LogOut(logged);
                        }
                        break;
                    case 'l':
                        if (logged != null)
                        {
                            logged = LogOut(logged);
                        }
                        Console.WriteLine("Please, enter your login");
                        string login = Console.ReadLine();
                        data found = SearchInList(login, RootList);
                        if (found != null)
                            Console.WriteLine("Please, enter your password");
                        string password = Console.ReadLine();
                        string GuidHashPassword = Convert.ToBase64String(PBKDF2.HashPassword(Encoding.UTF8.GetBytes(password), found.GetSalt(), 20 * 10000));
                        if (GuidHashPassword == Convert.ToBase64String(found.GetPassword()))
                        {
                            Console.WriteLine("Success!");
                            logged = LogIn(found);
                        }
                        else
                        {
                            Console.WriteLine("Wrong password!");
                        }
                        break;
                }
            }
        }
        public class data
        {
            public string login;
            private byte[] SaltedHash_password;
            private byte[] Salt;
            public bool status = false;
            public void AddData(string Login, string password)
            {
                login = Login;
                Salt = PBKDF2.GenerateSalt();
                SaltedHash_password = PBKDF2.HashPassword(Encoding.UTF8.GetBytes(password), Salt, 20 * 10000);
            }
            public byte[] GetPassword()
            {
                return SaltedHash_password;
            }
            public byte[] GetSalt()
            {
                return Salt;
            }
            public data Next;
        }
        static data SignUp(data cdata)
        {
            Console.Clear();
            Console.WriteLine("Signing up");
            Console.WriteLine("Please, enter desirable login");
            string login = Console.ReadLine();
            Console.WriteLine("Please, enter password");
            string password = Console.ReadLine();
            cdata.AddData(login, password);
            data nextCounter = new data();
            cdata.Next = nextCounter;
            return nextCounter;
        }
        static data LogIn(data currentUser)
        {
            currentUser.status = true;
            return currentUser;
        }
        static data LogOut(data logged)
        {
            logged.status = false;
            Console.WriteLine(logged.login + " logged out");
            return null;
        }
        static data SearchInList(string login, data curent)
        {
            while (curent != null)
            {
                if (curent.login == login)
                {
                    return curent;
                }
                else
                {
                    curent = curent.Next;
                }
            }
            return null;
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
    }
}
