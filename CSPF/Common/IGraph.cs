using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSPF.Common
{
    public interface IGraph<TPosition>
    {
        uint ActualWidth { get; }
        uint ActualHeight { get; }
        uint CanvasWidth { get; }
        uint CanvasHeight { get; }

        void AddNodes(params TPosition[] positions);
        void RemoveNodes(params TPosition[] positions);
        void Clear(bool resetCanvasSize = true);
        IReadOnlyList<INode> GetNeighbors(INode node, bool includeNullCostNodes = true);
    }
}
