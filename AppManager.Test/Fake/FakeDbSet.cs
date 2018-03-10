using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace AppManager.Test.Fake
{
    public class FakeDbSet<T> : DbSet<T>, IDbSet<T> where T : class
    {
        public FakeDbSet()
        {
            Local = new List<T>();
        }

        public FakeDbSet(List<T> data)
        {
            Local = data;
        }

        public List<T> Local { get; }

        public override T Find(params object[] keyValues)
        {
            throw new NotImplementedException("Derive from FakeDbSet<T> and override Find");
        }

        public override T Add(T item)
        {
            Local.Add(item);
            return item;
        }

        public override T Remove(T item)
        {
            Local.Remove(item);
            return item;
        }

        public override T Attach(T item)
        {
            return null;
        }

        public override T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        Type IQueryable.ElementType => Local.AsQueryable().ElementType;

        Expression IQueryable.Expression => Local.AsQueryable().Expression;

        IQueryProvider IQueryable.Provider => Local.AsQueryable().Provider;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Local.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return Local.GetEnumerator();
        }

        public T Detach(T item)
        {
            Local.Remove(item);
            return item;
        }

        public override IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            Local.AddRange(entities);
            return Local;
        }

        public override IEnumerable<T> RemoveRange(IEnumerable<T> entities)
        {
            for (var i = entities.Count() - 1; i >= 0; i--)
            {
                var entity = entities.ElementAt(i);
                if (Local.Contains(entity)) Remove(entity);
            }

            return this;
        }
    }
}