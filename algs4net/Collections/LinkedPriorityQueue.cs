using System;
using System.Collections.Generic;

namespace algs4net.Collections
{
    /// <summary>
    /// An 'elementary' but 'functional' Priority Queue implementation, used
    /// as a basis for more advanced implementations.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LinkedPriorityQueue<T> :
        CollectionBase<T>,
        IQueue<T>,
        IEnumerable<T>
        where T : IComparable<T>
    {
        protected readonly IComparer<T> _comparer;

        protected readonly LinkedList<T> _items;

#if DEBUG
        protected ulong _dequeues = 0L;

        protected ulong _enqueues = 0L;
#endif

        // TODO: LinkedList variant with `Parent` node, and re-implement LinkedPQ with relevant Sink()/Swim() logic (replacing current elementary implementation)

        public override int Count => _items.Count;

        public LinkedPriorityQueue()
            : this(default(IEnumerable<T>))
        {
        }

        public LinkedPriorityQueue(IEnumerable<T> items)
            : this(items, default(IComparer<T>))
        {
        }

        public LinkedPriorityQueue(IEnumerable<T> items, IComparer<T> comparer)
        {
            _comparer = comparer ?? Comparers<T>.DefaultComparer;
            _items = new LinkedList<T>(null, _comparer);
            if (items != null)
            {
                foreach (var item in items)
                {
                    Enqueue(item);
                }
            }
        }

        public virtual T Dequeue()
        {
            if (!_items.RemoveAt(0, out T value))
            {
                throw new NotSupportedException("Queue was empty during `Dequeue()` call.");
            }
#if DEBUG
            _dequeues++;
#endif
            return value;
        }

        public virtual void Enqueue(T item)
        {
            _items.InsertAfter(item, item);
#if DEBUG
            _enqueues++;
#endif
        }

        public override IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

#if DEBUG

        public override string ToString()
        {
            return $"{base.ToString()}, enqueues:{_enqueues}, dequeues:{_dequeues}, list:{{ {_items} }}";
        }

#endif
    }
}
