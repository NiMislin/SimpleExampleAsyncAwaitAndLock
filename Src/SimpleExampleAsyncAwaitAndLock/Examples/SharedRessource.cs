using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleExampleAsyncAwaitAndLock.Examples
{
    public class SharedResource
    {
        private int _sharedResource = 0;
        private readonly object _resourceLock = new();

        public async Task PlayScenario()
        {
            Console.Write(Environment.NewLine);
            Console.WriteLine("Play 10 times without lock on shared resource");
            var resultWithoutLock = new List<int>();
            for (var i = 0; i < 10; i++)
            {
                resultWithoutLock.Add(await Play(UpdateSharedResource));
            }

            if (resultWithoutLock.Any(r => r != 1600))
            {
                Console.WriteLine($"Results of 10 executions : {string.Join(", ", resultWithoutLock)}");
                Console.WriteLine("Without lock, the results aren't equal to 1600");
            }

            Console.Write(Environment.NewLine);
            Console.WriteLine("Play 10 times with lock on shared resource");
            var resultWithLock = new List<int>();
            for (var i = 0; i < 10; i++)
            {
                resultWithLock.Add(await Play(UpdateSharedResourceWithLock));
            }
            
            if (resultWithLock.TrueForAll(r => r == 1600))
            {
                Console.WriteLine($"Results of 10 executions : {string.Join(", ", resultWithLock)}");
                Console.WriteLine("With lock, all results are equal to 1600");
            }
        }
        
        private async Task<int> Play(Action<int> update)
        {
            _sharedResource = 0;
            var task = new List<Task>();
            for (var i = 0; i < 100; i++)
            {
                task.Add(Task.Run(() => AddASeriesOfNumbersOnSharedResource(update)));
            }
            
            await Task.WhenAll(task);
            return _sharedResource;
        }
        
        private static void AddASeriesOfNumbersOnSharedResource(Action<int> update)
        {
            var acts = new [] { 10, -5, 3, 18, 2, -15, 3 } ;
            foreach (var act in acts)
            {
                update(act);
            }
        }

        private void UpdateSharedResource(int act)
        {
            _sharedResource += act;
        }
        
        private void UpdateSharedResourceWithLock(int act)
        {
            lock(_resourceLock)
            {
                _sharedResource += act;
            }
        }
    }
}