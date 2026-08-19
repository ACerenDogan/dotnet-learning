using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PusulaSu.Models;
using Microsoft.AspNetCore.Mvc;
using PusulaSu.Data;

namespace PusulaSu.Controllers;

[Authorize(Roles = "Admin")]
public class YoneticiController : Controller
{
    private readonly ApplicationDbContext _context;

    public YoneticiController(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
{
    var bugun = DateTime.Today;

    var viewModel = new YoneticiPaneliViewModel
    {
        ToplamAboneSayisi =
            await _context.AboneKayitlari.CountAsync(),

        KayitliKullaniciSayisi =
            await _context.AboneKayitlari
                .CountAsync(a => a.KullaniciId != null),

        ToplamOkumaSayisi =
            await _context.SayacOkumalari.CountAsync(),

        GuncelTarifeSayisi =
            await _context.Tarifeler
                .CountAsync(t =>
                    t.Yil == bugun.Year &&
                    t.Ay == bugun.Month),
        SonAboneler = await _context.AboneKayitlari
    .OrderByDescending(a => a.Id)
    .Take(5)
    .ToListAsync(),
    SonOkumalar = await _context.SayacOkumalari
    .Include(o => o.AboneKaydi)
    .OrderByDescending(o => o.Tarih)
    .ThenByDescending(o => o.Id)
    .Take(5)
    .ToListAsync()



    };

    return View(viewModel);
}
}