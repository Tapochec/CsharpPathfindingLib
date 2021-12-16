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

            SquareGraph graph = new SquareGraph(4, 4, false,
                nodeType, nodeFactory);


            // Act
            graph.AddCols(2, 2);

            // Assert
            Assert.Equal(expectedWidth, graph.Width);
            Assert.Equal(expectedHeight, graph.Height);

            for (int y = 0; y < graph.Height; y++)
            {
                for (int x = 0; x < graph.Width; x++)
                {
                    Assert.True(expectedPositions[x + y * 6] == graph[x, y].Pos);
                }
            }
        }
    }
}
