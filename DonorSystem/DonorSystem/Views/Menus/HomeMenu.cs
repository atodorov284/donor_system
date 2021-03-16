using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using DonorSystem.Controllers;
//using DonorSystem.Models;

namespace DonorSystem.Views
{
    /// <summary>
    /// Отговаря за началното меню от конзолния интерфейс,
    /// навигира към останалите менюта.
    /// </summary>
    public class HomeMenu
    {
        /// <summary>
        /// Изброява видовете команди, които потребителя може да ползва.
        /// </summary>
        private enum Command : int
        {
            Login = 1,
            Register = 2,
            Info = 3,
            Exit = 4
        }
        private HomeController homeController;
        private DonorMenu donorMenu;
        private PatientMenu patientMenu;

        /// <summary>
        /// Създава контролер и започва цикъл за въвеждане на информация от и към потребителя.
        /// </summary>
        public HomeMenu()
        {
            Console.Title = "Donor system";
            homeController = new HomeController();
            Input();
        }

        /// <summary>
        /// Извежда началното меню на конзолата.
        /// </summary>
        private void ShowMenu()
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
                    if (int.TryParse(Console.ReadLine(), out command) == false) throw new FormatException("Value must be an integer.");
                } 
                catch(FormatException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Return();
                    //return;
                }
                
                switch (command)
                {
                    case (int)Command.Login:
                        Login();
                        break;
                    case (int)Command.Register:
                        Register();
                        break;
                    case (int)Command.Info:
                        ShowUsefulInfo();
                        break;
                    default:
                        break;
                }
            } while (command != (int)Command.Exit);
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
                    if (int.TryParse(Console.ReadLine(), out role) == false) throw new FormatException("Value must be an integer.");
                }
                catch (FormatException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Return();
                }
            } while (role != 1 && role != 2);
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Password: ");
            string password = homeController.HashPassword(Console.ReadLine());


            if (role == 1)
            {
                var donor = homeController.LoginAsDonor(email, password);
                if (donor != null)
                {
                    donorMenu = new DonorMenu(donor);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid credentials.");
                    Console.ResetColor();
                }
            }
            else
            {
                var patient = homeController.LoginAsPatient(email, password);
                if (patient != null)
                {
                    patientMenu = new PatientMenu(patient);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid credentials.");
                    Console.ResetColor();
                }
            }
            Return();

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
                    if (int.TryParse(Console.ReadLine(), out role) == false) throw new FormatException("Value must be an integer.");
                }
                catch (FormatException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Return();
                }
            } while (role != 1 && role != 2);
            Console.Write("Email: ");
            string email = Console.ReadLine();
            try
            {
                Console.ForegroundColor = ConsoleColor.Red;
                if (homeController.ValidateEmail(email) == false) throw new Exception("Invalid email");
                Console.ResetColor();
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Return();
                return;
            }
            Console.Write("Password: ");
            string password = Console.ReadLine();
            if (password.Length < 6) 
            {
                Console.WriteLine("Password must be atleast 6 symbols.");
                Return();
                return;
            }
            Console.Write("Repeat password: ");
            string repeatedPassword = Console.ReadLine();
            if (password != repeatedPassword)
            {
                Console.WriteLine("Password mismatch.");
                Return();
                return;
            }
            password = homeController.HashPassword(password);


            if (role == 1)
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();
                if (name.Any(char.IsSymbol) || name.Any(char.IsDigit) || name.Length < 2)
                {
                    Console.WriteLine("Name cannot contain other symbols or be less than 2 letters.");
                    Return();
                    return;
                }
                Console.Write("Phone number: ");
                string phoneNumber = Console.ReadLine();
                if (phoneNumber.Length != 10 || phoneNumber.All(char.IsDigit) == false)
                {
                    Console.WriteLine("Phone number should be 10 symbols and all numbers.");
                    Return();
                    return;
                }
                string status = "Available";
                Console.Write("Blood group: ");
                string bloodGroup = Console.ReadLine();
                if (!(bloodGroup[0] != 'A' || bloodGroup[0] != 'B') && !(bloodGroup[1] != '+' || bloodGroup[1] != '-'))
                {
                    Console.WriteLine("Enter a valid blood group");
                    Return();
                    return;
                }

                if (homeController.RegisterAsDonor(email, password, name, phoneNumber, status, bloodGroup) == true)
                {
                    Console.WriteLine("Register successful! Redirecting...");
                    donorMenu = new DonorMenu(homeController.LoginAsDonor(email, password));
                }
                else
                {
                    Console.WriteLine("Email already in use. Please use a different email.");
                }
            }
            else if (role == 2)
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();
                if (name.Any(char.IsSymbol) || name.Any(char.IsDigit) || name.Length < 2)
                {
                    Console.WriteLine("Name cannot contain other symbols or be less than 2 letters.");
                    Return();
                    return;
                }
                Console.Write("Phone number: ");
                string phoneNumber = Console.ReadLine();
                if (phoneNumber.Length != 10 || phoneNumber.All(char.IsDigit) == false)
                {
                    Console.WriteLine("Phone number should be 10 symbols and all numbers.");
                    Return();
                    return;
                }
                Console.Write("Blood group: ");
                string bloodGroup = Console.ReadLine();
                if (!(bloodGroup[0] != 'A' || bloodGroup[0] != 'B') && !(bloodGroup[1] != '+' || bloodGroup[1] != '-'))
                {
                    Console.WriteLine("Enter a valid blood group.");
                    Return();
                    return;
                }
                Console.Write("Diagnose: ");
                string diagnose = Console.ReadLine();
                if (diagnose.Length < 3 || name.Any(char.IsSymbol) || name.Any(char.IsDigit))
                {
                    Console.WriteLine("Diagnose cannot be less than 3 symbols and must be all letters.");
                    Return();
                    return;
                }

                if (homeController.RegisterAsPatient(email, password, name, phoneNumber, bloodGroup, diagnose) == true)
                {
                    Console.WriteLine("Register successful! Redirecting...");
                    patientMenu = new PatientMenu(homeController.LoginAsPatient(email, password));
                }
                else
                {
                    Console.WriteLine("Email already in use. Please use a different email.");
                }

            }
            Return();
        }

        /// <summary>
        /// Извежда полезна информация на конзолния интерфейс.
        /// </summary>
        private void ShowUsefulInfo()
        {
            Console.Clear();
            Console.WriteLine("Websites that may be useful to donors and patients: ");
            Console.WriteLine(homeController.GetUsefulInfo());
            Return();
        }

        /// <summary>
        /// Връща към началото на менюто.
        /// </summary>
        private void Return()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Press any key to return.");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
