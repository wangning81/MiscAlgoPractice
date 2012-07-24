using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaxSequntialSum
{
    class Program
    {
        static int MaxSequentialSum(int[] a, out int s, out int e)
        {
            int max = int.MinValue;
            int n = a.Length;
            int i = 0;
            s = e = -1;

            while(i < n && a[i] <= 0)
            {
                if (a[i] > max)
                { 
                    max = a[i];
                    s = e = i;
                }
                i++;
            }

            int sum = 0;
            int ts = -1, te = -1;
            for (; i < n; i++)
            {
                if (sum + a[i] > 0)
                {
                    if (ts == -1)
                    {
                        ts = te = i;
                    }

                    sum += a[i];
                    if (sum >= max)
                    {
                        max = sum;
                        te = i;
                    }
                }
                else
                {
                    sum = 0;
                    ts = te = -1;
                }
            }

            if (ts != -1)
            {
                s = ts;
                e = te;
            }

            return max;

        }

        static void Main(string[] args)
        {
            int[] a0 = { -1, 2, 3, 4, -5, -2, -1, 3, 7, 8, 0, -2 };
            int[] a1 = { -1, 2, 3, 4, -5, -10, -1, 3, 7, 8, 0, -2 };
            int[] a2 = { 1, 2, 3, 4, -5, -10, -1, 3, 7, 8, 0, -2 };
            int[] aneg = { -10, -2, -3, -4, -5, -2};

            int[] apos = { 10, 2, 3, 4, 5, 2 };


            int s, e;
            Console.WriteLine(MaxSequentialSum(a1, out s, out e));
            Console.WriteLine(s);
            Console.WriteLine(e);
        }
    }
}
