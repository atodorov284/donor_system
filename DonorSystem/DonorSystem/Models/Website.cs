﻿using System;
using System.Collections.Generic;

namespace DonorSystem.Models
{
    /// <summary>
    /// Данни за уебсайт
    /// </summary>
    public partial class Website
    {
        public int WebsiteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}