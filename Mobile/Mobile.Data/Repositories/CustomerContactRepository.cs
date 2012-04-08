using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class CustomerContactRepository : RepositoryBase<MobileContext, CustomerContact> , ICustomerContactRepository
    {
        public CustomerContactRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface ICustomerContactRepository : IRepository<CustomerContact>
    {
    }
    
}
