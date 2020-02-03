using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks.DataStructure
{
    [TestClass]
    public class AddAndSearchWordTest
    {
        [TestMethod]
        [DataRow("r.n", true)]
        public void Test(string word, bool expected)
        {
            var wordDict = new WordDictionary();
            wordDict.AddWord("ran");
            wordDict.AddWord("rune");
            wordDict.AddWord("runner");

            var result = wordDict.Search(word); 

            Assert.AreEqual(expected, result);
        }
    }
    
    public class WordDictionary
    {
        private static readonly char Zero = '\0';
        private TrieNode root = new TrieNode(Zero);

        //time/space O(m) where m is word length
        public void AddWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                throw new ArgumentNullException(nameof(word));

            TrieNode node = root;
            foreach (var ch in word)
            {
                node = node.TryGetValue(ch, out var temp) ? temp : node.Put(ch);
            }

            node.IsEnd = true;
        }

        //time O(m), space O(1)
        public bool Search(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                throw new ArgumentNullException(nameof(word));

            Queue<TrieNode> queue = new Queue<TrieNode>();
            queue.Enqueue(root);
            for (var index = 0; index < word.Length; index++)
            {
                var ch = word[index];
                var stackCount = queue.Count;
                for (int i = 0; i < stackCount; i++)
                {
                    var node = queue.Dequeue();
                    if (ch == '.')
                    {
                        foreach (var trieNode in node.GetChilds())
                        {
                            if (index == word.Length - 1 && trieNode.IsEnd)
                                return true;

                            queue.Enqueue(trieNode);
                        }
                    }
                    else
                    {
                        if (node.TryGetValue(ch, out var trieNode))
                        {
                            if (index == word.Length - 1 && trieNode.IsEnd)
                                return true;
                            
                            queue.Enqueue(trieNode);
                        }
                    }
                }
            }

            return false;
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
