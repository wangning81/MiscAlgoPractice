using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CountSort
{
    class Program
    {
        static bool Equal<T>(T[] a, T[] b)
        {
            for (int i = 1; i < a.Length; i++)
                if (!a[i].Equals(b[i]))
                    return false;
            return true;
        }

        static bool IsSorted<T>(T[] a) where T : IComparable<T>
        {
            for (int i = 1; i < a.Length; i++)
                if (a[i].CompareTo(a[i - 1]) < 0)
                    return false;
            return true;
        }

        static void PrintArray<T>(T[] a)
        {
            foreach (var t in a)
                Console.Write(t.ToString() + " ");
        }

        static void CountingSort(int[] a)
        {
            int R = 200;
            int[] count = new int[R];
            int n = a.Length;
            for (int i = 0; i < n; i++)
                count[a[i]]++;

            int k = 0;
            for (int i = 0; i < R; i++)
                for (int j = 0; j < count[i]; j++)
                    a[k++] = i;
            return;
        }

        static int[] GenerateArray(int n, int minVal, int maxVal)
        {
            int[] ret = new int[n];
            Random r = new Random();
            for (int i = 0; i < n; i++)
                ret[i] = r.Next(minVal, maxVal);
            return ret;
        }

        static void Main(string[] args)
        {
            var a = GenerateArray(10000, 0, 150);
            var b = new int[a.Length];
            Array.Copy(a, b, a.Length);
            Console.WriteLine(Equal(a, b));
            Console.WriteLine(IsSorted(a));
            CountingSort(a);
            Console.WriteLine(Equal(a, b));
            Array.Sort(b);
            Console.WriteLine(IsSorted(a));
            Console.WriteLine(Equal(a, b));
            Console.ReadKey();
        }
    }
}
