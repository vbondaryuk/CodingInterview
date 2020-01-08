using CodingInterview.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class IsSubTreeTest
    {
        [TestMethod]
        [DataRow("[ 3, 4, 5, 1, 2, 0]", "[ 4,1,2 ]", true)]
        [DataRow("3, 4, 5, 1, 2, null, null, 0", "4, 1, 2 ", false)]
        [DataRow("1,1", "1 ", true)]
        public void Test(string binaryTreeStr, string binarySubTreeStr, bool expected)
        {
            var binaryTree = binaryTreeStr.SplitAndCast<int?>();
            var tree = binaryTree.ToTreeNode((i)=>i.Value);
            var binarySubTree = binarySubTreeStr.SplitAndCast<int?>();
            var subTree = binarySubTree.ToTreeNode((i) => i.Value);

            var sl = new Solution();
            var result = sl.IsSubtree(tree, subTree);

            Assert.AreEqual(expected, result);
        }
    }
    //https://leetcode.com/problems/subtree-of-another-tree
    public class Solution
    {
        public bool IsSubtree(TreeNode s, TreeNode t)
        {
            var q = 10;
            var q1 = new int[q];
            return s != null && (Equals(s,t) || IsSubtree(s.right, t) || IsSubtree(s.left, t));
        }

        private static bool Equals(TreeNode s, TreeNode t)
        {
            if (s == t && t == null)
                return true;
            if (s == null || t == null)
                return false;

            return s.val == t.val && Equals(s.left, t.left) && Equals(s.right, t.right);
        }
    }
}