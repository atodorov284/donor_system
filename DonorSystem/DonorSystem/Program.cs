using System;
using DonorSystem.Views;

namespace DonorSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            //Създава начално меню с конзолен интерфейс, откъдето потребителите могат да навигират
            //към менюто с пациенти или менюто с дарители.
            HomeMenu homeMenu = new HomeMenu();
        }
    }
}
