using System;
using System.Text;
using DonorSystem.DAO;
using DonorSystem.Models;

namespace DonorSystem.Controllers
{
    public class DonorController
    {
        readonly DonorsDAO donorsDAO;

        /// <summary>Initializes a new instance of the <see cref="T:DonorSystem.Controllers.DonorController" /> class and encapsulates the logic for the donor role.</summary>
        public DonorController()
        {
            donorsDAO = new DonorsDAO();
        }

        /// <summary>Enrolls the specified donor and changes their status back to Available.</summary>
        /// <param name="donor">The donor.</param>
        public void Enroll(Donor donor)
        {
            donorsDAO.ChangeDonorStatus(donor);
        }

        /// <summary>Unrolls the specified donor and deletes them from the system.</summary>
        /// <param name="donor">The donor.</param>
        public void Unroll(Donor donor)
        {
            donorsDAO.DeleteDonor(donor);
        }
    }
}
