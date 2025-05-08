using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Event_Applikation.Services.Interfaces;
using Event_Applikation.Models;

namespace Event_Applikation.Pages.Events;

public class CreateModel : PageModel
{
	private IEventRepository _eventRepo;   //TODO lav createmodel

	[BindProperty]
	public Event? NewEvent { get; set; }

	public CreateModel(IEventRepository eventRepo)
	{
		_eventRepo = eventRepo;
	}



	public void OnGet()
	{
	}

	public IActionResult OnPost()
	{
        if (!ModelState.IsValid || NewEvent == null)
        {
			return Page();
		}



		_eventRepo.Create(NewEvent);   //Laver nyt event og redirecter til en page
		return RedirectToPage("All");
	}
}
