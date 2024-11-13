using System;
using Simulator.Maps;

namespace Simulator
{
    internal class Program
    {
        static void Lab5a()
        {
            Random random = new Random();

            try
            {
                int x1 = random.Next(-10, 10);
                int y1 = random.Next(-10, 10);
                int x2 = random.Next(-10, 10);
                int y2 = random.Next(-10, 10);

                Console.WriteLine($"Attempting to create rectangle with coordinates: ({x1}, {y1}) and ({x2}, {y2})");

                Rectangle rectangle1 = new Rectangle(x1, y1, x2, y2);
                Console.WriteLine($"rectangle1: {rectangle1}");

                Point p1 = new Point(random.Next(-10, 10), random.Next(-10, 10));
                Point p2 = new Point(random.Next(-10, 10), random.Next(-10, 10));

                Console.WriteLine($"Attempting to create rectangle with points: {p1} and {p2}");

                Rectangle rectangle2 = new Rectangle(p1, p2);
                Console.WriteLine($"rectangle2: {rectangle2}");

                Point pointInside = new Point(random.Next(Math.Min(x1, x2), Math.Max(x1, x2)),
                    random.Next(Math.Min(y1, y2), Math.Max(y1, y2)));
                Point pointOutside = new Point(random.Next(-20, -11), random.Next(-20, -11));

                Console.WriteLine(
                    $"Punkt {pointInside} wewnatrz prostokata {rectangle1}: {rectangle1.Contains(pointInside)}");
                Console.WriteLine(
                    $"Punkt {pointOutside} wewnatrz prostokata {rectangle1}: {rectangle1.Contains(pointOutside)}");
            }
            catch (ArgumentException error)
            {
                Console.WriteLine("Error creating rectangle: " + error.Message);
            }
        }

        static void Lab5b()
        {
            Random random = new Random();

            try
            {
                int size = random.Next(5, 21);
                SmallSquareMap map = new SmallSquareMap(size);
                Console.WriteLine($"Created SmallSquareMap with Size: {map.Size}");
                
                Point randomPointInside = new Point(random.Next(0, size), random.Next(0, size));
                Point randomPointOutside = new Point(random.Next(size, size + 10), random.Next(size, size + 10));

                Console.WriteLine($"Point {randomPointInside} exists in map: {map.Exist(randomPointInside)}");  // Expected: True
                Console.WriteLine($"Point {randomPointOutside} exists in map: {map.Exist(randomPointOutside)}"); // Expected: False
                
                Point startPoint = new Point(random.Next(0, size), random.Next(0, size));
                Console.WriteLine($"Starting point: {startPoint}");
                Console.WriteLine($"Next point from {startPoint} moving up: {map.Next(startPoint, Direction.Up)}");
                Console.WriteLine($"Next point from {startPoint} moving right: {map.Next(startPoint, Direction.Right)}");
                
                Point boundaryPoint = new Point(size - 1, size - 1);
                Console.WriteLine($"Boundary point: {boundaryPoint}");
                Console.WriteLine($"Next point from {boundaryPoint} moving right: {map.Next(boundaryPoint, Direction.Right)}");
                Console.WriteLine($"Next diagonal point from {boundaryPoint} moving up: {map.NextDiagonal(boundaryPoint, Direction.Up)}");
                
                Point diagonalStart = new Point(random.Next(0, size), random.Next(0, size));
                Console.WriteLine($"Starting diagonal point: {diagonalStart}");
                Console.WriteLine($"Next diagonal point from {diagonalStart} moving up: {map.NextDiagonal(diagonalStart, Direction.Up)}");
                Console.WriteLine($"Next diagonal point from {diagonalStart} moving left: {map.NextDiagonal(diagonalStart, Direction.Left)}");
                
                try
                {
                    SmallSquareMap invalidMap = new SmallSquareMap(25);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Caught exception: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected exception: {ex.Message}");
            }
        }

        public static void Main(string[] args)
        {
            Lab5a();
            Lab5b();
        }
    }
}