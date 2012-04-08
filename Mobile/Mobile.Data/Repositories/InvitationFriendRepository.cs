using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class InvitationFriendRepository : RepositoryBase<MobileContext, InvitationFriend> , IInvitationFriendRepository
    {
        public InvitationFriendRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IInvitationFriendRepository : IRepository<InvitationFriend>
    {
    }
    
}
