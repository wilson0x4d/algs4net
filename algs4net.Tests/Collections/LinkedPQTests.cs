using algs4net.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;

namespace algs4net.Tests.Collections
{
    [TestClass]
    public class LinkedPQTests
    {
        [TestMethod]
        public void LinkedPQ_ctor_CanConstructPopulated()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(1000).ToArray();
            var pq = new LinkedPQ<int>(expectedValues);
            expectedValues = expectedValues.OrderBy(e => e).ToArray();
            foreach (var expectedValue in expectedValues)
            {
                var actualValue = pq.Dequeue();
                Assert.AreEqual(expectedValue, actualValue);
            }
            pq.Trace();
        }

        [TestMethod]
        public void LinkedPQ_ctor_CanConstructPopulated_WithComparer()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(1000).ToArray();
            var pq = new LinkedPQ<int>(expectedValues, Comparers<int>.DefaultInversionComparer);
            expectedValues = expectedValues.OrderBy(e => e).Reverse().ToArray();
            foreach (var expectedValue in expectedValues)
            {
                var actualValue = pq.Dequeue();
                Assert.AreEqual(expectedValue, actualValue);
            }
            pq.Trace();
        }

        [TestMethod]
        public void LinkedPQ_Dequeue_YieldsExpectedResults()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(1000).ToArray();
            var pq = new LinkedPQ<int>(expectedValues);
            expectedValues = expectedValues.OrderBy(e => e).ToArray();
            foreach (var expectedValue in expectedValues)
            {
                var actualValue = pq.Dequeue();
                Assert.AreEqual(expectedValue, actualValue);
            }
            pq.Trace();
        }

        [TestMethod]
        public void LinkedPQ_Dequeue_YieldsExpectedResults_WithComparer()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(1000).ToArray();
            var pq = new LinkedPQ<int>(null, Comparers<int>.DefaultInversionComparer);
            foreach (var v in expectedValues)
            {
                pq.Enqueue(v);
            }
            expectedValues = expectedValues.OrderBy(e => e).Reverse().ToArray();
            foreach (var expectedValue in expectedValues)
            {
                var actualValue = pq.Dequeue();
                Assert.AreEqual(expectedValue, actualValue);
            }
            pq.Trace();
        }

        [TestMethod]
        public void LinkedPQ_Enqueue_YieldsExpectedResults()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(1000).ToArray();
            var pq = new LinkedPQ<int>();
            foreach (var v in expectedValues)
            {
                pq.Enqueue(v);
            }
            expectedValues = expectedValues.OrderBy(e => e).ToArray();
            var i = 0;
            foreach (var actualValue in pq)
            {
                Assert.AreEqual(expectedValues[i], actualValue);
                i++;
            }
            pq.Trace();
        }

        [TestMethod]
        public void LinkedPQ_Enqueue_YieldsExpectedResults_WithComparer()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(1000).ToArray();
            var pq = new LinkedPQ<int>(null, Comparers<int>.DefaultInversionComparer);
            foreach (var v in expectedValues)
            {
                pq.Enqueue(v);
            }
            expectedValues = expectedValues.OrderBy(e => e).Reverse().ToArray();
            var i = 0;
            foreach (var actualValue in pq)
            {
                Assert.AreEqual(expectedValues[i], actualValue);
                i++;
            }
            pq.Trace();
        }
    }
}
