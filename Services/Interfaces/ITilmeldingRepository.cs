using Event_Applikation.Models;

namespace Event_Applikation.Services.Interfaces;

public interface ITilmeldingRepository
{
    int Create(Tilmelding tilmelding);
    bool Delete(int id);
    int TælTilmeldte(int eventid);
}
