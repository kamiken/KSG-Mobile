using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class SystemCompanyConfigRepository : RepositoryBase<MobileContext, SystemCompanyConfig> , ISystemCompanyConfigRepository
    {
        public SystemCompanyConfigRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface ISystemCompanyConfigRepository : IRepository<SystemCompanyConfig>
    {
    }
    
}
