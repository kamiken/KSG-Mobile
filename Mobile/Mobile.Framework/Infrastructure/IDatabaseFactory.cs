using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Mobile.Framework.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        DbContext Get();
    }

    public interface IDatabaseFactory<T> : IDatabaseFactory where T : DbContext
    {
        new T Get();
        TSource Get<TSource>(string connectionString) where TSource : class;
    }
}
