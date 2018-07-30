using algs4net.Generators;
using algs4net.Sorts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;

namespace algs4net.Tests.Sorts
{
    [TestClass]
    public class QuickSortTests
    {
        [TestMethod]
        public void QuickSort_CanSupplementSubsetSorts()
        {
            var sort = new QuickSort<int>(
                (int)IntegralNumberGenerator.YieldFibonacciSeries(9).Last(),
                new InsertionSort<int>());
            var set = IntegralNumberGenerator
                .YieldPredictableSeries(10000)
                .ToArray();
            set = sort.Sort(set);
            set.AssertIsOrdered();
            Trace.WriteLine(sort);
        }

        [TestMethod]
        public void QuickSort_Sort_CanSortSubset()
        {
            var mid = 5000;
            var sort = new QuickSort<int>();
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

            Trace.WriteLine(sort);
        }

        [TestMethod]
        public void QuickSort_Sort_ProducesOrderedSets()
        {
            var sort = new QuickSort<int>();
            var set = IntegralNumberGenerator
                .YieldPredictableSeries(10000)
                .ToArray();
            set = sort.Sort(set);
            set.AssertIsOrdered();
            Trace.WriteLine(sort);
        }
    }
}
