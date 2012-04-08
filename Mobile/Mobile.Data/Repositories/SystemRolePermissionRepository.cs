using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class SystemRolePermissionRepository : RepositoryBase<MobileContext, SystemRolePermission> , ISystemRolePermissionRepository
    {
        public SystemRolePermissionRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface ISystemRolePermissionRepository : IRepository<SystemRolePermission>
    {
    }
    
}
