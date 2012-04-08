using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class ManufacturerRepository : RepositoryBase<MobileContext, Manufacturer> , IManufacturerRepository
    {
        public ManufacturerRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IManufacturerRepository : IRepository<Manufacturer>
    {
    }
    
}
