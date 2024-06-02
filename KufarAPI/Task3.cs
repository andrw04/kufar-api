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

    public static List<BookingFlatAd> GetBookingFlatsOnDates(
        List<BookingFlatAd> ads, params DateOnly[] dates)
    {
        var now = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        foreach (var date in dates)
        {
            if (date < now)
                throw new ArgumentException($"Incorrect date: {date}");
        }
        
        var filteredAds = new List<BookingFlatAd>();
        foreach (var ad in ads)
        {
            var available = true;
            foreach (var date in ad.AdParameters.BookingCalendar)
            {
                if (dates.Contains(date))
                {
                    available = false;
                    break;
                }
            }
            
            if (available)
                filteredAds.Add(ad);
        }

        return filteredAds;
    }
}