using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThoughtWorks_Trains
{
    public static class RouteBuilder
    {
        public static Route<char> Build(string route)
        {
            string[] vs = route.Split('-');
            Route<char> ret = new Route<char>();
            foreach (var v in vs)
            {
                ret.AddVertice(v[0]);
            }
            return ret;
        }
    }
}
