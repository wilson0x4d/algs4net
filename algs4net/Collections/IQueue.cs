using System;

namespace algs4net.Collections
{
    public interface IQueue<T>
        where T : IComparable<T>
    {
        T Dequeue();

        void Enqueue(T item);
    }
}
