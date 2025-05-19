using Event_Applikation.Models;

namespace Event_Applikation.Services.Interfaces;

/// <summary>
/// Interface for et Repository der rummer Event-objekter
/// </summary>
public interface IEventRepository
{
	int Create(Event @event);
	List<Event> All { get; }
    bool DeleteEvent(int caseId);
}