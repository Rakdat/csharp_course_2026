using Marketplace.Core.Geometry;

namespace Marketplace.Core.Tests.Geometry;

public class ClockwiseComparerTests
{
    [Test]
    public void Sort_PointsAroundOrigin_OrdersCounterClockwise()
    {
        var p1 = new Vertex2D(1, 0);
        var p2 = new Vertex2D(1, 1);
        var p3 = new Vertex2D(0, 1);
        var p4 = new Vertex2D(-1, 1);
        var p5 = new Vertex2D(-1, 0);
        var p6 = new Vertex2D(-1, -1);
        var p7 = new Vertex2D(0, -1);
        var p8 = new Vertex2D(1, -1);

        var vertices = new List<I2DVertex> { p6, p3, p8, p1, p5, p2, p7, p4 };

        vertices.Sort(new ClockwiseComparer());

        Assert.That(vertices, Is.EqualTo(new I2DVertex[] { p1, p2, p3, p4, p5, p6, p7, p8 }));
    }

    [Test]
    public void Sort_PointsAroundCustomCenter_OrdersCounterClockwise()
    {
        var topRight = new Vertex2D(4, 3);
        var topLeft = new Vertex2D(0, 3);
        var bottomLeft = new Vertex2D(0, 0);
        var bottomRight = new Vertex2D(4, 0);

        var vertices = new List<I2DVertex> { bottomLeft, topRight, bottomRight, topLeft };
        var center = new Vertex2D(2, 1.5);

        vertices.Sort(new ClockwiseComparer(center));

        Assert.That(vertices, Is.EqualTo(new I2DVertex[] { topRight, topLeft, bottomLeft, bottomRight }));
    }

    [Test]
    public void Sort_CollinearPointsOnSameRay_OrdersByDistanceFromCenter()
    {
        var near = new Vertex2D(1, 1);
        var middle = new Vertex2D(2, 2);
        var far = new Vertex2D(5, 5);

        var vertices = new List<I2DVertex> { far, near, middle };

        vertices.Sort(new ClockwiseComparer());

        Assert.That(vertices, Is.EqualTo(new I2DVertex[] { near, middle, far }));
    }

    [Test]
    public void Compare_NullAndVertex_PlacesNullFirst()
    {
        var comparer = new ClockwiseComparer();
        var vertex = new Vertex2D(1, 1);

        Assert.Multiple(() =>
        {
            Assert.That(comparer.Compare(null, null), Is.EqualTo(0));
            Assert.That(comparer.Compare(null, vertex), Is.LessThan(0));
            Assert.That(comparer.Compare(vertex, null), Is.GreaterThan(0));
        });
    }

    [Test]
    public void Constructor_NullCenter_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new ClockwiseComparer(null!));
    }
}