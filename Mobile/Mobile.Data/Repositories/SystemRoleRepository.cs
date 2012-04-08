using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class SystemRoleRepository : RepositoryBase<MobileContext, SystemRole> , ISystemRoleRepository
    {
        public SystemRoleRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface ISystemRoleRepository : IRepository<SystemRole>
    {
    }
    
}
