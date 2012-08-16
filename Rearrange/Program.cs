using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rearrange
{
    class Program
    {
        //Rearrange to first odd then even
        static void Rearrange(int[] a)
        {
            int ocount = 0;
            int n = a.Length;
            for (int k = 0; k < n; k++)
                if (a[k] % 2 == 1) ocount++;

            int i = 0, j = ocount;
            while (i < ocount && j < n)
            {
                if (a[i] % 2 == 1) i++;
                else Swap(a, i, j++);
            }
        }

        static void Rearrange2(int[] a)
        {
            int lo = 0, hi = a.Length - 1;
            while (lo < hi)
            {
                while (lo < hi && (a[lo] & 0x1) == 0)
                    Swap(a, lo, hi--);
                lo++;
            }
        }

        // can this be done in space = O(1)?
        static void RearrangeKeepSequence(int[] a)
        {
            int n = a.Length;
            int i = 0, j = 0;
            int[] even = new int[n];
            int[] odd = new int[n];

            foreach (var e in a)
                if ((e & 0x1) == 0) even[j++] = e;
                else odd[i++] = e;

            int p = 0;
            for (int k = i - 1; k >= 0; k--)
                a[p++] = odd[k];
            for (int k = j - 1; k >= 0; k--)
                a[p++] = even[k];
        }

        static void Swap<T>(T[] a, int i, int j)
        {
            T t = a[i];
            a[i] = a[j];
            a[j] = t;
        }

        static void Print<T>(T[] a)
        {
            foreach (T t in a)
                Console.WriteLine(t);
        }

        static bool IsArranged(int[] a)
        {
            int n = a.Length;
            int i = 0;
            while (i < n && a[i++] % 2 == 1)
                ;
            while (i < n && a[i++] % 2 == 0)
                ;
            return i == n;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("1st function");
            Test(Rearrange);
            Console.WriteLine("2nd function");
            Test(Rearrange2);
            Console.WriteLine("3rd function");
            Test(RearrangeKeepSequence);
            Console.ReadKey();
        }

        static void Test(Action<int[]> Act)
        {
            int[] a = { 5, 14, 3, 7, 6, 8, 1 };
            Console.WriteLine(IsArranged(a));

            Act(a);
            Console.WriteLine(IsArranged(a));

            int[] b = { 1 };
            Act(b);
            Console.WriteLine(IsArranged(a));

            int[] c = { 2 };
            Act(c);
            Console.WriteLine(IsArranged(c));

            int[] d = { 2, 1 };
            Act(d);
            Console.WriteLine(IsArranged(d));

            int[] e = { 1, 2 };
            Act(e);
            Console.WriteLine(IsArranged(e));

            int[] f = { 1, 1, 1 };
            Act(f);
            Console.WriteLine(IsArranged(f));

            int[] g = { 2, 2, 2 };
            Act(g);
            Console.WriteLine(IsArranged(g));
        }
    }
}
