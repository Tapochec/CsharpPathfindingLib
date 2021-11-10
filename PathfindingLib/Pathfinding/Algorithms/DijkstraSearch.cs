using System;
using System.Collections.Generic;
using System.Linq;
using PathfindingLib.Pathfinding.Simulating;
using Priority_Queue;

namespace PathfindingLib.Pathfinding.Algorithms
{
    // Represents Dijkstra searching algorithm
    public sealed class DijkstraSearch : IPFAlgorithm
    {
        public string AlgorithmName => "Dijkstra algorithm";

        public List<Position> Search(SquareGraph grid, Node start, Node goal)
        {
            throw new NotImplementedException();
        }

        public PFHistory SearchWithHistory(SquareGraph grid, Node start, Node goal)
        {
            List<PFHistoryItem> steps = new List<PFHistoryItem>();
            SimplePriorityQueue<Node, double> frontier = new SimplePriorityQueue<Node, double>();
            Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();
            Dictionary<Node, double> costSoFar = new Dictionary<Node, double>();

            //frontier.Enqueue(start);
            frontier.Enqueue(start, 0);
            cameFrom.Add(start, null);
            costSoFar.Add(start, 0);

            int counter = 0;
            bool success = false;

            while (frontier.Count != 0)
            {
                Node current = frontier.Dequeue();
                //current.Value = counter.ToString();

                if (current == goal)
                {
                    success = true;
                    break;
                }

                foreach (Node next in grid.GetNeighbors(current))
                {
                    double newCost = costSoFar[current] + next.Cost;
                    if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                    {
                        costSoFar[next] = newCost;
                        frontier.Enqueue(next, newCost);
                        cameFrom.Add(next, current);

                        next.Value = newCost.ToString();//(counter + frontier.Count + 1).ToString();
                        if (next.Type != NodeType.Forest)
                            next.Type = NodeType.Visited;
                    }
                }
                counter++;

                // Adding info about current step
                PFHistoryItem step = new PFHistoryItem(current, cameFrom, frontier.Select(n => n).ToList());
                steps.Add(step);
            }

            PFHistoryItem lastStep = new PFHistoryItem(goal, cameFrom, frontier.Select(n => n).ToList());
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
