using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class CompanyProfileRepository : RepositoryBase<MobileContext, CompanyProfile> , ICompanyProfileRepository
    {
        public CompanyProfileRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface ICompanyProfileRepository : IRepository<CompanyProfile>
    {
    }
    
}
