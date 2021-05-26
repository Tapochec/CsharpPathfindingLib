namespace PathfindingLib.Pathfinding
{
    public class Node
    {
        public readonly Position Pos;
        public double Cost;

        public string Value = "0";
        public NodeType Type = NodeType.NotVisited;

        public Node(int x, int y)
        {
            Pos = new Position(x, y);
            Cost = 1;
        }

        public static Node Forest(int x, int y)
        {
            Node forest = new Node(x, y);
            forest.Type = NodeType.Forest;
            forest.Cost = 2;
            return forest;
        }
    }
}
