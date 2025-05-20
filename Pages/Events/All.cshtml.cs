using Event_Applikation.Models;
using Event_Applikation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Event_Applikation.Pages.Events;

public class AllModel : PageModel
{
	private readonly IEventRepository _eventrepo;

	public List<Event> Data { get; private set; }

	public AllModel(IEventRepository eventrepo)
	{
		_eventrepo = eventrepo;
		Data = new List<Event>();
	}

	public void OnGet()
	{
		Data = _eventrepo.All.ToList();
	}

	/// <summary>
	/// OnPostDelete() kalder vores "DeleteEvent" metode fra EventRepository
	/// på et event og sletter eventet fra Databasen.
	/// Bruger omdirigeres til Event oversigten.
	/// </summary>
    public IActionResult OnPostDelete(int caseId)
    {
		_eventrepo.DeleteEvent(caseId);
        return RedirectToPage("All");
    }

}