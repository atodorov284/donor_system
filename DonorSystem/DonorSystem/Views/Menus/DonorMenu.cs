using System;
using System.Collections.Generic;
using System.Text;
using DonorSystem.Models;
using DonorSystem.DAO;
using DonorSystem.Controllers;

namespace DonorSystem.Views
{

    /// <summary>
    /// Отговаря за менюто с дарителите от конзолния интерфейс,
    /// може да се достигне от началното меню.
    /// </summary>
    public class DonorMenu
    {
        DonorController donorController;

        /// <summary>
        /// Конструктор. 
        /// </summary>
        /// <param name="donor"></param>
        public DonorMenu(Donor donor)
        {
            donorController = new DonorController();
            ShowDonorMenu(donor);
        }

        /// <summary>
        /// Приветства успешно влезлия потребител(дарител).
        /// </summary>
        /// <param name="donor"></param>
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

                if (command == 1)
                {
                    donorController.Enroll(donor);
                    Console.WriteLine("You've successfully enrolled in the program again. ");
                }
                else
                {
                    donorController.Disenroll(donor);
                    Console.WriteLine("You've successfully disenrolled from the program. You may return back at any time.");
                }
            }
            else
            {
                Console.WriteLine("You are enrolled in the program. Patients will connect with you soon.");
            }
        }
    }
}
