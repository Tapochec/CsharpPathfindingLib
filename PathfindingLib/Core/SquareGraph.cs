using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PathfindingLib.Core
{

    // Represents a directed weighted graph
    public class SquareGraph/*<TNode>*/ : ISquareGraph//<TNode> where TNode : INode, new()
    {
        private int _width;
        private int _height;
        private List<INode> _nodes;

        public int Width => _width;
        public int Height => _height;
        public List<INode> Nodes => _nodes;
        public bool GetNeighborsAllowDiagnalNodes
        {
            set
            {
                if (value)
                    GetNeighbors = GetEightNeighbors;
                else
                    GetNeighbors = GetFourNeighbors;
            }
        }

        public delegate IEnumerable<INode> GetNeighborsDelegate(INode node);
        public GetNeighborsDelegate GetNeighbors { get; private set; }

        public SquareGraph(int width, int height, bool allowDiagnalNodesForGetNeighborsMethod, INodeType fillingType, INodeFactory nodeFactory)
        {
            _width = width;
            _height = height;
            _nodes = new List<INode>();
            GetNeighborsAllowDiagnalNodes = allowDiagnalNodesForGetNeighborsMethod;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //_nodes.Add(new TNode() { Pos = new Position(x, y), Type = fillingType });
                    _nodes.Add(nodeFactory.CreateNode(new Position(x, y), fillingType));
                }
            }
        }
        
        public INode this[int x, int y]
        {
            get { return _nodes.Find(n => (n.Pos.X == x) && (n.Pos.Y == y)); }
        }

        private IEnumerable<INode> GetFourNeighbors(INode node)
        {
            int x = node.Pos.X;
            int y = node.Pos.Y;
            List<INode> neighbors = new List<INode>();

            // Смена хода часовой стрелки в зависимости от чётности стрелки
            if ((x + y) % 2 == 0)
            {
                neighbors.Add(this[x, y - 1]); // up
                neighbors.Add(this[x - 1, y]); // left
                neighbors.Add(this[x, y + 1]); // down
                neighbors.Add(this[x + 1, y]); // right
            }
            else
            {
                neighbors.Add(this[x + 1, y]); // right
                neighbors.Add(this[x, y + 1]); // down
                neighbors.Add(this[x - 1, y]); // left
                neighbors.Add(this[x, y - 1]); // up
            }

            neighbors.RemoveAll(n => n == null);
            neighbors.RemoveAll(n => n.IsPassable == false); // Исключаем стены

            return neighbors;
        }

        private IEnumerable<INode> GetEightNeighbors(INode node)
        {
            int x = node.Pos.X;
            int y = node.Pos.Y;
            List<INode> neighbors = new List<INode>();

            INode up = (this[x, y - 1]);
            INode left = (this[x - 1, y]);
            INode down = (this[x, y + 1]);
            INode right = (this[x + 1, y]);

            INode upRight = (this[x + 1, y - 1]);
            INode upLeft = (this[x - 1, y - 1]);
            INode downLeft = (this[x - 1, y + 1]);
            INode downRight = (this[x + 1, y + 1]);
            neighbors.AddRange(new List<INode>() { up, left, down, right, upRight, upLeft, downLeft, downRight });

            // excepting diagonal nodes between walls
            if (left != null && up != null)
            {
                if (!left.IsPassable && !up.IsPassable)
                {
                    neighbors.Remove(upLeft);
                }
            }
            if (up != null && right != null)
            {
                if (!up.IsPassable && !right.IsPassable)
                {
                    neighbors.Remove(upRight);
                }
            }
            if (right != null && down != null)
            {
                if (!right.IsPassable && !down.IsPassable)
                {
                    neighbors.Remove(downRight);
                }
            }
            if (down != null && left != null)
            {
                if (!down.IsPassable && !left.IsPassable)
                {
                    neighbors.Remove(downLeft);
                }
            }

            neighbors.RemoveAll(n => n == null);
            neighbors.RemoveAll(n => n.IsPassable == false); // except walls

            return neighbors;
        }

        IEnumerable<INode> ISquareGraph.GetNeighbors(INode node)
        {
            return GetNeighbors(node);
        }

        public List<INode> GetAllNodesOfCertainType(string typeName)
        {
            return null;
        }
    }
}
