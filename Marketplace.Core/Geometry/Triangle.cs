using System;

namespace Marketplace.Core.Geometry;

public class Triangle : Polygon
{
    public Triangle(I2DVertex first, I2DVertex second, I2DVertex third) : base(first, second, third)
    {
        if (CalculateTriangleArea(vertices[0], vertices[1], vertices[2]) <= Epsilon)
            throw new ArgumentException("Вершины не образуют треугольник.");
    }

    public override double CalculateArea() => CalculateTriangleArea(vertices[0], vertices[1], vertices[2]);

    private static double CalculateTriangleArea(I2DVertex a, I2DVertex b, I2DVertex c) =>
        0.5 * Math.Abs((b.X - a.X) * (c.Y - a.Y) - (c.X - a.X) * (b.Y - a.Y));
}