using System;

namespace CodingInterview.Coding.Tasks
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int x) { val = x; }
    }

    public static class TreeNodeExtension
    {
        private static int _getLeftIndex(int i) => i * 2 + 1;
        private static int _getRightIndex(int i) => i * 2 + 2;

        public static TreeNode ToTreeNode<T>(this T[] arr, Func<T, int> cast)
        {
            if (arr == null || arr.Length == 0)
                throw new ArgumentNullException(nameof(arr));
            var treeNode = new TreeNode(cast(arr[0]));

            Fill(arr, 0, treeNode, cast);

            return treeNode;
        }
        private static void Fill<T>(this T[] arr, int index, TreeNode node, Func<T, int> cast)
        {
            int leftIndex = _getLeftIndex(index);
            int rightIndex = _getRightIndex(index);

            if (leftIndex < arr.Length && arr[leftIndex] != null)
            {
                node.left = new TreeNode(cast(arr[leftIndex]));
                Fill(arr, leftIndex, node.left, cast);
            }

            if (rightIndex < arr.Length && arr[rightIndex] != null)
            {
                node.right = new TreeNode(cast(arr[rightIndex]));
                Fill(arr, rightIndex, node.right, cast);
            }
        }
    }
}