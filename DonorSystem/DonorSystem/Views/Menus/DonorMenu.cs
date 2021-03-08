using System;
using System.Collections.Generic;
using System.Text;
using DonorSystem.Models;
using DonorSystem.DAO;
using DonorSystem.Controllers;

namespace DonorSystem.Views
{
    class DonorMenu
    {
        DonorController donorController;
        public DonorMenu(Donors donor)
        {
            donorController = new DonorController();
            ShowDonorMenu(donor);
        }

        private void ShowDonorMenu(Donors donor)
        {
            Console.WriteLine($"Login successful! Welcome {donor.Name}.");
            if (donor.Status != "Available")
            {
                donorController.DonorInteractions(donor);
            }
            else
            {
                Console.WriteLine("You are enrolled in the program. Patients will connect with you soon.");
                Console.WriteLine("Press any key to return.");
                Console.ReadKey();
            }
        }
    }
}
