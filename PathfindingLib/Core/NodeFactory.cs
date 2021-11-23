using System;
using System.Collections.Generic;
using System.Text;

namespace PathfindingLib.Core
{
    public class NodeFactory : INodeFactory
    {
        public INode CreateNode(Position position, INodeType nodeType)
        {
            return new Node(position, nodeType) { Value = "0" };
        }
    }
}
