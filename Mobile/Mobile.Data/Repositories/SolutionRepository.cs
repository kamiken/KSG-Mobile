using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class SolutionRepository : RepositoryBase<MobileContext, Solution> , ISolutionRepository
    {
        public SolutionRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface ISolutionRepository : IRepository<Solution>
    {
    }
    
}
