using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PathfindingLib.Core
{
    public interface ISquareGraph
    {
        int Width { get; }
        int Height { get; }
        List<INode> Nodes { get; }
        INode this[int x, int y] { get; }
        IEnumerable<INode> GetNeighbors(INode node);
        double GetHeuristicCost(INode from, INode to);
        List<INode> GetAllNodesOfCertainType(string typeName);
        //void AddLeft(ISquareGraph<TNode> graph);
        //void AddRight(ISquareGraph<TNode> graph);
        //void AddTop(ISquareGraph<TNode> graph);
        //void AddBottom(ISquareGraph<TNode> graph);
    }
}
