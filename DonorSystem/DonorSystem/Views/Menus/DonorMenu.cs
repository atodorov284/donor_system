using System;
using DonorSystem.Models;
using DonorSystem.Controllers;

namespace DonorSystem.Views
{
    class DonorMenu
    {
        readonly DonorController donorController;

        /// <summary>Initializes a new instance of the <see cref="T:DonorSystem.Views.DonorMenu" /> class and displays the menu for the donor role.</summary>
        /// <param name="donor">The donor.</param>
        public DonorMenu(Donors donor)
        {
            donorController = new DonorController();
            ShowDonorMenu(donor);
        }

        /// <summary>Shows the donor menu and checks if donor's blood has been received and redirects.</summary>
        /// <param name="donor">The donor.</param>
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
