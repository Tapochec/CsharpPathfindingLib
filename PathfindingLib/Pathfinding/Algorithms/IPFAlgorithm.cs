using System.Collections.Generic;
using PathfindingLib.Core;
using PathfindingLib.Pathfinding.Simulating;

namespace PathfindingLib.Pathfinding.Algorithms
{
    /// <summary>
    /// Interface of pathfinding algorithm.
    /// </summary>
    public interface IPFAlgorithm/*<TNode> where TNode : class, INode*/
    {
        /// <summary>
        /// Unique name, serves as identifier
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Algorithm name for display to user
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Method finds the shortest path in the grid and return list of node positions of the path.
        /// </summary>
        /// <param name="graph">square grid</param>
        /// <param name="start">start node</param>
        /// <param name="goal">goal (finish) node</param>
        /// <returns>shortest path from start to goal</returns>
        List<Position> FindPath(ISquareGraph graph, INode start, INode goal);

        /// <summary>
        /// Method finds the shortest path in the grid and return detailed history of finding. Use it for pathfinding visualization.
        /// </summary>
        /// <param name="graph">square grid</param>
        /// <param name="start">start node</param>
        /// <param name="goal">goal (finish) node</param>
        /// <returns>the shortest path, and detailed history of finding this path</returns>
        PFHistory FindPathWithHistory(ISquareGraph graph, INode start, INode goal, INodeTypesManager typesManager);
    }
}
