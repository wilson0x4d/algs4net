using algs4net.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;

namespace algs4net.Tests.Collections
{
    [TestClass]
    public class SequentialIndexTests
    {
        [TestMethod]
        public void SequentialIndex_Add_CanUpdateExistingEntry()
        {
            var guid = Guid.NewGuid();
            var expectedSymbols = new[]
            {
                new { Key = guid, Value = Guid.NewGuid() },
                new { Key = guid, Value = Guid.NewGuid() }
            };
            IIndex<Guid, Guid> index = new SequentialIndex<Guid, Guid>();
            foreach (var expectedSymbol in expectedSymbols)
            {
                index[expectedSymbol.Key] = expectedSymbol.Value;
            }
            Assert.AreEqual(1, index.Count());
            index.Trace();
        }

        [TestMethod]
        public void SequentialIndex_GetEnumerator_YieldsExpectedSet()
        {
            var expectedKeys = Generators.IntegralNumberGenerator
                .YieldPredictableSeries(10000)
                .ToArray();
            IIndex<int, Guid> index = new SequentialIndex<int, Guid>();
            foreach (var key in expectedKeys)
            {
                index[key] = Guid.NewGuid();
            }
            expectedKeys = expectedKeys
                .Distinct()
                .OrderBy(e => e)
                .ToArray();
            Assert.AreEqual(expectedKeys.Length, index.Count());
            int i = 0;
            foreach (var actualKey in index)
            {
                Assert.AreEqual(expectedKeys[i], actualKey);
                i++;
            }
            index.Trace();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void SequentialIndex_indexer_ThrowsOnMiss()
        {
            IIndex<Guid, Guid> index = new SequentialIndex<Guid, Guid>();
            var result = index[Guid.NewGuid()];
        }

        [TestMethod]
        public void SequentialIndex_indexer_YieldsExpectedSet()
        {
            var expectedKeys = Generators.IntegralNumberGenerator
                .YieldPredictableSeries(10000)
                .ToArray();
            IIndex<int, int> index = new SequentialIndex<int, int>();
            foreach (var key in expectedKeys)
            {
                index[key] = key ^ 0x4d;
            }
            expectedKeys = expectedKeys
                .Distinct()
                .ToArray();
            Assert.AreEqual(expectedKeys.Length, index.Count());
            foreach (var expectedKey in expectedKeys)
            {
                var actualValue = index[expectedKey];
                Assert.AreEqual(expectedKey ^ 0x4d, actualValue);
            }
            index.Trace();
        }

        [TestMethod]
        public void SequentialIndex_Keys_YieldsExpectedSet()
        {
            var expectedKeys = Generators.IntegralNumberGenerator
                .YieldPredictableSeries(10000)
                .ToArray();
            IIndex<int, Guid> index = new SequentialIndex<int, Guid>();
            foreach (var key in expectedKeys)
            {
                index[key] = Guid.NewGuid();
            }
            expectedKeys = expectedKeys
                .Distinct()
                .OrderBy(e => e)
                .ToArray();
            Assert.AreEqual(expectedKeys.Length, index.Count());
            int i = 0;
            foreach (var actualKey in index)
            {
                Assert.AreEqual(expectedKeys[i], actualKey);
                i++;
            }
            index.Trace();
        }

        [TestMethod]
        public void SequentialIndex_Remove_DoesNotThrowOnMiss()
        {
            IIndex<Guid, Guid> index = new SequentialIndex<Guid, Guid>();
            Assert.IsFalse(index.TryRemove(Guid.NewGuid(), out Guid discard));
            index.Trace();
        }

        [TestMethod]
        public void SequentialIndex_Values_YieldsExpectedSet()
        {
            var expectedKeys = Generators.IntegralNumberGenerator
                .YieldPredictableSeries(10000)
                .ToArray();
            IIndex<int, int> index = new SequentialIndex<int, int>();
            foreach (var key in expectedKeys)
            {
                index[key] = key ^ 0x4d;
            }
            expectedKeys = expectedKeys
                .Distinct()
                .OrderBy(e => e)
                .ToArray();
            Assert.AreEqual(expectedKeys.Length, index.Count());
            int i = 0;
            foreach (var expectedKey in expectedKeys)
            {
                var actualValue = index[expectedKey];
                Assert.AreEqual(expectedKey ^ 0x4d, actualValue);
                i++;
            }
            index.Trace();
        }
    }
}
