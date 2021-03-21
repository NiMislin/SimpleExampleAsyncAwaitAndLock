# Simple example of using Async/Await and Lock in .net

## SyncCoffeeMachine().MakeCoffee()
Make a coffee and wait for each step

```
21/03/2021 12:14:38 - Void MakeCoffee() - Start the coffee sync
21/03/2021 12:14:38 - Void PrepareCup() - Start preparing a cup
21/03/2021 12:14:39 - Void PrepareCup() - Finish preparing a cup
21/03/2021 12:14:39 - Void AddCoffee() - Start adding coffee
21/03/2021 12:14:40 - Void AddCoffee() - Finish adding coffee
21/03/2021 12:14:40 - Void BoilWater() - Start boiling water
21/03/2021 12:14:41 - Void BoilWater() - 25°C
21/03/2021 12:14:42 - Void BoilWater() - 50°C
21/03/2021 12:14:43 - Void BoilWater() - 75°C
21/03/2021 12:14:44 - Void BoilWater() - 100°C
21/03/2021 12:14:44 - Void BoilWater() - Finish boiling water
21/03/2021 12:14:44 - Void PourWaterIntoCup() - Start pouring the water into the cup
21/03/2021 12:14:45 - Void PourWaterIntoCup() - Finish pouring the water into the cup
21/03/2021 12:14:45 - Void MakeCoffee() - Finish the coffee sync
```

## AsyncCoffeeMachine().MakeCoffee()
Prepare a coffee where I put the water to heat first and I prepare the cup in parallel.  
I wait until the water is hot so I can pour it into the cup.
```
21/03/2021 12:14:45 - Void MoveNext() - Start the coffee async
21/03/2021 12:14:45 - Void MoveNext() - Start boiling water
21/03/2021 12:14:45 - Void MoveNext() - Start preparing a cup
21/03/2021 12:14:46 - Void MoveNext() - Finish preparing a cup
21/03/2021 12:14:46 - Void MoveNext() - 25°C
21/03/2021 12:14:46 - Void MoveNext() - Start adding coffee
21/03/2021 12:14:47 - Void MoveNext() - 50°C
21/03/2021 12:14:47 - Void MoveNext() - Finish adding coffee
21/03/2021 12:14:47 - Void MoveNext() - Waiting the boiled water finish
21/03/2021 12:14:48 - Void MoveNext() - 75°C
21/03/2021 12:14:49 - Void MoveNext() - 100°C
21/03/2021 12:14:49 - Void MoveNext() - Finish boiling water
21/03/2021 12:14:49 - Void MoveNext() - Start pouring the water into the cup
21/03/2021 12:14:50 - Void MoveNext() - Finish pouring the water into the cup
21/03/2021 12:14:50 - Void MoveNext() - Finish the coffee async
```

## LaunchTwoDifferedMethodAsync
Start two asynchronous methods and use Task.WhenAll to wait for them to complete.

I use Task.WhenAll for waiting both.  
Task.WhenAll returns a Task which could be await later in code.  
We can see the status of each task.

```
21/03/2021 12:14:50 - LaunchTwoDifferedMethodAsync
21/03/2021 12:14:50 - DoSomethingAsync#1 - I start something async
21/03/2021 12:14:50 - DoSomethingAsync#2 - I start something async
Task#1 - TaskStatus : WaitingForActivation - IsCompleted : False - IsFaulted : False - IsCanceled : False - IsCompletedSuccessfully : False
Task#2 - TaskStatus : WaitingForActivation - IsCompleted : False - IsFaulted : False - IsCanceled : False - IsCompletedSuccessfully : False
With Task.WhenAll
Task#1 - TaskStatus : WaitingForActivation - IsCompleted : False - IsFaulted : False - IsCanceled : False - IsCompletedSuccessfully : False
Task#2 - TaskStatus : WaitingForActivation - IsCompleted : False - IsFaulted : False - IsCanceled : False - IsCompletedSuccessfully : False
Task#3 - TaskStatus : WaitingForActivation - IsCompleted : False - IsFaulted : False - IsCanceled : False - IsCompletedSuccessfully : False
21/03/2021 12:14:51 - DoSomethingAsync#2 - I continue something async
21/03/2021 12:14:51 - DoSomethingAsync#1 - I continue something async
21/03/2021 12:14:52 - DoSomethingAsync#2 - I finished something async
21/03/2021 12:14:52 - DoSomethingAsync#1 - I finished something async
Task#1 - TaskStatus : RanToCompletion - IsCompleted : True - IsFaulted : False - IsCanceled : False - IsCompletedSuccessfully : True
Task#2 - TaskStatus : RanToCompletion - IsCompleted : True - IsFaulted : False - IsCanceled : False - IsCompletedSuccessfully : True
Task#3 - TaskStatus : RanToCompletion - IsCompleted : True - IsFaulted : False - IsCanceled : False - IsCompletedSuccessfully : True
21/03/2021 12:14:52 - LaunchTwoDifferedMethodAsync
```

