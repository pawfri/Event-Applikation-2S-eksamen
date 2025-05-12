using Event_Applikation.Services.BaseClasses;
using Event_Applikation.Models;
using Event_Applikation.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Event_Applikation.Models.BaseClasses;

namespace Event_Applikation.Services.Repositories;

public class EventRepository : IEventRepository
{
	
	private readonly mvp2_dk_db_eventapplikationContext _context;

    public EventRepository(mvp2_dk_db_eventapplikationContext context)
    {
        _context = context;
    }

    public IEnumerable<object> All { get; private set; }

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
    /// Hjælpemetode til Create-metoden, der finder 
    /// næste tilgængelige Id.
    /// </summary>
    private int NextId()
    {
        return _context.Events.Select(e => e.Id).ToList().DefaultIfEmpty(0).Max() + 1;
    }
}

