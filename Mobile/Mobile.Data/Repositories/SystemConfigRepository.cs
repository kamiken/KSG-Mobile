using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class SystemConfigRepository : RepositoryBase<MobileContext, SystemConfig> , ISystemConfigRepository
    {
        public SystemConfigRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface ISystemConfigRepository : IRepository<SystemConfig>
    {
    }
    
}
