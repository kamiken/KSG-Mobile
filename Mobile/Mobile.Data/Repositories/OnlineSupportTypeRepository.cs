using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class OnlineSupportTypeRepository : RepositoryBase<MobileContext, OnlineSupportType> , IOnlineSupportTypeRepository
    {
        public OnlineSupportTypeRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IOnlineSupportTypeRepository : IRepository<OnlineSupportType>
    {
    }
    
}
