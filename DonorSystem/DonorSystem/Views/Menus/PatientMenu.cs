using System;
using System.Collections.Generic;
using DonorSystem.Models;
using DonorSystem.Controllers;

namespace DonorSystem.Views
{
    public class PatientMenu
    {
        readonly PatientController patientController;

        /// <summary>
        ///   <para>
        /// Initializes a new instance of the <see cref="T:DonorSystem.Views.PatientMenu" /> class and displays the menu for the patient role.</para>
        /// </summary>
        /// <param name="patient">The patient.</param>
        public PatientMenu(Patient patient)
        {
            patientController = new PatientController();
            ShowPatientMenu(patient);
        }

        /// <summary>Shows the patient menu and gets input from the user to receive blood.</summary>
        /// <param name="patient">The patient.</param>
        /// <exception cref="FormatException">Value must be an integer. Try again.</exception>
        public void ShowPatientMenu(Patient patient)
        {
            int numberOfDonors = 15;
            Console.WriteLine($"Login successful! Welcome {patient.Name}.");
            Console.WriteLine($"Select how many potential donors you want displayed. Max: {numberOfDonors}");
            Console.WriteLine("If the number you type is greater than what we have, only the available will be shown.");
            do
            {
                try
                {

                    Console.Write("Number of donors: ");
                    if (!int.TryParse(Console.ReadLine(), out numberOfDonors)) throw new FormatException("Value must be an integer. Try again.");
                }
                catch (FormatException e)
                {
                    numberOfDonors = 15;
                    Console.WriteLine(e.Message);
                }
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
                try
                {
                    Console.Write("Pick the number of donor you want blood from: ");
                    if (!int.TryParse(Console.ReadLine(), out donorIndex)) throw new FormatException("Value must be an integer. Try again.");
                    if (donorIndex > potentialDonors.Count) throw new Exception("Invalid donor. Try again.");
                }
                catch (Exception e)
                {
                    donorIndex = 0;
                    Console.WriteLine(e.Message);
                }
            } while (donorIndex <= 0);

            Donor donatingDonor = potentialDonors[donorIndex - 1];
            patientController.ReceiveBlood(patient, donatingDonor);
            Console.WriteLine($"You successfully received blood from {donatingDonor.Name}. You can call them to thank.");
            Console.WriteLine($"We hope your {patient.Diagnose} will be cured. Your account will be deleted now.");
            Console.WriteLine("If you want to receive blood again you can register at any time.");
        }
    }
}
