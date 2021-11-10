namespace PathfindingLib.Core
{
    public interface INodeType
    {
        string Name { get; }
        double? NodeCost { get; }
    }
}
