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
        [Fact]
        public void CanAddRows()
        {
            // Arrange
            NodeType nodeType = new NodeType("", 1);
            NodeFactory nodeFactory = new NodeFactory();

            List<Position> actualPositions = new List<Position>();

            int expectedWidth = 13;
            int expectedHeight = 10;
            List<Position> expectedPositions = new List<Position>();
            for (int y = 0; y < expectedHeight; y++)
            {
                for (int x = 0; x < expectedWidth; x++)
                {
                    expectedPositions.Add(new Position(x, y));
                }
            }

            SquareGraph graph = new SquareGraph(10, 10, false,
                nodeType, nodeFactory);


            // Act
            graph.AddCols(5, 3);

            // Assert
            Assert.Equal(expectedWidth, graph.Width);
            Assert.Equal(expectedHeight, graph.Height);

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 13; x++)
                {
                    Assert.Equal(expectedPositions[x + y * 10], graph[x, y].Pos);
                }
            }
        }
    }
}
