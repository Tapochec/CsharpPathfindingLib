using System.Collections.Generic;
using PathfindingLib.Core;

namespace PathfindingLib.Pathfinding.Simulating
{
    public class PFHistory
    {
        public INode Start { get; }
        public INode Goal { get; }
        public List<PFHistoryItem> Steps { get; }
        public Dictionary<INodeType, List<INode>> StaticNodes { get; }
        public List<INode> Path { get; }

        public PFHistory(
            INode start,
            INode goal,
            List<PFHistoryItem> steps,
            Dictionary<INodeType, List<INode>> staticNodes,
            List<INode> path)
        {
            Start = start;
            Goal = goal;
            Steps = steps;
            StaticNodes = staticNodes;
            Path = path;
        }
    }

    //public class PFHistory<TNode> where TNode : INode
    //{
    //    public TNode Start { get; }
    //    public TNode Goal { get; }
    //    public List<PFHistoryItem<TNode>> Steps { get; }
    //    public Dictionary<INodeType, List<TNode>> StaticNodes { get; }
    //    public List<TNode> Path { get; }

    //    public PFHistory(
    //        TNode start,
    //        TNode goal,
    //        List<PFHistoryItem<TNode>> steps,
    //        Dictionary<INodeType, List<TNode>> staticNodes,
    //        List<TNode> path)
    //    {
    //        Start = start;
    //        Goal = goal;
    //        Steps = steps;
    //        StaticNodes = staticNodes;
    //        Path = path;
    //    }
    //}
}
