using algs4net.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;

namespace algs4net.Tests.Collections
{
    [TestClass]
    public class StequeTests
    {
        [TestMethod]
        public void Steque_Dequeue_YieldsExpectedItems()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(10).ToArray();
            var steque = new Steque<int>();
            foreach (var v in expectedValues)
            {
                steque.Push(v);
            }
            foreach (var expectedValue in expectedValues)
            {
                var actualValue = steque.Dequeue();
                Assert.AreEqual(expectedValue, actualValue);
            }
            steque.Trace();
        }

        [TestMethod]
        public void Steque_Enqueue_YieldsExpectedItems()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(10).ToArray();
            var steque = new Steque<int>();
            foreach (var v in expectedValues)
            {
                steque.Enqueue(v);
            }
            var i = 0;
            foreach (var actualValue in steque)
            {
                Assert.AreEqual(expectedValues[i], actualValue);
                i++;
            }
            steque.Trace();
        }

        [TestMethod]
        public void Steque_Pop_YieldsExpectedValues()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(10).ToArray();
            var steque = new Steque<int>();
            foreach (var v in expectedValues)
            {
                steque.Enqueue(v);
            }
            foreach (var expectedValue in expectedValues.Reverse())
            {
                var actualValue = steque.Pop();
                Assert.AreEqual(expectedValue, actualValue);
            }
            steque.Trace();
        }

        [TestMethod]
        public void Steque_Push_YieldsExpectedValues()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(10).ToArray();
            var steque = new Steque<int>();
            foreach (var v in expectedValues)
            {
                steque.Push(v);
            }
            var i = 0;
            foreach (var actualValue in steque)
            {
                Assert.AreEqual(expectedValues[i], actualValue);
                i++;
            }
            steque.Trace();
        }
    }
}
