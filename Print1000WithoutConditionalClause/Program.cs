using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Print1000WithoutConditionalClause
{
    class Program
    {
        //use exception.
        static void PrintWithException()
        {
            try
            {
                for (int i = 1, j = 0; true; i++, j = 1 / (1001 - i))
                    Console.WriteLine(i);
            }
            catch { }
            return;
        }

        static Action<int, Action<int>>[] actions = new Action<int, Action<int>>[2]
        {
            (i, a) => {Console.WriteLine(i); a(i + 1);},
            (i, a) => {return;}
        };

        static void PrintWithDelegateArray(int i)
        {
            actions[i / 1001](i, PrintWithDelegateArray);
        }

        static void Main(string[] args)
        {
            //PrintWithException();
            PrintWithDelegateArray(1);
        }
    }
}
