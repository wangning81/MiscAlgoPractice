using System;
using System.Collections.Generic;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] easyBoard = {
                          4, 0, 0, 0, 0, 2, 6, 5, 9,
                          2, 3, 0, 0, 0, 9, 0, 0, 0,
                          0, 7, 9, 0, 5, 0, 0, 2, 3,
                          
                          5, 4, 3, 0, 0, 1, 0, 0, 0,
                          0, 0, 8, 0, 0, 0, 4, 0, 0,
                          0, 0, 0, 4, 0, 0, 9, 3, 5,

                          8, 5, 0, 0, 1, 0, 3, 7, 0,
                          0, 0, 0, 2, 0, 0, 0, 9, 4,
                          1, 9, 2, 7, 0, 0, 0, 0, 8
                           };

            int[] hardBoard = {
                            0, 0, 5,  2, 0, 0,  7, 0, 8,
                            6, 0, 0,  0, 0, 0,  0, 0, 5,
                            9, 2, 0,  0, 0, 5,  6, 0, 0,

                            0, 0, 6,  1, 0, 0,  0, 0, 0,
                            0, 9, 0,  6, 0, 2,  0, 7, 0,
                            0, 0, 0,  0, 0, 8,  3, 0, 0,

                            0, 0, 2,  3, 0, 0,  0, 5, 7,
                            8, 0, 0,  0, 0, 0,  0, 0, 6,
                            1, 0, 3,  0, 0, 7,  9, 0, 0
                              };

            int[] claimedToBeHardest = {
                            8, 0, 0,  0, 0, 0,  0, 0, 0,
                            0, 0, 3,  6, 0, 0,  0, 0, 0,
                            0, 7, 0,  0, 9, 0,  2, 0, 0,

                            0, 5, 0,  0, 0, 7,  0, 0, 0,
                            0, 0, 0,  0, 4, 5,  7, 0, 0,
                            0, 0, 0,  1, 0, 0,  0, 3, 0,

                            0, 0, 1,  0, 0, 0,  0, 6, 8,
                            0, 0, 8,  5, 0, 0,  0, 1, 0,
                            0, 9, 0,  0, 0, 0,  4, 0, 0

                                       };
            var board = ReadBoard(claimedToBeHardest);
            board.PrintToConsole();
            board.Solve();
            board.PrintToConsole();
            Console.ReadKey();
        }

        static SudokuBoard ReadBoard(int[] rawBoard)
        {
            int n = rawBoard.Length;
            if(n != SudokuBoard.ROW * SudokuBoard.COLUM) throw new ArgumentException("raw board is not valid");
            
            var rows = new List<int>();
            var cols = new List<int>();
            var vals = new List<int>();

            for (int i = 0; i < n; i++)
            {
                if (rawBoard[i] != 0)
                {
                    rows.Add(i / 9);
                    cols.Add(i % 9);
                    vals.Add(rawBoard[i]);
                }
            }

            var ret = new SudokuBoard();
            ret.Init(rows.ToArray(), cols.ToArray(), vals.ToArray());
            return ret;
        }
    }
}
