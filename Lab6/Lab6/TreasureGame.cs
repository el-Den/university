using System;

namespace Lab6WinForms.Core
{
    public class TreasureGame
    {
        private const string TREASURE = "C";
        private const string TRAP = "T";
        private const string EMPTY = "0";

        private readonly int _rows;
        private readonly int _cols;
        private readonly int _treasureCount;
        private readonly int _trapCount;
        private readonly string[,] _field;
        private readonly bool[,] _revealed;
        private readonly Random _rnd = new Random();

        public int Rows => _rows;
        public int Cols => _cols;
        public int TreasureCount => _treasureCount;
        public int TrapCount => _trapCount;

        public TreasureGame(int rows, int cols, int treasureCount, int trapCount)
        {
            _rows = rows;
            _cols = cols;
            _treasureCount = treasureCount;
            _trapCount = trapCount;
            _field = new string[_rows, _cols];
            _revealed = new bool[_rows, _cols];
            Generate();
        }

        private void Generate()
        {
            for (int i = 0; i < _treasureCount; i++)
            {
                int x, y;
                do { x = _rnd.Next(_rows); y = _rnd.Next(_cols); } while (_field[x, y] != null);
                _field[x, y] = TREASURE;
            }
            for (int i = 0; i < _trapCount; i++)
            {
                int x, y;
                do { x = _rnd.Next(_rows); y = _rnd.Next(_cols); } while (_field[x, y] != null);
                _field[x, y] = TRAP;
            }
            for (int i = 0; i < _rows; i++)
                for (int j = 0; j < _cols; j++)
                    if (_field[i, j] == null) _field[i, j] = EMPTY;
        }

        public string RevealCell(int row, int col)
        {
            if (row < 0 || row >= _rows || col < 0 || col >= _cols) return null;
            _revealed[row, col] = true;
            return _field[row, col];
        }

        public string GetCellView(int row, int col)
        {
            if (_revealed[row, col]) return _field[row, col];
            return null;
        }

        public bool IsRevealed(int row, int col) => _revealed[row, col];

        public int CountRevealedTreasures()
        {
            int c = 0;
            for (int i = 0; i < _rows; i++)
                for (int j = 0; j < _cols; j++)
                    if (_revealed[i, j] && _field[i, j] == TREASURE) c++;
            return c;
        }

        public int CountRevealedTraps()
        {
            int c = 0;
            for (int i = 0; i < _rows; i++)
                for (int j = 0; j < _cols; j++)
                    if (_revealed[i, j] && _field[i, j] == TRAP) c++;
            return c;
        }

        // For debugging / full reveal
        public string[,] GetFullFieldCopy()
        {
            var copy = new string[_rows, _cols];
            Array.Copy(_field, copy, _field.Length);
            return copy;
        }
    }
}
