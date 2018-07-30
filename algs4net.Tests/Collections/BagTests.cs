using algs4net.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace algs4net.Tests.Collections
{
    [TestClass]
    public class BagTests
    {
        [TestMethod]
        public void Bag_Add_IncreasesItemCount()
        {
            var bag = new Bag<int>();
            foreach (var v in Generators.IntegralNumberGenerator.YieldPredictableSeries(10))
            {
                bag.Add(v);
            }
            Assert.AreEqual(10, bag.Count);
        }

        [TestMethod]
        public void Bag_GetEnumerator_DoesYieldItems()
        {
            var expectedValues = new LinkedList<int>(
                Generators.IntegralNumberGenerator.YieldPredictableSeries(10));
            var bag = new Bag<int>();
            foreach (var v in expectedValues)
            {
                bag.Add(v);
            }
            Assert.AreEqual(10, bag.Count);
            foreach (var actualValue in bag)
            {
                Assert.IsTrue(expectedValues.RemoveAt(0, out int expectedValue));
                Assert.AreEqual(expectedValue, actualValue);
            }
            Assert.AreEqual(0, expectedValues.Count);
        }
    }
}
