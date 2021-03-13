using System;

namespace DonorSystem.Models
{
    public partial class Donors
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
