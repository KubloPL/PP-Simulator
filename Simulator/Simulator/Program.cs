namespace Simulator;
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

    public static void Main(string[] args)
    {
        Lab5a();
    }
}