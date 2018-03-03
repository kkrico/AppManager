using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace AppManager.Data.Access
{
    internal interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T Get(object id);
        int Count();
        void Add(T entity);
        void Remove(T entidade);
        void Remove(int id);
        void Update(T entidade);
        DbSet<T> DbSet { get; }
    }
}