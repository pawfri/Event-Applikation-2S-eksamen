using Event_Applikation.Models;
using Event_Applikation.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Event_Applikation.Services.Repositories;

public class EventRepository : IEventRepository
{

	private readonly mvp2_dk_db_eventapplikationContext _context;

    public EventRepository(mvp2_dk_db_eventapplikationContext context)
    {
        _context = context;
    }

    /// <summary>
    /// CRUD operation: Håndtering af oprettelse 
    /// af nyt Event og gemmer det i databasen.
    /// </summary>
    public int Create(Event @event)
    {
        @event.Id = NextId();
        _context.Events.Add(@event);
        _context.SaveChanges();

        return @event.Id;
    }

    /// <summary>
    /// Bruges pt. ikke, da vi ikke har en OnPost() som skal hente data
    /// men blot en OnGet(), hvorved vi blot kan bruge en liste til at hente data.
    /// </summary>
    public Event? Read(int id)
    {
        return GetAllWithIncludes(_context).FirstOrDefault(e => e.Id == id);
    }


    public List<Event> All => GetAllWithIncludes(_context).ToList();

    protected virtual IQueryable<Event> GetAllWithIncludes(DbContext context)
    {
        return context.Set<Event>()
                      .Include(e => e.Kategori);
    }

    /// <summary>
	/// Hjælpemetode til Create-metoden, der finder 
	/// næste tilgængelige Id.
	/// </summary>
	private int NextId()
    {
        return _context.Events.Select(e => e.Id).ToList().DefaultIfEmpty(0).Max() + 1;
	}

    /// <summary>
    /// Finder event med det pågældende id i databasen.
    /// Sletter det hvis det findes og returnerer true, 
    /// ellers returnerer false.
    /// </summary>
    public bool DeleteEvent(int caseId)
    {
        Event? @event = _context.Events.Find(caseId);
        
        if (@event != null)
        {
            _context.Events.Remove(@event);
            _context.SaveChanges();
            return true;
        }
        return false;
    }
}