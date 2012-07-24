using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpEval
{
    class Program
    {
        static double ExpEval(string exp)
        {
            Stack<char> op = new Stack<char>();
            Stack<double> operand = new Stack<double>();

            for (int i = 0; i < exp.Length; )
            {
                char c = exp[i];

                if (c == ' ')
                {
                    i++;
                }
                else if (c == ')')
                {
                    while (op.Peek() != '(')
                    {
                        double result = EvalOnce(operand.Pop(), op.Pop(), operand.Pop());
                        operand.Push(result);
                    }
                    op.Pop();
                    i++;
                }
                else if (IsNumber(c))
                {
                    operand.Push(NextVal(exp, i, out i));
                }
                else
                {
                    if (c == '*' || c == '/')
                    {
                        double result = EvalOnce(operand.Pop(), c, NextVal(exp, i, out i));
                        operand.Push(result);
                    }
                    else
                    {
                        op.Push(c);
                        i++;
                    }
                }
            }

            while (op.Count != 0)
            {
                double result = EvalOnce(operand.Pop(), op.Pop(), operand.Pop());
                operand.Push(result);
            }

            return operand.Pop();
        }

        private static double EvalOnce(double lhs, char op, double rhs)
        {
            switch (op)
            {
                case '+':
                    return lhs + rhs;
                case '-':
                    return lhs - rhs;
                case '*':
                    return lhs * rhs;
                case '/':
                    return lhs / rhs;
                default:
                    throw new InvalidOperationException();
            }
        }

        static double NextVal(string exp, int s, out int e)
        {
            while (!IsNumber(exp[s]))
                s++;
            
            int i = s;
            double val = 0.0;
            for (; i < exp.Length && IsNumber(exp[i]); i++)
                val = 10 * val + ToNumber(exp[i]);

            if (i < exp.Length && exp[i] == '.')
            {
                i++;
                for (double multi = 0.1; i < exp.Length && IsNumber(exp[i]); i++, multi *= 0.1)
                    val += (exp[i] - '0') * multi;
            }

            e = i;

            return val;
        }

        static bool IsNumber(char c)
        {
            return c <= '9' && c >= '0';
        }

        static int ToNumber(char c)
        {
            return c - '0';
        }

        static void Main(string[] args)
        {
            string one_one = "1 + 1";
            string s = "12.5 + 37.4";
            string ss = "(12.44 * 12 / 3 + (14.3 + 15.7 + 33.6) * 23.2 + 22) / 123.66 + 7";


            Console.WriteLine(ExpEval(ss));

        }
    }
}
