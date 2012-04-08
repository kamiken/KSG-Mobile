using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class SystemPermissionRepository : RepositoryBase<MobileContext, SystemPermission> , ISystemPermissionRepository
    {
        public SystemPermissionRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface ISystemPermissionRepository : IRepository<SystemPermission>
    {
    }
    
}
