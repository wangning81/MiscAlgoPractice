using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindValueInSortedMatrix
{
    class Program
    {
        //this is an algorithm wit T(n)~n^1.6+
        static Tuple<int, int> GetPosition(int[,] matrix, int i, int j,
                                           int m, int n, int val)
        {
           
            if (m <= 0 || n <= 0) return null;
            if (m == 1 && n == 1)
            {
                if (matrix[i, j] == val) 
                    return new Tuple<int, int>(i, j);
                return null;
            }
            int mid_i = i + m / 2;
            int mid_j = j + n / 2;
            int a = matrix[mid_i, mid_j];
            if (val == a)
            {
                return new Tuple<int, int>(mid_i, mid_j);
            }
            if (val < a)
            {
                //top-left
                var ret = GetPosition(matrix, i, j, (int)Math.Floor((double)m / 2), (int)Math.Floor((double)n / 2), val);
                if (ret != null) return ret;
                //bottom-left
                ret = GetPosition(matrix, mid_i, j, (int)Math.Ceiling((double)m / 2), (int)Math.Floor((double)n / 2), val);
                if (ret != null) return ret;
                //top-right
                ret = GetPosition(matrix, i, mid_j, (int)Math.Floor((double)m / 2), (int)Math.Ceiling((double)n / 2), val);
                return ret;
            }
            else
            {
                //top-right
                var ret = GetPosition(matrix, i, mid_j + 1, (int)Math.Ceiling((double)m / 2), (int)Math.Floor((double)n / 2), val);
                if (ret != null) return ret;
                //bottom-left
                ret = GetPosition(matrix, mid_i + 1, j, (int)Math.Floor((double)m / 2), (int)Math.Ceiling((double)n / 2), val);
                if (ret != null) return ret;
                //bottom-right
                ret = GetPosition(matrix, mid_i + 1, mid_j + 1, (int)Math.Floor((double)m / 2), (int)Math.Floor((double)n / 2), val);
                return ret;
            }
        }

        //this is an algorithm wit T(n)~2n
        static Tuple<int, int> GetPosition2(int[,] matrix, int val)
        {
            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);
            int i = m - 1, j = 0;
            while (i >= 0 && j < n)
            {
                int c = matrix[i, j];
                if (val < c) i--;
                else if (val > c) j++;
                else return new Tuple<int, int>(i, j);
            }
            return null;
        }

        static void Main(string[] args)
        {
            int[,] m0 = new int[3, 3]
            {
                {2, 5, 12},
                {3, 8, 15},
                {7, 10, 17}
            };

            int[,] m1 = new int[4, 4]
            {
                {-2, 7, 15, 20},
                {3, 8, 18, 22},
                {5, 10, 25, 29},
                {7, 12, 27, 35}
            };

            int[,] m10000 = new int[10000, 10000];
            int k = 0;
            for (int i = 0; i < 10000; i++)
                for (int j = 0; j < 10000; j++)
                    m10000[i, j] = k++;
            double r = Math.Log(10000 * 10000, 4.0 / 3);

            
            var tp = GetPosition2(m10000,55253);
            Console.ReadKey();
        }
    }
}
