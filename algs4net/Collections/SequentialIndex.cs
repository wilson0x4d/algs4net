using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
                if (TryGetValue(key, out TValue value))
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

        public int Count => _entries.Count;

        public TKey Ceil(TKey key)
        {
            return _entries.FirstOrDefault(e => e.Key.CompareTo(key) >= 0).Key;
        }

        public TKey Floor(TKey key)
        {
            return _entries.LastOrDefault(e => e.Key.CompareTo(key) <= 0).Key;
        }

        public IEnumerator<TKey> GetEnumerator()
        {
            foreach (var entry in _entries)
            {
                yield return entry.Key;
            }
        }

        public int IndexOf(TKey key)
        {
            var i = -1;
            foreach (var entry in _entries)
            {
                i++;
                if (entry.Key.CompareTo(key) == 0)
                {
                    return i;
                }
            }
            return i;
        }

        public TKey Max()
        {
            return _entries.Max().Key;
        }

        public TKey Min()
        {
            return _entries.Min().Key;
        }

        public bool Remove(TKey key)
        {
            return TryRemove(key, out TValue value);
        }

        public bool TryGetValue(TKey key, out TValue value)
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

        public bool TryGetByIndex(int index, out TKey key)
        {
            var entry = _entries.ElementAtOrDefault(index);
            if (entry != default)
            {
                key = entry.Key;
                return true;
            }
            else
            {
                key = default;
                return false;
            }
        }

        public bool TryGetRange(TKey from, TKey to, out IEnumerable<TKey> keys)
        {
            keys = _entries
                .Where(entry =>
                    entry.Key.CompareTo(from) >= 0
                    && entry.Key.CompareTo(to) <= 0)
                .Select(e => e.Key)
                .ToArray();
            return keys.Count() > 0;
        }

        public bool TryRemove(TKey key, out TValue value)
        {
            return TryGetValue(key, out value)
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
