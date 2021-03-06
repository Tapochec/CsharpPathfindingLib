using PathfindingLib.Core;
using PathfindingLib.Pathfinding.Simulating;
using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PathfindingLib.Pathfinding.Algorithms
{
    public class AStarSearch : IPFAlgorithm
    {
        public string Name => "astar";
        public string DisplayName => "A* search";

        public List<Position> FindPath(ISquareGraph graph, INode start, INode goal)
        {
            throw new NotImplementedException();
        }

        public PFHistory FindPathWithHistory(ISquareGraph graph, INode start, INode goal, INodeTypesManager typesManager)
        {
            List<PFHistoryItem> steps = new List<PFHistoryItem>();
            SimplePriorityQueue<INode, double> frontier = new SimplePriorityQueue<INode, double>();
            Dictionary<INode, INode> cameFrom = new Dictionary<INode, INode>();
            Dictionary<INode, double> costSoFar = new Dictionary<INode, double>();

            //frontier.Enqueue(start);
            frontier.Enqueue(start, 0);
            cameFrom.Add(start, null);
            costSoFar.Add(start, 0);

            int counter = 0;
            bool success = false;

            while (frontier.Count != 0)
            {
                INode current = frontier.Dequeue();
                //current.Value = counter.ToString();

                if (current == goal)
                {
                    success = true;
                    break;
                }

                foreach (INode next in graph.GetNeighbors(current))
                {
                    double newCost = costSoFar[current] + next.Cost.Value;
                    if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                    {
                        costSoFar[next] = newCost;
                        frontier.Enqueue(next, newCost + graph.GetHeuristicCost(goal, next));
                        cameFrom.Add(next, current);

                        next.Value = newCost.ToString();//(counter + frontier.Count + 1).ToString();
                    }
                }
                counter++;

                // Adding info about current step
                PFHistoryItem step = new PFHistoryItem(
                    current,
                    cameFrom.ToDictionary(entry => entry.Key, entry => entry.Value),
                    frontier.ToList());

                steps.Add(step);
            }

            PFHistoryItem lastStep = new PFHistoryItem(
                goal,
                cameFrom.ToDictionary(entry => entry.Key, entry => entry.Value),
                frontier.ToList());

            steps.Add(lastStep);

            // shortest path
            List<INode> path = new List<INode>();
            if (success)
            {
                path.Add(goal);
                while (path.Last() != start)
                    path.Add(cameFrom[path.Last()]);
            }

            Dictionary<INodeType, List<INode>> staticNodes = new Dictionary<INodeType, List<INode>>();
            foreach (INodeType nodeType in typesManager.GetAllNodeTypes(false))
            {
                staticNodes.Add(nodeType, graph.GetAllNodesOfCertainType(nodeType.Name) as List<INode>);
            }

            PFHistory history = new PFHistory(
                start,
                goal,
                steps,
                staticNodes,
                path,
                graph);

            return history;
        }
    }
}
