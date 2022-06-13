namespace CSPF.Common
{
    public class Edge : IEdge
    {
        public INode NodeA { get; set; }
        public INode NodeB { get; set; }
        public EdgeDir Type { get; set; } = EdgeDir.TwoWay;

        public Edge(INode nodeA, INode nodeB)
        {
            NodeA = nodeA;
            NodeB = nodeB;
        }
    }
}
