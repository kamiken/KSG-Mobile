using Mobile.Framework.Infrastructure;
using Mobile.Data;
using System;
namespace Mobile.Business
{
    public class BLLBase : Disposable
    {
        private readonly IUnitOfWork unitOfWork;

        private string ConnectionString
        {
            get { return System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString; }
        }

        protected IDatabaseFactory<MobileContext> DatabaseFactory { get; private set; }

        public BLLBase()
        {
            DatabaseFactory = new DatabaseFactory<MobileContext>(ConnectionString);
            this.unitOfWork = new UnitOfWork(DatabaseFactory);
        }

        public void SaveChanges()
        {
            unitOfWork.Commit();
        }
        public MobileContext Context { get { return DatabaseFactory.Get(); } }

        
    }
}
