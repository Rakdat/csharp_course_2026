using Marketplace.Core.Geometry;

namespace Marketplace.Core.Tests.Geometry;

public class SquareTests
{
    [Test]
    public void VertexCount_ValidSquare_ReturnsFour()
    {
        var square = new Square(
            new Vertex2D(0, 0),
            new Vertex2D(4, 0),
            new Vertex2D(4, 4),
            new Vertex2D(0, 4));

        Assert.That(square.VertexCount, Is.EqualTo(4));
    }

    [TestCase(0, 0, 4, 0, 4, 4, 0, 4, 16.0, TestName = "CalculateArea_AxisAlignedSquare")]
    [TestCase(-3, -3, 2, -3, 2, 2, -3, 2, 25.0, TestName = "CalculateArea_SquareWithNegativeCoordinates")]
    [TestCase(0, 0, 1, 1, 0, 2, -1, 1, 2.0, TestName = "CalculateArea_RotatedSquare")]
    public void CalculateArea_ValidSquare_ReturnsExpectedArea(
        double x1, double y1,
        double x2, double y2,
        double x3, double y3,
        double x4, double y4,
        double expectedArea)
    {
        var square = new Square(
            new Vertex2D(x1, y1),
            new Vertex2D(x2, y2),
            new Vertex2D(x3, y3),
            new Vertex2D(x4, y4));

        Assert.That(square.CalculateArea(), Is.EqualTo(expectedArea).Within(1e-6));
    }

    [TestCase(0, 0, 4, 0, 4, 3, 0, 3, TestName = "Constructor_NonSquareRectangle_ThrowsArgumentException")]
    [TestCase(0, 0, 2, 0, 3, 2, 1, 2, TestName = "Constructor_Parallelogram_ThrowsArgumentException")]
    [TestCase(0, 0, 0, 0, 0, 0, 0, 0, TestName = "Constructor_CoincidentVertices_ThrowsArgumentException")]
    public void Constructor_InvalidVertices_ThrowsArgumentException(
        double x1, double y1,
        double x2, double y2,
        double x3, double y3,
        double x4, double y4)
    {
        Assert.Throws<ArgumentException>(() =>
            new Square(
                new Vertex2D(x1, y1),
                new Vertex2D(x2, y2),
                new Vertex2D(x3, y3),
                new Vertex2D(x4, y4)));
    }

    [Test]
    public void Constructor_NullVertex_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new Square(new Vertex2D(0, 0), new Vertex2D(4, 0), new Vertex2D(4, 4), null!));
    }
}