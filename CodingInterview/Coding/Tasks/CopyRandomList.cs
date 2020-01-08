using System.Collections.Generic;

namespace CodingInterview.Coding.Tasks
{
    //https://leetcode.com/problems/copy-list-with-random-pointer/submissions/
    //https://leetcode.com/problems/copy-list-with-random-pointer/discuss/43491/A-solution-with-constant-space-complexity-O(1)-and-linear-time-complexity-O(N)
    public class CopyRandomListSolution
    {
        private readonly Dictionary<Node, int> nodeToIndex = new Dictionary<Node, int>();
        private Node[] copiedNode;

        public Node CopyRandomList(Node head)
        {
            if (head == null)
                return null;

            FillSets(head);
            var newHead = Copy(head, 0);
            var temp = head.next;
            for (int i = 1; i < nodeToIndex.Count; i++)
            {
                Copy(temp, i);
                temp = temp.next;
            }

            return newHead;
        }

        private Node Copy(Node head, int index)
        {
            if (head == null)
                return null;

            if (copiedNode[index] != null)
            {
                return copiedNode[index];
            }

            var node = new Node(head.val);
            copiedNode[index] = node;
            var next = head.next == null ? null : Copy(head.next, nodeToIndex[head.next]);
            var random = head.random == null ? null : Copy(head.random, nodeToIndex[head.random]);

            node.next = next;
            node.random = random;
            return node;
        }

        private void FillSets(Node head)
        {
            var temp = head;
            int i = 0;
            for (; ; i++)
            {
                nodeToIndex[temp] = i;
                temp = temp.next;
                if (temp == null)
                    break;
            }
            copiedNode = new Node[i + 1];
        }
    }

    public class Node
    {
        public int val;
        public Node next;
        public Node random;

        public Node(int _val)
        {
            val = _val;
            next = null;
            random = null;
        }
    }
}