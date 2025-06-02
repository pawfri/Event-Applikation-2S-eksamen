using Event_Applikation.Models;
using Event_Applikation.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Event_Applikation.Services.Repositories;

public class TilmeldingRepository : ITilmeldingRepository
{
    private readonly mvp2_dk_db_eventapplikationContext _context;

    public TilmeldingRepository(mvp2_dk_db_eventapplikationContext context)
    {
        _context = context;
    }

    /// <summary>
    /// CRUD operation: Håndtering af oprettelse 
    /// af ny tilmelding og gemmer det i databasen.
    /// </summary>
    public int Create(Tilmelding tilmelding)
    {
        tilmelding.Id = NextId();
        _context.Tilmeldings.Add(tilmelding);
        _context.SaveChanges();

        return tilmelding.Id;
    }

    /// <summary>
	/// Hjælpemetode til Create-metoden, der finder 
	/// næste tilgængelige Id.
	/// </summary>
	private int NextId()
    {
        return _context.Tilmeldings.Select(e => e.Id).ToList().DefaultIfEmpty(0).Max() + 1;
    }

    /// <summary>
    /// Finder tilmelding med det pågældende id i databasen.
    /// Sletter det hvis det findes og returnerer true, 
    /// ellers returnerer false.
    /// </summary>
    //TODO: Har vi pt. ikke implementeret til brug.
    public bool Delete(int id)
    {
        Tilmelding? tilmelding = _context.Tilmeldings.Find(id);

        if (tilmelding != null)
        {
            _context.Tilmeldings.Remove(tilmelding);
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    //TODO: Ikke implementeret endnu..
    public int TælTilmeldte(int eventid)
    {
        return _context.Tilmeldings.Count(t => t.EventId == eventid);
    }
}
