using System;
using System.Collections.Concurrent;
using System.Threading;

namespace ProducerConsumerPattern
{
    public class Program
    {
        static ConcurrentQueue<string> queue = new ConcurrentQueue<string>();

        static void Main()
        {
            new Thread(Consumer).Start();
            new Thread(Producer).Start(1);
            new Thread(Producer).Start(2);

            Console.WriteLine($"Press any key to close....");
            Console.ReadKey();
        }

        static void Producer(object dataObj)
        {
            var id = (int)dataObj;
            var currentData = 1;

            while (true)
            {
                queue.Enqueue($"Producer {id}: {currentData++}");

                Thread.Sleep(4000);
            }
        }

        static void Consumer()
        {
            Console.WriteLine($"Start consumer");
            while (true)
            {
                if (queue.Count > 0)
                {
                    while (queue.TryDequeue(out string response))
                    {
                        Console.WriteLine($"From consumer -> {response}");
                    }
                }
                else
                {
                    Console.WriteLine("Consumer does not find data");
                }

                Thread.Sleep(1000);
            }
        }
    }

}