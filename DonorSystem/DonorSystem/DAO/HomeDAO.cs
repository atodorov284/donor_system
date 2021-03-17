using System;
using System.Linq;
using DonorSystem.Models;

namespace DonorSystem.DAO
{
    public class HomeDAO
    {
        DonorDBContext context;

        /// <summary>Initializes a new instance of the <see cref="T:DonorSystem.DAO.HomeDAO" /> class and registers/logins the users.</summary>
        public HomeDAO()
        {
            context = new DonorDBContext();
        }


        /// <summary>Checks for valid credential and logs the donor in.</summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>The donor if login successful, null otherwise</returns>
        public Donor DonorLogin(string email, string password)
        {
            var donor = this.context.Donors
                .FirstOrDefault(d => d.Email.Equals(email) && d.Password.Equals(password));
            if (donor != null)
            {
                context.Entry(donor).Reload();
            }
            return donor;
        }

        /// <summary>Checks for valid credential and logs the patient in.</summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>The patient if login successful, null otherwise</returns>
        public Patient PatientLogin(string email, string password)
        {
            var patient = this.context.Patients
                .FirstOrDefault(d => d.Email.Equals(email) && d.Password.Equals(password));
            if (patient != null)
            {
                context.Entry(patient).Reload();
            }
            return patient;
        }


        /// <summary>Registers the donor.</summary>
        /// <param name="donor">The donor.</param>
        /// <returns>
        ///   <c>true</c> if register successful, <c>false</c> otherwise.</returns>
        public bool DonorRegister(Donor donor)
        {
            if (!EmailAlreadyExists(donor.Email, true)) 
            {
                context.Donors.Add(donor);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>Registers the patient.</summary>
        /// <param name="patient">The patient.</param>
        /// <returns>
        ///   <c>true</c> if register is successful, <c>false</c> otherwise.</returns>
        public bool PatientRegister(Patient patient)
        {
            if (!EmailAlreadyExists(patient.Email, false)) 
            {
                context.Patients.Add(patient);
                context.SaveChanges();
                return true;
            }

            return false;

        }

        /// <summary>Checks if email already exists based on the role</summary>
        /// <param name="email">The email.</param>
        /// <param name="isDonor">if set to <c>true</c> [is donor].</param>
        /// <returns>
        ///   <c>true</c> if email already exists, <c>false</c> otherwise.</returns>
        public bool EmailAlreadyExists(string email, bool isDonor)
        {
            if (isDonor)
            {
                var existingUser = this.context.Donors
                    .FirstOrDefault(d => d.Email.Equals(email));
                if (existingUser == null)
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
    }
}
