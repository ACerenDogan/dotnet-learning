using Microsoft.AspNetCore.Identity;

namespace PusulaSu.Data;

public static class IdentitySeed
{
    public static async Task AdminOlusturAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        const string adminRolu = "Admin";
        const string adminKullaniciAdi = "Admin";
        const string adminSifresi = "Admin123!";

        if (!await roleManager.RoleExistsAsync(adminRolu))
        {
            await roleManager.CreateAsync(new IdentityRole(adminRolu));
        }

        var admin = await userManager.FindByNameAsync(adminKullaniciAdi);
        if (admin == null)
        {
         admin = new IdentityUser 

            { 
                UserName = adminKullaniciAdi, 
                 };

                 var sonuc = await userManager.CreateAsync(admin, adminSifresi);
            if (sonuc.Succeeded)
            {
                throw new Exception("Admin kullanıcısı oluşturulamadı: ");
            }
        }
        if (!await userManager.IsInRoleAsync(admin, adminRolu))
        {
            await userManager.AddToRoleAsync(admin, adminRolu);
        }
    }
}
