using algs4net.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;

namespace algs4net.Tests.Collections
{
    [TestClass]
    public class HeapPriorityQueueTests
    {
        [TestMethod]
        public void HeapPriorityQueue_CanHandleLargeSets()
        {
            var count = 10000000;
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(count).ToArray();
            var pq = new HeapPriorityQueue<int>(expectedValues);
            foreach (var v in expectedValues)
            {
                pq.Enqueue(v);
            }
            Assert.AreEqual(2 * count, pq.Count);
            pq.Trace();
        }

        [TestMethod]
        public void HeapPriorityQueue_ctor_CanConstructPopulated()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(10000).ToArray();
            var pq = new HeapPriorityQueue<int>(expectedValues);
            expectedValues = expectedValues.OrderBy(e => e).Reverse().ToArray();
            foreach (var expectedValue in expectedValues)
            {
                var actualValue = pq.Dequeue();
                Assert.AreEqual(expectedValue, actualValue);
            }
            pq.Trace();
        }

        [TestMethod]
        public void HeapPriorityQueue_ctor_CanConstructPopulated_WithComparer()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(10000).ToArray();
            var pq = new HeapPriorityQueue<int>(expectedValues, Comparers<int>.DefaultInversionComparer);
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
        public void HeapPriorityQueue_ctor_ThrowOnZeroOrNegativeCapacity()
        {
            var pq = new HeapPriorityQueue<int>(0);
            pq.Trace();
        }

        [TestMethod]
        public void HeapPriorityQueue_Dequeue_YieldsExpectedResults()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(10000).ToArray();
            var pq = new HeapPriorityQueue<int>();
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
        public void HeapPriorityQueue_Dequeue_YieldsExpectedResults_WithComparer()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(10000).ToArray();
            var pq = new HeapPriorityQueue<int>(Comparers<int>.DefaultInversionComparer);
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
        public void HeapPriorityQueue_Enqueue_YieldsExpectedCount()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(10000).ToArray();
            var pq = new HeapPriorityQueue<int>();
            foreach (var v in expectedValues)
            {
                pq.Enqueue(v);
            }
            Assert.AreEqual(expectedValues.Length, pq.Count);
            pq.Trace();
        }

        [TestMethod]
        public void HeapPriorityQueue_Enqueue_YieldsExpectedCount_WithComparer()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(10000).ToArray();
            var pq = new HeapPriorityQueue<int>(Comparers<int>.DefaultInversionComparer);
            foreach (var v in expectedValues)
            {
                pq.Enqueue(v);
            }
            Assert.AreEqual(expectedValues.Length, pq.Count);
            pq.Trace();
        }
    }
}
