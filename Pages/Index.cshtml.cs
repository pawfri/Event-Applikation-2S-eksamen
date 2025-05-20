using Event_Applikation.Pages.BrugerLogin;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Event_Applikation.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        ///Tvinger en signout når man starter programmet
        public void OnGet()
        {
			if (LoginModel.CurrentUser == null)
			{
				HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			}

		}
	}
}
