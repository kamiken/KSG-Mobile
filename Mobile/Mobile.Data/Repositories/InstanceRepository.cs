using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class InstanceRepository : RepositoryBase<MobileContext, Instance> , IInstanceRepository
    {
        public InstanceRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IInstanceRepository : IRepository<Instance>
    {
    }
    
}