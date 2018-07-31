using System;
using System.Collections;
using System.Collections.Generic;

namespace algs4net.Collections
{
    public abstract class CollectionBase<T> :
        IEnumerable<T>
        where T : IComparable<T>
    {
        public abstract int Count { get; }

        public abstract IEnumerator<T> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override string ToString()
        {
            return $"count:{Count}";
        }
    }
}
