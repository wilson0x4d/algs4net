using System;
using System.Collections.Generic;

namespace algs4net.Sorts
{
    public class MergeSort<T> :
        SortBase<T>,
        ISupportsSubsetSort<T>
        where T : IComparable<T>
    {
#if DEBUG
        protected long _cycles = 0L;
#endif

        public MergeSort(IComparer<T> comparer)
            : base(comparer)
        {
        }

        public MergeSort()
            : base(default(IComparer<T>))
        {
        }

        public override T[] Sort(T[] input)
        {
#if DEBUG
            _inputLength = input.Length;
#endif
            if (input.Length <= 1)
            {
                return input;
            }
            else if (input.Length == 2)
            {
                return IsLessThan(input[0], input[1])
                    ? input
                    : new T[] { input[1], input[0] };
            }
            else
            {
                Sort(input, 0, input.Length - 1);
                return input;
            }
        }

        public virtual T[] Sort(T[] input, int lo, int hi)
        {
#if DEBUG
            _cycles++;
#endif
            if (hi > lo)
            {
                var mid = ((hi - lo) / 2) + lo;
                Sort(input, lo, mid);
                Sort(input, mid + 1, hi);
                Merge(input, lo, mid, hi);
            }
            return input;
        }

#if DEBUG
        public override string ToString()
        {
            return $"{base.ToString()}, cycles:{_cycles}";
        }
#endif

        private void Merge(T[] input, int lo, int mid, int hi)
        {
            var clone = new T[input.Length];
            Array.Copy(input, clone, clone.Length); // TODO: using linked lists instead of arrays would allow us to eliminate the clone without dramatically changing any logic -- consider a set of LinkedList-aware Sort() interfaces
            var i = lo;
            var j = mid + 1;
            for (int k = 0; k < clone.Length; k++)
            {
                if (i <= mid && (j == hi || IsLessThan(clone[i], clone[j])))
                {
                    input[k] = clone[i];
#if DEBUG
                    _exchangeCount++;
#endif
                }
                else if (j <= hi && (i == mid || IsLessThan(clone[j], clone[i])))
                {
                    input[k] = clone[j];
#if DEBUG
                    _exchangeCount++;
#endif
                }
            }
        }
    }
}
