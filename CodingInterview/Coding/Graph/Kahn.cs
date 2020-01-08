using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Graph
{
    [TestClass]
    public class KahnSorting
    {
        //  5 → 0 ← 4
        //  ↓       ↓
        //  2 → 3 → 1
        private static readonly int[,] DirectedUnweightedAcyclicGraph =
        {
            {-1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1},
            {-1, -1, -1, 1, -1, -1},
            {-1, 1, -1, -1, -1, -1},
            {1, 1, -1, -1, -1, -1},
            {1, -1, 1, -1, -1, -1}
        };

        [TestMethod]
        public void TestAdjacencyMatrix()
        {
            var expected = new[] {4, 5, 0, 2, 3, 1};
            var kahn = new Kahn();
            var orderedNodes = kahn.Sort(DirectedUnweightedAcyclicGraph);

            CollectionAssert.AreEqual(expected, orderedNodes);
        }
    }

    //https://www.geeksforgeeks.org/all-topological-sorts-of-a-directed-acyclic-graph/
    public class Kahn
    {
        public int[] Sort(int[,] grid)
        {
            var order = new List<int>(grid.GetLength(0));
            int[] indergee = new int [grid.GetLength(0)];

            //fill number of incoming edges for every vertex
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] != -1)
                    {
                        indergee[j]++;
                    }
                }
            }

            //pick all vertices with in-degree as 0
            Queue<int> queue = new Queue<int>();
            for (int i = 0; i < indergee.Length; i++)
            {
                if(indergee[i] == 0)
                    queue.Enqueue(i);
            }

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();
                order.Add(vertex);

                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    if(grid[vertex, i] != -1 && --indergee[i] == 0)
                        queue.Enqueue(i);
                }
            }

            return order.ToArray();
        }
    }
}