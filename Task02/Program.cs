/*
Personel personel1 = new Personel();
{ 
personel1.Id = 1;
personel1.Ad = "Ceren";
personel1.Soyad = "Dogan";
personel1.Birim = "IT";
personel1.Yas = 25;
personel1.Aktif = true;
 }

Personel personel2 = new Personel();
{
personel2.Id = 2;
personel2.Ad = "Mehmet";
personel2.Soyad = "Doğan";
personel2.Birim = "HR";
personel2.Yas = 30;
personel2.Aktif = false;
 }
Personel personel3 = new Personel();
{
personel3.Id = 3;
personel3.Ad = "Canan";
personel3.Soyad = "Yılmaz";
personel3.Birim = "Finance";
personel3.Yas = 28;
personel3.Aktif = true;
 }

Personel personel4 = new Personel();
{
personel4.Id = 4;
personel4.Ad = "Ahmet";
personel4.Soyad = "Kaya";
personel4.Birim = "IT";
personel4.Yas = 35;
personel4.Aktif = true; }
 
Personel personel5 = new Personel();
{

personel5.Id = 5;
personel5.Ad = "Zeren";
personel5.Soyad = "Doğan";
personel5.Birim = "HR";
personel5.Yas = 29;
personel5.Aktif = false;
 }
 */

using System.Text.Json;
string jsonMetni = File.ReadAllText("personeller.json");
JsonSerializerOptions ayarlar = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};
List<Personel> personeller =
    JsonSerializer.Deserialize<List<Personel>>(jsonMetni, ayarlar)
    ?? new List<Personel>();


while (true)
{
    Console.WriteLine();
    Console.WriteLine("================================");
    Console.WriteLine("Personel Yönetim Sistemi");
    Console.WriteLine("================================");
    Console.WriteLine("1 - Tüm Personelleri Listele");
    Console.WriteLine("2 - ID ile Personel Ara");
    Console.WriteLine("3 - Birim ile Personel Ara");
    Console.WriteLine("4 -  Aktif Personelleri Listele");
    Console.WriteLine("5 -  Aktif Olmayan Personelleri Listele");
    Console.WriteLine("6 -  30 yaşından büyük personelleri listele");
    Console.WriteLine("7 -  Ada göre personelleri listele");
    Console.WriteLine("8 -  Ada göre personelleri sırala");
    Console.WriteLine("0 - Çıkış");
    Console.WriteLine("================================");
    Console.Write("Seçiminizi yapınız: ");
    string secim =  Console.ReadLine() ?? "";

    switch (secim)
    {
        case "1":
            Console.WriteLine("Tüm Personeller:");
            foreach ( Personel personel in personeller)
            {
                Console.WriteLine($"ID: {personel.Id}, Ad: {personel.Ad}, Soyad: {personel.Soyad}, Birim: {personel.Birim}, Yaş: {personel.Yas}, Aktif: {personel.Aktif}");
            }
            break;
        case "2":
            Console.Write("Aramak istediğiniz personelin ID'sini giriniz: ");
            int id = int.TryParse(Console.ReadLine(), out int parsedId) ? parsedId : 0;
            var personelById = personeller.FirstOrDefault(p => p.Id == id);
            if (personelById != null)
            {
                Console.WriteLine($"ID: {personelById.Id}, Ad: {personelById.Ad}, Soyad: {personelById.Soyad}, Birim: {personelById.Birim}, Yaş: {personelById.Yas}, Aktif: {personelById.Aktif}");
            }
            else
            {
                Console.WriteLine("Personel bulunamadı.");
            }
            break;
        case "3":
            Console.Write("Aramak istediğiniz birimi giriniz: ");
            string birim = Console.ReadLine() ?? "";
            var personellerByBirim = personeller.Where(p => p.Birim.Equals(birim, StringComparison.OrdinalIgnoreCase)).ToList();
            if (personellerByBirim.Any())
            {
                foreach (var personel in personellerByBirim)
                {
                    Console.WriteLine($"ID: {personel.Id}, Ad: {personel.Ad}, Soyad: {personel.Soyad}, Birim: {personel.Birim}, Yaş: {personel.Yas}, Aktif: {personel.Aktif}");
                }
            }
            else
            {
                Console.WriteLine("Bu birimde personel bulunamadı.");
            }
            break;
        case "4":
            Console.WriteLine("Aktif Personeller:");
            var aktifPersoneller = personeller.Where(p => p.Aktif).ToList();
            foreach (var personel in aktifPersoneller)
            {
                Console.WriteLine($"ID: {personel.Id}, Ad: {personel.Ad}, Soyad: {personel.Soyad}, Birim: {personel.Birim}, Yaş: {personel.Yas}, Aktif: {personel.Aktif}");
            }
            break;
        case "5":
            Console.WriteLine("Aktif Olmayan Personeller:");
            var aktifOlmayanPersoneller = personeller.Where(p => !p.Aktif).ToList();
            foreach (var personel in aktifOlmayanPersoneller)
            {
                Console.WriteLine($"ID: {personel.Id}, Ad: {personel.Ad}, Soyad: {personel.Soyad}, Birim: {personel.Birim}, Yaş: {personel.Yas}, Aktif: {personel.Aktif}");
            }
            break;
        case "6":
            Console.WriteLine("30 yaşından büyük personeller:");
            var yas30UzeriPersoneller = personeller.Where(p => p.Yas > 30).ToList();
            foreach (var personel in yas30UzeriPersoneller)
            {
                Console.WriteLine($"ID: {personel.Id}, Ad: {personel.Ad}, Soyad: {personel.Soyad}, Birim: {personel.Birim}, Yaş: {personel.Yas}, Aktif: {personel.Aktif}");
            }
            break;
        case "7":
            Console.Write("Aramak istediğiniz personelin adını giriniz: ");
            string ad = Console.ReadLine() ?? "";
            var personellerByAd = personeller.Where(p => p.Ad.Equals(ad, StringComparison.OrdinalIgnoreCase)).ToList();
            if (personellerByAd.Any())
            {
                foreach (var personel in personellerByAd)
                {
                    Console.WriteLine($"ID: {personel.Id}, Ad: {personel.Ad}, Soyad: {personel.Soyad}, Birim: {personel.Birim}, Yaş: {personel.Yas}, Aktif: {personel.Aktif}");
                }
            }
            else
            {
                Console.WriteLine("Bu ada sahip personel bulunamadı."); 
            }
            break;
        case "8":
            Console.WriteLine("Personeller ada göre sıralanıyor...");
            var siraliPersoneller = personeller.OrderBy(p => p.Ad).ToList();
            foreach (var personel in siraliPersoneller)
            {
                Console.WriteLine($"ID: {personel.Id}, Ad: {personel.Ad}, Soyad: {personel.Soyad}, Birim: {personel.Birim}, Yaş: {personel.Yas}, Aktif: {personel.Aktif}");
            }
            break;
        case "0":
            Console.WriteLine("Çıkış yapılıyor...");
            return;
        default:
            Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyiniz.");
            break;
    }
}
