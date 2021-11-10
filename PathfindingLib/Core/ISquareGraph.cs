using System.Collections.Generic;

namespace PathfindingLib.Core
{
    public interface ISquareGraph<TNode> where TNode : INode
    {
        IReadOnlyList<TNode> Nodes { get; }
        TNode this[int x, int y] { get; }
        IEnumerable<TNode> GetNeighbors(TNode node);
        IReadOnlyList<TNode> GetAllNodesOfCertainType(string typeName);
        //void AddLeft(ISquareGraph<TNode> graph);
        //void AddRight(ISquareGraph<TNode> graph);
        //void AddTop(ISquareGraph<TNode> graph);
        //void AddBottom(ISquareGraph<TNode> graph);
    }
}
