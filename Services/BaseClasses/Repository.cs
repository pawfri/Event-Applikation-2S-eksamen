using Event_Applikation.Models;
using Event_Applikation.Models.BaseClasses;
using Microsoft.EntityFrameworkCore;
namespace Event_Applikation.Services.BaseClasses;


/// <summary>
/// Base-klasse for en EFCore-baseret implementation af IRepository interfacet.
/// </summary>
public abstract class Repository<T> where T : class, IHasId
{
    public List<T> All
    {
        get
        {
            using DbContext context = CreateDbContext();

            return GetAllWithIncludes(context).ToList();
        }
    }

    public virtual int Create(T t)
    {
        using DbContext context = CreateDbContext();

        int id = NextId();
        t.Id = id;

        context.Set<T>().Add(t);
        context.SaveChanges();

        return t.Id;
    }

    public T? Read(int id)
    {
        using DbContext context = CreateDbContext();

        return GetAllWithIncludes(context).FirstOrDefault(t => t.Id == id);
    }

    public bool Delete(int id)
    {
        using DbContext context = CreateDbContext();

        T? tEx = GetAllWithIncludes(context).FirstOrDefault(t => t.Id == id);
        if (tEx == null)
            return false;

        context.Set<T>().Remove(tEx);
        return (context.SaveChanges() > 0);
    }

    /// <summary>
    /// Denne metode skal returnere alle objekter af type T, 
    /// med alle objekt-referencer sat korrekt. Sub-klasser
    /// kan override denne metode, hvis nødvendigt.
    /// </summary>
    protected virtual IQueryable<T> GetAllWithIncludes(DbContext context)
    {
        return context.Set<T>();
    }

    /// <summary>
    /// Denne metode skal returnere den specifikke DBContext-klasse
    /// der bruges i en specifik applikation.
    /// </summary>
    protected abstract DbContext CreateDbContext();

    private int NextId()
    {
        return All.Select(t => t.Id).DefaultIfEmpty(0).Max() + 1;
    }
}
