using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class ErrorRepository : RepositoryBase<MobileContext, Error> , IErrorRepository
    {
        public ErrorRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IErrorRepository : IRepository<Error>
    {
    }
    
}
