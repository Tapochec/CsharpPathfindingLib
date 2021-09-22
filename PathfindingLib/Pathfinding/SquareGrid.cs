﻿using System.Collections.Generic;

namespace PathfindingLib.Pathfinding
{

    // Represents a directed weighted graph
    public class SquareGrid
    {
        public int Width { get; private set; }
        public int Heigth { get; private set; }
        public List<Node> Nodes { get; private set; } = new List<Node>();
        public List<Node> Walls { get; private set; } = new List<Node>();
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

        public delegate List<Node> GetNeighborsDelegate(Node node);
        public GetNeighborsDelegate GetNeighbors { get; private set; }

        public SquareGrid(int width, int heigth, bool allowDiagnalNodesForGetNeighborsMethod)
        {
            Width = width;
            Heigth = heigth;
            GetNeighborsAllowDiagnalNodes = allowDiagnalNodesForGetNeighborsMethod;
        }
        
        public Node this[int x, int y]
        {
            get { return Nodes.Find(n => (n.Pos.X == x) && (n.Pos.Y == y)); }
        }

        public void AddWall(int x, int y)
        {
            Node node = this[x, y];

            node.Type = NodeType.NotAvailable;
            node.Value = "";
            Walls.Add(node);
        }

        public void RemoveWall(int x, int y)
        {
            Node node = Walls.Find(n => (n.Pos.X == x) && (n.Pos.Y == y));

            if (node != null)
            {
                node.Type = NodeType.NotVisited;
                Walls.Remove(node);
            }
        }

        public void RemoveAllWalls()
        {
            Walls.ForEach(w => w.Type = NodeType.NotVisited);
            Walls.Clear();
        }

        public List<Node> GetFourNeighbors(Node node)
        {
            int x = node.Pos.X;
            int y = node.Pos.Y;
            List<Node> neighbors = new List<Node>();

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
            neighbors.RemoveAll(n => n.Type == NodeType.NotAvailable); // Исключаем стены

            return neighbors;
        }

        public List<Node> GetEightNeighbors(Node node)
        {
            int x = node.Pos.X;
            int y = node.Pos.Y;
            List<Node> neighbors = new List<Node>();

            neighbors.Add(this[x, y - 1]); // up
            neighbors.Add(this[x - 1, y]); // left
            neighbors.Add(this[x, y + 1]); // down
            neighbors.Add(this[x + 1, y]); // right
            neighbors.Add(this[x + 1, y - 1]); // up right
            neighbors.Add(this[x - 1, y - 1]); // up left
            neighbors.Add(this[x - 1, y + 1]); // down left
            neighbors.Add(this[x + 1, y + 1]); // down right
            
            neighbors.RemoveAll(n => n == null);
            neighbors.RemoveAll(n => n.Type == NodeType.NotAvailable); // Исключаем стены

            return neighbors;
        }

        public static SquareGrid Create(int width, int height, bool allowDiagnalNodesForGetNeighborsMethod)
        {
            SquareGrid grid = new SquareGrid(width, height, allowDiagnalNodesForGetNeighborsMethod);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    grid.Nodes.Add(new Node(x, y));
                }
            }

            return grid;
        }

        public static SquareGrid CreateWithForest(int width, int height, bool allowDiagnalNodesForGetNeighborsMethod)
        {
            SquareGrid grid = new SquareGrid(width, height, allowDiagnalNodesForGetNeighborsMethod);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    grid.Nodes.Add(new Node(x, y));
                }
            }

            for (int x = 3; x < 7; x++)
            {
                for (int y = 3; y < 7; y++)
                {
                    grid[x, y].Type = NodeType.Forest;
                    grid[x, y].Cost = 5;
                }
            }

            return grid;
        }

        //public void Clear()
        //{
        //    Nodes.ForEach(n => { n.Value = "0"; n.Type = NodeType.NotVisited; });
        //    Walls.Clear();
        //}
    }
}
