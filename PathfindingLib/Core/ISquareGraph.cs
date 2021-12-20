using System.Collections.Generic;

namespace PathfindingLib.Core
{
    public interface ISquareGraph
    {
        /// <summary>
        /// Actual graph width
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Actual graph height
        /// </summary>
        int Height { get; }

        List<INode> Nodes { get; }

        INode this[int x, int y] { get; }

        IEnumerable<INode> GetNeighbors(INode node);

        /// <summary>
        /// Heuristic cost from one node to another
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        double GetHeuristicCost(INode from, INode to);

        List<INode> GetAllNodesOfCertainType(string typeName);

        /// <summary>
        /// Change actual graph size
        /// </summary>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        void Resize(int newWidth, int newHeight);

        /// <summary>
        /// Add new rows after y position
        /// </summary>
        /// <param name="y">positive y position</param>
        /// <param name="count">count of new rows to add</param>
        void AddRows(int y, int count);

        /// <summary>
        /// Add new columns after x position
        /// </summary>
        /// <param name="x">positive x position</param>
        /// <param name="count">count of new columns to add</param>
        void AddCols(int x, int count);
        
        /// <summary>
        /// Remove rows
        /// </summary>
        /// <param name="y">positive y position</param>
        /// <param name="count">count of rows to delete</param>
        void RemoveRows(int y, int count);

        /// <summary>
        /// Remove cols
        /// </summary>
        /// <param name="x">positive x position</param>
        /// <param name="count">count of cols to delete</param>
        void RemoveCols(int x, int count);
    }
}
