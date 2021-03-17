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
        public void LoginAsDonorSuccessfulTest()
        {
            //Arrange
            HomeController homeController = new HomeController();
            string email = "petur.stoqnov@abv.bg";
            string password = "123456";
            Donor expected = new Donor
            {
                DonorId = 1,
                Name = "Petur Stoqnov",
                Email = "petur.stoqnov@abv.bg",
                Password = "btWDPPNShuv4Zit7WUnw10K77D8=",
                PhoneNumber = "0881231234",
                Status = "Available",
                BloodGroup = "A+"
            };

            //Act
            Donor actual = homeController.LoginAsDonor(email, homeController.HashPassword(password));

            //Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests if an existing patient can login in the system.
        /// </summary>
        [Test]
        public void LoginAsPatientSuccessfulTest()
        {
            //Arrange
            HomeController homeController = new HomeController();
            string email = "dimitur.dimitrov@abv.bg";
            string password = "123456";
            Patient expected = new Patient
            {
                PatientId = 1,
                Name = "Dimitur Dimitrov",
                Email = "dimitur.dimitrov@abv.bg",
                Password = "btWDPPNShuv4Zit7WUnw10K77D8=",
                PhoneNumber = "0881231234",
                Diagnose = "Blood Cancer",
                BloodGroup = "A+"
            };

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
            donorController.Unroll(donor);
        }

        /// <summary>
        /// Tests if there can be registered a donor
        /// that already exist currently in the system.
        /// </summary>
        [Test]
        public void RegisterAlreadyExistingDonorTest()
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
            Assert.IsTrue(patient != null);

            //Clean-Up
            patientController.DeletePatient(patient);
        }

        /// <summary>
        /// Tests if there can be registered a patient
        /// that already exist currently in the system.
        /// </summary>
        [Test]
        public void RegisterAlreadyExistingPatientTest()
        {
            //Arrange
            HomeController homeController = new HomeController();
            string name = "Dimitur Dimitrov";
            string email = "dimitur.dimitrov@abv.bg";
            string password = "btWDPPNShuv4Zit7WUnw10K77D8=";
            string phoneNumber = "0881231234";
            string diagnose = "Blood Cancer";
            string bloodGroup = "A+";

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
                            "1. https://www.blood.co.uk/ Explore being a donor, the donation process and centres where you can give blood in the UK." +
                            Environment.NewLine +
                            "2. https://www.redcrossblood.org/ Helping others in need just feels good. Donate blood today." +
                            Environment.NewLine +
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
        public void ValidEmailTest()
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
        public void InvalidEmailTest()
        {
            //Arrange
            HomeController homeController = new HomeController();
            string email = "notanemail@";

            //Act

            //Assert
            Assert.IsFalse(homeController.ValidateEmail(email));
        }

        /// <summary>
        /// Tests if a password is too short to meet the validation.
        /// </summary>
        [Test]
        public void PasswordIsShortTest()
        {
            //Arrange 
            HomeController homeController = new HomeController();
            string shortPassword = "12345";

            //Act
            bool result = homeController.InvalidPassword(shortPassword);

            //Assert
            Assert.IsTrue(result);

        }

        /// <summary>
        /// Tests a valid password.
        /// </summary>
        [Test]
        public void PasswordIsValidTest()
        {
            //Arrange 
            HomeController homeController = new HomeController();
            string validPassword = "123456";

            //Act
            bool result = homeController.InvalidPassword(validPassword);

            //Assert
            Assert.IsFalse(result);

        }

        /// <summary>
        /// Tests if the PasswordMismatch method detects 2 different passwords.
        /// </summary>
        [Test]
        public void PasswordMismatchTest()
        {
            //Arrange 
            HomeController homeController = new HomeController();
            string firstPassword = "random text";
            string secondPassword = "different text";

            //Act
            bool result = homeController.PasswordMismatch(firstPassword, secondPassword);

            //Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests if a name is too short.
        /// </summary>
        [Test]
        public void NameIsShortTest()
        {
            //Arrange 
            HomeController homeController = new HomeController();
            string shortName = "o";

            //Act
            bool result = homeController.InvalidName(shortName);

            //Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests if a name contains digits which is not allowed.
        /// </summary>
        [Test]
        public void NameContainsDigitsTest()
        {
            //Arrange 
            HomeController homeController = new HomeController();
            string nameDigits = "test123";

            //Act
            bool result = homeController.InvalidName(nameDigits);

            //Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests a valid name.
        /// </summary>
        [Test]
        public void NameIsValidTest()
        {
            //Arrange 
            HomeController homeController = new HomeController();
            string validName = "Petur Stoqnov";

            //Act
            bool result = homeController.InvalidName(validName);

            //Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests if a phone number is not exactly 10 digits.
        /// </summary>
        [Test]
        public void PhoneNumberIsNotTenDigitsTest()
        {
            //Arrange 
            HomeController homeController = new HomeController();
            string shortPhoneNumber = "123";

            //Act
            bool result = homeController.InvalidPhoneNumber(shortPhoneNumber);

            //Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests a valid phone number.
        /// </summary>
        [Test]
        public void PhoneNumberIsValidTest()
        {
            //Arrange 
            HomeController homeController = new HomeController();
            string validPhoneNumber = "0881231234";

            //Act
            bool result = homeController.InvalidPhoneNumber(validPhoneNumber);

            //Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests if a random string is not a valid blood group.
        /// </summary>
        [Test]
        public void BloodGroupIsInvalidTest()
        {
            //Arrange 
            HomeController homeController = new HomeController();
            string invalidGroup = "og";

            //Act
            bool result = homeController.InvalidBloodGroup(invalidGroup);

            //Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests a valid blood group.
        /// </summary>
        [Test]
        public void BloodGroupIsValidTest()
        {
            //Arrange 
            HomeController homeController = new HomeController();
            string validBloodGroup = "0-";

            //Act
            bool result = homeController.InvalidBloodGroup(validBloodGroup);

            //Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests an invalid diagnose with special characters and digits.
        /// </summary>
        [Test]
        public void DiagnoseIsInvalidTest()
        {
            //Arrange 
            HomeController homeController = new HomeController();
            string invalidDiagnose = "%35r342!";

            //Act
            bool result = homeController.InvalidDiagnose(invalidDiagnose);

            //Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests a valid diagnose.
        /// </summary>
        [Test]
        public void DiagnoseIsValidTest()
        {
            //Arrange 
            HomeController homeController = new HomeController();
            string validDiagnose = "Blood Cancer";

            //Act
            bool result = homeController.InvalidDiagnose(validDiagnose);

            //Assert
            Assert.IsFalse(result);
        }
    }
}
