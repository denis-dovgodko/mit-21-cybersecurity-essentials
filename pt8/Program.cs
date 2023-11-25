using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace pt8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            File.WriteAllBytes("./messages/recieved/DashkovskiyMessage.dat", Convert.FromBase64String("A7lP5ZQ3CWO00boQhmbkMzRVIdn6+9g7/iJQqH0RjDhpp4i6k7HUKucnI4TsPfezqJDmhtzZ+5Jovq5SRZOdncS2D0EEim+QcZ7qjS+sHVQzdXmfzgxlabiHLmg34VpkqSmf8E99xsG2at8MeByB82YkDycdptKW7U++KHU+coU="));
            _currentPath = "./keys/Dovgodko_RSAPublicKey.xml";
            surname = "Dovgodko";
            if (File.Exists(_currentPath))
            {
                var keyFile = File.ReadAllText(_currentPath);
                using (var rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(keyFile);
                    _currentPublicKey = rsa.ExportParameters(false);
                }
            }
            while (true)
            {
                Console.WriteLine("Choose command: ");
                Console.WriteLine("r - reset(set) keys pair [undesirable]");
                Console.WriteLine("c - cypher message for someone");
                Console.WriteLine("d - decrypt&view recieved message");
                Console.WriteLine("u - change data's destination user(public key)");
                var a = Console.ReadKey();
                switch (a.KeyChar)
                {
                    case 'u':
                        Console.Clear();
                        Console.WriteLine("Available keys: ");
                        string[] files = Directory.GetFiles("./keys");
                        foreach (string b in files)
                        {
                            Console.WriteLine(b);
                        }
                        Console.WriteLine("Please, enter surname of chosen key's owner");
                        var sname = Console.ReadLine();
                        _currentPath = "./keys/" + sname + "_RSAPublicKey.xml";
                        try
                        {
                            ChangeUser(File.ReadAllText(_currentPath));
                            surname = sname;
                        }
                        catch 
                        {
                            Console.WriteLine("Mistake: Incorrect surname(chosen key unreachable)");
                        }
                        break;
                     case 'r':
                        Console.WriteLine("Do you sure? It will cause to lose of last keys pair and make recieved encrypted messages unreachable");
                        Console.WriteLine("[Y/n]");
                        var submit = Console.ReadKey();
                        if (submit.KeyChar == 'Y')
                        {
                           AssignNewKey();
                        }
                        else
                        {
                           break;
                        }
                        break;
                     case 'c':
                        Console.WriteLine("Enter message: ");
                        string data = Console.ReadLine();
                        if (File.Exists(_currentPath))
                        {
                            byte[] cypher = EncryptData(Encoding.UTF8.GetBytes(data));
                            if (surname == "Dovgodko")
                            {
                                File.WriteAllBytes("./messages/recieved/DovgodkoMessage.dat", cypher);
                            }
                            else
                            {
                                File.WriteAllBytes("./messages/to_send/MessageFor" + surname + ".dat", cypher);
                            }
                        }
                        else
                        {
                            Console.WriteLine("You didn't create keys pair!");
                        }
                        break;
                     case 'd':
                        string[] recievedMessages = Directory.GetFiles("./messages/recieved");
                        foreach (string b in recievedMessages)
                        {
                           Console.WriteLine(b);
                        }
                        Console.WriteLine("Please, enter surname of message's creator");
                        string SName = Console.ReadLine();
                        try
                        {
                            string path = "./messages/recieved/" + SName+"Message.dat";
                            Console.WriteLine(Encoding.UTF8.GetString(DecryptData(File.ReadAllBytes(path))));
                        }
                        catch
                        {
                            Console.WriteLine("Mistake: Incorrect surname(chosen key unreachable) or private key not founded");
                        }
                        break;
                }
            }
        }
        public static string _currentPath;
        public static string surname;
        private static RSAParameters _currentPublicKey;
        private readonly static string CspContainerName = "RSAContainer";
        public static void AssignNewKey()
        {
            CspParameters cspParameters = new CspParameters(1)
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore,
                ProviderName = "Microsoft Strong Cryptographic Provider"
            };
            using (var rsa = new RSACryptoServiceProvider(cspParameters))
            {
                rsa.PersistKeyInCsp = true;
                _currentPublicKey = rsa.ExportParameters(false);
                File.WriteAllText("./keys/Dovgodko_RSAPublicKey.xml", rsa.ToXmlString(false));
            };
        }
        public static void ChangeUser(string key)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(key);
                _currentPublicKey = rsa.ExportParameters(false);
            }
        }
        public static byte[] EncryptData(byte[] dataToEncrypt)
        {
            byte[] cypherBytes;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.ImportParameters(_currentPublicKey);
                cypherBytes = rsa.Encrypt(dataToEncrypt, true);
            }
            return cypherBytes;
        }
        public static byte[] DecryptData(byte[] dataToDecrypt)
        {
            byte[] plainBytes;
            var cspParams = new CspParameters
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            using (var rsa = new RSACryptoServiceProvider(cspParams))
            {
                rsa.PersistKeyInCsp = true;
                plainBytes = rsa.Decrypt(dataToDecrypt, true);
            }
            return plainBytes;
        }
    }
}
