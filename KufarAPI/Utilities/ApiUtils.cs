namespace KufarAPI;

public static class ApiUtils
{
    private static HttpClient _httpClient = new();
    
    public static async Task<string?> GetSellAdsFromApi()
    {
        var response = await _httpClient.GetAsync(Constants.SellAdsApiEndpoint);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        return null;
    }

    public static async Task<string?> GetBookingAdsFromApi()
    {
        var response = await _httpClient.GetAsync(Constants.BookingAdsApiEndpoint);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        return null;
    }
}