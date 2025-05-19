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
    public DateTime AktuelMåned { get; set; }
    public DateTime ForrigeMåned { get; set; }
    public DateTime NæsteMåned { get; set; }

    public KalenderModel(IEventRepository eventrepo)
    {
        _eventrepo = eventrepo;
    }

    /// <summary>
    /// Indlæser og sorterer events, og klargør til navigation mellem måneder.
    /// </summary>
    public void OnGet(int? måned, int? år)
    {
        // Sætter dags dato
        DagsDato = DateTime.Today;

        // Brug angivne måned og år, ellers dags dato
        int valgtMåned = måned ?? DagsDato.Month;
        int valgtÅr = år ?? DagsDato.Year;
        
        // Opretter en DateTime og sætter AktuelMåned til d. 1. i måneden
        AktuelMåned = new DateTime(valgtÅr, valgtMåned, 1);
        
        // Bruger AktuelMåned til at beregne forrige og næste måned
        ForrigeMåned = AktuelMåned.AddMonths(-1);
        NæsteMåned = AktuelMåned.AddMonths(1);

        // Henter events fra repo, der matcher AktuelMåned og sorterer på dato og tid
        Data = _eventrepo.All
            .Where(e => e.Dato.Month == AktuelMåned.Month && e.Dato.Year == AktuelMåned.Year)
            .OrderBy(e => e.Dato)
            .ThenBy(e => e.Starttid)
            .ToList();
    }

    public DateTime VisDenneMåned()
    {
        return DateTime.Today;
    }

    public DateTime VisNæsteMåned()
    {
        return AktuelMåned.AddMonths(1);
    }

    public DateTime VisForrigeMåned()
    {
        return AktuelMåned.AddMonths(-1);
    }

}