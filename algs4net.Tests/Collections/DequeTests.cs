using algs4net.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;

namespace algs4net.Tests.Collections
{
    [TestClass]
    public class DequeTests
    {
        [TestMethod]
        public void Deque_PopLeft_YieldsExpectedItems()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(10).ToArray();
            var deque = new Deque<int>();
            foreach (var v in expectedValues)
            {
                deque.PushLeft(v);
            }
            Assert.AreEqual(expectedValues.Length, deque.Count);
            foreach (var expectedValue in expectedValues.Reverse())
            {
                var actualValue = deque.PopLeft();
                Assert.AreEqual(expectedValue, actualValue);
            }
            deque.Trace();
        }

        [TestMethod]
        public void Deque_PopRight_YieldsExpectedItems()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(10).ToArray();
            var deque = new Deque<int>();
            foreach (var v in expectedValues)
            {
                deque.PushRight(v);
            }
            Assert.AreEqual(expectedValues.Length, deque.Count);
            foreach (var expectedValue in expectedValues.Reverse())
            {
                var actualValue = deque.PopRight();
                Assert.AreEqual(expectedValue, actualValue);
            }
            deque.Trace();
        }

        [TestMethod]
        public void Deque_PushLeft_YieldsExpectedItems()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(10).ToArray();
            var deque = new Deque<int>();
            foreach (var v in expectedValues)
            {
                deque.PushLeft(v);
            }
            Assert.AreEqual(expectedValues.Length, deque.Count);

            var i = expectedValues.Length;
            foreach (var actualValue in deque)
            {
                Assert.AreEqual(expectedValues[--i], actualValue);
            }
            deque.Trace();
        }

        [TestMethod]
        public void Deque_PushRight_YieldsExpectedItems()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(10).ToArray();
            var deque = new Deque<int>();
            foreach (var v in expectedValues)
            {
                deque.PushRight(v);
            }
            Assert.AreEqual(expectedValues.Length, deque.Count);
            var i = 0;
            foreach (var actualValue in deque)
            {
                Assert.AreEqual(expectedValues[i], actualValue);
                i++;
            }
            deque.Trace();
        }
    }
}
