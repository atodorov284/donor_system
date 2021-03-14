using System;
using System.Security.Cryptography;
using System.Text;
using DonorSystem.Controllers;

namespace DonorSystem.Views
{
    class HomeMenu
    {
        /// <summary>The exit value used to exit the <see cref="Input"/> method.</summary>
        private const int Exit = 4;
        readonly HomeController homeController;
        /// <summary>Initializes a new instance of the <see cref="T:DonorSystem.Views.HomeMenu" /> class and starts the whole program.</summary>
        public HomeMenu()
        {
            homeController = new HomeController();
            Input();
        }
        /// <summary>Displays main menu.</summary>
        private static void ShowMenu()
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
        private void Input()
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
                }
            } while (command != Exit);
        }

        /// <summary>Gets user information, validates and calls <see cref="homeController"/> to login the user.</summary>
        /// <exception cref="FormatException">Value must be an integer.</exception>
        private void Login()
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

        /// <summary>Gets user email and password, validates, and calls <see cref="homeController"/> to register user.</summary>
        /// <exception cref="FormatException">Value must be an integer.</exception>
        /// <exception cref="Exception">Invalid email</exception>
        /// <exception cref="Exception">Email already in use.</exception>
        private void Register()
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
                if (ExistingEmail(email, role)) throw new Exception("Email already in use.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press any key to return.");
                Console.ReadKey();
                return;
            }

            Console.Write("Password: ");
            string password = Console.ReadLine();
            if (!ValidatePassword(password)) 
            {
                Console.WriteLine("Password must be at least 6 symbols.");
                Console.WriteLine("Press any key to return.");
                Console.ReadKey();
                return;
            }

            Console.Write("Repeat password: ");
            string repeatedPassword = Console.ReadLine();
            if (!PasswordMatch(password, repeatedPassword))
            {
                Console.WriteLine("Password mismatch.");
                Console.WriteLine("Press any key to return.");
                Console.ReadKey();
                return;
            }

            password = HashPassword(password);

            homeController.Register(email, password, role);
        }

        /// <summary>Hashes the password.</summary>
        /// <param name="password">The password.</param>
        /// <returns>Hashed password</returns>
        private static string HashPassword(string password)
        {
            var provider = new SHA1CryptoServiceProvider();
            var encoding = new UnicodeEncoding();
            return Convert.ToBase64String(provider.ComputeHash(encoding.GetBytes(password)));
        }

        /// <summary>Calls <see cref="homeController"/> to show useful websites.</summary>
        private void ShowUsefulInfo()
        {
            homeController.ShowUsefulInfo();
        }

        /// <summary>Calls <see cref="homeController"/> to validate the password.</summary>
        /// <param name="password">The password.</param>
        /// <returns>
        ///   <c>true</c> if password is valid, <c>false</c> otherwise.</returns>
        private bool ValidatePassword(string password)
        {
            return homeController.ValidatePassword(password);
        }

        /// <summary>Calls <see cref="homeController"/> to validate the email.</summary>
        /// <param name="email">The email.</param>
        /// <returns>
        ///   <c>true</c> if email is valid, <c>false</c> otherwise.</returns>
        private bool ValidateEmail(string email)
        {
            return homeController.ValidateEmail(email);
        }

        /// <summary>Checks if the passwords match.</summary>
        /// <param name="password">The password.</param>
        /// <param name="repeated">The repeated password.</param>
        /// <returns>
        ///   <c>true</c> if equal, <c>false</c> otherwise.</returns>
        private bool PasswordMatch(string password, string repeated)
        {
            return password == repeated;
        }

        /// <summary>Calls <see cref="homeController"/> to see if the email already exists based on the role.</summary>
        /// <param name="email">The email.</param>
        /// <param name="role">The role.</param>
        /// <returns>
        ///   <c>true</c> if exists, <c>false</c> otherwise.</returns>
        private bool ExistingEmail(string email, int role)
        {
            bool isDonor = role == 1;
            return homeController.ExistingEmail(email, isDonor);
        }
    }
}
