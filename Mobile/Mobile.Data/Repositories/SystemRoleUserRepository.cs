using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class SystemRoleUserRepository : RepositoryBase<MobileContext, SystemRoleUser> , ISystemRoleUserRepository
    {
        public SystemRoleUserRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface ISystemRoleUserRepository : IRepository<SystemRoleUser>
    {
    }
    
}
