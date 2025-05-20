using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Event_Applikation.Services.Interfaces;
using Event_Applikation.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;


namespace Event_Applikation.Pages.Events;


[Authorize(Roles = "Admin,Medlem")]
public class CreateModel : PageModel
{
    private IEventRepository _eventRepo;
    private readonly mvp2_dk_db_eventapplikationContext _context;

    [BindProperty]
    public Event NytEvent { get; set; }
    public SelectList KategoriList { get; set; }

    public CreateModel(mvp2_dk_db_eventapplikationContext context, IEventRepository eventrepo)
    {
        _context = context;
        _eventRepo = eventrepo;
    }

    /// <summary>
    /// Når Create-siden hentes kaldes metoden: "LoadKategorier", 
    /// så de kategorierne kan benyttes på siden.
    /// </summary>
    public void OnGet()
    {
        LoadKategorier();
    }

    /// <summary>
    /// Håndterer oprettelse af nyt event.
    /// </summary>
    public IActionResult OnPostSubmit()
    {
        // Kontrol af gyldig data
        if (!ModelState.IsValid || NytEvent == null || !ErDatoOgTidGyldig(NytEvent))
        {
            OnGet();
            return Page();
        }

        // Opretter et nyt event og omdirigerer til All-Event siden
        _eventRepo.Create(NytEvent);
        return RedirectToPage("All");
    }

    /// <summary>
    /// Indlæser kategorier fra databasen, for at de kan anvendes
	/// i drop-down menu i applikationen. Kaldes i OnGet().
    /// </summary>
    public void LoadKategorier()
    {
        KategoriList = new SelectList(_context.Kategoris, "Id", "Type");
    }

    /// <summary>
    /// Kontroller om dato og tid er overskredet og at sluttid
    /// er senere end starttid. Kaldes i OnPostSubmit().
    /// </summary>
    public bool ErDatoOgTidGyldig(Event @event)
    {
        DateTime startdatetime = @event.Dato.ToDateTime(@event.Starttid);
        DateTime slutdatetime = @event.Dato.ToDateTime(@event.Sluttid);

        // event skal starte i fremtiden, og slutte efter det begynder
        return startdatetime >= DateTime.Now && slutdatetime > startdatetime;
    }

}
