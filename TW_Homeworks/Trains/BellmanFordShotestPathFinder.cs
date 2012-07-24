using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThoughtWorks_Trains
{
    public class BellmanFordShotestPathFinder<TVertex>
    {
        private Digraph<TVertex> g;
        private IDictionary<TVertex, double> distTo = new Dictionary<TVertex, double>();
        private HashSet<TVertex> onQueueSet = new HashSet<TVertex>();
        private Queue<TVertex> q = new Queue<TVertex>();

        public BellmanFordShotestPathFinder(Digraph<TVertex> g)
        {
            this.g = g;
        }

        private void Init()
        {
            distTo.Clear();
            onQueueSet.Clear();
            q.Clear();

            var V = g.AllVertices;
            foreach (var v in V)
            {
                distTo[v] = double.MaxValue;
            }
        }

        public double Find(TVertex from, TVertex to)
        {
            Init();

            distTo[from] = 0.0;
            onQueueSet.Add(from);
            q.Enqueue(from);

            RelaxAll();
            return distTo[to];
        }

        private void RelaxAll()
        {
            while (q.Count > 0)
            {
                var v = q.Dequeue();
                onQueueSet.Remove(v);
                Relax(v);
            }
        }

        public double FindShortestLoop(TVertex sp)
        {
            Init();

            var adj = g.GetAdjacent(sp);
            foreach (var e in adj)
            {
                distTo[e.E] = e.Weight;
                onQueueSet.Add(e.E);
                q.Enqueue(e.E);
            }

            RelaxAll();
            return distTo[sp];
        }

        private void Relax(TVertex v)
        {
            var adj = g.GetAdjacent(v);
            foreach (var e in adj)
            {
                double newDist = distTo[v] + e.Weight;
                TVertex end = e.E;
                if (newDist < distTo[end])
                {
                    distTo[end] = newDist;
                    if (!onQueueSet.Contains(end))
                    {
                        q.Enqueue(end);
                        onQueueSet.Add(end);
                    }
                }
            }
        }

      }
}
