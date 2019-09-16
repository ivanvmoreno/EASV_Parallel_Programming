using System.Collections.Generic;
using System.Threading;
namespace Parallel_Week36
{
    public class ThreadSafeQueue<T>
    {
        private List<T> list;
        private SemaphoreSlim addSemaphore;
        private SemaphoreSlim removeSemaphore;

        public ThreadSafeQueue(int size)
        {

            list = new List<T>();
            addSemaphore = new SemaphoreSlim(size);
            removeSemaphore = new SemaphoreSlim(0);
        }

        public void Add(T element)
        {
            addSemaphore.Wait();
            list.Add(element);
            removeSemaphore.Release();
        }

        public T Remove()
        {
            removeSemaphore.Wait();
            T element = list[0];
            list.RemoveAt(0);
            addSemaphore.Release();
            return element;
        }
    }
}
