using System;
using System.Threading;

namespace HWRaceConditionAndAtomicity
{
    public class RaceCondition
    {
        public string GetWork { get; private set; }

        public void Work1() { GetWork = "First work"; }
        public void Work2() { GetWork = "Second work"; }
        public void Work3() { GetWork = "Third work"; }

        public void Explanation()
        {
            Console.WriteLine("Class 'RaceCondition' contains 1 variable - 'work' and 3 methods - 'Work1', \n" +
                "'Work2' and 'Work3', which set value to 'work'.\nCreated 3 threads for execution 3 work methods.\n" +
                $"In this case Race Condition is explained that every method will rewrite 'work' variable via threads\n" +
                $"and we can't predict which method will be last.\n");
        }
    }

    public static class RaceConditionExtension
    {
        public static RaceCondition RetryCount(this RaceCondition raceCondition, int retryCount, Action action)
        {
            for (int i = 0; i < retryCount; i++)
            {
                action.Invoke();
            }
            return raceCondition;
        }

        public static RaceCondition StartRaceCondition(this RaceCondition raceCondition)
        {
            Thread work1 = new Thread(() => raceCondition.Work1());
            Thread work2 = new Thread(() => raceCondition.Work2());
            Thread work3 = new Thread(() => raceCondition.Work3());

            work1.Start();
            work2.Start();
            work3.Start();

            work1.Join();
            work2.Join();
            work3.Join();

            return raceCondition;
        }

        public static void ShowResult(this RaceCondition raceCondition)
        {
            Console.WriteLine($"Race condition: Final work = {raceCondition.GetWork}");
        }
    }
}
