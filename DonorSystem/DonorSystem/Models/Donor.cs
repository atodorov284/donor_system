using System;
using System.Collections.Generic;

namespace DonorSystem.Models
{
    /// <summary>
    /// Данни на дарител
    /// </summary>
    public partial class Donor
    {
        public int DonorId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
        public string BloodGroup { get; set; }
    }
}
