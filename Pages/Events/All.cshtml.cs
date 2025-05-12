using Event_Applikation.Models;
using Event_Applikation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Event_Applikation.Pages.Events;

public class AllModel : PageModel
{
	private readonly IEventRepository _eventrepo;


	public List<Event> Data { get; private set; }

	public AllModel(IEventRepository eventrepo)
	{
		_eventrepo = eventrepo;
	}

	public void OnGet()
	{
		Data = _eventrepo.All.ToList();
	}
}
