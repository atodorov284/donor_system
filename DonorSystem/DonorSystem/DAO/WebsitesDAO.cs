using System;
using System.Collections.Generic;
using DonorSystem.Models;
using System.Linq;

namespace DonorSystem.DAO
{
    class WebsitesDAO
    {
        readonly DonorDBContext context;

        /// <summary>Initializes a new instance of the <see cref="T:DonorSystem.DAO.WebsitesDAO" /> class which controls the Websites table in the database.</summary>
        public WebsitesDAO()
        {
            context = new DonorDBContext();
        }

        /// <summary>Shows all info from the Websites table.</summary>
        public void ShowAll()
        {
            List<Websites> websites = context.Websites.ToList();
            for (int i = 0; i < websites.Count; i++)
            {
                Console.WriteLine($"{i+1}. {websites[i].Name} {websites[i].Description}");
            }
        }
    }
}
