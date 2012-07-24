using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThoughtWorks_MarsRovers
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] cmds = new string[]{
                            "5 5",
                            "1 2 N",
                            "LMLMLMLMM",
                            "3 3 E",
                            "MMRMMRMRRM"
                };

            MarsRover r = new MarsRover();
            string[] plXY = cmds[0].Split(' ');
            RoverCommander rc = new RoverCommander(r, int.Parse(plXY[0]), int.Parse(plXY[1]));

            for (int i = 1; i < cmds.Length;)
            {
                rc.SendCmd(cmds[i++], cmds[i++]);
                Console.WriteLine(rc.GetRoverPosition());
            }
        }
    }
}
