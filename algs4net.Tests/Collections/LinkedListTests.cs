﻿using algs4net.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;

namespace algs4net.Tests.Collections
{
    [TestClass]
    public class LinkedListTests
    {
        private static int SET_SIZE = 25000; // TODO: relo/share all 'index' impls

        [TestMethod]
        public void LinkedList_Add_PreservesOrder()
        {
            var expectedItems = Generators.IntegralNumberGenerator
                .YieldPredictableSeries(SET_SIZE)
                .ToArray();
            var list = new LinkedList<int>();
            foreach (var item in expectedItems)
            {
                list.Add(item);
            }
            Assert.AreEqual(expectedItems.Length, list.Count);
            var i = 0;
            foreach (var item in list)
            {
                Assert.AreEqual(expectedItems[i], item);
                i++;
            }
            Assert.AreEqual(expectedItems.Length, i);
            list.Trace();
        }

        [TestMethod]
        public void LinkedList_Contains_YieldsExpectedResult()
        {
            var expectedItems = Generators.IntegralNumberGenerator
                .YieldPredictableSeries(SET_SIZE)
                .ToArray();
            var list = new LinkedList<int>(expectedItems);
            foreach (var v in expectedItems)
            {
                Assert.IsTrue(list.Contains(v));
            }
            list.Trace();
        }


        [TestMethod]
        public void LinkedList_AddRange_PreservesOrder()
        {
            var expectedItems = Generators.IntegralNumberGenerator
                .YieldPredictableSeries(SET_SIZE)
                .ToArray();
            var list = new LinkedList<int>();
            list.AddRange(expectedItems);
            Assert.AreEqual(expectedItems.Length, list.Count);
            var i = 0;
            foreach (var item in list)
            {
                Assert.AreEqual(expectedItems[i], item);
                i++;
            }
            Assert.AreEqual(expectedItems.Length, i);
            list.Trace();
        }

