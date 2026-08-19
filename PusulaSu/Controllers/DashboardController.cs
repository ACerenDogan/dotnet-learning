using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PusulaSu.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PusulaSu.Models;

namespace PusulaSu.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly ApplicationDbContext _context;

    public readonly UserManager<IdentityUser> _userManager;

    public DashboardController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
      string? kullaniciId = _userManager.GetUserId(User);

        var aboneKaydi = await _context.AboneKayitlari
    .Include(a => a.SayacOkumalari)
    .FirstOrDefaultAsync(a => a.KullaniciId == kullaniciId);

        if (aboneKaydi == null)
        {
            return NotFound();
        }

    aboneKaydi.SayacOkumalari = aboneKaydi.SayacOkumalari
    .OrderByDescending(o => o.Tarih)
    .ToList();
       var viewModel = new DashboardViewModel
    {
        Abone = aboneKaydi,
        
    };

    var sonOkuma = aboneKaydi.SayacOkumalari
        .OrderBy(o => o.Tarih)
        .LastOrDefault();

    if (sonOkuma == null)
    {
        return View(viewModel);
    }

    var donemOkumalari = aboneKaydi.SayacOkumalari
        .Where(o =>
            o.Tarih.Year == sonOkuma.Tarih.Year &&
            o.Tarih.Month == sonOkuma.Tarih.Month)
        .OrderBy(o => o.Tarih)
        .ToList();

    if (donemOkumalari.Count >= 2)
    {
        decimal ilkEndeks = donemOkumalari.First().Endeks;
        decimal sonEndeks = donemOkumalari.Last().Endeks;

        viewModel.AylikSuTuketimi = sonEndeks - ilkEndeks;
    }

    var tarifeler = await _context.Tarifeler
        .Where(t =>
            t.Yil == sonOkuma.Tarih.Year &&
            t.Ay == sonOkuma.Tarih.Month &&
            t.AboneTuru == aboneKaydi.AboneTuru)
        .OrderBy(t => t.AltSinir)
        .ToListAsync();

    viewModel.TarifeDönemi =
        $"{sonOkuma.Tarih.Month:D2}/{sonOkuma.Tarih.Year}";

    if (tarifeler.Count > 0)
    {
        viewModel.TarifeBulundu = true;

        foreach (var tarife in tarifeler)
        {
            if (viewModel.AylikSuTuketimi <= tarife.AltSinir)
            {
                continue;
            }

            decimal kademeSonu =
                tarife.UstSinir ?? viewModel.AylikSuTuketimi;

            decimal buKademedekiTuketim =
                Math.Min(viewModel.AylikSuTuketimi, kademeSonu)
                - tarife.AltSinir;

            viewModel.SuBedeli +=
                buKademedekiTuketim * tarife.SuBirimFiyati;

            viewModel.AtikSuBedeli +=
                buKademedekiTuketim * tarife.AtikSuBirimFiyati;
        }

        viewModel.ToplamBedel =
            viewModel.SuBedeli + viewModel.AtikSuBedeli;
    }

return View(viewModel);
}
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> OkumaEkle(DateTime tarih, decimal endeks)
{
    string? kullaniciId = _userManager.GetUserId(User);

    var aboneKaydi = await _context.AboneKayitlari
        .FirstOrDefaultAsync(a => a.KullaniciId == kullaniciId);

    if (aboneKaydi == null)
    {
        return NotFound();
    }

   
    
tarih = tarih.Date;

if (tarih > DateTime.Today)
{
    TempData["Hata"] = "Gelecek bir tarih için sayaç okuması giremezsiniz.";
    return RedirectToAction(nameof(Index));
}

if (endeks < 0)
{
    TempData["Hata"] = "Sayaç endeksi negatif olamaz.";
    return RedirectToAction(nameof(Index));
}

DateTime ertesiGun = tarih.AddDays(1);

bool ayniTarihteOkumaVar = await _context.SayacOkumalari.AnyAsync(o =>
    o.AboneKaydiId == aboneKaydi.Id &&
    o.Tarih >= tarih &&
    o.Tarih < ertesiGun);

if (ayniTarihteOkumaVar)
{
    TempData["Hata"] = "Bu tarih için daha önce sayaç okuması girilmiş.";
    return RedirectToAction(nameof(Index));
}
var sonOkuma = await _context.SayacOkumalari
    .Where(o => o.AboneKaydiId == aboneKaydi.Id)
    .OrderByDescending(o => o.Tarih)
    .ThenByDescending(o => o.Id)
    .FirstOrDefaultAsync();

if (sonOkuma != null && tarih <= sonOkuma.Tarih)
{
    TempData["Hata"] =
        $"Yeni okuma tarihi {sonOkuma.Tarih:dd.MM.yyyy} tarihinden sonra olmalıdır.";

    return RedirectToAction(nameof(Index));
}

if (sonOkuma != null && endeks < sonOkuma.Endeks)
{
    TempData["Hata"] =
        $"Yeni endeks, son endeks olan {sonOkuma.Endeks:0.##} m³ değerinden küçük olamaz.";

    return RedirectToAction(nameof(Index));
}
    var yeniOkuma = new SayacOkumasi 
    {
        AboneKaydiId = aboneKaydi.Id,
        Tarih = tarih,
        Endeks = endeks
    };

    _context.SayacOkumalari.Add(yeniOkuma);
    await _context.SaveChangesAsync();
TempData["Basari"] = "Sayaç okuması başarıyla kaydedildi.";
    return RedirectToAction(nameof(Index));
    
}
 [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> SonOkumayiSil(int id)
{
    string? kullaniciId = _userManager.GetUserId(User);

    var aboneKaydi = await _context.AboneKayitlari
        .FirstOrDefaultAsync(a => a.KullaniciId == kullaniciId);

    if (aboneKaydi == null)
    {
        return NotFound();
    }

    var sonOkuma = await _context.SayacOkumalari
        .Where(o => o.AboneKaydiId == aboneKaydi.Id)
        .OrderByDescending(o => o.Tarih)
        .ThenByDescending(o => o.Id)
        .FirstOrDefaultAsync();

    if (sonOkuma == null || sonOkuma.Id != id)
    {
        TempData["Hata"] = "Yalnızca en son sayaç okuması silinebilir.";
        return RedirectToAction(nameof(Index));
    }

    _context.SayacOkumalari.Remove(sonOkuma);
    await _context.SaveChangesAsync();

    TempData["Basari"] =
        "Son sayaç okuması silindi. Doğru değeri yeniden girebilirsiniz.";

    return RedirectToAction(nameof(Index));
}
}