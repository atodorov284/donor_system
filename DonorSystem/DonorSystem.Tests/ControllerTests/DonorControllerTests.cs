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
    /// Tests the DonorController class.
    /// </summary>
    [TestFixture]
    class DonorControllerTests
    {
        HomeController homeController;
        Donor donor;
        Donor testDonor;

        /// <summary>
        /// Setup before all the tests.
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            homeController = new HomeController();

            donor = new Donor
            {
                DonorId = 4,
                Name = "Teodora Stoqnova",
                Email = "teodora.stoqnova@abv.bg",
                Password = "btWDPPNShuv4Zit7WUnw10K77D8=",
                PhoneNumber = "0881231234",
                Status = null,
                BloodGroup = "B-"
            };

            testDonor = new Donor
            {
                DonorId = 100,
                Name = "Test Donor",
                Email = "test.donor@abv.bg",
                Password = "btWDPPNShuv4Zit7WUnw10K77D8=",
                PhoneNumber = "0881231234",
                Status = null,
                BloodGroup = "A+"
            };

            homeController.RegisterAsDonor(
                testDonor.Email,
                testDonor.Password,
                testDonor.Name,
                testDonor.PhoneNumber,
                testDonor.Status,
                testDonor.BloodGroup);
        }

        /// <summary>
        /// Enrolls an existing donor.
        /// </summary>
        [Test]
        public void EnrollAgainInSystem()
        {
            //Arrange
            DonorController donorController = new DonorController();

            //Act
            donorController.Enroll(donor);

            //Assert
            Assert.AreEqual("Available", donor.Status);
        }

        /// <summary>
        /// Deletes the test donor from the system.
        /// </summary>
        [Test]
        public void UnrollFromSystem()
        {
            //Arrange
            DonorController donorController = new DonorController();

            //Act
            testDonor = homeController.LoginAsDonor(testDonor.Email, testDonor.Password);
            donorController.Unroll(testDonor);

            //Assert
            Assert.IsTrue(homeController.LoginAsDonor(testDonor.Email, testDonor.Password) == null);
        }
    }
}
