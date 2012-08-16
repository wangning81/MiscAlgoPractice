using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllPermutations
{
    class Program
    {
        /*
         *
         * Interview question from Axxxxx.cn
         * 
         * Print all permutations of 1, 2, 3, 4, 5
         * 
         * E.g. all permutaitons of 1, 2, 3 are...
         * 
         * 1 2 3
         * 1 3 2
         * 2 1 3
         * 2 3 1
         * 3 1 2
         * 3 2 1
         *  
         */

        static IList<string> GetAllPermutations(int[] a)
        {
            var ret = new List<string>();
            StringBuilder sb = new StringBuilder();
            GetAllPermutations(ret, sb, a, 0);
            return ret;
        }

        //O(factorial(n)) algorithm. Since all permuation of an array has factorial(n) possibility
        //this can be seen as the optimal algorithm. Space = O(n)
        static void GetAllPermutations(IList<string> ret, StringBuilder sb, int[] a, int index)
        {
            int n = a.Length;
            if (index == n)
            {
                ret.Add(sb.ToString());
                return;
            }

            for (int i = index; i < n; i++)
            {
                Swap(a, i, index);
                sb.Append(a[index]);
                GetAllPermutations(ret, sb, a, index + 1);
                sb.Remove(sb.Length - 1, 1);
                Swap(a, i, index);
            }
        }

        //this is another approach - we don't try to permute the array but exam all possible
        //numbers from 100...0 to n00...0.
        static IList<string> AllPermutationsForFive()
        {
            var ret = new List<string>();

            for (int i = 10000; i < 60000; i++)
            {
                var mark = new bool[6];
                int k = i;
                while (k > 0)
                {
                    int d = k % 10;
                    if (d < 1 || d > 5 || mark[d]) break;
                    mark[d] = true;
                    k /= 10;
                }
                if (k == 0) ret.Add(i.ToString());
            }
            return ret;
        }

        private static void Swap(int[] a, int i, int j)
        {
            int t = a[i];
            a[i] = a[j];
            a[j] = t;
        }

        private static bool NoDup(IList<string> lst)
        {
            var set = new HashSet<string>();
            foreach (var e in lst)
                if (!set.Add(e)) return false;
            return true;
        }
        
        static void Main(string[] args)
        {
            var ret1 = GetAllPermutations(new int[] { 1, 2, 3, 4, 5 });
            var ret2 = AllPermutationsForFive();

            Console.WriteLine(NoDup(ret1));
            Console.WriteLine(NoDup(ret2));

            Console.ReadKey();
        }
    }
}
