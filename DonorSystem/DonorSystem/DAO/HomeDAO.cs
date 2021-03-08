using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DonorSystem.Views;
using DonorSystem.Models;

namespace DonorSystem.DAO
{
    class HomeDAO
    {
        DonorDBContext context;
        DonorMenu donorMenu;
        PatientMenu patientMenu;
        public void DonorLogin(string email, string password)
        {
            var donor = this.context.Donors
                .Where(d => d.Email.Equals(email) && d.Password.Equals(password))
                .FirstOrDefault();
            if (donor == null)
            {
                Console.WriteLine("Invalid credentials.");
                Console.WriteLine("Redirecting to main menu...");
                System.Threading.Thread.Sleep(1300);
            }
            else
            {
                donorMenu = new DonorMenu(donor);
            }
        }

        public void PatientLogin(string email, string password)
        {
            var patient = this.context.Patients
                .Where(d => d.Email.Equals(email) && d.Password.Equals(password))
                .FirstOrDefault();
            if (patient == null)
            {
                Console.WriteLine("Invalid credentials.");
                Console.WriteLine("Redirecting to main menu...");
                System.Threading.Thread.Sleep(1300);
            }
            else
            {
                patientMenu = new PatientMenu(patient);
            }
        }

        public void DonorRegister(Donors donor)
        {
            context.Donors.Add(donor);
            context.SaveChanges();
            Console.WriteLine("Register successful! Redirecting...");
            DonorLogin(donor.Email, donor.Password);
        }

        public void PatientRegister(Patients patient)
        {
            context.Patients.Add(patient);
            context.SaveChanges();
            Console.WriteLine("Register successful! Redirecting...");
            PatientLogin(patient.Email, patient.Password);
        }

        public HomeDAO()
        {
            context = new DonorDBContext();
        }
    }
}
