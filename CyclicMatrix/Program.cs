using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CyclicMatrix
{
    class Program
    {

        static int GetCell(int n, int s, int i, int j)
        { 
            //level k top-left corner formular = s + 4 * k * (n - k), where k >= 0.
            int k = Math.Min(Math.Min(i, n - 1 - i), Math.Min(j, n - 1 - j));
            int m = n - 2 * k;
            int pivot = s + 4 * k * (n - k);
            if (i == k) //on the top
                return pivot + j - k;
            else if (j == k + m - 1) //on the right
                return pivot + m - 1 + i - k;
            else if (i == k + m - 1) //on the bottom
                return pivot + 3 * (m - 1) + k - j;
            else //on the left
                return pivot + 4 * (m - 1) + k - i;
        }
        static void Main(string[] args)
        {
            int n = 20;
            for(int i = 0 ; i < n ; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write(GetCell(n, 1, i, j) + " ");
                Console.WriteLine();
            }
        }
    }
}
