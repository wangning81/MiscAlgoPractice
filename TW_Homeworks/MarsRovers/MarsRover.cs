using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThoughtWorks_MarsRovers
{
    public enum Heading { N = 0, E = 1, S = 2, W = 3};
    public enum Direction { Left, Right };

    public class MarsRover
    {
        private int[] posYX = new int[2];
        private int[] offset = new int[2] { 1, -1 };
        private Heading heading;

        public void FlyTo(int x, int y, Heading heading)
        {
            this.posYX[0] = y;
            this.posYX[1] = x;
            this.heading = heading;
        }

        public void Move()
        {
            posYX[(int)heading % 2] += offset[(int)heading / 2];
        }

        public void TurnTo(Direction dir)
        {
            int newDirect = 4 + (int)(heading + (dir == Direction.Right ? 1 : -1));
            heading = (Heading)(newDirect % 4);
        }

        public string ReportPosition()
        {
            return string.Format("{0} {1} {2}", posYX[1], posYX[0], heading);
        }
    }
}
