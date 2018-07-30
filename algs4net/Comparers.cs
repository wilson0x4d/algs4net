using System;
using System.Collections.Generic;

namespace algs4net
{
    /// <summary>
    /// A set of purpose-built and default <see cref="IComparer{T}"/> implementations.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Comparers<T>
        where T : IComparable<T>
    {
        public static readonly IComparer<T> DefaultComparer;

        public static readonly IComparer<T> InversionComparer;

        static Comparers()
        {
            DefaultComparer = new ComparableComparer();
            InversionComparer = new ComparableInversionComparer();
        }

        private sealed class ComparableComparer : IComparer<T>
        {
            public int Compare(T x, T y)
            {
                return (x as IComparable<T>).CompareTo(y);
            }
        }

        private sealed class ComparableInversionComparer : IComparer<T>
        {
            public int Compare(T x, T y)
            {
                return -1 * (x as IComparable<T>).CompareTo(y);
            }
        }
    }
}
