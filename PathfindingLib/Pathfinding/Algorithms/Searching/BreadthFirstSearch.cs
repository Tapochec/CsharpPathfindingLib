using PathfindingLib.Pathfinding.Simulating;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PathfindingLib.Pathfinding.Algorithms.Searching
{
    // Represents Breadth First Search algorithm
    public sealed class BreadthFirstSearch : ISearchingAlg
    {
        public string AlgorithmName => "Breadth first search algorithm";

        public List<Position> Search(SquareGrid grid, Node start, Node goal)
        {
            throw new NotImplementedException();
        }

        public SearchHistory SearchWithHistory(SquareGrid grid, Node start, Node goal)
        {
            List<StepHistoryItem> steps = new List<StepHistoryItem>();
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
                StepHistoryItem step = new StepHistoryItem(current, cameFrom, frontier.ToList());
                steps.Add(step);
            }

            StepHistoryItem lastStep = new StepHistoryItem(goal, cameFrom, frontier.ToList());
            steps.Add(lastStep);

            // Our shortest path
            List<Node> path = null;
            if (success)
            {
                path = new List<Node> { goal };
                while (path.Last() != start)
                    path.Add(cameFrom[path.Last()]);
            }

            SearchHistory history = new SearchHistory(start, goal, grid.Walls, steps, path);
            return history;
        }
    }
}
