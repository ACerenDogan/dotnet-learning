# Personel Veri İşleme Uygulaması

C# ve .NET kullanılarak geliştirilmiş konsol tabanlı bir personel veri işleme uygulamasıdır. Personel bilgileri JSON dosyasından okunarak C# nesnelerine dönüştürülür.

## Özellikler

- Tüm personelleri listeleme
- ID ile personel arama
- Birime göre filtreleme
- Aktif personelleri listeleme
- 30 yaş ve üzerindeki personelleri listeleme
- Ada göre arama
- Personelleri ada göre alfabetik sıralama
- Geçersiz menü seçimlerini kontrol etme

## Kullanılan Teknolojiler

- C#
- .NET Console Application
- `System.Text.Json`
- LINQ
- JSON

## Proje Yapısı

```text
Task02/
├── Program.cs
├── Personel.cs
├── personeller.json
├── Task02.csproj
└── README.md
```

## Personel Modeli

Her personel aşağıdaki property’lere sahiptir:

- `Id`
- `Ad`
- `Soyad`
- `Birim`
- `Yas`
- `Aktif`

## Uygulamanın Çalışma Mantığı

```text
personeller.json
        ↓
File.ReadAllText()
        ↓
JsonSerializer.Deserialize()
        ↓
List<Personel>
        ↓
Arama, filtreleme ve sıralama
```

## Çalıştırma

Proje klasöründe terminal açılır:

```bash
dotnet run
```

Program çalıştığında kullanıcıya işlem menüsü gösterilir:

```text
1 - Tüm personelleri listele
2 - ID ile personel ara
3 - Birime göre filtrele
4 - Aktif personelleri listele
5 - 30 yaş ve üzerini listele
6 - Ada göre ara
7 - Ada göre listele
8 - Ada göre sırala
0 - Çıkış
```

## Öğrenilen Konular

- Sınıf ve nesne oluşturma
- Property ve `{ get; set; }` kullanımı
- `List<Personel>` yapısı
- JSON dosyası okuma
- Serialization ve deserialization
- `while` ve `switch` ile menü oluşturma
- `foreach` ile liste dolaşma
- LINQ kullanımı
- `FirstOrDefault()`
- `Where()`
- `OrderBy()`
- Nullable değer kontrolü

## Karşılaşılan Hatalar ve Çözümleri

### 1. Deserialize Sonucunun Null Olabilmesi

İlk kullanımda aşağıdaki satır nullable uyarısına neden oldu:

```csharp
List<Personel> personeller =
    JsonSerializer.Deserialize<List<Personel>>(jsonMetni, ayarlar);
```

`Deserialize()` metodunun teorik olarak `null` döndürebilmesi nedeniyle `CS8600`, `CS8602` ve `CS8604` uyarıları oluştu.

Sorun, null-coalescing operatörü `??` kullanılarak çözüldü:

```csharp
List<Personel> personeller =
    JsonSerializer.Deserialize<List<Personel>>(jsonMetni, ayarlar)
    ?? new List<Personel>();
```

Böylece JSON dönüşümü `null` döndürürse boş bir personel listesi oluşturulur.

### 2. Uyarıyı Gizlemek Sorunu Çözmedi

Başlangıçta uyarıyı gizlemek için `#pragma warning disable` kullanıldı:

```csharp
#pragma warning disable CS8600
```

Bu yaklaşım yalnızca derleyici uyarısını gizlediği, fakat olası `null` değerini engellemediği için kaldırıldı. Sorun `??` operatörüyle gerçek anlamda çözüldü.

### 3. JSON ve C# Property Adlarının Eşleşmesi

JSON dosyasında property adları küçük harfle yazılmıştı:

```json
{
  "id": 1,
  "ad": "Ceren"
}
```

C# sınıfında ise property adları büyük harfle başlıyordu:

```csharp
public int Id { get; set; }
public string Ad { get; set; } = "";
```

Büyük-küçük harf farkının sorun oluşturmaması için aşağıdaki ayar kullanıldı:

```csharp
JsonSerializerOptions ayarlar = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};
```

### 4. Console.ReadLine Null Uyarısı

`Console.ReadLine()` teorik olarak `null` döndürebildiği için aşağıdaki kullanım tercih edildi:

```csharp
string secim = Console.ReadLine() ?? "";
```

Değer `null` gelirse boş metin kullanılarak programın güvenli biçimde devam etmesi sağlandı.

### 5. Sayısal Giriş Hatası

ID alınırken `Convert.ToInt32()` kullanılması, kullanıcının harf girmesi durumunda programın kapanmasına neden olabilir.

Daha güvenli kullanım:

```csharp
if (!int.TryParse(Console.ReadLine(), out int id))
{
    Console.WriteLine("Geçerli bir ID giriniz.");
    break;
}
```

`TryParse()` dönüşüm başarılıysa `true`, başarısızsa `false` döndürür.

## Örnek Personel Çıktısı

```text
ID: 1
Ad: Ceren
Soyad: Dogan
Birim: IT
Yaş: 25
Durum: Aktif
-------------------------
```

## Projenin Amacı

Bu proje ile JSON verisinin C# nesnelerine dönüştürülmesi, listeler üzerinde arama ve filtreleme yapılması ve LINQ kullanımı uygulamalı olarak öğrenilmiştir.

## Gelecekte Eklenebilecek Özellikler

- Yeni personel ekleme
- Personel güncelleme ve silme
- Verileri tekrar JSON dosyasına kaydetme
- Birimlere göre personel istatistikleri
- Veritabanı desteği
- ASP.NET Core Web API’ye dönüştürme