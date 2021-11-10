using System;

namespace PathfindingLib.Core
{
    public struct Position : IEquatable<Position>
    {
        public readonly int X;
        public readonly int Y;

        public static Position NaN => new Position(-1, -1);

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            return Equals((Position)obj);
        }

        public bool Equals(Position other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return unchecked(X ^ Y);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }

        public static bool operator ==(Position left, Position right)
        {
            return left.X == right.X && left.Y == right.Y;
        }
    }
}
