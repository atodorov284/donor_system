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
    /// <summary>
    /// Разделя логиката от графичния интерефейс на класа HomeMenu.
    /// </summary>
    public class HomeController
    {
        private HomeDAO homeDAO;
        private WebsitesDAO websitesDAO;
         
        /// <summary>
        /// Взема информация за уебсайтовете под формата на стринг.
        /// </summary>
        /// <returns>Таблицата с уебсайтовете под формата на стринг.</returns>
        public string GetUsefulInfo()
        {
            return websitesDAO.ToString();
        }

        /// <summary>
        /// Влиза в системата като дарител със съответно въведените данни.
        /// </summary>
        /// <param name="email">Имейл, с който потребителя се е регистрирал.</param>
        /// <param name="password">Парола, с която потребителя се е регистрирал.</param>
        /// <returns>Дарителя, ако е в системата, или null.</returns>
        public Donor LoginAsDonor(string email, string password)
        {
            return homeDAO.DonorLogin(email, password);
        }

        /// <summary>
        /// Влиза в системата като пациент със съответно въведените данни.
        /// </summary>
        /// <param name="email">Имейл, с който потребителя се е регистрирал.</param>
        /// <param name="password">Парола, с която потребителя се е регистрирал.</param>
        /// <returns>Пациента, ако е в системата, или null.</returns>
        public Patient LoginAsPatient(string email, string password)
        {
            return homeDAO.PatientLogin(email, password);
        }

        /// <summary>
        /// Регистрира данните на дарителя в системата.
        /// </summary>
        /// <param name="email">Имейл</param>
        /// <param name="password">Парола</param>
        /// <param name="name">Име</param>
        /// <param name="phoneNumber">Телефонен номер</param>
        /// <param name="status">Статус</param>
        /// <param name="bloodGroup">Кръвна група</param>
        /// <returns>Връща дали успешно е регистриран дарителя в системата.</returns>
        public bool RegisterAsDonor(string email, string password, string name, string phoneNumber, string status, string bloodGroup)
        {
            Donor donor = new Donor();
            donor.Email = email;
            donor.Password = password;
            donor.Name = name;
            donor.PhoneNumber = phoneNumber;
            donor.Status = status;
            donor.BloodGroup = bloodGroup;

            if (homeDAO.DonorRegister(donor) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Регистрира данните на пациента в системата.
        /// </summary>
        /// <param name="email">Имейл</param>
        /// <param name="password">Парола</param>
        /// <param name="name">Име</param>
        /// <param name="phoneNumber">Телефонен номер</param>
        /// <param name="bloodGroup">Кръфвна група</param>
        /// <param name="diagnose">Диагноза</param>
        /// <returns></returns>
        public bool RegisterAsPatient(string email, string password, string name, string phoneNumber, string bloodGroup, string diagnose)
        {
            Patient patient = new Patient();
            patient.Email = email;
            patient.Password = password;
            patient.Name = name;
            patient.PhoneNumber = phoneNumber;
            patient.BloodGroup = bloodGroup;
            patient.Diagnose = diagnose;

            if (homeDAO.PatientRegister(patient) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        /// <summary>
        /// Генерира кодирана версия на парола.
        /// </summary>
        /// <param name="password">Стринг с некодираната парола.</param>
        /// <returns>Кодираната стойност на паролата.</returns>
        public string HashPassword(string password)
        {
            var provider = new SHA1CryptoServiceProvider();
            var encoding = new UnicodeEncoding();
            return Convert.ToBase64String(provider.ComputeHash(encoding.GetBytes(password)));
        }

        /// <summary>
        /// Проверява дали имейл адресът съществува
        /// </summary>
        /// <param name="email">Имейл адрес</param>
        /// <returns>true при въвеждане на съществуващ в системата имейл адрес, в обратен случай false.</returns>
        public bool ValidateEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public HomeController()
        {
            homeDAO = new HomeDAO();
            websitesDAO = new WebsitesDAO();
        }
    }
}
