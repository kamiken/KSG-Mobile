using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class MediaImageCategoryRepository : RepositoryBase<MobileContext, MediaImageCategory> , IMediaImageCategoryRepository
    {
        public MediaImageCategoryRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IMediaImageCategoryRepository : IRepository<MediaImageCategory>
    {
    }
    
}
