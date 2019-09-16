using System;
using System.Threading;

namespace Parallel_Week36
{
    public class Exercise_5
    {
        private const int PROD_DELAY = 500;
        private const int CONS_DELAY = 2000;
        private const int ARRAY_SIZE = 10;
        private const int EXIT = -1;
        private ThreadSafeQueue<int> sharedArray = new ThreadSafeQueue<int>(ARRAY_SIZE);

        public static void Main()
        {
            Exercise_5 program = new Exercise_5();

            Thread producer = new Thread(() => program.Produce(0, 25));
            Thread consumer_1 = new Thread(() => program.Consume());
            consumer_1.Name = "1";
            Thread consumer_2 = new Thread(() => program.Consume());
            consumer_2.Name = "2";

            producer.Start();
            consumer_1.Start();
            consumer_2.Start();

            producer.Join();
            consumer_1.Join();
            consumer_2.Join();
        }

        private void Produce(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                sharedArray.Add(i);
                Console.WriteLine("Added {0}", i);
                Thread.Sleep(PROD_DELAY);
            }

            sharedArray.Add(EXIT);
            Console.WriteLine("Producer finished");
        }

        private void Consume()
        {
            bool done = false;
            while (!done)
            {
                int element = sharedArray.Remove();
                if (element == EXIT)
                {
                    done = true;
                    sharedArray.Add(element);
                    Console.WriteLine("Consumer {0} finished", Thread.CurrentThread.Name);
                }
                else
                {
                    Console.WriteLine("Consumed {0}", element);
                    Thread.Sleep(CONS_DELAY);
                }
            }
        }
    }
}