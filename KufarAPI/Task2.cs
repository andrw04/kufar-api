using KufarAPI.Models;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace KufarAPI;

public static class Task2
{
    public static List<SellAd> GetAdsInShape(List<SellAd> ads, params Coordinate[] coordinates)
    {
        return ads
            .Where(a => IsInShape(a, coordinates))
            .ToList();
    }

    private static bool IsInShape(SellAd sellAd, params Coordinate[] coordinates)
    {
        if (coordinates.Length < 4 || !coordinates[0].Equals2D(coordinates[^1]))
            throw new ArgumentException("Invalid figure");
        
        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

        var point = geometryFactory.CreatePoint(
            new Coordinate(sellAd.SellAdParameters.Latitude, sellAd.SellAdParameters.Longitude));

        var polygon = geometryFactory.CreatePolygon(coordinates);

        return polygon.Contains(point);
    }
}