﻿using algs4net.Generators;
using algs4net.Sorts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;

namespace algs4net.Tests.Sorts
{
    [TestClass]
    public class SelectionSortTests
    {
        [TestMethod]
        public void SelectionSort_Sort_ProducesOrderedSet()
        {
            var sort = new SelectionSort<int>();
            var set = IntegralNumberGenerator
                .YieldPredictableSeries(SortTestHelpers.BASELINE_SORT_SIZE)
                .ToArray();
            set = sort.Sort(set);
            set.AssertIsOrdered();
            sort.Trace();
        }
    }
}
