using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mobile.Framework.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();
        void Commit();
        void RollBackTransaction();
    }
}
