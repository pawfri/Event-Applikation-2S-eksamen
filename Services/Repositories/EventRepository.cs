using Event_Applikation.Services.BaseClasses;
using Event_Applikation.Models;
using Event_Applikation.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

namespace Event_Applikation.Services.Repositories;

//public class EventRepository : Repository<Event>, IEventRepository
//{
//    protected override DbContext CreateDbContext()
//    {
//        return new mvp2_dk_db_eventapplikationContext();
//    }
//}



public class EventRepository : IEventRepository
{
    private readonly mvp2_dk_db_eventapplikationContext _context;

    public EventRepository(mvp2_dk_db_eventapplikationContext context)
    {
        _context = context;
    }

    public void Create(Event @event)
    {
        _context.Events.Add(@event);
        _context.SaveChanges(); // This writes to the database
    }
}