namespace CSPF.Common
{
    public class NodeType : INodeType
    {
        public string Name { get; set; }

        public double? NodeCost { get; set; }
        public string? NodeColor { get; set; }

        public NodeType(string name, double? nodeCost = 1, string? nodeColor = null)
        {
            Name = name;
            NodeCost = nodeCost;
            NodeColor = nodeColor;
        }
    }
}
