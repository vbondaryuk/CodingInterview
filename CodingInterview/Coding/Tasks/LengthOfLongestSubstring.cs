using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class LengthOfLongestSubstringTest
    {
        [TestMethod]
        [DataRow("tammag", 3, DisplayName = "abcabc")]
        [DataRow("tmsmfdut", 6, DisplayName = "abcabc")]
        public void Test(string s, int expected)
        {
            var substringFinder = new SubstringFinder();
            var result = substringFinder.LengthOfLongestSubstring(s);

            Assert.AreEqual(expected, result);
        }
    }

    public class SubstringFinder
    {
        public int LengthOfLongestSubstring(string s)
        {
            if (string.IsNullOrEmpty(s))
                return 0;
            
            int max = 0;
            Dictionary<char, int> hash = new Dictionary<char, int>();
            for (int right = 0, left = 0; right < s.Length; right++)
            {
                if (hash.TryGetValue(s[right], out int index))
                {
                    //here keep the start window. for example bellow, after find M on index 1 move window to this index 
                    //check for max in case where char before window(tammag - here second A after second M so left open window from first M+1)
                    //tmsqmfdt
                    // ↑  ↑
                    // L  R
                    left = Math.Max(left, index + 1);  
                }

                hash[s[right]] = right;
                max = Math.Max(max, right - left + 1);
            }

            return max;
        }
    }
}