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

        public WebsitesDAO()
        {
            context = new DonorDBContext();
        }

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
