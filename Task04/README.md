# Task04 — ASP.NET Core MVC Metin Analizi

Kullanıcının girdiği metni analiz eden basit bir ASP.NET Core MVC uygulamasıdır.

## Özellikler

- Kelime sayısını hesaplama
- Karakter sayısını hesaplama
- En uzun kelimeyi bulma
- Sonuçları aynı web sayfasında gösterme

## Kullanılan Teknolojiler

- C# , .NET , ASP.NET Core MVC , Razor View , Bootstrap

## MVC Yapısı

- **Model:** Metni ve analiz sonuçlarını tutar.
- **View:** Metin kutusunu ve sonuçları gösterir.
- **Controller:** Formdan gelen metni analiz eder.

## Çalıştırma

Proje klasörüne girilir:

```bash
cd Task04
```

Uygulama çalıştırılır:

```bash
dotnet run
```

Terminalde gösterilen localhost adresinin sonuna `/MetinAnaliz` eklenir:

```text
http://localhost:PORT/MetinAnaliz
```

## Öğrendiklerim

- ASP.NET Core MVC proje yapısı
- Model, View ve Controller görevleri
- GET ve POST istekleri
- Form verisini C# tarafında işleme
- Controller'dan View'a Model gönderme
- Localhost üzerinde web uygulaması çalıştırma
- Bootstrap sınıflarıyla temel görünüm düzenleme

## Karşılaşılan Hatalar ve Çözümleri

### 1. Controller Bulunamadı — 404

Controller sınıfı `MetinAnalizKontroller` olarak yazıldığı için ASP.NET Core tarafından bulunamadı.

ASP.NET Core, Controller sınıflarının `Controller` kelimesiyle bitmesini bekler.

**Çözüm:**

```csharp
public class MetinAnalizController : Controller
```

Dosyanın adı da `MetinAnalizController.cs` olarak düzeltildi.

### 2. NullReferenceException

Controller içinde `return View();` kullanıldığı için View'a Model gönderilmiyordu. View, `Model.Metin` değerini okumaya çalışınca hata oluşuyordu.

**Çözüm:**

GET metodunda boş bir Model gönderildi:

```csharp
[HttpGet]
public IActionResult Index()
{
    return View(new MetinAnalizModel());
}
```

POST işleminden sonra hesaplanan Model tekrar View'a gönderildi:

```csharp
return View(model);
```

### 3. Address Already in Use

Uygulamanın eski bir örneği aynı localhost portunda çalışmaya devam ettiği için yeni uygulama başlatılamadı.

**Çözüm:**

Çalışan uygulama terminalde `Control + C` ile durduruldu.

Eski terminal bulunamadığında şu komut kullanıldı:

```bash
killall Task04
```

Ardından uygulama yeniden çalıştırıldı:

```bash
dotnet run
```

Gerekli olduğunda uygulama farklı bir portta çalıştırıldı:

```bash
dotnet run --urls http://localhost:5216
```