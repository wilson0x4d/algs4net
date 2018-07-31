using System;
using System.Collections.Generic;

namespace algs4net.Sorts
{
    public class ShellSort<T> :
        SortBase<T>
        where T : IComparable<T>
    {
        public ShellSort(IComparer<T> comparer)
            : base(comparer)
        {
        }

        public ShellSort()
            : base(default(IComparer<T>))
        {
        }

        public override T[] Sort(T[] input)
        {
            // derive an outer increment `h`, bounded to 1/3rd of the set
            var maxIncrement = input.Length / 3;
            var h = 1;
            while (h < maxIncrement)
            {
                h = (3 * h) + 1;
            }
            while (h >= 1)
            {
                for (var i = h; i < input.Length; i++)
                {
                    for (var j = i; j >= h && IsLessThan(input[j], input[j - h]); j -= h)
                    {
                        Exchange(input, j, j - h);
                    }
                }
                h /= 3;
            }
#if DEBUG
            _inputLength = input.Length;
#endif
            return input;
        }

        /// <summary>
        /// An overload to play around with alternative increment sequeqnces.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="incrementSeries"></param>
        /// <returns></returns>
        public T[] Sort(T[] input, int[] incrementSeries)
        {
            // derive an outer increment `h`, bounded to 1/3rd of the set
            var maxIncrement = input.Length / 3;
            foreach (var h in incrementSeries)
            {
                for (var i = h; i < input.Length; i++)
                {
                    for (var j = i; j >= h && IsLessThan(input[j], input[j - h]); j -= h)
                    {
                        Exchange(input, j, j - h);
                    }
                }
            }
#if DEBUG
            _inputLength = input.Length;
#endif
            return input;
        }
    }
}
