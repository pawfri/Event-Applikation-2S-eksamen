using Event_Applikation.Services.BaseClasses;
using Event_Applikation.Models;
using Event_Applikation.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
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
    public void Create(Event @event)
    {
		_context.Events.Add(@event);
        _context.SaveChanges();
    }
}