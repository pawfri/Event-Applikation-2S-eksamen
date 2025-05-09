using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Event_Applikation.Services.Interfaces;
using Event_Applikation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;


namespace Event_Applikation.Pages.Events;




public class CreateModel : PageModel
{
	private IEventRepository _eventRepo;
	private readonly mvp2_dk_db_eventapplikationContext _context;

	[BindProperty]
	public Event NewEvent { get; set; }

	public SelectList KategoriList { get; set; }

	public CreateModel(mvp2_dk_db_eventapplikationContext context, IEventRepository eventrepo)
	{
		_context = context;
		_eventRepo = eventrepo;
	}

	/// <summary>
	/// Når Create-siden hentes kaldes metoden: "LoadKategorier", så de kategorierne kan benyttes på siden.
	/// </summary>
	public void OnGet()
	{
		LoadKategorier();
	}

    /// <summary>
    /// Håndterer at kontrollere og indsende input, for at oprette et nyt event.
    /// </summary>
    public IActionResult OnPostSubmit()
	{
		//Kontrol af gyldig data
		if (!ModelState.IsValid || NewEvent == null)
		{
			return Page();
		}

        //Opretter et nyt event og omdirigerer til All-Event siden
        _eventRepo.Create(NewEvent);
		return RedirectToPage("All");
	}

    /// <summary>
    /// Metode, der indlæser kategorier fra databasen, for at de kan anvendes i drop-down menu i applikationen.
    /// </summary>
    public void LoadKategorier()
	{
		KategoriList = new SelectList(_context.Kategoris, "Id", "Type");
	}

}
