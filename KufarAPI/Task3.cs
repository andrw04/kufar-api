using KufarAPI.Models;

namespace KufarAPI;

public class Task3
{
    public static List<BookingFlatAd> GetBookingFlatsByArea(List<BookingFlatAd> ads, string area)
    {
        var filteredAds = ads
            .Where(a => a.AdParameters.BookingEnabled)
            .Where(a => a.AdParameters.Area.Equals(area, StringComparison.OrdinalIgnoreCase))
            .OrderBy(a => a.Price)
            .ToList();

        return filteredAds;
    }
}