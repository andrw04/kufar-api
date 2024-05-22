using KufarAPI.Models;

namespace KufarAPI;

public static class Task1
{
    public static void GetFloorDependence(List<SellAd> ads, float minimalPrice = 1000)
    {
        var groups = ads
            .Where(a => a.SellAdParameters.SquareMeter > minimalPrice)
            .GroupBy(a => a.SellAdParameters.Floor)
            .OrderBy(a => a.Key);

        foreach (var group in groups)
        {
            var averagePrice = group.Average(a => a.SellAdParameters.SquareMeter);
            Console.WriteLine($"Этаж: {group.Key} | Средняя цена: {averagePrice}$");
        }
    }

    public static void GetRoomDependence(List<SellAd> ads, float minimalPrice = 1000)
    {
        var groups = ads
            .Where(a => a.SellAdParameters.SquareMeter > minimalPrice)
            .GroupBy(a => a.SellAdParameters.Rooms)
            .OrderBy(a => a.Key);

        foreach (var group in groups)
        {
            var averagePrice = group.Average(a => a.SellAdParameters.SquareMeter);
            Console.WriteLine($"Количество комнат: {group.Key} | Средняя цена: {averagePrice}$");
        }
    }

    public static void GetMetroDependence(List<SellAd> ads, float minimalPrice = 1000)
    {
        var groups = ads
            .Where(a => a.SellAdParameters.SquareMeter > minimalPrice)
            .Where(a => !string.IsNullOrEmpty(a.SellAdParameters.Metro))
            .GroupBy(a => a.SellAdParameters.Metro)
            .OrderBy(a => a.Key);

        foreach (var group in groups)
        {
            var averagePrice = group.Average(a => a.SellAdParameters.SquareMeter);
            Console.WriteLine($"Метро: {group.Key} | Средняя цена: {averagePrice}$");
        }
    }
}