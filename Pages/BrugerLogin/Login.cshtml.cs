using Event_Applikation.Models;
using Event_Applikation.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Event_Applikation.Pages.BrugerLogin;

public class LoginModel : PageModel
{
    private IBrugerRepository _brugerRepository;

    public static Bruger? CurrentUser { get; set; }

    [BindProperty]
    public string Navn { get; set; }

    [BindProperty, DataType(DataType.Password)]
    public string Adgangskode { get; set; }

    public string FejlBesked { get; set; }

    public LoginModel(IBrugerRepository brugerRepository)
    {
        _brugerRepository = brugerRepository;
    }


    public async Task<IActionResult> OnPost()
    {
        CurrentUser = _brugerRepository.VerifyUser(Navn, Adgangskode);

        if (CurrentUser == null)
        {
            FejlBesked = "Kunne ikke logge ind";
            return Page();
        }

        // Log ind
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            BuildClaimsPrincipal(CurrentUser));

        return RedirectToPage("/Index");
    }


    private ClaimsPrincipal BuildClaimsPrincipal(Bruger bruger)
    {
        // Opbyg Claims-liste
        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, bruger.Navn));
        //claims.Add(new Claim(ClaimTypes.Role, bruger.Rolle)); ← Til senere implementering ift. Rolle

        // Opret ClaimsIdentity (claims plus Authentication-strategi)
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        // Opret endeligt ClaimsPrincipal-objekt
        return new ClaimsPrincipal(claimsIdentity);
    }

}