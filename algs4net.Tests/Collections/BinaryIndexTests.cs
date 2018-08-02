using algs4net.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;

namespace algs4net.Tests.Collections
{
    [TestClass]
    public class BinaryIndexTests
    {
        private static int SET_SIZE = 25000; // TODO: relo/share all 'index' impls

        [TestMethod]
        public void BinaryIndex_Ceil_IsPredictableWithSingleItem()
        {
            var index = new BinaryIndex<int, int>();
            index[0x4d] = 0x4d;
            Assert.AreEqual(0x4d, index.Ceil(int.MinValue));
            Assert.AreEqual(0x4d, index.Ceil(0));
            Assert.AreEqual(0x4d, index.Ceil(int.MaxValue));
            index.Trace();
        }

        [TestMethod]
        public void BinaryIndex_Ceil_YieldsExpectedValue()
        {
            var fib = Generators.IntegralNumberGenerator
                .YieldFibonacciSeries(9)
                .ToArray();
            var index = new BinaryIndex<long, long>();
            foreach (var v in fib)
            {
                index[v] = v;
            }
            Assert.AreEqual(8, index.Ceil(7));
            Assert.AreEqual(8, index.Ceil(8));
            Assert.AreEqual(13, index.Ceil(9));
            index.Trace();
        }

        [TestMethod]
        public void BinaryIndex_Floor_IsPredictableWithSingleItem()
        {
            var index = new BinaryIndex<int, int>();
            index[0x4d] = 0x4d;
            Assert.AreEqual(0x4d, index.Floor(int.MinValue));
            Assert.AreEqual(0x4d, index.Floor(0));
            Assert.AreEqual(0x4d, index.Floor(int.MaxValue));
            index.Trace();
        }

        [TestMethod]
        public void BinaryIndex_Floor_YieldsExpectedValue()
        {
            var fib = Generators.IntegralNumberGenerator
                .YieldFibonacciSeries(9)
                .ToArray();
            var index = new BinaryIndex<long, long>();
            foreach (var v in fib)
            {
                index[v] = v;
            }
            Assert.AreEqual(8, index.Floor(9));
            Assert.AreEqual(8, index.Floor(8));
            Assert.AreEqual(5, index.Floor(7));
            index.Trace();
        }

        [TestMethod]
        public void BinaryIndex_GetEnumerator_YieldsOrderedSet()
        {
            var expectedKeys = Generators.IntegralNumberGenerator
                .YieldPredictableSeries(SET_SIZE)
                .ToArray();
            var index = new BinaryIndex<int, int>();
            foreach (var key in expectedKeys)
            {
                index[key] = key ^ 0x4d;
            }
            expectedKeys = expectedKeys
                .Distinct()
                .OrderBy(e => e)
                .ToArray();
            Assert.AreEqual(expectedKeys.Length, index.Count);
            int i = 0;
            foreach (var actualKey in index)
            {
                Assert.AreEqual(expectedKeys[i], actualKey);
                i++;
            }
            Assert.AreEqual(expectedKeys.Length, i);
            index.Trace();
        }

