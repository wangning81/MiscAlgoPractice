using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThoughtWorks_Trains
{
    class Program
    {
        static string[] rs_arr = new string[]{
                    "A-B-C",
                    "A-D",
                    "A-D-C",
                    "A-E-B-C-D",
                    "A-E-D"
        };

        static void Main(string[] args)
        {
            string s = "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7";
            Digraph<char> g = DigraphBuilder.Build(s);
            SimpleRouteNavigator<char> srn = new SimpleRouteNavigator<char>(g);

            foreach (var rs in rs_arr)
            {
                Route<char> r = RouteBuilder.Build(rs);
                Console.WriteLine(srn.GetDistance(r));
            }

            Console.WriteLine(srn.GetNumberOfTrips('C', 'C', 3, MaxStopMatchType.Threshold));
            Console.WriteLine(srn.GetNumberOfTrips('A', 'C', 4, MaxStopMatchType.Exact));

            BellmanFordShotestPathFinder<char> finder = new BellmanFordShotestPathFinder<char>(g);
            Console.WriteLine(finder.Find('A', 'C'));
            Console.WriteLine(finder.FindShortestLoop('B'));

            Console.WriteLine(srn.GetNumberOfLoopTrip('C', 30));
        }
    }
}
