using System;
using System.Reflection;
using System.Threading;

namespace SimpleAsyncAwaitProject
{
    public class SyncCoffeeMachine
    {
        public void MakeCoffee()
        {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine($"{DateTime.Now} - {MethodBase.GetCurrentMethod()} - Start the coffee sync");
            PrepareCup();
            AddCoffee();
            BoilWater();
            PourWaterIntoCup();
            Console.WriteLine($"{DateTime.Now} - {MethodBase.GetCurrentMethod()} - Finish the coffee sync");
        }
        
        private void BoilWater()
        {
            Console.WriteLine($"{DateTime.Now} - {MethodBase.GetCurrentMethod()} - Start boiling water");
            for (var i = 1; i <= 4; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"{DateTime.Now} - {MethodBase.GetCurrentMethod()} - {i * 25}°C");
            }
            Console.WriteLine($"{DateTime.Now} - {MethodBase.GetCurrentMethod()} - Finish boiling water");
        }

        private void PrepareCup()
        {
            Console.WriteLine($"{DateTime.Now} - {MethodBase.GetCurrentMethod()} - Start preparing a cup");
            Thread.Sleep(1000);
            Console.WriteLine($"{DateTime.Now} - {MethodBase.GetCurrentMethod()} - Finish preparing a cup");
        }

        private void AddCoffee()
        {
            Console.WriteLine($"{DateTime.Now} - {MethodBase.GetCurrentMethod()} - Start adding coffee");
            Thread.Sleep(1000);
            Console.WriteLine($"{DateTime.Now} - {MethodBase.GetCurrentMethod()} - Finish adding coffee");
        }

        private void PourWaterIntoCup()
        {
            Console.WriteLine($"{DateTime.Now} - {MethodBase.GetCurrentMethod()} - Start pouring the water into the cup");
            Thread.Sleep(1000);
            Console.WriteLine($"{DateTime.Now} - {MethodBase.GetCurrentMethod()} - Finish pouring the water into the cup");
        }
    }
}