using System;
using System.Collections.Generic;
using System.Linq;
using CodingInterview.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Graph
{
    [TestClass]
    public class ArticulationPointTest
    {
        [TestMethod]
        public void Test()
        {
            var expected = new[] { 2, 3, 5 };
            int[,] grid = { { 0, 1 }, { 0, 2 }, { 1, 3 }, { 2, 3 }, { 2, 5 }, { 5, 6 }, { 3, 4 } };

            var adjacencyList = grid.CreateHashedAdjacencyList();
            var articulationPoints = new ArticulationPoint().Get(adjacencyList, 7, 7);

            CollectionAssert.AreEqual(expected, articulationPoints.OrderBy(x => x).ToArray());
        }
    }

    //https://www.geeksforgeeks.org/articulation-points-or-cut-vertices-in-a-graph/
    public class ArticulationPoint
    {
        public ICollection<int> Get(Dictionary<int, HashSet<int>> adjacencyList, int numNodes, int numEdges)
        {
            bool[] visited = new bool[numEdges];
            int[] disc = new int[numEdges];
            int[] low = new int[numEdges];
            ICollection<int> articulationPoint = new HashSet<int>();

            for (int i = 0; i < numNodes; i++)
            {
                visited[i] = false;
                disc[i] = -1;
                low[i] = -1;
            }

            int time = 0;
            for (int i = 0; i < numNodes; i++)
            {
                if (!visited[i])
                    FillArticulationPoint(i, visited, disc, low, -1, adjacencyList, articulationPoint, ref time);
            }

            return articulationPoint;
        }

        /// <param name="disc">Entry time of vertices</param>
        /// <param name="low">Minimum time</param>
        private void FillArticulationPoint(
            int v,
            bool[] visited,
            int[] disc,
            int[] low,
            int parent,
            Dictionary<int, HashSet<int>> adjacencyList,
            ICollection<int> articulationPoint,
            ref int time)
        {
            int children = 0;
            visited[v] = true;
            disc[v] = low[v] = ++time;

            foreach (var vTo in adjacencyList[v])
            {
                if (vTo == parent)
                    continue;

                if (visited[vTo])
                {
                    low[v] = Math.Min(low[v], disc[vTo]);
                }
                else
                {
                    children++;
                    FillArticulationPoint(vTo, visited, disc, low, v, adjacencyList, articulationPoint, ref time);
                    low[v] = Math.Min(low[vTo], low[v]);

                    if (parent != -1 && disc[v] <= low[vTo])
                        articulationPoint.Add(v);
                }
            }

            if (parent == -1 && children > 1)
                articulationPoint.Add(v);

        }
    }
}