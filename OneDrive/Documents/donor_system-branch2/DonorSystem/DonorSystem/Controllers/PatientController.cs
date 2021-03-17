using System;
using System.Collections.Generic;
using System.Text;
using DonorSystem.Models;
using DonorSystem.DAO;
using System.Linq;

namespace DonorSystem.Controllers
{
    public class PatientController
    {
        private PatientsDAO patientsDAO;
        private DonorsDAO donorsDAO;

        /// <summary>Initializes a new instance of the <see cref="T:DonorSystem.Controllers.PatientController" /> class and encapsulates the logic for the patient role.</summary>
        public PatientController()
        {
            patientsDAO = new PatientsDAO();
            donorsDAO = new DonorsDAO();
        }

        /// <summary>Gets the potential donors.</summary>
        /// <param name="patient">The patient.</param>
        /// <param name="numberOfDonors">The number of donors.</param>
        /// <returns>List&lt;Donor&gt;.</returns>
        public List<Donor> GetPotentialDonors(Patient patient, int numberOfDonors)
        {
            List<Donor> potentialDonors = patientsDAO.FindCompatibleDonors(patient);
            potentialDonors = potentialDonors.OrderBy(d => d.BloodGroup).Take(numberOfDonors).ToList();

            return potentialDonors;
        }

        /// <summary>The patient receives the blood from the donor.</summary>
        /// <param name="patient">The patient.</param>
        /// <param name="donatingDonor">The donating donor.</param>
        public void ReceiveBlood(Patient patient, Donor donatingDonor)
        {
            donorsDAO.TransfuseBlood(donatingDonor, patient);
            patientsDAO.DeletePatient(patient);
        }

        /// <summary>Deletes the patient from the database.</summary>
        /// <param name="patient">The patient.</param>
        public void DeletePatient(Patient patient)
        {
            patientsDAO.DeletePatient(patient);
        }
    }
}
