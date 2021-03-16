using System;
using System.Collections.Generic;
using System.Text;
using DonorSystem.Models;
using DonorSystem.DAO;
using System.Linq;

namespace DonorSystem.Controllers
{
    /// <summary>
    /// Управлява базата от данни с пациентите и операциите, свързани с нея.
    /// </summary>
    public class PatientController
    {
        private PatientsDAO patientsDAO;
        private DonorsDAO donorsDAO;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public PatientController()
        {
            patientsDAO = new PatientsDAO();
            donorsDAO = new DonorsDAO();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="numberOfDonors"></param>
        /// <returns></returns>
        public List<Donor> GetPotentialDonors(Patient patient, int numberOfDonors)
        {
            List<Donor> potentialDonors = patientsDAO.FindCompatibleDonors(patient);
            potentialDonors = potentialDonors.OrderBy(d => d.BloodGroup).Take(numberOfDonors).ToList();

            return potentialDonors;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="donatingDonor"></param>
        public void ReceiveBlood(Patient patient, Donor donatingDonor)
        {
            donorsDAO.TransfuseBlood(donatingDonor, patient);
            patientsDAO.DeletePatient(patient);
        }

        /// <summary>
        /// Изтрива пациента от БД.
        /// </summary>
        /// <remarks>
        /// Този метод е необходим при unit testing.
        /// </remarks>
        /// <param name="patient">Пациентът, който ще бъде изтрит.</param>
        public void DeletePatient(Patient patient)
        {
            patientsDAO.DeletePatient(patient);
        }
    }
}
