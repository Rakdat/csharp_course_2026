using Marketplace.Core.Geometry;

namespace Marketplace.Core.Tests.Geometry;

public class RectangleTests
{
    [Test]
    public void VertexCount_ValidRectangle_ReturnsFour()
    {
        var rectangle = new Rectangle(
            new Vertex2D(0, 0),
            new Vertex2D(4, 0),
            new Vertex2D(4, 3),
            new Vertex2D(0, 3));

        Assert.That(rectangle.VertexCount, Is.EqualTo(4));
    }

    [TestCase(0, 0, 4, 0, 4, 3, 0, 3, 12.0, TestName = "CalculateArea_AxisAlignedRectangle")]
    [TestCase(-2, -1, 3, -1, 3, 2, -2, 2, 15.0, TestName = "CalculateArea_RectangleWithNegativeCoordinates")]
    [TestCase(0, 0, 2, 2, 1, 3, -1, 1, 4.0, TestName = "CalculateArea_RotatedRectangle")]
    public void CalculateArea_ValidRectangle_ReturnsExpectedArea(
        double x1, double y1,
        double x2, double y2,
        double x3, double y3,
        double x4, double y4,
        double expectedArea)
    {
        var rectangle = new Rectangle(
            new Vertex2D(x1, y1),
            new Vertex2D(x2, y2),
            new Vertex2D(x3, y3),
            new Vertex2D(x4, y4));

        Assert.That(rectangle.CalculateArea(), Is.EqualTo(expectedArea).Within(1e-6));
    }

    [TestCase(0, 0, 4, 0, 5, 3, 1, 3, TestName = "Constructor_Parallelogram_ThrowsArgumentException")]
    [TestCase(0, 0, 4, 3, 4, 0, 0, 3, TestName = "Constructor_CrossedVertices_ThrowsArgumentException")]
    [TestCase(0, 0, 4, 0, 4, 3, 4, 3, TestName = "Constructor_CoincidentVertices_ThrowsArgumentException")]
    public void Constructor_InvalidVertices_ThrowsArgumentException(
        double x1, double y1,
        double x2, double y2,
        double x3, double y3,
        double x4, double y4)
    {
        Assert.Throws<ArgumentException>(() =>
            new Rectangle(
                new Vertex2D(x1, y1),
                new Vertex2D(x2, y2),
                new Vertex2D(x3, y3),
                new Vertex2D(x4, y4)));
    }

    [Test]
    public void Constructor_NullVertex_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new Rectangle(new Vertex2D(0, 0), new Vertex2D(4, 0), null!, new Vertex2D(0, 3)));
    }
}