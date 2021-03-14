using System;
using DonorSystem.Models;
using DonorSystem.Controllers;

namespace DonorSystem.Views
{
    class PatientMenu
    {
        readonly PatientController patientController;
        /// <summary>
        ///   <para>
        /// Initializes a new instance of the <see cref="T:DonorSystem.Views.PatientMenu" /> class and displays the menu for the patient role.</para>
        /// </summary>
        /// <param name="patient">The patient.</param>
        public PatientMenu(Patients patient)
        {
            patientController = new PatientController();
            ShowPatientMenu(patient);
        }

        /// <summary>Shows the patient menu and gets input from the user to receive blood.</summary>
        /// <param name="patient">The patient.</param>
        /// <exception cref="FormatException">Value must be an integer. Try again.</exception>
        private void ShowPatientMenu(Patients patient)
        {
            int donors = 15;
            Console.WriteLine($"Login successful! Welcome {patient.Name}.");
            Console.WriteLine($"Select how many potential donors you want displayed. Max: {donors}");
            Console.WriteLine("If the number you type is greater than what we have, only the available will be shown.");
            do
            {
                try
                {
                    
                    Console.Write("Number of donors: ");
                    if (!int.TryParse(Console.ReadLine(), out donors)) throw new FormatException("Value must be an integer. Try again.");
                }
                catch (FormatException e)
                {
                    donors = 15;
                    Console.WriteLine(e.Message);
                }
            } while (donors >= 15);
            
            patientController.ReceiveBlood(patient, donors);
            Console.ReadKey();
        }

    }
}
