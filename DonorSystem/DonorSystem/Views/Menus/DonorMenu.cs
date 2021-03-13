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
    class DonorMenu
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
