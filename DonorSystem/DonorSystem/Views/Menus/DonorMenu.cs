using System;
using DonorSystem.Models;
using DonorSystem.Controllers;

namespace DonorSystem.Views
{
    class DonorMenu
    {
        readonly DonorController donorController;
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
