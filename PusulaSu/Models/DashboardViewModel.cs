namespace PusulaSu.Models;
public class DashboardViewModel
{
    public AboneKaydi Abone { get; set; } = new();
    public decimal AylikSuTuketimi { get; set; }
    public decimal SuBedeli { get; set; }
    public decimal AtikSuBedeli { get; set; }
    public decimal ToplamBedel { get; set; }
    public string TarifeDönemi { get; set; } = "";
    public bool TarifeBulundu { get; set; }

} 