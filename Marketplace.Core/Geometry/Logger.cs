namespace Marketplace.Core.Geometry;

public class Logger
{
    public string LogGeometry(IGeometry? geometry) => geometry switch
    {
        Triangle => "треугольник",
        Square => "квадрат",
        Rectangle => "прямоугольник",
        _ => "неизвестная геометрия"
    };
}