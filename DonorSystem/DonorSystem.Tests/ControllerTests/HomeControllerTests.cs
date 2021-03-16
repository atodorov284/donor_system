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
    /// Tests the HomeController class.
    /// </summary>
    [TestFixture]
    class HomeControllerTests
    {
        /// <summary>
        /// Tests if an existing donor can login in the system.
        /// </summary>
        [Test]
        public void LoginAsDonorTest()
        {
            //Arrange
            HomeController homeController = new HomeController();
            string email = "petur.stoqnov@abv.bg";
            string password = "123456";
            Donor expected = new Donor();
            expected.DonorId = 1;
            expected.Name = "Petur Stoqnov";
            expected.Email = "petur.stoqnov@abv.bg";
            expected.Password = "btWDPPNShuv4Zit7WUnw10K77D8=";
            expected.PhoneNumber = "0881231234";
            expected.Status = null;
            expected.BloodGroup = "A+";

            //Act
            Donor actual = homeController.LoginAsDonor(email, homeController.HashPassword(password));

            //Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests if an existing patient can login in the system.
        /// </summary>
        [Test]
        public void LoginAsPatientTest()
        {
            //Arrange
            HomeController homeController = new HomeController();
            string email = "test.patient@abv.bg";
            string password = "123456";
            Patient expected = new Patient();
            expected.PatientId = 1;
            expected.Name = "Test Patient";
            expected.Email = "test.patient@abv.bg";
            expected.Password = "btWDPPNShuv4Zit7WUnw10K77D8=";
            expected.PhoneNumber = "0888123456";
            expected.Diagnose = "test";
            expected.BloodGroup = "B-";

            //Act
            Patient actual = homeController.LoginAsPatient(email, homeController.HashPassword(password));

            //Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests if there can be registered a donor
        /// that does not exist currently in the system.
        /// </summary>
        [Test]
        public void RegisterAsDonorTest()
        {
            //Arrange
            DonorController donorController = new DonorController();

            HomeController homeController = new HomeController();
            string name = "Ralitsa Mladenova";
            string email = "ralitsa.mladenova@abv.bg";
            string password = "6Pl/upEE0epQR5SObftn+s2fW3M=";
            string phoneNumber = "0888654321";
            string status = "Available";
            string bloodGroup = "0+";

            //Act
            bool expected = homeController.RegisterAsDonor(email, password, name, phoneNumber, status, bloodGroup);
            Donor donor = homeController.LoginAsDonor(email, password);

            //Assert
            Assert.IsTrue(expected && donor != null);

            //Clean-Up
            donorController.Disenroll(donor);
        }

        /// <summary>
        /// Tests if there can be registered a donor
        /// that already exist currently in the system.
        /// </summary>
        [Test]
        public void RegisterAsDonorTest2()
        {
            //Arrange
            HomeController homeController = new HomeController();
            string name = "Stefan Stoqnov";
            string email = "stefan.stoqnov@abv.bg";
            string password = "btWDPPNShuv4Zit7WUnw10K77D8=";
            string phoneNumber = "0881231234";
            string status = null;
            string bloodGroup = "0-";

            //Act
            bool expected = homeController.RegisterAsDonor(email, password, name, phoneNumber, status, bloodGroup);
            Donor donor = homeController.LoginAsDonor(email, password);

            //Assert
            Assert.IsFalse(expected && donor != null);
        }

        /// <summary>
        /// Tests if there can be registered a patient
        /// that does not exist currently in the system.
        /// </summary>
        [Test]
        public void RegisterAsPatientTest()
        {
            //Arrange
            PatientController patientController = new PatientController();

            HomeController homeController = new HomeController();
            string name = "Test Patient 2";
            string email = "test.patient2@abv.bg";
            string password = "btWDPPNShuv4Zit7WUnw10K77D8=";
            string phoneNumber = "0888123456";
            string diagnose = "test";
            string bloodGroup = "B-";

            //Act
            bool expected = homeController.RegisterAsPatient(email, password, name, phoneNumber, bloodGroup, diagnose);
            Patient patient = homeController.LoginAsPatient(email, password);

            //Assert
            Assert.IsTrue(expected && patient != null);

            //Clean-Up
            patientController.DeletePatient(patient);
        }

        /// <summary>
        /// Tests if there can be registered a patient
        /// that already exist currently in the system.
        /// </summary>
        [Test]
        public void RegisterAsPatientTest2()
        {
            //Arrange
            HomeController homeController = new HomeController();
            string name = "Test Patient";
            string email = "test.patient@abv.bg";
            string password = "btWDPPNShuv4Zit7WUnw10K77D8=";
            string phoneNumber = "0888123456";
            string diagnose = "test";
            string bloodGroup = "B-";

            //Act
            bool expected = homeController.RegisterAsPatient(email, password, name, phoneNumber, bloodGroup, diagnose);

            //Assert
            Assert.IsFalse(expected);
        }

        /// <summary>
        /// Checks if the function GetUsefulInfo() returns the right information.
        /// </summary>
        [Test]
        public void GetUsefulInfoTest()
        {
            //Arrange
            HomeController homeController = new HomeController();
            string output = Environment.NewLine +
                            "1. https://www.blood.co.uk/ Explore being a donor, the donation process and centres where you can give blood in the UK." + Environment.NewLine +
                            "2. https://www.redcrossblood.org/ Helping others in need just feels good. Donate blood today." + Environment.NewLine +
                            "3. https://www.friends2support.org/ World's Largest Voluntary Blood Donors Database";

            //Act
            Console.WriteLine(homeController.GetUsefulInfo());

            //Assert
            Assert.AreEqual(output, homeController.GetUsefulInfo());
        }

        /// <summary>
        /// Checks if a string is in the correct email format.
        /// The tested string is in a correct format.
        /// </summary>
        [Test]
        public void ValidateEmailTest()
        {
            //Arrange
            HomeController homeController = new HomeController();
            string email = "email.format@email.com";

            //Act

            //Assert
            Assert.IsTrue(homeController.ValidateEmail(email));
        }

        /// <summary>
        /// Checks if a string is in the correct email format.
        /// The tested string is not in a correct format.
        /// </summary>
        [Test]
        public void ValidateEmailTest2()
        {
            //Arrange
            HomeController homeController = new HomeController();
            string email = "notanemail@";

            //Act

            //Assert
            Assert.IsFalse(homeController.ValidateEmail(email));
        }
    }
}
