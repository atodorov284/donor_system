using NUnit.Framework;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using DonorSystem.Controllers;
using DonorSystem.Models;

namespace DonorSystem.Tests
{
    /// <summary>
    /// Tests the PatientController class.
    /// </summary>
    [TestFixture]
    class PatientControllerTests
    {
        HomeController homeController;
        Patient patient;

        /// <summary>
        /// Setup before all the tests.
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            homeController = new HomeController();

            patient = new Patient();
            patient.PatientId = 2;
            patient.Name = "Test Patient 2";
            patient.Email = "test.patient2@abv.bg";
            patient.Password = "btWDPPNShuv4Zit7WUnw10K77D8=";
            patient.PhoneNumber = "0888123456";
            patient.Diagnose = "A+";
            patient.BloodGroup = "test 2";

            homeController.RegisterAsPatient(
                patient.Email,
                patient.Password,
                patient.Name,
                patient.PhoneNumber,
                patient.Diagnose,
                patient.BloodGroup);
        }

        /// <summary>
        /// Gets all donors which blood group is compatible with the test patient
        /// and checks their ids.
        /// </summary>
        [Test]
        public void GetPotentialDonorsTest()
        {
            //Arrange
            PatientController patientController = new PatientController();
            Patient patient = new Patient
            {
                PatientId = 5,
                Name = "Test Patient",
                Email = "test.patient@abv.bg",
                Password = "btWDPPNShuv4Zit7WUnw10K77D8=",
                PhoneNumber = "0888123456",
                Diagnose = "test",
                BloodGroup = "B-"
            };
            int numberOfDonors = 5;
            bool result = true;
            List<int> ids = new List<int>() { 3, 4 };

            //Act
            List<Donor> donors = patientController.GetPotentialDonors(patient, numberOfDonors);
            foreach (var donor in donors)
            {
                result &= ids.Contains(donor.DonorId);
            }

            //Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test patient receives blood from an existing donor
        /// then its account is being deleted.
        /// </summary>
        [Test]
        public void ReceiveBloodTest()
        {
            //Arrange
            PatientController patientController = new PatientController();
            patient = new Patient();
            patient.PatientId = 2;
            patient.Name = "Test Patient 2";
            patient.Email = "test.patient2@abv.bg";
            patient.Password = "btWDPPNShuv4Zit7WUnw10K77D8=";
            patient.PhoneNumber = "0888123456";
            patient.Diagnose = "A+";
            patient.BloodGroup = "test 2";
            homeController.RegisterAsPatient(
                patient.Email,
                patient.Password,
                patient.Name,
                patient.PhoneNumber,
                patient.Diagnose,
                patient.BloodGroup);
            string email = "teodora.stoqnova@abv.bg";
            string password = "btWDPPNShuv4Zit7WUnw10K77D8=";

            //Act
            Donor donor = homeController.LoginAsDonor(email, password);
            patient = homeController.LoginAsPatient(patient.Email, patient.Password);
            patientController.ReceiveBlood(patient, donor);

            //Assert
            Assert.IsTrue(homeController.LoginAsPatient(patient.Email, patient.Password) == null);

            //Clean-Up
            homeController.RegisterAsPatient(
                "cleanup@abv.bg",
                "cleanup",
                "Available",
                "0123456789",
                "",
                "A+");
            Patient cleanup = homeController.LoginAsPatient("cleanup@abv.bg", "cleanup");
            donor = homeController.LoginAsDonor(email, password);
            patientController.ReceiveBlood(cleanup, donor);
        }

        /// <summary>
        /// Test patient is registered in the system then deleted.
        /// </summary>
        [Test]
        public void DeletePatientTest()
        {
            //Arrange
            PatientController patientController = new PatientController();
            homeController.RegisterAsPatient(
                patient.Email,
                patient.Password,
                patient.Name,
                patient.PhoneNumber,
                patient.Diagnose,
                patient.BloodGroup);

            //Act
            patient = homeController.LoginAsPatient(patient.Email, patient.Password);
            patientController.DeletePatient(patient);

            //Assert
            Assert.IsTrue(homeController.LoginAsPatient(patient.Email, patient.Password) == null);
        }
    }
}
