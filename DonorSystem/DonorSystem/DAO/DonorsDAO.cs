using System;
using System.Collections.Generic;
using System.Text;
using DonorSystem.Models;
using System.Linq;

namespace DonorSystem.DAO
{
    class DonorsDAO
    {
        DonorDBContext context;
        public DonorsDAO()
        {
            context = new DonorDBContext();
        }

        public void ChangeDonorStatus(Donor donor)
        {
            var updatedDonor = context.Donors.First(d => d.DonorId == donor.DonorId);
            updatedDonor.Status = "Available";
            context.SaveChanges();
        }

        public void DeleteDonor(Donor donor)
        {
            var donorToDelete = context.Donors.First(d => d.DonorId == donor.DonorId);
            context.Donors.Remove(donorToDelete);
            context.SaveChanges();
        }

        public void TransfuseBlood(Donor donatingDonor, Patient receivingPatient)
        {
            var updatedDonor = context.Donors.First(d => d.DonorId == donatingDonor.DonorId);
            updatedDonor.Status = $"{receivingPatient.Name}";
            context.SaveChanges();
        }
    }
}
