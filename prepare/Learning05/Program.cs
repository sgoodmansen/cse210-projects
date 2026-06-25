using System;

class Program
{
    static void Main(string[] args)
    {
        // Console.WriteLine("Hello Learning05 World!");
        List<Shape> shapes = new List<Shape>();
        shapes.Add(new Circle("Red", 5));
        shapes.Add(new Rectangle("Green", 10, 4));
        shapes.Add(new Square("Yellow", 3));

        foreach (Shape shape in shapes)
        {
            Console.WriteLine($"Color: {shape.GetColor()}");
            Console.WriteLine($"Area: {shape.GetArea()}");
            Console.WriteLine("");
        }
        

        // Console.WriteLine("SHAPE 1: ");
        // Shape Square1 = new Square("blue", 5);
        // string shapeColor = Square1.GetColor();
        // double area = Square1.GetArea();
        // Console.WriteLine($"The shape is {shapeColor} and the area is {area}");

        // Console.WriteLine("SHAPE 2: ");
        // Shape Rectangle1 = new Rectangle("red", 4, 6);
        // shapeColor = Rectangle1.GetColor();
        // area = Rectangle1.GetArea();
        // Console.WriteLine($"The shape is {shapeColor} and the area is {area}");

        // Console.WriteLine("SHAPE 3: ");
        // Shape Circle1 = new Circle("yellow", 4.5);
        // shapeColor = Circle1.GetColor();
        // area = Circle1.GetArea();
        // Console.WriteLine($"The shape is {shapeColor} and the area is {area}");
    }
}