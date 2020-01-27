using System.Collections;
using System.Collections.Generic;
using CodingInterview.Coding.Stucts.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class MergeLinkedListTEst
    {
        [TestMethod]
        public void Test()
        {
            var lists = new ListNode[2]
            {
                new ListNode(1) { next = new ListNode(2) { next = new ListNode(2)} },
                new ListNode(1) { next = new ListNode(1) { next = new ListNode(2)} }
            };
            
            var mkl = new MergeLinkedList();
            var result = mkl.MergeKLists(lists);

        }
    }
    public class MergeLinkedList
    {
        public ListNode MergeKLists(ListNode[] lists)
        {
            var set = FillSet(lists);
            ListNode root = null;
            ListNode current = null;
            foreach ((ListNode root, ListNode current) value in set.Values)
            {
                if (root == null)
                    root = value.root;
                else
                    current.next = value.root;

                current = value.current;
            }

            return root;
        }

        private static SortedDictionary<int, (ListNode, ListNode)> FillSet(ListNode[] lists)
        {
            var set = new SortedDictionary<int, (ListNode, ListNode)>();
            for (int j = 0; j < lists.Length; j++)
            {
                var node = lists[j];
                while (node != null)
                {
                    ListNode newNode = new ListNode(node.val);
                    if (set.TryGetValue(node.val, out (ListNode root, ListNode current) nodeTuple))
                    {
                        nodeTuple.current.next = newNode;
                        set[node.val] = (nodeTuple.root, newNode);
                    }
                    else
                    {
                        set[node.val] = (newNode, newNode);
                    }
                    node = node.next;
                }
            }

            return set;
        }
    }
}