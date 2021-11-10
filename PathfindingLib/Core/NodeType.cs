namespace PathfindingLib.Core
{
    public class NodeType : INodeType
    {
        public string Name { get; }
        public double? NodeCost { get; }

        public NodeType(string name, double? nodeCost)
        {
            Name = name;
            NodeCost = nodeCost;
        }
    }
}
