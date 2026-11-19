namespace Marketplace.Core.Geometry;

public class Vertex2D : I2DVertex
{
    public double X { get; set; }
    public double Y { get; set; }

    public Vertex2D(double x, double y)
    {
        X = x;
        Y = y;
    }
}