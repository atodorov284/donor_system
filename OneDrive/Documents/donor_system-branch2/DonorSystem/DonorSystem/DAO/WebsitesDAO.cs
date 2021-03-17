using System;
using System.Collections.Generic;
using System.Text;
using DonorSystem.Models;
using System.Linq;

namespace DonorSystem.DAO
{
    public class WebsitesDAO
    {
        private DonorDBContext context;

        /// <summary>Initializes a new instance of the <see cref="T:DonorSystem.DAO.WebsitesDAO" /> class which controls the Websites table in the database.</summary>
        public WebsitesDAO()
        {
            context = new DonorDBContext();
        }


        /// <summary>Gets all websites from the Website table.</summary>
        /// <returns>The website table with every record as a new line.</returns>
        public override string ToString()
        {
            string output = "";
            List<Website> websites = context.Websites.ToList();
            for (int i = 0; i < websites.Count; i++)
            {
                output = $"{output}{Environment.NewLine}{i + 1}. {websites[i].Name} {websites[i].Description}";
            }
            return output;
        }
    }
}
