namespace PusulaSu.Models;

public class YoneticiPaneliViewModel
{
    public int ToplamAboneSayisi { get; set; }

    public int KayitliKullaniciSayisi { get; set; }

    public int ToplamOkumaSayisi { get; set; }

    public int GuncelTarifeSayisi { get; set; }

    public List<AboneKaydi> SonAboneler { get; set; } = new();

    public List<SayacOkumasi> SonOkumalar { get; set; } = new();
}