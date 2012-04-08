using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class SystemUserPermissionRepository : RepositoryBase<MobileContext, SystemUserPermission> , ISystemUserPermissionRepository
    {
        public SystemUserPermissionRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface ISystemUserPermissionRepository : IRepository<SystemUserPermission>
    {
    }
    
}
