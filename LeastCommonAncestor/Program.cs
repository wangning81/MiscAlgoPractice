using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeastCommonAncestor
{
    class BinarySearchNode<T> where T : IComparable<T>
    {
        public T Data { get; set; }
        public BinarySearchNode<T> Left { get; set; }
        public BinarySearchNode<T> Right { get; set; }
        public BinarySearchNode(T data, BinarySearchNode<T> left, BinarySearchNode<T> right)
        {
            Data = data;
            Left = left;
            Right = right;
        }
    }

    class BinaryNode<T>
    {
        public T Data { get; set; }
        public BinaryNode<T> Left { get; set; }
        public BinaryNode<T> Right { get; set; }

        public BinaryNode(T data, BinaryNode<T> left, BinaryNode<T> right)
        {
            Data = data;
            Left = left;
            Right = right;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BinarySearchNode<int> root = new BinarySearchNode<int>
                (
                    6,
                    new BinarySearchNode<int>(
                                            2,
                                            new BinarySearchNode<int>(1, null, null),
                                            new BinarySearchNode<int>(3, null, null)),
                    new BinarySearchNode<int>(
                                            10,
                                            new BinarySearchNode<int>(9,
                                                                    new BinarySearchNode<int>(
                                                                           8,
                                                                           new BinarySearchNode<int>(7, null, null),
                                                                           null),
                                                                       null),
                                           null
                                            )
                );

            Console.WriteLine(FindLeastCommonAncestor(root, 2, 7).Data);

            var node3 = new BinaryNode<int>(3, null, null);
            var node17 = new BinaryNode<int>(17, null, null);
            BinaryNode<int> rt = new BinaryNode<int>(2,
                                                     new BinaryNode<int>(7,
                                                                         node3,
                                                                         new BinaryNode<int>(8,
                                                                                             new BinaryNode<int>(15,
                                                                                                                 node17,
                                                                                                                 null),
                                                                                             null)
                                                                         ),
                                                     new BinaryNode<int>(6,
                                                                         new BinaryNode<int>(5,
                                                                                             new BinaryNode<int>(12, null, null),
                                                                                             new BinaryNode<int>(10, null, null)
                                                                                             ),
                                                                         new BinaryNode<int>(11, null, null)
                                                                        )
                );

            Console.WriteLine(FindLeastCommonAncestor(rt, 3, 17));
            Console.WriteLine(FindCommLeastAncestor2(rt, node3, node17).Data);
            Console.ReadKey();
        }

        static T FindLeastCommonAncestor0<T>(BinaryNode<T> n, BinaryNode<T> t1, BinaryNode<T> t2)
        {
            if (ContainsNode(n.Left, t1) && ContainsNode(n.Left, t2))
                return FindLeastCommonAncestor0(n.Left, t1, t2);
            if (ContainsNode(n.Right, t1) && ContainsNode(n.Right, t2))
                return FindLeastCommonAncestor0(n.Right, t1, t2);
            return n.Data;
        }

        static bool ContainsNode<T>(BinaryNode<T> root, BinaryNode<T> n)
        {
            if (root == null) return false;
            if (root == n) return true;
            return ContainsNode(root.Left, n) || ContainsNode(root.Right, n);
        }

        static T FindLeastCommonAncestor<T>(BinaryNode<T> n, T t1, T t2)
        {
            var anc1 = new Stack<BinaryNode<T>>();
            var anc2 = new Stack<BinaryNode<T>>();
            GetAncestors(n, t1, anc1);
            GetAncestors(n, t2, anc2);
            anc1 = Reverse(anc1);
            anc2 = Reverse(anc2);

            BinaryNode<T> prev = null;
            while (anc1.Peek().Equals(anc2.Peek()))
            {
                prev = anc1.Pop();
                anc2.Pop();
            }

            return prev.Data;
        }

        static Stack<T> Reverse<T>(Stack<T> s)
        {
            var ret = new Stack<T>(s.Count);
            while (s.Count > 0)
                ret.Push(s.Pop());
            return ret;
        }

        static BinarySearchNode<T> FindLeastCommonAncestor<T>(
                                            BinarySearchNode<T> n,
                                            T n1,
                                            T n2)
            where T : IComparable<T>
        {
            int cmp1 = n.Data.CompareTo(n1);
            int cmp2 = n.Data.CompareTo(n2);
            if (cmp1 * cmp2 <= 0) return n;
            if (cmp1 > 0) return FindLeastCommonAncestor(n.Right, n1, n2);
            return FindLeastCommonAncestor(n.Left, n1, n2);
        }

        static bool GetAncestors<T>(BinaryNode<T> n, T data, Stack<BinaryNode<T>> s)
        {
            if (n == null) return false;
            if (n.Data.Equals(data))
            {
                s.Push(n);
                return true;
            }
            s.Push(n);
            if (GetAncestors(n.Left, data, s)) return true;
            if (GetAncestors(n.Right, data, s)) return true;
            s.Pop();
            return false;
        }

        static BinaryNode<T> FindCommLeastAncestor2<T>(BinaryNode<T> root, BinaryNode<T> n1, BinaryNode<T> n2)
        {
            var count = new Dictionary<BinaryNode<T>, int>();
            return FindCommonLeastAncestor2Impl(root, n1, n2, count);
        }

        static BinaryNode<T> FindCommonLeastAncestor2Impl<T>(BinaryNode<T> root, BinaryNode<T> n1, BinaryNode<T> n2, Dictionary<BinaryNode<T>, int> count)
        {
            if (root == null) return null;
            var l = FindCommonLeastAncestor2Impl(root.Left, n1, n2, count);
            var r = FindCommonLeastAncestor2Impl(root.Right, n1, n2, count);
            if (l != null) return l;
            if (r != null) return r;
            int lcount = root.Left == null ? 0 : count[root.Left];
            int rcount = root.Right == null ? 0 : count[root.Right];
            if (lcount == 1 && rcount == 1) return root;
            int c = Math.Max(lcount, rcount);
            if (root == n1 || root == n2)
                if (c == 1) return root;
                else c += 1;
            count[root] = c;
            return null;
        }
    }


}
