namespace Marketplace.Core.Geometry;

public interface IGeometry
{
    double CalculateArea();
    int VertexCount { get; }
}