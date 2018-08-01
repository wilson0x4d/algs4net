using System;
using System.Collections;
using System.Collections.Generic;

namespace algs4net.Collections
{
    /// <summary>
    /// An 'elementary' implementation of <see cref="IIndex{TKey, TValue}"/>
    /// which uses a Linked List internally, to be used as a basis for
    /// verifying more advanced implementations.
    /// </summary>
    public sealed class SequentialIndex<TKey, TValue> :
        IIndex<TKey, TValue>
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        private readonly LinkedList<IndexEntry> _entries = new LinkedList<IndexEntry>();

        public TValue this[TKey key]
        {
            get
            {
                if (TryGet(key, out TValue value))
                {
                    return value;
                }
                throw new Exception($"Key not found.");
            }
            set
            {
                var entry = new IndexEntry(key, value);
                _entries.Remove(entry);
                _entries.InsertBefore(entry, entry);
            }
        }

        public IEnumerator<TKey> GetEnumerator()
        {
            foreach (var entry in _entries)
            {
                yield return entry.Key;
            }
        }

        public bool Remove(TKey key)
        {
            return TryRemove(key, out TValue value);
        }

        public bool TryGet(TKey key, out TValue value)
        {
            foreach (var entry in _entries)
            {
                if ((entry.Key as IEquatable<TKey>).Equals(key))
                {
                    value = entry.Value;
                    return true;
                }
            }
            value = default;
            return false;
        }

        public bool TryRemove(TKey key, out TValue value)
        {
            return TryGet(key, out value)
                && _entries.Remove(new IndexEntry(key, value));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class IndexEntry :
            IComparable<IndexEntry>,
            IEquatable<IndexEntry>
        {
            public readonly TKey Key;

            public TValue Value;

            public IndexEntry(TKey key, TValue value = default)
            {
                Key = key;
                Value = value;
            }

            public int CompareTo(IndexEntry other)
            {
                return Key.CompareTo(other.Key);
            }

            public bool Equals(IndexEntry other)
            {
                return ReferenceEquals(this, other)
                    || Key.Equals(other.Key);
            }

            public override string ToString()
            {
                return $"{Key}:{Value}";
            }
        }
    }
}
