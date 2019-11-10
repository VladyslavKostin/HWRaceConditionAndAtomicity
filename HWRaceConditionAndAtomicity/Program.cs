using System;

namespace HWRaceConditionAndAtomicity
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Race Condition\n");
            RaceCondition raceCondition = new RaceCondition();
            raceCondition.Explanation();

            int retryCount = 3; // retry count for execution RaceCondition 
            raceCondition.RetryCount(retryCount, () =>
            {
                raceCondition.StartRaceCondition()
                             .ShowResult();
            });

            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Atomicity\n");
            Atomicity atomicity = new Atomicity();
            atomicity.StartAtomicity()
                     .Reset()
                     .StartAtomicityWithLockInstance();
        }
    }
}
