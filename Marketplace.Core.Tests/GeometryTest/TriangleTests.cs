using Marketplace.Core.Geometry;

namespace Marketplace.Core.Tests.Geometry;

public class TriangleTests
{
    [Test]
    public void VertexCount_ValidTriangle_ReturnsThree()
    {
        var triangle = new Triangle(new Vertex2D(0, 0), new Vertex2D(3, 0), new Vertex2D(0, 4));

        Assert.That(triangle.VertexCount, Is.EqualTo(3));
    }

    [TestCase(0, 0, 3, 0, 0, 4, 6.0, TestName = "CalculateArea_RightTriangle")]
    [TestCase(-2, -1, 4, -1, 1, 5, 18.0, TestName = "CalculateArea_IsoscelesWithNegativeCoordinates")]
    [TestCase(1, 1, 4, 2, 2, 5, 5.5, TestName = "CalculateArea_ArbitraryTriangle")]
    public void CalculateArea_ValidTriangle_ReturnsExpectedArea(
        double x1, double y1,
        double x2, double y2,
        double x3, double y3,
        double expectedArea)
    {
        var triangle = new Triangle(new Vertex2D(x1, y1), new Vertex2D(x2, y2), new Vertex2D(x3, y3));

        Assert.That(triangle.CalculateArea(), Is.EqualTo(expectedArea).Within(1e-6));
    }

    [TestCase(0, 0, 1, 1, 2, 2, TestName = "Constructor_CollinearPoints_ThrowsArgumentException")]
    [TestCase(1, 1, 1, 1, 2, 3, TestName = "Constructor_CoincidentPoints_ThrowsArgumentException")]
    public void Constructor_DegenerateTriangle_ThrowsArgumentException(
        double x1, double y1,
        double x2, double y2,
        double x3, double y3)
    {
        Assert.Throws<ArgumentException>(() =>
            new Triangle(new Vertex2D(x1, y1), new Vertex2D(x2, y2), new Vertex2D(x3, y3)));
    }

    [Test]
    public void Constructor_NullVertex_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new Triangle(new Vertex2D(0, 0), null!, new Vertex2D(0, 4)));
    }
}