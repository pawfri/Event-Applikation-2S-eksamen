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

	public Event? Event { get; set; }
	public int AntalTilmeldte { get; set; }

	public DetaljerModel(IEventRepository eventRepo, ITilmeldingRepository tilmeldingRepo)
	{
		_eventRepo = eventRepo;
		_tilmeldingRepo = tilmeldingRepo;
	}

	public IActionResult OnGet(int id)
    {
		Event = _eventRepo.Read(id);
		AntalTilmeldte = _tilmeldingRepo.TælTilmeldte(id);

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
        OnGet(id);
        return Page();
	}

    /// <summary>
    /// OnPostFramelding() finder en tilmelding på aktuelle bruger og event.
	/// Hvis tilmelding findes, slettes event fra databasen.
    /// </summary>
    public IActionResult OnPostFramelding(int id)
    {
        int brugerId = LoginModel.CurrentUser.Id;

        var tilmelding = _tilmeldingRepo.FindBrugerOgEvent(brugerId, id);

        if (tilmelding != null)
        {
            _tilmeldingRepo.Delete(tilmelding.Id);
        }

		OnGet(id);
        return Page();
    }


}
