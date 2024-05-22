namespace KufarAPI.Models;

public record BookingFlatAdParameters
{
    public string Area { get; set; }
    
    public bool BookingEnabled { get; set; }

    public override string ToString()
    {
        return $"Район {Area} | Онлайн-бронирование {BookingEnabled}";
    }
}