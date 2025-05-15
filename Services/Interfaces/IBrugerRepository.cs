using Event_Applikation.Models;
using Event_Applikation.Models.BaseClasses;
using Event_Applikation.Services.BaseClasses;

namespace Event_Applikation.Services.Interfaces;

public interface IBrugerRepository
{
    /// <summary>
    /// Returnerer alle objekter i repository.
    /// </summary>
    List<Bruger> All { get; }

    int Create(Bruger bruger);
    Bruger? VerifyUser(string providedUserName, string providedPassword);
    List<string> Roller { get; }
}