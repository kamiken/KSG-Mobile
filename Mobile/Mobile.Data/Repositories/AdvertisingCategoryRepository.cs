using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class AdvertisingCategoryRepository : RepositoryBase<MobileContext, AdvertisingCategory> , IAdvertisingCategoryRepository
    {
        public AdvertisingCategoryRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IAdvertisingCategoryRepository : IRepository<AdvertisingCategory>
    {
    }
    
}
