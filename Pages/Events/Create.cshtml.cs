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

	//public List<Kategori> KategoriList { get; set; }
	public SelectList KategoriList { get; set; }

	public CreateModel(mvp2_dk_db_eventapplikationContext context, IEventRepository eventrepo)
	{
		_context = context;
		_eventRepo = eventrepo;
	}

	public void OnGet()
	{
		LoadKategorier();
	}

	public IActionResult OnPost()
	{
		if (!ModelState.IsValid || NewEvent == null)
		{
			return Page();
		}

		//_eventRepo.Add(NewEvent);
		_eventRepo.Create(NewEvent);   //Laver nyt event og redirecter til en page
		return RedirectToPage("All");
	}

	public void LoadKategorier()
	{
		KategoriList = new SelectList(_context.Kategoris, "Id", "Type");

		//KategoriList = _context.Kategoris
		//	.Select(k => new Kategori
		//	{
		//		Id = k.Id.ToString()

		//		Type = k.Type
		//	}).ToList();
	}

}
