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
    public class HeapPQ<T> :
        CollectionBase<T>,
        IQueue<T>,
        IEnumerable<T>
        where T : IComparable<T>
    {
        private IComparer<T> _comparer;

        private int _count = 0;

        private T[] _heap; // TODO: looks like it may be possible to perform N-ary heap distributions during `Sink()`

        public override int Count => _count;

        public HeapPQ(int capacity, IComparer<T> comparer)
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    $"Zero or Negative values are not acceptable.");
            }
            _heap = new T[capacity + 1];
            _comparer = comparer ?? Comparers<T>.DefaultComparer;
        }

        public HeapPQ(IEnumerable<T> items, IComparer<T> comparer)
            : this(comparer)
        {
            foreach (var item in items)
            {
                Enqueue(item);
            }
        }

        public HeapPQ(IEnumerable<T> items)
            : this(items, default(IComparer<T>))
        {
        }

        public HeapPQ(int capacity)
            : this(capacity, default(IComparer<T>))
        {
        }

        public HeapPQ(IComparer<T> comparer)
            : this(0x4d, comparer)
        {
        }

        public HeapPQ()
            : this(default(IComparer<T>))
        {
        }

        /// <summary>
        /// Provide indexed access into the data structure, useful for some
        /// operations. Not to be confused with an "Indexed Heap".
        /// <para>Accepts a 0-based index as is typical for indexers.</para>
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual T this[int index]
        {
            get
            {
                index++;
                if (index > _count)
                {
                    throw new IndexOutOfRangeException();
                }
                return _heap[index];
            }
            set
            {
                index++;
                if (index > _count)
                {
                    throw new IndexOutOfRangeException();
                }
                _heap[index] = value;
            }
        }

        public T Dequeue()
        {
            var item = _heap[1];
            _heap[1] = _heap[_count];
            _heap[_count] = default(T);
            _count--;
            var mid = _heap.Length >> 1;
            if (_count < mid)
            {
                Array.Resize(ref _heap, mid);
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
            }
            var index = _count;
            _heap[index] = item;
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

        private int Sink(int index)
        {
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
            }
            return index;
        }

        private int Swim(int index)
        {
            var swapIndex = index / 2;
            while (swapIndex >= 1 && _comparer.Compare(_heap[index], _heap[swapIndex]) > 0)
            {
                var item = _heap[index];
                _heap[index] = _heap[swapIndex];
                _heap[swapIndex] = item;
                index = swapIndex;
                swapIndex /= 2;
            }
            return index;
        }
    }
}
