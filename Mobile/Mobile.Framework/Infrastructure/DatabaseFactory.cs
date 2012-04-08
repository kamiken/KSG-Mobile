using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Mobile.Framework.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private string connectionString;
        private DbContext dataContext;

        protected override void DisposeCore()
        {
            dataContext.Dispose();
            base.DisposeCore();
        }

        public DatabaseFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }
        
        public System.Data.Entity.DbContext Get()
        {
            return dataContext ?? (dataContext = new DbContext(connectionString));
        }
    }

    public class DatabaseFactory<T> : Disposable, IDatabaseFactory<T> where T : DbContext
    {
        private string connectionString;
        private T dataContext;
        private Dictionary<string, object> _dicDataContext;

        protected override void DisposeCore()
        {
            dataContext.Dispose();
            base.DisposeCore();
        }

        public DatabaseFactory(string connectionString)
        {
            this.connectionString = connectionString;
            _dicDataContext = new Dictionary<string, object>();
        }

        public T Get()
        {
            if (dataContext == null)
            {
                var type = typeof(T);
                dataContext = (T)type.InvokeMember(type.Name, System.Reflection.BindingFlags.CreateInstance, null, null, new object[] { connectionString });
                _dicDataContext[connectionString] = dataContext;
            }
            return dataContext;
        }

        public TSource Get<TSource>(string connectionString) where TSource : class
        {
            if (!_dicDataContext.ContainsKey(connectionString))
            {
                var type = typeof(TSource);
                _dicDataContext.Add(connectionString, type.InvokeMember(type.Name, System.Reflection.BindingFlags.CreateInstance, null, null, new object[] { connectionString }));
            }
            return _dicDataContext[connectionString] as TSource;
        }

        DbContext IDatabaseFactory.Get()
        {
            return Get();
        }
    }
}
