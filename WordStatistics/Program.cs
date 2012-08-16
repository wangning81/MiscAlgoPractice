using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace WordsStatistics
{
    class WordStatistic : IComparable<WordStatistic>
    {
        public string Word { get; set; }
        public int Count { get; set; }

        public int CompareTo(WordStatistic other)
        {
            return this.Count - other.Count;
        }
    }

    class Program
    {
        /*
         *
         *  A simple phone screen question from Mxxxxxxxx:
         *  
         *  Given an English article, how do you find the 10 words with highest frequency.
         *  
         */
        static void Main(string[] args)
        {
            var file = @"..\..\Menifesto.txt";
            var content = ReadContent(file);
            var wordDict = Statistics(content);
            var wordStats = ConstructStats(wordDict);
            var topTenWords = SelectTop(wordStats, 10);

            foreach (var top in topTenWords.OrderBy(w => w.Count).Reverse())
                Console.WriteLine("WORD = {0}, FREQ = {1}", top.Word, top.Count);

            Console.ReadKey();
        }

        static string ReadContent(string file)
        {
            using (var sr = File.OpenText(file))
            {
                return sr.ReadToEnd();
            }
        }

        static IDictionary<string, int> Statistics(string content)
        {
            var ret = new Dictionary<string, int>();
            var reg = new Regex(@"\w+");
            foreach (Match m in reg.Matches(content))
            {
                var word = m.Value.ToLower();
                if (!ret.ContainsKey(word))
                    ret.Add(word, 0);
                ret[word]++;
            }
            return ret;
        }

        static WordStatistic[] ConstructStats(IDictionary<string, int> dict)
        {
            var ret = new WordStatistic[dict.Count];
            int i = 0;
            foreach(var entry in dict)
            {
                ret[i++] = new WordStatistic() { Word = entry.Key, Count = entry.Value };
            }
            return ret;
        }

        static WordStatistic[] SelectTop(WordStatistic[] statistics, int top)
        {
            var ret = new WordStatistic[top];

            RandomSelect(statistics, statistics.Length - top);

            int k = 0;
            for (int i = statistics.Length - top; i < statistics.Length; i++)
                ret[k++] = statistics[i];
            return ret;
        }

        static T RandomSelect<T>(T[] a, int k) where T : IComparable<T>
        {
            int lo = 0, hi = a.Length - 1;
            while (lo < hi)
            {
                int p = Partition(a, lo, hi);
                if (p == k) return a[k];
                else if (p > k) hi = p - 1;
                else lo = p + 1;
            }
            return a[lo];
        }

        static int Partition<T>(T[] a, int lo, int hi) where T : IComparable<T>
        {
            T piv = a[lo];
            int i = lo + 1, j = hi;
            while (i <= j)
            {
                if (a[i].CompareTo(piv) > 0)
                {
                    Swap(a, i, j--);
                }
                else i++;
            }

            Swap(a, lo, j);
            return j;
        }

        static void Swap<T>(T[] a, int i, int j)
        {
            T t = a[i];
            a[i] = a[j];
            a[j] = t;
        }
    }
}
