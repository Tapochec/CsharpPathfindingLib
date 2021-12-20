using System;
using System.Collections.Generic;
using System.Text;

namespace PathfindingLib.Core
{
    /// <summary>
    /// Compares positions from top left to bottom right
    /// </summary>
    public class PositionComparer : IComparer<Position>
    {
        int _graphWidth;

        public PositionComparer(int graphWidth)
        {
            _graphWidth = graphWidth;
        }

        public int Compare(Position p1, Position p2)
        {
            int p1Val = p1.X + p1.Y * _graphWidth;
            int p2Val = p2.X + p2.Y * _graphWidth;

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
