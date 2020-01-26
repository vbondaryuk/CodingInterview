using System.Collections.Generic;
using CodingInterview.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Graph
{
    [TestClass]
    public class TopologicalSortTest
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
            var expected = new[] { 5, 4, 2, 3, 1, 0 };
            var topoSort = new TopologicalSort();
            var orderedNodes = topoSort.Sort(DirectedUnweightedAcyclicGraph);

            CollectionAssert.AreEqual(expected, orderedNodes);
        }

        [TestMethod]
        public void TestAdjacencyList()
        {
            var expected = new[] { 5, 4, 2, 3, 1, 0 };
            var adjList = DirectedUnweightedAcyclicGraph.CreateAdjacencyList();

            var topoSort = new TopologicalSort();
            var orderedNodes = topoSort.Sort(adjList);

            CollectionAssert.AreEqual(expected, orderedNodes);

        }

        [TestMethod]
        public void TestGetAllSorts()
        {
            var topoSort = new TopologicalSort();
            var allSorts = topoSort.GetAllSorts(DirectedUnweightedAcyclicGraph);

            Assert.AreEqual(13, allSorts.Count);
        }
    }
    public class TopologicalSort
    {
        #region ajecancy matrix
        public int[] Sort(int[,] grid)
        {
            Stack<int> order = new Stack<int>(grid.Length);
            var visited = new bool[grid.Length];

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                if (!visited[i])
                    BFS(i, grid, visited, order);
            }

            return order.ToArray();
        }

        public void BFS(
            int index,
            int[,] grid,
            bool[] visited,
            Stack<int> order
            )
        {
            visited[index] = true;

            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[index, j] != -1 && !visited[j])
                {
                    BFS(j, grid, visited, order);
                }
            }

            order.Push(index);
        }

        #endregion

        #region ajacency list

        public int[] Sort(IList<(int, IList<int>)> adjList)
        {
            var ordered = new int[adjList.Count];
            int orderIndex = adjList.Count - 1;
            var visited = new bool[adjList.Count];

            for (int i = 0; i < adjList.Count; i++)
            {
                if (!visited[i])
                    orderIndex = DFS(i, orderIndex, ordered, visited, adjList);
            }

            return ordered;
        }

        private static int DFS(
            int index,
            int orderIndex,
            int[] ordered,
            bool[] visited,
            IList<(int vertex, IList<int> Edges)> adjList)
        {
            visited[index] = true;

            for (int i = 0; i < adjList[index].Edges.Count; i++)
            {
                if (!visited[adjList[index].Edges[i]])
                    orderIndex = DFS(adjList[index].Edges[i], orderIndex, ordered, visited, adjList);
            }

            ordered[orderIndex] = adjList[index].Item1;
            return --orderIndex;
        }

        #endregion

        #region all sort

        public List<List<int>> GetAllSorts(int[,] grid)
        {
            var order = new List<List<int>>();
            int[] indegree = new int[grid.GetLength(0)];
            bool[] visited = new bool[grid.GetLength(0)];

            //fill number of incoming edges for every vertex
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] != -1)
                    {
                        indegree[j]++;
                    }
                }
            }

            FillAllSortPaths(grid, order, new List<int>(),  indegree, visited);

            return order;
        }

        private void FillAllSortPaths(int[,] grid, List<List<int>> orders, List<int> order, int[] indegree, bool[] visited)
        {
            //flag for validating whether all topo found 
            bool flag = false;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                if (!visited[i] && indegree[i] == 0)
                {
                    visited[i] = true;

                    order.Add(i);

                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        if (grid[i, j] != -1)
                            indegree[j]--;
                    }

                    FillAllSortPaths(grid, orders, order, indegree, visited);

                    //reset for backtracking
                    visited[i] = false;
                    order.Remove(i);
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        if (grid[i, j] != -1)
                            indegree[j]++;
                    }

                    flag = true;
                }
            }

            if(!flag)
                orders.Add(new List<int>(order));
        }

        #endregion
    }
}
