using System;

namespace algs4net.Collections
{
    public interface IBinarySearchTree<TKey, TValue>
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        TValue this[TKey key] { get; set; }

        bool TryGetValue(TKey key, out TValue value);

        bool TryRemove(TKey key, out TValue value);
    }
}