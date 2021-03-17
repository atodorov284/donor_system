using System;

namespace DonorSystem.Models
{
    /// <summary>
    /// Website data
    /// </summary>
    public partial class Website
    {
        public int WebsiteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
