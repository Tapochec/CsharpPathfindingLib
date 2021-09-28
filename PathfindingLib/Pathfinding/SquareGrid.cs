using System.Collections.Generic;

namespace PathfindingLib.Pathfinding
{

    // Represents a directed weighted graph
    public class SquareGrid
    {
        public int Width { get; private set; }
        public int Heigth { get; private set; }
        public List<Node> Nodes { get; private set; } = new List<Node>();
        public List<Node> Walls { get; private set; } = new List<Node>();
        public List<Node> Forest { get; private set; } = new List<Node>();
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

            node.Type = NodeType.Wall;
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

        public void AddForest(int x, int y)
        {
            Node node = this[x, y];

            node.Type = NodeType.Forest;
            node.Cost = 5;
            Forest.Add(node);
        }

        public void RemoveForest(int x, int y)
        {
            Node node = Forest.Find(n => (n.Pos.X == x) && (n.Pos.Y == y));

            if (node != null)
            {
                node.Type = NodeType.NotVisited;
                node.Cost = 1;
                Forest.Remove(node);
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
            neighbors.RemoveAll(n => n.Type == NodeType.Wall); // Исключаем стены

            return neighbors;
        }

        public List<Node> GetEightNeighbors(Node node)
        {
            int x = node.Pos.X;
            int y = node.Pos.Y;
            List<Node> neighbors = new List<Node>();

            Node up = (this[x, y - 1]);
            Node left = (this[x - 1, y]);
            Node down = (this[x, y + 1]);
            Node right = (this[x + 1, y]);
            
            Node upRight = (this[x + 1, y - 1]);
            Node upLeft = (this[x - 1, y - 1]);
            Node downLeft = (this[x - 1, y + 1]);
            Node downRight = (this[x + 1, y + 1]);
            neighbors.AddRange(new List<Node>() { up, left, down, right, upRight, upLeft, downLeft, downRight });

            // excepting diagonal nodes between walls
            if (left != null && up != null)
            {
                if (left.Type == NodeType.Wall && up.Type == NodeType.Wall)
                {
                    neighbors.Remove(upLeft);
                }
            }
            if (up != null && right != null)
            {
                if (up.Type == NodeType.Wall && right.Type == NodeType.Wall)
                {
                    neighbors.Remove(upRight);
                }
            }
            if (right != null && down != null)
            {
                if (right.Type == NodeType.Wall && down.Type == NodeType.Wall)
                {
                    neighbors.Remove(downRight);
                }
            }
            if (down != null && left != null)
            {
                if (down.Type == NodeType.Wall && left.Type == NodeType.Wall)
                {
                    neighbors.Remove(downLeft);
                }
            }

            neighbors.RemoveAll(n => n == null);
            neighbors.RemoveAll(n => n.Type == NodeType.Wall); // except walls

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
                    grid.Forest.Add(grid[x, y]);
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
