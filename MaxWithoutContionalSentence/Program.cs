using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaxWithoutContionalSentence
{
    class Program
    {

        //Based on obversation that accessing array with negative index throws an exception.
        static int MaxWithException(int a, int b)
        {
            int[] foo = new int[2];
            try
            {
                int c = (a - b) >> 31;
                int i = foo[c];
            }
            catch
            {
                return b;
            }
            return a;
        }

        //Based on obversation that adding a positive integer to int.MaxValue throws an exception when checked
        static int MaxWithException2(int a, int b)
        {
            checked
            {
                try
                {
                    int c =  a - b + int.MaxValue;
                }
                catch
                {
                    return a;
                }
                return b;
            }
        }

        //MAX = ((a + b) + |a - b|) / 2
        static int MaxWithFormula(int a, int b)
        {
            return (a + b + (a - b) & int.MaxValue) / 2;
        }

        //Manipulate bits of 2 numbers to get correct result.
        static int MaxWithBitOperation(int a, int b)
        {
            int c = a - b;
            int k = c >> 31 & 0x1;
            return a - c * k;
        }
        

        static void Main(string[] args)
        {
            Console.WriteLine(MaxWithException2(3, 2));
            Console.WriteLine(MaxWithFormula(1, 2));
            Console.WriteLine(MaxWithBitOperation(18, 17));
        }
    }
}
