using Event_Applikation.Models;
using Event_Applikation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Event_Applikation.Pages.Events;

public class DetaljerModel : PageModel
{
    private readonly IEventRepository _eventrepo;

    public Event? Event { get; set; }

    public DetaljerModel(IEventRepository eventrepo)
	{
		_eventrepo = eventrepo;
	}

    public IActionResult OnGet(int id)
    {
        Event = _eventrepo.Read(id);

		return Page();

        
    }
	//Flyttet fra "All" siden
	/// <summary>
	/// OnPostDelete() kalder vores "DeleteEvent" metode fra EventRepository
	/// på et event og sletter eventet fra Databasen.
	/// Bruger omdirigeres til Event oversigten.
	/// </summary>
	public IActionResult OnPostDelete(int id)
	{
		_eventrepo.DeleteEvent(id);
		return RedirectToPage("All");
	}

}
