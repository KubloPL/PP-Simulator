using System;

namespace Simulator.Maps
{
    /// <summary>
    /// Represents a small square map with a specified size between 5 and 20.
    /// </summary>
    public class SmallSquareMap : Map
    {
        /// <summary>
        /// Size of the square map (side length).
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Initializes a new instance of the SmallSquareMap class.
        /// </summary>
        /// <param name="size">Size of the square map (must be between 5 and 20).</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when size is not within the valid range.</exception>
        public SmallSquareMap(int size)
        {
            if (size < 5 || size > 20)
            {
                throw new ArgumentOutOfRangeException(nameof(size), "Size must be between 5 and 20.");
            }
            Size = size;
        }

        /// <summary>
        /// Determines if the given point exists within the bounds of the map.
        /// </summary>
        /// <param name="p">Point to check.</param>
        /// <returns>True if the point is within bounds; otherwise, false.</returns>
        public override bool Exist(Point p)
        {
            return p.X >= 0 && p.X < Size && p.Y >= 0 && p.Y < Size;
        }

        /// <summary>
        /// Returns the next point in the specified direction, clamped to the map boundaries.
        /// </summary>
        /// <param name="p">Starting point.</param>
        /// <param name="d">Direction of movement.</param>
        /// <returns>The next point, or the same point if movement exceeds map boundaries.</returns>
        public override Point Next(Point p, Direction d)
        {
            Point nextPoint = p.Next(d);
            return Exist(nextPoint) ? nextPoint : p;
        }

        /// <summary>
        /// Returns the next diagonal point, rotated 45 degrees clockwise, clamped to the map boundaries.
        /// </summary>
        /// <param name="p">Starting point.</param>
        /// <param name="d">Direction of diagonal movement.</param>
        /// <returns>The next diagonal point, or the same point if movement exceeds map boundaries.</returns>
        public override Point NextDiagonal(Point p, Direction d)
        {
            Point nextDiagonalPoint = p.NextDiagonal(d);
            return Exist(nextDiagonalPoint) ? nextDiagonalPoint : p;
        }
    }
}