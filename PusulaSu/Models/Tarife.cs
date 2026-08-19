namespace PusulaSu.Models;
public class Tarife
{
    public int Id { get; set; }
    public int Yil { get; set; }
    public int Ay { get; set; }
    public string AboneTuru { get; set; } = "";
    public decimal? UstSinir { get; set; }
    public decimal AltSinir { get; set; }
    public decimal SuBirimFiyati { get; set; }
    public decimal AtikSuBirimFiyati { get; set; }
     }
