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
            data RootList = new data(); // Корінь(початок) списку
            data cdata = RootList; // Поточний кінець списку зареєстрованих
            data logged = null; // Залогінений користувач
            while (true)
            {
                string s;
                if (logged != null)
                {
                    Console.WriteLine("Now logged: "+ logged.login);
                }
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
                        if (logged != null){
                            logged = LogOut(logged);
                        }
                        Console.WriteLine("Please, enter your login");
                        string login = Console.ReadLine();
                        data found = SearchInList(login, RootList);
                        if(found != null)
                            Console.WriteLine("Please, enter your password");
                            string password = Console.ReadLine();
                            var hash = SHA256.Create();
                            string GuidHashPassword = Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));
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
            private byte[] hash_password;
            public bool status = false;
            public void AddData(string Login, byte[] password)
            {
                login = Login;
                var hash = SHA256.Create();
                hash_password = hash.ComputeHash(password);
            }
            public byte[] GetPassword()
            {
                return hash_password;
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
            byte[] password = Encoding.Unicode.GetBytes(Console.ReadLine());
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
                if (curent.login ==  login)
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
    }
}
