namespace Marketplace.Core.Geometry;

public abstract class Polygon : IGeometry
{
    protected const double Epsilon = 1e-9;

    protected readonly I2DVertex[] vertices;

    public int VertexCount => vertices.Length;

    protected Polygon(params I2DVertex[] vertices)
    {
        ArgumentNullException.ThrowIfNull(vertices);

        this.vertices = new I2DVertex[vertices.Length];
        for (var i = 0; i < vertices.Length; i++)
        {
            ArgumentNullException.ThrowIfNull(vertices[i]);
            this.vertices[i] = new Vertex2D(vertices[i].X, vertices[i].Y);
        }
    }

    public abstract double CalculateArea();

    protected static double CalculateDistance(I2DVertex first, I2DVertex second)
    {
        var dx = second.X - first.X;
        var dy = second.Y - first.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}