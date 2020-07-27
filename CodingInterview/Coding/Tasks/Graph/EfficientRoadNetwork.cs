using System.Collections.Generic;
using CodingInterview.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks.Graph
{
    [TestClass]
    public class EfficientRoadNetworkTest
    {
        [TestMethod]
        public void Test_EfficientNetwork()
        {
            int[,] grid = { { 3, 0 }, { 0, 4 }, { 5, 0 }, { 2, 1 }, { 1, 4 }, { 2, 3 }, { 5, 2 } };
            var result = EfficientRoadNetwork(6, grid);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void Test_NonEfficientNetwork()
        {
            int[,] grid = { { 0, 4 }, { 5, 0 }, { 2, 1 }, { 1, 4 }, { 2, 3 }, { 5, 2 } };
            var result = EfficientRoadNetwork(6, grid);

            Assert.IsFalse(result);
        }
        private bool EfficientRoadNetwork(int n, int[,] roads)
        {
            var adjacencyList = roads.CreateHashedAdjacencyList();
            foreach (var point in adjacencyList)
            {
                var visited = new bool[adjacencyList.Count];
                var queue = new Queue<int>();
                queue.Enqueue(point.Key);
                visited[point.Key] = true;
                int steps = 1;
                int availableVertexes = 0;
                while (steps < 3)
                {
                    var length = queue.Count;
                    for (int i = 0; i < length; i++)
                    {
                        var vertex = queue.Dequeue();
                        foreach (var to in adjacencyList[vertex])
                        {
                            if (!visited[to])
                            {
                                queue.Enqueue(to);
                                availableVertexes++;
                            }
                        }
                    }
                    steps++;
                }

                if (availableVertexes < adjacencyList.Count - 1)
                    return false;
            }

            return true;
        }
    }
    /*
     Once upon a time, in a kingdom far, far away, there lived a King Byteasar III. 
     As a smart and educated ruler, he did everything in his (unlimited) power 
     to make every single system within his kingdom efficient. 
     The king went down in history as a great road builder: during his reign a great number of roads were built, 
     and the road system he created was the most efficient during those dark times.

     When you started learning about Byteasar's III deeds in your history classes, 
     a creeping doubt crawled into the back of your mind: what if the famous king wasn't so great after all? 
     According to the most recent studies, there were n cities in Byteasar's kingdom, connected by two-way roads. 
     You managed to get information about these roads from the university library, 
     so now you can write a function that will determine whether the road system in Byteasar's kingdom was efficient or not.

     A road system is considered efficient if it is possible to travel from any city to any other city by traversing at most 2 roads.

     Example
        1)
            For n = 6 and

            roads = [[3, 0], [0, 4], [5, 0], [2, 1],
                      [1, 4], [2, 3], [5, 2]]

            the output should be
            efficientRoadNetwork(n, roads) = true.
        2)
            For n = 6 and

            roads = [[0, 4], [5, 0], [2, 1],
                  [1, 4], [2, 3], [5, 2]]

            the output should be
            efficientRoadNetwork(n, roads) = false.
     */
}
