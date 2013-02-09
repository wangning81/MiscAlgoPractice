using System;
using System.Collections.Generic;

namespace FindBlocks
{
    class Program
    {
        class Point<T>
        {
            public Point(T x, T y) { X = x; Y = y; } 
            public T X { get; private set; }
            public T Y { get; private set; }
            public override string ToString()
            {
                return string.Format("({0},{1})", X, Y);
            } 
        }

        class Block
        {
            private IList<Point<int>> tiles = new List<Point<int>>();
            public IList<Point<int>> Tiles { get { return tiles; } }
            public int TileCount { get { return tiles.Count; } }
            public override string ToString()
            {
                return string.Join(",", tiles);
            }
        }

        static Block[] FindAllBlocks<T>(T[,] matrix, T identity) where T : IComparable<T>
        {
            int TotalRow = matrix.GetLength(0);
            int TotalCol = matrix.GetLength(1);
            var mark = new bool[TotalRow, TotalCol];
            var ret = new List<Block>();

            for (int row = 0; row < TotalRow; row++)
            {
                for (int col = 0; col < TotalCol; col++)
                {
                    if (!mark[row, col] && matrix[row,col].CompareTo(identity) == 0)
                    {
                        var block = new Block();
                        Search(matrix, identity, TotalRow, TotalCol, mark, block, row, col);
                        ret.Add(block);
                    }
                }
            }
            return ret.ToArray();
        }

        static void Search<T>(T[,] matrix, T identity, int TotalRow, int TotalCol, bool[,] mark, 
                       Block block, int row, int col) where T : IComparable<T>
        {
            if (row < 0 || col < 0 || row >= TotalRow || col >= TotalCol || mark[row, col])
                return;

            mark[row, col] = true;

            if (matrix[row, col].CompareTo(identity) == 0)
            {
                block.Tiles.Add(new Point<int>(row, col));
                //up
                Search(matrix, identity, TotalRow, TotalCol, mark, block, row - 1, col);
                //down
                Search(matrix, identity, TotalRow, TotalCol, mark, block, row + 1, col);
                //right
                Search(matrix, identity, TotalRow, TotalCol, mark, block, row, col + 1);
                //left
                Search(matrix, identity, TotalRow, TotalCol, mark, block, row, col - 1);
            }
        }

        static void Main(string[] args)
        {
            /* 
             * Given a 2 dimension array like below...
             * Find all adjacent "1" blocks
             * 
             */
            var matrix = new int[,] { 
                                     {0, 1, 1, 1, 0, 0, 1},
                                     {1, 0, 0, 1, 1, 1, 1},
                                     {1, 0, 1, 1, 0, 0, 0},
                                     {1, 1, 0, 0, 0, 0, 0},
                                     {0, 0, 1, 0, 0, 0, 0},
                                     {0, 1, 0, 1, 1, 1, 1},
                                     {1, 0, 0, 0, 0, 0, 0},
                                     {1, 1, 1, 0, 1, 1, 1}
                                    };

            var Row = matrix.GetLength(0);
            var Col = matrix.GetLength(1);

            Console.WriteLine(Row);
            Console.WriteLine(Col);

            var ret = FindAllBlocks(matrix, 1);

            Console.WriteLine(ret.Length);

            foreach (var blk in ret)
            {
                Console.WriteLine(blk.ToString());
            }

            Console.ReadKey();
        }
    }
}
