using PathfindingLib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PathfindingLib.Tests.CoreTests
{
    public class SquareGraphTests
    {
        private SquareGraph GetSquareGraph(int width, int height)
        {
            NodeType nodeType = new NodeType("floor", 1);
            NodeFactory nodeFactory = new NodeFactory(nodeType);

            return new SquareGraph(width, height, false, nodeFactory);
        }

        [Fact]
        public void CanGetNodeByIndexer()
        {
            SquareGraph graph = GetSquareGraph(4, 4);

            Assert.True(graph[0, 0].Pos == new Position(0, 0));
            Assert.True(graph[0, 3].Pos == new Position(0, 3));
            Assert.True(graph[3, 0].Pos == new Position(3, 0));
            Assert.True(graph[3, 3].Pos == new Position(3, 3));
        }

        #region Add\Remove rows

        [Fact]
        public void CanAddRows_Beginning()
        {
            // Arrange
            int expectedWidth = 4;
            int expectedHeight = 6;
            List<Position> expectedPositions = new List<Position>();
            for (int y = 0; y < expectedHeight; y++)
            {
                for (int x = 0; x < expectedWidth; x++)
                {
                    expectedPositions.Add(new Position(x, y));
                }
            }

            SquareGraph graph = GetSquareGraph(4, 4);

            // Act
            graph.AddRows(0, 2);

            // Assert
            Assert.Equal(expectedWidth, graph.Width);
            Assert.Equal(expectedHeight, graph.Height);

            for (int y = 0; y < graph.Height; y++)
            {
                for (int x = 0; x < graph.Width; x++)
                {
                    Assert.True(expectedPositions[x + y * expectedWidth] == graph[x, y].Pos);
                }
            }
        }

        [Fact]
        public void CanRemoveRows()
        {
            // Arrange
            int expectedWidth = 6;
            int expectedHeight = 4;
            List<Position> expectedPositions = new List<Position>();
            for (int y = 0; y < expectedHeight; y++)
            {
                for (int x = 0; x < expectedWidth; x++)
                {
                    expectedPositions.Add(new Position(x, y));
                }
            }

            SquareGraph graph = GetSquareGraph(6, 6);

            // Act
            graph.RemoveRows(2, 2);

            // Assert
            Assert.Equal(expectedWidth, graph.Width);
            Assert.Equal(expectedHeight, graph.Height);

            for (int y = 0; y < graph.Height; y++)
            {
                for (int x = 0; x < graph.Width; x++)
                {
                    Assert.True(expectedPositions[x + y * expectedWidth] == graph[x, y].Pos);
                }
            }
        }

        #endregion

        #region Add\Remove cols

        [Fact]
        public void CanAddCols_Beginning()
        {
            // Arrange
            int expectedWidth = 6;
            int expectedHeight = 4;
            List<Position> expectedPositions = new List<Position>();
            for (int y = 0; y < expectedHeight; y++)
            {
                for (int x = 0; x < expectedWidth; x++)
                {
                    expectedPositions.Add(new Position(x, y));
                }
            }

            SquareGraph graph = GetSquareGraph(4, 4);

            // Act
            graph.AddCols(0, 2);

            // Assert
            Assert.Equal(expectedWidth, graph.Width);
            Assert.Equal(expectedHeight, graph.Height);

            for (int y = 0; y < graph.Height; y++)
            {
                for (int x = 0; x < graph.Width; x++)
                {
                    Assert.True(expectedPositions[x + y * expectedWidth] == graph[x, y].Pos);
                }
            }
        }

        [Fact]
        public void CanAddCols_Middle()
        {
            // Arrange
            int expectedWidth = 6;
            int expectedHeight = 4;
            List<Position> expectedPositions = new List<Position>();
            for (int y = 0; y < expectedHeight; y++)
            {
                for (int x = 0; x < expectedWidth; x++)
                {
                    expectedPositions.Add(new Position(x, y));
                }
            }

            SquareGraph graph = GetSquareGraph(4, 4);

            // Act
            graph.AddCols(2, 2);

            // Assert
            Assert.Equal(expectedWidth, graph.Width);
            Assert.Equal(expectedHeight, graph.Height);

            for (int y = 0; y < graph.Height; y++)
            {
                for (int x = 0; x < graph.Width; x++)
                {
                    Assert.True(expectedPositions[x + y * expectedWidth] == graph[x, y].Pos);
                }
            }
        }

        [Fact]
        public void CanAddCols_End()
        {
            // Arrange
            int expectedWidth = 6;
            int expectedHeight = 4;
            List<Position> expectedPositions = new List<Position>();
            for (int y = 0; y < expectedHeight; y++)
            {
                for (int x = 0; x < expectedWidth; x++)
                {
                    expectedPositions.Add(new Position(x, y));
                }
            }

            SquareGraph graph = GetSquareGraph(4, 4);

            // Act
            graph.AddCols(4, 2);

            // Assert
            Assert.Equal(expectedWidth, graph.Width);
            Assert.Equal(expectedHeight, graph.Height);

            for (int y = 0; y < graph.Height; y++)
            {
                for (int x = 0; x < graph.Width; x++)
                {
                    Assert.True(expectedPositions[x + y * expectedWidth] == graph[x, y].Pos);
                }
            }
        }

        [Fact]
        public void CanRemoveCols()
        {
            // Arrange
            int expectedWidth = 4;
            int expectedHeight = 6;
            List<Position> expectedPositions = new List<Position>();
            for (int y = 0; y < expectedHeight; y++)
            {
                for (int x = 0; x < expectedWidth; x++)
                {
                    expectedPositions.Add(new Position(x, y));
                }
            }

            SquareGraph graph = GetSquareGraph(6, 6);

            // Act
            graph.RemoveCols(2, 2);

            // Assert
            Assert.Equal(expectedWidth, graph.Width);
            Assert.Equal(expectedHeight, graph.Height);

            for (int y = 0; y < graph.Height; y++)
            {
                for (int x = 0; x < graph.Width; x++)
                {
                    Assert.True(expectedPositions[x + y * expectedWidth] == graph[x, y].Pos);
                }
            }
        }

        #endregion
    }
}
