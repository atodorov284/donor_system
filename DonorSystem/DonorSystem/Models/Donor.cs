using System;

namespace DonorSystem.Models
{
    /// <summary>
    /// Donor data
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

        /// <summary>Compares if one donor is equal to the other.</summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        ///   <c>true</c> if the specified donor is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            Donor other = obj as Donor;
            if (other == null)
            {
                return false;
            }
            return (this.Name == other.Name) &&
                   (this.Email == other.Email) &&
                   (this.Password == other.Password) &&
                   (this.PhoneNumber == other.PhoneNumber) &&
                   (this.Status == other.Status) &&
                   (this.BloodGroup == other.BloodGroup);
        }

        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
