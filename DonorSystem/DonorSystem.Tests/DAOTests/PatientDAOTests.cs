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
    /// Tests the PatientDAO class.
    /// </summary>
    [TestFixture]
    class PatientDAOTests
    {
        /// <summary>
        /// Compares the potential donors retrived by FindCompatibleDonors
        /// with all the actual donors that have blood group compatible with the Test Patient's one. 
        /// Donors are compared by their id.
        /// </summary>
        [Test]
        public void FindCompatibleDonorsTest()
        {
            //Arrange
            PatientsDAO patientsDAO = new PatientsDAO();
            Patient patient = new Patient();
            patient.PatientId = 5;
            patient.Name = "Test Patient";
            patient.Email = "test.patient@abv.bg";
            patient.Password = "btWDPPNShuv4Zit7WUnw10K77D8=";
            patient.PhoneNumber = "0888123456";
            patient.Diagnose = "test";
            patient.BloodGroup = "B-";
            bool result = true;
            List<int> ids = new List<int>() { 3, 4 };

            //Act
            List<Donor> donors = patientsDAO.FindCompatibleDonors(patient);
            foreach (var donor in donors)
            {
                result &= ids.Contains(donor.DonorId);
            }

            //Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Creates a new test patient object, registers as patient then deletes. 
        /// Sees if you can log in the system with the same account after the deletion proccess.
        /// </summary>
        [Test]
        public void DeletePatientTest()
        {
            //Arrange
            HomeDAO homeDAO = new HomeDAO();

            PatientsDAO patientsDAO = new PatientsDAO();
            Patient testPatient = new Patient();
            testPatient.PatientId = 102;
            testPatient.Name = "Test Patient 2";
            testPatient.Email = "test.patient2@abv.bg";
            testPatient.Password = "btWDPPNShuv4Zit7WUnw10K77D8=";
            testPatient.PhoneNumber = "0888123456";
            testPatient.Diagnose = "test";
            testPatient.BloodGroup = "A+";

            //Act
            homeDAO.PatientRegister(testPatient);
            patientsDAO.DeletePatient(testPatient);

            //Assert
            Assert.IsTrue(homeDAO.PatientLogin(testPatient.Email, testPatient.Password) == null);
        }
    }
}
