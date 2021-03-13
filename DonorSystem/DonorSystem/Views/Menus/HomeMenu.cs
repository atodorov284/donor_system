using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using DonorSystem.Controllers;

namespace DonorSystem.Views
{
    /// <summary>
    /// Отговаря за началното меню от конзолния интерфейс,
    /// навигира към останалите менюта.
    /// </summary>
    class HomeMenu
    {
        private const int Exit = 4;
        HomeController homeController;

        /// <summary>
        /// Създава контролер и започва цикъл за въвеждане на информация от и към потребителя.
        /// </summary>
        public HomeMenu()
        {
            homeController = new HomeController();
            Input();
        }

        /// <summary>
        /// Извежда началното меню на конзолата.
        /// </summary>
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

        /// <summary>
        /// Обработва въведения от потребителя избор за навигация.
        /// </summary>
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

        /// <summary>
        /// Въвеждат се данни на съществуващ акаунт и се влиза в системата.
        /// </summary>
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

        /// <summary>
        /// Въвеждат се данни за несъществуващ акаунт и го регистрира вф системата.
        /// </summary>
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
            try
            {
                if (!ValidateEmail(email)) throw new Exception("Invalid email");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press any key to return.");
                Console.ReadKey();
                return;
            }
            Console.Write("Password: ");
            string password = Console.ReadLine();
            if (password.Length < 6) 
            {
                Console.WriteLine("Password must be atleast 6 symbols.");
                Console.WriteLine("Press any key to return.");
                Console.ReadKey();
                return;
            }
            Console.Write("Repeat password: ");
            string repeatedPassword = Console.ReadLine();
            if (password != repeatedPassword)
            {
                Console.WriteLine("Password mismatch.");
                Console.WriteLine("Press any key to return.");
                Console.ReadKey();
                return;
            }
            password = HashPassword(password);
            homeController.Register(email, password, role);
        }

        /// <summary>
        /// Генерира кодирана версия на парола.
        /// </summary>
        /// <param name="password">Стринг с некодираната парол.а</param>
        /// <returns>Връща кодираната стойност на паролата.</returns>
        private string HashPassword(string password)
        {
            var provider = new SHA1CryptoServiceProvider();
            var encoding = new UnicodeEncoding();
            return Convert.ToBase64String(provider.ComputeHash(encoding.GetBytes(password)));
        }

        /// <summary>
        /// Извежда външни линкове на конзолния интерфейс.
        /// </summary>
        private void ShowUsefulInfo()
        {
            homeController.ShowUsefulInfo();
        }

        /// <summary>
        /// Проверява дали имейл адресът съществува
        /// </summary>
        /// <param name="email">Имейл адрес</param>
        /// <returns>Връща true при въвеждане на съществуващ в системата имейл адрес, в обратен случай връща false.</returns>
        private bool ValidateEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }
    }
}
