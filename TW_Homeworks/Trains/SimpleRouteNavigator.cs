using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThoughtWorks_Trains
{
    public enum MaxStopMatchType { Threshold, Exact };

    struct VertexWithDistance<TVertex>
    {
        public TVertex V { get; set; }
        public double Distance { get; set; }
    }

    public class SimpleRouteNavigator<TVertex>
    {
        private Digraph<TVertex> g;

        public SimpleRouteNavigator(Digraph<TVertex> g)
        {
            this.g = g;
        }

        public int GetNumberOfTrips(TVertex s, TVertex e, int maxStops, MaxStopMatchType matchType)
        {
            int ret = 0;
            IList<Edge<TVertex>> adj = g.GetAdjacent(s);
            int stopCount = 1;

            while (stopCount <= maxStops && adj != null && adj.Count > 0)
            {
                List<Edge<TVertex>> newAdj = new List<Edge<TVertex>>();
                foreach (var edge in adj)
                {
                    if (edge.E.Equals(e))
                    {
                        if (matchType == MaxStopMatchType.Threshold || (matchType == MaxStopMatchType.Exact && stopCount == maxStops))
                            ret++;
                    }
                    newAdj.AddRange(g.GetAdjacent(edge.E));
                }
                adj = newAdj;
                stopCount++;
            }
            return ret;
        }

        public int GetNumberOfLoopTrip(TVertex s, int maxDist)
        {
            int ret = 0;
            Queue<VertexWithDistance<TVertex>> vq = new Queue<VertexWithDistance<TVertex>>();
            foreach (var e in g.GetAdjacent(s))
            {
                vq.Enqueue(new VertexWithDistance<TVertex>() { V = e.E, Distance = e.Weight });
            }

            while (vq.Count > 0)
            {
                var dv = vq.Dequeue();
                foreach (var e in g.GetAdjacent(dv.V))
                {
                    if (e.Weight + dv.Distance < maxDist)
                    {
                        if (e.E.Equals(s)) ret++;
                        vq.Enqueue(new VertexWithDistance<TVertex>()
                                    {
                                        V = e.E,
                                        Distance = e.Weight + dv.Distance
                                    });
                    }
                }
            }

            return ret;
        }

        public double? GetDistance(Route<TVertex> r)
        {
            double ret = 0.0;
            if (r.Length > 0)
            {
                IList<Edge<TVertex>> l = g.GetAdjacent(r[0]);
                if (l == null) return null;

                for (int i = 1; i < r.Length; i++)
                {
                    bool found = false;
                    foreach (var e in l)
                    {
                        if (e.E.Equals(r[i]))
                        {
                            found = true;
                            l = g.GetAdjacent(r[i]);
                            ret += e.Weight;
                            break;
                        }
                    }
                    if (!found) return null;
                }
            }
            return ret;
        }
    }
}
