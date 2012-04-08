using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class ProductRepository : RepositoryBase<MobileContext, Product> , IProductRepository
    {
        public ProductRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IProductRepository : IRepository<Product>
    {
    }
    
}