        [TestMethod]
        public void LinkedList_ctor_CanConstructEmptyList()
        {
            var list = new LinkedList<int>();
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void LinkedList_ctor_CanConstructPopulatedList()
        {
            var expected = Generators.IntegralNumberGenerator
                .YieldPredictableSeries(SET_SIZE)
                .ToArray();
            var list = new LinkedList<int>(expected);
            Assert.AreEqual(expected.Length, list.Count);
            list.Trace();
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void LinkedList_GetEnumerator_ThrowsWhenCollectionModified()
        {
            var expected = Generators.IntegralNumberGenerator
                .YieldPredictableSeries(SET_SIZE)
                .ToArray();
            var list = new LinkedList<int>(expected);
            var i = 0;
            foreach (var item in list)
            {
                Assert.AreEqual(expected[i], item);
                i++;
                list.Remove(expected[expected.Length / 2]);
            }
            Assert.AreEqual(expected.Length, i);
            list.Trace();
        }

        [TestMethod]
        public void LinkedList_GetEnumerator_YieldsValues()
        {
            var expected = Generators.IntegralNumberGenerator
                .YieldPredictableSeries(SET_SIZE)
                .ToArray();
            var list = new LinkedList<int>(expected);
            var i = 0;
            foreach (var item in list)
            {
                Assert.AreEqual(expected[i], item);
                i++;
            }
            Assert.AreEqual(expected.Length, i);
            list.Trace();
        }

        [TestMethod]
        public void LinkedList_InsertAfter_InsertsAfterValueSpecified()
        {
            var expectedValues = new[] { 0, 1, 2, 3, 4, 5, 6 };
            var list = new LinkedList<int>();

            // NOTE: tests tail insertions via `3`
            foreach (var value in new[] { 1, 3, 5 })
            {
                list.InsertAfter(value, value);
            }

            // NOTE: tests head replacement via `0`
            foreach (var value in new[] { 0, 2, 4, 6 })
            {
                list.InsertAfter(value, value);
            }

            Assert.AreEqual(expectedValues.Length, list.Count);

            // test for ordered list
            foreach (var expectedValue in expectedValues)
            {
                Assert.IsTrue(list.RemoveAt(0, out int actualValue));
                Assert.AreEqual(expectedValue, actualValue);
            }
            list.Trace();
        }

        [TestMethod]
        public void LinkedList_InsertAt_InsertsToAbsolutePosition()
        {
            var expected = new[] { 1, 2, 3 };
            var list = new LinkedList<int>(expected);
            list.InsertAt(5, 1);
            Assert.AreEqual(4, list.Count);
            Assert.IsTrue(list.RemoveAt(0));
            Assert.AreEqual(5, list.First());
            list.Trace();
        }

        [TestMethod]
        public void LinkedList_InsertBefore_InsertsBeforeValueSpecified()
        {
            var expectedValues = new[] { 0, 1, 2, 3, 4, 5, 6 };
            var list = new LinkedList<int>();

            // NOTE: tests tail insertions via `3`
            foreach (var value in new[] { 1, 3, 5 })
            {
                list.InsertBefore(value, value);
            }

            // NOTE: tests head replacement via `0`
            foreach (var value in new[] { 0, 2, 4, 6 })
            {
                list.InsertBefore(value, value);
            }

            Assert.AreEqual(expectedValues.Length, list.Count);

            // test for ordered list
            foreach (var expectedValue in expectedValues)
            {
                Assert.IsTrue(list.RemoveAt(0, out int actualValue));
                Assert.AreEqual(expectedValue, actualValue);
            }
            list.Trace();
        }

        [TestMethod]
        public void LinkedList_Merge_BothEmptyContainers()
        {
            var list1 = new LinkedList<int>();
            var list2 = new LinkedList<int>();
            list1.Merge(list2);
            Assert.AreEqual(0, list1.Count);
            list1.Trace();
        }

        [TestMethod]
        public void LinkedList_Merge_FromEmptyContainer()
        {
            var expected = Generators.IntegralNumberGenerator
                .YieldPredictableSeries(SET_SIZE)
                .ToArray();
            var list1 = new LinkedList<int>(expected);
            var list2 = new LinkedList<int>();
            list1.Merge(list2);
            var i = 0;
            foreach (var item in list1)
            {
                Assert.AreEqual(expected[i], item);
                i++;
            }
            Assert.AreEqual(expected.Length, i);
            list1.Trace();
            list2.Trace();
        }

        [TestMethod]
        public void LinkedList_Merge_PreservesOrder()
        {
            var expected1 = new[] { 1, 3, 5, 7, 9, 11 };
            var expected2 = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
            var list1 = new LinkedList<int>(expected1);
            var list2 = new LinkedList<int>(expected2);
            list1.Merge(list2);
            var expected = expected1.Concat(expected2).OrderBy(e => e).ToArray();
            var i = 0;
            foreach (var item in list1)
            {
                Assert.AreEqual(expected[i], item);
                i++;
            }
            list1.Trace();
            list2.Trace();
        }

        [TestMethod]
        public void LinkedList_Merge_ToEmptyContainer()
        {
            var expected = Generators.IntegralNumberGenerator
                .YieldPredictableSeries(SET_SIZE)
                .ToArray();
            var list1 = new LinkedList<int>();
            var list2 = new LinkedList<int>(expected);
            list1.Merge(list2);
            var i = 0;
            foreach (var item in list1)
            {
                Assert.AreEqual(expected[i], item);
                i++;
            }
            Assert.AreEqual(expected.Length, i);
            Assert.AreEqual(expected.Length, list1.Count);
            list1.Trace();
            list2.Trace();
        }

        [TestMethod]
        public void LinkedList_Remove_CanRemoveHeadOrTailSameEffect()
        {
            var expected = new[] { 1, 2, 3 };
            var list = new LinkedList<int>(expected);
            Assert.AreEqual(3, list.Count);
            Assert.IsTrue(list.Remove(1));
            Assert.IsTrue(list.Remove(3));
            Assert.AreEqual(1, list.Count);
            foreach (var item in list)
            {
                Assert.AreEqual(2, item);
            }
            list.Trace();
        }

        [TestMethod]
        public void LinkedList_Remove_MaintainsOrder()
        {
            var expected = new[] { 1, 2, 3 };
            var list = new LinkedList<int>(expected);
            list.Remove(2);
            var i = 1;
            foreach (var item in list)
            {
                Assert.AreEqual(i, item);
                i += 2;
            }
            list.Trace();
        }

        [TestMethod]
        public void LinkedList_RemoveAt_CanRemoveHeadOrTailSameEffect()
        {
            var expected = new[] { 1, 2, 3 };
            var list = new LinkedList<int>(expected);
            Assert.AreEqual(3, list.Count);
            Assert.IsTrue(list.RemoveAt(2));
            Assert.IsTrue(list.RemoveAt(0));
            Assert.AreEqual(1, list.Count);
            foreach (var item in list)
            {
                Assert.AreEqual(2, item);
            }
            list.Trace();
        }

        [TestMethod]
        public void LinkedList_RemoveAt_MaintainsOrder()
        {
            var expected = new[] { 1, 2, 3 };
            var list = new LinkedList<int>(expected);
            list.RemoveAt(1);
            var i = 1;
            foreach (var item in list)
            {
                Assert.AreEqual(i, item);
                i += 2;
            }
            list.Trace();
        }
    }
}
