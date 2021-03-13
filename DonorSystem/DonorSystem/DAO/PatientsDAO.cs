using System;
using System.Collections.Generic;
using DonorSystem.Models;
using System.Linq;

namespace DonorSystem.DAO
{
    class PatientsDAO
    {
        readonly DonorDBContext context;

        public PatientsDAO()
        {
            context = new DonorDBContext();
        }

        public List<Donors> FindCompatibleDonors(Patients patient)
        {
            string bloodGroup = patient.BloodGroup;
            List<Donors> potentialDonors = context.Donors.Where(d => d.BloodGroup == bloodGroup || d.BloodGroup == $"0{bloodGroup[1]}").ToList();
            return potentialDonors;
        }

        public void DeletePatient(Patients patient)
        {
            var patientToDelete = context.Patients.First(p => p.PatientId == patient.PatientId);
            context.Patients.Remove(patientToDelete);
            context.SaveChanges();
        }
    }
}