        [TestMethod]
        public void BinaryIndex_indexer_CanUpdateExistingEntry()
        {
            var expectedKeys = Generators.IntegralNumberGenerator
                .YieldPredictableSeries(SET_SIZE / 2)
                .ToArray();
            expectedKeys = expectedKeys.Concat(expectedKeys).ToArray();

            var index = new BinaryIndex<int, int>();
            foreach (var expectedKey in expectedKeys)
            {
                index[expectedKey] = expectedKey ^ 0x4d;
            }
            Assert.AreEqual(SET_SIZE / 2, index.Count);

            expectedKeys = expectedKeys.OrderBy(e => e).Distinct().ToArray();
            int i = 0;
            foreach (var actualKey in index)
            {
                Assert.AreEqual(expectedKeys[i], actualKey);
                Assert.AreEqual(expectedKeys[i] ^ 0x4d, index[actualKey]);
                i++;
            }
            index.Trace();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BinaryIndex_indexer_ThrowsOnMiss()
        {
            IIndex<Guid, Guid> index = new BinaryIndex<Guid, Guid>();
            var result = index[Guid.NewGuid()];
        }

        [TestMethod]
        public void BinaryIndex_IndexOf_YieldsExpectedIndexes()
        {
            var expectedKeys = Generators.IntegralNumberGenerator
                .YieldPredictableSeries(SET_SIZE)
                .ToArray();
            var index = new BinaryIndex<int, int>();
            foreach (var v in expectedKeys)
            {
                index[v] = v;
            }
            expectedKeys = expectedKeys.OrderBy(e => e).Distinct().ToArray();
            for (int i = 0; i < expectedKeys.Length; i++)
            {
                var expectedKey = expectedKeys[i];
                Assert.AreEqual(i, index.IndexOf(expectedKey));
            }
            index.Trace();
        }

        [TestMethod]
        public void BinaryIndex_Keys_YieldsOrderedSet()
        {
            var expectedKeys = Generators.IntegralNumberGenerator
                .YieldPredictableSeries(SET_SIZE)
                .ToArray();
            var index = new BinaryIndex<int, int>();
            foreach (var key in expectedKeys)
            {
                index[key] = key ^ 0x4d;
            }
            expectedKeys = expectedKeys
                .Distinct()
                .OrderBy(e => e)
                .ToArray();
            Assert.AreEqual(expectedKeys.Length, index.Count);
            int i = 0;
            foreach (var actualKey in index)
            {
                Assert.AreEqual(expectedKeys[i], actualKey);
                var actualValue = index[actualKey];
                var expectedValue = actualKey ^ 0x4d;
                Assert.AreEqual(expectedValue, actualValue);
                i++;
            }
            index.Trace();
        }

        [TestMethod]
        public void BinaryIndex_Max_YieldsExpectedValue()
        {
            var expectedKeys = Generators.IntegralNumberGenerator
                .YieldPredictableSeries(SET_SIZE)
                .ToArray();
            var index = new BinaryIndex<int, int>();
            foreach (var key in expectedKeys)
            {
                index[key] = key;
            }
            Assert.AreEqual(expectedKeys.Max(), index.Max());
            index.Trace();
        }

        [TestMethod]
        public void BinaryIndex_Min_YieldsExpectedValue()
        {
            var expectedKeys = Generators.IntegralNumberGenerator
                .YieldPredictableSeries(SET_SIZE)
                .ToArray();
            var index = new BinaryIndex<int, int>();
            foreach (var key in expectedKeys)
            {
                index[key] = key;
            }
            Assert.AreEqual(expectedKeys.Min(), index.Min());
            index.Trace();
        }

        [TestMethod]
        public void BinaryIndex_Remove_DoesNotThrowOnMiss()
        {
            var index = new BinaryIndex<Guid, Guid>();
            Assert.IsFalse(index.TryRemove(Guid.NewGuid(), out Guid discard));
            index.Trace();
        }

        [TestMethod]
        public void BinaryIndex_TryGet_YieldsExpectedValues()
        {
            var expectedKeys = Generators.IntegralNumberGenerator
                .YieldPredictableSeries(SET_SIZE)
                .ToArray();
            var index = new BinaryIndex<int, int>();
            foreach (var key in expectedKeys)
            {
                index[key] = key ^ 0x4d;
            }
            expectedKeys = expectedKeys
                .Distinct()
                .OrderBy(e => e)
                .ToArray();
            Assert.AreEqual(expectedKeys.Length, index.Count);
            var i = 0;
            foreach (var expectedKey in expectedKeys)
            {
                Assert.AreEqual(expectedKeys[i], expectedKey);
                Assert.IsTrue(index.TryGet(expectedKey, out int actualValue));
                Assert.AreEqual(expectedKey ^ 0x4d, actualValue);
                i++;
            }
            index.Trace();
        }

        [TestMethod]
        public void BinaryIndex_TryGetByIndex_YieldsExpectedKeys()
        {
            var expectedKeys = Generators.IntegralNumberGenerator
                .YieldPredictableSeries(SET_SIZE)
                .ToArray();
            var index = new BinaryIndex<int, int>();
            foreach (var v in expectedKeys)
            {
                index[v] = v;
            }
            expectedKeys = expectedKeys.OrderBy(e => e).Distinct().ToArray();
            for (int i = 0; i < expectedKeys.Length; i++)
            {
                var expectedKey = expectedKeys[i];
                Assert.IsTrue(index.TryGetByIndex(i, out int actualKey));
                Assert.AreEqual(expectedKey, actualKey);
            }
            index.Trace();
        }

        [TestMethod]
        public void BinaryIndex_TryRemove_YieldsExpectedValues()
        {
            var expectedKeys = Generators.IntegralNumberGenerator
                .YieldPredictableSeries(SET_SIZE)
                .ToArray();
            var index = new BinaryIndex<int, int>();
            foreach (var key in expectedKeys)
            {
                index[key] = key ^ 0x4d;
            }

            expectedKeys = expectedKeys
                .OrderBy(e => e)
                .Distinct()
                .ToArray();
            Assert.AreEqual(expectedKeys.Length, index.Count);

            // NOTE: this test only removes half the keys, this is done to
            // perform consistency checks:
            //
            // 1. to ensure remaining keys are returned as expected
            // 2. to ensure removed keys are NOT returned
            // 3. to ensure order is maintained
            // 4. to verify ranks of remaining keys.
            var removals = new LinkedList<int>();
            var i = 0;
            foreach (var expectedKey in expectedKeys)
            {
                if ((i & 1) == 1)
                {
                    Assert.AreEqual(expectedKeys[i], expectedKey);
                    var expectedValue = expectedKeys[i] ^ 0x4d;
                    Assert.IsTrue(index.TryRemove(expectedKey, out int actualValue));
                    Assert.AreEqual(expectedValue, actualValue);
                    removals.Add(expectedKey);
                }
                i++;
            }

            expectedKeys = expectedKeys.Except(removals).ToArray();
            foreach (var expectedKey in expectedKeys)
            {
                Assert.IsTrue(index.TryGet(expectedKey, out int actualValue));
            }
            Assert.AreEqual(expectedKeys.Length, index.Count());
            index.Trace();
        }
    }
}
