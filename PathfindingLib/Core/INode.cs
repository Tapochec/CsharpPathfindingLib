namespace PathfindingLib.Core
{
    public interface INode
    {
        INodeType Type { get; set; }
        Position Pos { get; set; }
        double? Cost { get; }
        string Value { get; set; }
        bool IsPassable { get; }
    }
}
