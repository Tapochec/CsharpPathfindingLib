﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace PathfindingLib.Core
{

    // Represents a directed weighted graph
    public class SquareGraph : ISquareGraph
    {
        private int _width;
        private int _height;
        private List<INode> _nodes;
        private INodeFactory _nodeFactory;

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

        public SquareGraph(int width, int height, bool allowDiagnalNodesForGetNeighborsMethod, INodeFactory nodeFactory)
        {
            _width = width;
            _height = height;
            _nodes = new List<INode>();
            _nodeFactory = nodeFactory;
            GetNeighborsAllowDiagnalNodes = allowDiagnalNodesForGetNeighborsMethod;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    _nodes.Add(nodeFactory.Create(new Position(x, y)));
                }
            }
        }
        
        public INode this[int x, int y]
        {
            get
            {
                try
                {
                    INode node = _nodes[y * _width + x];
                    return node;
                }
                catch (ArgumentOutOfRangeException)
                {
                    return null;
                }
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

        #region Resize methods

        public void AddRows(int y, int count)
        {
            throw new NotImplementedException();
        }

        public void RemoveRows(int y, int count)
        {
            throw new NotImplementedException();
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
            _nodes = _nodes.OrderBy(n => n.Pos, new PositionComparer()).ToList();
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
    }
}
