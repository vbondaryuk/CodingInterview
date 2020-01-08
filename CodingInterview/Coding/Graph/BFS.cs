using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Graph
{
    [TestClass]
    public class BFSTest
    {
        [TestMethod]
        public void Test()
        {
            char[,] grid = {
                {'S', 'O', 'O', 'S', 'S'},
                {'D', 'O', 'D', 'O', 'D'},
                {'O', 'O', 'O', 'O', 'X'},
                {'X', 'D', 'D', 'O', 'O'},
                {'X', 'D', 'D', 'D', 'O'}};

            var bfs = new BFS_2('X');
            var shortestPath = bfs.FindShortestPath(grid, 'S');

            Assert.AreEqual(3, shortestPath);
        }
    }

    public class BFS
    {
        protected readonly char FindMark;

        //north south east west
        protected static readonly int[] DirectionRows = new[] {-1, 1, 0, 0};
        protected static readonly int[] DirectionColumns = new[] {0, 0, 1, -1};
        protected static readonly HashSet<char> DangerDirection = new HashSet<char>(new []{'D'});

        public BFS(char findMark)
        {
            FindMark = findMark;
        }

        public virtual int FindShortestPath(char[,] matrix, char entryMark)
        {
            Queue<(int, int)> queue = new Queue<(int, int)>();
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if(matrix[i,j] == entryMark)
                        queue.Enqueue((i,j));
                }

            int shortestPath = int.MaxValue;
            while (queue.Count > 0)
            {
                var path = FindShortestPath(matrix, queue.Dequeue());
                if (path != -1 && shortestPath > path)
                    shortestPath = path;
            }

            return shortestPath == int.MaxValue ? -1 : shortestPath;
        }

        public virtual int FindShortestPath(char[,] matrix, (int, int) entryPoint)
        {
            int rowLength = matrix.GetLength(0);
            int columnLength = matrix.GetLength(0);

            var visited = new bool[rowLength, columnLength];
            int stepCount = 0;
            int nodesInLayer = 1;
            int nodesInNextLayer = 0;
            bool isFound = false;

            Queue<(int,int)> queue = new Queue<(int, int)>();
            queue.Enqueue(entryPoint);
            while (queue.Count > 0)
            {
                (int row,int column) point = queue.Dequeue();
                if (matrix[point.row, point.column] == FindMark)
                {
                    isFound = true;
                    break;
                }
                VisitNeighbours(matrix, point, queue, visited, ref nodesInNextLayer);
                nodesInLayer--;
                if (nodesInLayer == 0)
                {
                    nodesInLayer = nodesInNextLayer;
                    nodesInNextLayer = 0;
                    stepCount++;
                }
            }

            return isFound ? stepCount : -1;
        }

        private void VisitNeighbours(
            char[,] matrix,
            (int row, int column) point, 
            Queue<(int, int)> queue,
            bool[,] visited,
            ref int nodesInNextLayer)
        {
            for (int i = 0; i < 4; i++)
            {
                var row = point.row + DirectionRows[i];
                var column = point.column + DirectionColumns[i];

                if (row < 0 || column < 0
                            || row > matrix.GetLength(0) - 1 
                            || column > matrix.GetLength(1) - 1
                            || DangerDirection.Contains(matrix[row, column]) || visited[row, column])
                {
                    continue;
                }

                visited[row, column] = true;
                nodesInNextLayer++;
                queue.Enqueue((row, column));
            }
        }
    }

    public class BFS_2 : BFS
    {
        public BFS_2(char findMark) : base(findMark)
        {
        }

        public override int FindShortestPath(char[,] matrix, char entryMark)
        {
            var sources = CollectSources(matrix, entryMark);
            for(int distance = 0; sources.Count > 0; distance++)
            {
                for (int s = sources.Count - 1; s >= 0; s--)
                {
                    (int row, int column) point = sources.Dequeue();
                    if (matrix[point.row, point.column] == FindMark)
                    {
                        return distance;
                    }

                    matrix[point.row, point.column] = 'V';//visited

                    for (int i = 0; i < 4; i++)
                    {
                        var row = point.row + DirectionRows[i];
                        var column = point.column + DirectionColumns[i];

                        if (row < 0 || column < 0
                                    || row > matrix.GetLength(0) - 1
                                    || column > matrix.GetLength(1) - 1
                                    || DangerDirection.Contains(matrix[row, column]))
                        {
                            continue;
                        }
                        sources.Enqueue((row, column));
                    }
                }
            }

            return -1;
        }

        private Queue<(int, int)> CollectSources(char[,] matrix, char entryMark)
        {
            Queue<(int, int)> queue = new Queue<(int, int)>();

            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == entryMark)
                        queue.Enqueue((i, j));
                }

            return queue;
        }
    }
}