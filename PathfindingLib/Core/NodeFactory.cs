namespace PathfindingLib.Core
{
    public class NodeFactory : INodeFactory
    {
        private INodeType _nodeType;

        public NodeFactory(INodeType nodeType)
        {
            _nodeType = nodeType;
        }

        public INode Create(Position position)
        {
            return new Node(position, _nodeType) { Value = "0" };
        }

        public INode Create(Position position, INodeType nodeType)
        {
            return new Node(position, nodeType) { Value = "0" };
        }
    }
}
