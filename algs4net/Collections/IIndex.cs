using System;
using System.Collections.Generic;

namespace algs4net.Collections
{
    /// <summary>
    /// An interface for implementing indexed data (ie. Symnbol
    /// Tables/Dictionaries/etc) where keys yield values.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <remarks>
    /// Expect additional <![CDATA[IIndex<...>]]> interfaces.
    ///
    /// Iterating an Index yields all keys stored to the index, this behavior
    /// differs from typical data structure implementations which also yield
    /// a value component (this is an intentional design decision.)
    /// </remarks>
    public interface IIndex<TKey, TValue> :
        IEnumerable<TKey>
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        TValue this[TKey key] { get; set; }

        bool TryGet(TKey key, out TValue value);

        bool TryRemove(TKey key, out TValue value);
    }
}
