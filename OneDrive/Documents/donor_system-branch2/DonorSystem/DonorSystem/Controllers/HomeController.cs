using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using DonorSystem.DAO;
using DonorSystem.Models;

namespace DonorSystem.Controllers
{
    public class HomeController
    {
        private HomeDAO homeDAO;
        private WebsitesDAO websitesDAO;

        /// <summary>Initializes a new instance of the <see cref="T:DonorSystem.Controllers.HomeController" /> class and encapsulates the logic of the program.</summary>
        public HomeController()
        {
            homeDAO = new HomeDAO();
            websitesDAO = new WebsitesDAO();
        }

        /// <summary>Calls <see cref="websitesDAO"/> to show useful websites.</summary>
        public string GetUsefulInfo()
        {
            return websitesDAO.ToString();
        }

        /// <summary>Logins in the system as a donor.</summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>The donor if successful, null otherwise.</returns>
        public Donor LoginAsDonor(string email, string password)
        {
            return homeDAO.DonorLogin(email, password);
        }

        /// <summary>Logins in the system as a patient.</summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>The patient if successful, null otherwise.</returns>
        public Patient LoginAsPatient(string email, string password)
        {
            return homeDAO.PatientLogin(email, password);
        }


        /// <summary>Registers as donor in the system.</summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="name">The name.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="status">The status.</param>
        /// <param name="bloodGroup">The blood group.</param>
        /// <returns>
        ///   <c>true</c> if register is successful, <c>false</c> otherwise.</returns>
        public bool RegisterAsDonor(string email, string password, string name, string phoneNumber, string status, string bloodGroup)
        {
            Donor donor = new Donor
            {
                Email = email,
                Password = password,
                Name = name,
                PhoneNumber = phoneNumber,
                Status = status,
                BloodGroup = bloodGroup
            };

            return homeDAO.DonorRegister(donor);
        }

        /// <summary>Registers as patient in the system.</summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="name">The name.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="status">The status.</param>
        /// <param name="bloodGroup">The blood group.</param>
        /// <returns>
        ///   <c>true</c> if register is successful, <c>false</c> otherwise.</returns>
        public bool RegisterAsPatient(string email, string password, string name, string phoneNumber, string bloodGroup, string diagnose)
        {
            Patient patient = new Patient
            {
                Email = email,
                Password = password,
                Name = name,
                PhoneNumber = phoneNumber,
                BloodGroup = bloodGroup,
                Diagnose = diagnose
            };

            return homeDAO.PatientRegister(patient);

        }

        /// <summary>Hashes the password.</summary>
        /// <param name="password">The not encoded password.</param>
        /// <returns>Hashed password</returns>
        public string HashPassword(string password)
        {
            var provider = new SHA1CryptoServiceProvider();
            var encoding = new UnicodeEncoding();
            return Convert.ToBase64String(provider.ComputeHash(encoding.GetBytes(password)));
        }

        /// <summary>Calls <see cref="homeDAO"/> to see if the email already exists based on the role.</summary>
        /// <param name="email">The email.</param>
        /// <param name="isDonor">The role</param>
        /// <returns>
        ///   <c>true</c> if exists, <c>false</c> otherwise.</returns>
        public bool EmailAlreadyExists(string email, int role)
        {
            bool isDonor = role == 1;
            return homeDAO.EmailAlreadyExists(email, isDonor);
        }

        /// <summary>Validates the email.</summary>
        /// <param name="email">The email.</param>
        /// <returns>
        ///   <c>true</c> if <see cref="EmailAddressAttribute"/> validates the email, <c>false</c> otherwise.</returns>
        public bool ValidateEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

        /// <summary>Validates the password.</summary>
        /// <param name="password">The password.</param>
        /// <returns>
        ///   <c>true</c> if password length is less or equal to 5, <c>false</c> otherwise.</returns>
        public bool InvalidPassword(string password)
        {
            return password.Length <= 5;
        }

        /// <summary>Checks if the passwords match.</summary>
        /// <param name="password">The password.</param>
        /// <param name="repeated">The repeated password.</param>
        /// <returns>
        ///   <c>true</c> if equal, <c>false</c> otherwise.</returns>
        public bool PasswordMismatch(string password, string repeated)
        {
            return password != repeated;
        }

        /// <summary>Checks if the name is invalid.</summary>
        /// <param name="name">The name.</param>
        /// <returns>
        ///   <c>true</c> if the name has special characters or is too short, <c>false</c> otherwise.</returns>
        public bool InvalidName(string name)
        {
            return name.Any(char.IsSymbol) || name.Any(char.IsDigit) || name.Length < 2;
        }

        /// <summary>Checks if the phone number is invalid.</summary>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns>
        ///   <c>true</c> if the phone number is not 10 digits, <c>false</c> otherwise.</returns>
        public bool InvalidPhoneNumber(string phoneNumber)
        {
            return phoneNumber.Length != 10 || !phoneNumber.All(char.IsDigit);
        }

        /// <summary>Checks if the blood group is invalid</summary>
        /// <param name="bloodGroup">The blood group.</param>
        /// <returns>
        ///   <c>true</c> if blood group is not in the form A+/- or B+/-, <c>false</c> otherwise.</returns>
        public bool InvalidBloodGroup(string bloodGroup)
        {
            return !((bloodGroup[0] == 'A' || bloodGroup[0] == 'B' || bloodGroup[0] == '0') && (bloodGroup[1] == '+' || bloodGroup[1] == '-'));
        }

        /// <summary>Checks if the diagnose is invalid.</summary>
        /// <param name="diagnose">The diagnose.</param>
        /// <returns>
        ///   <c>true</c> if diagnose has special characters or is too short, <c>false</c> otherwise.</returns>
        public bool InvalidDiagnose(string diagnose)
        {
            return diagnose.Length < 3 || diagnose.Any(char.IsSymbol) || diagnose.Any(char.IsDigit);
        }
    }
}
