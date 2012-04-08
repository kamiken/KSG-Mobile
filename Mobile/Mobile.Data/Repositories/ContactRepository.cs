using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class ContactRepository : RepositoryBase<MobileContext, Contact> , IContactRepository
    {
        public ContactRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IContactRepository : IRepository<Contact>
    {
    }
    
}
