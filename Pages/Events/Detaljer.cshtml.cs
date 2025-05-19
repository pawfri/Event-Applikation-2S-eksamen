using Event_Applikation.Models;
using Event_Applikation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Event_Applikation.Pages.Events;

public class DetaljerModel : PageModel
{
    private readonly IEventRepository _eventrepo;

    //[BindProperty]
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


}
