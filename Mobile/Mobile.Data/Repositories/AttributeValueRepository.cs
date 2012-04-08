using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class AttributeValueRepository : RepositoryBase<MobileContext, AttributeValue> , IAttributeValueRepository
    {
        public AttributeValueRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IAttributeValueRepository : IRepository<AttributeValue>
    {
    }
    
}
