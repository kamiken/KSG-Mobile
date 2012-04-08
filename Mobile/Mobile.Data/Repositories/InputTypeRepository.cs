using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class InputTypeRepository : RepositoryBase<MobileContext, InputType> , IInputTypeRepository
    {
        public InputTypeRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IInputTypeRepository : IRepository<InputType>
    {
    }
    
}
