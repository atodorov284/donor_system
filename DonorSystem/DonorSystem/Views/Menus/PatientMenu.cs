using System;
using DonorSystem.Models;
using DonorSystem.Controllers;

namespace DonorSystem.Views
{
    class PatientMenu
    {
        readonly PatientController patientController;
        public PatientMenu(Patients patient)
        {
            patientController = new PatientController();
            ShowPatientMenu(patient);
        }

        public void ShowPatientMenu(Patients patient)
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
