using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseScheduling
{

    struct Course
    {
        public int s;
        public int e;
    }

    class CourseStartTimeComparer : IComparer<Course>
    {

        #region IComparer<Course> Members

        public int Compare(Course x, Course y)
        {
            return x.s - y.s;
        }

        #endregion
    }

    class CourseEndTimeComparer : IComparer<Course>
    {
        #region IComparer<Course> Members

        public int Compare(Course x, Course y)
        {
            return x.e - y.e;
        }

        #endregion
    }

    class Program
    {
        static Course[] cs = new Course[]
        {
            new Course() {s = 2, e = 5},
            
            
            new Course() {s = 11, e = 15},
            new Course() {s = 4, e = 7},
            new Course() {s = 12, e = 15},
            new Course() {s = 3, e = 8},
            new Course() {s = 0, e = 3},
            new Course() {s = 10, e = 13},
            
            
            new Course() {s = 6, e = 10},
            new Course() {s = 8, e = 10},
            new Course() {s = 9, e = 11},
            
            
            
            new Course() {s = 13, e = 17},
            new Course() {s = 15, e = 18},
        };

        static Course[] cs2 = new Course[]
        {
            new Course() {s = 0, e = 1},
            new Course() {s = 2, e = 3},
            new Course() {s = 4, e = 5},
            new Course() {s = 6, e = 7},
            new Course() {s = 8, e = 9},
            new Course() {s = 9, e = 10},
        };

        static Course[] cs3 = new Course[]
        {
            new Course() {s = 0, e = 1},
            new Course() {s = 0, e = 1},
            new Course() {s = 0, e = 1},
            new Course() {s = 0, e = 1},
            new Course() {s = 0, e = 1},
            new Course() {s = 0, e = 1},
        };

        static int counter = 0;

        static void Main(string[] args)
        {
            Console.WriteLine(DpSchedule(cs));
            Console.WriteLine(counter);
        }

        static void GetGlobalStartEndTime(Course[] cs, out int s, out int e)
        {
            s = int.MaxValue;
            e = int.MinValue;
            foreach (var c in cs)
            {
                if (c.s < s)
                    s = c.s;
                if (c.e > e)
                    e = c.e;
            }
        }

        static int BfScheduleVer1(Course[] cs)
        {
            int s, e;
            GetGlobalStartEndTime(cs, out s, out e);
            return BfScheduleVer1(cs, s, e);
        }

        static int BfScheduleVer1(Course[] cs, int s, int e)
        {
            if (s >= e) return 0;
            int ret = 0;
            foreach (var c in cs)
            {
                if (c.s >= s && c.e <= e)
                {
                    counter++;
                    int r = BfScheduleVer1(cs, s, c.s) + 1 + BfScheduleVer1(cs, c.e, e);
                    if (r > ret)
                        ret = r;
                }
            }
            return ret;
        }

        static int BfScheduleVer2(Course[] cs)
        {
            int s, e;
            int n = cs.Length;
            int[] flags = new int[n];
            GetGlobalStartEndTime(cs, out s, out e);
            Array.Sort(cs, new CourseStartTimeComparer());
            return BfScheduleVer2(cs, s, e, flags, 0);
        }

        static int BfScheduleVer2(Course[] cs, int s, int e, int[] flags, int d)
        {
            int n = flags.Length;
            while (d < n && cs[d].s < s)
            {
                d++;
            }
            if (d == n)
            {
                counter++;
                return flags.Where(f => f == 1).Count();
            }

            flags[d] = 1;
            int wi = BfScheduleVer2(cs, cs[d].e, e, flags, d + 1);
            flags[d] = 0;
            int wo = BfScheduleVer2(cs, s, e, flags, d + 1);
            return Math.Max(wi, wo);
        }

        static int DpSchedule(Course[] cs)
        {
            Array.Sort(cs, new CourseEndTimeComparer());

            int n = cs.Length;
            Course[] css = new Course[n + 2];
            css[0] = new Course() { s = int.MinValue, e = 0};
            css[n + 1] = new Course() { s = cs[n - 1].e, e = int.MaxValue };
            for (int i = 0; i < n; i++)
                css[i + 1] = cs[i];

            int[,] dpTable = new int[n + 2, n + 2];

            for (int i = 0; i < n + 2; i++)
                dpTable[i, i] = 0;

            for (int d = 1; d < n + 2; d++)
            {
                for (int i = 0; i + d < n + 2; i++)
                {
                    int j = i + d;
                    int max = 0;
                    for (int k = i + 1; k < j; k++)
                    {
                        if (css[k].s >= css[i].e &&
                           css[k].e <= css[j].s
                            )
                        {
                            int t = dpTable[i, k] + 1 + dpTable[k, j];
                            if (t > max) max = t;
                        }
                    }
                    dpTable[i, j] = max;
                }
            }
            return dpTable[0, n + 1];
        }

        static int GreedySchedule(Course[] cs)
        {
            Array.Sort(cs, new CourseEndTimeComparer());
            int s = -1;
            int ret = 0;
            for (int i = 0; i < cs.Length; i++)
            {
                if (cs[i].s >= s)
                {
                    ret++;
                    s = cs[i].e;
                }
            }
            return ret;
        }
    }
}
