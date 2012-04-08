using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class AdvertisingRepository : RepositoryBase<MobileContext, Advertising> , IAdvertisingRepository
    {
        public AdvertisingRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IAdvertisingRepository : IRepository<Advertising>
    {
    }
    
}
