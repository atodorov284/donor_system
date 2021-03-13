using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DonorSystem.DAO;
using DonorSystem.Models;

namespace DonorSystem.Controllers
{
    /// <summary>
    /// Управлява базата от данни с уебсайтовете, както и системата за вход/изход
    /// заедно с услугите, предоставени от конзолния интерфейс.
    /// </summary>
    class HomeController
    {
        HomeDAO homeDAO;
        WebsitesDAO websitesDAO;

        /// <summary>
        /// 
        /// </summary>
        public void ShowUsefulInfo()
        {
            Console.Clear();
            Console.WriteLine("Websites that may be useful to donors and patients: ");
            websitesDAO.ShowAll();
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
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
                Donor donor = new Donor();
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
                Console.Write("Blood group: ");
                string bloodGroup = Console.ReadLine();
                if (!(bloodGroup[0] != 'A' || bloodGroup[0] != 'B') && !(bloodGroup[1] != '+' || bloodGroup[1] != '-'))
                {
                    Console.WriteLine("Enter a valid blood group.");
                    Console.WriteLine("Press any key to return.");
                    Console.ReadKey();
                    return;
                }
                Console.Write("Diagnose: ");
                string diagnose = Console.ReadLine();
                if (diagnose.Length < 3 || name.Any(char.IsSymbol) || name.Any(char.IsDigit))
                {
                    Console.WriteLine("Diagnose cannot be less than 3 symbols and must be all letters.");
                    Console.WriteLine("Press any key to return.");
                    Console.ReadKey();
                    return;
                }

                Patient patient = new Patient();
                patient.Email = email;
                patient.Password = password;
                patient.Name = name;
                patient.PhoneNumber = phoneNumber;
                patient.BloodGroup = bloodGroup;
                patient.Diagnose = diagnose;
                homeDAO.PatientRegister(patient);
            }
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public HomeController()
        {
            homeDAO = new HomeDAO();
            websitesDAO = new WebsitesDAO();
        }
    }
}
