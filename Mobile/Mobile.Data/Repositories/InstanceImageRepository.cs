using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class InstanceImageRepository : RepositoryBase<MobileContext, InstanceImage> , IInstanceImageRepository
    {
        public InstanceImageRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IInstanceImageRepository : IRepository<InstanceImage>
    {
    }
    
}
