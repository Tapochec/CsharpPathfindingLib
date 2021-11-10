namespace PathfindingLib.Core
{
    public interface INode
    {
        INodeType Type { get; }
        Position Pos { get; }
        double? Cost { get; }
        string Value { get; }
        bool IsPassable { get; }
    }
}
