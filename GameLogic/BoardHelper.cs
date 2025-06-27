using System;
using System.Linq;

namespace Game2048.GameLogic
{
    public static class BoardHelper
    {
        public static int[] GetRow(int[,] board, int row, bool reverse = false)
        {
            var line = Enumerable.Range(0, 4).Select(j => board[row, j]).ToArray();
            if (reverse) Array.Reverse(line);
            return line;
        }

        public static void SetRow(int[,] board, int row, int[] values)
        {
            for (int j = 0; j < 4; j++)
                board[row, j] = values[j];
        }

        public static int[] GetColumn(int[,] board, int col, bool reverse = false)
        {
            var line = Enumerable.Range(0, 4).Select(i => board[i, col]).ToArray();
            if (reverse) Array.Reverse(line);
            return line;
        }

        public static void SetColumn(int[,] board, int col, int[] values)
        {
            for (int i = 0; i < 4; i++)
                board[i, col] = values[i];
        }

        public static int[] MergeLine(int[] line, ref int score, ref bool hasWon)
        {
            var nonZero = line.Where(x => x != 0).ToList();

            for (int i = 0; i < nonZero.Count - 1; i++)
            {
                if (nonZero[i] == nonZero[i + 1])
                {
                    nonZero[i] *= 2;
                    score += nonZero[i];
                    if (nonZero[i] == 2048) hasWon = true;
                    nonZero[i + 1] = 0;
                }
            }

            return nonZero.Where(x => x != 0)
                          .Concat(Enumerable.Repeat(0, 4))
                          .Take(4)
                          .ToArray();
        }

        public static bool AreEqual(int[,] a, int[,] b)
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (a[i, j] != b[i, j]) return false;
            return true;
        }
    }
}
