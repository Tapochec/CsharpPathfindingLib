using System;
using System.Collections.Generic;

namespace PathfindingLib.Pathfinding.Simulating
{
    public class StepHistoryItem
    {
        public readonly Tuple<Position, string> Active;
        public readonly Dictionary<Tuple<Position, string, NodeType>, Position> CameFrom;
        public readonly List<Tuple<Position, string>> Frontier;
        

        public StepHistoryItem(Node active, Dictionary<Node, Node> cameFrom, List<Node> frontier)
        {
            Active = new Tuple<Position, string>(active.Pos, active.Value);

            CameFrom = new Dictionary<Tuple<Position, string, NodeType>, Position>();
            foreach (KeyValuePair<Node, Node> pair in cameFrom)
            {
                Tuple<Position, string, NodeType> nodeInf =
                    new Tuple<Position, string, NodeType>(pair.Key.Pos, pair.Key.Value, pair.Key.Type);

                Position prevNodePos = Position.NaN;
                if (pair.Value != null)
                    prevNodePos = pair.Value.Pos;

                CameFrom.Add(nodeInf, prevNodePos);
            }

            Frontier = new List<Tuple<Position, string>>();
            foreach (Node node in frontier)
                Frontier.Add(new Tuple<Position, string>(node.Pos, node.Value));
        }
    }
}
