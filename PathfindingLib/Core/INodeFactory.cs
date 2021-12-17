namespace PathfindingLib.Core
{
    public interface INodeFactory
    {
        INode Create(Position position);
        INode Create(Position position, INodeType nodeType);
    }
}
