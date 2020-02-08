using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class BombermanTest
    {
        [TestMethod]
        public void Test()
        {
            string[] grid = new[]
            {
             ".......",
             "...O.O.",
             "....O..",
             "..O....",
             "OO...OO",
             "OO.O..."
            };

            var w = Bomberman.BomberMan(5, grid);
        }
    }

    public class Bomberman
    {
        private static readonly int[,] Direction = {
            {-1, 0},
            {1, 0},
            {0, -1},
            {0, 1}
        };

        public static string[] BomberMan(int n, string[] initialGrid)
        {
            if (n == 1)
                return initialGrid;

            var (bombQueue, grid) = ParseGrid(initialGrid);
            var plantedSet = new HashSet<(int, int)>();
            PlantBombs(grid, plantedSet);

            if (n % 2 == 0)
            {
                return ToArray(grid);
            }

            ExplodeBombs(grid, bombQueue, plantedSet);
            bombQueue = new Queue<(int, int)>(plantedSet);

            if ((n - 1) % 4 == 0)
            {
                PlantBombs(grid, plantedSet);
                ExplodeBombs(grid, bombQueue, plantedSet);
            }

            return ToArray(grid);
        }

        private static string[] ToArray(List<IList<char>> grid) => grid.Select(x => string.Join("", x)).ToArray();

        private static void ExplodeBombs(List<IList<char>> grid, Queue<(int, int)> queue,
            HashSet<(int, int)> plantedSet)
        {
            while (queue.Count > 0)
            {
                (int x, int y) point = queue.Dequeue();
                if (plantedSet.Contains(point))
                    plantedSet.Remove(point);

                grid[point.x][point.y] = '.';
                for (int i = 0; i < Direction.GetLength(0); i++)
                {
                    var directionX = Direction[i, 0];
                    var directionY = Direction[i, 1];

                    var dX = point.x + directionX;
                    var dY = point.y + directionY;


                    if (dX >= 0 && dY >= 0 && dX < grid.Count && dY < grid[dX].Count && grid[dX][dY] != '.')
                    {
                        grid[dX][dY] = '.';

                        if (plantedSet.Contains((dX, dY)))
                            plantedSet.Remove((dX, dY));
                    }
                }
            }
        }

        private static void PlantBombs(List<IList<char>> grid, HashSet<(int, int)> queue)
        {
            for (int i = 0; i < grid.Count; i++)
            {
                for (int j = 0; j < grid[i].Count; j++)
                {
                    if (grid[i][j] != 'O')
                    {
                        grid[i][j] = 'O';
                        queue.Add((i, j));
                    }
                }
            }
        }

        private static (Queue<(int, int)>, List<IList<char>>) ParseGrid(string[] initialGrid)
        {
            var queue = new Queue<(int, int)>();
            var grid = new List<IList<char>>(initialGrid.Length);
            for (int i = 0; i < initialGrid.Length; i++)
            {
                var gridLength = (int)Math.Floor((double)initialGrid[i].Length / 2);
                var list = new List<char>(gridLength);
                for (int j = 0, k = 0; j < initialGrid[i].Length; j++)
                {
                    if (initialGrid[i][j] == ' ')
                        continue;

                    if (initialGrid[i][j] == 'O')
                        queue.Enqueue((i, k));

                    list.Add(initialGrid[i][j]);
                    k++;
                }

                grid.Add(list);
            }

            return (queue, grid);
        }
    }
}
