namespace KufarAPI.Models;

public record SellAd
{
    public int AdId { get; set; }
    
    public string AdLink { get; set; }
    
    public SellAdParameters SellAdParameters { get; set; }

    public override string ToString()
    {
        return $"ИД {AdId} | Ссылка {AdLink} | {SellAdParameters}";
    }
}