using System;
using System.Linq;
using DonorSystem.Views;
using DonorSystem.Models;

namespace DonorSystem.DAO
{
    class HomeDAO
    {
        readonly DonorDBContext context;

        /// <summary>Initializes a new instance of the <see cref="T:DonorSystem.DAO.HomeDAO" /> class and registers/logins the users.</summary>
        public HomeDAO()
        {
            context = new DonorDBContext();
        }

        /// <summary>Checks for valid credential and logs the donor in.</summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
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

        /// <summary>Checks for valid credential and logs the patient in.</summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
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

        /// <summary>Checks if email already exists based on the role</summary>
        /// <param name="email">The email.</param>
        /// <param name="isDonor">if set to <c>true</c> [is donor].</param>
        /// <returns>
        ///   <c>true</c> if email already exists, <c>false</c> otherwise.</returns>
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

        /// <summary>Registers the donor.</summary>
        /// <param name="donor">The donor.</param>
        public void DonorRegister(Donors donor)
        {
            context.Donors.Add(donor);
                context.SaveChanges();
                Console.WriteLine("Register successful! Redirecting...");
                DonorLogin(donor.Email, donor.Password);
        }

        /// <summary>Registers the patient.</summary>
        /// <param name="patient">The patient.</param>
        public void PatientRegister(Patients patient)
        {
            context.Patients.Add(patient);
                context.SaveChanges();
                Console.WriteLine("Register successful! Redirecting...");
                PatientLogin(patient.Email, patient.Password);
        }

        
    }
}
