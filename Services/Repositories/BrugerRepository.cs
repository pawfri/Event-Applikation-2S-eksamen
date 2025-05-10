using Event_Applikation.Models;
using Event_Applikation.Services.Interfaces;
using Event_Applikation.Models.BaseClasses;
using Event_Applikation.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Event_Applikation.Services.Repositories;

public class BrugerRepository : IBrugerRepository

{
    public List<string> Roller
    {
        get { return new List<string> { "Admin", "Medlem", "Studerende" }; }
    }

    private readonly mvp2_dk_db_eventapplikationContext _context;

    public BrugerRepository(mvp2_dk_db_eventapplikationContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Henter alle brugere fra databasen som en liste via GetAllWithIncludes-kaldet.
    /// </summary>
    public List<Bruger> All
    {
        get
        {
            return GetAllWithIncludes(_context).ToList();
        }
    }
    
    /// <summary>
    /// Håndterer verificering af at bruger oplysninger er korrekte 
    /// og returnere brugeren såfremt input matcher, ellers null.
    /// </summary>
    public Bruger? VerifyUser(string providedUserName, string providedPassword)
    {
        Bruger? bruger = All.FirstOrDefault(b => b.Navn == providedUserName);

        // Bruger findes ikke eller har angivet forkert adgangskode
        if (bruger == null || !VerifyPassword(bruger, providedPassword))
            return null;

        return bruger;
    }

    /// <summary>
    /// Matcher om den angivne adgangskode matcher brugeren adgangskode.
    /// </summary>
    private bool VerifyPassword(Bruger bruger, string providedPassword)
    {
        return bruger.Adgangskode == providedPassword;
    }

    /// <summary>
    /// Laver en forespørgsel på databasen, for at hente alle brugere 
    /// og deres rolle fra databasen.
    /// </summary>
    protected virtual IQueryable<Bruger> GetAllWithIncludes(DbContext context)
	{
		return context.Set<Bruger>()
        .Include(b => b.Rolle);
    }

}
