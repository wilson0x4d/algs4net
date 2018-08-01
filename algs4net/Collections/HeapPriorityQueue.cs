using System;
using System.Collections.Generic;

namespace algs4net.Collections
{
    /// <summary>
    /// A Priority Queue implementation which uses a binheap to order items.
    /// <para>
    /// You can supply a custom <see cref="IComparer{T}"/> during
    /// construction to alter ordering behavior.
    /// </para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HeapPriorityQueue<T> :
        CollectionBase<T>,
        IQueue<T>,
        IEnumerable<T>
        where T : IComparable<T>
    {
        private readonly IComparer<T> _comparer;

        private int _count = 0;

#if DEBUG

        private ulong _exchanges = 0L;

#endif

        private T[] _heap; // TODO: looks like it may be possible to perform N-ary heap distributions during `Sink()`

#if DEBUG

        private ulong _inserts = 0L;

        private ulong _removals = 0L;

        private ulong _resizesdown = 0L;

        private ulong _resizesup = 0L;

        private ulong _sinks = 0L;

        private ulong _swims = 0L;

#endif

        public override int Count => _count;

        public HeapPriorityQueue(int capacity, IComparer<T> comparer)
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    $"Zero or Negative values are not acceptable.");
            }
            _heap = new T[capacity + 1];
            _comparer = comparer ?? Comparers<T>.DefaultComparer;
        }

        public HeapPriorityQueue(IEnumerable<T> items, IComparer<T> comparer)
            : this(comparer)
        {
            foreach (var item in items)
            {
                Enqueue(item);
            }
        }

        public HeapPriorityQueue(IEnumerable<T> items)
            : this(items, default(IComparer<T>))
        {
        }

        public HeapPriorityQueue(int capacity)
            : this(capacity, default(IComparer<T>))
        {
        }

        public HeapPriorityQueue(IComparer<T> comparer)
            : this(0x4d, comparer)
        {
        }

        public HeapPriorityQueue()
            : this(default(IComparer<T>))
        {
        }

        public T Dequeue()
        {
            var item = _heap[1];
            _heap[1] = _heap[_count];
            _heap[_count] = default(T);
#if DEBUG
            _removals++;
#endif
            _count--;
            var mid = _heap.Length >> 1;
            if (_count < mid && mid >= 0x4d)
            {
                Array.Resize(ref _heap, mid);
#if DEBUG
                _resizesdown++;
#endif
            }
            Sink(1);
            return item;
        }

        public void Enqueue(T item)
        {
            _count++;
            if (_count >= _heap.Length)
            {
                Array.Resize(ref _heap, (_heap.Length << 2));
#if DEBUG
                _resizesup++;
#endif
            }
            var index = _count;
            _heap[index] = item;
#if DEBUG
            _inserts++;
#endif
            Swim(index);
        }

        public override IEnumerator<T> GetEnumerator()
        {
            // NOTE: implement a better heap walk -- also consider
            // re-intepreting the `this[int index]` indexer arg as well
            for (int i = 1; i <= _count; i++)
            {
                yield return _heap[i];
            }
        }

#if DEBUG

        public override string ToString()
        {
            return $"{base.ToString()}, capacity:{_heap.Length - 1}, sizeup:{_resizesup}, sizedown:{_resizesdown}, inserts:{_inserts}, removals:{_removals}, sinks:{_sinks}, swims:{_swims}, exchanges:{_sinks}, {_comparer}";
        }

#endif

        private int Sink(int index)
        {
#if DEBUG
            _sinks++;
#endif
            var swapIndex = index * 2;
            while (swapIndex <= _count)
            {
                if (swapIndex < _count && _comparer.Compare(_heap[swapIndex], _heap[swapIndex + 1]) < 0)
                {
                    swapIndex++;
                }
                if (_comparer.Compare(_heap[index], _heap[swapIndex]) >= 0)
                {
                    break;
                }
                var item = _heap[swapIndex];
                _heap[swapIndex] = _heap[index];
                _heap[index] = item;
                index = swapIndex;
                swapIndex *= 2;
#if DEBUG
                _exchanges++;
#endif
            }
            return index;
        }

        private int Swim(int index)
        {
#if DEBUG
            _swims++;
#endif
            var swapIndex = index / 2;
            while (swapIndex >= 1 && _comparer.Compare(_heap[index], _heap[swapIndex]) > 0)
            {
                var item = _heap[index];
                _heap[index] = _heap[swapIndex];
                _heap[swapIndex] = item;
                index = swapIndex;
                swapIndex /= 2;
#if DEBUG
                _exchanges++;
#endif
            }
            return index;
        }
    }
}
