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
        public ISquareGraph Graph { get; }

        public PFHistory(
            INode start,
            INode goal,
            List<PFHistoryItem> steps,
            Dictionary<INodeType, List<INode>> staticNodes,
            List<INode> path,
            ISquareGraph graph)
        {
            Start = start;
            Goal = goal;
            Steps = steps;
            StaticNodes = staticNodes;
            Path = path;
            Graph = graph;
        }
    }
}
