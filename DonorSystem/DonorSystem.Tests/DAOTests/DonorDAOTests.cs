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
    /// Tests the DonorDAO class.
    /// </summary>
    [TestFixture]
    class DonorDAOTests
    {
        /// <summary>
        /// Transfuses blood from an existing donor to a test patient.
        /// </summary>
        [Test]
        public void TransfuseBloodTest()
        {
            //Arrange
            HomeDAO homeDAO = new HomeDAO();

            DonorsDAO donorsDAO = new DonorsDAO();
            Patient testPatient = new Patient();
            testPatient.Name = "test";
            string email = "pesho.petrov@abv.bg";
            string password = "btWDPPNShuv4Zit7WUnw10K77D8=";

            //Act
            Donor donor = homeDAO.DonorLogin(email, password);
            donorsDAO.TransfuseBlood(donor, testPatient);

            //Assert
            Assert.AreEqual(testPatient.Name, donor.Status);

            //Clean-Up
            Patient cleanup = new Patient();
            cleanup.Email = "cleanup@abv.bg";
            cleanup.Password = "cleanup";
            cleanup.Name = "Available";
            cleanup.PhoneNumber = "0123456789";
            cleanup.BloodGroup = "A+";
            homeDAO.PatientRegister(cleanup);
            cleanup = homeDAO.PatientLogin("cleanup@abv.bg", "cleanup");
            donor = homeDAO.DonorLogin(email, password);
            donorsDAO.TransfuseBlood(donor, cleanup);
        }

        /// <summary>
        /// Changes the status of an existing donor.
        /// </summary>
        [Test]
        public void ChangeDonorStatusTest()
        {
            //Arrange
            HomeDAO homeDAO = new HomeDAO();

            DonorsDAO donorsDAO = new DonorsDAO();
            string email = "pesho.petrov@abv.bg";
            string password = "btWDPPNShuv4Zit7WUnw10K77D8=";
            string expected = "Available";

            //Act
            Donor donor = homeDAO.DonorLogin(email, password);
            donorsDAO.ChangeDonorStatus(donor);

            //Assert
            Assert.AreEqual(expected, donor.Status);
        }

        /// <summary>
        /// Deletes the test donor.
        /// </summary>
        [Test]
        public void DeleteDonorFromSystemTest()
        {
            //Arrange
            HomeDAO homeDAO = new HomeDAO();

            DonorsDAO donorsDAO = new DonorsDAO();
            Donor testDonor = new Donor();
            testDonor.DonorId = 101;
            testDonor.Name = "Test Donor 1";
            testDonor.Email = "testdonor1@abv.bg";
            testDonor.Password = "btWDPPNShuv4Zit7WUnw10K77D8=";
            testDonor.PhoneNumber = "0888123456";
            testDonor.Status = null;
            testDonor.BloodGroup = "B-";

            //Act
            homeDAO.DonorRegister(testDonor);
            donorsDAO.DeleteDonor(testDonor);

            //Assert
            Assert.IsTrue(homeDAO.DonorLogin(testDonor.Email, testDonor.Password) == null);
        }
    }
}
