using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Mobile.Framework.Infrastructure
{
    public class UnitOfWork : Disposable, IUnitOfWork
    {
        private readonly IDatabaseFactory databaseFactory;
        private DbContext dataContext;
        private System.Data.Common.DbTransaction _dbTrans;
        private bool _saved;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
            _saved = false;
        }

        protected override void DisposeCore()
        {
            if (!_saved) Commit();
            databaseFactory.Dispose();
            base.DisposeCore();
        }

        protected DbContext DataContext
        {
            get { return dataContext ?? (dataContext = databaseFactory.Get()); }
        }

        public void BeginTransaction()
        {
            _dbTrans = DataContext.Database.Connection.BeginTransaction();
        }

        public void Commit()
        {
            _saved = true;
            DataContext.SaveChanges();
            if (_dbTrans != null)
            {
                var conn = _dbTrans.Connection;
                _dbTrans.Commit();
                conn.Close();
            }
        }

        public void RollBackTransaction()
        {
            if (_dbTrans != null)
            {
                var conn = _dbTrans.Connection;
                _dbTrans.Rollback();
                conn.Close();
            }
        }
    }
}
