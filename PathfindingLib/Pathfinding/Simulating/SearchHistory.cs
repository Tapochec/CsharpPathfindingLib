using System.Collections.Generic;
using System.Linq;

namespace PathfindingLib.Pathfinding.Simulating
{
    public class SearchHistory
    {
        public readonly Position Start;
        public readonly Position Goal;
        public readonly List<Position> NotAvailable;
        public readonly List<StepHistoryItem> Steps;
        public readonly List<Position> Path;

        public SearchHistory(
            Node start,
            Node goal,
            List<Node> notAvailable,
            List<StepHistoryItem> steps,
            List<Node> path = null)
        {
            Start = start.Pos;
            Goal = goal.Pos;
            NotAvailable = notAvailable.Select(n => n.Pos).ToList();
            Steps = steps;
            Path = path?.Select(n => n.Pos).ToList();
        }
    }
}
