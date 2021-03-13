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

        public bool ExistingEmail(string email, bool isDonor)
        {
            return homeDAO.ExistingEmail(email, isDonor);
        }

        public bool ValidatePassword(string password)
        {
            return password.Length >= 6;
        }

        public bool ValidateEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

        public void ShowUsefulInfo()
        {
            Console.Clear();
            Console.WriteLine("Websites that may be useful to donors and patients: ");
            websitesDAO.ShowAll();
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }

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

        private static bool InvalidName(string name)
        {
            return name.Any(char.IsSymbol) || name.Any(char.IsDigit) || name.Length < 2;
        }

        private static bool InvalidPhoneNumber(string phoneNumber)
        {
            return phoneNumber.Length != 10 || !phoneNumber.All(char.IsDigit);
        }

        private static bool InvalidBloodGroup(string bloodGroup)
        {
            return !(bloodGroup[0] != 'A' || bloodGroup[0] != 'B') && !(bloodGroup[1] != '+' || bloodGroup[1] != '-');
        }

        private static bool InvalidDiagnose(string diagnose)
        {
            return diagnose.Length < 3 || diagnose.Any(char.IsSymbol) || diagnose.Any(char.IsDigit);
        }
        public HomeController()
        {
            homeDAO = new HomeDAO();
            websitesDAO = new WebsitesDAO();
        }
    }
}
