using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinSequenceWithAllCharacters
{
    /*
     * This is a real f2f interview question from M company
     * 
     * Given an array with characters in it, find a MINIMUM length continous subarray
     * that contains all characters appear in the whole array.
     * 
     * E.g.
     * 
     * 0 1 2 3 4 5 6 7 8 9 
     * A A C D E A F Z Q B
     * 
     * answer: 8 (2-9)
     * 
     * 0 1 2 3 4 5
     * A B C C B A
     * 
     * answer: 3 (0-2)
     * 
     * 0 1 2 3 4 5 6 7 8
     * A Z Z Z Z Z Z Z B
     * 
     * answer: 9 (0-8)
     * 
     */

    class Program
    {

        //brute force Time = O(n^2) space = O(n)
        static int MinimumLenBf(char[] a)
        {
            var set = new HashSet<char>();
            foreach (var c in a)
                set.Add(c);

            int n = a.Length;
            int m = set.Count;
            int min = int.MaxValue;

            for (int i = 0; i <= n - m; i++)
            {
                var s = new HashSet<char>();
                for (int j = i; j < n; j++)
                {
                    s.Add(a[j]);
                    if (s.Count == m && j - i + 1 < min)
                    {
                        min = j - i + 1;
                        break;
                    }
                }
            }

            return min;
        }

        //smart Time = O(n), space = O(n)
        static int MinimumLenSmart(char[] a)
        {
            var dict = new Dictionary<char, int>();
            foreach(var c in a)
                if(!dict.ContainsKey(c))
                    dict.Add(c, 0);
            
            int n = a.Length;
            int s = 0, e = 0;
            int found = 0, m = dict.Count;
            int min = int.MaxValue;

            while (e < n)
            {
                while (e < n && found < m)
                {
                    if(dict[a[e]] == 0) found++;
                    dict[a[e++]]++;
                }
                while (true)
                {
                    var c = a[s++];
                    dict[c]--;
                    if (dict[c] == 0)
                    {
                        found--;
                        break;
                    }
                }
                var len = e - s + 1;
                if (len < min)
                    min = len;
            }
            return min;
        }

        static void Main(string[] args)
        {
            var a1 = "AACDEAFZQB";
            var a2 = "ABCCBA";
            var a3 = "AZZZZZZZB";

            Console.WriteLine(MinimumLenBf(a1.ToCharArray()));
            Console.WriteLine(MinimumLenBf(a2.ToCharArray()));
            Console.WriteLine(MinimumLenBf(a3.ToCharArray()));

            Console.WriteLine(MinimumLenSmart(a1.ToCharArray()));
            Console.WriteLine(MinimumLenSmart(a2.ToCharArray()));
            Console.WriteLine(MinimumLenSmart(a3.ToCharArray()));

            Console.ReadKey();
        }
    }
}
