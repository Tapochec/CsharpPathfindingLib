namespace CSPF.Common
{
    public class Node : INode
    {
        public bool UseCustomCost { get; set; }
        public double? Cost
        {
            get
            {
                return UseCustomCost ? _customCost : _nodeType?.NodeCost;
            }
            set
            {
                if (!UseCustomCost)
                {
                    UseCustomCost = true;
                }
                _customCost = value;
            }
        }
        public INodeType? NodeType
        {
            get
            {
                return _nodeType;
            }
            set
            {
                if (UseCustomCost)
                {
                    UseCustomCost = false;
                }
                _nodeType = value;
            }
        }

        private double? _customCost = 1;
        private INodeType? _nodeType;

        public Node(INodeType nodeType)
        {
            NodeType = nodeType;
            UseCustomCost = false;
        }

        public Node(double? cost)
        {
            Cost = cost;
            UseCustomCost = true;
        }
    }
}
