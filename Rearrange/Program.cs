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
            int[] a = { 5, 14, 3, 7, 6, 8, 1 };
            Console.WriteLine(IsArranged(a));

            Rearrange(a);
            Console.WriteLine(IsArranged(a));

            int[] b = { 1 };
            Rearrange(b);
            Console.WriteLine(IsArranged(a));

            int[] c = { 2 };
            Rearrange(c);
            Console.WriteLine(IsArranged(c));

            int[] d = { 2, 1 };
            Rearrange(d);
            Console.WriteLine(IsArranged(d));

            int[] e = { 1, 2 };
            Rearrange(e);
            Console.WriteLine(IsArranged(e));

            int[] f = { 1, 1, 1 };
            Rearrange(f);
            Console.WriteLine(IsArranged(f));

            int[] g = { 2, 2, 2 };
            Rearrange(g);
            Console.WriteLine(IsArranged(g));
        }
    }
}
