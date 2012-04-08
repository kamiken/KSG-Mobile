using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Mobile.Framework.Infrastructure
{
    public abstract class RepositoryBase<TDb, T> where TDb: DbContext where T: class
    {
        private TDb dataContext;
        private readonly IDbSet<T> dbset;


        protected IDatabaseFactory<TDb> DatabaseFactory { get; private set; }
        protected TDb DataContext
        {
            get { return dataContext ?? (dataContext = DatabaseFactory.Get()); }
        }

        public RepositoryBase(IDatabaseFactory<TDb> databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbset = DataContext.Set<T>();
        }

        public virtual void Add(T entity)
        {
            dbset.Add(entity);
        }

        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            DataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbset.Remove(obj);
        }

        public virtual T GetById(long id)
        {
            return dbset.Find(id);
        }

        public T GetById(string id)
        {
            return dbset.Find(id);
        }

        public T GetById(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).FirstOrDefault();
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).FirstOrDefault<T>();
        }

        public IQueryable<T> GetAll()
        {
            return dbset;
        }

        public IQueryable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where);
        }

        public ObjectResult<TSource> ExecuteFunction<TSource>(string functionName, params ObjectParameter[] parameters)
        {
            try
            {
                var objectContext = (dataContext as IObjectContextAdapter).ObjectContext;
                return objectContext.ExecuteFunction<TSource>(objectContext.DefaultContainerName + "." + functionName, parameters);
            }
            catch { return null; }
        }

        public bool ExecuteFunctionNonReturn(string functionName, params ObjectParameter[] parameters)
        {
            try
            {
                var objectContext = (dataContext as IObjectContextAdapter).ObjectContext;
                objectContext.ExecuteFunction(objectContext.DefaultContainerName + "." + functionName, parameters);
                return true;
            }
            catch { return false; }
        }
    }
}
