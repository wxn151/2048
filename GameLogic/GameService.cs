using System;

namespace Game2048.GameLogic
{
    public class GameService
    {
        private int[,] _board = new int[4, 4];
        private readonly Random rand = new();

        public int[,] Board
        {
            get
            {
                var copy = new int[4, 4];
                Array.Copy(_board, copy, _board.Length);
                return copy;
            }
        }

        public int Score { get; private set; }
        public bool HasWon { get; private set; }

        public void StartNewGame()
        {
            Score = 0;
            HasWon = false;
            _board = new int[4, 4];
            TileHelper.AddRandomTile(_board, rand);
            TileHelper.AddRandomTile(_board, rand);
        }

        public bool Move(string direction)
        {
            int[,] previous = (int[,])_board.Clone();

            for (int i = 0; i < 4; i++)
            {
                int[] line = direction switch
                {
                    "up" => BoardHelper.GetColumn(_board, i),
                    "down" => BoardHelper.GetColumn(_board, i, reverse: true),
                    "left" => BoardHelper.GetRow(_board, i),
                    "right" => BoardHelper.GetRow(_board, i, reverse: true),
                    _ => throw new ArgumentException("Invalid direction")
                };

                // Copy Score and HasWon to local variables to avoid CS0206
                int score = Score;
                bool hasWon = HasWon;

                int[] merged = BoardHelper.MergeLine(line, ref score, ref hasWon);

                // Update Score and HasWon after ref call
                Score = score;
                HasWon = hasWon;

                if (direction == "down" || direction == "right")
                    Array.Reverse(merged);

                if (direction == "up" || direction == "down")
                    BoardHelper.SetColumn(_board, i, merged);
                else
                    BoardHelper.SetRow(_board, i, merged);
            }

            if (!BoardHelper.AreEqual(previous, _board))
            {
                TileHelper.AddRandomTile(_board, rand);
                return true;
            }

            return false;
        }
    }
}
