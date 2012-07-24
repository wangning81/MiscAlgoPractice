using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThoughtWorks_MarsRovers
{
    public class RoverCommander
    {
        private MarsRover rover;
        private int platueX;
        private int platueY;

        public RoverCommander(MarsRover rover, int platueX, int platueY)
        {
            this.rover = rover;
            this.platueX = platueX;
            this.platueY = platueY;
        }

        public void SendCmd(string flyPos, string movCmd)
        {
            string[] pos = flyPos.Split(' ');

            Heading h = Heading.N;
            switch (pos[2])
            {
                case "E": h = Heading.E; break;
                case "S": h = Heading.S; break;
                case "N": h = Heading.N; break;
                case "W": h = Heading.W; break;
            }

            this.rover.FlyTo(int.Parse(pos[0]), int.Parse(pos[1]), h);

            foreach (var c in movCmd)
            { 
                switch(c)
                {
                    case 'M':
                        this.rover.Move();
                        break;
                    case 'L':
                        this.rover.TurnTo(Direction.Left);
                        break;
                    case 'R':
                        this.rover.TurnTo(Direction.Right);
                        break;
                }
            }
        }

        public string GetRoverPosition()
        {
            return rover.ReportPosition();
        }
    }
}
