using KufarAPI;
using KufarAPI.Utilities;
using NetTopologySuite.Geometries;

var sellAdsJson = await ApiUtils.GetSellAdsFromApi();

if (sellAdsJson is not null)
{
    var sellAds = JsonUtils.GetAdsFromJson(sellAdsJson);
    
    // Task1
    WriteGreen("Зависимость стоимости квадратного метра от этажа квартиры");
    Task1.GetFloorDependence(sellAds, Constants.MinimalPrice);
    
    WriteGreen("Зависимость стоимости квадратного метра от количества комнат");
    Task1.GetRoomDependence(sellAds, Constants.MinimalPrice);

    WriteGreen("Зависимость стоимости от ближайшей станции метро");
    Task1.GetMetroDependence(sellAds, Constants.MinimalPrice);
    
    // Task2
    var coordinates = new Coordinate[] // вершины замкнутой фигуры
    {
        new Coordinate(53.879624, 27.398196),
        new Coordinate(53.825296, 27.614679),
        new Coordinate(53.988096, 27.635029),
        new Coordinate(53.971071, 27.354726),
        new Coordinate(53.879624, 27.398196)
    };

    var adsInShape = Task2.GetAdsInShape(sellAds, coordinates);
    WriteGreen("Квартиры, находящиеся внутри фигуры");
    foreach (var ad in adsInShape)
    {
        Console.WriteLine(ad);
    }
}

var bookingAdsJson = await ApiUtils.GetBookingAdsFromApi();

if (bookingAdsJson is not null)
{
    var bookingAds = JsonUtils.GetBookingFlatAdsFromJson(bookingAdsJson);

    var filteredAds = Task3.GetBookingFlatsByArea(bookingAds, Constants.Area);

    WriteGreen($"Квартиры в районе {Constants.Area}");
    foreach (var ad in filteredAds)
    {
        Console.WriteLine(ad);
    }
}

void WriteGreen(string text)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(text);
    Console.ResetColor();
}