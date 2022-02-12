using System;
using System.Collections.Generic;
using System.Linq;

namespace PathfindingLib.Core
{
    /// <summary>
    /// Represents a directed weighted graph
    /// </summary>
    public class SquareGraph : ISquareGraph
    {
        private int _width;
        private int _height;
        private List<INode> _nodes;
        private INodeFactory _nodeFactory;

        /// <summary> Maximum available width </summary>
        public const int MaxWidth = 10000;
        /// <summary> Maximum available height </summary>
        public const int MaxHeight = 10000;

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

        /// <summary>
        /// Creates new square graph
        /// </summary>
        /// <param name="width">graph width</param>
        /// <param name="height">graph height</param>
        /// <param name="allowDiagnalNodesForGetNeighborsMethod">true for allow</param>
        /// <param name="nodeFactory">node factory for creating new nodes in graph</param>
        public SquareGraph(int width, int height, bool allowDiagnalNodesForGetNeighborsMethod, INodeFactory nodeFactory)
        {
            #region Validation
            if (width <= 0 || width > MaxWidth)
            {
                throw new ArgumentException("new width must be positive number, that lower than allowed max width");
            }

            if (height <= 0 || height > MaxHeight)
            {
                throw new ArgumentException("new height must be positive number, that lower than allowed max height");
            }
            #endregion

            _width = width;
            _height = height;
            _nodes = new List<INode>();
            _nodeFactory = nodeFactory;
            GetNeighborsAllowDiagnalNodes = allowDiagnalNodesForGetNeighborsMethod;

            Fill();
        }
        
        public INode this[int x, int y]
        {
            get
            {
                return _nodes.Find(n => (n.Pos.X == x) && (n.Pos.Y == y));
            }
        }

        public double GetHeuristicCost(INode from, INode to)
        {
            return Math.Abs(from.Pos.X - to.Pos.X) + Math.Abs(from.Pos.Y - to.Pos.Y);
        }

        IEnumerable<INode> ISquareGraph.GetNeighbors(INode node)
        {
            return GetNeighbors(node);
        }

        public List<INode> GetAllNodesOfCertainType(string typeName)
        {
            return _nodes.FindAll(n => n.Type.Name == typeName);
        }

        #region Resize methods

        public void Resize(int newWidth, int newHeight)
        {
            #region Validation
            if (newWidth <= 0 || newWidth > MaxWidth)
            {
                throw new ArgumentException("new width must be positive number, that lower than allowed max width");
            }

            if (newHeight <= 0 || newHeight > MaxHeight )
            {
                throw new ArgumentException("new height must be positive number, that lower than allowed max height");
            }
            #endregion

            _width = newWidth;
            _height = newHeight;

            Fill();
        }

        public void AddRows(int y, int count)
        {
            #region Validation
            if (y < 0 || y > _height)
            {
                throw new ArgumentOutOfRangeException("y", y, "\"y\" must be positive number and lower than graph height.");
            }

            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException("count", count, "\"count\" must be greater than zero.");
            }

            if (y + count + _height > MaxHeight)
            {
                throw new ArgumentOutOfRangeException("you can't add more rows, that graph allows");
            }
            #endregion

            // Move existing nodes positions
            _nodes.Where(n => n.Pos.Y >= y)
                .ToList()
                .ForEach(n => n.Pos = new Position(n.Pos.X, n.Pos.Y + count));

            // Add new rows
            for (int y2 = y; y2 < y + count; y2++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _nodes.Add(_nodeFactory.Create(new Position(x, y2)));
                }
            }

            _height += count;
            _nodes = _nodes.OrderBy(n => n.Pos, new PositionComparer(_width)).ToList();
        }

        public void AddCols(int x, int count)
        {
            #region Validation
            if (x < 0 || x > _width)
            {
                throw new ArgumentOutOfRangeException("x", x, "\"x\" must be positive number and lower than graph width.");
            }

            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException("count", count, "\"count\" must be greater than zero.");
            }

            if (x + count + _width > MaxWidth)
            {
                throw new ArgumentOutOfRangeException("you can't add more cols, that graph allows");
            }
            #endregion

            // Move existing nodes positions
            _nodes.Where(n => n.Pos.X >= x)
                .ToList()
                .ForEach(n => n.Pos = new Position(n.Pos.X + count, n.Pos.Y));

            // Add new cols
            for (int y = 0; y < _height; y++)
            {
                for (int x2 = x; x2 < x + count; x2++)
                {
                    _nodes.Add(_nodeFactory.Create(new Position(x2, y)));
                }
            }

            _width += count;
            _nodes = _nodes.OrderBy(n => n.Pos, new PositionComparer(_width)).ToList();
        }

        public void RemoveRows(int y, int count)
        {
            #region Validation
            if (y < 0 || y >= _height)
            {
                throw new ArgumentOutOfRangeException("y", y, "\"y\" must be positive number and lower than graph height.");
            }

            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException("count", count, "\"count\" must be greater than zero.");
            }

            if (y + count > _height)
            {
                throw new ArgumentOutOfRangeException("count", count, "\"count\" + \"y\" must be lower than graph height.");
            }
            #endregion

            _nodes.RemoveAll(n => n.Pos.Y >= y && n.Pos.Y < y + count);

            _nodes.Where(n => n.Pos.Y >= y + count)
                .ToList()
                .ForEach(n => n.Pos = new Position(n.Pos.X, n.Pos.Y - count));

            _height -= count;
        }

        public void RemoveCols(int x, int count)
        {
            #region Validation
            if (x < 0 || x >= _width)
            {
                throw new ArgumentOutOfRangeException("x", x, "\"x\" must be positive number and lower than graph width.");
            }

            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException("count", count, "\"count\" must be greater than zero.");
            }

            if (x + count > _width)
            {
                throw new ArgumentOutOfRangeException("count", count, "\"count\" + \"x\" must be lower than graph width.");
            }
            #endregion

            _nodes.RemoveAll(n => n.Pos.X >= x && n.Pos.X < x + count);

            _nodes.Where(n => n.Pos.X >= x + count)
                .ToList()
                .ForEach(n => n.Pos = new Position(n.Pos.X - count, n.Pos.Y));

            _width -= count;
        }

        #endregion

        #region Private methods

        // creates list of nodes using node factory
        // deletes old, if exists
        private void Fill()
        {
            _nodes.Clear();

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _nodes.Add(_nodeFactory.Create(new Position(x, y)));
                }
            }
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

            INode up = this[x, y - 1];
            INode left = this[x - 1, y];
            INode down = this[x, y + 1];
            INode right = this[x + 1, y];

            INode upRight = this[x + 1, y - 1];
            INode upLeft = this[x - 1, y - 1];
            INode downLeft = this[x - 1, y + 1];
            INode downRight = this[x + 1, y + 1];
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

        #endregion
    }
}
