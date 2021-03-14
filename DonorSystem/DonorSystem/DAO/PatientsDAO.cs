using System;
using System.Collections.Generic;
using DonorSystem.Models;
using System.Linq;

namespace DonorSystem.DAO
{
    class PatientsDAO
    {
        readonly DonorDBContext context;

        /// <summary>Initializes a new instance of the <see cref="T:DonorSystem.DAO.PatientsDAO" /> class which controls the Patients table in the database.</summary>
        public PatientsDAO()
        {
            context = new DonorDBContext();
        }

        /// <summary>Finds all compatible donors for the patient.</summary>
        /// <param name="patient">The patient.</param>
        /// <returns>List&lt;Donors&gt;.</returns>
        public List<Donors> FindCompatibleDonors(Patients patient)
        {
            string bloodGroup = patient.BloodGroup;
            List<Donors> potentialDonors = context.Donors.Where(d => d.BloodGroup == bloodGroup || d.BloodGroup == $"0{bloodGroup[1]}").ToList();
            return potentialDonors;
        }

        /// <summary>Deletes the patient from the database.</summary>
        /// <param name="patient">The patient.</param>
        public void DeletePatient(Patients patient)
        {
            var patientToDelete = context.Patients.First(p => p.PatientId == patient.PatientId);
            context.Patients.Remove(patientToDelete);
            context.SaveChanges();
        }
    }
}
