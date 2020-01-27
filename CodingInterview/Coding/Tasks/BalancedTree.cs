using System;
using CodingInterview.Coding.Stucts.Nodes;

namespace CodingInterview.Coding.Tasks
{
    public class BalancedTree
    {
        public bool IsBalanced(TreeNode root)
        {
            if (root == null)
                return true;

            return GetDepth(root) != -1;
        }

        private static int GetDepth(TreeNode root)
        {
            if (root == null)
                return 0;

            var left = GetDepth(root.left);
            var right = GetDepth(root.right);
            if (left == -1 || right == -1 || Math.Abs(left - right) > 1)
            {
                return -1;
            }

            return Math.Max(left, right) + 1;
        }
    }
}