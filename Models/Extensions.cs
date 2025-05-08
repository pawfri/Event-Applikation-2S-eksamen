//namespace Event_Applikation.Models;
using Event_Applikation.Models.BaseClasses;

namespace Event_Applikation.Models;

public partial class Event : IHasId
{
	public override string ToString()
	{
		return $"[Event {Id}] {Titel} " +
			$"Starttid: {Starttid}" +
			$"Sluttid: {Sluttid}, " +
			$"Lokation {Lokation}" +
			$"Antalsbegrænsning: {Antalsbegrænsning}" +
			$"Beskrivelse: {Beskrivelse}";
	}

	/// <summary>
	/// Alternativet til en klassisk constructor til Events:
	/// </summary>

	public static Event Create(string titel, DateTime Starttid, DateTime Sluttid, string beskrivelse, bool Antalsbegrænsning, string lokation)
	{
		return new Event { Titel = titel, Starttid = Starttid, Sluttid = Sluttid, Beskrivelse = beskrivelse, Lokation = lokation };
	}






}