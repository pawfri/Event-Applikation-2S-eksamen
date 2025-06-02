using Event_Applikation.Models;
using Event_Applikation.Pages.BrugerLogin;
using Event_Applikation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Event_Applikation.Pages.Events;

public class DetaljerModel : PageModel
{
    private readonly IEventRepository _eventRepo;
	private readonly ITilmeldingRepository _tilmeldingRepo;
	private readonly IBrugerRepository _brugerRepo;

	public Event? Event { get; set; }
	public static Bruger? CurrentUser { get; set; }
	public int AntalTilmeldte { get; set; }

	private readonly mvp2_dk_db_eventapplikationContext _context;
	public DetaljerModel(mvp2_dk_db_eventapplikationContext context, IEventRepository eventRepo, ITilmeldingRepository tilmeldingRepo, IBrugerRepository brugerRepo)
	{
		_brugerRepo = brugerRepo;
		_eventRepo = eventRepo;
		_tilmeldingRepo = tilmeldingRepo;
		_context = context;
	}

    public IActionResult OnGet(int id)
    {
		Event = _eventRepo.Read(id);

		return Page();

    }
	
	/// <summary>
	/// OnPostDelete() kalder vores "DeleteEvent" metode fra EventRepository
	/// på et event og sletter eventet fra Databasen.
	/// Bruger omdirigeres til Event oversigten.
	/// </summary>
	public IActionResult OnPostDelete(int id)
	{
		_eventRepo.DeleteEvent(id);
		return RedirectToPage("All");
	}

    /// <summary>
    /// OnPostTilmelding() opretter en tilmelding for den bruger, der er logget ind.
    /// </summary>
    public IActionResult OnPostTilmelding(int id)
    {
		int brugerId = LoginModel.CurrentUser.Id;

		var nyTilmelding = new Tilmelding
        {
			EventId = id,
			BrugerId = brugerId,
			Dato = DateOnly.FromDateTime(DateTime.Now),
			AntalTilmeldt = 1

		};

        _tilmeldingRepo.Create(nyTilmelding);
        return RedirectToPage("All");
	}

}
