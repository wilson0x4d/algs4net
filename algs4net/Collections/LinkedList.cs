using System;
using System.Collections.Generic;

namespace algs4net.Collections
{
    public class LinkedList<T> :
        CollectionBase<T>,
        IEnumerable<T>,
        IBag<T>
        where T : IComparable<T>
    {
        // NOTE: this comparer exists only for elementary order-aware
        //       operations such as `insertbefore` and `insertafter`,
        //       defaults to `Comparers<T>.DefaultComparer` during
        //       construction if not specified
        protected readonly IComparer<T> _comparer;

        protected int _count = 0;
        protected Node _head;
        protected int _version = 0; // NOTE: only used as a "dirty flag" during enumeration -- not synchronized, if used for any other purpose most likely would require synchronization
        public override int Count => _count;

        public LinkedList()
            : this(default(IEnumerable<T>))
        {
        }

        public LinkedList(IEnumerable<T> items)
            : this(items, default(IComparer<T>))
        {
        }

        public LinkedList(IEnumerable<T> items, IComparer<T> comparer)
        {
            _comparer = comparer ?? Comparers<T>.DefaultComparer;
            if (items != null)
            {
                AddRange(items);
            }
        }

        public virtual void Add(T item)
        {
            if (_head == null)
            {
                _head = new Node { Value = item };
                _head.Previous = _head;
                _head.Next = _head;
            }
            else
            {
                var node = new Node { Value = item, Previous = _head.Previous, Next = _head };
                node.Previous.Next = node;
                node.Next.Previous = node;
            }
            _count++;
            _version++;
        }

        public virtual void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Add(item);
            }
            _version++;
        }

        public override IEnumerator<T> GetEnumerator()
        {
            if (_head == null)
            {
                yield break;
            }
            var version = _version;
            var current = _head;
            do
            {
                yield return current.Value;
                current = current.Next;
                if (version != _version)
                {
                    throw new NotSupportedException("Collection was modified.");
                }
            } while (current != _head);
        }

        /// <summary>
        /// Inserts <paramref name="value"/> before the specifeid <paramref
        /// name="comparand"/>, or before any value greater-than-or-equal-to
        /// the specified <paramref name="comparand"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="comparand"></param>
        public virtual void InsertAfter(T value, T comparand)
        {
            var current = _head;
            if (current == null)
            {
                Add(value);
                return;
            }

            Node node = null;
            do
            {
                if (_comparer.Compare(comparand, current.Value) < 0)
                {
                    node = new Node { Value = value, Next = current, Previous = current.Previous };
                    current.Previous.Next = node;
                    current.Previous = node;
                    if (current == _head)
                    {
                        _head = node;
                    }
                    break;
                }
                current = current.Next;
            } while (current != _head);
            if (node == null)
            {
                // requires append
                node = new Node { Value = value, Next = _head, Previous = _head.Previous };
                _head.Previous.Next = node;
                _head.Previous = node;
            }
            _count++;
        }

        // inserts at absolute position `index` (or throws) shifting
        // remaining items over/forward/next at the target position on forward
        public virtual void InsertAt(T value, int index)
        {
            // TODO: optimized which which end we traverse (position/2 vs. size)
            var current = _head;
            if (current == null)
            {
                Add(value);
                return;
            }
            var version = _version;
            int i = 0;
            for (; i < index; i++)
            {
                if (version != _version)
                {
                    throw new InvalidOperationException("Collection was modified.");
                }
                current = current.Next;
            }
            if (i != index)
            {
                throw new IndexOutOfRangeException($"Cannot insert at position {index}");
            }
            var node = new Node { Value = value, Previous = current.Previous, Next = current };
            node.Previous.Next = node;
            node.Next.Previous = node;
            if (current == _head)
            {
                _head = node;
            }
            _count++;
        }

        /// <summary>
        /// Inserts <paramref name="value"/> before the specifeid <paramref
        /// name="comparand"/>, or before any value greater-than-or-equal-to
        /// the specified <paramref name="comparand"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="comparand"></param>
        public virtual void InsertBefore(T value, T comparand)
        {
            var current = _head;
            if (current == null)
            {
                Add(value);
                return;
            }

            Node node = null;
            do
            {
                if (_comparer.Compare(current.Value, comparand) > 0)
                {
                    node = new Node { Value = value, Next = current, Previous = current.Previous };
                    current.Previous.Next = node;
                    current.Previous = node;
                    if (current == _head)
                    {
                        _head = node;
                    }
                    break;
                }
                current = current.Next;
            } while (current != _head);
            if (node == null)
            {
                // requires append
                node = new Node { Value = value, Next = _head, Previous = _head.Previous };
                _head.Previous.Next = node;
                _head.Previous = node;
            }
            _count++;
        }

        /// <summary>
        /// Merges the <paramref name="source"/> into this instance,
        /// destroying the <paramref name="source"/> in the process.
        /// </summary>
        /// <param name="source"></param>
        public void Merge(LinkedList<T> source)
        {
            if (source.Count == 0)
            {
                return;
            }
            else if (this.Count == 0)
            {
                this._head = source._head;
                this._count = source._count;
                source._count = 0;
                source._head = null;
            }
            else
            {
                var left = this._head;
                var right = source._head;
                var sourceTail = source._head.Previous;
                do
                {
                    if (left.Value.CompareTo(right.Value) <= 0)
                    {
                        left = (left.Next == _head)
                            ? null
                            : left.Next;
                    }
                    else
                    {
                        var rightNext = right.Next;
                        right.Next = left;
                        right.Previous = left.Previous;
                        left.Previous.Next = right;
                        left.Previous = right;
                        if (_head == right.Next)
                        {
                            _head = right;
                        }
                        if (source._head == rightNext)
                        {
                            right = null;
                        }
                        else
                        {
                            right = rightNext;
                        }
                    }
                } while (left != null && right != null);
                if (right != null)
                {
                    _head.Previous.Next = right;
                    right.Previous = _head.Previous;
                    _head.Previous = sourceTail;
                    sourceTail.Next = _head;
                }
                _count += source._count;
                source._count = 0;
                source._head = null;
            }
            _version++;
        }

        /// <summary>
        /// Removes the first occurrence with mathing value.
        /// </summary>
        /// <param name="value"></param>
        public virtual bool Remove(T value)
        {
            if (_head != null)
            {
                var current = _head;
                do
                {
                    if (current.Value.CompareTo(value) == 0)
                    {
                        current.Next.Previous = current.Previous;
                        current.Previous.Next = current.Next;
                        _count--;
                        if (_count == 0)
                        {
                            _head = null;
                        }
                        else if (current == _head)
                        {
                            _head = current.Next;
                        }
                        _version++;
                        return true;
                    }
                    current = current.Next;
                } while (current != _head);
            }
            return false;
        }

        public virtual bool RemoveAt(int index)
        {
            // NOTE: a simplified signature for removals when the value is
            //       not required by caller
            return RemoveAt(index, out T value);
        }

        /// <summary>
        /// Removes the items from the specified position.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool RemoveAt(int index, out T value)
        {
            // TODO: if we know the list is ordered we can binary index the structure instead if iterating as is currently done.
            if (_count == 0 || _count < index)
            {
                value = default(T);
                return false;
            }

            // determine shortest path (direction) based on index vs. size
            var forward = !(_count / 2 < index);
            var current = forward ? _head : _head.Previous;
            var position = forward ? 0 : _count - 1;
            do
            {
                if (position == index)
                {
                    current.Next.Previous = current.Previous;
                    current.Previous.Next = current.Next;
                    _count--;
                    if (_count == 0)
                    {
                        _head = null;
                    }
                    else if (current == _head)
                    {
                        _head = current.Next;
                    }
                    _version++;
                    value = current.Value;
                    return true;
                }
                if (forward)
                {
                    current = current.Next;
                    position++;
                }
                else
                {
                    current = current.Previous;
                    position--;
                }
            } while (current != _head);

            value = default(T);
            return false;
        }

        protected sealed class Node
        {
            public Node Next;
            public Node Previous;
            public T Value;

            public override string ToString()
            {
                return $"{Value}";
            }
        }
    }
}
