using System;
using System.Collections.Generic;
using System.Text;
using DonorSystem.Models;
using System.Linq;

namespace DonorSystem.DAO
{
    public class PatientsDAO
    {
        DonorDBContext context;

        public PatientsDAO()
        {
            context = new DonorDBContext();
        }

        public List<Donor> FindCompatibleDonors(Patient patient)
        {
            string bloodGroup = patient.BloodGroup;
            List<Donor> potentialDonors = context.Donors.Where(d => d.BloodGroup == bloodGroup || d.BloodGroup == $"0{bloodGroup[1]}").ToList();
            return potentialDonors;
        }

        public void DeletePatient(Patient patient)
        {
            var patientToDelete = context.Patients.First(p => p.PatientId == patient.PatientId);
            context.Patients.Remove(patientToDelete);
            context.SaveChanges();
        }
    }
}
