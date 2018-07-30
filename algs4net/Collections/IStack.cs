using System;

namespace algs4net.Collections
{
    public interface IStack<T>
        where T : IComparable<T>
    {
        T Pop();

        void Push(T item);
    }
}
