using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace algs4net.Tests
{
    public static class SortTestHelpers
    {
        public const int BASELINE_SORT_SIZE = 25000;

        public static void AssertIsOrdered<T>(this T[] input)
            where T : IComparable<T>
        {
            for (int i = 1; i < input.Length; i++)
            {
                Assert.IsFalse(input[i - 1].CompareTo(input[i]) > 0,
                    $"Expected a value less-than-or-equal to `{input[i - 1]}@{i - 1}` but observed `{input[i]}@{i}`.");
            }
        }

        public static void AssertIsUnordered<T>(this T[] input)
            where T : IComparable<T>
        {
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i - 1].CompareTo(input[i]) > 0)
                {
                    return;
                }
            }
            Assert.Fail();
        }
    }
}
