using System;
using System.Collections.Generic;

namespace DonorSystem.Models
{
    /// <summary>
    /// Patient data
    /// </summary>
    public partial class Patient
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Diagnose { get; set; }
        public string BloodGroup { get; set; }

        /// <summary>
        /// Compares this with another object.
        /// </summary>
        /// <remarks>
        /// Needed in unit testing.
        /// </remarks>
        /// <param name="obj">Comparate</param>
        /// <returns>if this and obj are indentical by their properties.</returns>
        public override bool Equals(object obj)
        {
            Patient other = obj as Patient;
            if (other == null)
            {
                return false;
            }
            return (this.PatientId == other.PatientId) &&
                    (this.Name == other.Name) &&
                    (this.Email == other.Email) &&
                    (this.Password == other.Password) &&
                    (this.PhoneNumber == other.PhoneNumber) &&
                    (this.Diagnose == other.Diagnose) &&
                    (this.BloodGroup == other.BloodGroup);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
