using System.Threading.Tasks;

namespace SimpleAsyncAwaitProject
{
    static class Program
    {
        private static async Task Main()
        {
            new SyncCoffeeMachine().MakeCoffee();
            await new AsyncCoffeeMachine().MakeCoffee();
            await DoSomething.LaunchTwoDifferedMethodAsync();
            await DoSomething.LaunchTwoDifferedMethodAndFailedAsync();
            await DoSomething.LaunchTwoDifferedMethodAndFailedWithWhenAllAsync();
            DoSomething.LaunchTwoDifferedMethodAndFailedWithWaitAllAsync();
            await new SharedResource().PlayScenario();
        }
    }
}