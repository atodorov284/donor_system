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

            donor = new Donor();
            donor.DonorId = 4;
            donor.Name = "Teodora Stoqnova";
            donor.Email = "teodora.stoqnova@abv.bg";
            donor.Password = "btWDPPNShuv4Zit7WUnw10K77D8=";
            donor.PhoneNumber = "0881231234";
            donor.Status = null;
            donor.BloodGroup = "B-";

            testDonor = new Donor();
            testDonor.DonorId = 100;
            testDonor.Name = "Test Donor";
            testDonor.Email = "test.donor@abv.bg";
            testDonor.Password = "btWDPPNShuv4Zit7WUnw10K77D8=";
            testDonor.PhoneNumber = "0881231234";
            testDonor.Status = null;
            testDonor.BloodGroup = "A+";

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
        public void EnrollTest()
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
        public void DisenrollTest()
        {
            //Arrange
            DonorController donorController = new DonorController();

            //Act
            testDonor = homeController.LoginAsDonor(testDonor.Email, testDonor.Password);
            donorController.Disenroll(testDonor);

            //Assert
            Assert.IsTrue(homeController.LoginAsDonor(testDonor.Email, testDonor.Password) == null);
        }
    }
}
