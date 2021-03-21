using System;
using System.Reflection;
using System.Threading.Tasks;

namespace SimpleExampleAsyncAwaitAndLock.Examples
{
    public class AsyncCoffeeMachine
    {
        public async Task MakeCoffee()
        {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine($"{DateTime.Now} - {MethodBase.GetCurrentMethod()} - Start the coffee async");
            var boilWaterTask = BoilWater();
            await PrepareCup();
            await AddCoffee();
            Console.WriteLine($"{DateTime.Now} - {MethodBase.GetCurrentMethod()} - Waiting the boiled water finish");
            await boilWaterTask;
            await PourWaterIntoCup();
            Console.WriteLine($"{DateTime.Now} - {MethodBase.GetCurrentMethod()} - Finish the coffee async");
        }
        
        private async Task BoilWater()
        {
            Console.WriteLine($"{DateTime.Now} - {MethodBase.GetCurrentMethod()} - Start boiling water");
            for (var i = 1; i <= 4; i++)
            {
                await Task.Delay(1000);
                Console.WriteLine($"{DateTime.Now} - {MethodBase.GetCurrentMethod()} - {i * 25}°C");
            }
            Console.WriteLine($"{DateTime.Now} - {MethodBase.GetCurrentMethod()} - Finish boiling water");
        }

        private async Task PrepareCup()
        {
            Console.WriteLine($"{DateTime.Now} - {MethodBase.GetCurrentMethod()} - Start preparing a cup");
            await Task.Delay(1000);
            Console.WriteLine($"{DateTime.Now} - {MethodBase.GetCurrentMethod()} - Finish preparing a cup");
        }

        private async Task AddCoffee()
        {
            Console.WriteLine($"{DateTime.Now} - {MethodBase.GetCurrentMethod()} - Start adding coffee");
            await Task.Delay(1000);
            Console.WriteLine($"{DateTime.Now} - {MethodBase.GetCurrentMethod()} - Finish adding coffee");
        }

        private async Task PourWaterIntoCup()
        {
            Console.WriteLine($"{DateTime.Now} - {MethodBase.GetCurrentMethod()} - Start pouring the water into the cup");
            await Task.Delay(1000);
            Console.WriteLine($"{DateTime.Now} - {MethodBase.GetCurrentMethod()} - Finish pouring the water into the cup");
        }
    }
}