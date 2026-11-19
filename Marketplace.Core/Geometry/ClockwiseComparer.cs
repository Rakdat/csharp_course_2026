namespace Marketplace.Core.Geometry;

public class ClockwiseComparer : IComparer<I2DVertex>
{
    private readonly double centerX;
    private readonly double centerY;

    public ClockwiseComparer() : this(0, 0)
    {
    }

    public ClockwiseComparer(I2DVertex center)
    {
        ArgumentNullException.ThrowIfNull(center);
        centerX = center.X;
        centerY = center.Y;
    }

    public ClockwiseComparer(double centerX, double centerY)
    {
        this.centerX = centerX;
        this.centerY = centerY;
    }

    public int Compare(I2DVertex? x, I2DVertex? y)
    {
        if (ReferenceEquals(x, y))
            return 0;

        if (x is null)
            return -1;

        if (y is null)
            return 1;

        var angleComparison = GetAngle(x).CompareTo(GetAngle(y));
        if (angleComparison != 0)
            return angleComparison;

        return GetSquaredDistance(x).CompareTo(GetSquaredDistance(y));
    }

    private double GetAngle(I2DVertex vertex)
    {
        var angle = Math.Atan2(vertex.Y - centerY, vertex.X - centerX);
        if (angle < 0)
            angle += 2 * Math.PI;

        return angle;
    }

    private double GetSquaredDistance(I2DVertex vertex)
    {
        var dx = vertex.X - centerX;
        var dy = vertex.Y - centerY;
        return dx * dx + dy * dy;
    }
}