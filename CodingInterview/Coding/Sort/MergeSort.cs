using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Sort
{
    [TestClass]
    public class MergeSortTest
    {
        [TestMethod]
        public void TestAsc()
        {
            int[] expected = { 5, 6, 7, 11, 12, 13 };

            int[] arr = { 12, 11, 13, 5, 6, 7 };

            var orderSettings = new OrderSettings<int, int>(x => x);
            var mergeSort = new MergeSort();
            mergeSort.Sort(arr, orderSettings);

            CollectionAssert.AreEqual(expected, arr);
        }

        [TestMethod]
        public void TestDesc()
        {
            int[] expected = { 13, 12, 11, 7, 6, 5 };

            int[] arr = { 12, 11, 13, 5, 6, 7 };

            var orderSettings = new OrderSettings<int, int>(OrderDirection.Desc, x => x);
            var mergeSort = new MergeSort();
            mergeSort.Sort(arr, orderSettings);

            CollectionAssert.AreEqual(expected, arr);
        }
    }

    public class MergeSort
    {
        public void Sort<TEntity, T>(IList<TEntity> list, OrderSettings<TEntity, T> orderSettings)
        {
            if (list == null || list.Count < 2)
                return;

            Sort(list, 0, list.Count - 1, orderSettings);
        }

        private static void Sort<TEntity, T>(IList<TEntity> list, int from, int to, OrderSettings<TEntity, T> orderSettings)
        {
            if (from >= to)
                return;

            int middle = (to + from) / 2;
            Sort(list, from, middle, orderSettings);
            Sort(list, middle + 1, to, orderSettings);
            Sort(list, from, middle, to, orderSettings);
        }

        private static void SortIterative<TEntity, T>(IList<TEntity> list, int from, int to, OrderSettings<TEntity, T> orderSettings)
        {
            for (int i = 1; i < to; i *= 2)
            {
                for (int left = 0; left < to; left += 2 * i)
                {
                    int middle = i + left - 1;
                    int right = Math.Min(left + 2 * i - 1, to);
                    Sort(list, left, middle, right, orderSettings);
                }
            }
        }

        private static void Sort<TEntity, T>(IList<TEntity> list, int from, int middle, int to, OrderSettings<TEntity, T> orderSettings)
        {
            int firstSize = middle - from + 1;
            int secondSize = to - middle;

            var firstArray = new TEntity[firstSize];
            var secondArray = new TEntity[secondSize];

            for (int i = 0; i < firstSize; i++)
                firstArray[i] = list[from + i];

            for (int i = 0; i < secondSize; i++)
                secondArray[i] = list[middle + i + 1];

            int firstIndex = 0, secondIndex = 0;

            int resultIndex = from;
            while (firstIndex < firstSize && secondIndex < secondSize)
            {
                if (orderSettings.Direction == OrderDirection.Asc)
                {
                    if (orderSettings.Compare(firstArray[firstIndex], secondArray[secondIndex]) <= 0)
                    {
                        list[resultIndex] = firstArray[firstIndex];
                        firstIndex++;
                    }
                    else
                    {
                        list[resultIndex] = secondArray[secondIndex];
                        secondIndex++;
                    }
                }
                else
                {
                    if (orderSettings.Compare(firstArray[firstIndex], secondArray[secondIndex]) >= 0)
                    {
                        list[resultIndex] = firstArray[firstIndex];
                        firstIndex++;
                    }
                    else
                    {
                        list[resultIndex] = secondArray[secondIndex];
                        secondIndex++;
                    }
                }

                resultIndex++;
            }

            while (firstIndex < firstSize)
                list[resultIndex++] = firstArray[firstIndex++];

            while (secondIndex < secondSize)
                list[resultIndex++] = secondArray[secondIndex++];
        }
    }
}