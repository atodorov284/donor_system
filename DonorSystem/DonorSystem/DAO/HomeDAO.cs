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
                Console.WriteLine("Press any key to return.");
                Console.ReadKey();
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
                Console.WriteLine("Press any key to return.");
                Console.ReadKey();
            }
            else
            {
                patientMenu = new PatientMenu(patient);
            }
        }

        private bool EmailExists(string email, bool isDonor)
        {
            if (isDonor)
            {
                var existingUser = this.context.Donors
                .Where(d => d.Email.Equals(email))
                .FirstOrDefault();
                if(existingUser == null)
                {
                    return false;
                }
            }
            else
            {
                var existingUser = this.context.Patients
                .Where(d => d.Email.Equals(email))
                .FirstOrDefault();
                if (existingUser == null)
                {
                    return false;
                }
            }
            return true;
        }

        public void DonorRegister(Donors donor)
        {
            if (!EmailExists(donor.Email, true)) 
            {
                context.Donors.Add(donor);
                context.SaveChanges();
                Console.WriteLine("Register successful! Redirecting...");
                DonorLogin(donor.Email, donor.Password);
            }
            else
            {
                Console.WriteLine("Email already in use. Please use a different email.");
                Console.WriteLine("Press any key to return.");
                Console.ReadKey();
            }
        }

        public void PatientRegister(Patients patient)
        {
            if (!EmailExists(patient.Email, false)) 
            {
                context.Patients.Add(patient);
                context.SaveChanges();
                Console.WriteLine("Register successful! Redirecting...");
                PatientLogin(patient.Email, patient.Password);
            }
            else
            {
                Console.WriteLine("Email already in use. Please use a different email.");
                Console.WriteLine("Press any key to return.");
                Console.ReadKey();
            }
            
        }

        public HomeDAO()
        {
            context = new DonorDBContext();
        }
    }
}
