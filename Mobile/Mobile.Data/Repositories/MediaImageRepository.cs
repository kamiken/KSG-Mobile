using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Framework.Infrastructure;
using Mobile.Object;

namespace Mobile.Data.Repositories
{
    public partial class MediaImageRepository : RepositoryBase<MobileContext, MediaImage> , IMediaImageRepository
    {
        public MediaImageRepository(IDatabaseFactory<MobileContext> databaseFactory) : base(databaseFactory)
    	{
    	
    	}
        
    }
    
    public interface IMediaImageRepository : IRepository<MediaImage>
    {
    }
    
}
