using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

/*
 * Question:
 * http://acm.hdu.edu.cn/showproblem.php?pid=1069&PHPSESSID=e9h33nbhg8vmmrnm38p0uiee00
 */

namespace MonkeyAndBanana
{
    class Block
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }

    class PlacedBlock : IComparable<PlacedBlock>
    {
        public int L { get; set; }
        public int W { get; set; }
        public int H { get; set; }
        public int Area { get { return L * W; } }

        public int CompareTo(PlacedBlock other)
        {
            return this.Area - other.Area;
        }

        public bool CanBePlacedOn(PlacedBlock other)
        {
            return
                    (this.L < other.L && this.W < other.W)
                    ||
                    (this.L < other.W && this.W < other.L);
        }
    }

    class Program
    {
        static int MaxPossibleHeightOf(Block[] blks)
        {
            var placedBlocks = new List<PlacedBlock>();

            //~N
            foreach (var blk in blks)
            {
                placedBlocks.AddRange(new[]
                                        {
                                            new PlacedBlock(){ L = blk.X, W = blk.Y, H = blk.Z },
                                            new PlacedBlock(){ L = blk.Y, W = blk.Z, H = blk.X },
                                            new PlacedBlock(){ L = blk.Z, W = blk.X, H = blk.Y }
                                        }
                                   );
            }

            //O(NlgN)
            placedBlocks.Sort();

            var m = placedBlocks.Count;
            var maxHeights = new int[m];

            for(int i = 0 ; i < m ; i++)
                maxHeights[i] = placedBlocks[i].H;

            //O(N^2)
            for (int i = 1; i < m; i++)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    var curBlk = placedBlocks[i];
                    var smallerBlk = placedBlocks[j];
                    if (smallerBlk.CanBePlacedOn(curBlk))
                    {
                        var height = maxHeights[j] + curBlk.H;
                        if (height > maxHeights[i])
                            maxHeights[i] = height;
                    }
                }
            }

            return maxHeights.Max();
        }

        static void Main(string[] args)
        {
            var blcks1 = new Block[] { new Block() { X = 10, Y = 20, Z = 30 } };
            var blcks2 = new Block[] { 
                                   new Block(){X = 6, Y = 8, Z = 10},
                                   new Block(){X = 5, Y = 5, Z = 5},
                                 };

            var blcks3 = new Block[] { 
                                   new Block(){X = 1, Y = 1, Z = 1},
                                   new Block(){X = 2, Y = 2, Z = 2},
                                   new Block(){X = 3, Y = 3, Z = 3},
                                   new Block(){X = 4, Y = 4, Z = 4},
                                   new Block(){X = 5, Y = 5, Z = 5},
                                   new Block(){X = 6, Y = 6, Z = 6},
                                   new Block(){X = 7, Y = 7, Z = 7},
                                };

            var blcks4 = new Block[] {
                                    new Block() {X = 31, Y = 41, Z = 59},
                                    new Block() {X = 26, Y = 53, Z = 58},
                                    new Block() {X = 97, Y = 93, Z = 23},
                                    new Block() {X = 84, Y = 62, Z = 64},
                                    new Block() {X = 33, Y = 83, Z = 27}
                                };

            Debug.Assert(40 == MaxPossibleHeightOf(blcks1));
            Debug.Assert(21 == MaxPossibleHeightOf(blcks2));
            Debug.Assert(28 == MaxPossibleHeightOf(blcks3));
            Debug.Assert(342 == MaxPossibleHeightOf(blcks4));
            Console.WriteLine("Success..");
            Console.ReadKey();
        }
    }
}
