using System;
using DonorSystem.DAO;
using DonorSystem.Models;

namespace DonorSystem.Controllers
{
    class DonorController
    {
        readonly DonorsDAO donorsDAO;

        /// <summary>Initializes a new instance of the <see cref="T:DonorSystem.Controllers.DonorController" /> class and encapsulates the logic for the donor role.</summary>
        public DonorController()
        {
            donorsDAO = new DonorsDAO();
        }

        /// <summary>Displays the possible options after a donor's blood has been received.</summary>
        /// <param name="donor">The donor.</param>
        /// <exception cref="FormatException">Value must be an integer.</exception>
        public void DonorInteractions(Donors donor)
        {
            Console.WriteLine($"Congratulations! {donor.Status} accepted your generous gesture and you donated your blood to him.");
            Console.WriteLine("Do you wish to enroll to donate blood again or leave the blood donation program?");
            Console.WriteLine("1. I wish to enroll again.");
            Console.WriteLine("2. I want to leave the program.");

            int command;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out command)) throw new FormatException("Value must be an integer.");

            } while (command != 1 && command != 2);

            if (command == 1)
            {
                donorsDAO.EnrollAgain(donor);
                Console.WriteLine("You've successfully enrolled in the program again. ");
                Console.WriteLine("Press any key to return.");
                Console.ReadKey();
            }
            else
            {
                donorsDAO.DeleteDonor(donor);
                Console.WriteLine("You've successfully unrolled from the program. You may return back at any time.");
                Console.WriteLine("Press any key to return.");
                Console.ReadKey();
            }
        }
    }
}
