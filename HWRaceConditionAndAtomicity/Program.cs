using System;

namespace HWRaceConditionAndAtomicity
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            RaceCondition raceCondition = new RaceCondition();
            raceCondition.Explanation();

            int retryCount = 20; // retry count for execution RaceCondition 
            raceCondition.RetryCount(retryCount, () =>
            {
                raceCondition.StartRaceCondition()
                             .ShowResult();
            });
        }
    }
}
