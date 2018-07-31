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
        private static IComparer<T> _defaultComparer;

        private static IEqualityComparer<T> _defaultEqualityComparer;

        private static IComparer<T> _defaultInversionComparer;

        private static IEqualityComparer<T> _defaultInversionEqualityComparer;

        /// <summary>
        /// Gets the default <see cref="IComparer{T}"/> used by various data
        /// structures and algorithms.
        /// </summary>
        public static IComparer<T> DefaultComparer =>
            _defaultComparer ?? new BasicComparer();

        public static IEqualityComparer<T> DefaultEqualityComparer =>
            _defaultEqualityComparer ?? new BasicComparer();

        /// <summary>
        /// Gets the default "inverted" <see cref="IComparer{T}"/> used by
        /// various data structures and algorithms, used to invert the order
        /// of a data structure or algorithm.
        /// </summary>
        public static IComparer<T> DefaultInversionComparer =>
            _defaultInversionComparer ?? new BasicInversionComparer();

        public static IEqualityComparer<T> DefaultInversionEqualityComparer =>
            _defaultInversionEqualityComparer ?? new BasicInversionComparer();

        static Comparers()
        {
#if !DEBUG
            var defaultComparer = new ComparableComparer();
            _defaultComparer = defaultComparer;
            _defaultEqualityComparer = defaultComparer;
            var defaultInversionComparer = new ComparableInvestionComparer();
            _defaultInversionComparer = defaultInversionComparer;
            _defaultInversionEqualityComparer = defaultInversionComparer;
#else
            // if DEBUG we do not re-use comparers, allows capture
            // per-instance counts in-test
            _defaultComparer = null;
            _defaultEqualityComparer = null;
            _defaultInversionComparer = null;
            _defaultInversionEqualityComparer = null;
#endif
        }

        public sealed class BasicComparer :
            IComparer<T>,
            IEqualityComparer<T>
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

            public bool Equals(T x, T y)
            {
#if DEBUG
                _compares++;
#endif
                return (x is IEquatable<T> equatable)
                    ? equatable.Equals(y)
                    : x.Equals(y);
            }

            public int GetHashCode(T obj)
            {
                return obj.GetHashCode();
            }

#if DEBUG

            public override string ToString()
            {
                return $"compares:{_compares}";
            }

#endif
        }

        public sealed class BasicInversionComparer :
            IComparer<T>,
            IEqualityComparer<T>
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

            public bool Equals(T x, T y)
            {
#if DEBUG
                _compares++;
#endif
                return (x is IEquatable<T> equatable)
                    ? equatable.Equals(y)
                    : x.Equals(y);
            }

            public int GetHashCode(T obj)
            {
                return obj.GetHashCode();
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
