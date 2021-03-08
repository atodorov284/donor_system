using System;
using System.Collections.Generic;
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
            Console.WriteLine("Press any key to return to main menu.");
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
                Console.Write("Phone number: ");
                string phoneNumber = Console.ReadLine();
                string status = "Available";
                Console.Write("Blood group: ");
                string bloodGroup = Console.ReadLine();

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
                Console.Write("Phone number: ");
                string phoneNumber = Console.ReadLine();
                Console.Write("Blood group: ");
                string bloodGroup = Console.ReadLine();
                Console.Write("Diagnose: ");
                string diagnose = Console.ReadLine();

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
