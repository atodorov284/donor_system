using System;
using System.Linq;
using DonorSystem.Views;
using DonorSystem.Models;

namespace DonorSystem.DAO
{
    class HomeDAO
    {
        readonly DonorDBContext context;
        public void DonorLogin(string email, string password)
        {
            var donor = this.context.Donors
                .FirstOrDefault(d => d.Email.Equals(email) && d.Password.Equals(password));
            if (donor == null)
            {
                Console.WriteLine("Invalid credentials.");
                Console.WriteLine("Press any key to return.");
                Console.ReadKey();
            }
            else
            {
                new DonorMenu(donor);
            }
        }

        public void PatientLogin(string email, string password)
        {
            var patient = this.context.Patients
                .FirstOrDefault(d => d.Email.Equals(email) && d.Password.Equals(password));
            if (patient == null)
            {
                Console.WriteLine("Invalid credentials.");
                Console.WriteLine("Press any key to return.");
                Console.ReadKey();
            }
            else
            {
                new PatientMenu(patient);
            }
        }

        public bool ExistingEmail(string email, bool isDonor)
        {
            if (isDonor)
            {
                var existingUser = this.context.Donors
                    .FirstOrDefault(d => d.Email.Equals(email));
                if(existingUser == null)
                {
                    return false;
                }
            }
            else
            {
                var existingUser = this.context.Patients
                    .FirstOrDefault(d => d.Email.Equals(email));
                if (existingUser == null)
                {
                    return false;
                }
            }
            return true;
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
