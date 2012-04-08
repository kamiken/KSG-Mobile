using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class MediaCategoryRepository : RepositoryBase<MobileContext, MediaCategory> , IMediaCategoryRepository
    {
        public MediaCategoryRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IMediaCategoryRepository : IRepository<MediaCategory>
    {
    }
    
}
