using System;
using System.Collections.Generic;
using System.Text;
using DonorSystem.Models;
using System.Linq;

namespace DonorSystem.DAO
{
    public class DonorsDAO
    {
        DonorDBContext context;

        /// <summary>Initializes a new instance of the <see cref="T:DonorSystem.DAO.DonorsDAO" /> class which controls the Donors table in the database.</summary>
        public DonorsDAO()
        {
            context = new DonorDBContext();
        }

        /// <summary>Changes the donor status back to Available and enrolls him back in the system..</summary>
        /// <param name="donor">The donor.</param>
        public void ChangeDonorStatus(Donor donor)
        {
            var updatedDonor = context.Donors.FirstOrDefault(d => d.DonorId == donor.DonorId);
            updatedDonor.Status = "Available";
            donor.Status = "Available";
            context.SaveChanges();
        }

        /// <summary>Deletes the donor from the database.</summary>
        /// <param name="donor">The donor.</param>
        public void DeleteDonor(Donor donor)
        {
            var donorToDelete = context.Donors.First(d => d.DonorId == donor.DonorId);
            context.Donors.Remove(donorToDelete);
            context.SaveChanges();
        }

        /// <summary>Patient receives the donor's blood and updates the donor's status to the patient's name.</summary>
        /// <param name="donatingDonor">The donating donor.</param>
        /// <param name="receivingPatient">The receiving patient.</param>
        public void TransfuseBlood(Donor donatingDonor, Patient receivingPatient)
        {
            var updatedDonor = context.Donors.First(d => d.DonorId == donatingDonor.DonorId);
            updatedDonor.Status = $"{receivingPatient.Name}";
            donatingDonor.Status = $"{receivingPatient.Name}";
            context.SaveChanges();
        }
    }
}
