using System;

namespace algs4net.Sorts
{
    public interface ISupportsSubsetSort<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Perform a constrained sort on a subset of the specified input.
        /// <para>
        /// Take care that <paramref name="hi"/> is a valid rank within the
        /// input, ie. you would never pass `array.Length` in as a `hi`
        /// value, it would be out-of-bounds.
        /// </para>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <returns></returns>
        T[] Sort(T[] input, int lo, int hi);
    }
}
