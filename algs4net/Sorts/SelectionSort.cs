using System;
using System.Collections.Generic;

namespace algs4net.Sorts
{
    public class SelectionSort<T> :
        SortBase<T>,
        ISupportsSubsetSort<T>
        where T : IComparable<T>
    {
        public SelectionSort(IComparer<T> comparer)
            : base(comparer)
        {
        }

        public SelectionSort()
            : base(default(IComparer<T>))
        {
        }

        public override T[] Sort(T[] input)
        {
            return Sort(input, 0, input.Length - 1);
        }

        public T[] Sort(T[] input, int lo, int hi)
        {
            for (int i = lo; i <= hi; i++)
            {
                var k = i;
                for (int j = i + 1; j <= hi; j++)
                {
                    if (IsLessThan(input[j], input[k]))
                    {
                        k = j;
                    }
                }
                Exchange(input, i, k);
            }
#if DEBUG
            _inputLength = input.Length;
#endif
            return input;
        }
    }
}
