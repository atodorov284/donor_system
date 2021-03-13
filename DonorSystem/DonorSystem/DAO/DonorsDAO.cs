using System;
using DonorSystem.Models;
using System.Linq;

namespace DonorSystem.DAO
{
    class DonorsDAO
    {
        readonly DonorDBContext context;
        public DonorsDAO()
        {
            context = new DonorDBContext();
        }

        public void ChangeDonorStatus(Donors donor)
        {
            var updatedDonor = context.Donors.First(d => d.DonorId == donor.DonorId);
            updatedDonor.Status = "Available";
            context.SaveChanges();
        }

        public void DeleteDonor(Donors donor)
        {
            var donorToDelete = context.Donors.First(d => d.DonorId == donor.DonorId);
            context.Donors.Remove(donorToDelete);
            context.SaveChanges();
        }

        public void TransfuseBlood(Donors donatingDonor, Patients receivingPatient)
        {
            var updatedDonor = context.Donors.First(d => d.DonorId == donatingDonor.DonorId);
            updatedDonor.Status = $"{receivingPatient.Name}";
            context.SaveChanges();
        }
    }
}
