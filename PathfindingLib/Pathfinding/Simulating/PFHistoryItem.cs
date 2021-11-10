using System;
using System.Collections.Generic;
using PathfindingLib.Core;

namespace PathfindingLib.Pathfinding.Simulating
{
    public class PFHistoryItem
    {
        public INode Active { get; }
        public Dictionary<INode, INode> CameFrom { get; }
        public List<INode> Frontier { get; }
        

        public PFHistoryItem(INode active, Dictionary<INode, INode> cameFrom, List<INode> frontier)
        {
            Active = active;
            CameFrom = cameFrom;
            Frontier = frontier;
        }
    }

    //public class PFHistoryItem<TNode> where TNode : INode
    //{
    //    public TNode Active { get; }
    //    public Dictionary<TNode, TNode> CameFrom { get; }
    //    public List<TNode> Frontier { get; }


    //    public PFHistoryItem(TNode active, Dictionary<TNode, TNode> cameFrom, List<TNode> frontier)
    //    {
    //        Active = active;
    //        CameFrom = cameFrom;
    //        Frontier = frontier;
    //    }
    //}
}
