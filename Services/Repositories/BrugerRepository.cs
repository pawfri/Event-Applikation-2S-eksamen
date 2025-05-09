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
        get { return new List<string> { "admin", "medlem", "Studerende" }; }
    }

    private readonly mvp2_dk_db_eventapplikationContext _context;

    public BrugerRepository(mvp2_dk_db_eventapplikationContext context)
    {
        _context = context;
    }

    //public List<Bruger> All { get; }

    public List<Bruger> All
    {
        get
        {
            return GetAllWithIncludes(_context).ToList();
        }
    }

    public Bruger? VerifyUser(string providedUserName, string providedPassword)
    {
        Bruger? bruger = All.FirstOrDefault(b => b.Navn == providedUserName);

        // Bruger findes ikke eller har angivet forkert password
        if (bruger == null || !VerifyPassword(bruger, providedPassword))
            return null;

        return bruger;
    }

    private bool VerifyPassword(Bruger user, string providedPassword)
    {
        return user.Adgangskode == providedPassword;
    }



	protected virtual IQueryable<Bruger> GetAllWithIncludes(DbContext context)
	{
		return context.Set<Bruger>();
	}

}


//Repository<Bruger, mvp2_dk_db_eventapplikationContext>,

