using System;
using System.Collections.Generic;
using DonorSystem.Models;
using DonorSystem.DAO;
using System.Linq;

namespace DonorSystem.Controllers
{
    class PatientController
    {
        readonly PatientsDAO patientsDAO;
        readonly DonorsDAO donorsDAO;

        /// <summary>Initializes a new instance of the <see cref="T:DonorSystem.Controllers.PatientController" /> class and encapsulates the logic for the patient role.</summary>
        public PatientController()
        {
            patientsDAO = new PatientsDAO();
            donorsDAO = new DonorsDAO();
        }

        /// <summary>Displays the available donors and patient chooses which donor to get blood from.</summary>
        /// <param name="patient">The patient.</param>
        /// <param name="numberOfDonors">The number of donors.</param>
        /// <exception cref="FormatException">Value must be an integer. Try again.</exception>
        /// <exception cref="Exception">Invalid donor. Try again.</exception>
        public void ReceiveBlood(Patients patient, int numberOfDonors)
        {
            List<Donors> potentialDonors = patientsDAO.FindCompatibleDonors(patient);
            potentialDonors = potentialDonors.OrderBy(d => d.BloodGroup).Take(numberOfDonors).ToList();
            Console.WriteLine($"Here are all the donors compatible with your blood group {patient.BloodGroup} and their phone numbers.");
            for (int i = 0; i < potentialDonors.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {potentialDonors[i].Name} {potentialDonors[i].PhoneNumber}");
            }
            Console.WriteLine();
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

            Donors donatingDonor = potentialDonors[donorIndex - 1];
            donorsDAO.TransfuseBlood(donatingDonor, patient);
            Console.WriteLine($"You successfully received blood from {donatingDonor.Name}. You can call him to thank.");
            Console.WriteLine($"We hope your {patient.Diagnose} will be cured. Your account will be deleted now.");
            Console.WriteLine("If you need to receive blood again you can register at any time.");
            patientsDAO.DeletePatient(patient);
            Console.WriteLine("Press any key to return.");
        }
    }
}
