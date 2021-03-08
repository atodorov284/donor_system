using System;
using System.Collections.Generic;
using System.Text;
using DonorSystem.Models;
using DonorSystem.DAO;
using System.Linq;
using DonorSystem.Controllers;

namespace DonorSystem.Views
{
    class PatientMenu
    {
        PatientController patientController;
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
                Console.Write("Number of donors: ");
                donors = int.Parse(Console.ReadLine());
            } while (donors >= 15);
            patientController.ShowPotentialDonors(patient, donors);
            Console.ReadKey();
        }

    }
}
