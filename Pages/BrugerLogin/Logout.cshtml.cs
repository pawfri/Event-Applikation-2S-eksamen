using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Event_Applikation.Pages.BrugerLogin;

public class LogoutModel : PageModel
{
    /// <summary>
    /// Logger brugeren ud ved at fjerne autentificeringscookien 
    /// sætter den aktuelle bruger i null (nulstiller).
    /// Omdirigerer herefter til login-siden.
    /// </summary>
    public async Task<IActionResult> OnGet()
    {
        LoginModel.CurrentUser = null;

        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToPage("/BrugerLogin/Login");
    }
}

