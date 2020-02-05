using System;
using System.Collections.Generic;
using System.Text;
using CodingInterview.Coding.Stucts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class RearrangeCharacterInStringTest
    {
        [TestMethod]
        [DataRow("aaabc", "abaca")]
        [DataRow("aaabb", "ababa")]
        [DataRow("aa", "Not Possible")]
        [DataRow("aaaabc", "Not Possible")]
        public void Test(string str, string expected)
        {
            var result = RearrangeCharacterInString(str);

            Assert.AreEqual(expected, result);
        }

        #region explanation
        /*
         Given a string with repeated characters, the task is to rearrange characters in a string so that no two adjacent characters are same.
         Note : It may be assumed that the string has only lowercase English alphabets.
            Examples:
            Input: aaabc 
            Output: abaca 

            Input: aaabb
            Output: ababa 

            Input: aa 
            Output: Not Possible

            Input: aaaabc 
            Output: Not Possible
         */
        #endregion
        private static string RearrangeCharacterInString(string str)
        {
            var dict = new Dictionary<char, int>();
            foreach (var ch in str)
            {
                if (!dict.ContainsKey(ch))
                    dict[ch] = 0;
                dict[ch]++;
            }

            var queue = new PriorityQueue<Tuple<char, int>, int>();
            foreach (var pair in dict)
            {
                queue.Enqueue( Tuple.Create(pair.Key, pair.Value), pair.Value);
            }
            var builder = new StringBuilder();
            var previous = new Tuple<char, int>('\0', -1);
            while (queue.Count() > 0)
            {
                var (pair, cost) = queue.DequeueMax();

                builder.Append(pair.Item1);

                if (previous.Item2 > 0)
                    queue.Enqueue(previous, previous.Item2);

                previous = new Tuple<char, int>(pair.Item1, pair.Item2 - 1);
            }

            return builder.Length == str.Length ? builder.ToString() : "Not Possible";
        }
    }
}
