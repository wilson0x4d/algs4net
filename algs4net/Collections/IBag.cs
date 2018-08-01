using System;

namespace algs4net.Collections
{
    /// <summary>
    /// Represents a bag of <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBag<T>
        where T : IComparable<T>
    {
        void Add(T item);
    }

    /// <summary>
    /// Represents a bag of <typeparamref name="TKey"/> and <typeparamref name="TValue"/> pairs.
    /// name="TValue"/> pairs.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface IBag<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        void Add(TKey key, TValue value);
    }
}
