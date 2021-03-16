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

        /// <summary>
        /// Сравнява дарителя с друг обект.
        /// </summary>
        /// <remarks>
        /// Този метод е необходим при unit testing.
        /// </remarks>
        /// <param name="obj">Обект, с когото се сравнява.</param>
        /// <returns>true при съвпадане на типа и данните на obj с обекта на дарителя; false при несъвпадение.</returns>
        public override bool Equals(object obj)
        {
            Donor other = obj as Donor;
            if (other == null)
            {
                return false;
            }
            return  (this.DonorId == other.DonorId) &&
                    (this.Name == other.Name) &&
                    (this.Email == other.Email) &&
                    (this.Password == other.Password) &&
                    (this.PhoneNumber == other.PhoneNumber) &&
                    (this.Status == other.Status) &&
                    (this.BloodGroup == other.BloodGroup);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
