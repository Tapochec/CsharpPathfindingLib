using System.Collections.Generic;

namespace PathfindingLib.Core
{

    // Represents a directed weighted graph
    public class SquareGraph<TNode> where TNode : INode, new()
    {
        private int _width;
        private int _height;
        private List<TNode> _nodes;

        public int Width => _width;
        public int Height => _height;
        public IReadOnlyList<TNode> Nodes => _nodes;
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

        public delegate IEnumerable<TNode> GetNeighborsDelegate(TNode node);
        public GetNeighborsDelegate GetNeighbors { get; private set; }

        public SquareGraph(int width, int height, bool allowDiagnalNodesForGetNeighborsMethod, INodeType fillingType)
        {
            _width = width;
            _height = height;
            _nodes = new List<TNode>();
            GetNeighborsAllowDiagnalNodes = allowDiagnalNodesForGetNeighborsMethod;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    _nodes.Add(new TNode() { Pos = new Position(x, y), Type = fillingType });
                }
            }
        }
        
        public TNode this[int x, int y]
        {
            get { return _nodes.Find(n => (n.Pos.X == x) && (n.Pos.Y == y)); }
        }

        private IEnumerable<TNode> GetFourNeighbors(TNode node)
        {
            int x = node.Pos.X;
            int y = node.Pos.Y;
            List<TNode> neighbors = new List<TNode>();

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

        private IEnumerable<TNode> GetEightNeighbors(TNode node)
        {
            int x = node.Pos.X;
            int y = node.Pos.Y;
            List<TNode> neighbors = new List<TNode>();

            TNode up = (this[x, y - 1]);
            TNode left = (this[x - 1, y]);
            TNode down = (this[x, y + 1]);
            TNode right = (this[x + 1, y]);

            TNode upRight = (this[x + 1, y - 1]);
            TNode upLeft = (this[x - 1, y - 1]);
            TNode downLeft = (this[x - 1, y + 1]);
            TNode downRight = (this[x + 1, y + 1]);
            neighbors.AddRange(new List<TNode>() { up, left, down, right, upRight, upLeft, downLeft, downRight });

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
    }
}
