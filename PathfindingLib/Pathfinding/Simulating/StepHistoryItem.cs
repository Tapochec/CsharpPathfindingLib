using System;
using System.Collections.Generic;

namespace PathfindingLib.Pathfinding.Simulating
{
    public class StepHistoryItem
    {
        public readonly Tuple<Position, string> Active;
        public readonly Dictionary<Tuple<Position, string>, Position> CameFrom;
        public readonly List<Tuple<Position, string>> Frontier;
        

        public StepHistoryItem(Node active, Dictionary<Node, Node> cameFrom, Queue<Node> frontier)
        {
            Active = new Tuple<Position, string>(active.Pos, active.Value);

            CameFrom = new Dictionary<Tuple<Position, string>, Position>();
            foreach (KeyValuePair<Node, Node> pair in cameFrom)
            {
                Tuple<Position, string> nodePos = new Tuple<Position, string>(pair.Key.Pos, pair.Key.Value);

                Position prevNodePos = Position.NaN;
                if (pair.Value != null)
                    prevNodePos = pair.Value.Pos;

                CameFrom.Add(nodePos, prevNodePos);
            }

            Frontier = new List<Tuple<Position, string>>();
            foreach (Node node in frontier)
                Frontier.Add(new Tuple<Position, string>(node.Pos, node.Value));
        }
    }
}
