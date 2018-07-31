using System;
using System.Collections.Generic;

namespace algs4net.Sorts
{
    public class HeapSort<T> :
        SortBase<T>
        where T : IComparable<T>
    {
#if DEBUG
        protected ulong _sinks = 0L;

        protected ulong _maxSinkDepth = 0L;
#endif

        public HeapSort(IComparer<T> comparer)
            : base(comparer)
        {
        }

        public HeapSort()
            : base(default(IComparer<T>))
        {
        }

        public override T[] Sort(T[] heap)
        {
            return Sort(heap, 0, heap.Length - 1);
        }

#if DEBUG
        public override string ToString()
        {
            return $"{base.ToString()}, sinks:{_sinks}";
        }
#endif

        protected virtual T[] Sort(T[] heap, int lo, int hi)
        {
#if DEBUG
            _inputLength = hi - lo;
#endif
            for (int i = lo + ((hi - lo) / 2); i >= lo; i--)
            {
                Sink(heap, i, hi);
            }
            while (hi > lo)
            {
                Exchange(heap, lo, hi--);
                Sink(heap, lo, hi);
            }
            return heap;
        }

        private T[] Sink(T[] heap, int index, int hi)
        {
#if DEBUG
            _sinks++;
#endif
            var swapIndex = index * 2;
#if DEBUG
            ulong depth = 0L;
#endif
            while (swapIndex < hi)
            {
#if DEBUG
                depth++;
#endif
                if (swapIndex < (hi) && IsLessThan(heap[swapIndex], heap[swapIndex + 1]))
                {
                    swapIndex++;
                }
                if (!IsLessThan(heap[index], heap[swapIndex]))
                {
                    break;
                }
                Exchange(heap, index, swapIndex);
                index = swapIndex;
                swapIndex *= 2;
            }
#if DEBUG
            if (depth > _maxSinkDepth)
            {
                _maxSinkDepth = depth;
            }
#endif
            return heap;
        }
    }
}
