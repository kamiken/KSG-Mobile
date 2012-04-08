using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class SystemModuleRepository : RepositoryBase<MobileContext, SystemModule> , ISystemModuleRepository
    {
        public SystemModuleRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface ISystemModuleRepository : IRepository<SystemModule>
    {
    }
    
}
