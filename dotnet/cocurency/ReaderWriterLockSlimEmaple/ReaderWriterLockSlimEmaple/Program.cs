using System;
using System.Collections.Generic;
using System.Threading;

namespace ReaderWriterLockSlimEmaple
{
    public class Program
    {
        private static ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
        private static List<string> innerCache = new List<string>();

        static void Main()
        {
            new Thread(Consumer).Start();
            new Thread(Producer).Start(1);

            Console.WriteLine($"Press any key to close....");
            Console.ReadKey();
        }

        static void Producer(object dataObj)
        {
            var id = (int)dataObj;
            var currentData = 1;


            while (true)
            {
                //Console.WriteLine($"TryEnterWriteLock {id}");
                if (cacheLock.TryEnterWriteLock(1000))
                {
                    Console.WriteLine($"EnterWriteLock {id}");

                    innerCache.Add($"Producer {id}: {currentData++}");

                    Thread.Sleep(5000);

                    cacheLock.ExitWriteLock();
                    Console.WriteLine($"ExitWriteLock  {id}");
                }

                Thread.Sleep(4000);
            }
        }

        static void Consumer()
        {
            Console.WriteLine($"Start consumer");
            while (true)
            {
                if (cacheLock.TryEnterReadLock(20000))
                {
                    Console.WriteLine($"EnterReadLock");

                    if (innerCache.Count > 0)
                    {
                        foreach (var response in innerCache)
                        {
                            Console.WriteLine($"From consumer -> {response}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Consumer does not find data");
                    }

                    cacheLock.ExitReadLock();
                    Console.WriteLine($"ExitReadLock");
                }

                Thread.Sleep(1000);
            }
        }
    }
}
