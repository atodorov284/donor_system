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
        public PatientController()
        {
            patientsDAO = new PatientsDAO();
            donorsDAO = new DonorsDAO();
        }

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
            Console.Write("Pick the number of donor you want blood from: ");
            int donorIndex;
            do
            {
                donorIndex = int.Parse(Console.ReadLine());
            } while (donorIndex <= 0 && donorIndex > numberOfDonors);
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
