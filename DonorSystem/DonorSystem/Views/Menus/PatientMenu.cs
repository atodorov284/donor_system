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
    public class PatientMenu
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
        /// Приветства успешно влезлия в системата пациент.
        /// </summary>
        /// <param name="patient"></param>
        public void ShowPatientMenu(Patient patient)
        {
            int numberOfDonors = 15;
            Console.WriteLine($"Login successful! Welcome {patient.Name}.");
            Console.WriteLine($"Select how many potential donors you want displayed. Max: {numberOfDonors}");
            Console.WriteLine("If the number you type is greater than what we have, only the available will be shown.");
            do
            {
                Console.Write("Number of donors: ");
                numberOfDonors = int.Parse(Console.ReadLine());
            } while (numberOfDonors >= 15);

            List<Donor> potentialDonors = patientController.GetPotentialDonors(patient, numberOfDonors);
            Console.WriteLine($"Here are all the donors compatible with your blood group {patient.BloodGroup} and their phone numbers.");
            for (int i = 0; i < potentialDonors.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {potentialDonors[i].Name} {potentialDonors[i].PhoneNumber}");
            }

            Console.WriteLine();
            Console.Write("Pick the number of donor you want blood from: ");
            int donorIndex = 0;
            do
            {
                donorIndex = int.Parse(Console.ReadLine());
            } while (donorIndex <= 0 && donorIndex > numberOfDonors);
            Donor donatingDonor = potentialDonors[donorIndex - 1];
            patientController.ReceiveBlood(patient, donatingDonor);
            Console.WriteLine($"You successfully received blood from {donatingDonor.Name}. You can call them to thank.");
            Console.WriteLine($"We hope your {patient.Diagnose} will be cured. Your account will be deleted now.");
            Console.WriteLine("If you want to receive blood again you can register at any time.");
        }

    }
}
