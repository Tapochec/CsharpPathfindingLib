using PathfindingLib.Core;
using PathfindingLib.Pathfinding.Simulating;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PathfindingLib.Pathfinding.Algorithms
{
    // Represents Breadth First Search algorithm
    public sealed class BreadthFirstSearch : IPFAlgorithm/*<TNode> where TNode : class, INode*/
    {
        public string AlgorithmName => "Breadth first search algorithm";

        public List<Position> FindPath(ISquareGraph graph, INode start, INode goal)
        {
            throw new NotImplementedException();
        }

        public PFHistory FindPathWithHistory(ISquareGraph graph, INode start, INode goal, INodeTypesManager typesManager)
        {
            List<PFHistoryItem> steps = new List<PFHistoryItem>();
            Queue<INode> frontier = new Queue<INode>();
            Dictionary<INode, INode>  cameFrom = new Dictionary<INode, INode>();

            frontier.Enqueue(start);
            cameFrom.Add(start, null);

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
                    if (!cameFrom.ContainsKey(next))
                    {
                        //next.Value = (counter + frontier.Count + 1).ToString();

                        frontier.Enqueue(next);
                        cameFrom.Add(next, current);
                    }
                }
                counter++;

                // Adding info about current step
                PFHistoryItem step = new PFHistoryItem(
                    current,
                    cameFrom as Dictionary<INode, INode>,
                    frontier.ToList() as List<INode>);

                steps.Add(step);
            }

            PFHistoryItem lastStep = new PFHistoryItem(
                goal,
                cameFrom as Dictionary<INode, INode>,
                frontier.ToList() as List<INode>);

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
