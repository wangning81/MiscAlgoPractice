using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 取胜之道
 * 题目描述：
 * Program国度的人，喜欢玩这样一个游戏，在一块板上写着一行数，共n个。
 * 两个游戏者，轮流从最右或最左取一个数。刚开始，每个游戏者的得分均为零。
 * 如果一个游戏者取下一个数，则将该数的值加到该游戏者的得分上，最后谁的得分最高谁就赢了游戏。
 * 给出这n个数(从左往右), 假设游戏者都是非常聪明的，问最后两个人的得分(假设第一个人首先取数)．
 * 输入格式：第一行为n(2<=n<=100)，第二行为n个数，每个数字之间均用空格隔开。
 * 输出为两个游戏者的得分．第一个数表示第一个游戏者的得分，第二个数为第二个游戏者的得分，两个数字之间用空格隔开。
 * 样例输入：
 * 6
 * 4 7 2 9 5 2
 * 样例输出：
 * 18 11
*/

namespace TwoPartiesChoice
{
    struct Result
    {
        public Result(int f, int s)
        {
            First = f;
            Second = s;
        }
        public void Swap()
        {
            int t = First;
            First = Second;
            Second = t;
        }
        public int First;
        public int Second;

        public override string ToString()
        {
            return string.Format("First = {0}, Second = {1}", First, Second);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int[] conf = new int[] { 4, 7, 2, 9, 5, 2 };
            Console.WriteLine(DpFindSolution(conf).ToString());
        }

        static Result FindSolution(int[] conf, int lo, int hi)
        {
            if (lo == hi) return new Result(conf[lo], 0);
            
            Result r1 = FindSolution(conf, lo + 1, hi);
            r1.Swap();
            r1.First += conf[lo];

            Result r2 = FindSolution(conf, lo, hi - 1);
            r2.Swap();
            r2.First += conf[hi];

            return r1.First >= r2.First ? r1 : r2;
        }

        static Result DpFindSolution(int[] conf)
        {
            int n = conf.Length;
            Result[,] retTable = new Result[n, n];
            
            for (int i = 0; i < n; i++)
                retTable[i, i] = new Result(conf[i], 0);

            for (int gap = 1; gap < n ; gap++)
            {
                for (int i = 0; i + gap < n; i++)
                { 
                    Result l = retTable[i, i + gap - 1];
                    Result r = retTable[i + 1, i + gap];
                    int chooseL = r.Second + conf[i];
                    int chooseR = l.Second + conf[i + gap];

                    if (chooseL >= chooseR)
                        retTable[i, i + gap] = new Result(chooseL, r.First);
                    else
                        retTable[i, i + gap] = new Result(chooseR, l.First);
                }
            }

            return retTable[0, n - 1];
        }
    }
}
