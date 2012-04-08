using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class MessageTypeRepository : RepositoryBase<MobileContext, MessageType> , IMessageTypeRepository
    {
        public MessageTypeRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IMessageTypeRepository : IRepository<MessageType>
    {
    }
    
}
