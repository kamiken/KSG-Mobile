using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class FriendRequestRepository : RepositoryBase<MobileContext, FriendRequest> , IFriendRequestRepository
    {
        public FriendRequestRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IFriendRequestRepository : IRepository<FriendRequest>
    {
    }
    
}
