using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThoughtWorks_Trains
{
    public class Route<TVertex>
    {
        private List<TVertex> vertices = new List<TVertex>();
        public List<TVertex> Vertices
        {
            get { return vertices; }
        }

        public void AddVertice(TVertex v)
        {
            vertices.Add(v);
        }

        public TVertex this[int i]
        {
            get
            {
                return vertices[i];
            }
        }

        public int Length
        {
            get
            {
                return vertices.Count;
            }
        }
    }
}
