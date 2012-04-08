using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class CompanyOnlineSupportRepository : RepositoryBase<MobileContext, CompanyOnlineSupport> , ICompanyOnlineSupportRepository
    {
        public CompanyOnlineSupportRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface ICompanyOnlineSupportRepository : IRepository<CompanyOnlineSupport>
    {
    }
    
}
