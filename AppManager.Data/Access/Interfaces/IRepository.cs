using System.Data.Entity;
using System.Linq;

namespace AppManager.Data.Access.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> List();
        T Get(object id);
        int Count();
        void Add(T entity);
        void Remove(T entidade);
        void Remove(int id);
        void Update(T entidade);
        DbSet<T> DbSet { get; }
    }
}