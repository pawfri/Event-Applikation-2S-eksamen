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
    public string Email { get; set; }
    [BindProperty, DataType(DataType.Password)]
    public string Adgangskode { get; set; }
    public string FejlBesked { get; set; }

    public LoginModel(IBrugerRepository brugerRepository)
    {
        _brugerRepository = brugerRepository;
    }

    /// <summary>
    /// Håndterer login ved at validere loginoplysninger og opretter 
    /// autentificeringscookie med brugerens identitet.
    /// </summary>
    public async Task<IActionResult> OnPost()
    {
        CurrentUser = _brugerRepository.VerifyUser(Email, Adgangskode);

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

    /// <summary>
    /// Opretter et ClaimsPrincipal-objekt på den 
    /// godkendte bruger og de tilhørende oplysninger.
    /// </summary>
    private ClaimsPrincipal BuildClaimsPrincipal(Bruger bruger)
    {
        // Bygger en profil af brugeren (liste med Claim-pbjekter), der kan gemmes i en cookie og som kan identificere brugeren
        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, bruger.Navn));
        claims.Add(new Claim(ClaimTypes.Role, bruger.Rolle.Brugertype));

        // Opretter en identitet, der skal bruges med cookie-autentificering.
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        // Opret endeligt ClaimsPrincipal-objekt
        return new ClaimsPrincipal(claimsIdentity);
    }

}