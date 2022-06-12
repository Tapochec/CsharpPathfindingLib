namespace CSPF.Common
{
    public interface IGraph<TPosition>
    {
        uint ActualWidth { get; }
        uint ActualHeight { get; }

        /// <summary>
        /// Creates new nodes with corresponding positions.
        /// </summary>
        /// <param name="nodeType">The type of all new nodes</param>
        void AddNode(INode node, TPosition position);
        void RemoveNode(INode node);
        void RemoveNodeAt(TPosition position);
        void Clear(bool resetCanvasSize = true);
        IReadOnlyList<INode> GetNeighbors(INode node, bool includeNullCostNodes = true);
    }
}
