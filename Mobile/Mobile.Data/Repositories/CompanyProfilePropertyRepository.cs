using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class CompanyProfilePropertyRepository : RepositoryBase<MobileContext, CompanyProfileProperty> , ICompanyProfilePropertyRepository
    {
        public CompanyProfilePropertyRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface ICompanyProfilePropertyRepository : IRepository<CompanyProfileProperty>
    {
    }
    
}
