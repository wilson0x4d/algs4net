using System;
using System.Collections.Generic;

namespace algs4net
{
    /// <summary>
    /// A set of purpose-built and default <see cref="IComparer{T}"/> implementations.
    /// </summary>
    /// <typeparam name="T">
    /// Must implement <see cref="IComparable{T}"/>, which may alter behavior
    /// of the algorithms which use <typeparamref name="T"/> as a key/comparand.
    /// </typeparam>
    public static class Comparers<T>
        where T : IComparable<T>
    {
        private static BasicComparer _defaultComparer;

        private static BasicInversionComparer _defaultInversionComparer;

        /// <summary>
        /// Gets the default <see cref="IComparer{T}"/> used by various data
        /// structures and algorithms.
        /// </summary>
        public static IComparer<T> DefaultComparer =>
            _defaultComparer ?? new BasicComparer();

        /// <summary>
        /// Gets the default "inverted" <see cref="IComparer{T}"/> used by
        /// various data structures and algorithms, used to invert the order
        /// of a data structure or algorithm.
        /// </summary>
        public static IComparer<T> DefaultInversionComparer =>
            _defaultInversionComparer ?? new BasicInversionComparer();

        static Comparers()
        {
#if !DEBUG
            _defaultComparer = new ComparableComparer();
            _defaultInversionComparer = new ComparableInversionComparer();
#else
            // if DEBUG we do not re-use comparers, allows capture
            // per-instance counts in-test
            _defaultComparer = null;
            _defaultInversionComparer = null;
#endif
        }

        public sealed class BasicComparer : IComparer<T>
        {
#if DEBUG
            private ulong _compares = 0L;
#endif

            public int Compare(T x, T y)
            {
#if DEBUG
                _compares++;
#endif
                return (x as IComparable<T>).CompareTo(y);
            }

#if DEBUG

            public override string ToString()
            {
                return $"compares:{_compares}";
            }

#endif
        }

        public sealed class BasicInversionComparer : IComparer<T>
        {
#if DEBUG
            private ulong _compares = 0L;
#endif

            public int Compare(T x, T y)
            {
#if DEBUG
                _compares++;
#endif
                return -1 * (x as IComparable<T>).CompareTo(y);
            }

#if DEBUG

            public override string ToString()
            {
                return $"compares:{_compares}";
            }

#endif
        }
    }
}
