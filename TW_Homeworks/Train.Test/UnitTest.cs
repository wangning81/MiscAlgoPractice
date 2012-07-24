using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ThoughtWorks_Trains.Test
{
    [TestClass]
    public class UnitTest
    {
        private const string g_s = "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7";
        private Digraph<char> g;
        private SimpleRouteNavigator<char> srn;
        private BellmanFordShotestPathFinder<char> finder;

        public UnitTest()
        { }

        [TestInitialize]
        public void Init()
        {
            g = DigraphBuilder.Build(g_s);
            srn = new SimpleRouteNavigator<char>(g);
            finder = new BellmanFordShotestPathFinder<char>(g);
        }

        [TestMethod]
        public void TestGetRouteDistance()
        {
            IDictionary<string, int?> cases = new Dictionary<string, int?>(){
               { "A-B-C", 9},
               { "A-D", 5},
               { "A-D-C", 13},
               { "A-E-B-C-D", 22},
               { "A-E-D", null}
            };

            foreach (var c in cases)
            {
                var d = srn.GetDistance(RouteBuilder.Build(c.Key));
                Assert.AreEqual(d, c.Value);
            }
        }

        [TestMethod]
        public void TestGetNumberOfTrips()
        {
            Assert.AreEqual(srn.GetNumberOfTrips('C', 'C', 3, MaxStopMatchType.Threshold), 2);
            Assert.AreEqual(srn.GetNumberOfTrips('A', 'C', 4, MaxStopMatchType.Exact), 3);
        }

        [TestMethod]
        public void TestGetShortestPath()
        {
            Assert.AreEqual(finder.Find('A', 'C'), 9);
            Assert.AreEqual(finder.FindShortestLoop('B'), 9);
        }

        [TestMethod]
        public void TestGetDifferentRouteWithThreshold()
        {
            Assert.AreEqual(srn.GetNumberOfLoopTrip('C', 30), 7);
        }
    }
}
