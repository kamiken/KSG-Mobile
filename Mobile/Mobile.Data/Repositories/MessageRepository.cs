using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class MessageRepository : RepositoryBase<MobileContext, Message> , IMessageRepository
    {
        public MessageRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IMessageRepository : IRepository<Message>
    {
    }
    
}
