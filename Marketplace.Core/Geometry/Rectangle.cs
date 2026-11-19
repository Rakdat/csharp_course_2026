using System;

namespace Marketplace.Core.Geometry;

public class Rectangle : Polygon
{
    public Rectangle(I2DVertex first, I2DVertex second, I2DVertex third, I2DVertex fourth)
        : base(first, second, third, fourth)
    {
        if (!IsValidRectangle(vertices[0], vertices[1], vertices[2], vertices[3]))
            throw new ArgumentException("Вершины не образуют прямоугольник.");
    }

    public override double CalculateArea() =>
        CalculateDistance(vertices[0], vertices[1]) * CalculateDistance(vertices[1], vertices[2]);

    private static bool IsValidRectangle(I2DVertex a, I2DVertex b, I2DVertex c, I2DVertex d)
    {
        var ab = CalculateDistance(a, b);
        var bc = CalculateDistance(b, c);
        var cd = CalculateDistance(c, d);
        var da = CalculateDistance(d, a);

        if (ab <= Epsilon || bc <= Epsilon || cd <= Epsilon || da <= Epsilon)
            return false;

        return IsPerpendicular(a, b, c)
               && IsPerpendicular(b, c, d)
               && IsPerpendicular(c, d, a);
    }

    private static bool IsPerpendicular(I2DVertex a, I2DVertex b, I2DVertex c)
    {
        var dotProduct = (b.X - a.X) * (c.X - b.X) + (b.Y - a.Y) * (c.Y - b.Y);
        return Math.Abs(dotProduct) <= Epsilon;
    }
}