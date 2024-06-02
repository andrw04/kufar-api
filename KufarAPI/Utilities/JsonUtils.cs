using System.Text.Json;
using System.Text.Json.Nodes;
using KufarAPI.Models;

namespace KufarAPI.Utilities;

public static class JsonUtils
{
    public static List<SellAd> GetAdsFromJson(string json)
    {
        var ads = new List<SellAd>();
        var adsJson = JsonNode.Parse(json)?["ads"]?.AsArray();

        if (adsJson is not null)
        {
            foreach (var adJson in adsJson)
            {
                if (adJson is null)
                    continue;
                
                var adId = JsonSerializer.Deserialize<int>(adJson["ad_id"]);
                var adLink = JsonSerializer.Deserialize<string>(adJson["ad_link"]);

                var parametersJson = adJson["ad_parameters"]?.AsArray();
                if (parametersJson is null)
                    continue;
                
                var adParameters = GetAdParametersFromJson(parametersJson);

                if (adParameters is null)
                    continue;
                
                ads.Add(new SellAd()
                {
                    AdId = adId,
                    AdLink = adLink,
                    SellAdParameters = adParameters
                });
            }
        }

        return ads;
    }

    private static SellAdParameters? GetAdParametersFromJson(JsonArray paramArrayJson)
    {
        float? squareMeter = null;
        int? rooms = null;
        int? floor = null;
        string? area = null;
        string? metro = null;
        float? latitude = null;
        float? longitude = null;
        
        foreach (var paramJson in paramArrayJson)
        {
            if (paramJson is null)
                continue;

            var p = JsonSerializer.Deserialize<string>(paramJson["p"]);

            if (p == "square_meter")
                squareMeter = JsonSerializer.Deserialize<float>(paramJson["v"]);
            else if (p == "floor")
                floor = JsonSerializer.Deserialize<List<int>>(paramJson["v"])?[0];
            else if (p == "rooms")
                rooms = JsonSerializer.Deserialize<int>(paramJson["v"]?.GetValue<string>()!);
            else if (p == "metro")
                metro = JsonSerializer.Deserialize<List<string>>(paramJson["vl"])?[0];
            else if (p == "area")
                area = paramJson["vl"]?.GetValue<string>();
            else if (p == "coordinates")
            {
                var coordinates = JsonSerializer.Deserialize<List<float>>(paramJson["v"]);
                latitude = coordinates?[1];
                longitude = coordinates?[0];
            }
        }

        return !(squareMeter.HasValue && rooms.HasValue && floor.HasValue && latitude.HasValue && longitude.HasValue)
            ? null
            : new SellAdParameters()
            {
                Area = area,
                Floor = (int)floor,
                Metro = metro,
                Rooms = (int)rooms,
                SquareMeter = (float)squareMeter,
                Latitude = (float)latitude,
                Longitude = (float)longitude
            };
    }

    public static List<BookingFlatAd> GetBookingFlatAdsFromJson(string json)
    {
        var ads = new List<BookingFlatAd>();
        var adsJson = JsonNode.Parse(json)?["ads"]?.AsArray();

        if (adsJson is not null)
        {
            foreach (var adJson in adsJson)
            {
                if (adJson is null)
                    continue;

                var adId = JsonSerializer.Deserialize<int>(adJson["ad_id"]);
                var adLink = JsonSerializer.Deserialize<string>(adJson["ad_link"]);
                if (string.IsNullOrEmpty(adLink))
                    continue;

                var price = JsonSerializer.Deserialize<float>(adJson["price_usd"].GetValue<string>()) / 100;
                
                var parametersJson = adJson["ad_parameters"]?.AsArray();
                if (parametersJson is null)
                    continue;

                var adParameters = GetBookingFlatAdParametersFromJson(parametersJson);

                if (adParameters is null)
                    continue;
                
                ads.Add(new BookingFlatAd()
                {
                    AdId = adId,
                    AdLink = adLink,
                    Price = price,
                    AdParameters = adParameters
                });
            }
        }

        return ads;
    }

    private static BookingFlatAdParameters? GetBookingFlatAdParametersFromJson(JsonArray paramArrayJson)
    {
        string? area = null;
        bool? bookingEnabled = null;
        var bookingCalendar = new List<DateOnly>();

        foreach (var paramJson in paramArrayJson)
        {
            if (paramJson is null)
                continue;
            
            var p = JsonSerializer.Deserialize<string>(paramJson["p"]);
            
            if (p is null)
                continue;

            if (p == "area")
                area = paramJson["vl"]?.GetValue<string>();
            else if (p == "booking_enabled")
                bookingEnabled = JsonSerializer.Deserialize<bool>(paramJson["v"]);
            else if (p == "booking_calendar")
            {
                var baseDate = new DateOnly(1970, 1, 1);

                foreach (var item in paramJson["v"].AsArray())
                {
                    if (item is JsonValue jsonValue && jsonValue.TryGetValue(out int days))
                    {
                        bookingCalendar.Add(baseDate.AddDays(days));
                    }
                }
            }
        }

        return !(bookingEnabled.HasValue && !string.IsNullOrEmpty(area))
            ? null
            : new BookingFlatAdParameters()
            {
                Area = area,
                BookingEnabled = (bool)bookingEnabled,
                BookingCalendar = bookingCalendar
            };
    }
}