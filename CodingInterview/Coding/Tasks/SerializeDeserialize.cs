using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class SerializeDeserializeTest
    {
        [TestMethod]
        public void Test()
        {
            var node = new Node("root", new Node("left", new Node("left.left", null, null), null), new Node("right", null, null));

            var serialized = Serializer.Serialize(node);
            var deserialized = Serializer.Deserialize(serialized);

            Assert.AreEqual("left.left", deserialized.Left.Left.Name);
        }


        private class Serializer
        {
            public static string Serialize(Node node)
            {
                var heap = new Heap();
                heap.ToBinaryHeap(node);
                var arr = heap.Items;
                var builder = new StringBuilder();
                for (int i = 0; i <= heap.LastPosition; i++)
                {
                    builder.Append(arr[i]?.Name);
                    if (i != heap.LastPosition)
                        builder.Append(";");
                }

                return builder.ToString();
            }

            public static Node Deserialize(string value)
            {
                var arr = value.Split(';');
                var heap = new Heap();
                var node = heap.FromBinaryHeap(arr);

                return node;
            }
        }

        private class Heap
        {
            private int Left(int i) => i * 2 + 1;
            private int Right(int i) => i * 2 + 2;
            private int Parent(int i) => i - 1 / 2;

            public int LastPosition { get; private set; }

            public Node[] Items { get; private set; } = new Node[10];

            public void ToBinaryHeap(Node node)
            {
                ToBinaryHeap(node, 0);
            }

            private void ToBinaryHeap(Node node, int currentPosition)
            {
                if (node == null)
                    return;
                if (LastPosition < currentPosition)
                    LastPosition = currentPosition;

                if (currentPosition == Items.Length - 1)
                {
                    var temp = new Node[Items.Length * 2];
                    Array.Copy(Items, temp, Items.Length);
                    Items = temp;
                }

                Items[currentPosition] = node;
                ToBinaryHeap(node.Right, Right(currentPosition));
                ToBinaryHeap(node.Left, Left(currentPosition));
            }

            public Node FromBinaryHeap(string[] arr)
            {
                return FromBinaryHeap(arr, 0);
            }

            private Node FromBinaryHeap(string[] arr, int index)
            {
                if (index >= arr.Length)
                    return null;

                var left = FromBinaryHeap(arr, Left(index));
                var right = FromBinaryHeap(arr, Right(index));
                var node = new Node(arr[index], left, right);

                return node;
            }
        }

        private class Node
        {
            public string Name { get; }
            public Node Left { get; }
            public Node Right { get; }

            public Node(string name, Node left, Node right)
            {
                Name = name;
                Left = left;
                Right = right;
            }
        }
    }


}