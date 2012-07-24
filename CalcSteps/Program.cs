using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalcSteps
{
    class Program
    {

        #region premitive methods

        static int CalcStepsPrimitive(int n)
        {
            if (n < 0) return 0;
            if (n == 0) return 1;
            return CalcStepsPrimitive(n - 1) + CalcStepsPrimitive(n - 2) + CalcStepsPrimitive(n - 5);
        }

        static int CalcStepsPrimitiveDiff(int n, int prevStep = 1)
        {
            if (n < 0) return 0;
            if (n == 0) return 1;
            int ret = 0;
            if (prevStep <= 5)
                ret += CalcStepsPrimitiveDiff(n - 5, 5);
            if (prevStep <= 2)
                ret += CalcStepsPrimitiveDiff(n - 2, 2);
            if (prevStep <= 1)
                ret += CalcStepsPrimitiveDiff(n - 1, 1);
            return ret;
        }

        static long CalcStepsRec(int n, Stack<int> trace, int bound)
        {
            if (n < 0)
            {
                return 0;
            }
            if (n == 0)
            {
                PrintStack(trace);
                return 1;
            }

            long oneStepRet = 0;
            long twoStepRet = 0;
            long fiveStepRet = 0;
            if (bound == 1)
            {
                trace.Push(1);
                oneStepRet = CalcStepsRec(n - 1, trace, 1);
                trace.Pop();
            }


            if (bound <= 2)
            {
                trace.Push(2);
                twoStepRet = CalcStepsRec(n - 2, trace, 2);
                trace.Pop();
            }

            if (bound <= 5)
            {
                trace.Push(5);
                fiveStepRet = CalcStepsRec(n - 5, trace, 5);
                trace.Pop();
            }

            return oneStepRet + twoStepRet + fiveStepRet;
        }

        private static void PrintStack<T>(Stack<T> trace)
        {
            foreach (T ss in trace)
            {
                Console.Write(ss + " ");
            }
            Console.WriteLine();
        }

        static int CalcStepNonRec(int n)
        {
            Stack<int> s = new Stack<int>(n + 1);
            int result = 0;
            int last = -1, next = 1;
            while (true)
            {
                if (n == 0)
                {
                    PrintStack(s);
                    result++;
                }
                if (n <= 0)
                {
                    while (s.Count != 0)
                    {
                        last = s.Pop();
                        n += last;
                        if (last != 5)
                            break;
                    }
                    if (last == 5 && s.Count == 0)
                        break;
                    else
                        next = last == 1 ? 2 : 5;
                }
                s.Push(next);
                n -= next;
                if (next == 1) next = 1;
                else if (next == 2) next = 2;
                else next = 5;
            }
            return result;
        }

        #endregion

        #region dp methods

        #region primitive

        static long CalcStepDp(int n)
        {
            long[] cache = new long[n + 1];
            cache[0] = 1;
            for (int i = 1; i <= n; i++)
            {
                cache[i] = cache[i - 1];
                if (i - 2 >= 0)
                    cache[i] += cache[i - 2];
                if (i - 5 >= 0)
                    cache[i] += cache[i - 5];
            }
            return cache[n];
        }

        static long CalcDiffStepDp(int n)
        {
            long[] cacheFor1 = new long[n + 1];
            cacheFor1[0] = 1;

            long[] cacheFor2 = new long[n + 1];
            cacheFor2[0] = cacheFor2[1] = 0;

            long[] cacheFor5 = new long[n + 1];
            cacheFor5[0] = cacheFor5[1] = cacheFor5[2] = cacheFor5[3] = cacheFor5[4] = 0;

            for (int i = 1; i <= n; i++)
            {
                cacheFor1[i] = cacheFor1[i - 1];
                if (i - 2 >= 0)
                    cacheFor2[i] = cacheFor1[i - 2] + cacheFor2[i - 2];
                if (i - 5 >= 0)
                    cacheFor5[i] = cacheFor1[i - 5] + cacheFor2[i - 5] + cacheFor5[i - 5];
            }

            return cacheFor1[n] + cacheFor2[n] + cacheFor5[n];
        }
        #endregion

        #region withAssociateArray
        
        static long CalcStepDpWithAssociateArray(int n)
        {
            var cache = new Dictionary<int, long>(6);
            cache[0] = 1;
            for (int i = 1; i <= n; i++)
            {
                cache[i] = (i - 1 >= 0 ? cache[i - 1] : 0)
                           +
                           (i - 2 >= 0 ? cache[i - 2] : 0)
                           +
                           (i - 5 >= 0 ? cache[i - 5] : 0);

                if (cache.ContainsKey(i - 5))
                    cache.Remove(i - 5);
            }
            return cache[n];
        }

        static long CalcStepDpWithAssociateArrayDiff(int n)
        {
            var cacheFor1 = new Dictionary<int, long>(6);
            var cacheFor2 = new Dictionary<int, long>(6);
            var cacheFor5 = new Dictionary<int, long>(6);

            cacheFor1[0] = 1;
            cacheFor2[0] = cacheFor2[1] = 0;
            cacheFor5[0] = cacheFor5[1] = cacheFor5[2] = cacheFor5[3] = cacheFor5[4] = 0;

            for (int i = 1; i <= n; i++)
            {
                cacheFor1[i] = cacheFor1[i - 1];
                if (i - 2 >= 0)
                    cacheFor2[i] = cacheFor2[i - 2] + cacheFor1[i - 2];
                if (i - 5 >= 0)
                    cacheFor5[i] = cacheFor1[i - 5] + cacheFor2[i - 5] + cacheFor5[i - 5];

                if (cacheFor1.ContainsKey(i - 5))
                {
                    cacheFor1.Remove(i - 5);
                }

                if (cacheFor2.ContainsKey(i - 5))
                {
                    cacheFor2.Remove(i - 5);
                }

                if (cacheFor5.ContainsKey(i - 5))
                {
                    cacheFor5.Remove(i - 5);
                }
            }
            return cacheFor5[n] + cacheFor2[n] + cacheFor1[n];
        }

        #endregion

        #region withModular
        static long CalcStepDpWithModular(int n)
        {
            long[] cache = new long[5];
            cache[0] = 1;
            for (int i = 1; i <= n; i++)
            {
                cache[i % 5] = (i - 1 >= 0 ? cache[(i - 1) % 5] : 0)
                                +
                                (i - 2 >= 0 ? cache[(i - 2) % 5] : 0)
                                +
                                (i - 5 >= 0 ? cache[(i - 5) % 5] : 0);
            }
            return cache[n % 5];
        }
        #endregion

        #endregion

        static void Main(string[] args)
        {
            Stack<int> s = new Stack<int>();
            //Console.WriteLine(CalcStepsRec(30, s, 1));
            //Console.WriteLine("=====================");
            //Console.WriteLine(CalcStep(20));
            //Console.WriteLine("=====================");
            //Console.WriteLine(CalcStepsPrimitiveDiff(30));
            //Console.WriteLine(CalcDiffStepDp(30));
            //Console.WriteLine("=====================");
            //Console.WriteLine(CalcStepDpWithAssociateArrayDiff(30));
            //Console.WriteLine("=====================");
            //Console.WriteLine(CalcStepsPrimitive(50));
            Console.WriteLine(CalcStepDpWithAssociateArray(50));
            Console.WriteLine(CalcStepDpWithModular(50));
        }
    }
}
