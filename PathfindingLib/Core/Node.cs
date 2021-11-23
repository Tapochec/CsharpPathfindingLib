namespace PathfindingLib.Core
{
    public class Node : INode
    {
        public INodeType Type { get; set; }
        public Position Pos { get; set; }
        public double? Cost => Type.NodeCost;
        public string Value { get; set; }
        public bool IsPassable => Type.NodeCost != null;

        public Node()
        {
            Pos = Position.NaN;
        }

        public Node(Position pos, INodeType type)
        {
            Pos = pos;
            Type = type;
        }
    }
}
