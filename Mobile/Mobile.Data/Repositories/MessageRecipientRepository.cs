using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class MessageRecipientRepository : RepositoryBase<MobileContext, MessageRecipient> , IMessageRecipientRepository
    {
        public MessageRecipientRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IMessageRecipientRepository : IRepository<MessageRecipient>
    {
    }
    
}
