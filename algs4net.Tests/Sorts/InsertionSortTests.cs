﻿using algs4net.Generators;
using algs4net.Sorts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;

namespace algs4net.Tests.Sorts
{
    [TestClass]
    public class InsertionSortTests
    {
        [TestMethod]
        public void InsertionSort_Sort_CanSortSubset()
        {
            var mid = SortTestHelpers.BASELINE_SORT_SIZE / 2;
            var sort = new InsertionSort<int>();
            var set = IntegralNumberGenerator
                .YieldPredictableSeries(mid)
                .ToArray();
            set = set.Concat(set).ToArray();

            // sort left
            set = sort.Sort(set, 0, mid - 1);

            // sort right
            set = sort.Sort(set, mid, set.Length - 1);

            // assert
            for (int i = 0; i < mid; i++)
            {
                Assert.AreEqual(0, set[i].CompareTo(set[mid + i]));
            }

            sort.Trace();
        }

        [TestMethod]
        public void InsertionSort_Sort_ProducesOrderedSet()
        {
            var sort = new InsertionSort<int>();
            var set = IntegralNumberGenerator
                .YieldPredictableSeries(SortTestHelpers.BASELINE_SORT_SIZE)
                .ToArray();
            set = sort.Sort(set);

            set.AssertIsOrdered();

            sort.Trace();
        }
    }
}
