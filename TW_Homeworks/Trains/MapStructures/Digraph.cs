using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThoughtWorks_Trains
{
    public struct Edge<TVertex>
    {
        public TVertex S { get; set; }
        public TVertex E { get; set; }
        public double Weight { get; set; }
    }

    public class Digraph<TVertex>
    {
        private IDictionary<TVertex, IList<Edge<TVertex>>> adjDict
                                    = new Dictionary<TVertex, IList<Edge<TVertex>>>();

        private int vcount;
        private int VCount
        {
            get { return vcount; }
        }

        public Digraph()
        {
        }

        public void AddVertex(TVertex v)
        {
            if (!adjDict.ContainsKey(v))
            {
                adjDict.Add(v, new List<Edge<TVertex>>());
                vcount++;
            }
            else throw new ArgumentException("vertex " + v + " has been in Digraph");
        }

        public void AddEdge(Edge<TVertex> edge)
        {
            TVertex start = edge.S;
            TVertex end = edge.E;

            if (!adjDict.ContainsKey(start))
                AddVertex(start);
            if (!adjDict.ContainsKey(end))
                AddVertex(end);

            adjDict[start].Add(edge);
        }

        public void AddEdge(TVertex from, TVertex to, double weight)
        {
            AddEdge(new Edge<TVertex>() { S = from, E = to, Weight = weight });
        }

        public IEnumerable<TVertex> AllVertices
        {
            get
            {
                return adjDict.Keys;
            }
        }

        public IList<Edge<TVertex>> GetAdjacent(TVertex s)
        {
            if (adjDict.ContainsKey(s))
                return adjDict[s];
            return null;
        }
    }
}
