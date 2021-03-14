using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DonorSystem.DAO;
using DonorSystem.Models;

namespace DonorSystem.Controllers
{
    class HomeController
    {
        readonly HomeDAO homeDAO;
        readonly WebsitesDAO websitesDAO;

        /// <summary>Calls <see cref="homeDAO"/> to see if the email already exists based on the role.</summary>
        /// <param name="email">The email.</param>
        /// <param name="isDonor">The role</param>
        /// <returns>
        ///   <c>true</c> if exists, <c>false</c> otherwise.</returns>
        public bool ExistingEmail(string email, bool isDonor)
        {
            return homeDAO.ExistingEmail(email, isDonor);
        }

        /// <summary>Validates the password.</summary>
        /// <param name="password">The password.</param>
        /// <returns>
        ///   <c>true</c> if password length is greater or equal to 6, <c>false</c> otherwise.</returns>
        public bool ValidatePassword(string password)
        {
            return password.Length >= 6;
        }

        /// <summary>Validates the email.</summary>
        /// <param name="email">The email.</param>
        /// <returns>
        ///   <c>true</c> if <see cref="EmailAddressAttribute"/> validates the email, <c>false</c> otherwise.</returns>
        public bool ValidateEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }


        /// <summary>Calls <see cref="websitesDAO"/> to show useful websites.</summary>
        public void ShowUsefulInfo()
        {
            Console.Clear();
            Console.WriteLine("Websites that may be useful to donors and patients: ");
            websitesDAO.ShowAll();
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }

        /// <summary>Calls <see cref="homeDAO"/> to login with the credentials based on the role.</summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="role">The role.</param>
        public void Login(string email, string password, int role)
        {
            switch (role)
            {
                case 1:
                    homeDAO.DonorLogin(email, password);
                    break;
                case 2:
                    homeDAO.PatientLogin(email, password);
                    break;
            }
        }

        /// <summary>Collects user information based on the role, validates, and calls <see cref="homeDAO"/> to register the user.</summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="role">The role.</param>
        public void Register(string email, string password, int role)
        {
            switch (role)
            {
                case 1:
                {
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    if (InvalidName(name))
                    {
                        Console.WriteLine("Name cannot contain other symbols or be less than 2 letters.");
                        Console.WriteLine("Press any key to return.");
                        Console.ReadKey();
                        return;
                    }
                    Console.Write("Phone number: ");
                    string phoneNumber = Console.ReadLine();
                    if (InvalidPhoneNumber((phoneNumber)))
                    {
                        Console.WriteLine("Phone number should be 10 symbols and all numbers.");
                        Console.WriteLine("Press any key to return.");
                        Console.ReadKey();
                        return;
                    }
                    string status = "Available";
                    Console.Write("Blood group: ");
                    string bloodGroup = Console.ReadLine();
                    if (InvalidBloodGroup(bloodGroup))
                    {
                        Console.WriteLine("Enter a valid blood group");
                        Console.WriteLine("Press any key to return.");
                        Console.ReadKey();
                        return;
                    }

                    Donors donor = new Donors
                    {
                        Email = email,
                        Password = password,
                        Name = name,
                        PhoneNumber = phoneNumber,
                        Status = status,
                        BloodGroup = bloodGroup
                    };
                    homeDAO.DonorRegister(donor);
                    break;
                }
                case 2:
                {
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    if (InvalidName((name)))
                    {
                        Console.WriteLine("Name cannot contain other symbols or be less than 2 letters.");
                        Console.WriteLine("Press any key to return.");
                        Console.ReadKey();
                        return;
                    }
                    Console.Write("Phone number: ");
                    string phoneNumber = Console.ReadLine();
                    if (InvalidPhoneNumber(phoneNumber))
                    {
                        Console.WriteLine("Phone number should be 10 symbols and all numbers.");
                        Console.WriteLine("Press any key to return.");
                        Console.ReadKey();
                        return;
                    }
                    Console.Write("Blood group: ");
                    string bloodGroup = Console.ReadLine();
                    if (InvalidBloodGroup(bloodGroup))
                    {
                        Console.WriteLine("Enter a valid blood group.");
                        Console.WriteLine("Press any key to return.");
                        Console.ReadKey();
                        return;
                    }
                    Console.Write("Diagnose: ");
                    string diagnose = Console.ReadLine();
                    if (InvalidDiagnose(diagnose))
                    {
                        Console.WriteLine("Diagnose cannot be less than 3 symbols and must be all letters.");
                        Console.WriteLine("Press any key to return.");
                        Console.ReadKey();
                        return;
                    }

                    Patients patient = new Patients
                    {
                        Email = email,
                        Password = password,
                        Name = name,
                        PhoneNumber = phoneNumber,
                        BloodGroup = bloodGroup,
                        Diagnose = diagnose
                    };
                    homeDAO.PatientRegister(patient);
                    break;
                }
            }
        }

        /// <summary>Checks if the name is invalid.</summary>
        /// <param name="name">The name.</param>
        /// <returns>
        ///   <c>true</c> if the name has special characters or is too short, <c>false</c> otherwise.</returns>
        private static bool InvalidName(string name)
        {
            return name.Any(char.IsSymbol) || name.Any(char.IsDigit) || name.Length < 2;
        }

        /// <summary>Checks if the phone number is invalid.</summary>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns>
        ///   <c>true</c> if the phone number is not 10 digits, <c>false</c> otherwise.</returns>
        private static bool InvalidPhoneNumber(string phoneNumber)
        {
            return phoneNumber.Length != 10 || !phoneNumber.All(char.IsDigit);
        }

        /// <summary>Checks if the blood group is invalid</summary>
        /// <param name="bloodGroup">The blood group.</param>
        /// <returns>
        ///   <c>true</c> if blood group is not in the form A+/- or B+/-, <c>false</c> otherwise.</returns>
        private static bool InvalidBloodGroup(string bloodGroup)
        {
            return !(bloodGroup[0] != 'A' || bloodGroup[0] != 'B' || bloodGroup[0] != '0') && !(bloodGroup[1] != '+' || bloodGroup[1] != '-');
        }

        /// <summary>Checks if the diagnose is invalid.</summary>
        /// <param name="diagnose">The diagnose.</param>
        /// <returns>
        ///   <c>true</c> if diagnose has special characters or is too short, <c>false</c> otherwise.</returns>
        private static bool InvalidDiagnose(string diagnose)
        {
            return diagnose.Length < 3 || diagnose.Any(char.IsSymbol) || diagnose.Any(char.IsDigit);
        }

        /// <summary>Initializes a new instance of the <see cref="T:DonorSystem.Controllers.HomeController" /> class and encapsulates the logic of the program.</summary>
        public HomeController()
        {
            homeDAO = new HomeDAO();
            websitesDAO = new WebsitesDAO();
        }
    }
}
