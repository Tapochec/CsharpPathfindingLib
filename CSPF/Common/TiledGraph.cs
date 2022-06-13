using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSPF.Common
{
    public class TiledGraph : IGraph<Point>
    {
        public uint ActualWidth => (uint)(_maxX - _minX);
        public uint ActualHeight => (uint)(_maxY - _minY);

        // Graph boundaries
        private int _minX;
        private int _maxX;
        private int _minY;
        private int _maxY;

        private Dictionary<Point, INode> _nodes = new Dictionary<Point, INode>();
        private List<IEdge> _edges = new List<IEdge>();


        public TiledGraph() { }


        public void AddNode(INode node, Point position)
        {
            #region Validation
            ArgumentNullException.ThrowIfNull(node);
            if (_nodes.ContainsKey(position))
            {
                throw new Exception($"Node at position {position} alreary exists.");
            }
            if (_nodes.ContainsValue(node))
            {
                throw new ArgumentException($"This node already has been added to the graph.", nameof(node));

            }
            #endregion

            var neighbors = new List<INode>();
            var neighborsPositions = new List<Point> 
            {
                new Point(position.X - 1, position.Y),
                new Point(position.X, position.Y - 1),
                new Point(position.X + 1, position.Y),
                new Point(position.X, position.Y + 1)
            };
            foreach (Point pos in neighborsPositions)
            {
                INode? nodeNeighbor;
                _nodes.TryGetValue(pos, out nodeNeighbor);
                if (nodeNeighbor is not null)
                {
                    neighbors.Add(nodeNeighbor);
                }
            }

            _nodes.Add(position, node);

            foreach (INode neighbor in neighbors)
            {
                _edges.Add(new Edge(neighbor, node));
            }
            

            // Update graph boundaries
            if (position.X < _minX)
            {
                _minX = position.X;
            }
            else if (position.X > _maxX)
            {
                _maxX = position.X;
            }

            if (position.Y < _minY)
            {
                _minY = position.Y;
            }
            else if (position.Y > _maxY)
            {
                _maxY = position.Y;
            }
        }

        public void RemoveNode(INode node)
        {
            throw new NotImplementedException();
        }

        public void RemoveNodeAt(Point position)
        {

        }

        public void Clear(bool resetCanvasSize = true)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<INode> GetNeighbors(INode node, bool includeNullCostNodes = true)
        {
            throw new NotImplementedException();
        }
    }
}
