using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DonorSystem.DAO;
using DonorSystem.Models;

namespace DonorSystem.Controllers
{
    class HomeController
    {
        HomeDAO homeDAO;
        WebsitesDAO websitesDAO;

        public void ShowUsefulInfo()
        {
            Console.WriteLine("Websites that may be useful to donors and patients: ");
            websitesDAO.ShowAll();
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }

        public void Login(string email, string password, int role)
        {
            if (role == 1)
            {
                homeDAO.DonorLogin(email, password);
            }
            else if (role == 2) 
            {
                homeDAO.PatientLogin(email, password);
            }
        }

        public void Register(string email, string password, int role)
        {
            if (role == 1)
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();
                if (name.Any(char.IsSymbol) || name.Any(char.IsDigit) || name.Length < 2)
                {
                    Console.WriteLine("Name cannot contain other symbols or be less than 2 letters.");
                    Console.WriteLine("Press any key to return.");
                    Console.ReadKey();
                    return;
                }
                Console.Write("Phone number: ");
                string phoneNumber = Console.ReadLine();
                if (phoneNumber.Length != 10 || !phoneNumber.All(char.IsDigit))
                {
                    Console.WriteLine("Phone number should be 10 symbols and all numbers.");
                    Console.WriteLine("Press any key to return.");
                    Console.ReadKey();
                    return;
                }
                string status = "Available";
                Console.Write("Blood group: ");
                string bloodGroup = Console.ReadLine();
                if (!(bloodGroup[0] != 'A' || bloodGroup[0] != 'B') && !(bloodGroup[1] != '+' || bloodGroup[1] != '-'))
                {
                    Console.WriteLine("Enter a valid blood group");
                    Console.WriteLine("Press any key to return.");
                    Console.ReadKey();
                    return;
                }
                Donors donor = new Donors();
                donor.Email = email;
                donor.Password = password;
                donor.Name = name;
                donor.PhoneNumber = phoneNumber;
                donor.Status = status;
                donor.BloodGroup = bloodGroup;
                homeDAO.DonorRegister(donor);
            }
            else if (role == 2) 
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();
                if (!name.All(char.IsLetter) || name.Length < 2)
                {
                    Console.WriteLine("Name cannot contain other symbols or be less than 2 letters.");
                    Console.WriteLine("Press any key to return.");
                    Console.ReadKey();
                    return;
                }
                Console.Write("Phone number: ");
                string phoneNumber = Console.ReadLine();
                if (phoneNumber.Length != 10 || !phoneNumber.All(char.IsDigit))
                {
                    Console.WriteLine("Phone number should be 10 symbols and all numbers.");
                    Console.WriteLine("Press any key to return.");
                    Console.ReadKey();
                    return;
                }
                Console.Write("Blood group: ");
                string bloodGroup = Console.ReadLine();
                if (bloodGroup[0] != 'A' || bloodGroup[0] != 'B' || bloodGroup[1] != '+' || bloodGroup[1] != '-')
                {
                    Console.WriteLine("Enter a valid blood group.");
                    Console.WriteLine("Press any key to return.");
                    Console.ReadKey();
                    return;
                }
                Console.Write("Diagnose: ");
                string diagnose = Console.ReadLine();
                if (diagnose.Length < 3 || !diagnose.All(char.IsLetter))
                {
                    Console.WriteLine("Diagnose cannot be less than 3 symbols and must be all letters.");
                    Console.WriteLine("Press any key to return.");
                    Console.ReadKey();
                    return;
                }

                Patients patient = new Patients();
                patient.Email = email;
                patient.Password = password;
                patient.Name = name;
                patient.PhoneNumber = phoneNumber;
                patient.BloodGroup = bloodGroup;
                patient.Diagnose = diagnose;
                homeDAO.PatientRegister(patient);
            }
        }

        public HomeController()
        {
            homeDAO = new HomeDAO();
            websitesDAO = new WebsitesDAO();
        }
    }
}
