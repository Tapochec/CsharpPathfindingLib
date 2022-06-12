namespace CSPF.Common
{
    public interface IEdge
    {
        INode NodeA { get; set; }
        INode NodeB { get; set; }
        EdgeType Type { get; set; }
    }
}
