using Microsoft.AspNetCore.Mvc;
using Task04.Models;

namespace Task04.Controllers;
public class MetinAnalizController : Controller
{
   [HttpGet]
public IActionResult Index()
{
    return View(new MetinAnalizModel());
}

    [HttpPost]
public IActionResult Index(MetinAnalizModel model)
{
    if (string.IsNullOrWhiteSpace(model.Metin))
    {
        return View(model);
    }
     char[] ayiraclar = { ' ', '\n', '\r', '\t' };
    
        string[] kelimeler = model.Metin.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        
     

     model.KelimeSayisi = kelimeler.Length;
     model.KarakterSayisi = model.Metin.Length;
     string enUzunKelime = kelimeler[0];
     model.AlfabetikKelimeler = string.Join(", ", kelimeler.OrderBy(k => k, StringComparer.OrdinalIgnoreCase));
     foreach (string kelime in kelimeler)
     {
        if (kelime.Length > enUzunKelime.Length)
        {
            enUzunKelime = kelime;
        }
     }
     model.EnUzunKelime = enUzunKelime;

    return View(model);

    }
}