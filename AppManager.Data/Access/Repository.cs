using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AppManager.Data.Access.Interfaces;

namespace AppManager.Data.Access
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppManagerDbContext Context;
        protected readonly DbSet<T> Set;

        public Repository(AppManagerDbContext context)
        {
            Context = context;
            Set = Context.Set<T>();
        }

        public IQueryable<T> List()
        {
            return Set;
        }

        public T Get(object id)
        {
            return Set.Find(id);
        }

        public int Count()
        {
            return Set.Count();
        }

        public void Add(T entity)
        {
            Set.Add(entity);
        }

        public void Remove(T entidade)
        {
            DbEntityEntry<T> dbEntityEntry = Context.Entry(entidade);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                Set.Attach(entidade);
                Set.Remove(entidade);
            }
        }

        public void Remove(int id)
        {
            var entry = Get(id);
            if (entry == null) return;

            Remove(entry);
        }

        public void Update(T entidade)
        {
            DbEntityEntry<T> entry = Context.Entry(entidade);
            if (entry.State == EntityState.Detached)
            {
                Set.Attach(entidade);
                entry = Context.Entry(entidade);
            }

            entry.State = EntityState.Modified;
        }

        public DbSet<T> DbSet => Set;
    }
}