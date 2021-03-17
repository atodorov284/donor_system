using NUnit.Framework;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using DonorSystem.DAO;
using DonorSystem.Models;

namespace DonorSystem.Tests
{
    /// <summary>
    /// Tests the HomeDAO class.
    /// </summary>
    [TestFixture]
    class HomeDAOTests
    {
        /// <summary>
        /// Generates hash code of the password.
        /// </summary>
        /// <param name="password">string with the original password</param>
        /// <returns>the hash code of the password</returns>
        private string HashPassword(string password)
        {
            var provider = new SHA1CryptoServiceProvider();
            var encoding = new UnicodeEncoding();
            return Convert.ToBase64String(provider.ComputeHash(encoding.GetBytes(password)));
        }

        /// <summary>
        /// Logs in an existing donor in the system.
        /// </summary>
        [Test]
        public void DonorLoginTest()
        {
            //Arrange
            HomeDAO homeDAO = new HomeDAO();
            string email = "petur.stoqnov@abv.bg";
            string password = "123456";
            Donor expected = new Donor
            {
                DonorId = 37,
                Name = "Petur Stoqnov",
                Email = "petur.stoqnov@abv.bg",
                Password = "btWDPPNShuv4Zit7WUnw10K77D8=",
                PhoneNumber = "0881231234",
                Status = "Available",
                BloodGroup = "A+"
            };

            //Act
            Donor result = homeDAO.DonorLogin(email, HashPassword(password));

            //Assert
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Logs in an existing patient in the system.
        /// </summary>
        [Test]
        public void PatientLoginTest()
        {
            //Arrange
            HomeDAO homeDAO = new HomeDAO();
            string email = "nikolai.petrov@abv.bg";
            string password = "123456";
            Patient expected = new Patient
            {
                PatientId = 14,
                Name = "Nikolai Petrov",
                Email = "nikolai.petrov@abv.bg",
                Password = "btWDPPNShuv4Zit7WUnw10K77D8=",
                PhoneNumber = "0881231234",
                Diagnose = "Blood Cancer",
                BloodGroup = "A-"
            };

            //Act
            Patient result = homeDAO.PatientLogin(email, HashPassword(password));

            //Assert
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Tries to register a donor that already exists in the DB.
        /// </summary>
        [Test]
        public void DonorRegisterAlreadyExistingTest()
        {
            //Arrange
            HomeDAO homeDAO = new HomeDAO();
            Donor donor = new Donor
            {
                DonorId = 2,
                Name = "Dimitur Petrov",
                Email = "dimitur.petrov@abv.bg",
                Password = "btWDPPNShuv4Zit7WUnw10K77D8=",
                PhoneNumber = "0881231234",
                Status = null,
                BloodGroup = "0+"
            };

            //Act
            bool result = homeDAO.DonorRegister(donor);

            //Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tries to register a donor that does not exist in the DB.
        /// </summary>
        [Test]
        public void DonorRegisterTest()
        {
            //Arrange
            DonorsDAO donorsDAO = new DonorsDAO();

            HomeDAO homeDAO = new HomeDAO();
            Donor donor = new Donor();
            donor.DonorId = 7;
            donor.Name = "Ralitsa Mladenova";
            donor.Email = "ralitsa.mladenova@abv.bg";
            donor.Password = "6Pl/upEE0epQR5SObftn+s2fW3M=";
            donor.PhoneNumber = "0888654321";
            donor.Status = "Available";
            donor.BloodGroup = "0+";

            //Act
            bool result = homeDAO.DonorRegister(donor);

            //Assert
            Assert.IsTrue(result);

            //Clean-Up
            donorsDAO.DeleteDonor(donor);
        }

        /// <summary>
        /// Tries to register a patient that already exists in the DB.
        /// </summary>
        [Test]
        public void PatientRegisterAlreadyExistingTest()
        {
            //Arrange
            HomeDAO homeDAO = new HomeDAO();
            Patient patient = new Patient();
            patient.PatientId = 1;
            patient.Name = "Test Patient";
            patient.Email = "test.patient@abv.bg";
            patient.Password = "btWDPPNShuv4Zit7WUnw10K77D8=";
            patient.PhoneNumber = "0888123456";
            patient.Diagnose = "test";
            patient.BloodGroup = "B-";

            //Act
            bool result = homeDAO.PatientRegister(patient);

            //Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tries to register a patient that does not exist in the DB.
        /// </summary>
        [Test]
        public void PatientRegisterTest()
        {
            //Arrange
            PatientsDAO patientDAO = new PatientsDAO();

            HomeDAO homeDAO = new HomeDAO();
            Patient patient = new Patient
            {
                PatientId = 90,
                Name = "Test Patient 2",
                Email = "test.patient2@abv.bg",
                Password = "btWDPPNShuv4Zit7WUnw10K77D8=",
                PhoneNumber = "0888123456",
                Diagnose = "test",
                BloodGroup = "B-"
            };

            //Act
            bool result = homeDAO.PatientRegister(patient);

            //Assert
            Assert.IsTrue(result);

            //Clean-Up
            patientDAO.DeletePatient(patient);
        }

        /// <summary>
        /// Checks an email that does not exist in the patients' DB.
        /// </summary>
        [Test]
        public void EmailDoesNotExistInPatientsTest()
        {
            //Arrange
            HomeDAO homeDAO = new HomeDAO();
            string email = "nonexisting.person@abv.bg";
            bool isDonor = false;

            //Act
            bool result = homeDAO.EmailAlreadyExists(email, isDonor);

            //Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Checks an email that exists in the patients' DB.
        /// </summary>
        [Test]
        public void EmailExistsInPatientsTest()
        {
            //Arrange
            HomeDAO homeDAO = new HomeDAO();
            string email = "test.patient@abv.bg";
            bool isDonor = false;

            //Act
            bool result = homeDAO.EmailAlreadyExists(email, isDonor);

            //Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Checks an email that does not exist in the donors' DB.
        /// </summary>
        [Test]
        public void EmailDoesNotExistInDonorsTest()
        {
            //Arrange
            HomeDAO homeDAO = new HomeDAO();
            string email = "test.patient@abv.bg";
            bool isDonor = true;

            //Act
            bool result = homeDAO.EmailAlreadyExists(email, isDonor);

            //Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Checks an email that exists in the donors' DB.
        /// </summary>
        [Test]
        public void EmailExistsInDonorsTest()
        {
            //Arrange
            HomeDAO homeDAO = new HomeDAO();
            string email = "dimitur.petrov@abv.bg";
            bool isDonor = true;

            //Act
            bool result = homeDAO.EmailAlreadyExists(email, isDonor);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
