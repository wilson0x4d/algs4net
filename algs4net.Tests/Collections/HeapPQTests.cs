using algs4net.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;

namespace algs4net.Tests.Collections
{
    [TestClass]
    public class HeapPQTests
    {
        [TestMethod]
        public void HeapPQ_CanHandleLargeSets()
        {
            var count = 10000000;
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(count);
            var pq = new HeapPQ<int>(expectedValues);
            foreach (var v in expectedValues)
            {
                pq.Enqueue(v);
            }
            Assert.AreEqual(2 * count, pq.Count);
            pq.Trace();
        }

        [TestMethod]
        public void HeapPQ_ctor_CanConstructPopulated()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(10000).ToArray();
            var pq = new HeapPQ<int>(expectedValues);
            expectedValues = expectedValues.OrderBy(e => e).Reverse().ToArray();
            foreach (var expectedValue in expectedValues)
            {
                var actualValue = pq.Dequeue();
                Assert.AreEqual(expectedValue, actualValue);
            }
            pq.Trace();
        }

        [TestMethod]
        public void HeapPQ_ctor_CanConstructPopulated_WithComparer()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(10000).ToArray();
            var pq = new HeapPQ<int>(expectedValues, Comparers<int>.InversionComparer);
            expectedValues = expectedValues.OrderBy(e => e).ToArray();
            foreach (var expectedValue in expectedValues)
            {
                var actualValue = pq.Dequeue();
                Assert.AreEqual(expectedValue, actualValue);
            }
            pq.Trace();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HeapPQ_ctor_ThrowOnZeroOrNegativeCapacity()
        {
            var pq = new HeapPQ<int>(0);
            pq.Trace();
        }

        [TestMethod]
        public void HeapPQ_Dequeue_YieldsExpectedResults()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(10000).ToArray();
            var pq = new HeapPQ<int>();
            foreach (var v in expectedValues)
            {
                pq.Enqueue(v);
            }
            Assert.AreEqual(expectedValues.Length, pq.Count);
            expectedValues = expectedValues.OrderBy(e => e).Reverse().ToArray();
            foreach (var expectedValue in expectedValues)
            {
                var actualValue = pq.Dequeue();
                Assert.AreEqual(expectedValue, actualValue);
            }
            pq.Trace();
        }

        [TestMethod]
        public void HeapPQ_Dequeue_YieldsExpectedResults_WithComparer()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(10000).ToArray();
            var pq = new HeapPQ<int>(Comparers<int>.InversionComparer);
            foreach (var v in expectedValues)
            {
                pq.Enqueue(v);
            }
            Assert.AreEqual(expectedValues.Length, pq.Count);
            expectedValues = expectedValues.OrderBy(e => e).ToArray();
            foreach (var expectedValue in expectedValues)
            {
                var actualValue = pq.Dequeue();
                Assert.AreEqual(expectedValue, actualValue);
            }
            pq.Trace();
        }

        [TestMethod]
        public void HeapPQ_Enqueue_YieldsExpectedCount()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(10000).ToArray();
            var pq = new HeapPQ<int>();
            foreach (var v in expectedValues)
            {
                pq.Enqueue(v);
            }
            Assert.AreEqual(expectedValues.Length, pq.Count);
            pq.Trace();
        }

        [TestMethod]
        public void HeapPQ_Enqueue_YieldsExpectedCount_WithComparer()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(10000).ToArray();
            var pq = new HeapPQ<int>(Comparers<int>.InversionComparer);
            foreach (var v in expectedValues)
            {
                pq.Enqueue(v);
            }
            Assert.AreEqual(expectedValues.Length, pq.Count);
            pq.Trace();
        }

        [TestMethod]
        public void HeapPQ_indexer_CanAccessHeap()
        {
            var originValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(1000).ToArray();
            var expectedValues = originValues.OrderBy(e => e).ToArray();
            SortTestHelpers.AssertIsUnordered(originValues);
            SortTestHelpers.AssertIsOrdered(expectedValues);
            var pq = new HeapPQ<int>(originValues);
            //
            for (int i = 0; i < expectedValues.Length; i++)
            {
                pq[i] = expectedValues[i];
            }
            for (int i = 0; i < expectedValues.Length; i++)
            {
                Assert.AreEqual(expectedValues[i], pq[i]);
            }
            pq.Trace();
        }
    }
}
