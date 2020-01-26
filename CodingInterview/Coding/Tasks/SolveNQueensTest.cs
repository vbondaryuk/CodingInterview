using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class SolveNQueensTest
    {
        [TestMethod]
        public void Test()
        {

        }

        //https://leetcode.com/problems/n-queens/
        public static IList<IList<string>> SolveNQueens(int n)
        {
            var solutions = new List<IList<string>>();
            var boards = new char[n][];

            FillBoard(boards, n);
            FindNQueens(n, 0, boards, solutions);

            return solutions;
        }

        private static void FindNQueens(int queens, int column, char[][] boards, IList<IList<string>> solutions)
        {
            if (column == queens)
            {
                Append(solutions, boards, queens);
                return;
            }

            for (int row = 0; row < queens; row++)
            {
                boards[row][column] = 'Q';
                if (!IsUnderAttack(row, column, boards, queens))
                {
                    FindNQueens(queens, column + 1, boards, solutions);
                }

                boards[row][column] = '.';
            }
        }

        private static bool IsUnderAttack(int row, int column, char[][] boards, int n)
        {
            int rowIndex = row - 1;
            int columnIndex = column - 1;
            int rowDegree135 = (row - rowIndex) + row;
            while (rowIndex >= 0 || columnIndex >= 0 || rowDegree135 < n)
            {
                if (rowIndex >= 0 && boards[rowIndex][column] == 'Q')
                    return true;
                if (columnIndex >= 0 && boards[row][columnIndex] == 'Q')
                    return true;

                if (rowIndex >= 0 && columnIndex >= 0 && boards[rowIndex][columnIndex] == 'Q')
                    return true;
                if (rowDegree135 < n && columnIndex >= 0 && boards[rowDegree135][columnIndex] == 'Q')
                    return true;

                rowIndex--;
                columnIndex--;
                rowDegree135 = (row - rowIndex) + row;
            }

            return false;
        }

        private static void FillBoard(char[][] boards, int n)
        {
            for (int i = 0; i < n; i++)
            {
                boards[i] = new char[n];
                for (int j = 0; j < n; j++)
                    boards[i][j] = '.';
            }
        }

        private static void Append(IList<IList<string>> solutions, char[][] board, int n)
        {
            var result = new List<string>();

            for (int i = 0; i < n; i++)
                result.Add(new string(board[i]));

            solutions.Add(result);
        }
    }
}
