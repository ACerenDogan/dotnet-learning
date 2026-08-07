Console.WriteLine("1 - Metni kendim gireceğim.");
Console.WriteLine("2 - metin.txt dosyasını okuyor");
Console.Write("Seçiminiz:");
string secim = Console.ReadLine() ??"";
string metin = "";
if (secim == "1")
{
    Console.Write("Metni giriniz:");
    metin = Console.ReadLine() ?? "";
}
else if (secim == "2")
{
    metin = File.ReadAllText("metin.txt");
}
int karakterSayisi = metin.Length;
string temizMetin = metin.ToLower();
temizMetin= temizMetin.Replace(",", "");
temizMetin= temizMetin.Replace(".", "");
temizMetin= temizMetin.Replace("!", "");
temizMetin= temizMetin.Replace("?", "");
string []kelimeler = temizMetin.Split(
    new char[] { ' ', '\t', '\n', '\r' }, 
    StringSplitOptions.RemoveEmptyEntries);
int kelimeSayisi = kelimeler.Length;

string enUzunKelime = "";
foreach (string kelime in kelimeler)
{
    if (kelime.Length > enUzunKelime.Length)
    {
        enUzunKelime = kelime;
    }
}

string enKisaKelime = kelimeler[0];
foreach (string kelime in kelimeler)
{
    if (kelime.Length < enKisaKelime.Length)
    {
        enKisaKelime = kelime;
    }
}

Console.WriteLine($"Karakter sayısı: {karakterSayisi}");
Console.WriteLine($"Kelime sayısı: {kelimeSayisi}");
Console.WriteLine($"En uzun kelime: {enUzunKelime}");   
Console.WriteLine($"En kısa kelime: {enKisaKelime}");

Array.Sort(kelimeler);
Console.WriteLine("Kelimeler alfabetik sırayla:");
foreach (string kelime in kelimeler) 
{
    Console.WriteLine(kelime);
}
