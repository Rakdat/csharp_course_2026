using Marketplace.Core.Geometry;

namespace Marketplace.Core.Tests.Geometry;

public class LoggerTests
{
    private Logger logger;

    [SetUp]
    public void Setup()
    {
        logger = new Logger();
    }

    [Test]
    public void LogGeometry_Triangle_ReturnsTriangleName()
    {
        var triangle = new Triangle(new Vertex2D(0, 0), new Vertex2D(3, 0), new Vertex2D(0, 4));

        Assert.That(logger.LogGeometry(triangle), Is.EqualTo("треугольник"));
    }

    [Test]
    public void LogGeometry_Rectangle_ReturnsRectangleName()
    {
        var rectangle = new Rectangle(
            new Vertex2D(0, 0),
            new Vertex2D(4, 0),
            new Vertex2D(4, 3),
            new Vertex2D(0, 3));

        Assert.That(logger.LogGeometry(rectangle), Is.EqualTo("прямоугольник"));
    }

    [Test]
    public void LogGeometry_Square_ReturnsSquareName()
    {
        var square = new Square(
            new Vertex2D(0, 0),
            new Vertex2D(4, 0),
            new Vertex2D(4, 4),
            new Vertex2D(0, 4));

        Assert.That(logger.LogGeometry(square), Is.EqualTo("квадрат"));
    }

    [Test]
    public void LogGeometry_UnknownGeometry_ReturnsUnknownMessage()
    {
        var unknown = new UnknownGeometry();

        Assert.That(logger.LogGeometry(unknown), Is.EqualTo("неизвестная геометрия"));
    }

    [Test]
    public void LogGeometry_Null_ReturnsUnknownMessage()
    {
        Assert.That(logger.LogGeometry(null), Is.EqualTo("неизвестная геометрия"));
    }

    private class UnknownGeometry : IGeometry
    {
        public int VertexCount => 5;

        public double CalculateArea() => 0;
    }
}