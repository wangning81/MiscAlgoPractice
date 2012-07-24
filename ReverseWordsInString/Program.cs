using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReverseWordsInString
{
    class Program
    {
        static StringBuilder ReverseString(StringBuilder s, int lo, int hi)
        {
            int mid = lo + (hi - lo) / 2;
            for (int i = lo; i <= mid; i++)
            {
                char c = s[i];
                s[i] = s[hi - i + lo];
                s[hi - i + lo] = c;
            }
            return s;
        }

        static StringBuilder ReverseWordsInString(StringBuilder s)
        {
            //1. reverse the whole string
            int n = s.Length;
            ReverseString(s, 0, n - 1);

            //2. reverse back every word
            int start = 0, end = 0;
            while (end < n)
            {
                while (end < n && s[end] != ' ')
                    end++;
                ReverseString(s, start, end - 1);
                start = ++end;
            }
            return s;
        }

        static void Main(string[] args)
        {
            string s = "the house is blue";
            string s1 = "perfect";
            string s2 = "roses are red, violets are blue";
            Console.WriteLine(ReverseString(new StringBuilder(s), 0, s.Length - 1));
            Console.WriteLine(ReverseWordsInString(new StringBuilder(s)));

            Console.WriteLine(ReverseString(new StringBuilder(s1), 0, s1.Length - 1));
            Console.WriteLine(ReverseWordsInString(new StringBuilder(s1)));


            Console.WriteLine(ReverseString(new StringBuilder(s2), 0, s2.Length - 1));
            Console.WriteLine(ReverseWordsInString(new StringBuilder(s2)));
        }
    }
}
