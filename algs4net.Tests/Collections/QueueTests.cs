using algs4net.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;

namespace algs4net.Tests.Collections
{
    [TestClass]
    public class QueueTests
    {
        [TestMethod]
        public void Queue_Dequeue_YieldsExpectedItems()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(10).ToArray();
            var fifo = new Queue<int>();
            foreach (var v in expectedValues)
            {
                fifo.Enqueue(v);
            }
            foreach (var expectedValue in expectedValues)
            {
                var actualValue = fifo.Dequeue();
                Assert.AreEqual(expectedValue, actualValue);
            }
            fifo.Trace();
        }

        [TestMethod]
        public void Queue_Enqueue_YieldsExpectedItems()
        {
            var expectedValues = Generators.IntegralNumberGenerator.YieldPredictableSeries(10).ToArray();
            var fifo = new Queue<int>();
            foreach (var v in expectedValues)
            {
                fifo.Enqueue(v);
            }
            var i = 0;
            foreach (var actualValue in fifo)
            {
                Assert.AreEqual(expectedValues[i], actualValue);
                i++;
            }
            fifo.Trace();
        }
    }
}
