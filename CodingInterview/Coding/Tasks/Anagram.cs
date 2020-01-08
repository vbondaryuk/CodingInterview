using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class AnagramTest
    {
        [TestMethod]
        public void Test()
        {
            const int expected = 2;
            var a = "showman";
            var b = "woman";

            var anagram = new Anagram();
            var result = anagram.MakeAnagram(a, b);

            Assert.AreEqual(expected, result);
        }
    }

    public class Anagram
    {
        public int MakeAnagram(string a, string b)
        {
            int number = 0;//keep the number of removed chars
            var array = new int[26];

            //populate an array of number of characters
            //c = 's' => 115
            //'a' => 97
            //c - 'a' = 21
            //array[18]++;
            foreach (char c in a)
                array[c - 'a']++;

            //remove 
            foreach (char c in b)
                array[c - 'a']--;

            //if any number in array more than 0 than increase number
            foreach (int i in array)
                number += Math.Abs(i);

            return number;
        }
    }
}