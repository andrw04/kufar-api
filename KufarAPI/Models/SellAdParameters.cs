namespace KufarAPI.Models;

public record SellAdParameters
{
    public float SquareMeter { get; set; }
    
    public int Rooms { get; set; }
    
    public int Floor { get; set; }
    
    public string Area { get; set; }

    public string Metro { get; set; }

    public float Longitude { get; set; }

    public float Latitude { get; set; }

    public override string ToString()
    {
        return $"Цена/м {SquareMeter}$ | Комнаты {Rooms} | Этаж {Floor} | Район {Area} | Метро {Metro} | Коодинаты [{Latitude},{Longitude}]";
    }
}
