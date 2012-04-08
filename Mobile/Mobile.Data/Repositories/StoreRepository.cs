using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class StoreRepository : RepositoryBase<MobileContext, Store> , IStoreRepository
    {
        public StoreRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IStoreRepository : IRepository<Store>
    {
    }
    
}
