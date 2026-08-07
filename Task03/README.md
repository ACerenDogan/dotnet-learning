# Task03 — Kayıt Yönetim Sistemi

C# ve .NET ile geliştirilmiş konsol tabanlı CRUD uygulamasıdır.

## Özellikler

- Kayıt ekleme
- Kayıtları listeleme
- ID ile kayıt arama
- Kayıt güncelleme
- Kayıt silme
- Geçersiz giriş kontrolü

## Proje Yapısı

```text
Task03/
├── Program.cs
├── Kayit.cs
├── Task03.csproj
└── README.md
```

## Çalıştırma

```bash
dotnet run
```

## Kullanılan Konular

- `class` ve property
- `List<Kayit>`
- CRUD
- `while` ve `switch`
- `foreach`
- LINQ
- `int.TryParse()`

## Karşılaşılan Hatalar

- `Kayit` sınıfı ayrı `Kayit.cs` dosyasına taşındı.
- Olası `null` değerleri için `Console.ReadLine() ?? ""` kullanıldı.
- `case` bloklarının sonuna `break` eklendi.
- Programdan çıkmak için `break` yerine `return` kullanıldı.
- Silme sonrasında ID tekrarlanmaması için en büyük ID’nin bir fazlası kullanıldı.

## Not

Veriler bellekte tutulduğu için program kapatıldığında silinir.