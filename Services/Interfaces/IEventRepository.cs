namespace Event_Applikation.Services.Interfaces;
using Event_Applikation.Models;

/// <summary>
/// Interface for et Repository der rummer Event-objekter
/// </summary>
public interface IEventRepository
{
    //List<Event> All { get; }
    void Create(Event @event);
}