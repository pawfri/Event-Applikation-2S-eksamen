using Event_Applikation.Models;

namespace Event_Applikation.Services.Interfaces;

/// <summary>
/// Interface for et Repository der rummer Tilmelding-objekter
/// </summary>
public interface ITilmeldingRepository
{
    int Create(Tilmelding tilmelding);
    bool Delete(int id);
    int TælTilmeldte(int eventid);
    Tilmelding? FindBrugerOgEvent(int brugerId, int eventId);
}
