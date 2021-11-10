using PathfindingLib.Core;
using PathfindingLib.Pathfinding.Simulating;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PathfindingLib.Pathfinding.Algorithms
{
    // Represents Breadth First Search algorithm
    public sealed class BreadthFirstSearch<TNode> : IPFAlgorithm<TNode> where TNode : INode 
    {
        public string AlgorithmName => "Breadth first search algorithm";

        public List<Position> FindPath(ISquareGraph<TNode> grid, TNode start, TNode goal)
        {
            throw new NotImplementedException();
        }

        public PFHistory FindPathWithHistory(ISquareGraph<TNode> grid, TNode start, TNode goal, INodeTypesManager typesManager)
        {
            List<PFHistoryItem> steps = new List<PFHistoryItem>();
            Queue<Node> frontier = new Queue<Node>();
            Dictionary<Node, Node>  cameFrom = new Dictionary<Node, Node>();

            frontier.Enqueue(start);
            cameFrom.Add(start, null);

            int counter = 0;
            bool success = false;

            while (frontier.Count != 0)
            {
                Node current = frontier.Dequeue();
                current.Value = counter.ToString();

                if (current == goal)
                {
                    success = true;
                    break;
                }

                foreach (Node next in grid.GetNeighbors(current))
                {
                    if (!cameFrom.ContainsKey(next))
                    {
                        next.Value = (counter + frontier.Count + 1).ToString();
                        if (next.Type != NodeType.Forest)
                            next.Type = NodeType.Visited;

                        frontier.Enqueue(next);
                        cameFrom.Add(next, current);
                    }
                }
                counter++;

                // Adding info about current step
                PFHistoryItem step = new PFHistoryItem(current, cameFrom, frontier.ToList());
                steps.Add(step);
            }

            PFHistoryItem lastStep = new PFHistoryItem(goal, cameFrom, frontier.ToList());
            steps.Add(lastStep);

            // Our shortest path
            List<Node> path = null;
            if (success)
            {
                path = new List<Node> { goal };
                while (path.Last() != start)
                    path.Add(cameFrom[path.Last()]);
            }

            PFHistory history = new PFHistory(start, goal, grid.Walls, grid.Forests, steps, path);
            return history;
        }
    }
}
