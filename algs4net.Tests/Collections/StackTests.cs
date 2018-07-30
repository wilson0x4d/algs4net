using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace algs4net.Tests.Collections
{
    [TestClass]
    public class StackTests
    {
        [TestMethod]
        public void Stack_Pop_YieldsExpectedValues()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(10).ToArray();
            var lifo = new Stack<int>();
            foreach (var v in expectedValues)
            {
                lifo.Push(v);
            }
            foreach (var expectedValue in expectedValues.Reverse())
            {
                var actualValue = lifo.Pop();
                Assert.AreEqual(expectedValue, actualValue);
            }
            lifo.Trace();
        }

        [TestMethod]
        public void Stack_Push_YieldsExpectedValues()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(10).ToArray();
            var lifo = new Stack<int>();
            foreach (var v in expectedValues)
            {
                lifo.Push(v);
            }
            var i = 0;
            foreach (var actualValue in lifo.Reverse())
            {
                Assert.AreEqual(expectedValues[i], actualValue);
                i++;
            }
            lifo.Trace();
        }
    }
}
