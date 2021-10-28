using System.Collections.Generic;
using PathfindingLib.Pathfinding.Simulating;

namespace PathfindingLib.Pathfinding.Algorithms.Searching
{
    /// <summary>
    /// Interface for realizing searching algorithms
    /// </summary>
    public interface ISearchingAlgorithm
    {
        string AlgorithmName { get; }

        /// <summary>
        /// Method finds the shortest path in the grid and return list of node positions of the path.
        /// </summary>
        /// <param name="grid">square grid</param>
        /// <param name="start">start node</param>
        /// <param name="goal">goal (finish) node</param>
        /// <returns>shortest path from start to goal</returns>
        List<Position> Search(SquareGrid grid, Node start, Node goal);

        /// <summary>
        /// Method finds the shortest path in the grid and return detailed history of finding. Use it for pathfinding visualization.
        /// </summary>
        /// <param name="grid">square grid</param>
        /// <param name="start">start node</param>
        /// <param name="goal">goal (finish) node</param>
        /// <returns>the shortest path, and detailed history of finding this path</returns>
        SearchHistory SearchWithHistory(SquareGrid grid, Node start, Node goal);
    }
}
