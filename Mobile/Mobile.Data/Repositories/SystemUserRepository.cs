using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class SystemUserRepository : RepositoryBase<MobileContext, SystemUser> , ISystemUserRepository
    {
        public SystemUserRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface ISystemUserRepository : IRepository<SystemUser>
    {
    }
    
}
