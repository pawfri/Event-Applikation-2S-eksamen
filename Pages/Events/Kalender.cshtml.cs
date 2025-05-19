using Event_Applikation.Models;
using Event_Applikation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Event_Applikation.Pages.Events;

public class KalenderModel : PageModel
{
    private readonly IEventRepository _eventrepo;

    public List<Event> Data { get; private set; }
    public DateTime DagsDato { get; set; }
    public DateTime AktuelM�ned { get; set; }
    public DateTime ForrigeM�ned { get; set; }
    public DateTime N�steM�ned { get; set; }

    public KalenderModel(IEventRepository eventrepo)
    {
        _eventrepo = eventrepo;
    }

    /// <summary>
    /// Indl�ser og sorterer events, og klarg�r til navigation mellem m�neder.
    /// </summary>
    public void OnGet(int? m�ned, int? �r)
    {
        // S�tter dags dato
        DagsDato = DateTime.Today;

        // Brug angivne m�ned og �r, ellers dags dato
        int valgtM�ned = m�ned ?? DagsDato.Month;
        int valgt�r = �r ?? DagsDato.Year;
        
        // Opretter en DateTime og s�tter AktuelM�ned til d. 1. i m�neden
        AktuelM�ned = new DateTime(valgt�r, valgtM�ned, 1);
        
        // Bruger AktuelM�ned til at beregne forrige og n�ste m�ned
        ForrigeM�ned = AktuelM�ned.AddMonths(-1);
        N�steM�ned = AktuelM�ned.AddMonths(1);

        // Henter events fra repo, der matcher AktuelM�ned og sorterer p� dato og tid
        Data = _eventrepo.All
            .Where(e => e.Dato.Month == AktuelM�ned.Month && e.Dato.Year == AktuelM�ned.Year)
            .OrderBy(e => e.Dato)
            .ThenBy(e => e.Starttid)
            .ToList();
    }

    public DateTime VisDenneM�ned()
    {
        return DateTime.Today;
    }

    public DateTime VisN�steM�ned()
    {
        return AktuelM�ned.AddMonths(1);
    }

    public DateTime VisForrigeM�ned()
    {
        return AktuelM�ned.AddMonths(-1);
    }

}