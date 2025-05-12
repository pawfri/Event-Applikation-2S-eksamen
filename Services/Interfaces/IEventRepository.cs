namespace Event_Applikation.Services.Interfaces;
using Event_Applikation.Models;
using Event_Applikation.Models.BaseClasses;

/// <summary>
/// Interface for et Repository der rummer Event-objekter
/// </summary>
public interface IEventRepository
{
	int Create(Event @event);
}