using PathfindingLib.Pathfinding.Simulating;
using System.Collections.Generic;
using System.Linq;

namespace PathfindingLib.Pathfinding
{
    public static class WidthSearch
    {
        public static void FillGrid(NodeGrid grid)
        {
            Node start = grid.Nodes[35];
            Queue<Node> frontier = new Queue<Node>();
            frontier.Enqueue(start);
            List<Node> visited = new List<Node>();
            visited.Add(start);

            int counter = 0;
            while (frontier.Count != 0)
            {
                Node current = frontier.Dequeue();
                current.Value = counter.ToString();
                foreach (Node next in grid.GetNeighbors(current))
                {
                    if (!visited.Contains(next))
                    {
                        frontier.Enqueue(next);
                        next.Type = NodeType.Visited;
                        visited.Add(next);
                    }
                }
                counter++;
            }
        }

        public static SearchHistory FillGridWithHistory(NodeGrid grid, Node start, Node goal)
        {
            List<StepHistoryItem> steps = new List<StepHistoryItem>();
            Queue<Node> frontier = new Queue<Node>();
            List<Node> visited = new List<Node>();

            //Node start = grid.Nodes[35];
            frontier.Enqueue(start);
            visited.Add(start);

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
                    if (!visited.Contains(next))
                    {
                        next.Value = (counter + frontier.Count + 1).ToString();
                        frontier.Enqueue(next);
                        next.Type = NodeType.Visited;
                        visited.Add(next);
                        next.Prev = current;
                    }
                }
                counter++;

                // Добавление сведений о шаге в историю
                StepHistoryItem step = new StepHistoryItem(current, visited, frontier);
                steps.Add(step);
            }

            StepHistoryItem lastStep = new StepHistoryItem(goal, visited, frontier);
            steps.Add(lastStep);

            List<Node> path = null;
            if (success)
            {
                path = new List<Node> { goal };
                while (path.Last() != start)
                    path.Add(path.Last().Prev);
            }

            SearchHistory history = new SearchHistory(start, goal, grid.Walls, steps, path);
            return history;
        }
    }
}
