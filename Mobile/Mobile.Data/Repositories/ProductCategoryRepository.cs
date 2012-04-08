using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class ProductCategoryRepository : RepositoryBase<MobileContext, ProductCategory> , IProductCategoryRepository
    {
        public ProductCategoryRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
    }
    
}
