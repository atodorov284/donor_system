using System;
using DonorSystem.Models;
using DonorSystem.Controllers;

namespace DonorSystem.Views
{
    public class DonorMenu
    {
        DonorController donorController;

        /// <summary>Initializes a new instance of the <see cref="T:DonorSystem.Views.DonorMenu" /> class and displays the menu for the donor role.</summary>
        /// <param name="donor">The donor.</param>
        public DonorMenu(Donor donor)
        {
            donorController = new DonorController();
            ShowDonorMenu(donor);
        }

        /// <summary>Shows the donor menu and checks if donor's blood has been received and redirects.</summary>
        /// <param name="donor">The donor.</param>
        private void ShowDonorMenu(Donor donor)
        {
            Console.WriteLine($"Login successful! Welcome {donor.Name}.");
            if (donor.Status != "Available")
            {
                Console.WriteLine($"Congratulations! {donor.Status} accepted your generous gesture and you donated your blood to them.");
                Console.WriteLine("Do you wish to enroll to donate blood again or leave the blood donation program?");
                Console.WriteLine("1. I wish to enroll again.");
                Console.WriteLine("2. I want to leave the program.");

                int command = 0;
                do
                {
                    if (!int.TryParse(Console.ReadLine(), out command)) throw new FormatException("Value must be an integer.");

                } while (command != 1 && command != 2);

                switch(command)
                {
                    case 1:
                        donorController.Enroll(donor);
                        Console.WriteLine("You've successfully enrolled in the program again. ");
                        break;
                    case 2:
                        donorController.Unroll(donor);
                        Console.WriteLine("You've successfully unrolled from the program. You may return back at any time.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("You are enrolled in the program. Patients will connect with you soon.");
            }
        }
    }
}
