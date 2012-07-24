using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Globalization;

namespace ToChineseNumber
{
    class Program
    {
        static readonly string[] InnerUnit = new string[] { "", "十", "百", "千" };
        static readonly string[] SignificantUnit = new string[] { "", "万", "亿", "万亿", "亿亿", "万亿亿" };
        static readonly string[] ChineseNumber = new string[] { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };

        static void Main(string[] args)
        {
            var n1 = ToChineseNumber(1234567899);
            var n2 = ToChineseNumber(1200007899);
            var n3 = ToChineseNumber(120000909);
            var n4 = ToChineseNumber(12000000);
            var n5 = ToChineseNumber(int.MaxValue);
            var n6 = ToChineseNumber(long.MaxValue);
            Console.WriteLine(n1);
            Console.ReadKey();
        }

        static string ToChineseNumber(long n)
        {
            if (n == 0) return ChineseNumber[n];
            StringBuilder sb = new StringBuilder();
            for (int i = 0; n > 0; i++)
            {
                int d = (int)(n % 10);
                if (i % InnerUnit.Length == 0)
                {
                    if (n % 10000 > 0)
                        sb.Insert(0, SignificantUnit[(i / InnerUnit.Length) % SignificantUnit.Length]);
                }
                if (d > 0)
                { 
                    sb.Insert(0, ChineseNumber[d] + InnerUnit[i % InnerUnit.Length]);
                }
                n /= 10;
            }
            return sb.ToString();
        }
    }
}
