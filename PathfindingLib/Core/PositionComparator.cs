using System;
using System.Collections.Generic;
using System.Text;

namespace PathfindingLib.Core
{
    public class PositionComparer : IComparer<Position>
    {
        public int Compare(Position p1, Position p2)
        {
            int p1Val = p1.X + p1.Y * 10;
            int p2Val = p2.X + p2.Y * 10;

            if (p1Val < p2Val)
            {
                return -1;
            }
            else if (p1Val > p2Val)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
