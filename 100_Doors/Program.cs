using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _100_Doors
{
    [Flags]
    enum DoorStatus { Close, Open };

    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i <= 100; i++)
            {
                Console.WriteLine(GetStatus(i) + ", ");
            }
        }

        static DoorStatus GetStatus(int doorNo)
        {
            int sqr = (int)Math.Sqrt(doorNo);
            HashSet<int> factors = new HashSet<int>();
            for (int i = 1; i <= sqr; i++)
            {
                if (doorNo % i == 0)
                {
                    factors.Add(i);
                    factors.Add(doorNo / i);
                }
            }
            return factors.Count % 2 == 0 ? DoorStatus.Close : DoorStatus.Open;
        }
    }
}
