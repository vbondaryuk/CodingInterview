using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class SlidingWindowTest
    {
        [TestMethod]
        public void Test()
        {
            var expected = new[] {78, 90, 90, 90, 89};
            var result = SlidingWindow(new[] { 12, 1, 78, 90, 57, 89, 56 }, 3);

            CollectionAssert.AreEqual(expected, result);
        }

        //https://www.geeksforgeeks.org/sliding-window-maximum-maximum-of-all-subarrays-of-size-k/
        public static int[] SlidingWindow(int[] arr, int k)
        {
            int length = arr.Length;
            var max = new List<int>();
            var list = new LinkedList<int>();

            list.AddFirst(0);
            int i = 0;
            for (; i < k; i++)
            {
                while (list.Count != 0 && arr[list.Last.Value] < arr[i])
                    list.RemoveLast();

                list.AddLast(i);
            }

            for (; i < length; i++)
            {
                max.Add(arr[list.First.Value]);
                while (list.Count != 0 && list.First.Value < i - k + 1)
                    list.RemoveFirst();
                while (list.Count != 0 && arr[list.Last.Value] < arr[i])
                    list.RemoveLast();
                list.AddLast(i);
            }

            max.Add(arr[list.First.Value]);

            return max.ToArray();
        }
    }
}
