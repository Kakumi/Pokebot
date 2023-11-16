using Newtonsoft.Json;
using System;

namespace Pokebot.Models
{
    public class Position
    {
        public int X { get; }
        public int Y { get; }

        [JsonConstructor]
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj) => this.Equals(obj as Position);

        public override int GetHashCode() => (X, Y).GetHashCode();

        public bool Equals(Position? pos)
        {
            if (pos is null)
            {
                return false;
            }

            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, pos))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != pos.GetType())
            {
                return false;
            }

            return (X == pos.X) && (Y == pos.Y);
        }

        public static bool operator ==(Position? pos1, Position? pos2)
        {
            if (pos1 is null || pos2 is null)
            {
                return false;
            }

            return pos1.Equals(pos2);
        }

        public static bool operator !=(Position? pos1, Position? pos2)
        {
            return !(pos1 == pos2);
        }
    }
}