## LaunchTwoDifferedMethodAndFailedAsync
Start two asynchronous methods which should throw an exception and wait one after the other.
Only the first exception is caught, but both tasks are Fault. 
```
21/03/2021 12:14:52 - LaunchTwoDifferedMethodAndFailedAsync
21/03/2021 12:14:52 - DoSomethingAsync#1 - I start something async
21/03/2021 12:14:52 - DoSomethingAsync#2 - I start something async
Task#4 - TaskStatus : WaitingForActivation - IsCompleted : False - IsFaulted : False - IsCanceled : False - IsCompletedSuccessfully : False
Task#5 - TaskStatus : WaitingForActivation - IsCompleted : False - IsFaulted : False - IsCanceled : False - IsCompletedSuccessfully : False
With await one after the other
21/03/2021 12:14:53 - DoSomethingAsync#2 - I continue something async
21/03/2021 12:14:53 - DoSomethingAsync#1 - I continue something async
21/03/2021 12:14:54 - DoSomethingAsync#1 - Oops I failed
Task#4 - TaskStatus : Faulted - IsCompleted : True - IsFaulted : True - IsCanceled : False - IsCompletedSuccessfully : False
Task#5 - TaskStatus : Faulted - IsCompleted : True - IsFaulted : True - IsCanceled : False - IsCompletedSuccessfully : False
21/03/2021 12:14:54 - LaunchTwoDifferedMethodAndFailedAsync
```

## LaunchTwoDifferedMethodAndFailedWithWhenAllAsync
Start two asynchronous methods which should throw an exception and wait both with Task.WhenAll which throw only the first exception.
```
21/03/2021 12:14:54 - LaunchTwoDifferedMethodAndFailedWithWhenAllAsync
21/03/2021 12:14:54 - DoSomethingAsync#1 - I start something async
21/03/2021 12:14:54 - DoSomethingAsync#2 - I start something async
Task#6 - TaskStatus : WaitingForActivation - IsCompleted : False - IsFaulted : False - IsCanceled : False - IsCompletedSuccessfully : False
Task#7 - TaskStatus : WaitingForActivation - IsCompleted : False - IsFaulted : False - IsCanceled : False - IsCompletedSuccessfully : False
With Task.WhenAll
21/03/2021 12:14:55 - DoSomethingAsync#2 - I continue something async
21/03/2021 12:14:55 - DoSomethingAsync#1 - I continue something async
21/03/2021 12:14:56 - DoSomethingAsync#1 - Oops I failed
Task#6 - TaskStatus : Faulted - IsCompleted : True - IsFaulted : True - IsCanceled : False - IsCompletedSuccessfully : False
Task#7 - TaskStatus : Faulted - IsCompleted : True - IsFaulted : True - IsCanceled : False - IsCompletedSuccessfully : False
21/03/2021 12:14:56 - LaunchTwoDifferedMethodAndFailedWithWhenAllAsync
```

## LaunchTwoDifferedMethodAndFailedWithWaitAllAsync
Start two asynchronous methods which should throw an exception and wait both with Task.WhenAll which throw a AggregateException with exceptions from both task.
```
21/03/2021 12:14:56 - LaunchTwoDifferedMethodAndFailedWithWaitAllAsync
21/03/2021 12:14:56 - DoSomethingAsync#1 - I start something async
21/03/2021 12:14:56 - DoSomethingAsync#2 - I start something async
Task#8 - TaskStatus : WaitingForActivation - IsCompleted : False - IsFaulted : False - IsCanceled : False - IsCompletedSuccessfully : False
Task#9 - TaskStatus : WaitingForActivation - IsCompleted : False - IsFaulted : False - IsCanceled : False - IsCompletedSuccessfully : False
With Task.WaitAll
21/03/2021 12:14:57 - DoSomethingAsync#2 - I continue something async
21/03/2021 12:14:57 - DoSomethingAsync#1 - I continue something async
One or more errors occurred. (21/03/2021 12:14:58 - DoSomethingAsync#1 - Oops I failed) (21/03/2021 12:14:58 - DoSomethingAsync#2 - Oops I failed)
Task#8 - TaskStatus : Faulted - IsCompleted : True - IsFaulted : True - IsCanceled : False - IsCompletedSuccessfully : False
Task#9 - TaskStatus : Faulted - IsCompleted : True - IsFaulted : True - IsCanceled : False - IsCompletedSuccessfully : False
21/03/2021 12:14:58 - LaunchTwoDifferedMethodAndFailedWithWaitAllAsync
```

## SharedResource().PlayScenario()
Compare use of a shared resource without and with a lock to update it.
```
Play 10 times without lock on shared resource
Results of 10 executions : 1537, 1426, 1492, 1501, 1472, 1546, 1476, 1535, 1414, 1507
Without lock, the results aren't equal to 1600

Play 10 times with lock on shared resource
Results of 10 executions : 1600, 1600, 1600, 1600, 1600, 1600, 1600, 1600, 1600, 1600
With lock, all results are equal to 1600
```