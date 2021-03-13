﻿using System;
using System.Collections.Generic;
using System.Text;
using DonorSystem.DAO;
using DonorSystem.Models;

namespace DonorSystem.Controllers
{
    /// <summary>
    /// Управлява базата данни с дарителите и операциите, свързани с нея.
    /// </summary>
    class DonorController
    {
        DonorsDAO donorsDAO;

        /// <summary>
        /// 
        /// </summary>
        public DonorController()
        {
            donorsDAO = new DonorsDAO();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="donor"></param>
        public void DonorInteractions(Donor donor)
        {
            Console.WriteLine($"Congratulations! {donor.Status} accepted your generous gesture and you donated your blood to him.");
            Console.WriteLine("Do you wish to enroll to donate blood again or leave the blood donation program?");
            Console.WriteLine("1. I wish to enroll again.");
            Console.WriteLine("2. I want to leave the program.");

            int command = 0;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out command)) throw new FormatException("Value must be an integer.");

            } while (command != 1 && command != 2);

            if (command == 1)
            {
                donorsDAO.ChangeDonorStatus(donor);
                Console.WriteLine("You've successfully enrolled in the program again. ");
                Console.WriteLine("Press any key to return.");
                Console.ReadKey();
            }
            else
            {
                donorsDAO.DeleteDonor(donor);
                Console.WriteLine("You've successfully unenrolled from the program. You may return back at any time.");
                Console.WriteLine("Press any key to return.");
                Console.ReadKey();
            }
        }
    }
}
