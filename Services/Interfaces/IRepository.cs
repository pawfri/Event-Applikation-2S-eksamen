using Event_Applikation.Models.BaseClasses;

namespace Event_Applikation.Services.Interfaces;

public interface IRepository<T> where T : IHasId
{
	/// <summary>
	/// Returnerer alle objekter i repository.
	/// </summary>
	List<T> All { get; }

	/// <summary>
	/// Gemmer det givne objekt i repository, og tildeler det et nyt Id.
	/// </summary>
	/// <returns>Det valgte Id for objektet</returns>
	int Create(T t);

	/// <summary>
	/// Læser det objekt der matcher det givne Id.
	/// </summary>
	/// <returns>Objekt der matcher Id, null hvis ingen objekter matcher.</returns>
	T? Read(int id);

	/// <summary>
	/// Sletter det objekt der matcher det givne Id.
	/// </summary>
	/// <returns>true if et objekt blev slettet, ellers falsefalse</returns>
	bool Delete(int id);
}
