using Event_Applikation.Models.BaseClasses;
using Microsoft.EntityFrameworkCore;

namespace Event_Applikation.Services.BaseClasses;

/// <summary>
/// Superklasse for alle Repository-klasser.
/// Et "repository" er et Design Pattern for en klasse, der har til ansvar 
/// at holde styr på et antal objekter af samme slags, f.eks. alle Product-objekter.
/// NB: Denne klasse benytter "generics", hvilket kan ses på brugen af den
/// såkaldte type-parameter <T>. Når en klasse arver fra Repository, skal den 
/// indsætte den specifikke klasse som sub-klassen skal holde styr på. f.eks. Product.
/// </summary>
public abstract class Repository<T> where T : class, IHasId
{
    private Dictionary<int, T> _data;

    public List<T> All { get { return _data.Values.ToList(); } }

    public Repository()
    {
        _data = new Dictionary<int, T>();

        // Hvis vi ønsker at bruge "mock"-data, fyldes disse
        // ind i repository fra start. 
        //if (MockDataSettings.UseMockData)
        //{
        //    foreach (T t in GetMockData())
        //    {
        //        Create(t);
        //    }
        //}
    }

    ///// <summary>
    ///// Denne metode er abstract, da super-klassen ikke kan afgøre hvordan
    ///// den skal implementeres for en konkret klasse. Det må sub-klasen gøre.
    ///// </summary>
    //public abstract List<T> GetMockData();

    /// <summary>
    /// Create: Find det næste ledige Id, sæt det på det givne objekt, 
    /// og gem objektet i den interne Dictionary.
    /// </summary>
    /// <param name="t">Det objekt der nu skal gemmes i repository</param>
    public void Create(T t)
    {
        t.Id = NextId();

        _data[t.Id] = t;
    }

    /// <summary>
    /// Read: Find det objekt der matcher det givne Id, og returnér det.
    /// Hvis ikke der findes et objekt med det givne Id, returneres null.
    /// </summary>
    public T? Read(int id)
    {
        if (_data.ContainsKey(id))
        {
            return _data[id];
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Update: NB: Bruges p.t. ikke i denne app.
    /// </summary>
    //public bool Update(int id, T t)
    //{
    //    if (_data.ContainsKey(id))
    //    {
    //        UpdateObject(_data[id], t);
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    /// <summary>
    /// Delete: Prøv at slette det objekt der matcher det givne Id.
    /// </summary>
    public bool Delete(int id)
    {
        return _data.Remove(id);
    }

    protected abstract DbContext CreateDbContext();
        

    ////Udkommenteret fordi vi ikke pt. skal opdatere vores event (måske i senere sprint)
    //protected abstract void UpdateObject(T tExisting, T t);

    /// <summary>
    /// Det næste Id udregnes ved at finde det højeste Id der 
    /// p.t. er brugt, og lægge 1 til dette.
    /// </summary>
    private int NextId()
    {
        int highestId = 0;

        foreach (int id in _data.Keys)
        {
            if (id > highestId)
            {
                highestId = id;
            }
        }

        return highestId + 1;
    }
}
