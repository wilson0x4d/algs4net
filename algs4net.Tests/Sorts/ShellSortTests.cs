using algs4net.Generators;
using algs4net.Sorts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;

namespace algs4net.Tests.Sorts
{
    [TestClass]
    public class ShellSortTests
    {
        [TestMethod]
        public void ShellSort_Sort_ProducesOrderedSet()
        {
            var sort = new ShellSort<int>();
            var set = IntegralNumberGenerator
                .YieldPredictableSeries(SortTestHelpers.BASELINE_SORT_SIZE)
                .ToArray();
            set = sort.Sort(set);
            set.AssertIsOrdered();
            Trace.WriteLine(sort);
        }

        [TestMethod]
        public void ShellSort_Sort_ProducesOrderedSet_WithIncrementSeries()
        {
            var N = 10000;
            var incrementSeries = IntegralNumberGenerator
                .YieldIncrementSeries(N / 3)
                .Reverse()
                .ToArray();
            var sort = new ShellSort<int>();
            var set = IntegralNumberGenerator
                .YieldPredictableSeries(N)
                .ToArray();
            set = sort.Sort(set);
            set.AssertIsOrdered();
            Trace.WriteLine(sort);
        }
    }
}
