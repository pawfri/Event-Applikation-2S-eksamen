using Event_Applikation.Models;
using Event_Applikation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Event_Applikation.Pages.Brugere.Admin;

public class CreateModel : PageModel
{
	private IBrugerRepository _brugerRepo;
	private readonly mvp2_dk_db_eventapplikationContext _context;

	[BindProperty]
	public Bruger NyBruger { get; set; }
    public SelectList RolleList { get; set; }
    public SelectList CampusList { get; set; }
    public string? Fejlbesked { get; set; }
    public bool EmailEksisterer
    {
        get 
        {
            return _context.Brugers.Any(b => b.Email == NyBruger.Email);
        }
    }

    public CreateModel(mvp2_dk_db_eventapplikationContext context, IBrugerRepository brugerRepo)
    {
	    _context = context;
	    _brugerRepo = brugerRepo;
    }

    /// <summary>
    /// N�r Create-siden hentes kaldes metoden: "LoadRollerCampus", 
    /// s� de kan benyttes p� sidens dropdown.
    /// </summary>
    public void OnGet()
    {
	    LoadRollerCampus();
    }

    /// <summary>
    /// H�ndterer at kontrollere og indsende input, for at oprette en ny bruger.
    /// </summary>
    public IActionResult OnPostSubmit()
    {
        // Kontrol af gyldig data
        if (!ModelState.IsValid || NyBruger != null)
	    {
            // Kontrol om Email allerede findes
            if (EmailEksisterer == true)
            {
                Fejlbesked = "Denne Email findes allerede i systemet";
            }

            OnGet();
		    return Page();
	    }

        // Opretter en ny bruger og omdirigerer til Forsiden
        _brugerRepo.Create(NyBruger);
        return RedirectToPage("/Index");
    }

    /// <summary>
    /// Indl�ser roller og Campus fra databasen, for at de kan anvendes
    /// i drop-down menu i applikationen. Kaldes i OnGet.
    /// </summary>
    public void LoadRollerCampus()
    {
        RolleList = new SelectList(_context.Rolles, "Id", "Brugertype");
        CampusList = new SelectList(_context.Campuses, "Id", "By");
    }
}
