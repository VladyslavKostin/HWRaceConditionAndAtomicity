using System;
using System.Threading;

namespace HWRaceConditionAndAtomicity
{
    public struct BigBanner
    {
        public string slot1;
        public string slot2;
        public string slot3;
        public BigBanner(string a, string b, string c)
        {
            Thread.Sleep(300);
            slot1 = a;
            Thread.Sleep(300);
            slot2 = b;
            slot3 = c;
        }
    }

    public class Atomicity
    {
        public BigBanner GetBanner { get; set; }

        public Atomicity()
        {
            GetBanner = new BigBanner("Sell", "The", "Cat");
        }
    }

    public static class AtomicityExtension
    {
        public static Atomicity StartAtomicity(this Atomicity atomicity)
        {
            Console.WriteLine("'StartAtomicity' method started\n");
            var bob = new Thread(() =>
            {
                atomicity.GetBanner = new BigBanner("Wash", "The", "Dog");
            });

            var alice = new Thread(() =>
            {
                for (int i = 0; i < 5; ++i)
                {
                    string a, b, c;
                    a = atomicity.GetBanner.slot1;
                    Thread.Sleep(200);
                    b = atomicity.GetBanner.slot2;
                    Thread.Sleep(200);
                    c = atomicity.GetBanner.slot3;
                    Thread.Sleep(200);

                    Console.WriteLine($"Banner in atomic state: {a} {b} {c}");
                }
            });

            alice.Start();
            Thread.Sleep(300);
            bob.Start();
            bob.Join();
            alice.Join();

            Console.WriteLine("\n'StartAtomicity' method finished\n\n");
            return atomicity;
        }

        public static Atomicity Reset(this Atomicity atomicity)
        {
            atomicity = new Atomicity();
            return atomicity;
        }

        public static Atomicity StartAtomicityWithLockInstance(this Atomicity atomicity)
        {
            Console.WriteLine("'StartAtomicityWithLockInstance' method started\n");
            object lockInstance = new object();

            var bob = new Thread(() =>
            {
                lock (lockInstance)
                {
                    atomicity.GetBanner = new BigBanner("Wash", "The", "Dog");
                }
            });

            var alice = new Thread(() =>
            {
                for (int i = 0; i < 5; ++i)
                {
                    string a, b, c;
                    lock (lockInstance)
                    {
                        a = atomicity.GetBanner.slot1;
                        Thread.Sleep(200);
                        b = atomicity.GetBanner.slot2;
                        Thread.Sleep(200);
                        c = atomicity.GetBanner.slot3;
                        Thread.Sleep(200);
                    }
                    Console.WriteLine($"Banner in atomic state: {a} {b} {c}");
                }
            });

            alice.Start();
            Thread.Sleep(300);
            bob.Start();
            bob.Join();
            alice.Join();

            Console.WriteLine("\n'StartAtomicityWithLockInstance' method finished\n");
            return atomicity;
        }
    }
}
