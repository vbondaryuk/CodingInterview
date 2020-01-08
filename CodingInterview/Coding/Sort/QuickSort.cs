using System.Collections.Generic;
using CodingInterview.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Sort
{
    [TestClass]
    public class QuickSortTest
    {
        [TestMethod]
        public void TestAsc()
        {
            int[] expected = { 5, 6, 7, 11, 12, 13 };

            int[] arr = { 12, 11, 13, 5, 6, 7 };

            var orderSettings = new OrderSettings<int, int>(x => x);
            var mergeSort = new QuickSort();
            mergeSort.Sort(arr, orderSettings);

            CollectionAssert.AreEqual(expected, arr);
        }

        [TestMethod]
        public void TestDesc()
        {
            int[] expected = { 13, 12, 11, 7, 6, 5 };

            int[] arr = { 12, 11, 13, 5, 6, 7 };

            var orderSettings = new OrderSettings<int, int>(OrderDirection.Desc, x => x);
            var mergeSort = new QuickSort();
            mergeSort.Sort(arr, orderSettings);

            CollectionAssert.AreEqual(expected, arr);
        }
    }
    public class QuickSort
    {
        public void Sort<TEntity, T>(IList<TEntity> list, OrderSettings<TEntity, T> orderSettings)
        {
            if (list == null || list.Count < 2)
                return;

            Sort(list, 0, list.Count - 1, orderSettings);
        }

        private void Sort<TEntity, T>(IList<TEntity> list, int from, int to, OrderSettings<TEntity, T> orderSettings)
        {
            if (from >= to)
                return;

            int partition = GetPartition(list, from, to, orderSettings);
            Sort(list, from, partition - 1, orderSettings);
            Sort(list, partition + 1, to, orderSettings);
        }

        private int GetPartition<TEntity, T>(IList<TEntity> list, int from, int to, OrderSettings<TEntity, T> orderSettings)
        {
            TEntity pivot = list[to];

            int lowestIndex = from - 1;
            for (int i = from; i < to; i++)
            {
                if (orderSettings.Direction == OrderDirection.Asc)
                {
                    if (orderSettings.Compare(pivot, list[i]) > 0)
                    {
                        lowestIndex++;
                        list.Swap(lowestIndex, i);
                    }
                }
                else
                {
                    if (orderSettings.Compare(pivot, list[i]) < 0)
                    {
                        lowestIndex++;
                        list.Swap(lowestIndex, i);
                    }
                }
            }

            lowestIndex++;
            list.Swap(lowestIndex, to);

            return lowestIndex;
        }
    }
}