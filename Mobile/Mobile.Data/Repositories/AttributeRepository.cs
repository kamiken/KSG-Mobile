using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class AttributeRepository : RepositoryBase<MobileContext, Attribute> , IAttributeRepository
    {
        public AttributeRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IAttributeRepository : IRepository<Attribute>
    {
    }
    
}
