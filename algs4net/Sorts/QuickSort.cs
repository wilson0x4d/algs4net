using System;
using System.Collections.Generic;

namespace algs4net.Sorts
{
    public class QuickSort<T> :
        SortBase<T>,
        ISupportsSubsetSort<T>
        where T : IComparable<T>
    {
        protected readonly int _maximumSubsetSortSize;

        protected readonly ISupportsSubsetSort<T> _subsetSort;

#if DEBUG
        protected ulong _cycles = 0L;

        protected ulong _partitions = 0L;
#endif

        private QuickSortAlgorithm _sortAlgorithm;

        public QuickSort()
            : this(0, null)
        {
        }

        public QuickSort(
            int maximumInsertionSortSetSize,
            ISupportsSubsetSort<T> subsetSort)
            : this(maximumInsertionSortSetSize, subsetSort, null)
        {
        }

        public QuickSort(
            int maximumInsertionSortSetSize,
            ISupportsSubsetSort<T> subsetSort,
            IComparer<T> comparer)
            : this(maximumInsertionSortSetSize, subsetSort, null, comparer)
        {
        }

        public QuickSort(
            int maximumInsertionSortSetSize,
            ISupportsSubsetSort<T> subsetSort,
            QuickSortAlgorithm sortAlgorithm,
            IComparer<T> comparer)
            : base(comparer)
        {
            _maximumSubsetSortSize = maximumInsertionSortSetSize;
            _subsetSort = subsetSort;
            _sortAlgorithm = sortAlgorithm ?? SortWithTwoWayPartition;
        }

        public override T[] Sort(T[] input)
        {
            input = RandomizeInput(input);
            return Sort(input, 0, input.Length - 1);
        }

        public virtual T[] Sort(T[] input, int lo, int hi)
        {
#if DEBUG
            _cycles++;
            _inputLength = input.Length;
#endif
            if (hi <= lo + _maximumSubsetSortSize)
            {
                return (_subsetSort != null)
                    ? _subsetSort.Sort(input, lo, hi)
                    : input;
            }
            else
            {
                // TODO: in the default case, use a variant of Dijkstra's 3-way partition/merge instead of `SortWithTwoWayPartition` as is done now:
                _sortAlgorithm = _sortAlgorithm ?? SortWithTwoWayPartition;
                _sortAlgorithm(input, lo, hi);
            }
            return input;
        }

#if DEBUG
        public override string ToString()
        {
            return $"type:{_sortAlgorithm.Method.Name}, {base.ToString()}, cycles:{_cycles}, parts:{_partitions}";
        }
#endif

        protected virtual int Partition(T[] input, int lo, int hi)
        {
#if DEBUG
            _partitions++;
#endif
            var i = lo;
            var j = hi;
            var v = input[i];
            while (true)
            {
                while (++i < j && IsLessThan(input[i], v)) ;
                while (IsLessThan(v, input[j]) && j-- >= i) ;
                if (i >= j)
                {
                    break;
                }
                Exchange(input, i, j);
            }
            Exchange(input, lo, j);
            return j;
        }

        protected virtual T[] RandomizeInput(T[] input)
        {
            // NOTE: not a true rand, simply interleaves values on an odd
            // boundary to reduce large contiquous sets of duplicates,
            // ordered input, or other odd interactions with binary algorithms
            for (int i = 1; i < (input.Length / 2); i += 3)
            {
                Exchange(input, i, input.Length - i);
            }
            return input;
        }

        private void SortWithThreeWayPartition(T[] input, int lo, int hi)
        {
            throw new NotImplementedException();
        }

        private void SortWithTwoWayPartition(T[] input, int lo, int hi)
        {
            var j = Partition(input, lo, hi);
            Sort(input, lo, j - 1);
            Sort(input, j + 1, hi);
        }

        public delegate void QuickSortAlgorithm(T[] input, int lo, int hi);
    }
}
