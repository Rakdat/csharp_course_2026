using Marketplace.Core.Geometry;

namespace Marketplace.Core.Tests.Geometry;

public class Vertex2DTests
{
    [TestCase(0, 0, "(x = 0.000, y = 0.000)", TestName = "ToString_ZeroCoordinates_FormatsWithThreeZeros")]
    [TestCase(1.5, -2, "(x = 1.500, y = -2.000)", TestName = "ToString_MixedCoordinates_PadsWithZeros")]
    [TestCase(3.14159, -0.0004, "(x = 3.142, y = -0.000)", TestName = "ToString_HighPrecision_RoundsToThreeDecimalPlaces")]
    [TestCase(12.345, 67.89, "(x = 12.345, y = 67.890)", TestName = "ToString_FractionalCoordinates_ReturnsExpectedFormat")]
    public void ToString_ReturnsFormattedCoordinates(double x, double y, string expected)
    {
        var vertex = new Vertex2D(x, y);

        Assert.That(vertex.ToString(), Is.EqualTo(expected));
    }
}