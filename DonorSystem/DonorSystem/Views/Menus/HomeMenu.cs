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
    public class HomeMenu
    {

        /// <summary>Commands used in <see cref="Input"/>.</summary>
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

        /// <summary>Initializes a new instance of the <see cref="T:DonorSystem.Views.HomeMenu" /> class and starts the whole program.</summary>
        public HomeMenu()
        {
            Console.Title = "Donor System";
            homeController = new HomeController();
            Input();
        }

        /// <summary>
        /// <summary>Displays main menu.</summary>
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

        /// <summary>Gets user input, validates and displays main menu.</summary>
        /// <exception cref="FormatException">Value must be an integer.</exception>
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
                }
            } while (command != (int)Command.Exit);
        }

        /// <summary>Gets user information, validates and calls <see cref="homeController"/> to login the user.</summary>
        /// <exception cref="FormatException">Value must be an integer.</exception>
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

            switch (role)
            {
                case 1:
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

                    break;
                case 2:
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

                    break;

            }
            Return();
        }

        /// <summary>Gets user email and password, validates, and calls <see cref="homeController"/> to register user.</summary>
        /// <exception cref="FormatException">Value must be an integer.</exception>
        /// <exception cref="Exception">Invalid email</exception>
        /// <exception cref="Exception">Email already in use.</exception>
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
                if (homeController.EmailAlreadyExists(email, role)) throw new Exception("Email already in use.");
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
            if (homeController.InvalidPassword(password)) 
            {
                Console.WriteLine("Password must be at least 6 symbols.");
                Return();
                return;
            }

            Console.Write("Repeat password: ");
            string repeatedPassword = Console.ReadLine();
            if (homeController.PasswordMismatch(password, repeatedPassword))
            {
                Console.WriteLine("Password mismatch.");
                Return();
                return;
            }

            password = homeController.HashPassword(password);

            switch (role)
            {
                case 1:
                {
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    if (homeController.InvalidName(name))
                    {
                        Console.WriteLine("Name cannot contain other symbols or be less than 2 letters.");
                        Return();
                        return;
                    }

                    Console.Write("Phone number: ");
                    string phoneNumber = Console.ReadLine();
                    if (homeController.InvalidPhoneNumber(phoneNumber))
                    {
                        Console.WriteLine("Phone number should be 10 symbols and all numbers.");
                        Return();
                        return;
                    }

                    string status = "Available";

                    Console.Write("Blood group: ");
                    string bloodGroup = Console.ReadLine();
                    if (homeController.InvalidBloodGroup(bloodGroup))
                    {
                        Console.WriteLine("Blood group is invalid");
                        Return();
                        return;
                    }

                    if (homeController.RegisterAsDonor(email, password, name, phoneNumber, status, bloodGroup))
                    {
                        Console.WriteLine("Register successful! Redirecting...");
                        donorMenu = new DonorMenu(homeController.LoginAsDonor(email, password));
                    }
                    else
                    {
                        Console.WriteLine("Register failed. Please try again.");
                    }

                    break;
                }
                case 2:
                {
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    if (homeController.InvalidName(name))
                    {
                        Console.WriteLine("Name cannot contain other symbols or be less than 2 letters.");
                        Return();
                        return;
                    }

                    Console.Write("Phone number: ");
                    string phoneNumber = Console.ReadLine();
                    if (homeController.InvalidPhoneNumber(phoneNumber))
                    {
                        Console.WriteLine("Phone number should be 10 symbols and all numbers.");
                        Return();
                        return;
                    }

                    Console.Write("Blood group: ");
                    string bloodGroup = Console.ReadLine();
                    if (homeController.InvalidBloodGroup(bloodGroup))
                    {
                        Console.WriteLine("Enter a valid blood group.");
                        Return();
                        return;
                    }

                    Console.Write("Diagnose: ");
                    string diagnose = Console.ReadLine();
                    if (homeController.InvalidDiagnose(diagnose))
                    {
                        Console.WriteLine("Diagnose cannot be less than 3 symbols and must be all letters.");
                        Return();
                        return;
                    }

                    if (homeController.RegisterAsPatient(email, password, name, phoneNumber, bloodGroup, diagnose))
                    {
                        Console.WriteLine("Register successful! Redirecting...");
                        patientMenu = new PatientMenu(homeController.LoginAsPatient(email, password));
                    }
                    else
                    {
                        Console.WriteLine("Register failed. Please try again.");
                    }
                    break;
                }
            }
            Return();
        }

        /// <summary>Calls <see cref="homeController"/> to show useful websites.</summary>
        private void ShowUsefulInfo()
        {
            Console.Clear();
            Console.WriteLine("Websites that may be useful to donors and patients: ");
            Console.WriteLine(homeController.GetUsefulInfo());
            Return();
        }

        /// <summary> Resets colors and waits for user to return to the main menu by pressing a key. </summary>
        private void Return()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Press any key to return.");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
