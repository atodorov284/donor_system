using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using DonorSystem.Controllers;

namespace DonorSystem.Views
{
    class HomeMenu
    {
        private const int Exit = 4;
        HomeController homeController;
        public HomeMenu()
        {
            homeController = new HomeController();
            Input();
        }

        public void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the blood donor app.");
            Console.WriteLine();
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Useful info");
            Console.WriteLine("4. Exit");
        }

        public void Input()
        {
            int command = 0;
            do
            {
                ShowMenu();
                Console.Write("Your choice: ");
                try
                {
                    if (!int.TryParse(Console.ReadLine(), out command)) throw new FormatException("Value must be an integer.");
                } 
                catch(FormatException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Press any key to return.");
                    Console.ReadKey();
                }
                
                switch (command)
                {
                    case 1:
                        Login();
                        break;
                    case 2:
                        Register();
                        break;
                    case 3:
                        ShowUsefulInfo();
                        break;
                    default:
                        break;
                }
            } while (command != Exit);
        }

        public void Login()
        {
            int role = 0;
            do
            {
                Console.Clear();
                Console.Write("Enter 1 for donor and 2 for patient: ");
                try
                {
                    if (!int.TryParse(Console.ReadLine(), out role)) throw new FormatException("Value must be an integer.");
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Press any key to return.");
                    Console.ReadKey();
                }
            } while (role != 1 && role != 2);
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Password: ");
            string password = HashPassword(Console.ReadLine());
            homeController.Login(email, password, role);
        }

        public void Register()
        {
            int role = 0;
            do
            {
                Console.Clear();
                Console.Write("Enter 1 for donor and 2 for patient: ");
                try
                {
                    if (!int.TryParse(Console.ReadLine(), out role)) throw new FormatException("Value must be an integer.");
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Press any key to return.");
                    Console.ReadKey();
                }
            } while (role != 1 && role != 2);
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Password:");
            string password = HashPassword(Console.ReadLine());
            homeController.Register(email, password, role);
        }

        private string HashPassword(string password)
        {
            var provider = new SHA1CryptoServiceProvider();
            var encoding = new UnicodeEncoding();
            return Convert.ToBase64String(provider.ComputeHash(encoding.GetBytes(password)));
        }

        private void ShowUsefulInfo()
        {
            homeController.ShowUsefulInfo();
        }

    }
}
