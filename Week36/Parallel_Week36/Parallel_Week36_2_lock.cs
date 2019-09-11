using System;
using System.Threading;

namespace Parallel_Week36
{
    public class Exercise_2_lock
    {
        public int sharedCounter;
        private Object _lockCounter = new Object();

        public static void _Main()
        {
            Exercise_2_lock program = new Exercise_2_lock();

            Thread thread_1 = new Thread(new ParameterizedThreadStart(program.IncrementCounter));
            Thread thread_2 = new Thread(new ParameterizedThreadStart(program.IncrementCounter));

            thread_1.Start();
            thread_2.Start();
            Console.WriteLine("Starting threads...");

            thread_1.Join();
            thread_2.Join();

            Console.WriteLine(program.sharedCounter);
        }

        private void IncrementCounter(object data)
        {
            lock (_lockCounter)
            {
                for (int i = 0; i < 100000; i++)
                {
                    sharedCounter++;
                }
            }
        }
    }
}
