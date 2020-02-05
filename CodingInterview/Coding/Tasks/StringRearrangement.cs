using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class StringRearrangementTest
    {
        [TestMethod]
        [DataRow(new[] { "aba", "bbb", "bab" }, false)]
        [DataRow(new[] { "ab", "bb", "aa" }, true)]
        [DataRow(new[] { "bb", "ab", "aa" }, true)]
        public void BackTrackingTest(string[] array, bool expected)
        {
            var result = RearrangeBackTracking(array);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(new[] { "aba", "bbb", "bab" }, false)]
        [DataRow(new[] { "ab", "bb", "aa" }, true)]
        [DataRow(new[] { "bb", "ab", "aa" }, true)]
        public void AdjacencyListTest(string[] array, bool expected)
        {
            var result = RearrangeAdjacencyList(array);

            Assert.AreEqual(expected, result);
        }

        #region explanation
        /*
            Given an array of equal-length strings, you'd like to know if it's possible to rearrange the order of the elements in such a way that each consecutive pair of strings 
            differ by exactly one character. Return true if it's possible, and false if not.

            Note: You're only rearranging the order of the strings, not the order of the letters within the strings!

            Example

             For inputArray = ["aba", "bbb", "bab"], the output should be
             stringsRearrangement(inputArray) = false.

             There are 6 possible arrangements for these strings:
	             ["aba", "bbb", "bab"]
	             ["aba", "bab", "bbb"]
	             ["bbb", "aba", "bab"]
	             ["bbb", "bab", "aba"]
	             ["bab", "bbb", "aba"]
	             ["bab", "aba", "bbb"]

             None of these satisfy the condition of consecutive strings differing by 1 character, so the answer is false.

             For inputArray = ["ab", "bb", "aa"], the output should be
             stringsRearrangement(inputArray) = true.

             It's possible to arrange these strings in a way that each consecutive pair of strings differ by 1 character (eg: "aa", "ab", "bb" or "bb", "ab", "aa"), so return true.
        */
        # endregion
        private static bool RearrangeBackTracking(string[] array)
        {
            bool[] visited = new bool[array.Length];
            bool IsChainExist(int previousIndex, int count, bool isFirst = false)
            {
                if (count == array.Length)
                    return true;
                for (int i = 0; i < array.Length; i++)
                {
                    if (isFirst || !visited[i] && IsAdjacency(array[i], array[previousIndex]))
                    {
                        visited[i] = true;
                        count++;
                        if (IsChainExist(i, count))
                            return true;
                        count--;
                        visited[i] = false;
                    }
                }

                return false;
            }

            return IsChainExist(0, 0, true);
        }

        private static bool RearrangeAdjacencyList(string[] array)
        {
            var agjList = Create(array);

            bool IsChainExist(int startIndex)
            {
                var set = new HashSet<int>();
                var stack = new Stack<int>(array.Length);
                stack.Push(startIndex);
                while (stack.Count > 0)
                {
                    var vertex = stack.Pop();
                    if (set.Contains(vertex))
                        continue;
                    set.Add(vertex);

                    int added = 0;
                    foreach (var index in agjList[vertex])
                    {
                        if(set.Contains(index))
                            continue;
                        added++;
                        stack.Push(index);
                    }

                    if (added == 0 && set.Count == array.Length)
                        return true;
                }

                return false;
            }


            for (int i = 0; i < array.Length; i++)
                if (IsChainExist(i))
                    return true;

            return false;
        }

        private static List<List<int>> Create(string[] array)
        {
            var list = new List<List<int>>(array.Length);
            list.AddRange(Enumerable.Range(0, array.Length).Select(_ => new List<int>()));
            for (int i = 0; i < array.Length; i++)
            for (int j = i + 1; j < array.Length; j++)
            {
                if (IsAdjacency(array[i], array[j]))
                {
                    list[i].Add(j);
                    list[j].Add(i);
                }
            }

            return list;
        }

        private static bool IsAdjacency(string str1, string str2)
        {
            if (str1 == str2 || str1.Length != str2.Length)
                return false;

            int diff = 0;
            for (int i = 0; i < str1.Length; i++)
            {
                if (str1[i] != str2[i])
                    diff++;
                if (diff > 1)
                    return false;
            }

            return diff == 1;
        }
    }
}
