using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataStructure.Elementary;

namespace BuySellApples
{
    /* Suppose you have a truck and there're n STOPS. 
     * 0. Initially the truck is empty and at each stop you can buy or sell apples.
     * 1. It's one-way road, you cannot go back.
     * Below is an exmpale:
     * 
     * X-------X-------X-------X-------X-------X
     * B[0]=5  B[1]=4  B[2]=8  B[3]=2  B[4]=7  B[5]=9
     * S[0]=3  S[0]=3  S[0]=7  S[0]=1  S[0]=6  S[0]=7
     * 
     * Q1: Suppose you can only buy ONCE and sell ONCE;
     * Q2: Suppose you can buy/sell at multiple stops, but cannot buy once before sell;
     * Q3: Suppose you can buy at multiple stops (but only once in a specific stop) before sell;
     * Q4: Suppose you can buy/sell at multiple stops and at every stop 
     *      there's a max 'buy amount' and 'sell amount' 
     *          (the truck can take arbitary amount of apples).
     * Q5: Same as Q4 but the truck has limited amount.
     * 
     * find the buy/sell positions to maximaze the profit
     * 
    */
    class Program
    {
        static void Main(string[] args)
        {
            int[] B = new int[] { 5, 4, 8, 2, 7, 9 };
            int[] S = new int[] { 3, 3, 7, 1, 6, 7 };
            int[] BA = new int[] { 2, 5, 1, 3, 4, 6 };
            int[] SA = new int[] { 1, 3, 2, 5, 4, 2};

            Console.WriteLine(Q1BF(B, S));
            Console.WriteLine(Q1BT(B, S));
            Console.WriteLine(Q1DC(B, S));
            Console.WriteLine(Q1Direct(B, S));
            Console.WriteLine(Q2BF(B, S));
            Console.WriteLine(Q2DP(B, S));

            Console.WriteLine(Q3BF(B, S));

            Console.ReadKey();
        }

        

        //straightforward brute force n^2 algorithm
        static int Q1BF(int[] buys, int[] sells)
        {
            int max = 0;
            for (int i = 0; i < buys.Length; i++)
                for (int j = i; j < sells.Length; j++)
                    if (sells[j] - buys[i] > max)
                        max = sells[j] - buys[i];
            return max;
        }


        class ValueWithIndex : IComparable<ValueWithIndex>
        {
            public int Val { get; set; }
            public int Index { get; set; }

            public int CompareTo(ValueWithIndex other)
            {
                return Val - other.Val;               
            }

        }

        //find the min buy and find the max sell after it then we get a candidate profit.
        //then all buys/sells after this index need not to be considered.
        //an nlgn (build heap) + nlgn (extract min buy) + n (check all sells) algorithm using heap
        //can be optimized to n + nlgn + n though.
        static int Q1BT(int[] buys, int[] sells)
        {
            int max = 0;
            
            MinPQ<ValueWithIndex> pq = new MinPQ<ValueWithIndex>();
            for(int i = 0 ; i < buys.Length ; i++)
                pq.Enqueue(new ValueWithIndex() { Val = buys[i], Index = i });

            int rightBound = sells.Length;
            
            while (pq.Count > 0)
            {
                var buyWithIndex = pq.Dequeue();
                if (buyWithIndex.Index < rightBound)
                {
                    int maxSell = 0;
                    for (int i = buyWithIndex.Index; i < rightBound; i++)
                        if (sells[i] > maxSell)
                            maxSell = sells[i];

                    if (maxSell - buyWithIndex.Val > max)
                        max = maxSell - buyWithIndex.Val;

                    rightBound = buyWithIndex.Index;
                }
            }

            return max;
        }


        //The linear divide and conquer algorithm...
        static int Q1DC(int[] buys,int[] sells)
        {
            return Q1DC(buys, sells, 0, buys.Length - 1);
        }

        static int Q1DC(int[] buys, int[] sells, int lo, int hi)
        {
            int minBuy, maxSell;
            return Q1DCImp(buys, sells, 0, buys.Length - 1, out minBuy, out maxSell);
        }

        static int Q1DCImp(int[] buys, int[] sells, int lo, int hi, out int minBuy, out int maxSell)
        {
            if (lo == hi)
            {
                minBuy = buys[lo];
                maxSell = sells[lo];
                return Math.Max(sells[lo] - buys[lo], 0);
            }
            int mid = lo + (hi - lo) / 2;

            int leftMinBuy, rightMinBuy;
            int leftMaxSell, rightMaxSell;

            int leftProfit = Q1DCImp(buys, sells, lo, mid, out leftMinBuy, out leftMaxSell);
            int rightProfit = Q1DCImp(buys, sells, mid + 1, hi, out rightMinBuy, out rightMaxSell);

            minBuy = Math.Min(leftMinBuy, rightMinBuy);
            maxSell = Math.Max(leftMaxSell, rightMaxSell);

            return Math.Max(Math.Max(Math.Max(leftProfit, rightProfit), rightMaxSell - leftMinBuy), 0);
        }

        static int Q1Direct(int[] buys, int[] sells)
        {
            int maxProfit = 0;
            int minBuy = int.MaxValue;
            for (int i = 0; i < buys.Length; i++)
            {
                if (buys[i] < minBuy)
                    minBuy = buys[i];

                int newProfit = sells[i] - minBuy;
                if (maxProfit < newProfit)
                    maxProfit = newProfit;
            }
            return maxProfit;
        }

        //Brute force algorithm, T(n) = 2Sum(k = 1...n-1)T(k) + n = O(2^n)
        static int Q2BF(int[] buys, int[] sells)
        {
            return Q2BFImp(buys, sells, 0, buys.Length - 1);
        }


        static int Q2BFImp(int[] buys, int[] sells, int lo, int hi)
        {
            if (lo > hi) return 0;
            if (lo == hi) return Math.Max(sells[lo] - buys[lo], 0);
            int splitMax = 0;
            for (int i = lo; i < hi; i++)
            {
                int left = Q2BFImp(buys, sells, lo, i);
                int right = Q2BFImp(buys, sells, i + 1, hi);
                if (left + right > splitMax)
                    splitMax = left + right;
            }
            
            int dontSplitMax = Q1DC(buys, sells, lo, hi);

            return Math.Max(splitMax, dontSplitMax);
        }

        static int Q2DP(int[] buys, int[] sells)
        {
            int n = buys.Length;
            int[,] dpTable = new int[n, n];
            for (int i = 0; i < n; i++)
                dpTable[i, i] = Math.Max(0, sells[i] - buys[i]);

            for (int l = 1; l < n; l++)
            {
                for (int i = 0; i + l < n; i++)
                {
                    int j = i + l;
                    for (int k = i; k < j; k++)
                    {
                        int sum = dpTable[i, k] + dpTable[k + 1, j];
                        if (sum > dpTable[i, j])
                            dpTable[i, j] = sum;
                    }
                    
                    int dontSplitMax = Q1DC(buys, sells, i, j);
                    if (dontSplitMax > dpTable[i, j])
                        dpTable[i, j] = dontSplitMax;
                }
            }

            return dpTable[0, n - 1];
        }

        //straightfoward O(n^2) algorithm
        private static int Q3BF(int[] B, int[] S)
        {
            int n = B.Length;
            int max = 0;
            for (int i = 0; i < n; i++)
            {
                int sell = S[i];
                int profit = 0;
                for (int j = i; j >= 0; j--)
                {
                    if (sell - B[j] > 0)
                    {
                        profit += sell - B[j];
                    }
                }
                if (profit > max)
                    max = profit;
            }
            return max;

        }
    }
}
