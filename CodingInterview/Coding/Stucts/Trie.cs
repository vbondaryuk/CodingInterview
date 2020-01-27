using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Stucts
{
    [TestClass]
    public class TrieTest
    {
        [TestMethod]
        public void InsertTest()
        {
            Trie trie = new Trie();
            trie.Insert("apple");

            var result = trie.Search("apple");
            
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SearchTest()
        {
            Trie trie = new Trie();
            trie.Insert("apple");

            var result = trie.Search("app");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SearchAfterAppendTest()
        {
            Trie trie = new Trie();
            trie.Insert("apple");

            var result = trie.Search("app");

            Assert.IsFalse(result);

            trie.Insert("app");
            result = trie.Search("app");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void StartsWithTest()
        {
            Trie trie = new Trie();
            trie.Insert("apple");

            var result = trie.StartsWith("app");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AutoComplite()
        {
            Trie trie = new Trie();
            trie.Insert("amazon");
            trie.Insert("amazon prime");
            trie.Insert("amazing");
            trie.Insert("amazing spider man");
            trie.Insert("amazed");
            trie.Insert("alibaba");
            trie.Insert("ali express");
            trie.Insert("ebay");
            trie.Insert("walmart");

            var words = trie.AutoComplete("amaz");

            Assert.AreEqual(5, words.Count);
        }
    }

    public class Trie
    {
        private static readonly char Zero = '\0';
        private TrieNode root = new TrieNode(Zero, null);

        //time/space O(m) where m is word length
        public void Insert(string word)
        { 
            if(string.IsNullOrWhiteSpace(word))
                throw new ArgumentNullException(nameof(word));

            TrieNode node = root;
            foreach (var ch in word)
            {
                node = node.TryGetValue(ch, out var temp) ? temp: node.Put(ch);
            }

            node.IsEnd = true;
        }

        //time O(m), space O(1)
        public bool Search(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                throw new ArgumentNullException(nameof(word));

            TrieNode node = SearchPrefix(word);

            return node != null && node.IsEnd;
        }

        public bool StartsWith(string prefix)
        {
            if (string.IsNullOrWhiteSpace(prefix))
                throw new ArgumentNullException(nameof(prefix));

            TrieNode node = SearchPrefix(prefix);

            return node != null;
        }

        public ICollection<string> AutoComplete(string prefix)
        {
            if (string.IsNullOrWhiteSpace(prefix))
                throw new ArgumentNullException(nameof(prefix));

            TrieNode node = SearchPrefix(prefix);
            
            if (node == null)
                return Array.Empty<string>();

            var words = node.GetWords();

            return words;
        }

        private TrieNode SearchPrefix(string word)
        {
            TrieNode node = root;
            foreach (var ch in word)
            {
                if (!node.TryGetValue(ch, out node))
                    return null;
            }

            return node;
        }

        private class TrieNode
        {
            private readonly Dictionary<char, TrieNode>
                _characters = new Dictionary<char, TrieNode>(); //Add new CharCaseInsensitiveComparer()

            public TrieNode(char value, TrieNode parent)
            {
                Value = value;
                Parent = parent;
            }

            public TrieNode Parent { get; }

            public char Value { get; }

            public bool IsEnd { get; set; }

            public bool Contains(char value) => _characters.ContainsKey(value);

            public TrieNode Get(char value) => _characters[value];

            public bool TryGetValue(char value, out TrieNode node) => _characters.TryGetValue(value, out node);

            public TrieNode Put(char value)
            {
                if (TryGetValue(value, out var node))
                    return node;

                node = new TrieNode(value, this);
                _characters[value] = node;

                return node;
            }

            public ICollection<string> GetWords()
            {
                var parentChars = new List<char>();
                TrieNode node = this;
                while (node.Value != Zero)
                {
                    parentChars.Add(node.Value);
                    node = node.Parent;
                }
                parentChars.Reverse();
                var words = new List<string>();
                GetWords(this, parentChars, words);

                return words;
            }

            private static void GetWords(TrieNode node, List<char> parentChars, List<string> words)
            {
                if (node.IsEnd)
                    words.Add(new string(parentChars.ToArray()));
                
                if (node._characters == null)
                    return;

                foreach (KeyValuePair<char, TrieNode> item in node._characters)
                {
                    parentChars.Add(item.Value.Value);
                    GetWords(item.Value, parentChars, words);
                    parentChars.RemoveAt(parentChars.Count - 1);
                }
            }

            public override string ToString()
            {
                var currentString = new string(Value, 1);
                if (Parent.Value == Zero)
                    return currentString;

                return Parent + currentString;
            }
        }

        private class CharCaseInsensitiveComparer : IEqualityComparer<char>
        {
            public bool Equals(char x, char y)
            {
                return char.ToUpperInvariant(x) == char.ToUpperInvariant(y);
            }

            public int GetHashCode(char obj)
            {
                return char.ToUpperInvariant(obj).GetHashCode();
            }
        }
    }
}
