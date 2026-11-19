namespace Marketplace.Core.Geometry;

public class Square : Rectangle
{
    public Square(I2DVertex first, I2DVertex second, I2DVertex third, I2DVertex fourth)
        : base(first, second, third, fourth)
    {
        var width = CalculateDistance(vertices[0], vertices[1]);
        var height = CalculateDistance(vertices[1], vertices[2]);

        if (Math.Abs(width - height) > Epsilon)
            throw new ArgumentException("У квадрата все стороны должны быть равны.");
    }
}