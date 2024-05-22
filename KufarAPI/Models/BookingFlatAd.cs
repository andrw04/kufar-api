namespace KufarAPI.Models;

public record BookingFlatAd
{
    public int AdId { get; set; }
    
    public string AdLink { get; set; }
    
    public float Price { get; set; }

    public BookingFlatAdParameters AdParameters { get; set; }

    public override string ToString()
    {
        return $"ИД {AdId} | Ссылка {AdLink} | Цена {Price}$ | {AdParameters}";
    }
}