using System.Collections.Generic;

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

        void AddRows(int y, int count);
        void RemoveRows(int y, int count);
        void AddCols(int x, int count);
        void RemoveCols(int x, int count);
    }
}
