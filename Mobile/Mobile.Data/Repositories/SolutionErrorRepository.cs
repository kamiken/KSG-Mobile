using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class SolutionErrorRepository : RepositoryBase<MobileContext, SolutionError> , ISolutionErrorRepository
    {
        public SolutionErrorRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface ISolutionErrorRepository : IRepository<SolutionError>
    {
    }
    
}
