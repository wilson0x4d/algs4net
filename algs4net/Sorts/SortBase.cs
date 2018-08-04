using System;
using System.Collections.Generic;

namespace algs4net.Sorts
{
    public abstract class SortBase<T>
        where T : IComparable<T>
    {
        protected readonly IComparer<T> _comparer;

#if DEBUG
        protected ulong _comparisonCount = 0;

        protected ulong _exchangeCount = 0;

        protected int _inputLength = 0;
#endif

        public SortBase(IComparer<T> comparer)
        {
            _comparer = comparer ?? Comparers<T>.DefaultComparer;
        }

        protected virtual void Exchange(T[] input, int leftIndex, int rightIndex)
        {
#if DEBUG
            _exchangeCount++;
#endif
            T value = input[leftIndex];
            input[leftIndex] = input[rightIndex];
            input[rightIndex] = value;
        }

        public virtual bool IsLessThan(T leftValue, T rightValue)
        {
#if DEBUG
            _comparisonCount++;
#endif
            return _comparer.Compare(leftValue, rightValue) < 0;
        }

        public abstract T[] Sort(T[] input);

#if DEBUG

        public override string ToString()
        {
            return $"len:{_inputLength}, compares:{_comparisonCount}, exchanges:{_exchangeCount}";
        }

#endif
    }
}
