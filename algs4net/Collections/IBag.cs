using System;

namespace algs4net.Collections
{
    public interface IBag<T>
        where T : IComparable<T>
    {
        void Add(T item);
    }
}
