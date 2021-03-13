using System;
using System.Collections.Generic;

namespace DonorSystem.Models
{
    /// <summary>
    /// Данни на пациент
    /// </summary>
    public partial class Patient
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Diagnose { get; set; }
        public string BloodGroup { get; set; }
        public string PhoneNumber { get; set; }
    }
}
