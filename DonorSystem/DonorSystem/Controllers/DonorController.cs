using System;
using System.Collections.Generic;
using System.Text;
using DonorSystem.DAO;
using DonorSystem.Models;

namespace DonorSystem.Controllers
{
    /// <summary>
    /// Управлява базата данни с дарителите и операциите, свързани с нея.
    /// </summary>
    public class DonorController
    {
        DonorsDAO donorsDAO;

        public DonorController()
        {
            donorsDAO = new DonorsDAO();
        }

        public void Enroll(Donor donor)
        {
            donorsDAO.ChangeDonorStatus(donor);
        }
        
        public void Disenroll(Donor donor)
        {
            donorsDAO.DeleteDonor(donor);
        }

    }
}
