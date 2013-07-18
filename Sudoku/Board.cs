using System;

namespace Sudoku
{
    public class SudokuBoard
    {
        public const int ROW = 9;
        public const int COLUM = 9;

        public int FreeCellCount
        {
            get;
            private set;
        }

        private bool initialized;
        private int[,] cells;
        private bool[,] filled;

        #region ctor & init
        public SudokuBoard()
        {
            FreeCellCount = ROW * COLUM;
            cells = new int[ROW, COLUM];
            filled = new bool[ROW, COLUM];
        }

        public void Init(int[] rows, int[] cols, int[] vals)
        {
            EnsureValidInit(rows, cols, vals);
            
            int n = rows.Length;

            for (int i = 0; i < n; i++)
                if (!Fill(rows[i], cols[i], vals[i]))
                    throw new ArgumentException("initilized failed: setup is invalid.");

            initialized = true;
        }
        #endregion

        #region Ensures

        private void EnsureValidInit(int[] rows, int[] cols, int[] vals)
        {
            if (rows == null || cols == null || vals == null)
                throw new ArgumentNullException("rows or cols or vals is null.");
            
            int n = rows.Length;

            if (cols.Length != n || vals.Length != n)
                throw new ArithmeticException("number of rows/cols/vals is NOT consistent.");

            for (int i = 0; i < n; i++)
            {
                EnsureOnBoard(rows[i], cols[i]);
                EnsureInRange(vals[i]);
            }
        }

        private void EnsureOnBoard(int row, int col)
        {
            if (row < 0 || row >= ROW || col < 0 || col >= COLUM)
                throw new InvalidOperationException(string.Format("row or colum is out of range: row = {0}, col = {1}.", row, col));
        }

        private void EnsureNotFilled(int row, int col)
        {
            if (filled[row, col])
                throw new InvalidOperationException(string.Format("row = {0}, col = {1} is already filled.", row, col));
        }

        private void EnsureInRange(int val)
        {
            if (val < 1 || val > 9) throw new ArgumentOutOfRangeException("Sudoku Rule: value must be in [1, 9].");
        }

        #endregion

        private bool Fill(int row, int col, int value)
        {
            EnsureOnBoard(row, col);
            EnsureNotFilled(row, col);
            EnsureInRange(value);

            if (IsValidFill(row, col, value))
            {
                cells[row, col] = value;
                filled[row, col] = true;
                FreeCellCount--;
                return true;
            }
            return false;
        }

        private void Erase(int row, int col)
        {
            EnsureOnBoard(row, col);
            filled[row, col] = false;
            FreeCellCount++;
        }

        #region helpers

        private int[] NextCell(int row, int col)
        {
            EnsureOnBoard(row, col);
            col++;
            if (col == 9) { col = 0; row++; }
            EnsureOnBoard(row, col);
            return new int[2] { row, col };
        }

        private bool IsValidFill(int row, int col, int val)
        {
            for (int i = 0; i < ROW; i++)
                if (filled[i, col])
                    if (cells[i, col] == val) return false;

            for (int j = 0; j < ROW; j++)
                if (filled[row, j])
                    if (cells[row, j] == val) return false;

            var topRow = row / 3 * 3;
            var topCol = col / 3 * 3;

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (filled[topRow + i, topCol + j])
                        if (cells[topRow + i, topCol + j] == val) return false;
            return true;
        }

        #endregion

        public void PrintToConsole()
        {
            Console.WriteLine("-------------------------------------------------");
            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COLUM; j++)
                {
                    if (filled[i, j]) Console.Write(cells[i, j]);
                    else Console.Write("X");
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("-------------------------------------------------");
        }

        public void Solve()
        {
            if (!initialized) throw new InvalidOperationException("Sudoku board is NOT initialized yet.");
            if (FreeCellCount == 0) return;
            SolveImp(0, 0);
        }

        private void SolveImp(int sRow, int sCol)
        {
            if (FreeCellCount == 0) 
                return;

            while (filled[sRow, sCol])
            {
                var next = NextCell(sRow, sCol);
                sRow = next[0];
                sCol = next[1];
            }

            for (int i = 1; i <= 9 && FreeCellCount > 0; i++)
            {
                if (Fill(sRow, sCol, i))
                {
                    SolveImp(sRow, sCol);
                    if(FreeCellCount > 0) Erase(sRow, sCol);
                }
            }
        }

        
    }
}
