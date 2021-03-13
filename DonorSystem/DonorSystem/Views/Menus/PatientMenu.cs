using System;
using System.Collections.Generic;
using System.Text;
using DonorSystem.Models;
using DonorSystem.DAO;
using System.Linq;
using DonorSystem.Controllers;

namespace DonorSystem.Views
{
    /// <summary>
    /// Отговаря за менюто с пациентите от конзолния интерфейс,
    /// може да се достигне от началното меню.
    /// </summary>
    class PatientMenu
    {
        PatientController patientController;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="patient"></param>
        public PatientMenu(Patient patient)
        {
            patientController = new PatientController();
            ShowPatientMenu(patient);
        }

        /// <summary>
        /// Приветства успешно влезлия потребител(пациент).
        /// </summary>
        /// <param name="patient"></param>
        public void ShowPatientMenu(Patient patient)
        {
            int donors = 15;
            Console.WriteLine($"Login successful! Welcome {patient.Name}.");
            Console.WriteLine($"Select how many potential donors you want displayed. Max: {donors}");
            Console.WriteLine("If the number you type is greater than what we have, only the available will be shown.");
            do
            {
                Console.Write("Number of donors: ");
                donors = int.Parse(Console.ReadLine());
            } while (donors >= 15);
            patientController.ShowPotentialDonors(patient, donors);
            Console.ReadKey();
        }

    }
}
