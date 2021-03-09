using System;
using System.Collections.Generic;
using System.Text;
using DonorSystem.Models;
using System.Linq;

namespace DonorSystem.DAO
{
    class WebsitesDAO
    {
        DonorDBContext context;
        public WebsitesDAO()
        {
            context = new DonorDBContext();
        }

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
