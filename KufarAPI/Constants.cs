namespace KufarAPI;

public class Constants
{
    public const string SellAdsApiEndpoint =
        "https://api.kufar.by/search-api/v2/search/rendered-paginated?cat=1010&cur=USD&gtsy=country-belarus~province-minsk~locality-minsk&lang=ru&size=30&typ=sell";

    public const string BookingAdsApiEndpoint =
        "https://api.kufar.by/search-api/v2/search/rendered-paginated?cat=1010&cur=USD&gtsy=country-belarus~province-minsk~locality-minsk&lang=ru&rnt=2&size=30&typ=let";

    public const string Area = "Московский"; // район для поиска (задание 3)

    public const float MinimalPrice = 900; // цена за квадратный метр
}