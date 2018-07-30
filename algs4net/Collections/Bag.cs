using System;
using System.Collections.Generic;

namespace algs4net.Collections
{
    public class Bag<T> :
        CollectionBase<T>,
        IBag<T>
        where T : IComparable<T>
    {
        protected readonly LinkedList<T> _set = new LinkedList<T>();

        public override int Count => _set.Count;

        public virtual void Add(T item)
        {
            _set.Add(item);
        }

        public override IEnumerator<T> GetEnumerator() => _set.GetEnumerator();
    }
}
