
     List<Kayit> kayitlar = new List<Kayit>();
while (true)
        {
     Console.WriteLine();
    Console.WriteLine("================================");
    Console.WriteLine("      KAYIT YÖNETİM SİSTEMİ");
    Console.WriteLine("================================");
    Console.WriteLine("1 - Yeni kayıt ekle");
    Console.WriteLine("2 - Kayıtları listele");
    Console.WriteLine("3 - ID ile kayıt ara");
    Console.WriteLine("4 - Kayıt güncelle");
    Console.WriteLine("5 - Kayıt sil");
    Console.WriteLine("0 - Çıkış");
    Console.Write("Seçiminiz: ");

     switch (Console.ReadLine() ?? "")
     {
         case "1":
             Console.WriteLine("Başlık giriniz: ");
             string baslik = Console.ReadLine() ?? "";
             Console.WriteLine("Açıklama giriniz: ");
             string aciklama = Console.ReadLine() ?? "";
             Console.WriteLine("Durum giriniz: ");
             string durum = Console.ReadLine() ?? "";
             int id = kayitlar.Count + 1;
             kayitlar.Add(new Kayit { Id = id, Baslik = baslik, Aciklama = aciklama, Durum = durum });
             Console.WriteLine("Kayıt eklendi.");
             break;
         case "2":
             foreach (var kayit in kayitlar)
            
             {
                 Console.WriteLine($"Id: {kayit.Id}, Başlık: {kayit.Baslik}, Açıklama: {kayit.Aciklama}, Durum: {kayit.Durum}");
             }
             break;
         case "3":
             Console.Write("Aranacak kayıt ID'sini giriniz: ");
             if (int.TryParse(Console.ReadLine(), out int searchId))
                {
                    var kayit = kayitlar.FirstOrDefault(k => k.Id == searchId);
                    if (kayit != null)
                    {
                        Console.WriteLine($"Id: {kayit.Id}, Başlık: {kayit.Baslik}, Açıklama: {kayit.Aciklama}, Durum: {kayit.Durum}");
                    }
                    else
                    {
                        Console.WriteLine("Kayıt bulunamadı.");
                    }
                }
                else
                {
                    Console.WriteLine("Geçerli bir ID giriniz.");
                }
             break;
             case "4":
                Console.Write("Güncellenecek kayıt ID'sini giriniz: ");
                if (int.TryParse(Console.ReadLine(), out int updateId))
                {
                      Console.WriteLine("Geçerli bir ID giriniz.");
        break;
    }
                    var guncellenecekKayit =
        kayitlar.FirstOrDefault(k => k.Id == updateId);

    if (guncellenecekKayit == null)
    {
        Console.WriteLine("Kayıt bulunamadı.");
        break;
    }

    Console.Write("Yeni başlık giriniz: ");
    guncellenecekKayit.Baslik = Console.ReadLine() ?? "";

    Console.Write("Yeni açıklama giriniz: ");
    guncellenecekKayit.Aciklama = Console.ReadLine() ?? "";

    Console.Write("Yeni durum giriniz: ");
    guncellenecekKayit.Durum = Console.ReadLine() ?? "";

    Console.WriteLine("Kayıt başarıyla güncellendi.");
    break;

             case "0":
                Console.WriteLine("Çıkış yapılıyor...");
                return;
         default:
             Console.WriteLine("Geçersiz seçim.");
             break;
        }

     }