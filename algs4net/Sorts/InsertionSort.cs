using System;
using System.Collections.Generic;

namespace algs4net.Sorts
{
    public class InsertionSort<T> :
        SortBase<T>,
        ISupportsSubsetSort<T>
        where T : IComparable<T>
    {
        public InsertionSort(IComparer<T> comparer)
            : base(comparer)
        {
        }

        public InsertionSort()
            : base(default(IComparer<T>))
        {
        }

        public override T[] Sort(T[] input)
        {
            return Sort(input, 0, input.Length - 1);
        }

        public virtual T[] Sort(T[] input, int lo, int hi)
        {
            for (int i = lo; i <= hi; i++)
            {
                for (int j = i; j > lo && IsLessThan(input[j], input[j - 1]); j--)
                {
                    Exchange(input, j, j - 1);
                }
            }
#if DEBUG
            _inputLength = hi - lo;
#endif
            return input;
        }
    }
}
