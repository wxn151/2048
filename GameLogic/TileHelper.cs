using System;
using System.Collections.Generic;

namespace Game2048.GameLogic
{
    public static class TileHelper
    {
        public static void AddRandomTile(int[,] board, Random rand)
        {
            var empty = new List<(int, int)>();
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (board[i, j] == 0)
                        empty.Add((i, j));

            if (empty.Count == 0) return;

            var (x, y) = empty[rand.Next(empty.Count)];
            board[x, y] = rand.Next(10) < 9 ? 2 : 4;
        }
    }
}
