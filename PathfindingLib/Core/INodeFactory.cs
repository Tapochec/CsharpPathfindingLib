using System;
using System.Collections.Generic;
using System.Text;

namespace PathfindingLib.Core
{
    public interface INodeFactory
    {
        INode CreateNode(Position position, INodeType nodeType);
    }
}
