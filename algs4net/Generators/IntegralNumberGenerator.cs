using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace algs4net.Generators
{
    /// <summary>
    /// Various methods for generating an integral series for testing/experimentation.
    /// </summary>
    public static class IntegralNumberGenerator
    {
        public static IEnumerable<T> YieldDupes<T>(T value, int count)
            where T : IComparable<T>
        {
            while (count-- > 0)
            {
                yield return value;
            }
        }

        /// <summary>
        /// Yield a finite Fibonacci sequence as an <see cref="IEnumerable{long}"/>
        /// </summary>
        /// <param name="count">number of elements to yield.</param>
        public static IEnumerable<long> YieldFibonacciSeries(int count)
        {
            var fib = new[] { 0L, 1L };
            while (count-- >= 0)
            {
                yield return fib[0];
                var next = fib[0] + fib[1];
                fib[0] = fib[1];
                fib[1] = next;
            }
        }

        public static IEnumerable<int> YieldGoldenSeries(int space)
        {
            var v = 0;
            var period = space / 3;
            for (int i = 0; i < space; i++)
            {
                yield return v;
                if (i > period)
                {
                    period = (i - period) / 3;
                }
            }
        }

        public static IEnumerable<int> YieldIncrementSeries(int maxIncrement)
        {
            // ie. { 1, 4, 13, ... }
            var increment = 1;
            while (increment < maxIncrement)
            {
                yield return increment;
                increment = (3 * increment) + 1;
            }
        }

        public static IEnumerable<int> YieldPredictableSeries(int count)
        {
            var rand = new Random(18593);
            while (count-- > 0)
            {
                yield return rand.Next(int.MinValue, int.MaxValue);
            }
        }

        public static IEnumerable<int> YieldRandomSeries(int count)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var buf = new byte[4];
                while (count > 0)
                {
                    rng.GetNonZeroBytes(buf);
                    yield return BitConverter.ToInt32(buf, 0);
                    count--;
                }
            }
        }
    }
}
