//namespace Event_Applikation.Models;
using Event_Applikation.Models.BaseClasses;
using System.ComponentModel.DataAnnotations;

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

	public static Event Create(string titel, TimeOnly Starttid, TimeOnly Sluttid, string beskrivelse, bool Antalsbegrænsning, string lokation)
	{
		return new Event { Titel = titel, Starttid = Starttid, Sluttid = Sluttid, Beskrivelse = beskrivelse, Lokation = lokation };
	}

}

public partial class Kategori : IHasId
{
	public override string ToString()
	{
		return $"[Kategori ID: {Id}]" +
			$"Type: {Type}";
	}
}