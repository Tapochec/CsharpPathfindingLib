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
        public int ActualWidth => throw new NotImplementedException();
        public int ActualHeight => throw new NotImplementedException();
        public int CanvasWidth => _canvasWidth;
        public int CanvasHeight => _canvasHeight;
        public bool IsCanvasAdaptive { get; set; } = true;

        private int _canvasWidth;
        private int _canvasHeight;
        private Dictionary<Point, INode> _nodes = new Dictionary<Point, INode>();
        private List<IEdge> _edges = new List<IEdge>();

        public TiledGraph() { }

        public TiledGraph(int canvasWidth, int canvasHeight, bool canvasAdaptive = true)
        {
            _canvasWidth = canvasWidth;
            _canvasHeight = canvasHeight;
            IsCanvasAdaptive = canvasAdaptive;
        }

        public void AddNodes(params Point[] positions)
        {
            throw new NotImplementedException();
        }

        public void RemoveNodes(params Point[] positions)
        {
            throw new NotImplementedException();
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
