using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class CompanyRepository : RepositoryBase<MobileContext, Company> , ICompanyRepository
    {
        public CompanyRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface ICompanyRepository : IRepository<Company>
    {
    }
    
}
