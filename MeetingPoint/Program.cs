using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeetingPoint
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] map = new int[,]{ 
                      {2, 0, 1, 7, 4},
                      {3, 6, 5, 8, 2},
                      {0, 0, 0, 1, 0},
                      {3, 3, 5, 2, 7},
                      {1, 2, 2, 2, 1},
                      {3, 8, 8, 2, 8},
                      {4, 2, 6, 1, 7}
                     };

            int ret1 = FindMeetingPoint(map);
            int ret2 = FindMeetingPoint2(map);
            Console.WriteLine(ret1);
            Console.WriteLine(ret2);
            Console.ReadKey();
        }

        //this is trivial O(m^2 * n^2) algo which checks every point on the map.
        private static int FindMeetingPoint(int[,] map)
        {
            int m = map.GetLength(0);
            int n = map.GetLength(1);
            int ret = int.MaxValue;

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    var dist = GetTotalDistance(map, i, j);
                    if (dist < ret)
                        ret = dist;
                }
            }

            return ret;
        }

        private static int GetTotalDistance(int[,] map, int x, int y)
        {
            int m = map.GetLength(0);
            int n = map.GetLength(1);
            int ret = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    ret += map[i, j] * GetDistance(i, j, x, y); 
                }
            }
            return ret;
        }

        private static int GetDistance(int a, int b, int x, int y)
        {
            return Math.Abs(a - x) + Math.Abs(b - y);
        }

        //this is a much better O(mn) algo.
        private static int FindMeetingPoint2(int[,] map)
        {
            int m = map.GetLength(0);
            int n = map.GetLength(1);

            int[] rowCount = new int[m];
            int[] colCount = new int[n];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    rowCount[i] += map[i, j];
                    colCount[j] += map[i, j];
                }
            }

            for (int i = 1; i < m; i++)
            {
                rowCount[i] += rowCount[i - 1];
            }

            for (int j = 1; j < n; j++)
            {
                colCount[j] += colCount[j - 1];
            }

            int p = BinaryFindNearest(rowCount, rowCount[m - 1] / 2);
            int q = BinaryFindNearest(colCount, colCount[n - 1] / 2);

            return GetTotalDistance(map, p, q);

        }

        private static int BinaryFindNearest(int[] a, int v)
        {
            int lo = 0, hi = a.Length - 1;
            while (lo < hi - 1)
            {
                int mid = lo + (hi - lo) / 2;
                if (a[mid] == v)
                    return mid;
                if (a[mid] > v) hi = mid;
                else lo = mid;
            }

            return Math.Abs(a[lo] - v) <= Math.Abs(a[hi] - v) ? lo : hi;
        }


    }
}
