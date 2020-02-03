using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks.DataStructure
{
    [TestClass]
    public class PrefixAndSuffixSearchTest
    {
        [TestMethod]
        [DataRow("", "pop", 0)]
        [DataRow("p", "pop", 0)]
        public void Test(string prefix, string suffix, int expected)
        {
            var wordDict = new WordFilter(new[]{ "pop" });
            var result = wordDict.F(prefix, suffix);

            Assert.AreEqual(expected, result);
        }
    }

    //https://leetcode.com/problems/prefix-and-suffix-search/
    public class WordFilter
    {
        private static readonly char Zero = '\0';
        private TrieNode root = new TrieNode(Zero);

        public WordFilter(string[] words)
        {
            for (var index = 0; index < words.Length; index++)
            {
                var word = words[index];
                AddWord(word, index);
            }
        }

        //time/space O(m) where m is word length
        public void AddWord(string word, int weight)
        {
            if (string.IsNullOrWhiteSpace(word))
                throw new ArgumentNullException(nameof(word));

            TrieNode node = root;
            node.LongestDepth = Math.Max(word.Length, node.LongestDepth);
            for (var index = 0; index < word.Length; index++)
            {
                var ch = word[index];
                node = node.TryGetValue(ch, out var temp) ? temp : node.Put(ch);
                node.LongestDepth = Math.Max(word.Length - 1 - index, node.LongestDepth);
            }

            node.Weight = weight;
            node.IsEnd = true;
        }

        // words[i] has length in range [1, 10].
        // prefix, suffix have lengths in range[0, 10].
        public int F(string prefix, string suffix)
        {
            var commonPart = prefix.Length + suffix.Length - root.LongestDepth;
            if (commonPart > 0)
            {
                var isCommonPartSame =
                    prefix.Substring(prefix.Length - commonPart).Equals(suffix.Substring(0, commonPart));
                if (isCommonPartSame)
                {
                    suffix = suffix.Substring(commonPart);
                }
                else
                {
                    return -1;
                }
            }

            Queue<TrieNode> queue = new Queue<TrieNode>();
            queue.Enqueue(root);
            {
                for (int i = 0; i < prefix.Length; i++)
                {
                    var ch = prefix[i];
                    var stackCount = queue.Count;
                    for (int j = 0; j < stackCount; j++)
                    {
                        var node = queue.Dequeue();
                        if (node.TryGetValue(ch, out var trieNode))
                        {
                            queue.Enqueue(trieNode);
                        }
                    }
                }
            }
            Queue<TrieNode> suffixQueue = new Queue<TrieNode>();
            {
                while (queue.Count > 0)
                {
                    var node = queue.Dequeue();
                    if (node.LongestDepth == suffix.Length)
                    {
                        suffixQueue.Enqueue(node);
                        continue;
                    }
                    foreach (var trieNode in node.GetChilds())
                    {
                        if (trieNode.LongestDepth > suffix.Length)
                        {
                            queue.Enqueue(trieNode);
                        }
                        else if (trieNode.LongestDepth == suffix.Length)
                        {
                            suffixQueue.Enqueue(trieNode);
                        }
                    }
                }
            }
            var maxWeight = -1;
            {
                while (suffixQueue.Count > 0)
                {
                    var node = suffixQueue.Dequeue();
                    foreach (var ch in suffix)
                    {
                        if (!node.TryGetValue(ch, out node))
                            break;
                    }

                    if (node == null)
                        continue;

                    if (node.IsEnd && maxWeight < node.Weight)
                        maxWeight = node.Weight;
                }
            }

            return maxWeight;
        }

        private class TrieNode
        {
            private readonly Dictionary<char, TrieNode>
                _characters = new Dictionary<char, TrieNode>();

            public TrieNode(char value)
            {
                Value = value;
            }

            public char Value { get; }

            public bool IsEnd { get; set; }

            public int LongestDepth { get; set; }
            public int Weight { get; set; }

            public ICollection<TrieNode> GetChilds() => _characters.Values;

            public bool TryGetValue(char value, out TrieNode node) => _characters.TryGetValue(value, out node);

            public TrieNode Put(char value)
            {
                if (TryGetValue(value, out var node))
                    return node;

                node = new TrieNode(value);
                _characters[value] = node;

                return node;
            }
        }
    }
}
