using algs4net.Generators;
using algs4net.Sorts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;

namespace algs4net.Tests.Sorts
{
    [TestClass]
    public class HeapSortTests
    {
        [TestMethod]
        public void HeapSort_Sort_ProducesOrderedSets()
        {
            var sort = new HeapSort<int>();
            var expected = IntegralNumberGenerator
                .YieldPredictableSeries(SortTestHelpers.BASELINE_SORT_SIZE)
                .OrderBy(e => e)
                .ToArray();
            var set = IntegralNumberGenerator
                .YieldPredictableSeries(SortTestHelpers.BASELINE_SORT_SIZE)
                .ToArray();
            set = sort.Sort(set);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], set[i]);
            }
            Trace.WriteLine(sort);
        }

        [TestMethod]
        public void HeapSort_Sort_ProducesOrderedSets_WithComparer()
        {
            var sort = new HeapSort<int>(Comparers<int>.DefaultInversionComparer);
            var expected = IntegralNumberGenerator
                .YieldPredictableSeries(SortTestHelpers.BASELINE_SORT_SIZE)
                .OrderBy(e => e)
                .Reverse()
                .ToArray();
            var set = IntegralNumberGenerator
                .YieldPredictableSeries(SortTestHelpers.BASELINE_SORT_SIZE)
                .ToArray();
            set = sort.Sort(set);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], set[i]);
            }
            Trace.WriteLine(sort);
        }
    }
}
