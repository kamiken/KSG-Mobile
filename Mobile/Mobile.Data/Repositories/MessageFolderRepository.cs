using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class MessageFolderRepository : RepositoryBase<MobileContext, MessageFolder> , IMessageFolderRepository
    {
        public MessageFolderRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IMessageFolderRepository : IRepository<MessageFolder>
    {
    }
    
}
