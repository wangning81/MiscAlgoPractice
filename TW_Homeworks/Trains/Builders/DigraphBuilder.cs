using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThoughtWorks_Trains
{
    public static class DigraphBuilder
    {
        public static Digraph<char> Build(string s)
        {
            string[] edges = s.Split(',');
            Digraph<char> ret = new Digraph<char>();

            foreach (var e in edges)
            {
                var trimed_e = e.Trim();
                ret.AddEdge(trimed_e[0], trimed_e[1], double.Parse(trimed_e.Substring(2)));
            }
            return ret;
        }
    }
}
