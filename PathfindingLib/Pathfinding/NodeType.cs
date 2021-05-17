using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfindingLib.Pathfinding
{
    public enum NodeType
    {
        NotVisited,
        Visited,
        NotAvailable,
        Frontier,
        Active,
        Start,
        Goal
        //Neibghor,
    }
}
