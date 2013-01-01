using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarthestPairInArray
{
    //In an array, find Max(abs(i - j)) where
    // a[i] < a[j], i < j

    class Program
    {
        //Brute force O(n^2)
        static int Bf1(int[] a)
        {
            int ret = 0;
            int n = a.Length;
            for (int i = 0; i < n; i++)
            {
                int cur = 0;
                for (int j = i + 1; j < n ; j++)
                {
                    if(a[i] < a[j])
                        cur = j - i;
                }
                if (cur > ret) ret = cur;
            }
            return ret;
        }

        //Better, but still O(n^2)
        static int Bf2(int[] a)
        {
            int n = a.Length;
            int w = n - 1;
            while (w > 0)
            {
                for (int i = 0; i + w < n; i++)
                {
                    if (a[i] < a[i + w]) return w;
                }
                w--;
            }
            return 0;
        }

        //really smart...2n <= T(n) <= 3n
        static int Smart1(int[] a)
        {
            int n = a.Length;
            int[] maxSofarInReverse = new int[n];
            maxSofarInReverse[n - 1] = a[n - 1];
            for (int i = n - 2; i >= 0; i--)
                maxSofarInReverse[i] = Math.Max(a[i], maxSofarInReverse[i + 1]);

            int ret = 0;
            int k = 0, j = 0;
            while (k < n)
            {
                while (k < n && maxSofarInReverse[k] >= a[j])
                    k++;
                int dis = k - j - 1;
                if (dis > ret) ret = dis;
                while (j < n && k < n && a[j] > maxSofarInReverse[k]) 
                    j++;
            }
            return ret;
        }

        static int[] RandomArray(int len, int min, int max)
        {
            Random rd = new Random();

            int[] ret = new int[len];
            for (int i = 0; i < len; i++)
                ret[i] = rd.Next(min, max);

            for (int i = 0; i < len - 1; i++)
            {
                var p = rd.Next(i + 1, len - 1);
                int t = ret[i];
                ret[i] = ret[p];
                ret[p] = t;
            }
            return ret;
        }

        static void Main(string[] args)
        {
            int[] test1 = {1, 2, 3, 4, 5};
            int[] test2 = { 5, 4, 3, 2, 1 };
            Console.WriteLine(Bf1(test1));
            Console.WriteLine(Bf2(test1));
            Console.WriteLine(Smart1(test1));

            Console.WriteLine(Bf1(test2));
            Console.WriteLine(Bf2(test2));
            Console.WriteLine(Smart1(test2));

            var testrd = RandomArray(50000, 0, 1000000);
            Console.WriteLine(Bf1(testrd));
            Console.WriteLine(Bf2(testrd));
            Console.WriteLine(Smart1(testrd));

            Console.ReadKey();
        }
    }
}
