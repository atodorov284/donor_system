using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DonorSystem.Views;
using DonorSystem.Models;

namespace DonorSystem.DAO
{
    /// <summary>
    /// Управлява базата данни и извършва фунмции полезни на класа HomeController.
    /// </summary>
    public class HomeDAO
    {
        DonorDBContext context;

        /// <summary>
        /// Конструктор. Създава нов контекст на обекта.
        /// </summary>
        public HomeDAO()
        {
            context = new DonorDBContext();
        }

        /// <summary>
        /// Прави опит за влизане в системата като дарител с въведените имейл и парола.
        /// </summary>
        /// <param name="email">Имейл на потребителя.</param>
        /// <param name="password">Парола на потребителя.</param>
        /// <returns>Данните на дарителя в системата.</returns>
        public Donor DonorLogin(string email, string password)
        {
            var donor = this.context.Donors
                .Where(d => d.Email.Equals(email) && d.Password.Equals(password))
                .FirstOrDefault();

            return donor;
        }

        /// <summary>
        /// Прави опит за влизане в системата като пациент с въведените имейл и парола.
        /// </summary>
        /// <param name="email">Имейл на потребителя.</param>
        /// <param name="password">Парола на потребителя.</param>
        /// <returns>Данните на пациента в системата.</returns>
        public Patient PatientLogin(string email, string password)
        {
            var patient = this.context.Patients
                .Where(d => d.Email.Equals(email) && d.Password.Equals(password))
                .FirstOrDefault();

            return patient;
        }
        
        /// <summary>
        /// Прави опит за регистриране на дарител в системата.
        /// </summary>
        /// <param name="donor">Дарител, който трябва да се регистрира в системата.</param>
        /// <returns>Връща дали потребителя може да се регистрира.</returns>
        public bool DonorRegister(Donor donor)
        {
            if (!EmailExists(donor.Email, true)) 
            {
                context.Donors.Add(donor);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Прави опит за регистриране на пациент в системата.
        /// </summary>
        /// <param name="patient">Пациент, който трябва да се регистрира в системата.</param>
        /// <returns>Връща дали потребител може да се регистрира.</returns>
        public bool PatientRegister(Patient patient)
        {
            if (!EmailExists(patient.Email, false)) 
            {
                context.Patients.Add(patient);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
            
        }

        /// <summary>
        /// Търси даден имейл дали е наличен в системата.
        /// </summary>
        /// <param name="email">Имейлът, който се търси дали съществува в системата.</param>
        /// <param name="isDonor">При true търси имейла в таблицата с дарители, при false търси в таблицата с пациенти.</param>
        /// <returns>true при намерен имейла в системата, false при неналичието му.</returns>
        public bool EmailExists(string email, bool isDonor)
        {
            if (isDonor)
            {
                var existingUser = this.context.Donors
                .Where(d => d.Email.Equals(email))
                .FirstOrDefault();
                if (existingUser == null)
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
    }
}
