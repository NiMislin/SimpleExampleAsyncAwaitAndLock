using System;
using System.Threading.Tasks;

namespace SimpleExampleAsyncAwaitAndLock.Examples
{
    public static class DoSomething
    {
        public static async Task LaunchTwoDifferedMethodAsync()
        {
            Console.Write(Environment.NewLine);
            Console.WriteLine($"{DateTime.Now} - {nameof(LaunchTwoDifferedMethodAsync)}");
            var task1 = DoSomethingAsync(1);
            var task2 = DoSomethingAsync(2);
            ShowTaskStatus(task1);
            ShowTaskStatus(task2);
            Console.WriteLine("With Task.WhenAll");
            var task3 = Task.WhenAll(task1, task2);
            ShowTaskStatus(task1);
            ShowTaskStatus(task2);
            ShowTaskStatus(task3);
            await task3;
            ShowTaskStatus(task1);
            ShowTaskStatus(task2);
            ShowTaskStatus(task3);
            Console.WriteLine($"{DateTime.Now} - {nameof(LaunchTwoDifferedMethodAsync)}");
        }

        public static async Task LaunchTwoDifferedMethodAndFailedAsync()
        {
            Console.Write(Environment.NewLine);
            Console.WriteLine($"{DateTime.Now} - {nameof(LaunchTwoDifferedMethodAndFailedAsync)}");
            var task1 = DoSomethingAndFailedAsync(1);
            var task2 = DoSomethingAndFailedAsync(2);
            ShowTaskStatus(task1);
            ShowTaskStatus(task2);

            try
            {
                Console.WriteLine("With await one after the other");
                await task1;
                await task2;
            }
            catch (Exception e)
            {
                PrintException(e);
            }

            ShowTaskStatus(task1);
            ShowTaskStatus(task2);
            Console.WriteLine($"{DateTime.Now} - {nameof(LaunchTwoDifferedMethodAndFailedAsync)}");
        }

        public static async Task LaunchTwoDifferedMethodAndFailedWithWhenAllAsync()
        {
            Console.Write(Environment.NewLine);
            Console.WriteLine($"{DateTime.Now} - {nameof(LaunchTwoDifferedMethodAndFailedWithWhenAllAsync)}");
            var task1 = DoSomethingAndFailedAsync(1);
            var task2 = DoSomethingAndFailedAsync(2);
            ShowTaskStatus(task1);
            ShowTaskStatus(task2);

            try
            {
                Console.WriteLine("With Task.WhenAll");
                await Task.WhenAll(task1, task2);
            }
            catch (Exception e)
            {
                PrintException(e);
            }

            ShowTaskStatus(task1);
            ShowTaskStatus(task2);
            Console.WriteLine($"{DateTime.Now} - {nameof(LaunchTwoDifferedMethodAndFailedWithWhenAllAsync)}");
        }

        public static void LaunchTwoDifferedMethodAndFailedWithWaitAllAsync()
        {
            Console.Write(Environment.NewLine);
            Console.WriteLine($"{DateTime.Now} - {nameof(LaunchTwoDifferedMethodAndFailedWithWaitAllAsync)}");
            var task1 = DoSomethingAndFailedAsync(1);
            var task2 = DoSomethingAndFailedAsync(2);
            ShowTaskStatus(task1);
            ShowTaskStatus(task2);

            try
            {
                Console.WriteLine("With Task.WaitAll");
                Task.WaitAll(task1, task2);
            }
            catch (Exception e)
            {
                PrintException(e);
            }

            ShowTaskStatus(task1);
            ShowTaskStatus(task2);
            Console.WriteLine($"{DateTime.Now} - {nameof(LaunchTwoDifferedMethodAndFailedWithWaitAllAsync)}");
        }

        private static async Task DoSomethingAsync(int number)
        {
            Console.WriteLine($"{DateTime.Now} - {nameof(DoSomethingAsync)}#{number} - I start something async");
            await Task.Delay(1000);
            Console.WriteLine($"{DateTime.Now} - {nameof(DoSomethingAsync)}#{number} - I continue something async");
            await Task.Delay(1000);
            Console.WriteLine($"{DateTime.Now} - {nameof(DoSomethingAsync)}#{number} - I finished something async");
        }

        private static async Task DoSomethingAndFailedAsync(int number)
        {
            Console.WriteLine($"{DateTime.Now} - {nameof(DoSomethingAsync)}#{number} - I start something async");
            await Task.Delay(1000);
            Console.WriteLine($"{DateTime.Now} - {nameof(DoSomethingAsync)}#{number} - I continue something async");
            await Task.Delay(1000);
            throw new Exception($"{DateTime.Now} - {nameof(DoSomethingAsync)}#{number} - Oops I failed");
        }

        private static void ShowTaskStatus(Task task)
        {
            Console.WriteLine($"Task#{task.Id} " +
                              $"- TaskStatus : {task.Status} " +
                              $"- IsCompleted : {task.IsCompleted} " +
                              $"- IsFaulted : {task.IsFaulted} " +
                              $"- IsCanceled : {task.IsCanceled} " +
                              $"- IsCompletedSuccessfully : {task.IsCompletedSuccessfully}");
        }

        private static void PrintException(Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.Message);
            Console.ResetColor();
        }
    }
}